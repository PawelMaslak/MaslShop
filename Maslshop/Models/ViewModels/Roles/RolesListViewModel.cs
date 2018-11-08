using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Maslshop.Models.ViewModels.Roles
{
    public class RolesListViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }

        public string Heading { get; set; }
    }
}