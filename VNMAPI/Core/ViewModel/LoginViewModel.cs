using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "LoginID")]
        public string LoginID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 12)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{12,}$\"", ErrorMessage = "Password did not meet requirements as per Company Policy.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [JsonIgnore]
        public string UserName { get; set; }

        [JsonIgnore]
        public string Email { get; set; }

        [JsonIgnore]
        public int UserRoleID { get; set; }

        [JsonIgnore]
        public int EmpID { get; set; }

        [JsonIgnore]
        public DateTime DateOfJoing { get; set; }

    }
}
