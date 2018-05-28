using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamietac ta przeglądarkę?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}