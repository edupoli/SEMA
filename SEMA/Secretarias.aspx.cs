using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class Secretarias : System.Web.UI.Page
    {
        string secretariaID;
        public string mensagem = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = GetSecretarias();
            GridView1.DataBind();
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            secretariaID = ((sender as LinkButton).CommandArgument);
            Response.Redirect("ViewSecretarias.aspx?secretariaID=" + secretariaID);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "Administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                secretariaID = ((sender as LinkButton).CommandArgument);
                Response.Redirect("EditSecretarias.aspx?secretariaID=" + secretariaID);
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "Administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                try
                {
                    int cod = Convert.ToInt32((sender as LinkButton).CommandArgument);
                    semaEntities ctx = new semaEntities();
                    secretaria sec = ctx.secretarias.First(p => p.id == cod);
                    ctx.secretarias.Remove(sec);
                    ctx.SaveChanges();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    GridView1.DataSource = GetSecretarias();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erroGeral();", true);
                }
            }
        }

        public List<secretaria> GetSecretarias()
        {
            var ctx = new semaEntities();
            return ctx.secretarias.ToList();

        }
    }
}