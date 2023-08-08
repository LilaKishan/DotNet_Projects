using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration) {
            Configuration = _configuration;
        }
        
        public IActionResult LOC_CountryList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable table = new DataTable();
            SqlConnection connection=new SqlConnection(connectionstr);
            connection.Open();
            SqlCommand objcmd = connection.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_Country_selectall";
            SqlDataReader objSDR = objcmd.ExecuteReader();
            table.Load(objSDR);
            return View("LOC_CountryList",table);
        }
      
        public IActionResult Add_Country() { return View(); }
    }
}
