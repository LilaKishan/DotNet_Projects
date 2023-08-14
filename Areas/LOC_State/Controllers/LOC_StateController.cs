using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        private IConfiguration configuration;

        public LOC_StateController(IConfiguration _configuration) 
        {
            configuration= _configuration;
        }
        public IActionResult State_List()
        {
            string connctionstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn=new SqlConnection(connctionstr);
            conn.Open();

            SqlCommand objcmd= conn.CreateCommand();
            objcmd.CommandType=CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_State_selectall";
            DataTable dt=new DataTable();
            SqlDataReader reader=objcmd.ExecuteReader();
            dt.Load(reader);
            return View(dt); 
        }
        public IActionResult State_Delete(int StateID) {
            string connctionstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connctionstr);
            conn.Open();

            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_State_DELETEBYPK";
            objcmd.Parameters.AddWithValue("@StateID",StateID);
            objcmd.ExecuteNonQuery();
            return RedirectToAction("State_List");
        }
        public IActionResult Add_State() { 
            return View();
        }
    }
}
