using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BAL.DTOs.Authentication.Login
{
    public class LoginUser
    {
        [Required(ErrorMessage = "UserName is required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 12)]
        [Display(Name = "Password")]
        public string Password { get; set; }


    }
}
