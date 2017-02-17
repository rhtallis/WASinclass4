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

        public RoleController (ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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
    }
}