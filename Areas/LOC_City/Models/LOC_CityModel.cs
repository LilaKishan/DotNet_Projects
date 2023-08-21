namespace WebApplication2.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string? Citycode { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime Modified { get; set; }
    }
    public class LOC_CityDropDown
    {
        public int CityId { get; set;}
        public string CityName { get; set;}
    }
}
