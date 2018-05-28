using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class File

    {
        public int FileId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}