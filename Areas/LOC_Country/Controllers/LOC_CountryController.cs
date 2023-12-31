﻿using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Areas.LOC_Country.Models;

namespace WebApplication2.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region Country_List
        public IActionResult LOC_CountryList()
        {
            string connectionstr = this.Configuration.GetConnectionString("myConnectionString");
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(connectionstr);
            connection.Open();
            SqlCommand objcmd = connection.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_Country_selectall";
            SqlDataReader objSDR = objcmd.ExecuteReader();
            table.Load(objSDR);
            return View("LOC_CountryList", table);
        }
        #endregion
        #region Country_Delete
        public IActionResult LOC_Country_Delete(int CountryId)
        {
            string connstr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connstr);
            connection.Open();
            SqlCommand objcmd = connection.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_Country_DELETEBYPK";
            objcmd.Parameters.AddWithValue("@CountryID", CountryId);
            objcmd.ExecuteNonQuery();

            return RedirectToAction("LOC_CountryList");
        }
        #endregion
        #region Country_Add
        public IActionResult Save(LOC_CountryModel model)
        {
            string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            if (model.CountryID == null)
            {
                objCmd.CommandText = "PR_LOC_Country_InsertData";
            }
            else
            {
                objCmd.CommandText = "PR_LOC_Country_UpdateByPk";
                objCmd.Parameters.AddWithValue("@CountryID", model.CountryID);
            }

            objCmd.Parameters.AddWithValue("@CountryName", model.CountryName);
            objCmd.Parameters.AddWithValue("@CountryCode", model.CountryCode);
            objCmd.ExecuteNonQuery();
            
            return RedirectToAction("LOC_CountryList");
        }
        public IActionResult LOC_CountryAdd(int? CountryID)
        {
            if (CountryID != null)
            {
                string connectionStr = this.Configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();
                SqlCommand objCmd = connection.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_LOC_Country_SelectByPk";
                objCmd.Parameters.AddWithValue("@CountryID", CountryID);
                DataTable dt = new DataTable();
                SqlDataReader objSDR = objCmd.ExecuteReader();

                dt.Load(objSDR);
                LOC_CountryModel model = new LOC_CountryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    model.CountryID = Convert.ToInt32(dr["CountryID"]);
                    model.CountryName = (string)dr["CountryName"];
                    model.CountryCode = (string)dr["CountryCode"];
                }
                return View("LOC_CountryAdd", model);
            }
            else
            {
                return View("LOC_CountryAdd");
            }
        }

        #endregion
    }
}
