using Maslshop.Models.DTOs;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Maslshop.Models.ViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public IEnumerable<UserDTO> User { get; set; }

        public string Id { get; set; }

    }
}