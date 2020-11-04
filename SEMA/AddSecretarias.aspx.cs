using System;
using System.Web.UI;

namespace SEMA
{
    public partial class AddSecretarias : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        static string prevPage = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                prevPage = Request.UrlReferrer.ToString();

                if (Session["secretaria"].ToString() != "1")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "acessoNegado();", true);
                    Response.Redirect(prevPage);
                }
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (nome.Text == "")
            {
                mensagem = "Campo Nome é obrigatorio";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                nome.Focus();
            }
            else
            {
                try
                {
                    semaEntities ctx = new semaEntities();
                    secretaria sec = new secretaria();
                    sec.nome = nome.Text.Trim();
                    ctx.secretarias.Add(sec);
                    ctx.SaveChanges();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
    }
}