using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackCogs.Data.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }

    }
}
