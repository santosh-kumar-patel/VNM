using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.ViewModel
{
    public class EmployeeViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [Phone]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Address")]
        public int Address { get; set; }

        [Required]
        [Display(Name = "DateOfJoing")]
        public DateTime DateOfJoing { get; set; }

        [Required]
        [Display(Name = "DOB")]
        public DateTime DOB { get; set; }
    }
}
