using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlackCogs.Data;
using BlackCogs.Data.Models;

namespace BlackCogs.Managers
{
    public class FeatureManager
    {
        Context db = new Context();
        public List<Feature> GetAllFeatures()
        {
            try
            {
                //  List<Feature> ap = null;


                return db.Features.ToList();


            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;

            }
        }
        public  Boolean FeatureExists(string name)
        {
            try
            {
                Boolean ap = false;
                if ( CommonTools.isEmpty(name)==false)
                {
                    List<Feature> fs = this.GetAllFeatures();
                     if ( fs !=null)
                    {
                        var f=fs.FindAll(x => x.Name == name).ToList();
                        if ( f !=null)
                        {
                            ap = true;
                        }

                    }
                }



                return ap;

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return false;

            }
        }
         public void AddFeature(string name, IController contr,Boolean disabled=false)
        {
            try
            {
                Feature modl = new Feature();
                if ( CommonTools.isEmpty(name)==false && contr !=null && this.FeatureExists(name)==false)
                {
                    modl.Controller = contr;
                    modl.Disabled = disabled;
                    modl.Name = name;
                    this.db.Features.Add(modl);
                    this.db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);

               
            }
        }
        public Feature GetFeatureByName(string name)
        {
            try
            {
                Feature ap = null; ;
                if (CommonTools.isEmpty(name) == false)
                {
                    List<Feature> fs = this.GetAllFeatures();
                    if (fs != null)
                    {
                        ap = fs.FirstOrDefault(x => x.Name == name);
                    }
                }



                return ap;

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;

            }
        }
        public void RemoveFeatureByName(string name)
        {
            try
            {
               
                if (CommonTools.isEmpty(name) == false)
                {
                    Feature  fs = this.GetFeatureByName(name);
                    if (fs != null)
                    {
                        this.db.Features.Remove(fs);
                        this.db.SaveChanges();
                    }
                }



               

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
               

            }
        }
    }
}
