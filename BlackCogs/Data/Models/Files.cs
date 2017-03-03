using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackCogs.Data.Models
{
    public class Files
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Path { get; set; }
        //  [DataType(DataType.Upload)]
        // public Byte[] Content { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public virtual FileType FileType { get; set; }

    }
}
