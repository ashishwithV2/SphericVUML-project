using Newtonsoft.Json.Linq;
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
    public partial class ServiceCall : System.Web.UI.Page
    {
        //string Studio1,Release1, hdsd1,Territories1="";
        protected void Page_Load(object sender, EventArgs e)
        {
             //Studio1 = Request.QueryString["s"];
             //Release1 = Request.QueryString["r"];

             ServiceRetrieve(Request.QueryString["s"], Request.QueryString["r"], Request.QueryString["hd"], Request.QueryString["t"]);
        }

        public void ServiceRetrieve(string Studio,string Release,string hdsd,string Territories)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Platform");
                dt.Columns.Add("ScoredLabels");
                dt.Columns.Add("ScoredProbabilities");
                DataSet dss = new DataSet();
                dss = MasterPlannerRecords(Studio, Release, hdsd, Territories);

                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    var InvokeData = InvokeRequestResponseService(dss);
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
        }

        public async Task<string> InvokeRequestResponseService(DataSet dsval)
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
                    // Label6.Text = responseContent;
                    Console.WriteLine(responseContent);
                }


            }
            return result;
        }
    }
}