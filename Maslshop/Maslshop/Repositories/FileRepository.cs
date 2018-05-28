using Maslshop.Models.Core;
using System.Collections.Generic;
using System.Linq;

namespace Maslshop.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;

        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<File> GetPhotosByProductId(int id)
        {
            return _context.Files.Where(i => i.ProductId == id).ToList();
        }

        public void DeleteProductPhotosFromDb(int id)
        {
            _context.Files.RemoveRange(GetPhotosByProductId(id));
        }

        public void RemoveSelectedPhoto(File photo)
        {
            _context.Files.Remove(photo);
        }

        public File SelectPhoto(int id)
        {
            return _context.Files.Single(i => i.FileId == id);
        }
    }
}