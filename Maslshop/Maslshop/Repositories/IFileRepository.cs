using Maslshop.Models.Core;
using System.Collections.Generic;

namespace Maslshop.Repositories
{
    public interface IFileRepository
    {
        List<File> GetPhotosByProductId(int id);
        void DeleteProductPhotosFromDb(int id);
        void RemoveSelectedPhoto(File photo);
        File SelectPhoto(int id);
    }
}