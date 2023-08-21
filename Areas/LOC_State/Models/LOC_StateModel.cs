using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Areas.LOC_State.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }
        [Required]
        public string? StateName { get; set; }
        [Required]
        public string? StateCode { get; set; }
        public string? CountryName { get; set; }
        [Required]
        public int? CountryID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
