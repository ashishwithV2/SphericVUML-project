using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//Web api call
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace IndependentPlannerML
{
    public class StringTable
    {
        public string[] ColumnNames
        {
            get;
            set;
        }
        public string[,] Values
        {
            get;
            set;
        }
    }

    //public class Person
    //{
    //    public string Licensor { get; set; }
    //    public string  'Scored Labels' { get; set; }
    //}


    public partial class IndependentPlannerAnalysis : System.Web.UI.Page
    {
        OleDbConnection conn;
        OleDbDataAdapter adapter;
        string drpvalue = "";
        DataTable dt;


        string Licensor = string.Empty;
        string Release = string.Empty;
        string HDSD = string.Empty;
        string Territories = string.Empty;
        string Hotel = string.Empty;
        string PLATFORM = string.Empty;
        DataSet DSMoviesName = new DataSet();
        DataSet DSScheduleExpert = new DataSet();
        DataSet DSMasterPlanner = new DataSet();
        DataTable dtvalu = new DataTable();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TxtAccuraccy.Visible = false;
                TxtMode.Visible = false;
                LicenserBind();
                ReleaseBind();
            }
          

        }

        // Bind the Licensor dropdownlist
        private void LicenserBind()
        {
            ListItemCollection collection = new ListItemCollection();
            collection.Add(new ListItem("-----Select-----"));
            collection.Add(new ListItem("Premiere Digital Services, Inc."));
            collection.Add(new ListItem("Image Entertainment"));
            collection.Add(new ListItem("All Channel Films"));
            collection.Add(new ListItem("MVD Entertainment Group"));
            collection.Add(new ListItem("Brainstorm Media"));
            collection.Add(new ListItem("Cinedigm Entertainment Corp."));
            collection.Add(new ListItem("Stonecutter Media"));
            collection.Add(new ListItem("Orchard Enterprises"));
            collection.Add(new ListItem("Breaking Glass Pictures"));
            collection.Add(new ListItem("Mar Vista"));
            collection.Add(new ListItem("Vertical Entertainment"));
            collection.Add(new ListItem("Level 33 Entertainment"));
            collection.Add(new ListItem("Freestyle Digital Media"));
            collection.Add(new ListItem("Neon Rated LLC"));
            collection.Add(new ListItem("Vision Films"));
            collection.Add(new ListItem("Global Asylum"));
            collection.Add(new ListItem("Lions Gate Entertainment"));
            //Pass ListItemCollection as datasource
            DropDownList1.DataSource = collection;
            DropDownList1.DataBind();
            //DropdownList1.DataSource = collection;
            //DropdownList1.DataValueField = "Value";
            //DropdownList1.DataTextField = "Text";
            //DropdownList1.DataBind();
        }
        // Bind the Release dropdownlist
        private void ReleaseBind()
        {
            ListItemCollection collection = new ListItemCollection();
            collection.Add(new ListItem("-----Select-----"));
            collection.Add(new ListItem("Independent Limited"));
            collection.Add(new ListItem("Independent Limited; Foreign Flix"));
            collection.Add(new ListItem("Independent"));
            collection.Add(new ListItem("Mini Major"));
            collection.Add(new ListItem("In Theaters"));
            collection.Add(new ListItem("Independent Limited; Charter"));
            collection.Add(new ListItem("Independent Limited; EST"));
            collection.Add(new ListItem("Independent; Foreign Flix"));
            collection.Add(new ListItem("Independent Library"));
            collection.Add(new ListItem("In Theaters; EST"));
            collection.Add(new ListItem("Pre-Theatrical"));
            //Pass ListItemCollection as datasource
            DropDownList2.DataSource = collection;
            DropDownList2.DataBind();
        }

    //    string message = "";
    //foreach (ListItem item in lstFruits.Items)
    //{
    //    if (item.Selected)
    //    {
    //        message += item.Text + " " + item.Value + "\\n";
    //    }
    //}
    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);





        //public string GetExect()
        //{
        //    // connect to xls file
        //    // NOTE: it will be created if not exists
        //    try
        //    {
        //        conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" +
        //            "Data Source=" + Application.StartupPath + "\\test.xlsx;" +
        //            "Extended Properties=Excel 12.0 Xml");
        //        conn.Open();
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.14.0;" +
        //                "Data Source=" + Application.StartupPath + "\\test.xlsx;" +
        //                "Extended Properties=Excel 14.0 Xml");
        //            conn.Open();
        //        }
        //        catch
        //        {
        //            try
        //            {
        //                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.15.0;" +
        //                    "Data Source=" + Application.StartupPath + "\\test.xlsx;" +
        //                    "Extended Properties=Excel 15.0 Xml");
        //                conn.Open();
        //            }
        //            catch
        //            {
        //            }
        //        }
        //    }
        //    // create a sheet "Sheet1" if not exists
        //    // NOTE: no "id" field needed
        //    // WARNING: spaces in sheet's name are supported if names are in [] (automatically replace with _)
        //    // spaces in column names NOT supported with OleDbCommandBuilder!
        //    try
        //    {
        //        string cmdText = "CREATE TABLE [Sheet 1] (text_col MEMO, int_col INT)";
        //        using (OleDbCommand cmd = new OleDbCommand(cmdText, conn))
        //        {
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch { }

        //    // get sheets list into combobox
        //    dt = conn.GetSchema("Tables");
        //    for (int i = 0; i < dt.Rows.Count - 1; i++)
        //    {
        //        if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_TYPE")].ToString() == "TABLE" &&
        //            !dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_NAME")].ToString().Contains("$"))
        //        {
        //           // comboBox1.Items.Add(dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_NAME")]);
        //        }
        //    }

        //}

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            Licensor = DropDownList1.SelectedValue;
            Release = DropDownList2.SelectedValue;

            // FInd the HD/SD Value
            foreach (ListItem item in lsthdsd.Items)
            {
                if (item.Selected)
                {
                    // HDSD += item.Value + "\\n";
                    HDSD += item.Value;
                }
            }
            // FInd the HD/SD Value
            foreach (ListItem item in LstTerritories.Items)
            {
                if (item.Selected)
                {
                    //Territories += item.Value + "\\n";
                    Territories += item.Value;
                }
            }
            // FInd the HD/SD Value
            foreach (ListItem item in LstHotel.Items)
            {
                if (item.Selected)
                {
                    //Hotel += item.Value + "\\n";
                    Hotel += item.Value;
                }
            }
          ///  string Platformdata,string Licenser,string Release,string HD/SD,string Terryterries,string Hotel

            List<string> arr = new List<string>();
            arr.Add("TVN_LIBRARY_INDEPENDENT_H264");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_25");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_H264");
            //arr.Add("TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED");
            //arr.Add("TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED_HD");
            //arr.Add("TVN_HOTEL_NEWRELEASE_IN_THEATERS_NOW_10");
            //arr.Add("TVN_HOTEL_NEW_RELEASE_IN_THEATERS_NOW_H264");
            //arr.Add("TVN_HOTEL_NEWRELEASE_HI_DEF_IN_THEATERS_NOW_10");
            //arr.Add("TVN_HOTEL_NEW_RELEASE_HI_DEF_IN_THEATERS_NOW_H264");
            //arr.Add("TVN_INDEPENDENT_20");
            //arr.Add("TVN_INDEPENDENT_H264_10");
            //arr.Add("TVN_INDEPENDENT_LIMITED_SINGLE_SCREEN_VZ");
            //arr.Add("TVN_INDEPENDENT_20_CANADA_15");
            //arr.Add("TVN_INDEPENDENT_H264_CANADA");
            //arr.Add("TVN_INDEPENDENT_CARIB");
            //arr.Add("TVN_INDEPENDENT_16x9_SD_HLS_CARIB");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_10");
            //arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_H264_10");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_VZ");
            //arr.Add("TVN_FTR_SINGLESCREEN_IN_THEATERS_NOW");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_10_CANADA");
            //arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_H264_10_CANADA");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_BERMUDA");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_BAHAMAS");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_16x9_SD_HLS_CARIB");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEFINITION_IN_THEATERS_NOW_10");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_IN_THEATERS_H264");
            //arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_HD_MBR_MP4_IVOD");
            //arr.Add("TVN_NEW_RELEASE_HD_IN_THEATERS_MEZZ_AVC30");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_ABR");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEFINITION_IN_THEATERS_NOW_VZ");
            //arr.Add("TVN_FTR_SINGLESCREEN_IN_THEATERS_NOW_HD");
            //arr.Add("TVN_NEWRELEASE_HI_DEF_IN_THEATERS_NOW_10_CANADA");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_IN_THEATERS_H264_CANADA");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_BERMUDA");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_BAHAMAS");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_25");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_H264");
            //arr.Add("TVN_INDEPENDENT_HIGH_DEF_ABR");
            //arr.Add("TVN_INDEPENDENT_HIGH_DEF_CARIB");
            //arr.Add("TVN_INDEPENDENT_LIMITED_10");
            //arr.Add("TVN_INDEPENDENT_LIMITED_10_CANADA_5");
            //arr.Add("TVN_INDEPENDENT_LIMITED_H264_10");
            //arr.Add("TVN_INDEPENDENT_LIMITED_H264_CANADA");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HD_MBR_MP4_IVOD");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HD_MEZZ_AVC30");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_CANADA");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264_CANADA");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_SINGLE_SCREEN_VZ");
            //arr.Add("TVN_FOREIGNFLIX");
            //arr.Add("TVN_FOREIGNFLIX_H264");
            //arr.Add("TVN_FOREIGNFLIX_Canada");
            //arr.Add("TVN_FOREIGNFLIX_H264_Canada");
            //arr.Add("TVN_FOREIGNFLIX_CARIB");
            //arr.Add("TVN_FOREIGNFLIX_H264_CARIB");
            //arr.Add("TVN_ASIAN_FLIX");
            //arr.Add("TVN_ASIAN_FLIX_MINI_PKG");
            //arr.Add("TVN_PASSION_ZONE_20");
            //arr.Add("TVN_PASSION_ZONE_H264_20");
            //arr.Add("TVN_PASSION_ZONE_20_CANADA_15");
            //arr.Add("TVN_PASSION_ZONE_H264_20_CANADA_15");
            //arr.Add("TVN_PASSION_ZONE_H264_CARIB");
            //arr.Add("TVN_PASSION_ZONE_4x3_SD_HLS_CARIB");
            //arr.Add("TVN_SPANISH_CINE_CENTRAL_TIER");
            //arr.Add("TVN_SPANISH_CINE_CENTRAL_H264_TIER");
            //arr.Add("TVN_NEW_RELEASE_HD_MINI_MAJOR_MEZZ_AVC30");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_MINI_MAJOR_H264");
            //arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_H264");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR_H264");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_H264");
            //arr.Add("TVN_JUST_IN_HD_MINI_MAJOR_MEZZ_AVC30");
            //arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_HD_MBR_MP4_IVOD");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_MINI_MAJOR");
            //arr.Add("TVN_NEWRELEASE_MINI_MAJOR");
            //arr.Add("TVN_NEWRELEASE_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_ABR");
            //arr.Add("TVN_LIBRARY_INDEPENDENT");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_CANADIAN");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_CARIB");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264_CANADIAN");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264_CARIB");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HD_MEZZ_AVC30");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_CANADIAN");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_CARIB");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264_CANADIAN");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264_CARIB");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_VZ");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_VZ");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_ABR");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_HD_EST");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_HD_IVOD");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_SD_EST");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_SD_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SD_AVC30_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_EST");
            //arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_EST");

            DataTable dt = new DataTable();
            dt.Columns.Add("Platform");
            dt.Columns.Add("ScoredLabels");
            dt.Columns.Add("ScoredProbabilities");
            int i = 0;
            foreach (var itemvalue in arr)
            {

                var InvokeData = InvokeRequestResponseService(itemvalue, Licensor, Release, HDSD, Territories, Hotel);
                //InvokeData.Wait();
                //string reciveData = InvokeData.Result;

                var obj = JObject.Parse(InvokeData.Result);
                var Val = obj["Results"]["output1"];


                //JavaScriptSerializer js = new JavaScriptSerializer();
                //Person[] persons = js.Deserialize((Convert.ToString(Val));

                var serializer = new JavaScriptSerializer();
                dynamic usr = serializer.DeserializeObject(Convert.ToString(Val));
                
               
                foreach (var item in usr)
                {

                    dt.Rows.Add();// Adding row into datatable
                    string platform = string.Empty;
                    string platformvalue = string.Empty;
                    string scoredlabels = string.Empty;
                    string scoredprobabilities = string.Empty;

                    foreach (KeyValuePair<string, object> pair in item)
                    {
                        platform = pair.Key.ToString();

                        if (platform == "Platform")
                        {
                            platformvalue = pair.Value.ToString();
                        }
                        if (platform == "Scored Labels")
                        {
                            scoredlabels = pair.Value.ToString();
                        }
                        if (platform == "Scored Probabilities")
                        {
                            scoredprobabilities = pair.Value.ToString();
                        }
                    }
                    dt.Rows[i]["platform"] = platformvalue;
                    dt.Rows[i]["scoredlabels"] = scoredlabels;
                    dt.Rows[i]["scoredprobabilities"] = scoredprobabilities;
                    i++;

                }
               
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();


          

        }

        private DataSet ScheduleExportRecords(string MoviesName)
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Licensor");
            dt.Columns.Add("Platform");

            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_ScheduleExxport", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@Name", MoviesName));

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dt.Rows.Add();
                dt.Rows[m][0] = rdr["Licensor"];
                dt.Rows[m][1] = rdr["Platform"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }

        private DataSet MasterPlannerRecords()
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Release");
            dt.Columns.Add("HDSD");
            dt.Columns.Add("Territories");
            dt.Columns.Add("Hotel");
            dt.Columns.Add("Platform");
            

            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();

            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_MasterPlanner", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            //cmd.Parameters.Add(new SqlParameter("@CustomerID", custId));

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dt.Rows.Add();
                dt.Rows[m][0] = rdr["Release"];
                dt.Rows[m][1] = rdr["HDSD"];
                dt.Rows[m][2] = rdr["Territories"];
                dt.Rows[m][3] = rdr["Hotel"];
                dt.Rows[m][4] = rdr["Platform"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }


        private DataSet MoviesNameRecords()
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("MoviesName");
      

            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();

            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("Usp_MoviesName", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            //cmd.Parameters.Add(new SqlParameter("@CustomerID", custId));

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dt.Rows.Add();
                dt.Rows[m][0] = rdr["MoviesName"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }


        private void FinalOutput()
        {
            try
            {
                string MoviesName = string.Empty;
                string Platform = string.Empty;
                string MPlatform = string.Empty;
                string STATUS = string.Empty;
                string Licensor = string.Empty;
                
                DSMoviesName = null;
                DSScheduleExpert = null;
                DSMasterPlanner = null;


                DSMoviesName = MoviesNameRecords();// Bind Movies List 
                if (DSMoviesName.Tables[0].Rows.Count > 1) 
                {
                    for (int i = 0; i < DSMoviesName.Tables[0].Rows.Count; i++)
                    {
                        MoviesName = Convert.ToString(DSMoviesName.Tables[0].Rows[i]["MoviesName"]);
                        if(!string.IsNullOrEmpty(MoviesName))
                        {
                           DSScheduleExpert=ScheduleExportRecords(MoviesName.Trim());
                           DSMasterPlanner = MasterPlannerRecords();

                            if(DSScheduleExpert.Tables[0].Rows.Count>1 && DSMasterPlanner.Tables[0].Rows.Count>1 )
                            {
                              Licensor= Convert.ToString(DSScheduleExpert.Tables[0].Rows[0][0]);


                              string sb ="";

                              for (int j = 0; j < DSScheduleExpert.Tables[0].Rows.Count; j++)
                              {

                                  Platform = Convert.ToString(DSScheduleExpert.Tables[0].Rows[j]["Platform"]);
                                  sb += Platform + ",";

                              }
                              string LastChar = sb.Remove(sb.Length - 1, 1);

                      

                               //dtvalu = null;
                               //dtvalu = DSMasterPlanner.Tables[0];
                               //int m = DSScheduleExpert.Tables[0].Rows.Count-1;
                              int result = 0;

                              for (int j = 0; j < DSMasterPlanner.Tables[0].Rows.Count; j++)
                              {
                                  MPlatform = Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Platform"]);

                                  //string value = MPlatform.CompareTo(LastChar);
                                  
                                  result = LastChar.IndexOf(MPlatform.Trim());

                                  if (result < 0)
                                  {
                                                    STATUS = "N";
                                                     //Insertrecords(Licensor, Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Release"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["HDSD"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Territories"]),
                                                     //   Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Hotel"]), MPlatform, STATUS, MoviesName);
                                  }
                                  else
                                  {
                                                    STATUS = "Y";
                                                     //Insertrecords(Licensor, Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Release"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["HDSD"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Territories"]),
                                                     //    Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Hotel"]), MPlatform, STATUS, MoviesName);

                                  }
                              }

                                //    if (j>=m)
                                //    {
                                //        Platform = "";
                                //    }
                                //    else
                                //    {
                                       
                                //        Platform = Convert.ToString(DSScheduleExpert.Tables[0].Rows[j]["Platform"]);
                                //    }


                                //    if (Platform != "") 
                                //    {
                                //        // Select by Platform.
                                //        DataRow[] result = dtvalu.Select("Platform = '" + Platform + "'");

                                //       
                                //        foreach (DataRow row in result)
                                //        {
                                //            string rowmatch= Convert.ToString(row[4]);
                                //            if (rowmatch.Trim().ToLower() == MPlatform.Trim().ToLower())
                                //            {
                                //                STATUS = "Y";
                                //                Insertrecords(Licensor, Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Release"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["HDSD"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Territories"]),
                                //                    Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Hotel"]), Platform, STATUS);

                                //                break;
                                //            }
                                //            else
                                //            {
                                //                STATUS = "N";
                                //                Insertrecords(Licensor, Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Release"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["HDSD"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Territories"]),
                                //                   Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Hotel"]), Platform, STATUS);
                                //                break;
                                //            }
                                //        }
                                //    }
                                //    else
                                //    {
                                //        STATUS = "N";
                                //        Insertrecords(Licensor, Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Release"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["HDSD"]), Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Territories"]),
                                //           Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Hotel"]), MPlatform, STATUS);
                                             
                                //    }
                                 
                                  

                            }
                        }
                    }
                }

               

            }
            catch (Exception)
            {
                
                //throw;
            }
        }


        private int Insertrecords(string Licensor, string Release, string HDSD, string Territories, string Hotel, string Platform, string Select)
        {

            string connectionnstring = "";
            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();

            SqlCommand objcmd = new SqlCommand("USP_Insert", objsqlconn);
            objcmd.CommandTimeout = 12000;
            objcmd.CommandType = CommandType.StoredProcedure;


            //SqlParameter property_url = objcmd.Parameters.Add("@Mmurl", SqlDbType.VarChar);
            //property_url.Value = Url;

            SqlParameter property_postid = objcmd.Parameters.Add("@Licensor", SqlDbType.VarChar);
            property_postid.Value = Licensor;

            SqlParameter property_name = objcmd.Parameters.Add("@Release", SqlDbType.VarChar);
            property_name.Value = Release;

            SqlParameter property_desc = objcmd.Parameters.Add("@HDSD", SqlDbType.VarChar);
            property_desc.Value = HDSD;

            SqlParameter property_rating = objcmd.Parameters.Add("@Territories", SqlDbType.VarChar);
            property_rating.Value = Territories;
            SqlParameter property_totrating = objcmd.Parameters.Add("@Hotel", SqlDbType.VarChar);
            property_totrating.Value = Hotel;
            SqlParameter property_ratingdesc = objcmd.Parameters.Add("@Platform", SqlDbType.VarChar);
            property_ratingdesc.Value = Platform;
            SqlParameter property_releasedate = objcmd.Parameters.Add("@Select", SqlDbType.VarChar);
            property_releasedate.Value = Select;

            //SqlParameter property_r = objcmd.Parameters.Add("@MoviesName", SqlDbType.VarChar);
            //property_r.Value = MoviesName;

            

            int i =objcmd.ExecuteNonQuery();
            objsqlconn.Close();
            return i;
        }


        private void Import_To_Grid(string FilePath, string Extension)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
          conStr = String.Format(conStr, FilePath);
           //conStr =  String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='+FilePath+';Extended Properties=""Excel 12.0 Xml;HDR=YES""");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            //connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            //GridView1.Caption = Path.GetFileName(FilePath);
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }

        public async Task<string> InvokeRequestResponseService( string Platformdata,string Licenser,string Release,string HDSD,string Terryterries,string Hotel)
        {
            List<string> arr = new List<string>();
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_25");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_H264");
            //arr.Add("TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED");
            //arr.Add("TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED_HD");
            //arr.Add("TVN_HOTEL_NEWRELEASE_IN_THEATERS_NOW_10");
            //arr.Add("TVN_HOTEL_NEW_RELEASE_IN_THEATERS_NOW_H264");
            //arr.Add("TVN_HOTEL_NEWRELEASE_HI_DEF_IN_THEATERS_NOW_10");
            //arr.Add("TVN_HOTEL_NEW_RELEASE_HI_DEF_IN_THEATERS_NOW_H264");
            //arr.Add("TVN_INDEPENDENT_20");
            //arr.Add("TVN_INDEPENDENT_H264_10");
            //arr.Add("TVN_INDEPENDENT_LIMITED_SINGLE_SCREEN_VZ");
            //arr.Add("TVN_INDEPENDENT_20_CANADA_15");
            //arr.Add("TVN_INDEPENDENT_H264_CANADA");
            //arr.Add("TVN_INDEPENDENT_CARIB");
            //arr.Add("TVN_INDEPENDENT_16x9_SD_HLS_CARIB");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_10");
            //arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_H264_10");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_VZ");
            //arr.Add("TVN_FTR_SINGLESCREEN_IN_THEATERS_NOW");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_10_CANADA");
            //arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_H264_10_CANADA");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_BERMUDA");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_BAHAMAS");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_16x9_SD_HLS_CARIB");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEFINITION_IN_THEATERS_NOW_10");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_IN_THEATERS_H264");
            //arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_HD_MBR_MP4_IVOD");
            //arr.Add("TVN_NEW_RELEASE_HD_IN_THEATERS_MEZZ_AVC30");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_ABR");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEFINITION_IN_THEATERS_NOW_VZ");
            //arr.Add("TVN_FTR_SINGLESCREEN_IN_THEATERS_NOW_HD");
            //arr.Add("TVN_NEWRELEASE_HI_DEF_IN_THEATERS_NOW_10_CANADA");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_IN_THEATERS_H264_CANADA");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_BERMUDA");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_BAHAMAS");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_25");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_H264");
            //arr.Add("TVN_INDEPENDENT_HIGH_DEF_ABR");
            //arr.Add("TVN_INDEPENDENT_HIGH_DEF_CARIB");
            //arr.Add("TVN_INDEPENDENT_LIMITED_10");
            //arr.Add("TVN_INDEPENDENT_LIMITED_10_CANADA_5");
            //arr.Add("TVN_INDEPENDENT_LIMITED_H264_10");
            //arr.Add("TVN_INDEPENDENT_LIMITED_H264_CANADA");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HD_MBR_MP4_IVOD");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HD_MEZZ_AVC30");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_CANADA");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264_CANADA");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_SINGLE_SCREEN_VZ");
            //arr.Add("TVN_FOREIGNFLIX");
            //arr.Add("TVN_FOREIGNFLIX_H264");
            //arr.Add("TVN_FOREIGNFLIX_Canada");
            //arr.Add("TVN_FOREIGNFLIX_H264_Canada");
            //arr.Add("TVN_FOREIGNFLIX_CARIB");
            //arr.Add("TVN_FOREIGNFLIX_H264_CARIB");
            //arr.Add("TVN_ASIAN_FLIX");
            //arr.Add("TVN_ASIAN_FLIX_MINI_PKG");
            //arr.Add("TVN_PASSION_ZONE_20");
            //arr.Add("TVN_PASSION_ZONE_H264_20");
            //arr.Add("TVN_PASSION_ZONE_20_CANADA_15");
            //arr.Add("TVN_PASSION_ZONE_H264_20_CANADA_15");
            //arr.Add("TVN_PASSION_ZONE_H264_CARIB");
            //arr.Add("TVN_PASSION_ZONE_4x3_SD_HLS_CARIB");
            //arr.Add("TVN_SPANISH_CINE_CENTRAL_TIER");
            //arr.Add("TVN_SPANISH_CINE_CENTRAL_H264_TIER");
            //arr.Add("TVN_NEW_RELEASE_HD_MINI_MAJOR_MEZZ_AVC30");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_MINI_MAJOR_H264");
            //arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_H264");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR_H264");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_H264");
            //arr.Add("TVN_JUST_IN_HD_MINI_MAJOR_MEZZ_AVC30");
            //arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_HD_MBR_MP4_IVOD");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_MINI_MAJOR");
            //arr.Add("TVN_NEWRELEASE_MINI_MAJOR");
            //arr.Add("TVN_NEWRELEASE_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_ABR");
            //arr.Add("TVN_LIBRARY_INDEPENDENT");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_CANADIAN");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_CARIB");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264_CANADIAN");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264_CARIB");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HD_MEZZ_AVC30");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_CANADIAN");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_CARIB");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264_CANADIAN");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264_CARIB");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_VZ");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_VZ");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_ABR");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_HD_EST");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_HD_IVOD");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_SD_EST");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_SD_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SD_AVC30_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_EST");
            //arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_EST");
            //arr.Add("TVN_FLEXVIEW_LIB_HD_PRORES_SCC_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_NR_HD_AVC30_SCC_BKFL_IVOD");
            //arr.Add("TVN_FLEXVIEW_LIB_HD_PRORES_SCC_BKFL_EST");
            //arr.Add("TVN_SPANISH_CINE_CENTRAL_H264_TIER");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_H264");
            //arr.Add("TVN_FLEXVIEW_LIB_SD_AVC30_SCC_BKFL_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_AVC30_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_PRORES_SCC_BKFL_EST");
            //arr.Add("TVN_INDEPENDENT_LIMITED_SINGLE_SCREEN_VZ");
            //arr.Add("TVN_FLEXVIEW_BONUS_SD_IVOD");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_AVC30_SCC_BKFL_EST");
            //arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_HD_MBR_MP4_IVOD");
            //arr.Add("TVN_FTR_MULTISCREEN_LIBRARY_SD_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_PRORES_SCC_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_AVC30_SCC_EST");
            //arr.Add("TVN_FLEXVIEW_NEWREL_SPANISH_SD_IVOD");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_AVC30_SCC_IVOD");
            //arr.Add("TVN_FTR_MULTISCREEN_SPANISH_NEWREL_SD_IVOD");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_PRORES_SCC_BKFL_IVOD");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_PRORES_SCC_BKFL_EST");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_VZ");
            //arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_HD_IVOD");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_AVC30_SCC_BKFL_IVOD");
            //arr.Add("TVN_FLEXVIEW_BONUS_HD_PRORES_SCC_BKFL_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_PRORES_SCC_BKFL_IVOD");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_PRORES_SCC_EST");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HD_MBR_MP4_IVOD");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_SINGLE_SCREEN_VZ");
            //arr.Add("TVN_FTR_MULTISCREEN_BONUS_SD_EST");
            //arr.Add("TVN_FLEXVIEW_NR_SD_AVC30_SCC_BKFL_IVOD");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_ABR");
            //arr.Add("TVN_DVS_FLEXVIEW_NEWREL_HIGH_DEF_IVOD");
            //arr.Add("TVN_FLEXVIEW_BONUS_HIGH_DEF_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_PRORES_SCC_BKFL_IVOD");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_ABR");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_PRORES_SCC_EST");
            //arr.Add("TVN_FTR_SINGLESCREEN_NEWREL_SD");
            //arr.Add("TVN_FLEXVIEW_LIB_SD_AVC30_SCC_BKFL_EST");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_H264");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_H264");
            //arr.Add("TVN_DVS_FLEXVIEW_NR_SD_PRORES_SCC_BKFL_EST");
            //arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_BKFL_IVOD");
            //arr.Add("TVN_INDEPENDENT_16x9_SD_HLS_CARIB");
            //arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED_HD");
            //arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_NDA_SD");
            //arr.Add("TVN_FLEXVIEW_LIB_SD_PRORES_SCC_BKFL_EST");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_CANADA");
            //arr.Add("TVN_PASSION_ZONE_H264_20");
            //arr.Add("TVN_INDEPENDENT_LIMITED_H264_10");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_CARIB");
            //arr.Add("TVN_FTR_SINGLESCREEN_DVS_NEWREL_HD");
            //arr.Add("TVN_FLEXVIEW_BONUS_HD_PRORES_SCC_IVOD");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264_CANADIAN");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_PRORES_SCC_IVOD");
            //arr.Add("TVN_LIBRARY_INDEPENDENT");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_HD_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_NEWREL_SD_EST");
            //arr.Add("TVN_FTR_DVS_MULTISCREEN_NEWREL_HD_IVOD");
            //arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_H264_CANADIAN");
            //arr.Add("TVN_FTR_SINGLESCREEN_LIBRARY_SD");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_AVC30_SCC_EST");
            //arr.Add("TVN_FLEXVIEW_NR_SD_AVC30_SCC_BKFL_EST");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_AVC30_SCC_BKFL_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_NR_HD_AVC30_SCC_BKFL_EST");
            //arr.Add("TVN_INDEPENDENT_CARIB");
            //arr.Add("TVN_FTR_SINGLESCREEN_IN_THEATERS_NOW_HD");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_BERMUDA");
            //arr.Add("TVN_DVS_FLEXVIEW_NEWREL_SD_IVOD");
            //arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_25");
            //arr.Add("TVN_FLEXVIEW_LIB_HD_PRORES_SCC_BKFL_IVOD");
            //arr.Add("TVN_FTR_SINGLESCREEN_LIBRARY_HD");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_SINGLE_SCREEN_VZ");
            //arr.Add("TVN_JUST_IN_HD_MINI_MAJOR_MEZZ_AVC30");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_PRORES_SCC_BKFL_EST");
            //arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_EST");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HD_MEZZ_AVC30");
            //arr.Add("TVN_FTR_MULTISCREEN_LIBRARY_HD_EST");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_AVC30_SCC_BKFL_EST");
            //arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_IVOD");
            //arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_SD_EST");
            //arr.Add("TVN_NEWRELEASE_HIGH_DEF_MINI_MAJOR");
            //arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_BKFL_EST");
            //arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_H264_10_CANADA");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_AVC30_SCC_BKFL_IVOD");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_BAHAMAS");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_PRORES_SCC_EST");
            //arr.Add("TVN_HOTEL_NEW_RELEASE_HI_DEF_IN_THEATERS_NOW_H264");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_PRORES_SCC_IVOD");
            //arr.Add("TVN_FOREIGNFLIX_H264_Canada");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_PRORES_SCC_IVOD");
            //arr.Add("TVN_FOREIGNFLIX_Canada");
            //arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264_CANADA");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_AVC30_SCC_BKFL_EST");
            //arr.Add("TVN_JUST_IN_MINI_MAJOR_CANADIAN");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_AVC30_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_BONUS_SD_PRORES_SCC_IVOD");
            //arr.Add("TVN_ASIAN_FLIX_MINI_PKG");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_PRORES_SCC_EST");
            //arr.Add("TVN_FLEXVIEW_LIB_HD_AVC30_SCC_IVOD");
            //arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_H264_10");
            //arr.Add("TVN_PASSION_ZONE_H264_CARIB");
            //arr.Add("TVN_FTR_MULTISCREEN_NEWREL_HD_EST");
            //arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_HD_EST");
            //arr.Add("TVN_DVS_FLEXVIEW_NR_SD_AVC30_SCC_EST");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_PRORES_SCC_EST");
            //arr.Add("TVN_INDEPENDENT_LIMITED_H264_CANADA");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_PRORES_SCC_IVOD");
            //arr.Add("TVN_INDEPENDENT_20");
            //arr.Add("TVN_FTR_MULTISCREEN_BONUS_HD_EST");
            //arr.Add("TVN_DVS_FLEXVIEW_NR_HD_AVC30_SCC_IVOD");
            //arr.Add("TVN_FOREIGNFLIX_CARIB");
            //arr.Add("TVN_INDEPENDENT_H264_10");
            //arr.Add("TVN_FTR_SINGLESCREEN_IN_THEATERS_NOW");
            //arr.Add("TVN_FLEXVIEW_LIB_SD_PRORES_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_PRORES_SCC_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_NR_SD_AVC30_SCC_BKFL_EST");
            //arr.Add("TVN_INDEPENDENT_LIMITED_10");
            //arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_AVC30_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NEWREL_SD_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_AVC30_SCC_IVOD");
            //arr.Add("TVN_FLEXVIEW_NEWREL_SPANISH_HD_IVOD");
            //arr.Add("TVN_FTR_MULTISCREEN_LIBRARY_SD_EST");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_PRORES_SCC_EST");
            //arr.Add("TVN_INDEPENDENT_20_CANADA_15");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_AVC30_SCC_IVOD");
            //arr.Add("TVN_HOTEL_NEWRELEASE_IN_THEATERS_NOW_10");
            //arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_CANADIAN");
            //arr.Add("TVN_FTR_DVS_MULTISCREEN_NEWREL_SD_EST");
            //arr.Add("TVN_FTR_MULTISCREEN_BONUS_HD_IVOD");
            //arr.Add("TVN_ASIAN_FLIX");
            //arr.Add("TVN_DVS_FLEXVIEW_NR_HD_PRORES_SCC_BKFL_IVOD");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_AVC30_SCC_EST");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_10");
            //arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_16x9_SD_HLS_CARIB");
            //arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_PRORES_SCC_BKFL_IVOD");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_PRORES_SCC_BKFL_EST");
            //arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_EST");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_AVC30_SCC_BKFL_EST");
            //arr.Add("TVN_FTR_DVS_MULTISCREEN_NEWREL_HD_EST");
            //arr.Add("TVN_NEW_RELEASE_HIGH_DEF_IN_THEATERS_H264");
            //arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_AVC30_SCC_EST");
            //arr.Add("TVN_FLEXVIEW_LIB_HD_PRORES_SCC_EST");
            //arr.Add("TVN_FTR_MULTISCREEN_SPANISH_NEWREL_HD_IVOD");


             arr.Add("TVN_FLEXVIEW_NR_HD_AVC30_SCC_EST");
 arr.Add("TVN_FLEXVIEW_NR_HD_AVC30_SCC_IVOD");
 arr.Add("TVN_FTR_MULTISCREEN_NEWREL_SD_EST");
 arr.Add("TVN_FTR_MULTISCREEN_NEWREL_SD_IVOD");
 arr.Add("TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED");
 arr.Add("TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED_HD");
 arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_25");
 arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_H264");
 arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_25");
 arr.Add("TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_H264");
 arr.Add("TVN_INDEPENDENT_HIGH_DEF_ABR");
 arr.Add("TVN_INDEPENDENT_LIMITED_10");
 arr.Add("TVN_INDEPENDENT_LIMITED_H264_10");
 arr.Add("TVN_INDEPENDENT_LIMITED_HD_MBR_MP4_IVOD");
 arr.Add("TVN_INDEPENDENT_LIMITED_HD_MEZZ_AVC30");
 arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF");
 arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_CANADA");
 arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264");
 arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_SINGLE_SCREEN_VZ");
 arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_HD_EST");
 arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_HD_IVOD");
 arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_NDA_HD");
 arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_NDA_SD");
 arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_SD_EST");
 arr.Add("TVN_ABR_FTR_MULTISCREEN_TV_SD_IVOD");
 arr.Add("TVN_ASIAN_FLIX");
 arr.Add("TVN_ASIAN_FLIX_MINI_PKG");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_AVC30_SCC_EST");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_AVC30_SCC_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_PRORES_SCC_EST");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_HD_PRORES_SCC_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_AVC30_SCC_EST");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_AVC30_SCC_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_PRORES_SCC_EST");
 arr.Add("TVN_DVS_FLEXVIEW_LIB_SD_PRORES_SCC_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NEWREL_HIGH_DEF_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NEWREL_HIGH_DEF_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NEWREL_SD_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NEWREL_SD_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NR_HD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NR_HD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NR_HD_AVC30_SCC_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NR_HD_AVC30_SCC_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NR_HD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NR_HD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NR_HD_PRORES_SCC_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NR_HD_PRORES_SCC_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NR_SD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NR_SD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NR_SD_AVC30_SCC_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NR_SD_AVC30_SCC_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NR_SD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NR_SD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_DVS_FLEXVIEW_NR_SD_PRORES_SCC_EST");
 arr.Add("TVN_DVS_FLEXVIEW_NR_SD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_HD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_HD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_HD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_HD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_HIGH_DEF_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_SD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_SD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_SD_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_SD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_BONUS_SD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_HD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_LIB_HD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_HD_AVC30_SCC_EST");
 arr.Add("TVN_FLEXVIEW_LIB_HD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_HD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_LIB_HD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_HD_PRORES_SCC_EST");
 arr.Add("TVN_FLEXVIEW_LIB_HD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SD_AVC30_SCC_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SD_PRORES_SCC_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_AVC30_SCC_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_PRORES_SCC_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_HD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_AVC30_SCC_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_PRORES_SCC_EST");
 arr.Add("TVN_FLEXVIEW_LIB_SPANISH_SD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_NEWREL_HIGH_DEF_EST");
 arr.Add("TVN_FLEXVIEW_NEWREL_HIGH_DEF_IVOD");
 arr.Add("TVN_FLEXVIEW_NEWREL_SD_EST");
 arr.Add("TVN_FLEXVIEW_NEWREL_SD_IVOD");
 arr.Add("TVN_FLEXVIEW_NEWREL_SPANISH_HD_EST");
 arr.Add("TVN_FLEXVIEW_NEWREL_SPANISH_HD_IVOD");
 arr.Add("TVN_FLEXVIEW_NEWREL_SPANISH_SD_EST");
 arr.Add("TVN_FLEXVIEW_NEWREL_SPANISH_SD_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_HD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_NR_HD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_EST");
 arr.Add("TVN_FLEXVIEW_NR_HD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_NR_SD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SD_AVC30_SCC_EST");
 arr.Add("TVN_FLEXVIEW_NR_SD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_EST");
 arr.Add("TVN_FLEXVIEW_NR_SD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_AVC30_SCC_EST");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_PRORES_SCC_EST");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_HD_PRORES_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_AVC30_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_AVC30_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_AVC30_SCC_EST");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_AVC30_SCC_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_PRORES_SCC_BKFL_EST");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_PRORES_SCC_BKFL_IVOD");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_PRORES_SCC_EST");
 arr.Add("TVN_FLEXVIEW_NR_SPANISH_SD_PRORES_SCC_IVOD");
 arr.Add("TVN_FOREIGNFLIX");
 arr.Add("TVN_FOREIGNFLIX_Canada");
 arr.Add("TVN_FOREIGNFLIX_CARIB");
 arr.Add("TVN_FOREIGNFLIX_H264");
 arr.Add("TVN_FOREIGNFLIX_H264_Canada");
 arr.Add("TVN_FOREIGNFLIX_H264_CARIB");
 arr.Add("TVN_FTR_DVS_MULTISCREEN_NEWREL_HD_EST");
 arr.Add("TVN_FTR_DVS_MULTISCREEN_NEWREL_HD_IVOD");
 arr.Add("TVN_FTR_DVS_MULTISCREEN_NEWREL_SD_EST");
 arr.Add("TVN_FTR_DVS_MULTISCREEN_NEWREL_SD_IVOD");
 arr.Add("TVN_FTR_MULTISCREEN_BONUS_HD_EST");
 arr.Add("TVN_FTR_MULTISCREEN_BONUS_HD_IVOD");
 arr.Add("TVN_FTR_MULTISCREEN_BONUS_SD_EST");
 arr.Add("TVN_FTR_MULTISCREEN_BONUS_SD_IVOD");
 arr.Add("TVN_FTR_MULTISCREEN_LIBRARY_HD_EST");
 arr.Add("TVN_FTR_MULTISCREEN_LIBRARY_HD_IVOD");
 arr.Add("TVN_FTR_MULTISCREEN_LIBRARY_SD_EST");
 arr.Add("TVN_FTR_MULTISCREEN_LIBRARY_SD_IVOD");
 arr.Add("TVN_FTR_MULTISCREEN_NEWREL_HD_EST");
 arr.Add("TVN_FTR_MULTISCREEN_NEWREL_HD_IVOD");
 arr.Add("TVN_FTR_MULTISCREEN_SPANISH_NEWREL_HD_EST");
 arr.Add("TVN_FTR_MULTISCREEN_SPANISH_NEWREL_HD_IVOD");
 arr.Add("TVN_FTR_MULTISCREEN_SPANISH_NEWREL_SD_EST");
 arr.Add("TVN_FTR_MULTISCREEN_SPANISH_NEWREL_SD_IVOD");
 arr.Add("TVN_FTR_SINGLESCREEN_DVS_LIBRARY");
 arr.Add("TVN_FTR_SINGLESCREEN_DVS_LIBRARY_HD");
 arr.Add("TVN_FTR_SINGLESCREEN_DVS_NEWREL");
 arr.Add("TVN_FTR_SINGLESCREEN_DVS_NEWREL_HD");
 arr.Add("TVN_FTR_SINGLESCREEN_IN_THEATERS_NOW");
 arr.Add("TVN_FTR_SINGLESCREEN_IN_THEATERS_NOW_HD");
 arr.Add("TVN_FTR_SINGLESCREEN_LIBRARY_HD");
 arr.Add("TVN_FTR_SINGLESCREEN_LIBRARY_SD");
 arr.Add("TVN_FTR_SINGLESCREEN_NEWREL_HD");
 arr.Add("TVN_FTR_SINGLESCREEN_NEWREL_SD");
 arr.Add("TVN_HOTEL_NEW_RELEASE_HI_DEF_IN_THEATERS_NOW_H264");
 arr.Add("TVN_HOTEL_NEW_RELEASE_IN_THEATERS_NOW_H264");
 arr.Add("TVN_HOTEL_NEWRELEASE_HI_DEF_IN_THEATERS_NOW_10");
 arr.Add("TVN_HOTEL_NEWRELEASE_IN_THEATERS_NOW_10");
 arr.Add("TVN_INDEPENDENT_16x9_SD_HLS_CARIB");
 arr.Add("TVN_INDEPENDENT_20");
 arr.Add("TVN_INDEPENDENT_20_CANADA_15");
 arr.Add("TVN_INDEPENDENT_CARIB");
 arr.Add("TVN_INDEPENDENT_CHARTER");
 arr.Add("TVN_INDEPENDENT_H264_10");
 arr.Add("TVN_INDEPENDENT_H264_CANADA");
 arr.Add("TVN_INDEPENDENT_HIGH_DEF_CARIB");
 arr.Add("TVN_INDEPENDENT_LIMITED_10_CANADA_5");
 arr.Add("TVN_INDEPENDENT_LIMITED_H264_CANADA");
 arr.Add("TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264_CANADA");
 arr.Add("TVN_INDEPENDENT_LIMITED_SINGLE_SCREEN_VZ");
 arr.Add("TVN_JUST_IN_HD_MINI_MAJOR_MEZZ_AVC30");
 arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR");
 arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_CANADIAN");
 arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_H264");
 arr.Add("TVN_JUST_IN_HIGH_DEF_MINI_MAJOR_H264_CANADIAN");
 arr.Add("TVN_JUST_IN_MINI_MAJOR");
 arr.Add("TVN_JUST_IN_MINI_MAJOR_CANADIAN");
 arr.Add("TVN_JUST_IN_MINI_MAJOR_H264");
 arr.Add("TVN_JUST_IN_MINI_MAJOR_H264_CANADIAN");
 arr.Add("TVN_LIBRARY_INDEPENDENT");
 arr.Add("TVN_LIBRARY_INDEPENDENT_CANADIAN");
 arr.Add("TVN_LIBRARY_INDEPENDENT_CARIB");
 arr.Add("TVN_LIBRARY_INDEPENDENT_H264");
 arr.Add("TVN_LIBRARY_INDEPENDENT_H264_CANADIAN");
 arr.Add("TVN_LIBRARY_INDEPENDENT_H264_CARIB");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HD_MEZZ_AVC30");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_ABR");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_CANADIAN");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_CARIB");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264_CANADIAN");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_H264_CARIB");
 arr.Add("TVN_LIBRARY_INDEPENDENT_HIGH_DEF_VZ");
 arr.Add("TVN_LIBRARY_INDEPENDENT_VZ");
 arr.Add("TVN_NEW_RELEASE_HD_IN_THEATERS_MEZZ_AVC30");
 arr.Add("TVN_NEW_RELEASE_HD_MINI_MAJOR_MEZZ_AVC30");
 arr.Add("TVN_NEW_RELEASE_HIGH_DEF_IN_THEATERS_H264");
 arr.Add("TVN_NEW_RELEASE_HIGH_DEF_IN_THEATERS_H264_CANADA");
 arr.Add("TVN_NEW_RELEASE_HIGH_DEF_MINI_MAJOR_H264");
 arr.Add("TVN_NEW_RELEASE_HIGH_DEF_MINI_MAJOR_H264_CANADIAN");
 arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_H264_10");
 arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_H264_10_CANADA");
 arr.Add("TVN_NEW_RELEASE_IN_THEATERS_NOW_HD_MBR_MP4_IVOD");
 arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_H264");
 arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_H264_CANADIAN");
 arr.Add("TVN_NEW_RELEASE_MINI_MAJOR_HD_MBR_MP4_IVOD");
 arr.Add("TVN_NEWRELEASE_HI_DEF_IN_THEATERS_NOW_10_CANADA");
 arr.Add("TVN_NEWRELEASE_HIGH_DEF_ABR");
 arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_ABR");
 arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_BAHAMAS");
 arr.Add("TVN_NEWRELEASE_HIGH_DEF_IN_THEATERS_BERMUDA");
 arr.Add("TVN_NEWRELEASE_HIGH_DEF_MINI_MAJOR");
 arr.Add("TVN_NEWRELEASE_HIGH_DEF_MINI_MAJOR_CANADIAN");
 arr.Add("TVN_NEWRELEASE_HIGH_DEF_SINGLE_SCREEN_VZ");
 arr.Add("TVN_NEWRELEASE_HIGH_DEFINITION_IN_THEATERS_NOW_10");
 arr.Add("TVN_NEWRELEASE_HIGH_DEFINITION_IN_THEATERS_NOW_VZ");
 arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_10");
 arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_10_CANADA");
 arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_16x9_SD_HLS_CARIB");
 arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_BAHAMAS");
 arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_BERMUDA");
 arr.Add("TVN_NEWRELEASE_IN_THEATERS_NOW_VZ");
 arr.Add("TVN_NEWRELEASE_MINI_MAJOR");
 arr.Add("TVN_NEWRELEASE_MINI_MAJOR_CANADIAN");
 arr.Add("TVN_NEWRELEASE_SINGLE_SCREEN_VZ");
 arr.Add("TVN_PASSION_ZONE_20");
 arr.Add("TVN_PASSION_ZONE_20_CANADA_15");
 arr.Add("TVN_PASSION_ZONE_4x3_SD_HLS_CARIB");
 arr.Add("TVN_PASSION_ZONE_H264_20");
 arr.Add("TVN_PASSION_ZONE_H264_20_CANADA_15");
 arr.Add("TVN_PASSION_ZONE_H264_CARIB");
 arr.Add("TVN_SPANISH_CINE_CENTRAL_H264_TIER");
 arr.Add("TVN_SPANISH_CINE_CENTRAL_TIER");



            string result = "";
            using (var client = new HttpClient())
            {

                dynamic abcd=null;
                List<Dictionary<string, string>> dictionary = new List<Dictionary<string, string>>();

                foreach (var item in arr)
                {

                    dictionary.Add(new Dictionary<string, string>()
                            {
                                            {
                                                "Licensor", Licenser
                                            },
                                            {
                                                "Release", Release
                                            },
                                            {
                                                "HD/SD", HDSD
                                            },
                                            {
                                                "Territories", Terryterries
                                            },
                                            {
                                                "Hotel", Hotel
                                            },
                                            {
                                                "Platform", item
                                            }
                                            ,
                                            {
                                                "Select", null
                                            },
                              }
                    );
                          
                 
                }

                //var abc = abcd;


                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                           "input1",dictionary

                          
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };


                const string apiKey = "L7USA2DaVFdA913UDQO8UkhzOBzs1HIKk0SCFdIkl39npHxmsz1yW1Oj7ca8TbF6av7sAwPWuJ8hqFmO6ITCRw=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2c9d645c7c874a329efa3850e3808afe/services/9e9fc70bbaa04d6f9782b638c7d28b25/execute?api-version=2.0&format=swagger");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);
                string len = Convert.ToString(response.Content.Headers.ContentLength);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                  

                    //Label6.Text = result;
                    // Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    //Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    //Console.WriteLine(response.Headers.ToString());

                    //string responseContent = await response.Content.ReadAsStringAsync();
                    // Console.WriteLine(responseContent);

                   // Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    //Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Label6.Text = responseContent;
                    Console.WriteLine(responseContent);
                }


            }
            return result;
        }

        protected void BtnPlatform_Click(object sender, EventArgs e)
        {
            try
            {
                //FinalOutput();
            }
            catch (Exception)
            {
                
                //throw;
            }
        }



        // New Changes Code Start From Here

        private DataSet TBLMasterPlannerMasterChangesUpdate()
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Licensor");
            dt.Columns.Add("Release");
            dt.Columns.Add("HDSD");
            dt.Columns.Add("Territories");
            dt.Columns.Add("Hotel");
            

            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_MasterPlannerMasterChanges", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            //cmd.Parameters.Add(new SqlParameter("@Name", Licensor));

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dt.Rows.Add();
                dt.Rows[m][0] = rdr["Licensor"];
                dt.Rows[m][1] = rdr["Release"];
                dt.Rows[m][2] = rdr["HDSD"];
                dt.Rows[m][3] = rdr["Territories"];
                dt.Rows[m][4] = rdr["Hotel"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }

        private DataSet MasterPlannerRecords(string licensor, string Release, string HDSD, string Territories, string Hotel)
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
           
            dt.Columns.Add("Platform");


            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();

            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_MasterPlannerChanges", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@licensor", licensor));
            cmd.Parameters.Add(new SqlParameter("@Release", Release));
            cmd.Parameters.Add(new SqlParameter("@HDSD", HDSD));
            cmd.Parameters.Add(new SqlParameter("@Territories", Territories));
            cmd.Parameters.Add(new SqlParameter("@Hotel", Hotel));

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
               dt.Rows.Add();
               dt.Rows[m][0] = rdr["Platform"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }

        // All Paltform records =total 123
        private DataSet TBLMasterPlannerMasterChanges()
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Platform");
           connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_MoviesPlatform", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            //cmd.Parameters.Add(new SqlParameter("@Name", Licensor));

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dt.Rows.Add();
                dt.Rows[m][0] = rdr["Platform"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }


        private void FinalOutputWithNewUpdate()
        {
            try
            {
                string licensor = string.Empty;
                string Platform = string.Empty;
                string MPlatform = string.Empty;
                string STATUS = string.Empty;
                string Licensor = string.Empty;

                DSMoviesName = null;
                DSScheduleExpert = null;
                DSMasterPlanner = null;
                DSMoviesName = TBLMasterPlannerMasterChangesUpdate();// Bind Licensor
                if (DSMoviesName.Tables[0].Rows.Count > 1)
                {
                    for (int i = 0; i < DSMoviesName.Tables[0].Rows.Count; i++)
                    {
                        licensor = Convert.ToString(DSMoviesName.Tables[0].Rows[i]["licensor"]);

                        if (!string.IsNullOrEmpty(licensor))
                        {
                            DSScheduleExpert = MasterPlannerRecords(licensor.Trim(), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Release"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["HDSD"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Territories"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Hotel"]));
                            DSMasterPlanner = TBLMasterPlannerMasterChanges(); // Master 

                            if (DSScheduleExpert.Tables[0].Rows.Count > 0 && DSMasterPlanner.Tables[0].Rows.Count > 0)
                            {
                                //Licensor = Convert.ToString(DSScheduleExpert.Tables[0].Rows[0][0]);
                                string sb = "";

                                for (int j = 0; j < DSScheduleExpert.Tables[0].Rows.Count; j++)
                                {

                                    Platform = Convert.ToString(DSScheduleExpert.Tables[0].Rows[j]["Platform"]);
                                    sb += Platform + ",";

                                }
                                string LastChar = sb.Remove(sb.Length - 1, 1);



                                //dtvalu = null;
                                //dtvalu = DSMasterPlanner.Tables[0];
                                //int m = DSScheduleExpert.Tables[0].Rows.Count-1;
                                int result = 0;

                                for (int j = 0; j < DSMasterPlanner.Tables[0].Rows.Count; j++)
                                {
                                    MPlatform = Convert.ToString(DSMasterPlanner.Tables[0].Rows[j]["Platform"]);

                                    //string value = MPlatform.CompareTo(LastChar);

                                    result = LastChar.IndexOf(MPlatform.Trim());

                                    if (result < 0)
                                    {
                                        STATUS = "N";
                                        Insertrecordschanges(Convert.ToString(DSMoviesName.Tables[0].Rows[i]["licensor"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Release"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["HDSD"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Territories"]),
                                           Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Hotel"]), MPlatform, STATUS);
                                    }
                                    else
                                    {
                                        STATUS = "Y";
                                        Insertrecordschanges(Convert.ToString(DSMoviesName.Tables[0].Rows[i]["licensor"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Release"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["HDSD"]), Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Territories"]),
                                            Convert.ToString(DSMoviesName.Tables[0].Rows[i]["Hotel"]), MPlatform, STATUS);

                                    }
                                }


                            }
                        }
                    }
                }



            }
            catch (Exception)
            {

                //throw;
            }
        }


        private int Insertrecordschanges(string Licensor, string Release, string HDSD, string Territories, string Hotel, string Platform, string Select)
        {

            string connectionnstring = "";
            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();

            SqlCommand objcmd = new SqlCommand("USP_InsertChanges", objsqlconn);
            objcmd.CommandTimeout = 12000;
            objcmd.CommandType = CommandType.StoredProcedure;


            //SqlParameter property_url = objcmd.Parameters.Add("@Mmurl", SqlDbType.VarChar);
            //property_url.Value = Url;

            SqlParameter property_postid = objcmd.Parameters.Add("@Licensor", SqlDbType.VarChar);
            property_postid.Value = Licensor;

            SqlParameter property_name = objcmd.Parameters.Add("@Release", SqlDbType.VarChar);
            property_name.Value = Release;

            SqlParameter property_desc = objcmd.Parameters.Add("@HDSD", SqlDbType.VarChar);
            property_desc.Value = HDSD;

            SqlParameter property_rating = objcmd.Parameters.Add("@Territories", SqlDbType.VarChar);
            property_rating.Value = Territories;
            SqlParameter property_totrating = objcmd.Parameters.Add("@Hotel", SqlDbType.VarChar);
            property_totrating.Value = Hotel;
            SqlParameter property_ratingdesc = objcmd.Parameters.Add("@Platform", SqlDbType.VarChar);
            property_ratingdesc.Value = Platform;
            SqlParameter property_releasedate = objcmd.Parameters.Add("@Select", SqlDbType.VarChar);
            property_releasedate.Value = Select;

            //SqlParameter property_r = objcmd.Parameters.Add("@MoviesName", SqlDbType.VarChar);
            //property_r.Value = MoviesName;



            int i = objcmd.ExecuteNonQuery();
            objsqlconn.Close();
            return i;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                FinalOutputWithNewUpdate();
            }
            catch (Exception)
            {
                
               // throw;
            }
        }

    }
}