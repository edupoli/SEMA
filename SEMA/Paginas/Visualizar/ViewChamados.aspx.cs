using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SEMA
{
    public partial class ViewChamados : System.Web.UI.Page
    {
        int chamadoID;
        public string mensagem = string.Empty;
        string historico;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCaminhoImg.Visible = false;
            chamadoID = Convert.ToInt32(Request.QueryString["chamadoID"]);
            if (!Page.IsPostBack)
            {
                GetChamados(chamadoID);
                historicoMsg.Text = getHistorico(chamadoID);
            }
        }
        public void GetChamados(int cod)
        {
            semaEntities ctx = new semaEntities();
            var resultado = (from a in ctx.chamadoes
                             join b in ctx.assuntoes on a.assunto equals b.id
                             join c in ctx.topicos on a.topico equals c.id
                             join d in ctx.historicoes on a.id equals d.chamadoID
                             where a.id == cod
                             select new
                             {
                                 a.id,
                                 a.protocolo,
                                 a.nome,
                                 a.cpf,
                                 a.email,
                                 a.telefone,
                                 assunto = b.descricao,
                                 topico = c.descricao,
                                 a.status,
                                 a.img,
                                 d.mensagem
                             });
            foreach (var item in resultado)
            {
                txtProtocolo.Text = item.protocolo;
                nome.Text = item.nome;
                email.Text = item.email;
                cpf.Text = item.cpf;
                telefone.Text = item.telefone;
                Image1.ImageUrl = "/dist/img/chamados/" + item.img;
                imgSel.ImageUrl = "/dist/img/chamados/" + item.img;
                lblCaminhoImg.Text = item.img;
                cboxAssunto.Items.Add(new ListItem(item.assunto, item.assunto));
                cboxTopico.Items.Add(new ListItem(item.topico, item.topico));
                cboxStatus.Items.Add(new ListItem(item.status, item.status));
            }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/home.aspx");
        }
        private string getHistorico(int cod)
        {
            StringBuilder sb = new StringBuilder();
            semaEntities ctx = new semaEntities();
            var resultado = (from a in ctx.historicoes
                             where a.chamadoID == cod
                             orderby a.sequencia ascending
                             select new
                             {
                                 a.data,
                                 a.mensagem,
                                 a.sequencia,
                                 a.origem,
                             });
            foreach (var item in resultado)
            {
                if (item.origem == "cidadao")
                {
                    sb.Append("<div class='container1'><img src ='/dist/img/cidadao.jpg' alt='Avatar'>");
                    sb.Append(item.mensagem);
                    sb.Append("<span class='time-right'>" + item.data + "</span></div>");
                }
                if (item.origem == "agente")
                {
                    sb.Append("<div class='container1 darker'><img src ='/dist/img/sema.jpg' alt='Avatar' class='right'>");
                    sb.Append(item.mensagem);
                    sb.Append("<span class='time-left'>" + item.data + "</span></div>");
                }
                historico = historico + item.mensagem;
            }
            return sb.ToString();
        }
        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            ModalPlaceHolder.Visible = true;
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "UpdatePanel1StartupScript", "setTimeout('window.scrollTo(0,0)', 0);", true);
        }
        protected void btnModalCloseHeader_Click(object sender, EventArgs e)
        {
            ModalPlaceHolder.Visible = false;
        }
    }
}