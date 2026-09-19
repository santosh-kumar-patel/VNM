using DAL.Interface.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Entities.Base
{
    //public class Entity : IEntity
    //{
    //    [Key]
    //    [JsonIgnore]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int ID { get; set; }

    //    [Required]
    //    [JsonIgnore]
    //    [Display(Name = "Status")]
    //    public bool Status { get; set; } = true;

    //    [Required]
    //    [JsonIgnore]
    //    [Display(Name = "CreatedOn")]
    //    public DateTime CreatedOn { get; set; } = DateTime.Now;

    //    [Required]
    //    [JsonIgnore]
    //    [Display(Name = "CreatedBy")]
    //    public int CreatedBy { get; set; }

    //    [JsonIgnore]
    //    [Display(Name = "UpdatedOn")]
    //    public DateTime UpdatedOn { get; set; }

    //    [JsonIgnore]
    //    [Display(Name = "UpdatedBy")]
    //    public int UpdatedBy { get; set; }
    //}

    public class Entity : IEntity
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

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
        public int CreatedBy { get; set; }

        [JsonIgnore]
        [Display(Name = "UpdatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonIgnore]
        [Display(Name = "UpdatedBy")]
        public int UpdatedBy { get; set; }
    }
}
