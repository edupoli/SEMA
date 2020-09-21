using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (!Page.IsPostBack)
            {
                SB_Configuracao.Text = getConfiguracao();
                SB_logo.Text = getLogo();
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
        public string getConfiguracao()
        {
            StringBuilder sb = new StringBuilder();
            int cod = int.Parse(Session["secretaria"].ToString());
            semaEntities ctx = new semaEntities();
            configuraco conf = ctx.configuracoes.First(p => p.secretariaID == cod);
            sb.Append("<style>" +
                /* cor da barra de navegação superior*/
                ".main-header .navbar," +
                ".main-header li.user-header," +
                ".main-header .logo {" +
                "background-color: " + conf.bckColorNavbar + "!important;}" + // bckColorNavbar
                                                                              /* cor de backgroud do menu lateral */
                ".main-sidebar{" +
                "background-color: " + conf.bckColorMenu + "!important;}" + //bckColorMenu
                                                                            /* Cor do texto do menu lateral */
                ".sidebar-menu > li > a {" +
                "color: " + conf.textColorMenu + "!important;}" + //textColorMenu
                                                                  /* cor do efeito ao passar o mouse sobre os itens do menu lateral */
                ".sidebar-menu > li:hover > a," +
                ".sidebar-menu > li.active > a," +
                ".sidebar-menu > li.menu - open > a {" +
                "color: " + conf.onHovertexColorMenu + "!important; /* cor da fonte */" + // onHovertexColorMenu
                "background: " + conf.onHoverbckColorMenu + "!important; /* cor do fundo */}" + //onHoverbckColorMenu
                                                                                                /* cor de background dos submenus laterais  */
                ".sidebar-menu > li > .treeview-menu {" +
                "background: " + conf.bckColorSbMenu + "!important;}" + //bckColorSbMenu
                                                                        /* cor do texto dos submenus laterais*/
                ".sidebar-menu .treeview-menu > li > a {" +
                "color: " + conf.textColorSbMenu + "!important;}" + //textColorSbMenu
                                                                    /* cor do texto dos submenus ao passar o mouse */
                ".sidebar-menu .treeview-menu > li > a:hover {" +
                "color: " + conf.onHovertextColorSbMenu + "!important;}" + //onHovertextColorSbMenu
                "background: " + conf.onHoverbckColorSbMenu + "!important;" + // onHoverbckColorSbMenu
            "</style>");
            return sb.ToString();
        }
        public string getLogo()
        {
            StringBuilder sb = new StringBuilder();
            int cod = int.Parse(Session["secretaria"].ToString());
            semaEntities ctx = new semaEntities();
            configuraco conf = ctx.configuracoes.First(p => p.secretariaID == cod);
            sb.AppendLine("<img src='dist/img/logos/" + conf.logo + "' class='img-thumbnail' style='background-color: transparent'");
            return sb.ToString();
        }
    }
}