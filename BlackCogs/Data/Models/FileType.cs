using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlackCogs.Data.Models
{
    public class FileType
    {
        [System.ComponentModel.DataAnnotations.Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }
    }
}