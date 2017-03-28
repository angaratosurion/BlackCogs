using BlackCogs.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackCogs.Data.ViewModels
{
    public  class ViewFiles
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

        public  ApplicationUser Owner { get; set; }

        public  FileType FileType { get; set; }


        public void ImportFromModel(Files md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.Owner) == false)
                {
                    ApplicationUser user = CommonTools.Blusrmng.GetUserbyID(md.Owner);
                    if (user != null)
                    {
                        // this.id = md.id;
                        this.Id = md.Id;
                        this.FileName = md.FileName;
                        this.Path = md.Path;
                        this.FileType= md.FileType;
                       
                        this.RowVersion = md.RowVersion;
                        this.Owner = user;



                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public Files ExportToModel()
        {
            try
            {
                Files ap = new Files();

                // ap.Categories = Categories;
               ap.Id = Id;
                ap.FileName =FileName;
                ap.Path = Path;
                ap.FileType = FileType;

                this.RowVersion = RowVersion;
                
                if (Owner != null)
                {
                    ap.Owner = Owner.Id;
                }

               



                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;

            }
        }
    }
}
