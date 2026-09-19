using System.ComponentModel.DataAnnotations;

namespace BAL.DTOs.Authentication.Signup
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "UserName is required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(30, MinimumLength = 12)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{12,}$", ErrorMessage = "Password did not meet requirements as per Company Policy.Password should be atleast 1 UPPERCASE, 1 lowercase and 1 special character.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
