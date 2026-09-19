using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class RoleViewModel
    {
        [Required (ErrorMessage ="Role Name is required")]
        public string RoleName { get; set; }

        public string RoleDescription { get; set; }
    }
}
