using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models.RoleViewModels
{
    public class RoleVM
    {
        
            [Required]
            [Display(Name = "User Role")]
            public string RoleName { get; set; }
        
    }
}
