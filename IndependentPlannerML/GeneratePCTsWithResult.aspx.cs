using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Collections;
using System.Globalization;

namespace IndependentPlannerML
{
    public partial class GeneratePCTsWithResult : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataSet ss = new DataSet();
            //DataTable dttss = new DataTable();
            if (!IsPostBack)
            {

                LicenserBind();
                ReleaseBind();
                hdsd();
                Territories();

                //dttss= GeneratePCTSForML();
                //ss= LocalDatabaseUnqiePCTs();
                //string dd = "jitendra";

                //  DataSet DsUnqe = new DataSet();
                //dt.Columns.Add("Platform", typeof(string));
                ////dt.Columns.Add("ScoredLabels");
                //dt.Columns.Add("ScoredProbabilities", typeof(string));
            
               
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet Dsgrivbind = new DataSet();
                string releaseCategory = string.Empty;
                string HDSDCategory = string.Empty;
                string TerritoriesCategory = string.Empty;
                ArrayList ARelase = new ArrayList();
                ArrayList AHDSD = new ArrayList();
                ArrayList ATerritories = new ArrayList();
                // binding releaase Data into ArrayList
                foreach (int i in Drprelease.GetSelectedIndices())
                {
                    ARelase.Add(Drprelease.Items[i].Text);
                }
                // binding HDSD Data into ArrayList
                foreach (int i in Drphdsd.GetSelectedIndices())
                {
                    AHDSD.Add(Drphdsd.Items[i].Text);
                }
                // binding Territories Data into ArrayList
                foreach (int i in DrpTerritories.GetSelectedIndices())
                {
                    ATerritories.Add(DrpTerritories.Items[i].Text);
                }
                releaseCategory = string.Join(",", ARelase.ToArray());// Join with comma seprated release category
                HDSDCategory = string.Join(",", AHDSD.ToArray());// Join with comma seprated release category
                TerritoriesCategory = string.Join(",", ATerritories.ToArray());//Join with comma seprated Territories category
                                                                               // Method 
                Dsgrivbind= MultipleRetrivePCTs(drpstudio.SelectedValue, releaseCategory, HDSDCategory, TerritoriesCategory);
                example.DataSource = Dsgrivbind;
                example.DataBind();


            }
            catch (Exception)
            {
               
            }
        }




        // Binding the Studio dropdown
        private void LicenserBind()
        {
            ListItemCollection collection = new ListItemCollection();
            //collection.Add(new ListItem("-----Select-----"));
            collection.Add(new ListItem("All Channel Films"));
            collection.Add(new ListItem("Brainstorm Media"));
            collection.Add(new ListItem("Breaking Glass Films"));
            collection.Add(new ListItem("Breaking Glass Pictures"));
            collection.Add(new ListItem("Broad Green Pictures"));
            collection.Add(new ListItem("Cinedigm Entertainment"));
            collection.Add(new ListItem("Cohen Media Group"));
            collection.Add(new ListItem("Electric Entertainment"));
            collection.Add(new ListItem("Epic Pictures"));
            collection.Add(new ListItem("Factory Film Studio"));
            collection.Add(new ListItem("Film Movement"));
            collection.Add(new ListItem("Freestyle Digital Media"));
            collection.Add(new ListItem("GoDigital"));
            collection.Add(new ListItem("Green Apple Entertainment"));
            collection.Add(new ListItem("Inception Digital Media"));
            collection.Add(new ListItem("Inception Media Group"));
            collection.Add(new ListItem("Juice Worldwide"));
            collection.Add(new ListItem("Lantern Lane"));
            collection.Add(new ListItem("Legendary"));
            collection.Add(new ListItem("Level 33 Entertainment"));
            collection.Add(new ListItem("Mar Vista Digital Entertainment"));
            collection.Add(new ListItem("Maxim Media"));
            collection.Add(new ListItem("Monterey Media"));
            collection.Add(new ListItem("MVD Entertainment"));
            collection.Add(new ListItem("Neon"));
            collection.Add(new ListItem("New City Releasing"));
            collection.Add(new ListItem("Premiere Digital"));
            collection.Add(new ListItem("Premiere Digital Services"));
            collection.Add(new ListItem("RLJ Entertainment"));
            collection.Add(new ListItem("Samuel Goldwyn Films"));
            collection.Add(new ListItem("Screen Media Films"));
            collection.Add(new ListItem("Screen Media Ventures"));
            collection.Add(new ListItem("Stonecutter"));
            collection.Add(new ListItem("STX Entertainment"));
            collection.Add(new ListItem("Syndicado"));
            collection.Add(new ListItem("The Asylum"));
            collection.Add(new ListItem("The Orchard"));
            collection.Add(new ListItem("Under the Milky Way"));
            collection.Add(new ListItem("Vertical Entertainment"));
            collection.Add(new ListItem("Virgil Films"));
            collection.Add(new ListItem("Vision Films"));
            collection.Add(new ListItem("Well Go, USA"));

            //Pass ListItemCollection as datasource
            drpstudio.DataSource = collection;
            drpstudio.DataBind();

        }
        // Bind the Release dropdownlist
        private void ReleaseBind()
        {
            ListItemCollection collection = new ListItemCollection();
            //collection.Add(new ListItem("Independent Limited; EST"));
            //collection.Add(new ListItem("Independent Limited; Charter"));
            //collection.Add(new ListItem("Independent Limited"));
            //collection.Add(new ListItem("Mini Major; IVOD; EST; Foreign Flix"));
            //collection.Add(new ListItem("Mini Major EST"));
            //collection.Add(new ListItem("Independent; Foreign Flix"));
            //collection.Add(new ListItem("In Theaters; Foreign Flix"));
            //collection.Add(new ListItem("In Theaters; Charter"));
            //collection.Add(new ListItem("Independent Limited; Charter; Foreign Flix"));
            //collection.Add(new ListItem("Foreign Flix"));
            //collection.Add(new ListItem("Independent Library"));
            //collection.Add(new ListItem("Independent"));
            //collection.Add(new ListItem("In Theaters; EST"));
            //collection.Add(new ListItem("Pre-Theatrical"));
            //collection.Add(new ListItem("Mini Major; EST; Foreign Flix"));
            //collection.Add(new ListItem("Mini Major; IVOD; EST"));
            //collection.Add(new ListItem("In Theaters"));
            //collection.Add(new ListItem("Mini Major"));
            //collection.Add(new ListItem("EST"));
            //collection.Add(new ListItem("Independent Limited; Foreign Flix"));
            //collection.Add(new ListItem("Passion Zone"));
            //collection.Add(new ListItem("Cine Central"));
            //collection.Add(new ListItem("In Theaters; IVOD; EST"));

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




            Drprelease.DataSource = collection;
            Drprelease.DataBind();

        }

        private void hdsd()
        {
            ListItemCollection collection = new ListItemCollection();
            // collection.Add(new ListItem("-----Select-----"));
            collection.Add(new ListItem("HD"));
            collection.Add(new ListItem("SD"));
            //collection.Add(new ListItem("2K"));
            Drphdsd.DataSource = collection;
            Drphdsd.DataBind();

        }
        private void Territories()
        {
            ListItemCollection collection = new ListItemCollection();
            // collection.Add(new ListItem("-----Select-----"));
            collection.Add(new ListItem("USA"));
            collection.Add(new ListItem("CAN"));
            collection.Add(new ListItem("CARIB"));
            //Pass ListItemCollection as datasource
            DrpTerritories.DataSource = collection;
            DrpTerritories.DataBind();
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

            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
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

        // Find the PCTs 
        // Getting data from user input and retrun PCTs as an output.
        private DataSet RetrivePCTs(string Studio,string Release,string HDSD, string Territories, string StudioType)
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("PCT");
            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_FetchRecords", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
           cmd.Parameters.Add(new SqlParameter("@Studio", Studio));
            cmd.Parameters.Add(new SqlParameter("@Release", Release));
            cmd.Parameters.Add(new SqlParameter("@HDSD", HDSD));
            cmd.Parameters.Add(new SqlParameter("@Territories", Territories));
            cmd.Parameters.Add(new SqlParameter("@Val", StudioType));
           

            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dt.Rows.Add();
                dt.Rows[m][0] = rdr["PCT"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }

        // Output 

        private string Result()
        {
            try
            {
                DataSet Dsunique = new DataSet();
                DataSet DsOutput = new DataSet();
                DataSet DsMasterPCTs = new DataSet();
                DataSet DsStatus = new DataSet();
                string Platform = string.Empty;
                string MPlatform = string.Empty;
                string STATUS = string.Empty;

                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("PCT");

                Dsunique = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
                DsMasterPCTs = TBLMasterPlannerMasterChanges();// Getting Unique Records 135
              
                if (Dsunique.Tables[0].Rows.Count>0)
                {
                    for (int i = 0; i < Dsunique.Tables[0].Rows.Count; i++)
                    {
                       
                        if (Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]) == "All")
                        {
                            DsOutput = RetrivePCTs(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));
                        }
                        else
                        {

                            DsOutput = RetrivePCTs(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));
                        }
                       

                        if (DsOutput.Tables[0].Rows.Count > 0)
                        {
                            // Insert Studio,Release, HDSD, Teritories and PCTs
                            string sb = "";

                            for (int j = 0; j < DsOutput.Tables[0].Rows.Count; j++)
                            {

                                Platform = Convert.ToString(DsOutput.Tables[0].Rows[j]["PCT"]);
                                sb += Platform + ",";

                            }
                            //string LastChar = sb.Remove(sb.Length - 1, 1);

                            //Insert each records one by one into Database
                            int result = 0;

                            for (int j = 0; j < DsMasterPCTs.Tables[0].Rows.Count; j++)
                            {
                                MPlatform = Convert.ToString(DsMasterPCTs.Tables[0].Rows[j]["Platform"])+ "," ;

                             //   MPlatform = Convert.ToString(DsMasterPCTs.Tables[0].Rows[j]["Platform"]);

                                //string value = MPlatform.CompareTo(LastChar);

                                result = sb.IndexOf(MPlatform.Trim());

                                if (result < 0)
                                {
                                    STATUS = "N";
                                    string LastChar = MPlatform.Remove(MPlatform.Length - 1, 1);
                                    Insertrecordschanges(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Hotel"]),
                                        LastChar,
                                        STATUS);
                                }
                                else
                                {
                                    STATUS = "Y";
                                    string LastChar = MPlatform.Remove(MPlatform.Length - 1, 1);
                                    Insertrecordschanges(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Hotel"]),
                                        LastChar,
                                        STATUS);

                                }
                            }


                        }
                    }
                }                

            }
            catch (Exception ex )
            {
                ex.Message.ToString();
                //throw;
            }
            return null;
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
        // Insert Code 
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

        // For Binding Status Records
        private DataSet StatusRec()
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("NameofFields");
           

            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_StatusRec", objsqlconn);
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
                dt.Rows[m][0] = rdr["NameofFields"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Result();
            }
            catch (Exception)
            {

             
            }
        }

        private string ResultofSingle(string Studio,string Release,string HDSD,string Territories)
        {
            try
            {
                DataSet Dsunique = new DataSet();
                DataSet DsOutput = new DataSet();
                DataSet DsMasterPCTs = new DataSet();
                DataSet DsStatus = new DataSet();
                string Platform = string.Empty;
                string MPlatform = string.Empty;
                string STATUS = string.Empty;

                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("PCT");

                //DsOutput = RetrivePCTs(drpstudio.SelectedValue,
                //                            Drprelease.SelectedValue,
                //                            Drphdsd.SelectedValue,
                //                            DrpTerritories.SelectedValue,
                //                            Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));


                //Dsunique = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
                //  DsMasterPCTs = TBLMasterPlannerMasterChanges();// Getting Unique Records 135



                //if (Dsunique.Tables[0].Rows.Count > 0)
                //{
                //    for (int i = 0; i < Dsunique.Tables[0].Rows.Count; i++)
                //    {

                //        if (Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]) == "All")
                //        {

                //                DsOutput = RetrivePCTs(drpstudio.SelectedValue,
                //                            Drprelease.SelectedValue,
                //                            Drphdsd.SelectedValue,
                //                            DrpTerritories.SelectedValue,
                //                            Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));
                //        }
                //        else
                //        {

                //            DsOutput = RetrivePCTs(drpstudio.SelectedValue,
                //                            Drprelease.SelectedValue,
                //                            Drphdsd.SelectedValue,
                //                            DrpTerritories.SelectedValue,
                //                            Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));
                //        }

                //        example.DataSource = DsOutput;
                //        example.DataBind();


                //        //if (DsOutput.Tables[0].Rows.Count > 0)
                //        //{
                //        // Insert Studio,Release, HDSD, Teritories and PCTs
                //        //string sb = "";

                //        //for (int j = 0; j < DsOutput.Tables[0].Rows.Count; j++)
                //        //{

                //        //    Platform = Convert.ToString(DsOutput.Tables[0].Rows[j]["PCT"]);
                //        //    sb += Platform + ",";

                //        //}
                //        // string LastChar = sb.Remove(sb.Length - 1, 1);

                //        //Insert each records one by one into Database
                //        //  int result = 0;

                //        //for (int j = 0; j < DsMasterPCTs.Tables[0].Rows.Count; j++)
                //        //{
                //        //    MPlatform = Convert.ToString(DsMasterPCTs.Tables[0].Rows[j]["Platform"]);

                //        //    //string value = MPlatform.CompareTo(LastChar);

                //        //    result = LastChar.IndexOf(MPlatform.Trim());

                //        //    if (result < 0)
                //        //    {
                //        //        STATUS = "N";
                //        //        Insertrecordschanges(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                //        //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                //        //            Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                //        //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                //        //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Hotel"]),
                //        //            MPlatform, STATUS);
                //        //    }
                //        //    else
                //        //    {
                //        //        STATUS = "Y";
                //        //        Insertrecordschanges(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                //        //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                //        //            Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                //        //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                //        //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Hotel"]),
                //        //            MPlatform, STATUS);

                //        //    }
                //        //}


                //        //}
                //    }
                //}

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //throw;
            }
            return null;
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

        public async Task<string> InvokeRequestUnitTest(DataSet dsval, string APIKEY, string APIURL)
        {
            List<string> arr = new List<string>();

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
                            {              {
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


                //        const string apiKey = "L7USA2DaVFdA913UDQO8UkhzOBzs1HIKk0SCFdIkl39npHxmsz1yW1Oj7ca8TbF6av7sAwPWuJ8hqFmO6ITCRw=="; // Replace this with the API key for the web service
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                //    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/2c9d645c7c874a329efa3850e3808afe/services/9e9fc70bbaa04d6f9782b638c7d28b25/execute?api-version=2.0&format=swagger");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                // ##################### API KEY and URL Dynamic Binding

                string apiKey = APIKEY;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri(APIURL);

                // ######################


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest).ConfigureAwait(false);
                string len = Convert.ToString(response.Content.Headers.ContentLength);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {

                    string responseContent = await response.Content.ReadAsStringAsync();

                }


            }
            return result;
        }
        public async Task<string> InvokeRequestResponseService(DataSet dsval)
        {
            List<string> arr = new List<string>();
          
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
                            {              {
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
                }
                else
                {
                
                    string responseContent = await response.Content.ReadAsStringAsync();
               
                }


            }
            return result;
        }

        // written a method for consuming ML Web service  
        public  DataTable GeneratePCTSForML()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Platform", typeof(string));
            //dt.Columns.Add("ScoredLabels");
            dt.Columns.Add("ScoredProbabilities", typeof(string));
            DataSet DsUnqe = new DataSet();
            DataSet Dsbindunque = new DataSet();
            DsUnqe = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
            if (DsUnqe.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DsUnqe.Tables[0].Rows.Count; i++)
                {

                   
                    Dsbindunque = MasterPlannerRecords(
                       Convert.ToString(DsUnqe.Tables[0].Rows[i]["Studio"]),
                       Convert.ToString(DsUnqe.Tables[0].Rows[i]["Release"]),
                       Convert.ToString(DsUnqe.Tables[0].Rows[i]["HDSD"]),
                       Convert.ToString(DsUnqe.Tables[0].Rows[i]["Territories"])
                        );
                    if (Dsbindunque != null && Dsbindunque.Tables[0].Rows.Count > 0)
                    {
                        int j = 0;
                        var InvokeData = InvokeRequestResponseService(Dsbindunque);
                        var obj = JObject.Parse(InvokeData.Result);
                        var Val = obj["Results"]["output1"];
                        var serializer = new JavaScriptSerializer();
                        dynamic usr = serializer.DeserializeObject(Convert.ToString(Val));

                        double d2 = 1.00;
                        foreach (var item in usr)
                        {


                            string platform = string.Empty;
                            string platformvalue = string.Empty;
                            string scoredlabels = string.Empty;
                            string scoredprobabilities = string.Empty;
                            string result = string.Empty;
                            int p200 = 0;

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
                                    dt.Rows[i]["platform"] = platformvalue;
                                    //dt.Rows[i]["scoredlabels"] = scoredlabels;
                                    dt.Rows[i]["scoredprobabilities"] = d2;
                                    i++;
                                }

                            }
                            else
                            {
                                Double myInt = Convert.ToDouble(scoredprobabilities);
                                result = myInt.ToString("#0.##");
                                double per = Convert.ToDouble(result) * 100;
                                p200 = Convert.ToInt32(per);

                                //if (scoredlabels == "Y" && p200 > 60)
                                //{
                                //    dt.Rows.Add();// Adding row into datatable
                                //    dt.Rows[i]["platform"] = platformvalue;
                                //    dt.Rows[i]["scoredprobabilities"] = Math.Round(myInt, 2);
                                //    i++;
                                //}
                                //else
                                //{

                                //}
                                if (scoredlabels == "Y")
                                {
                                    dt.Rows.Add();// Adding row into datatable
                                    dt.Rows[i]["platform"] = platformvalue;
                                    dt.Rows[i]["scoredprobabilities"] = Math.Round(myInt, 2);
                                    i++;
                                }
                                else
                                {

                                }

                            }



                        }
                    }
                }



            }
            return dt;
        }

        public  DataSet LocalDatabaseUnqiePCTs()
        {
            DataSet DsOutput = new DataSet();
            try
            {
                DataSet Dsunique = new DataSet();
                DataSet DsMasterPCTs = new DataSet();
                DataSet DsStatus = new DataSet();
                string Platform = string.Empty;
                string MPlatform = string.Empty;
                string STATUS = string.Empty;

                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("PCT");

                Dsunique = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
                DsMasterPCTs = TBLMasterPlannerMasterChanges();// Getting Unique Records 135

                if (Dsunique.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < Dsunique.Tables[0].Rows.Count; i++)
                    {
                       
                        if (Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]) == "All")
                        {

                            DsOutput = RetrivePCTs(
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));
                         }
                        else
                        {

                            DsOutput = RetrivePCTs(
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));
                        }

               
                    }
                }

            }
            catch (Exception)
            {
              
            }
            return DsOutput;
        }
        //public string Local()
        //{
        //    DataSet DsUnqe = new DataSet();
        //   DsUnqe = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
        //                                   //string column2 = "TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED,TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED_HD,TVN_HOTEL_INDEPENDENT_LIMITED_25,TVN_HOTEL_INDEPENDENT_LIMITED_H264,TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_25,TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_H264,TVN_INDEPENDENT_HIGH_DEF_ABR,TVN_INDEPENDENT_LIMITED_10,TVN_INDEPENDENT_LIMITED_H264_10,TVN_INDEPENDENT_LIMITED_HD_MEZZ_AVC30,TVN_INDEPENDENT_LIMITED_HIGH_DEF,TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264,TVN_INDEPENDENT_LIMITED_HIGH_DEF_SINGLE_SCREEN_VZ,TVN_INDEPENDENT_LIMITED_SINGLE_SCREEN_VZ";
        //                                     string column2 = Convert.ToString(DsUnqe.Tables[0].Rows[0][0]);
        //    //string column2 = "jitendra";
        //    return column2;
        //}
        //public string Localmain()
        //{
        //    DataSet DsUnqe = new DataSet();
        //    DsUnqe = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
        //                                   //string column1 = "TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED,TVN_FTR_SINGLESCREEN_INDEPENDENT_LIMITED_HD,TVN_HOTEL_INDEPENDENT_LIMITED_25,TVN_HOTEL_INDEPENDENT_LIMITED_H264,TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_25,TVN_HOTEL_INDEPENDENT_LIMITED_HIGH_DEF_H264,TVN_INDEPENDENT_HIGH_DEF_ABR,TVN_INDEPENDENT_LIMITED_10,TVN_INDEPENDENT_LIMITED_H264_10,TVN_INDEPENDENT_LIMITED_HD_MEZZ_AVC30,TVN_INDEPENDENT_LIMITED_HIGH_DEF,TVN_INDEPENDENT_LIMITED_HIGH_DEF_H264,TVN_INDEPENDENT_LIMITED_HIGH_DEF_SINGLE_SCREEN_VZ,TVN_INDEPENDENT_LIMITED_SINGLE_SCREEN_VZ";
        //                                    string column1 = Convert.ToString(DsUnqe.Tables[0].Rows[0][0]);
        //    //string column1 = "jitendra";
        //    return column1;
        //}

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

        public DataTable GeneratePCTSForMLWithParameter(string Studio, string Release, string HDSD, string Territories, string Hotel, string StudioType)
        {
            DataTable dt = new DataTable();
            DataSet DsUnqe = new DataSet();
            dt.Columns.Add("Platform", typeof(string));
            dt.Columns.Add("ScoredLabels", typeof(string));
            dt.Columns.Add("ScoredProbabilities", typeof(string));
            DataTable dty = new DataTable();
            DataTable DtAPI = new DataTable();
            DataTable StudioTyped = new DataTable();

            dty.Columns.Add("Studio");
            dty.Columns.Add("Release");
            dty.Columns.Add("HDSD");
            dty.Columns.Add("Territories");
            dty.Columns.Add("Hotel");
            dty.Columns.Add("StudioType");


            dty.Rows.Add();// Adding row into datatable
            dty.Rows[0]["Studio"] = Studio;
            dty.Rows[0]["Release"] = Release;
            dty.Rows[0]["HDSD"] = HDSD;
            dty.Rows[0]["Territories"] = Territories;
            dty.Rows[0]["Hotel"] = Hotel;
            dty.Rows[0]["StudioType"] = StudioType;
            DsUnqe.Tables.Add(dty);

            

            DataSet Dsbindunque = new DataSet();
           // DsUnqe = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
            if (DsUnqe.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < DsUnqe.Tables[0].Rows.Count; i++)
                {


                    Dsbindunque = MasterPlannerRecords(
                       Convert.ToString(DsUnqe.Tables[0].Rows[i]["Studio"]),
                       Convert.ToString(DsUnqe.Tables[0].Rows[i]["Release"]),
                       Convert.ToString(DsUnqe.Tables[0].Rows[i]["HDSD"]),
                       Convert.ToString(DsUnqe.Tables[0].Rows[i]["Territories"])
                        );
                    // get the studio name and return studio type such as "ALL" or "PRORES" and "AVC30" 
                    StudioTyped = StudioWiseCategory(Convert.ToString(DsUnqe.Tables[0].Rows[i]["Studio"]));
                    // Given release Category and Studio type as Input and will take API keys and URL.
                    DtAPI = ReteiveAPIKeyBASEAddress(Convert.ToString(DsUnqe.Tables[0].Rows[i]["Release"]), Convert.ToString(StudioTyped.Rows[0]["StudioCategory"]));
                    if (Dsbindunque != null && Dsbindunque.Tables[0].Rows.Count > 0)
                    {
                        int j = 0;
                        var InvokeData = InvokeRequestUnitTest(Dsbindunque, Convert.ToString(DtAPI.Rows[0]["RT_Key"]), Convert.ToString(DtAPI.Rows[0]["RT_URL"]));
                        var obj = JObject.Parse(InvokeData.Result);
                        var Val = obj["Results"]["output1"];
                        var serializer = new JavaScriptSerializer();
                        dynamic usr = serializer.DeserializeObject(Convert.ToString(Val));

                        foreach (var item in usr)
                        {


                            string platform = string.Empty;
                            string platformvalue = string.Empty;
                            string scoredlabels = string.Empty;
                            string scoredprobabilities = string.Empty;
                            string result = string.Empty;
                            int p200 = 0;
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
                                Double myInt = Convert.ToDouble(scoredprobabilities);
                                result = myInt.ToString("#0.##");
                                double per = Convert.ToDouble(result) * 100;
                                p200 = Convert.ToInt32(per);

                                // // Gets a NumberFormatInfo associated with the en-US culture.
                                //NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                                // // Displays a value with the default number of decimal digits (2).
                                // Double myInt = Convert.ToDouble(scoredprobabilities);
                                // int val = Convert.ToInt32(myInt.ToString("P", nfi).Substring(0, 1));

                                // if (val != 0 && val > 2)
                                // {
                                //     dt.Rows.Add();// Adding row into datatable
                                //     dt.Rows[i]["platform"] = platformvalue;
                                //     dt.Rows[i]["scoredlabels"] = scoredlabels;
                                //     dt.Rows[i]["scoredprobabilities"] = myInt.ToString("P", nfi);
                                //     i++;
                                //     // dt.Rows.Add();// Adding row into datatable
                                // }
                                // else
                                // {

                                // }

                                if (scoredlabels == "Y")
                                {
                                    dt.Rows.Add();// Adding row into datatable
                                    dt.Rows[i]["platform"] = platformvalue;
                                    dt.Rows[i]["scoredlabels"] = scoredlabels;
                                    dt.Rows[i]["scoredprobabilities"] = Convert.ToString(Math.Round(myInt, 2)* 100 +"%");
                                    i++;
                                }
                                else
                                {

                                }
                            }



                        }

                    }
                }



            }
            return dt;
        }

        public DataSet LocalDatabaseUnqiePCTswithParameter(string Studio, string Release, string HDSD, string Territories, string Hotel, string StudioType)
        {
            DataSet DsOutput = new DataSet();
            try
            {
                DataSet Dsunique = new DataSet();
                DataSet DsMasterPCTs = new DataSet();
                DataSet DsStatus = new DataSet();
                string Platform = string.Empty;
                string MPlatform = string.Empty;
                string STATUS = string.Empty;

                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("PCT");

                DataTable dty = new DataTable();

                dty.Columns.Add("Studio");
                dty.Columns.Add("Release");
                dty.Columns.Add("HDSD");
                dty.Columns.Add("Territories");
                dty.Columns.Add("Hotel");
                dty.Columns.Add("StudioType");


                dty.Rows.Add();// Adding row into datatable
                dty.Rows[0]["Studio"] = Studio;
                dty.Rows[0]["Release"] = Release;
                dty.Rows[0]["HDSD"] = HDSD;
                dty.Rows[0]["Territories"] = Territories;
                dty.Rows[0]["Hotel"] = Hotel;
                dty.Rows[0]["StudioType"] = StudioType;
                Dsunique.Tables.Add(dty);

                //Dsunique = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
                DsMasterPCTs = TBLMasterPlannerMasterChanges();// Getting Unique Records 135

                if (Dsunique.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < Dsunique.Tables[0].Rows.Count; i++)
                    {

                        if (Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]) == "All")
                        {

                            DsOutput = RetrivePCTs(
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                                   Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));
                        }
                        else
                        {

                            DsOutput = RetrivePCTs(
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                                      Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]));
                        }


                    }
                }

            }
            catch (Exception)
            {

            }
            return DsOutput;
        }

        private int InsertPCTsMatch(string MStudio, string Release, string HDSD, string Territories, string MatchUnMatchedStatus)
        {

            string connectionnstring = "";
            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();

            SqlCommand objcmd = new SqlCommand("USP_PCTsMatch", objsqlconn);
            objcmd.CommandTimeout = 12000;
            objcmd.CommandType = CommandType.StoredProcedure;


            //SqlParameter property_url = objcmd.Parameters.Add("@Mmurl", SqlDbType.VarChar);
            //property_url.Value = Url;

            SqlParameter property_postid = objcmd.Parameters.Add("@MStudio", SqlDbType.VarChar);
            property_postid.Value = MStudio;

            SqlParameter property_name = objcmd.Parameters.Add("@MRelase", SqlDbType.VarChar);
            property_name.Value = Release;

            SqlParameter property_desc = objcmd.Parameters.Add("@MHDSD", SqlDbType.VarChar);
            property_desc.Value = HDSD;

            SqlParameter property_rating = objcmd.Parameters.Add("@MTerritories", SqlDbType.VarChar);
            property_rating.Value = Territories;
            SqlParameter property_totrating = objcmd.Parameters.Add("@MMatchUnMatched", SqlDbType.VarChar);
            property_totrating.Value = MatchUnMatchedStatus;
            
            int i = objcmd.ExecuteNonQuery();
            objsqlconn.Close();
            return i;
        }

        // [ExpectedException(typeof(System.NullReferenceException))]
        public void OutputMatchUnmatched(string Studio, string Release, string HDSD, string Territories,string Hotel, string StudioType)
        {
            try
            {

                // defined system object
                DataTable DtExpected = new DataTable();
                DataSet DsActual = new DataSet();
               
                // Expected Value
                DtExpected = GeneratePCTSForMLWithParameter(Studio, Release, HDSD, Territories, Hotel, StudioType);
                //Actual OutPut
                DsActual = LocalDatabaseUnqiePCTswithParameter(Studio, Release, HDSD, Territories, Hotel, StudioType);
                DtExpected.DefaultView.Sort = "platform";
                DtExpected = DtExpected.DefaultView.ToTable();

                DsActual.Tables[0].DefaultView.Sort = "PCT";
                DsActual.Tables[0].DefaultView.ToTable();

                string Predict = ConvertDatatableToCommaSeperatedValues(DtExpected, "Platform");
                string Actual = ConvertDataSetToCommaSeperatedValues(DsActual, "PCT");

                int MatchCount = Actual.IndexOf(Actual);
                if(MatchCount==0)
                {
                    InsertPCTsMatch(Studio, Release, HDSD, Territories, "Success");
                }
                else
                {
                    if (Predict != "")
                    {
                        InsertPCTsMatch(Studio, Release, HDSD, Territories, "Failed");
                    }
                    else
                    {
                        InsertPCTsMatch(Studio, Release, HDSD, Territories, "NotExists");
                    }
                }
              

                //dt.DefaultView.Sort = Column;

                //if (Predict == Actual)
                //{

                //    InsertPCTsMatch(Studio, Release, HDSD, Territories, "Success");
                //}
                //else
                //{
                //    if (Predict != "")
                //    {
                //        InsertPCTsMatch(Studio, Release, HDSD, Territories, "Failed");
                //    }
                //    else
                //    {
                //        InsertPCTsMatch(Studio, Release, HDSD, Territories, "NotExists");
                //    }

                //}


                //Assert.AreEqual(Actual, DtExpected);
                // Assert.AreEqual(act, act1);

            }
            catch (Exception ex)
            {

                //throw;
            }


        }
        public string ConvertDatatableToCommaSeperatedValues(DataTable dt, string Column)
        {
            dt.DefaultView.Sort = Column;
            dt = dt.DefaultView.ToTable();
            var SelectedValues = dt.AsEnumerable().Select(s => s.Field<string>(Column)).ToArray();
            string commaSeperatedValues = string.Join(",", SelectedValues);
            return commaSeperatedValues;
        }

        public string ConvertDataSetToCommaSeperatedValues(DataSet ds, string Column)
        {
            DataTable dtd = new DataTable();
            ds.Tables[0].DefaultView.Sort = Column;
            ds.Tables[0].DefaultView.ToTable();
            var SelectedValues = ds.Tables[0].AsEnumerable().Select(s => s.Field<string>(Column)).ToArray();
            string commaSeperatedValues = string.Join(",", SelectedValues);
            return commaSeperatedValues;
        }
        public void run()
        {
            DataSet DSS = new DataSet();
            DSS = RetriveInputRecords();
            for (int i = 0; i < DSS.Tables[0].Rows.Count; i++)
            {
                OutputMatchUnmatched(Convert.ToString(DSS.Tables[0].Rows[i]["Studio"]),
                            Convert.ToString(DSS.Tables[0].Rows[i]["Release"]),
                            Convert.ToString(DSS.Tables[0].Rows[i]["HDSD"]),
                            Convert.ToString(DSS.Tables[0].Rows[i]["Territories"]),
                            Convert.ToString(DSS.Tables[0].Rows[i]["Hotel"]),
                            Convert.ToString(DSS.Tables[0].Rows[i]["StudioType"])
                           );
                         
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                run();
            }
            catch (Exception)
            {

                //throw;
            }
        }

        // Retrieving PCTs from database for multiple user input selection
        private DataSet MultipleRetrivePCTs(string Studio, string Release, string HDSD, string Territories)
        {
            string connectionnstring = "";
            SqlDataReader rdr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("PCT");
            connectionnstring = ConfigurationManager.ConnectionStrings["Conn"].ToString();
            SqlConnection objsqlconn = new SqlConnection(connectionnstring);
            objsqlconn.Open();
            // 1. create a command object identifying
            // the stored procedure
            SqlCommand cmd = new SqlCommand("USP_RetriveSTudioType", objsqlconn);
            cmd.CommandTimeout = 12000;

            // 2. set the command object so it knows
            // to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which
            // will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@Studio", Studio));
            cmd.Parameters.Add(new SqlParameter("@Release", Release));
            cmd.Parameters.Add(new SqlParameter("@HDSD", HDSD));
            cmd.Parameters.Add(new SqlParameter("@Territories", Territories));
          


            // execute the command
            rdr = cmd.ExecuteReader();

            // iterate through results, printing each to console
            int m = 0;
            while (rdr.Read())
            {
                dt.Rows.Add();
                dt.Rows[m][0] = rdr["PCT"];
                m++;

            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            objsqlconn.Close();
            return ds;
        }
    }
}