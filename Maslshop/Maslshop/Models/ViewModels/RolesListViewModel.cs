using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Maslshop.Models.ViewModels
{
    public class RolesListViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }

        public string Heading { get; set; }
    }
}