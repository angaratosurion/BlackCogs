using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using BlackCogs.Data.Models;
using BlackCogs.Data.Models;
namespace BlackCogs.Data.ViewModels
{
    public class ViewDeleteUserFromRole
    {
        public IdentityRole Role { get; set; }
        public ApplicationUser UserToDelete { get; set; }
    }
}
