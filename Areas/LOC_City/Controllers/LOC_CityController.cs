using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class LOC_CityController : Controller
    {

        private IConfiguration configuration;
        public LOC_CityController(IConfiguration _counfiguration)
        { 
            configuration = _counfiguration;
        }
        public IActionResult City_List()
        {
            string connectionstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn=new SqlConnection(connectionstr);
            conn.Open();   
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_selectall";
            DataTable table = new DataTable();
            SqlDataReader ObjSDR= cmd.ExecuteReader();
            table.Load(ObjSDR);
            return View(table); 
        }
        public IActionResult City_Delete(int CityId)
        {
            string connectionstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_deletebypk";
            cmd.Parameters.AddWithValue("@CityId", CityId);
            cmd.ExecuteNonQuery();
            return RedirectToAction("City_List");
        }
        public IActionResult Add_City() { return View(); }
    }
}
