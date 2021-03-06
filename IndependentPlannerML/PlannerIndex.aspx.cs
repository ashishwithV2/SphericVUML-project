﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndependentPlannerML
{
    public partial class PlannerIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LicenserBind();
                ReleaseBind();
                hdsd();
                Territories();


            
            }
          
        }
        // Fetching Input data records from database
        private DataTable StudioBinding()
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("StudioName");
            dt.Columns.Add("StudioCategory");
            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_BindStudio", objsqlconn);
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
                dt.Rows[m][0] = rdr["StudioName"];
                dt.Rows[m][1] = rdr["StudioName"];

                m++;

            }
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            objsqlconn.Close();
            return dt;
        }


        // Fetching Input data records from database
        private DataTable StudioWiseCategory(string StudioName)
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dtcat = new DataTable();
            dtcat.Columns.Add("StudioCategory");
            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_GetStudioCategory", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@StudioName", StudioName));

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dtcat.Rows.Add();
                dtcat.Rows[m][0] = rdr["StudioCategory"];
                m++;

            }
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            objsqlconn.Close();
            return dtcat;
        }


        // Fetching Input data records from database
        private DataTable ReteiveAPIKeyBASEAddress(string ReleaseType, string Studiotype)
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dtcat = new DataTable();
            dtcat.Columns.Add("RT_Name");
            dtcat.Columns.Add("RT_StudioType");
            dtcat.Columns.Add("RT_URL");
            dtcat.Columns.Add("RT_Key");
            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_ReleaseType", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@ReleaseType", ReleaseType));
            cmd.Parameters.Add(new SqlParameter("@StudioType", Studiotype));
            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dtcat.Rows.Add();
                dtcat.Rows[m][0] = rdr["RT_Name"];
                dtcat.Rows[m][1] = rdr["RT_StudioType"];
                dtcat.Rows[m][2] = rdr["RT_URL"];
                dtcat.Rows[m][3] = rdr["RT_Key"];
                m++;

            }
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            objsqlconn.Close();
            return dtcat;
        }
        // Bind the Licensor dropdownlist
        private void LicenserBind()
        {
            //ListItemCollection collection = new ListItemCollection();
            //collection.Add(new ListItem("-----Select-----"));


            //collection.Add(new ListItem("Premiere Digital Services, Inc."));
            //collection.Add(new ListItem("Image Entertainment"));
            //collection.Add(new ListItem("All Channel Films"));
            //collection.Add(new ListItem("Breaking Glass Pictures"));
            //collection.Add(new ListItem("Electric Entertainment"));
            //collection.Add(new ListItem("MVD Entertainment Group"));
            //collection.Add(new ListItem("Goldwyn"));
            //collection.Add(new ListItem("Columbia Tri-Star"));
            //collection.Add(new ListItem("Vertical Entertainment"));
            //collection.Add(new ListItem("Brainstorm Media"));
            //collection.Add(new ListItem("Cinedigm Entertainment Corp."));
            //collection.Add(new ListItem("Film Movement"));
            //collection.Add(new ListItem("Stonecutter Media"));
            //collection.Add(new ListItem("Orchard Enterprises"));
            //collection.Add(new ListItem("Freestyle Digital Media"));
            //collection.Add(new ListItem("Mar Vista"));
            //collection.Add(new ListItem("Level 33 Entertainment"));
            //collection.Add(new ListItem("Neon Rated LLC"));
            //collection.Add(new ListItem("Mongrel Media"));
            //collection.Add(new ListItem("Well Go"));
            //collection.Add(new ListItem("Elevation Pictures"));
            //collection.Add(new ListItem("Vision Films"));
            //collection.Add(new ListItem("Global Asylum"));
            //collection.Add(new ListItem("Lions Gate Entertainment"));
            //collection.Add(new ListItem("Screen Media Ventures"));
            //collection.Add(new ListItem("All Channel Films"));
            //collection.Add(new ListItem("Brainstorm Media"));
            //collection.Add(new ListItem("Breaking Glass Films"));
            //collection.Add(new ListItem("Breaking Glass Pictures"));
            //collection.Add(new ListItem("Broad Green Pictures"));
            //collection.Add(new ListItem("Cinedigm"));
            //collection.Add(new ListItem("Cinedigm Entertainment"));
            //collection.Add(new ListItem("Cohen Media Group"));
            //collection.Add(new ListItem("Electric Entertainment"));
            //collection.Add(new ListItem("Epic Pictures"));
            //collection.Add(new ListItem("Factory Film Studio"));
            //collection.Add(new ListItem("Film Movement"));
            //collection.Add(new ListItem("Freestyle Digital Media"));
            //collection.Add(new ListItem("GoDigital"));
            //collection.Add(new ListItem("Green Apple Entertainment"));
            //collection.Add(new ListItem("Inception Digital Media"));
            //collection.Add(new ListItem("Inception Media Group"));
            //collection.Add(new ListItem("Juice Worldwide"));
            //collection.Add(new ListItem("Lantern Lane"));
            //collection.Add(new ListItem("Legendary"));
            //collection.Add(new ListItem("Level 33 Entertainment"));
            //collection.Add(new ListItem("Mar Vista Digital Entertainment"));
            //collection.Add(new ListItem("Maxim Media"));
            //collection.Add(new ListItem("Monterey Media"));
            //collection.Add(new ListItem("MVD Entertainment"));
            //collection.Add(new ListItem("Neon"));
            //collection.Add(new ListItem("New City Releasing"));
            //collection.Add(new ListItem("Premiere Digital"));
            //collection.Add(new ListItem("Premiere Digital Services"));
            //collection.Add(new ListItem("RLJ Entertainment"));
            //collection.Add(new ListItem("Samuel Goldwyn Films"));
            //collection.Add(new ListItem("Screen Media Films"));
            //collection.Add(new ListItem("Screen Media Ventures"));
            //collection.Add(new ListItem("Stonecutter"));
            //collection.Add(new ListItem("STX Entertainment"));
            //collection.Add(new ListItem("Syndicado"));
            //collection.Add(new ListItem("The Asylum"));
            //collection.Add(new ListItem("The Orchard"));
            //collection.Add(new ListItem("Under the Milky Way"));
            //collection.Add(new ListItem("Vertical Entertainment"));
            //collection.Add(new ListItem("Virgil Films"));
            //collection.Add(new ListItem("Vision Films"));
            //collection.Add(new ListItem("Well Go, USA"));
            DataTable dt = new DataTable();
            dt = StudioBinding();
            drpstudio.DataSource = dt;
            drpstudio.DataBind();
            drpstudio.DataTextField = "StudioName";
            drpstudio.DataValueField = "StudioCategory";
            drpstudio.DataBind();


            //Pass ListItemCollection as datasource
            //drpstudio.DataSource = collection;
            //drpstudio.DataBind();

        }
        // Bind the Release dropdownlist
        private void ReleaseBind()
        {
            ListItemCollection collection = new ListItemCollection();
            //collection.Add(new ListItem("-----Select-----"));
            //  collection.Add(new ListItem("Independent Limited"));
            //collection.Add(new ListItem("Independent Limited; Foreign Flix"));
            //collection.Add(new ListItem("Independent"));
            //collection.Add(new ListItem("Mini Major"));
            //collection.Add(new ListItem("In Theaters"));
            //collection.Add(new ListItem("Independent Limited; Charter"));
            //collection.Add(new ListItem("Independent Limited; EST"));
            //collection.Add(new ListItem("Independent; Foreign Flix"));
            //collection.Add(new ListItem("Independent Library"));
            //collection.Add(new ListItem("In Theaters; EST"));
            //collection.Add(new ListItem("Pre-Theatrical"));

            collection.Add(new ListItem("Independent"));
            collection.Add(new ListItem("Independent Limited"));
            collection.Add(new ListItem("Pre-Theatrical"));
            collection.Add(new ListItem("In Theaters"));
            collection.Add(new ListItem("Foreign Flix"));
            collection.Add(new ListItem("Asian Flix"));
            collection.Add(new ListItem("Passion Zone"));
            collection.Add(new ListItem("Cine Central"));
            collection.Add(new ListItem("Mini Major"));
            collection.Add(new ListItem("Min - In Theaters"));
            collection.Add(new ListItem("Min - Pre Theatrical"));
            collection.Add(new ListItem("Independent Library"));
            collection.Add(new ListItem("EST"));
            //Pass ListItemCollection as datasource
            Drprelease.DataSource = collection;
            Drprelease.DataBind();

        }
         private void hdsd()
        {
            ListItemCollection collection = new ListItemCollection();
            // collection.Add(new ListItem("-----Select-----"));
            collection.Add(new ListItem("HD"));
            collection.Add(new ListItem("SD"));

            Drphdsd.DataSource = collection;
            Drphdsd.DataBind();

        }
        private void Territories()
        {
            ListItemCollection collection = new ListItemCollection();
            // collection.Add(new ListItem("-----Select-----"));
            //collection.Add(new ListItem("USA"));
            //collection.Add(new ListItem("US, CAN"));
            //collection.Add(new ListItem("US, CAN, CARIB"));
            collection.Add(new ListItem("USA"));
            collection.Add(new ListItem("CAN"));
            collection.Add(new ListItem("CARIB"));
            //Pass ListItemCollection as datasource
            DrpTerritories.DataSource = collection;
            DrpTerritories.DataBind();
        }

        private DataSet MasterPlannerRecords(string Title, string Release, string HDSD, string Territories)
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();

            dt.Columns.Add("Platform");
            dt.Columns.Add("select");
            dt.Columns.Add("HDSD");
            dt.Columns.Add("Territories");
            dt.Columns.Add("Licensor");
            dt.Columns.Add("Release");
            dt.Columns.Add("Hotel");


            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();

            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("Usp_ReadPlatform", objsqlconn); // Usp_ReadPlatform (Old one 20202 records)
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Title", Title));

            // For Troubleshooting 
            cmd.Parameters.Add(new SqlParameter("@Release", Release));
            cmd.Parameters.Add(new SqlParameter("@HDSD", HDSD));
            cmd.Parameters.Add(new SqlParameter("@Territories", Territories));


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
                dt.Rows[m][0] = rdr["Platform"];
                dt.Rows[m][1] = rdr["select"];
                dt.Rows[m][2] = rdr["HDSD"];
                dt.Rows[m][3] = rdr["Territories"];
                dt.Rows[m][4] = rdr["Licensor"];
                dt.Rows[m][5] = rdr["Release"];
                dt.Rows[m][6] = rdr["Hotel"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
            //string connectionnstring = "";
            //SqlDataReader rdr = null;
            //DataTable dt = new DataTable();

            //dt.Columns.Add("Platform");
            //// dt.Columns.Add("select");
            //dt.Columns.Add("HDSD");
            //dt.Columns.Add("Territories");
            //dt.Columns.Add("Licensor");
            //dt.Columns.Add("Release");
            //dt.Columns.Add("Hotel");


            //connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            //SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            //objsqlconn.Open();

            //// 1. create a command object identifying
            //// the stored procedure
            //SqlCommand cmd = new SqlCommand("Usp_ReadPlatform", objsqlconn); // Usp_ReadPlatform (Old one 20202 records)
            //cmd.CommandTimeout = 12000;

            //// 2. set the command object so it knows
            //// to execute a stored procedure
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@Title", Title));

            //// For Troubleshooting 
            //cmd.Parameters.Add(new SqlParameter("@Release", Release));
            //cmd.Parameters.Add(new SqlParameter("@HDSD", HDSD));
            //cmd.Parameters.Add(new SqlParameter("@Territories", Territories));


            //// 3. add parameter to command, which
            //// will be passed to the stored procedure
            ////cmd.Parameters.Add(new SqlParameter("@CustomerID", custId));

            //// execute the command
            //rdr = cmd.ExecuteReader();

            //// iterate through results, printing each to console
            //int m = 0;
            //while (rdr.Read())
            //{
            //    dt.Rows.Add();
            //    dt.Rows[m][0] = rdr["Platform"];
            //    //dt.Rows[m][1] = rdr["select"];
            //    dt.Rows[m][1] = rdr["HDSD"];
            //    dt.Rows[m][2] = rdr["Territories"];
            //    dt.Rows[m][3] = rdr["Licensor"];
            //    dt.Rows[m][4] = rdr["Release"];
            //    dt.Rows[m][5] = rdr["Hotel"];
            //    m++;

            //}
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            //objsqlconn.Close();
            //return ds;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Platform");
                dt.Columns.Add("ScoredLabels");
                dt.Columns.Add("ScoredProbabilities");
                DataTable StudioType = new DataTable();
                DataTable DtAPI = new DataTable();
                DataSet dss = new DataSet();
                //drpstudio.SelectedValue
                dss = MasterPlannerRecords(drpstudio.SelectedValue, Drprelease.SelectedValue, Drphdsd.SelectedValue, DrpTerritories.SelectedValue);
                // Getting Category through Studio Name
                StudioType= StudioWiseCategory(drpstudio.SelectedValue);

                // retrieving API Key and BaseAddress using ReleaseType and StudioType

                DtAPI=ReteiveAPIKeyBASEAddress(Drprelease.SelectedValue, Convert.ToString(StudioType.Rows[0]["StudioCategory"]));

                if (dss !=null && dss.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                      var InvokeData = InvokeRequestResponseService(dss, Convert.ToString(DtAPI.Rows[0]["RT_Key"]), Convert.ToString(DtAPI.Rows[0]["RT_URL"]));
                  //  var InvokeData = InvokeRequestResponseServicePCTsWithComma(dss);

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

                        if (Convert.ToInt32(scoredprobabilities.Substring(0, 1)) > 0)
                        {
                            dt.Rows.Add();// Adding row into datatable
                            if (Convert.ToInt32(scoredprobabilities.Substring(0, 1)) == 1)
                            {
                                scoredprobabilities = "100%";
                            }
                            dt.Rows[i]["platform"] = platformvalue;
                            dt.Rows[i]["scoredlabels"] = scoredlabels;
                            dt.Rows[i]["scoredprobabilities"] = scoredprobabilities;
                            i++;

                        }
                        else
                        {
                            // Gets a NumberFormatInfo associated with the en-US culture.
                            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                            // Displays a value with the default number of decimal digits (2).
                            Double myInt = Convert.ToDouble(scoredprobabilities);
                            int val = Convert.ToInt32(myInt.ToString("P", nfi).Substring(0, 1));

                            if (val != 0 && val > 2)
                            {
                                dt.Rows.Add();// Adding row into datatable
                                dt.Rows[i]["platform"] = platformvalue;
                                dt.Rows[i]["scoredlabels"] = scoredlabels;
                                dt.Rows[i]["scoredprobabilities"] = myInt.ToString("P", nfi);
                                i++;
                                // dt.Rows.Add();// Adding row into datatable
                            }
                            else
                            {

                            }
                        }



                    }


                    dt.DefaultView.Sort = "scoredlabels desc";


                    if (dt.Rows.Count > 0)
                    {
                        example.DataSource = dt;
                        example.DataBind();
                    }
                    else
                    {
                        example.DataSource = null;
                        example.DataBind();
                    }
                }
                else
                {
                    example.DataSource = dt;
                    example.DataBind();
                 
                }
         
            }
            catch (Exception)
            {
              
            }
          
       }
        
       

        public async Task<string> InvokeRequestResponseService(DataSet dsval,string APIKEY,string APIURL)
        {
            List<string> arr = new List<string>();
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264");
             string Licenser="";
                string Release=""; 
                string HDSD="";
                string Terryterries="";
                string Hotel = "";
                string Select = "";
            for (int i = 0; i < dsval.Tables[0].Rows.Count; i++)
            {
                arr.Add(Convert.ToString(dsval.Tables[0].Rows[i]["Platform"]));
                
            }
            
            string result = "";
            int j=0;
            using (var client = new HttpClient())
            {

                //dynamic abcd = null;
               

                

                List<Dictionary<string, string>> dictionary = new List<Dictionary<string, string>>();

                foreach (var item in arr)
                {
                    Licenser = Convert.ToString(dsval.Tables[0].Rows[j]["Licensor"]);
                    Release = Convert.ToString(dsval.Tables[0].Rows[j]["Release"]);
                    HDSD = Convert.ToString(dsval.Tables[0].Rows[j]["HDSD"]);
                    Terryterries = Convert.ToString(dsval.Tables[0].Rows[j]["Territories"]);
                    Hotel = Convert.ToString(dsval.Tables[0].Rows[j]["Hotel"]);
                    Select = Convert.ToString(dsval.Tables[0].Rows[j]["Select"]);//
                    j++;
                    dictionary.Add(new Dictionary<string, string>()
                            {
                                            //{
                                            //    "Licensor", Licenser
                                            //},
                                            //{
                                            //    "Release", TextBox1.Text
                                            //},
                                            //{
                                            //    "HD/SD", TextBox2.Text
                                            //},
                                            //{
                                            //    "Territories", TextBox3.Text
                                            //},
                                            //{
                                            //    "Hotel", "Yes"
                                            //},
                                            //{
                                            //    "Platform", item
                                            //}
                                            //,
                                            //{
                                            //    "Select", null
                                            //},
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
                                                "Hotel", "Yes"
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


                // Old Mine APIKEY 

                //const string apiKey = "L7USA2DaVFdA913UDQO8UkhzOBzs1HIKk0SCFdIkl39npHxmsz1yW1Oj7ca8TbF6av7sAwPWuJ8hqFmO6ITCRw=="; // Replace this with the API key for the web service
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                //client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2c9d645c7c874a329efa3850e3808afe/services/9e9fc70bbaa04d6f9782b638c7d28b25/execute?api-version=2.0&format=swagger");
                // ######################################


                // New API KEY For Ratings.V2@outlook.com
                //   const string apiKey = "PJMZCYOrnTg51ahKe5IewP0WZsS2NZQsvgYoR5qhH7KAODRWgm8+F1yVrdf54cy0J9zCjW65opmvNZCIh7pZPQ=="; // Replace this with the API key for the web service
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                // client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2b8bed4f6390463997d08b708f7e6151/services/977532eb83dd479ea823c4c4d5d254f9/execute?api-version=2.0&format=swagger");
                //  client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2b8bed4f6390463997d08b708f7e6151/services/edd6b88cbcef4a61aa61ec540e57a174/execute?api-version=2.0&format=swagger");


                //   https://ussouthcentral.services.azureml.net/workspaces/2b8bed4f6390463997d08b708f7e6151/services/42d45dea097847689b6877016c1b71a3/execute?api-version=2.0&format=swagger


                // ############# Set Dynamic Binding key and BaseAddress
                string apiKey = APIKEY;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri(APIURL);
               

                // #####################

                // The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
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
                   // Label6.Text = responseContent;
                    Console.WriteLine(responseContent);
                }


            }
            return result;
        }




        // New way to start written code 04/12/2017 11:04AM


        // New way to start written code 04/12/2017 11:04AM


        public async Task<string> InvokeRequestResponseServicePCTsWithComma(DataSet dsval)
        {
            List<string> arr = new List<string>();
            //arr.Add("TVN_LIBRARY_INDEPENDENT_H264");
            string Licenser = "";
            string Release = "";
            string HDSD = "";
            string Terryterries = "";
            string Hotel = "";
            string Select = "";
            for (int i = 0; i < dsval.Tables[0].Rows.Count; i++)
            {
                arr.Add(Convert.ToString(dsval.Tables[0].Rows[i]["Platform"]));

            }

            string result = "";
            int j = 0;
            using (var client = new HttpClient())
            {

                //dynamic abcd = null;




                List<Dictionary<string, string>> dictionary = new List<Dictionary<string, string>>();

                foreach (var item in arr)
                {
                    Licenser = Convert.ToString(dsval.Tables[0].Rows[j]["Licensor"]);
                    Release = Convert.ToString(dsval.Tables[0].Rows[j]["Release"]);
                    HDSD = Convert.ToString(dsval.Tables[0].Rows[j]["HDSD"]);
                    Terryterries = Convert.ToString(dsval.Tables[0].Rows[j]["Territories"]);
                    Hotel = Convert.ToString(dsval.Tables[0].Rows[j]["Hotel"]);
                    // Select = Convert.ToString(dsval.Tables[0].Rows[j]["Select"]);//
                    j++;
                    dictionary.Add(new Dictionary<string, string>()
                            {
                                            //{
                                            //    "Licensor", Licenser
                                            //},
                                            //{
                                            //    "Release", TextBox1.Text
                                            //},
                                            //{
                                            //    "HD/SD", TextBox2.Text
                                            //},
                                            //{
                                            //    "Territories", TextBox3.Text
                                            //},
                                            //{
                                            //    "Hotel", "Yes"
                                            //},
                                            //{
                                            //    "Platform", item
                                            //}
                                            //,
                                            //{
                                            //    "Select", null
                                            //},
                                              {
                                                "Licensor", Licenser
                                            },
                                            {
                                                "Release", Release
                                            },
                                            {
                                                "HDSD", HDSD
                                            },
                                            {
                                                "Territories", Terryterries
                                            },
                                            {
                                                "Hotel", "Yes"
                                            },
                                            //{
                                            //    "Platform", item
                                            //}
                                             {
                                                "ID", null
                                            }


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


                //  const string apiKey = "sDu/W7+N+14aW+i10z28+mGTh8u/ZFgqSTH8fze5nXGCyfoNs8PxaPOvC4feZWB5tfHXPft1R/oBXT0mc1otyw=="; // Replace this with the API key for the web service
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                //client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2c9d645c7c874a329efa3850e3808afe/services/8352056fa2664c3685bb3ee952c56e5f/execute?api-version=2.0&details=true");

                const string apiKey = "L7USA2DaVFdA913UDQO8UkhzOBzs1HIKk0SCFdIkl39npHxmsz1yW1Oj7ca8TbF6av7sAwPWuJ8hqFmO6ITCRw=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2c9d645c7c874a329efa3850e3808afe/services/9e9fc70bbaa04d6f9782b638c7d28b25/execute?api-version=2.0&format=swagger");


                //const string apiKey = "L7USA2DaVFdA913UDQO8UkhzOBzs1HIKk0SCFdIkl39npHxmsz1yW1Oj7ca8TbF6av7sAwPWuJ8hqFmO6ITCRw=="; // Replace this with the API key for the web service
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                //client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2c9d645c7c874a329efa3850e3808afe/services/9e9fc70bbaa04d6f9782b638c7d28b25/execute?api-version=2.0&format=swagger");

                //// WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
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
                    // Label6.Text = responseContent;
                    Console.WriteLine(responseContent);
                }


            }
            return result;
        }






    }
}