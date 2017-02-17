using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models.RoleViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public RoleController (ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RoleVM roleVM)
        {
            var newRole = new IdentityRole();
            newRole.Id = roleVM.RoleName;
            newRole.Name = roleVM.RoleName;
            newRole.NormalizedName = roleVM.RoleName;
            if (!_roleManager.RoleExistsAsync(newRole.Name).Result)
            {
                IdentityResult result = _roleManager.CreateAsync(newRole).Result;
                if (!result.Succeeded)
                {
                    ViewBag.Message = "Error, failed at adding";
                } else
                {
                    ViewBag.Message = "Role successfully added.";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddUserToRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUserToRole(UserRoleVM userRoleVM)
        {
            if (ModelState.IsValid) {
                using (_context)
                    try
                    {
                        var user = _context.Users.Where(u => u.UserName == userRoleVM.UserName).FirstOrDefault();
                        var role = _context.Roles.Where(r => r.Name == userRoleVM.RoleName).FirstOrDefault();
                        var userRole = new IdentityUserRole<string>();
                        userRole.RoleId = role.Id;
                        userRole.UserId = user.Id;
                        _context.UserRoles.Add(userRole);
                        _context.SaveChanges();
                        ViewBag.Message = "User Successfully Added to Role";
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        ViewBag.Message = "Something went wrong. Please try again.";
                    }
                }
            return View();
        }
    }
}