using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlackCogs.Data
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context()
            : base("DefaultConnection")
        {

        }
        public static Context Create()
        {
            return new Context();
        }
        public IDbSet<Feature> Features { get; set; }
        public IDbSet<GeneralSettings> Settings { get; set; }
        public GeneralSettings GeneralSettings
        {
            get
            {
                return this.Settings.FirstOrDefault();
            }
        }
        //
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}
