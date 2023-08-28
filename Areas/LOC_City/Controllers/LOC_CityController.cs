using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using WebApplication2.Areas.LOC_City.Models;
using WebApplication2.Areas.LOC_Country.Models;
using WebApplication2.Areas.LOC_State.Models;

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

        #region City_List
        public IActionResult City_List()
        {
            string connectionstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_selectall";
            DataTable table = new DataTable();
            SqlDataReader ObjSDR = cmd.ExecuteReader();
            table.Load(ObjSDR);
            return View(table);
        }
        #endregion

        #region City_Delete
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
        #endregion

        #region Add_city
        public IActionResult Save(LOC_CityModel model)
        {
            string connstr = this.configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            if (model.CityId == null)
            {
                cmd.CommandText = "PR_LOC_CITY_INSERTData";
            }
            else
            {
                cmd.CommandText = "PR_LOC_CITY_UPDATEBYPK";
                cmd.Parameters.AddWithValue("@CityId", model.CityId);
            }
            cmd.Parameters.AddWithValue("@CityName", model.CityName);
            cmd.Parameters.AddWithValue("@Citycode", model.Citycode);
            cmd.Parameters.AddWithValue("@CountryId", model.CountryId);
            cmd.Parameters.AddWithValue("@StateID", model.StateId);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("City_List");
        }
        public IActionResult Add_City(int? CityId)
        {
            string connstr = this.configuration.GetConnectionString("myConnectionString");

            #region country_dropdown
            SqlConnection sqlConnection = new SqlConnection(connstr);
            sqlConnection.Open();
            SqlCommand cmd1 = sqlConnection.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_COUNTRY_COMBOBOX";
            SqlDataReader reader = cmd1.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            sqlConnection.Close();

            List<LOC_CountryModel> list = new List<LOC_CountryModel>();

            foreach (DataRow dr in dt.Rows)
            {
                LOC_CountryModel countryModel = new LOC_CountryModel();
                countryModel.CountryID = Convert.ToInt32(dr["CountryId"]);
                countryModel.CountryName = dr["CountryName"].ToString();
                list.Add(countryModel);
            }
            ViewBag.CountryList = list;
            #endregion

            #region State_dropdown
            SqlConnection connection2 = new SqlConnection(connstr);
            connection2.Open();
            SqlCommand objCmd2 = connection2.CreateCommand();
            objCmd2.CommandType = CommandType.StoredProcedure;
            objCmd2.CommandText = "PR_LOC_State_ComboBox";
            SqlDataReader reader2 = objCmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(reader2);
            connection2.Close();

            List<LOC_StateModel> list2 = new List<LOC_StateModel>();

            foreach (DataRow dr in dt2.Rows)
            {
                LOC_StateModel stateModel = new LOC_StateModel();
                stateModel.StateID = Convert.ToInt32(dr["StateId"]);
                stateModel.StateName = dr["StateName"].ToString();
                list2.Add(stateModel);
            }
            ViewBag.StateList = list2;

            #endregion

            if (CityId != null)
            {
                SqlConnection connection = new SqlConnection(connstr);
                connection.Open();
                SqlCommand objCmd = connection.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_CITY_SELECTBYPK";
                objCmd.Parameters.AddWithValue("@CityId", CityId);
                DataTable dt1 = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();

                dt1.Load(objSDR);
                LOC_CityModel model = new LOC_CityModel();

                foreach (DataRow dr in dt1.Rows)
                {
                    model.CityId = Convert.ToInt32(dr["CityID"]);
                    model.CityName = (string)dr["CityName"];
                    model.Citycode = (string)dr["Citycode"];
                    model.CountryId = Convert.ToInt32(dr["CountryId"]);
                    model.CountryName = (string)dr["CountryName"];
                    model.StateId = Convert.ToInt32(dr["StateID"]);
                    model.StateName = (string)dr["StateName"];
                }
                return View("Add_City", model);
            }
            else
            {
                return View("Add_City");
            }

        }
        #endregion
    }
}
