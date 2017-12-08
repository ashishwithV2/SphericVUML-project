using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IndependentPlannerML;
using System.Data;
using System.Net.Http;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Configuration;
namespace IndependentPlannerUnitTesting
{
    [TestClass]
    public class IndependentPlannerUnitTest
    {
      [TestMethod]
       public void run()
        {
            // defined system object
            DataTable DtExpected = new DataTable();
            DataSet DsActual = new DataSet();
            IndependentPlannerML.GeneratePCTsWithResult obj = new IndependentPlannerML.GeneratePCTsWithResult();
            DataSet DSS = new DataSet();
            DSS = RetriveInputRecords();
            for (int i = 0; i < DSS.Tables[0].Rows.Count; i++)
            {
                //NUnitOutput(Convert.ToString(DSS.Tables[0].Rows[i]["Studio"]),
                //            Convert.ToString(DSS.Tables[0].Rows[i]["Release"]),
                //            Convert.ToString(DSS.Tables[0].Rows[i]["HDSD"]),
                //            Convert.ToString(DSS.Tables[0].Rows[i]["Territories"]),
                //            Convert.ToString(DSS.Tables[0].Rows[i]["Hotel"]),
                //            Convert.ToString(DSS.Tables[0].Rows[i]["StudioType"])
                //            );


                DtExpected = obj.GeneratePCTSForMLWithParameter(Convert.ToString(DSS.Tables[0].Rows[i]["Studio"]), Convert.ToString(DSS.Tables[0].Rows[i]["Release"]), Convert.ToString(DSS.Tables[0].Rows[i]["HDSD"]), Convert.ToString(DSS.Tables[0].Rows[i]["Territories"]), Convert.ToString(DSS.Tables[0].Rows[i]["Hotel"]), Convert.ToString(DSS.Tables[0].Rows[i]["StudioType"]));
                //Actual OutPut
                DsActual = obj.LocalDatabaseUnqiePCTswithParameter(Convert.ToString(DSS.Tables[0].Rows[i]["Studio"]), Convert.ToString(DSS.Tables[0].Rows[i]["Release"]), Convert.ToString(DSS.Tables[0].Rows[i]["HDSD"]), Convert.ToString(DSS.Tables[0].Rows[i]["Territories"]), Convert.ToString(DSS.Tables[0].Rows[i]["Hotel"]), Convert.ToString(DSS.Tables[0].Rows[i]["StudioType"]));

                string Predict = ConvertDatatableToCommaSeperatedValues(DtExpected, "Platform");
                string Actual = ConvertDataSetToCommaSeperatedValues(DsActual, "PCT");
                Assert.AreEqual(Predict, Actual);
            }
        
        }
        //[TestInitialize]
       
        // [ExpectedException(typeof(System.NullReferenceException))]
        public void NUnitOutput(string Studio, string Release, string HDSD, string Territories, string Hotel, string StudioType)
        {
            try
            {
                
               // // defined system object
               // DataTable DtExpected = new DataTable();
               // DataSet DsActual = new DataSet();
               // IndependentPlannerML.GeneratePCTsWithResult obj = new IndependentPlannerML.GeneratePCTsWithResult();

               // //string act = obj.Local();
               // //string act1 = obj.Localmain();
               //// Expected Value
               //  DtExpected = obj.GeneratePCTSForMLWithParameter(Studio, Release, HDSD, Territories, Hotel, StudioType);
               // //Actual OutPut
               //  DsActual = obj.LocalDatabaseUnqiePCTswithParameter(Studio, Release, HDSD, Territories, Hotel, StudioType);

               // string Predict = ConvertDatatableToCommaSeperatedValues(DtExpected, "Platform");
               // string Actual = ConvertDataSetToCommaSeperatedValues(DsActual, "PCT");
               // Assert.AreEqual(Predict, Actual);
                //Assert.AreEqual(Actual, DtExpected);
               // Assert.AreEqual(act, act1);
               
            }
            catch (Exception)
            {

                //throw;
            }
            

        }
        public string ConvertDatatableToCommaSeperatedValues(DataTable dt, string Column)
        {

            var SelectedValues = dt.AsEnumerable().Select(s => s.Field<string>(Column)).ToArray();
            string commaSeperatedValues = string.Join(",", SelectedValues);
            return commaSeperatedValues;
        }

        public string ConvertDataSetToCommaSeperatedValues(DataSet ds, string Column)
        {
            var SelectedValues = ds.Tables[0].AsEnumerable().Select(s => s.Field<string>(Column)).ToArray();
            string commaSeperatedValues = string.Join(",", SelectedValues);
            return commaSeperatedValues;
        }

        // Fetching Input data records from database
        private DataSet RetriveInputRecords()
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Studio");
            dt.Columns.Add("Release");
            dt.Columns.Add("HDSD");
            dt.Columns.Add("Territories");
            dt.Columns.Add("Hotel");
            dt.Columns.Add("StudioType");
            connectionnstring = "Data Source=V2MUMPC0478;Initial Catalog=MoviesPlannerDB;User Id=sa;Password=mail_123;Integrated Security=False;";
            //connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_InputData", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            //cmd.Parameters.Add(new SqlParameter("@Name", MoviesName));

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dt.Rows.Add();
                dt.Rows[m][0] = rdr["IStudio"];
                dt.Rows[m][1] = rdr["IRelease"];
                dt.Rows[m][2] = rdr["IHDSD"];
                dt.Rows[m][3] = rdr["ITerritories"];
                dt.Rows[m][4] = rdr["IHotel"];
                dt.Rows[m][5] = rdr["IStudioType"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }

        // written a method for consuming ML Web service  

    }
}
