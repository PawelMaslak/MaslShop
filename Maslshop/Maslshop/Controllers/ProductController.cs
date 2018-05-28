using Maslshop.Models.Core;
using Maslshop.Models.ViewModels;
using Maslshop.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using File = Maslshop.Models.Core.File;

namespace Maslshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Edit(int productId)
        {
            var product = _unitOfWork.Product.GetProductById(productId);

            product.Files = _unitOfWork.File.GetPhotosByProductId(productId);

            var viewModel = new ProductFormViewModel()
            {
                Heading = "Edytuj produkt",
                Name = product.Name,
                Id = product.Id,
                Description = product.Description,
                StockAmount = product.StockAmount,
                Price = product.Price,
                Year = product.Year,
                Dimensions = product.Dimensions,
                Manufacturer = product.Manufacturer,
                Category = product.CategoryId,
                Categories = _unitOfWork.Product.GetCategories(),
                Products = product.Files
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _unitOfWork.Product.GetCategories();
                return View(viewModel);
            }

            var product = _unitOfWork.Product.GetProductById(viewModel.Id);

            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.CategoryId = viewModel.Category;
            product.StockAmount = viewModel.StockAmount;
            product.Price = viewModel.Price;
            product.Year = viewModel.Year;
            product.Dimensions = viewModel.Dimensions;
            product.Manufacturer = viewModel.Manufacturer;

            var uploads = viewModel.UploadedFiles;

            if (uploads != null)
            {
                foreach (var upload in uploads)
                {
                    if (upload != null && upload.ContentType.Contains("image"))
                    {
                        var productPhoto = new File
                        {
                            FileName = Path.GetFileName(upload.FileName),
                            ProductId = product.Id
                        };

                        var path = Path.Combine(Server.MapPath("~/Content/Images/"), productPhoto.FileName);

                        Resize(100, 100, upload.InputStream, Path.Combine(Server.MapPath("~/Content/Images/Thumbnails/" + productPhoto.FileName)));

                        product.Files.Add(productPhoto);

                        upload.SaveAs(path);
                    }
                    else if (upload != null && !upload.ContentType.Contains("image"))
                    {
                        viewModel.Categories = _unitOfWork.Product.GetCategories();
                        return View(viewModel);
                    }
                }

                if (product.Files.Count != 0)
                {
                    _unitOfWork.Complete();

                    return RedirectToAction("Index", "Home");
                }
            }

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SearchProduct(ProductsViewModel viewModel)
        {
            return RedirectToAction("Index", new { query = viewModel.SearchTerm });
        }

        public ActionResult Index(string query = null)
        {
            var viewModel = new ProductsViewModel()
            {
                Heading = "Lista produktów",
                Categories = _unitOfWork.Product.GetCategories(),
                Products = _unitOfWork.Product.GetSearchedProducts(query),
                SearchTerm = query
            };

            return View(viewModel);
        }

        public ActionResult DeletePhoto(int photoId)
        {
            var photo = _unitOfWork.File.SelectPhoto(photoId);

            var product = _unitOfWork.Product.SelectProductMatchingPhotoId(photo);

            _unitOfWork.File.RemoveSelectedPhoto(photo);

            System.IO.File.Delete(Request.MapPath("~/Content/Images/") + photo.FileName);
            System.IO.File.Delete(Request.MapPath("~/Content/Images/Thumbnails/") + photo.FileName);

            _unitOfWork.Complete();

            return RedirectToAction("Edit", new { productId = product.Id });
        }

        public ActionResult Delete(int productId)
        {
            _unitOfWork.Product.DeleteProduct(productId);

            _unitOfWork.File.DeleteProductPhotosFromDb(productId);

            foreach (var file in _unitOfWork.File.GetPhotosByProductId(productId))
            {
                System.IO.File.Delete(Request.MapPath("~/Content/Images/") + file.FileName);
                System.IO.File.Delete(Request.MapPath("~/Content/Images/Thumbnails/") + file.FileName);
            }

            _unitOfWork.Complete();

            return RedirectToAction("Index", "Product");
        }

        public ActionResult Add()
        {
            var viewModel = new ProductFormViewModel()
            {
                Heading = "Dodaj produkt",
                Categories = _unitOfWork.Product.GetCategories()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _unitOfWork.Product.GetCategories();
                return View(viewModel);
            }

            var product = new Product()
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                Description = viewModel.Description,
                CategoryId = viewModel.Category,
                StockAmount = viewModel.StockAmount,
                Year = viewModel.Year,
                Dimensions = viewModel.Dimensions,
                Manufacturer = viewModel.Manufacturer,
                AddedDate = DateTime.Now
            };

            var uploads = viewModel.UploadedFiles;

            product.Files = new List<File>();

            if (uploads != null)
            {
                foreach (var upload in uploads)
                {
                    if (upload != null && upload.ContentType.Contains("image"))
                    {
                        var productPhoto = new File
                        {
                            FileName = Path.GetFileName(upload.FileName),
                            ProductId = product.Id
                        };

                        var path = Path.Combine(Server.MapPath("~/Content/Images/"), productPhoto.FileName);

                        Resize(100, 100, upload.InputStream, Path.Combine(Server.MapPath("~/Content/Images/Thumbnails/" + productPhoto.FileName)));

                        product.Files.Add(productPhoto);

                        upload.SaveAs(path);
                    }
                    else if (upload != null && !upload.ContentType.Contains("image"))
                    {
                        viewModel.Categories = _unitOfWork.Product.GetCategories();
                        return View(viewModel);
                    }
                }
                if (product.Files.Count != 0)
                {
                    _unitOfWork.Product.AddProduct(product);

                    _unitOfWork.Complete();

                    return RedirectToAction("Index");
                }
            }



            if (product.Files.Count == 0)
            {
                _unitOfWork.Product.AddProduct(product);

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public static void Resize(int Width, int Height, Stream streamImg, string saveFilePath)
        {
            Bitmap sourceImage = new Bitmap(streamImg);
            using (Bitmap objBitmap = new Bitmap(Width, Height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                    objBitmap.Save(saveFilePath);
                }
            }
        }
    }
}