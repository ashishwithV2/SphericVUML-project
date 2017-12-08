using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IndependentPlannerML
{
    public class Person
    {
        public int MOId
        {
            get;
            set;
        }
        public string Licensor
        {
            get;
            set;
        }
        //public string Release
        //{
        //    get;
        //    set;
        //}
        //public string HDSD
        //{
        //    get;
        //    set;
        //}


        //public string Territories
        //{
        //    get;
        //    set;
        //}
        //public string Hotel
        //{
        //    get;
        //    set;
        //}
        public string Platform
        {
            get;
            set;
        }
        //public string Select
        //{
        //    get;
        //    set;
        //}
    }
    public class parent
    {
        public string test
        {
            get;
            set;
        }
    }
    public partial class Testing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {



            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //string Root = (Directory.GetCurrentDirectory() + @"\app_data\Training_file.xlsx");


            //string startupPath = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
            //string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            //parent

            //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"app_data\Training_file.xlsx");
            //string[] files = File.ReadAllLines(path);
            //var path = Path.Combine(Directory.GetCurrentDirectory());
            //string json = "[" + "{ 'MOId': '1','Licensor': 'Premiere Digital Services, Inc.','Platform': 'TVN_HOTEL_INDEPENDENT_LIMITED_25'},{ 'MOId': '1','Licensor': 'Premiere Digital Services, Inc.','Platform': 'TVN_HOTEL_INDEPENDENT_LIMITED_25'}" + "]";

            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //dynamic item11 = serializer.Deserialize<Person>(json);
            //string platform101 = "";
            //foreach (var item229 in item11)
            //{
            //    platform101 = item229.Platform;
            //}
            //Label1.Text= platform101;
        }
    }
}