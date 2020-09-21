using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        bool du;
        protected void Page_Load(object sender, EventArgs e)
        {
            du = true;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int sec = int.Parse(Session["secretaria"].ToString());
            
            string gg= testeText.Text;
            string str = gg.Replace("\\", " ");
            // string test = gg.Replace("/", "");
            semaEntities ctx = new semaEntities();
            configuraco conf = ctx.configuracoes.First(p => p.secretariaID == sec);
            //conf.templateEmail = gg;
            ctx.configuracoes.Add(conf);
            ctx.SaveChanges();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

        }
    }
}