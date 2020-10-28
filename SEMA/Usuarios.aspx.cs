using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SEMA
{
    public partial class Usuarios : System.Web.UI.Page
    {
        int usuarioID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (!Page.IsPostBack)
            {
                int codSecretaria = int.Parse(Session["secretaria"].ToString());
                if (codSecretaria == 1)
                {
                    getUsuariosALL();
                }
                else
                {
                    getUsuarios();
                }
            }
        }
        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            usuarioID = int.Parse((sender as LinkButton).CommandArgument);
            Response.Redirect("ViewUsuarios.aspx?usuarioID=" + usuarioID);
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "Administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                usuarioID = int.Parse((sender as LinkButton).CommandArgument);
                Response.Redirect("EditUsuarios.aspx?usuarioID=" + usuarioID);
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
                    usuarioID = int.Parse((sender as LinkButton).CommandArgument);
                    semaEntities ctx = new semaEntities();
                    usuario user = ctx.usuarios.First(p => p.id == usuarioID);
                    ctx.usuarios.Remove(user);
                    ctx.SaveChanges();
                    getUsuarios();
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        private void getUsuarios()
        {
            int cod = int.Parse(Session["secretaria"].ToString());
            semaEntities ctx = new semaEntities();
            var resultado = (from a in ctx.usuarios
                             join b in ctx.secretarias on a.secretariaID equals b.id
                             where a.secretariaID == cod
                             orderby a.nome ascending
                             select new
                             {
                                 a.id,
                                 a.nome,
                                 a.email,
                                 a.login,
                                 a.senha,
                                 a.perfil,
                                 secretaria = b.nome,
                                 a.img,
                             });
            GridView1.DataSource = resultado.ToList();
            GridView1.DataBind();
        }
        private void getUsuariosALL()
        {
            int cod = int.Parse(Session["secretaria"].ToString());
            semaEntities ctx = new semaEntities();
            var resultado = (from a in ctx.usuarios
                             join b in ctx.secretarias on a.secretariaID equals b.id
                             orderby a.nome ascending
                             select new
                             {
                                 a.id,
                                 a.nome,
                                 a.email,
                                 a.login,
                                 a.senha,
                                 a.perfil,
                                 secretaria = b.nome,
                                 a.img,
                             });
            GridView1.DataSource = resultado.ToList();
            GridView1.DataBind();
        }
    }
}