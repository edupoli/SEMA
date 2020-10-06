using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class AddDepartamentos : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            if (Session["perfil"].ToString() != "Administrador")
            {
                ClientScript.RegisterStartupScript(GetType(), "Popup", "acessoNegado();", true);
                Response.Redirect("login.aspx");
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
            Response.Redirect("home.aspx");
        }
    }
}