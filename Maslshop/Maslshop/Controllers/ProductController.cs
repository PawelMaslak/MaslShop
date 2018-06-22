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

        public ActionResult ViewProduct(int productId)
        {
            var product = _unitOfWork.Product.GetProductById(productId);

            product.Files = _unitOfWork.File.GetPhotosByProductId(productId);

            var categoryName = _unitOfWork.Category.GetCategoryById(product.CategoryId).Name;

            var viewModel = new ProductsViewModel()
            {
                Heading = product.Name + " - " + categoryName,
                Name = product.Name,
                Manufacturer = product.Manufacturer,
                StockAmount = product.StockAmount,
                Price = product.Price,
                Dimensions = product.Dimensions,
                Year = product.Year,
                Description = product.Description,
                Category = product.CategoryId,
                CategoryName = categoryName,
                Files = product.Files,
                Categories = _unitOfWork.Product.GetCategories()
            };

            return View(viewModel);
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
                Categories = _unitOfWork.Category.GetCategoriesList(),
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
                viewModel.Categories = _unitOfWork.Category.GetCategoriesList();
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

                    return RedirectToAction("Index");
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
            if (query != null)
            {
                var viewModel = new ProductsListViewModel()
                {
                    Heading = "Lista wyszukanych produktów",
                    Categories = _unitOfWork.Product.GetCategories(),
                    Products = _unitOfWork.Product.GetSearchedProducts(query),
                    Files = _unitOfWork.File.GetPhotos(),
                    SearchTerm = query
                };

                return View(viewModel);
            }
            else
            {
                var viewModel = new ProductsListViewModel()
                {
                    Heading = "Lista produktów",
                    Categories = _unitOfWork.Product.GetCategories(),
                    Products = _unitOfWork.Product.GetProducts(),
                    Files = _unitOfWork.File.GetPhotos()
                };

                return View(viewModel);
            }
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
                Categories = _unitOfWork.Category.GetCategoriesList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ProductFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _unitOfWork.Category.GetCategoriesList();
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

            _unitOfWork.Product.AddProduct(product);

            _unitOfWork.Complete();

            var uploads = viewModel.UploadedFiles;

            var productFiles = new List<File>();

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

                        ResizeBigImage(upload.InputStream, Path.Combine(Server.MapPath("~/Content/Images/"), productPhoto.FileName));

                        //var path = Path.Combine(Server.MapPath("~/Content/Images/"), productPhoto.FileName);

                        Resize(100, 100, upload.InputStream, Path.Combine(Server.MapPath("~/Content/Images/Thumbnails/" + productPhoto.FileName)));

                        productFiles.Add(productPhoto);

                        //upload.SaveAs(path);
                    }
                    else if (upload != null && !upload.ContentType.Contains("image"))
                    {
                        viewModel.Categories = _unitOfWork.Category.GetCategoriesList();
                        return View(viewModel);
                    }
                }

                product.Files = productFiles;

                _unitOfWork.Complete();
            }


            return RedirectToAction("Index", "Product");
        }

        public static void ResizeBigImage(Stream streamImg, string saveFilePath)
        {
            Bitmap sourceImage = new Bitmap(streamImg);

            if (sourceImage.Width < 4000 || sourceImage.Height < 4000)
            {
                int maxImageWidth = 1920;

                if (sourceImage.Width > maxImageWidth)
                {
                    int newImageHeight = (int) (sourceImage.Height * (maxImageWidth / (float) sourceImage.Width));

                    using (Bitmap objBitmap = new Bitmap(maxImageWidth, newImageHeight))
                    {
                        objBitmap.SetResolution(maxImageWidth, newImageHeight);
                        using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                        {
                            objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                            objGraphics.InterpolationMode =
                                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            objGraphics.DrawImage(sourceImage, 0, 0, maxImageWidth, newImageHeight);

                            objBitmap.Save(saveFilePath);
                        }
                    }
                }
                else
                {
                    using (Bitmap objBitmap = new Bitmap(sourceImage.Width, sourceImage.Height))
                    {
                        objBitmap.SetResolution(sourceImage.Width, sourceImage.Height);
                        using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                        {
                            objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                            objGraphics.InterpolationMode =
                                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            objGraphics.DrawImage(sourceImage, 0, 0, sourceImage.Width, sourceImage.Height);

                            objBitmap.Save(saveFilePath);
                        }
                    }
                }
            }
        }

        public static void Resize(int width, int height, Stream streamImg, string saveFilePath)
        {
            Bitmap sourceImage = new Bitmap(streamImg);

            using (Bitmap objBitmap = new Bitmap(width, height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, 0, 0, width, height);

                    objBitmap.Save(saveFilePath);
                }
            }
        }
    }
}