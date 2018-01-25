using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndependentPlannerML
{
    public partial class CommaSeparatedPCTs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string Result()
        {
            try
            {
                DataSet Dsunique = new DataSet();
                DataSet DsOutput = new DataSet();
                DataSet DsMasterPCTs = new DataSet();
                DataSet DsStatus = new DataSet();
                DataTable dtMatchStatus = new DataTable();
                string Platform = string.Empty;
                string MPlatform = string.Empty;
                string STATUS = string.Empty;
                string MatchStatus = string.Empty;

                DataTable dtFinal = new DataTable();
                dtFinal.Columns.Add("PCT");

                Dsunique = RetriveInputRecords();// Binding the unique Records for combination of Studio,Release,HDSD and Territories
                                                 // DsMasterPCTs = TBLMasterPlannerMasterChanges();// Getting Unique Records 127
                 if (Dsunique.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < Dsunique.Tables[0].Rows.Count; i++)
                    {
                        MatchStatus = Convert.ToString(Dsunique.Tables[0].Rows[i]["StudioType"]);

                        if (MatchStatus == "All")
                        {
                            DsOutput = RetrivePCTs(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                                Convert.ToString(MatchStatus));
                        }
                        else
                        {

                            DsOutput = RetrivePCTs(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                                Convert.ToString(MatchStatus));
                        }

                        string LastChar = string.Empty;
                        if (DsOutput.Tables[0].Rows.Count > 0)
                        {
                            // Insert Studio,Release, HDSD, Teritories and PCTs
                            string sb = "";

                            for (int j = 0; j < DsOutput.Tables[0].Rows.Count; j++)
                            {

                                Platform = Convert.ToString(DsOutput.Tables[0].Rows[j]["PCT"]);
                                sb += Platform + ",";

                            }
                            LastChar = sb.Remove(sb.Length - 1, 1);

                          
                            Insertrecordschanges(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                Convert.ToString(Dsunique.Tables[0].Rows[i]["Hotel"]),
                                LastChar);

                            //Insert each records one by one into Database
                            //int result = 0;

                            //for (int j = 0; j < DsMasterPCTs.Tables[0].Rows.Count; j++)
                            //{
                            //    MPlatform = Convert.ToString(DsMasterPCTs.Tables[0].Rows[j]["Platform"]) + ",";

                            //    //string value = MPlatform.CompareTo(LastChar);

                            //    result = sb.IndexOf(MPlatform.Trim());

                            //    if (result < 0)
                            //    {
                            //        STATUS = "N";
                            //        string LastChar = MPlatform.Remove(MPlatform.Length - 1, 1);
                            //        Insertrecordschanges(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                            //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                            //            Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                            //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                            //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Hotel"]),
                            //            LastChar, STATUS);
                            //    }
                            //    else
                            //    {
                            //        STATUS = "Y";
                            //        string LastChar = MPlatform.Remove(MPlatform.Length - 1, 1);
                            //        Insertrecordschanges(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                            //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                            //            Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                            //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                            //            Convert.ToString(Dsunique.Tables[0].Rows[i]["Hotel"]),
                            //            LastChar, STATUS);

                            //    }
                            //}


                        }
                        else
                        {
                            STATUS = "NA";
                            Insertrecordschanges(Convert.ToString(Dsunique.Tables[0].Rows[i]["Studio"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Release"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["HDSD"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Territories"]),
                                        Convert.ToString(Dsunique.Tables[0].Rows[i]["Hotel"]),
                                        STATUS);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //throw;
            }
            return null;
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
                dt.Rows[m][1] = rdr["StudioCategory"];

                m++;

            }
            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt);
            objsqlconn.Close();
            return dt;
        }


        // Find the PCTs 
        // Getting data from user input and retrun PCTs as an output.
        private DataSet RetrivePCTs(string Studio, string Release, string HDSD, string Territories, string StudioType)
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
        private int Insertrecordschanges(string Licensor, string Release, string HDSD, string Territories, string Hotel, string Platform)
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
           // SqlParameter property_releasedate = objcmd.Parameters.Add("@Select", SqlDbType.VarChar);
           // property_releasedate.Value = Select;

            //SqlParameter property_r = objcmd.Parameters.Add("@MoviesName", SqlDbType.VarChar);
            //property_r.Value = MoviesName;



            int i = objcmd.ExecuteNonQuery();
            objsqlconn.Close();
            return i;
        }
        protected void btngo_Click(object sender, EventArgs e)
        {
            try
            {
                Result();
            }
            catch (Exception)
            {


            }
        }


    }
}