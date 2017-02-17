using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models.RoleViewModels;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext _db;

        public RolesController(ApplicationDbContext db) { _db = db; }
        [Authorize]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                // *** New: Connect to AspNetRole using code first.
                using (_db)
                {
                    
                    var role = new IdentityRole();
                    role.Id = roleVM.RoleName;
                    role.Name = roleVM.RoleName;
                    _db.Roles.Add(role);
                 
                    _db.SaveChanges();
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddUserToRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUserToRole(UserRoleVM userRoleVM)
        {
            if (ModelState.IsValid)
            {
                using (_db)
                {
                    var user = _db.Users
                                        .Where(u => u.UserName == userRoleVM.UserName)
                                        .FirstOrDefault();
                    var role = _db.Roles
                                        .Where(r => r.Name == userRoleVM.RoleName).FirstOrDefault();

                    var userRole = new IdentityUserRole<string>();
                    userRole.RoleId = role.Id;
                    userRole.UserId = user.Id;

                    _db.UserRoles.Add(userRole);
                    _db.SaveChanges();
                }
            }
            return View();
        }

    }
}