using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Areas.LOC_Country.Models;
using WebApplication2.Areas.LOC_State.Models;


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
        #region LOC_StateList
        public IActionResult LOC_StateList()
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
        #endregion
        #region LOC_StateDelete
        public IActionResult LOC_StateDelete(int StateID) {
            string connctionstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connctionstr);
            conn.Open();

            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_State_DELETEBYPK";
            objcmd.Parameters.AddWithValue("@StateID",StateID);
            objcmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("LOC_StateList");
        }
        #endregion
        #region State_Add/update
        public IActionResult Save(LOC_StateModel model) 
        {
            string connstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType= CommandType.StoredProcedure;

            if (model.StateID == null)
            {
                objcmd.CommandText = "PR_LOC_State_InsertDATA";

            }
            else
            {
                objcmd.CommandText = "PR_LOC_State_updatebypk";
                objcmd.Parameters.AddWithValue("@StateID",model.StateID);
            }

            objcmd.Parameters.AddWithValue("@StateName", model.StateName);
            objcmd.Parameters.AddWithValue("@StateCode", model.StateCode);
            objcmd.Parameters.AddWithValue("@CountryID", model.CountryID);
            objcmd.ExecuteNonQuery();
            conn.Close();

            return RedirectToAction("LOC_StateList");
        }
        public IActionResult LOC_StateAdd(int? StateID) {

            string connstr = this.configuration.GetConnectionString("myConnectionString");

        #region Country_DropDown

            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();

            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_Country_ComboBox";
            SqlDataReader reader1 = objcmd.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(reader1);
            conn.Close();

            List<LOC_CountryModel> list = new List<LOC_CountryModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryModel countryModel = new LOC_CountryModel();
                countryModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryList = list;

        #endregion

            if (StateID != null)
            {
                SqlConnection connection = new SqlConnection(connstr);
                connection.Open();
                SqlCommand objCmd = connection.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_State_SelectByPk";
                objCmd.Parameters.AddWithValue("@StateID", StateID);
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();

                dt.Load(objSDR);
                LOC_StateModel model = new LOC_StateModel();
                foreach (DataRow dr in dt.Rows)
                {
                    model.StateID = Convert.ToInt32(dr["StateID"]);
                    model.StateName = (string)dr["StateName"];
                    model.StateCode = (string)dr["StateCode"];
                    model.CountryID = Convert.ToInt32(dr["CountryID"]);
                    model.CountryName = (string)dr["CountryName"];
                }
                return View("LOC_StateAdd", model);
            }
            else
            {
                return View("LOC_StateAdd");
            }
            
        }
        #endregion
    }
}
