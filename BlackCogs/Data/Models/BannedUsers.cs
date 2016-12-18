using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackCogs.Data.Models
{
   public class BannedUsers
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public virtual ApplicationUser BannedBy{ get; set; }

    }
}
