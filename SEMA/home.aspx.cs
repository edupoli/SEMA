using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class home : System.Web.UI.Page
    {
        public string[] Labels { get; set; }
        public int[] DataChamados { get; set; }
        public int[] Data3 { get; set; }
        public int[] Data4 { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Labels = new string[] { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
            Data3 = new int[] { 22, 39, 63, 45, 32, 53,25 };
            Data4 = new int[] { 32, 59, 43, 65, 22, 73,45 };
            DataChamados = new int[] { 22, 12, 8, 33 };

        }
    }
}