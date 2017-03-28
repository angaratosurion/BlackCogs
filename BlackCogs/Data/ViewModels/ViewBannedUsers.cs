using BlackCogs.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackCogs.Data.ViewModels
{
    public class ViewBannedUsers
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public  ApplicationUser BannedBy { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }


        public void ImportFromModel(BannedUsers md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.BannedBy) == false)
                {
                    ApplicationUser user = CommonTools.Blusrmng.GetUserbyID(md.BannedBy);
                    if (user != null)
                    {
                        // this.id = md.id;
                        this.Id = md.Id;
                        this.DateTime = md.DateTime;

                        RowVersion = md.RowVersion;
                        this.BannedBy = user;



                    }
                    user = CommonTools.Blusrmng.GetUserbyID(md.User);
                    if (user != null)
                    {
                        this.User = user;
                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public BannedUsers ExportToModel()
        {
            try
            {
                BannedUsers ap = new BannedUsers();

                // ap.Categories = Categories;

               
                    // this.id = md.id;
                    ap.Id = Id;
                    ap.DateTime = DateTime;

                    ap.RowVersion = RowVersion;
                    if (BannedBy != null)
                    {
                    ap.BannedBy = BannedBy.Id;
                    }
                    if (User != null)
                    {
                    ap.User = User.Id ;
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
