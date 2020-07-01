using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                imgUser.ImageUrl = "dist/img/users/" + Session["img"].ToString();
                imgUser1.ImageUrl = "dist/img/users/" + Session["img"].ToString();
                lblNome.Text = Session["nome"].ToString();
                lblCargo.Text = Session["cargo"].ToString();
            }
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewUsuarios.aspx?usuarioID=" + Session["id"].ToString());
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}