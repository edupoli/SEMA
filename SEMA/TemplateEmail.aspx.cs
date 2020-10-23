using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SEMA
{
    public partial class TemplateEmail : System.Web.UI.Page
    {
        semaEntities ctx = new semaEntities();
        public string mensagem = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PreencherCbox();
            }
        }
        protected void cboxSecretaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            getConfiguracao();
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cboxSecretaria.SelectedValue == "selecione")
            {
                mensagem = "Favor Selecione a Secretaria!";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                cboxSecretaria.Focus();
            }
            else
            {
                int cod = int.Parse(cboxSecretaria.SelectedValue);
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.configuracoes where secretariaID=" + cod, con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                int result = dt.Rows.Count;
                if (result == 0)
                {
                    semaEntities ctx = new semaEntities();
                    configuraco cfg = new configuraco();
                    cfg.secretariaID = int.Parse(cboxSecretaria.SelectedValue);
                    cfg.smtp = textSMTP.Text.Trim();
                    cfg.porta = textPorta.Text.Trim();
                    cfg.email = textEmail.Text.Trim();
                    cfg.senhaEmail = textSenha.Text.Trim();
                    cfg.nomeRemetente = textNomeRemetente.Text.Trim();
                    cfg.assunto = textAssunto.Text.Trim();
                    cfg.bodyEmailAuto = textBodyEmailAuto.Value.Trim();
                    cfg.bodyEmailResposta = textBodyEmailResposta.Value.Trim();
                    ctx.configuracoes.Add(cfg);
                    ctx.SaveChanges();
                    mensagem = "Adicionado com sucesso !";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                if (result == 1)
                {
                    semaEntities ctx = new semaEntities();
                    configuraco cfg = ctx.configuracoes.First(p => p.secretariaID == cod);
                    cfg.secretariaID = int.Parse(cboxSecretaria.SelectedValue);
                    cfg.smtp = textSMTP.Text.Trim();
                    cfg.porta = textPorta.Text.Trim();
                    cfg.email = textEmail.Text.Trim();
                    cfg.senhaEmail = textSenha.Text.Trim();
                    cfg.nomeRemetente = textNomeRemetente.Text.Trim();
                    cfg.assunto = textAssunto.Text.Trim();
                    cfg.bodyEmailAuto = textBodyEmailAuto.Value.Trim();
                    cfg.bodyEmailResposta = textBodyEmailResposta.Value.Trim();
                    ctx.SaveChanges();
                    mensagem = "Alterado com sucesso !";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
            }
        }
        private void PreencherCbox()
        {
            semaEntities ctx = new semaEntities();
            var resultado = (from t in ctx.secretarias
                             orderby t.nome
                             select new
                             {
                                 t.id,
                                 t.nome,
                             });
            cboxSecretaria.Items.Insert(0, new ListItem("Selecione", "selecione"));
            foreach (var item in resultado)
            {
                cboxSecretaria.Items.Add(new ListItem(item.nome, item.id.ToString()));
            }
        }
        public void getConfiguracao()
        {
            try
            {
                int cod = int.Parse(cboxSecretaria.SelectedValue);
                configuraco conf = ctx.configuracoes.First(p => p.secretariaID == cod);
                textSMTP.Text = conf.smtp;
                textPorta.Text = conf.porta;
                textEmail.Text = conf.email;
                textSenha.Text = conf.senhaEmail;
                textNomeRemetente.Text = conf.nomeRemetente;
                textAssunto.Text = conf.assunto;
                textBodyEmailAuto.Value = conf.bodyEmailAuto;
                textBodyEmailResposta.Value = conf.bodyEmailResposta;
            }
            catch (Exception)
            {
                textSMTP.Text = "";
                textPorta.Text = "";
                textEmail.Text = "";
                textSenha.Text = "";
                textNomeRemetente.Text = "";
                textAssunto.Text = "";
                textBodyEmailAuto.Value = "";
                textBodyEmailResposta.Value = "";
                mensagem = "A Secretaria Selecionada ainda não tem Configurações Definidas";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "info();", true);
            }
        }
    }
}