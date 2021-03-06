﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Application;
using BlackCogs.Data;
using BlackCogs.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlackCogs.Managers
{
   public class BlackCogsUserManager
    {
         Context db = new Context();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public BlackCogsUserManager(ApplicationUserManager usrmngr, ApplicationSignInManager singmngr)
        {
            this._userManager = usrmngr;
            this._signInManager = singmngr;
        }
        public BlackCogsUserManager()
        {
           
        }
        
        public Context Context { get { return db; } set { db = value; } }
        //WikiManager wkmngr = CommonTools.wkmngr;
        public static string AdminRoles = "Administrators";
        #region User
        public ApplicationUser GetUser(string id)
        {
            try
            {
                ApplicationUser ap = null;
                if (id != null)
                {
                    ap = this.db.Users.First(u => u.UserName == id);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public ApplicationUser GetUserbyID(string id)
        {
            try
            {
                ApplicationUser ap = null;
                if (id != null)
                {
                    ap = this.db.Users.First(u => u.Id== id);
                }
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void EditUser(string username, ApplicationUser user)
        {
            try
            {
                if (CommonTools.isEmpty(username) == false && user != null &&
                    this.UserExists(user.UserName))
                {
                    db.Entry(this.GetUser(username)).CurrentValues.SetValues(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public Boolean UserExists(string id)
        {
            try
            {
                Boolean ap = false;
                if (id != null)
                {
                    ApplicationUser us = this.GetUser(id);
                    if (us != null)
                    {
                        ap = true;
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

        public List<ApplicationUser> GetUsers()
        {
            try
            {
                return this.db.Users.ToList();

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void DeleteUser(string username)
        {
            try
            {
                if (CommonTools.isEmpty(username) == false && this.UserExists(username)==true)
                {
                    ApplicationUser user = this.GetUser(username);
                    List<IdentityRole> roles=this.GetRolesOfUser(username);
                    if ( roles!=null)
                    {
                       foreach(var r in roles)
                        {
                            this.RemoveUserFromRole(r.Name, username);

                        }
                    }
                    if (user != null)//&& this._userManager!=null)
                    {
                        
                        this.db.Users.Remove(user);
                        this.db.SaveChanges();

                    }
                   
                  
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }


        #endregion
        #region roles
        public Boolean RoleExists(string Name)
        {
            try
            {
                Boolean ap = false;
                if (Name != null)
                {
                    IdentityRole rol = this.GetRole(Name);
                    if (rol != null)
                    {
                        ap = true;
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
        public IdentityRole GetRole(string Name)
        {
            try
            {
                IdentityRole ap = null;
                if (Name != null)
                {
                    List<IdentityRole> rols = this.GetRoles();

                    if (rols != null)
                    {
                        ap = rols.FirstOrDefault(r => r.Name == Name);
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
        public List<IdentityRole> GetRolesOfUser(string UserName)
        {
            try
            {
                List<IdentityRole> ap = null;
                if (UserName != null && this.UserExists(UserName))
                {
                    ApplicationUser usr = this.GetUser(UserName);
                    if (usr != null && usr.Roles != null)
                    {
                        ap = new List<IdentityRole>();
                        foreach (IdentityUserRole rl in usr.Roles)
                        {
                            IdentityRole r = this.db.Roles.FirstOrDefault(x => x.Id == rl.RoleId);
                            ap.Add(r);
                        }

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
        public List<IdentityRole> GetRoles()
        {
            try
            {
                List<IdentityRole> ap = this.db.Roles.ToList();
                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void CreateNewRole(IdentityRole role)
        {
            try
            {
                if (role != null && this.RoleExists(role.Name) == false)
                {
                    this.db.Roles.Add(role);
                    this.db.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }
        public void EditRole(string rolename, IdentityRole role)
        {
            try
            {
                if (CommonTools.isEmpty(rolename) == false &&
                    role != null && this.RoleExists(role.Name) == false &&
                    this.RoleExists(rolename))
                {
                    IdentityRole or = this.GetRole(rolename);
                    if (or != null && or.Name != "Administrators"
                        && rolename != "Administrators")
                    {
                        this.db.Entry(or).CurrentValues.SetValues(role);
                        this.db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }
        public void DeleteRole(string rolename)
        {
            try
            {
                if (CommonTools.isEmpty(rolename) == false)
                {
                    IdentityRole or = this.GetRole(rolename);
                    if (or != null && rolename != "Administrators")
                    {
                        this.db.Roles.Remove(or);
                        this.db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }

        public List<ApplicationUser> GetUsersofRole(string Name)
        {
            try
            {
                List<ApplicationUser> ap = null;
                if (Name != null && this.RoleExists(Name))
                {
                    IdentityRole rol = this.GetRole(Name);
                    if (rol != null && rol.Users != null && rol.Users.Count > 0)
                    {
                        ap = new List<ApplicationUser>();
                        foreach (var u in rol.Users)
                        {
                            ApplicationUser t = this.db.Users.FirstOrDefault(x => x.Id == u.UserId);
                            ap.Add(t);
                        }
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
        public void AddUserToRole(string rolename, string username)
        {
            try
            {
                if (CommonTools.isEmpty(rolename) == false
                    && CommonTools.isEmpty(username) == false &&
                    this.RoleExists(rolename) && this.UserExists(username) == true)
                // && rolemngr !=null)

                {
                    IdentityRole or = this.GetRole(rolename);
                    ApplicationUser user = this.GetUser(username);
                    if (this.UserExistsInRole(rolename, username) == false)
                    {
                        IdentityUserRole r = new IdentityUserRole();
                        r.RoleId = or.Id;
                        r.UserId = user.Id;
                        if (or.Users != null)
                        {
                            or.Users.Add(r);
                            this.db.Entry(or).CurrentValues.SetValues(or);
                            this.db.SaveChanges();

                        }

                    }
                }

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }
        public void RemoveUserFromRole(string rolename, string username)
        {
            try
            {
                if (CommonTools.isEmpty(rolename) == false
                    && CommonTools.isEmpty(username) == false &&
                    this.RoleExists(rolename) && this.UserExists(username) == true)
                {
                    IdentityRole or = this.GetRole(rolename);
                    ApplicationUser user = this.GetUser(username);
                    if (this.UserExistsInRole(rolename, username) != false)
                    {
                        IdentityUserRole r = new IdentityUserRole();
                        r.RoleId = or.Id;
                        r.UserId = user.Id;
                        if (or.Users != null)
                        {
                            or.Users.Remove(r);
                            user.Roles.Remove(r);
                            this.db.Entry(GetRole(rolename)).CurrentValues.SetValues(or);
                            this.db.Entry(user).CurrentValues.SetValues(user);


                            this.db.SaveChanges();
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                // return null;
            }
        }
        public Boolean UserExistsInRole(string rolename, string username)
        {
            try
            {
                Boolean ap = false;
                if (CommonTools.isEmpty(rolename) == false
                     && CommonTools.isEmpty(username) == false &&
                     this.RoleExists(rolename) && this.UserExists(username) == true)
                {
                    ApplicationUser us = this.GetUser(username);
                    IdentityRole or = this.GetRole(rolename);
                    IdentityUserRole r = or.Users.FirstOrDefault(x => x.UserId == us.Id && x.RoleId == or.Id);
                    IdentityRole r1 = null;
                    List<IdentityRole> rls = this.GetRolesOfUser(username);
                    if (rls != null)
                    {
                        r1 = rls.FirstOrDefault(x => x.Name == rolename);
                    }
                    if (r != null && r1 != null)
                    {
                        ap = true;
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

        #endregion
    }
}
