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
        public string User { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string BannedBy{ get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }

    }
}
