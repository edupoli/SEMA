using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class EditSecretarias : System.Web.UI.Page
    {
        string secretariaID;
        public string mensagem = string.Empty;
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            secretariaID = Request.QueryString["secretariaID"];
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                    if (Session["logado"] == null)
                    {
                        Response.Redirect("../../login.aspx");
                    }
                    else
                if (Session["perfil"].ToString() != "Administrador")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "acessoNegado();", true);
                        Response.Redirect("../../login.aspx");
                    }
                    getSecretaria(int.Parse(secretariaID));
                    prevPage = Request.UrlReferrer.ToString();
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
                    int cod = int.Parse(secretariaID);
                    semaEntities ctx = new semaEntities();
                    secretaria sec = ctx.secretarias.First(p => p.id == cod);
                    sec.nome = nome.Text.Trim();
                    ctx.SaveChanges();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                    throw;
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        private void getSecretaria(int cod)
        {
            semaEntities ctx = new semaEntities();
            secretaria sec = ctx.secretarias.First(p => p.id == cod);
            nome.Text = sec.nome;
        }
    }
}