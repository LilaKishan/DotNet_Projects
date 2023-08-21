using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID{ get; set; }
        [Required]
        public string? CountryName { get; set; }
        [Required]
        public string? CountryCode { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
    public class Loc_CountryDropDown
    {
        public int? CountryID { get; set; }
        public string? CountryName { get; set; }
    }
}
