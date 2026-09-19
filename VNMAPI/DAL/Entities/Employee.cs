using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
    [Table("Employee", Schema = "dbo")]
    public class Employee : IdentityUser
    {

        [Required]
        [Display(Name = "Address")]
        public int Address { get; set; }

        [Required]
        [Display(Name = "DateOfJoing")]
        public DateTime DateOfJoing { get; set; }

        [Required]
        [Display(Name = "DOB")]
        public DateTime DOB { get; set; }

        [Required]
        [JsonIgnore]
        [Display(Name = "Status")]
        public bool Status { get; set; } = true;

        [Required]
        [JsonIgnore]
        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        [JsonIgnore]
        [Display(Name = "CreatedBy")]
        public string? CreatedBy { get; set; }

        [JsonIgnore]
        [Display(Name = "UpdatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonIgnore]
        [Display(Name = "UpdatedBy")]
        public string? UpdatedBy { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        [JsonIgnore]
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
