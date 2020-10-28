using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SEMA
{
    public partial class ViewSecretarias : System.Web.UI.Page
    {
        string secretariaID;
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            secretariaID = Request.QueryString["secretariaID"];
            if (!Page.IsPostBack)
            {
                getSecretarias(int.Parse(secretariaID));
                prevPage = Request.UrlReferrer.ToString();
            }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private void getSecretarias(int cod)
        {
            semaEntities ctx = new semaEntities();
            secretaria sec = ctx.secretarias.First(p => p.id == cod);
            nome.Text = sec.nome;
        }
    }
}