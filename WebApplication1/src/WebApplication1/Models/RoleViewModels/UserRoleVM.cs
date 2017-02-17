using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.RoleViewModels
{
    public class UserRoleVM
    {
        [Required]
        [Display(Name = "Login Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
