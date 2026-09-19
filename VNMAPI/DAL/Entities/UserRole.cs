using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Entities
{

    [Table("UserRole", Schema = "dbo")]
    public class UserRole : IdentityRole
    {
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

    }
}
