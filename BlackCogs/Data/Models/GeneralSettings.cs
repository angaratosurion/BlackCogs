using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackCogs.Data.Models
{
    public class GeneralSettings
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Web Site Name")]
        public string WebSiteName { get; set; }
        public int ItemsPerPage { get; set; }
        public Boolean FeatureManagment { get; set; }
    }
}
