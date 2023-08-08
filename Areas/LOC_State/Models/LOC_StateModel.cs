namespace WebApplication2.Areas.LOC_State.Models
{
    public class LOC_StateModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string CountryName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
