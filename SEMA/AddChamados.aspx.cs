using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SEMA
{
    public partial class AddChamados : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        string e_mail;
        string valido;
        public string numProtocolo;
        string prevPage;
        DateTime data = DateTime.Now;
        int LastID;
        protected void Page_Load(object sender, EventArgs e)
        {
            LastID = 0;
            numProtocolo = string.Format("{0:00000000000000}", GerarProtocolo());
            prevPage = Request.UrlReferrer.ToString();
            e_mail = txtemail.Text;
            if (!Page.IsPostBack)
            {
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.assunto where secretariaID=" + Session["secretaria"].ToString() + " order by descricao asc", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                cboxAssunto.DataSource = dt;
                cboxAssunto.DataBind();
                txtProtocolo.Text = numProtocolo;
                getStatusColor();
            }
            GerarProtocolo();
        }
        public Int64 GerarProtocolo()
        {
            try
            {
                DateTime data = new DateTime();
                data = DateTime.Today;
                string s = data.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
                return Convert.ToInt64(s);
            }
            catch (Exception ex)
            {
                mensagem = "Ocorreu o Seguinte erro: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                throw;
            }
        }
        private void getStatusColor()
        {
            for (int i = 0; i < cboxStatus.Items.Count; i++)
            {
                if (cboxStatus.SelectedValue == "Finalizado")
                {
                    string verde = "#478978";
                    cboxStatus.ForeColor = System.Drawing.Color.White;
                    cboxStatus.BackColor = System.Drawing.ColorTranslator.FromHtml(verde);
                }
                if (cboxStatus.SelectedValue == "Aberto")
                {
                    string azul = "#3C8DBC";
                    cboxStatus.ForeColor = System.Drawing.Color.White;
                    cboxStatus.BackColor = System.Drawing.ColorTranslator.FromHtml(azul);
                }
                if (cboxStatus.SelectedValue == "Em Atendimento")
                {
                    string laranja = "#F39C12";
                    cboxStatus.ForeColor = System.Drawing.Color.White;
                    cboxStatus.BackColor = System.Drawing.ColorTranslator.FromHtml(laranja);
                }
                if (cboxStatus.SelectedValue == "Pendente")
                {
                    string vermelho = "#DD4B39";
                    cboxStatus.ForeColor = System.Drawing.Color.White;
                    cboxStatus.BackColor = System.Drawing.ColorTranslator.FromHtml(vermelho);
                }
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (descricao.Text == "")
            {
                valido = "nao";
                mensagem = "Favor faça a descrição da solicitação !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                descricao.Focus();
            }
            else
            if (txtnome.Text == "")
            {
                valido = "nao";
                mensagem = "Favor informe o Nome !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                txtnome.Focus();
            }
            else
            if (txttelefone.Text == "")
            {
                valido = "nao";
                mensagem = "É necessáro informar um Telefone para contato !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                txttelefone.Focus();
            }
            else
            if ((txtcpf.Text != "") && (ValidaCPF.IsCpf(txtcpf.Text) == false))
            {
                valido = "nao";
                mensagem = "O CPF informado é inválido !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            if ((txtemail.Text != "") && (ValidaEmail.ValidarEmail(txtemail.Text) == false))
            {
                valido = "nao";
                mensagem = "O e-mail digitado esta incorreto !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                txtemail.Focus();
            }
            else
            if (cboxAssunto.SelectedItem.ToString() == "Selecione")
            {
                valido = "nao";
                mensagem = "Favor Selecionar o Assunto !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                cboxAssunto.Focus();
            }
            else
            if (cboxTopico.SelectedItem.ToString() == "Selecione")
            {
                valido = "nao";
                mensagem = "Favor Selecionar o Tópico !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                cboxTopico.Focus();
            }
            else
            {
                valido = "sim";
            }
            if (valido == "sim")
            {
                try
                {
                    semaEntities ctx = new semaEntities();
                    chamado ch = new chamado();
                    ch.secretariaID = int.Parse(Session["secretaria"].ToString());
                    ch.protocolo = txtProtocolo.Text;
                    ch.nome = txtnome.Text;
                    ch.telefone = txttelefone.Text;
                    ch.email = txtemail.Text;
                    ch.cpf = txtcpf.Text;
                    ch.img = "user-800x600.png";
                    ch.assunto = int.Parse(cboxAssunto.SelectedValue);
                    ch.topico = int.Parse(cboxTopico.SelectedValue);
                    //ch.descricao = descricao.Text;
                    ch.status = cboxStatus.SelectedValue;
                    ctx.chamadoes.Add(ch);
                    ctx.SaveChanges();
                    LastID = ch.id;
                    if (LastID != 0)
                    {
                        pushMensage();
                    }
                    int sec = int.Parse(Session["secretaria"].ToString());
                    secretaria sr = ctx.secretarias.First(p => p.id == sec);
                    string nomeSec = sr.nome;
                    string numCel = txttelefone.Text.Trim();
                    if (numCel.Length == 14)
                    {
                        numCel = numCel.Remove(0, 1);
                        numCel = numCel.Remove(2, 3);
                    }
                    string dadosMensagem = "👏🏻👏🏻 Sua solicitação foi aberta com Sucesso e registrada com Protocolo Nº *" + txtProtocolo.Text + "* ✅ " +
                    "A *" + nomeSec + "* irá analizar o pedido e respondera o mais breve possivel.\n" +
                    "Voce pode acompanhar o andamento da solicitação , com o numero de protocolo em todos os " +
                    "nossos canais de atendimento.\n\n\n" +
                    "Agradecemos por utilzar nossos serviços.\n\n" +
                    "👋🏼👋🏼 Ate logo.";
                    var request = (HttpWebRequest)WebRequest.Create("http://42ffaec75924.ngrok.io/api?celular=" + numCel + "&mensagem=" + dadosMensagem);
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    mensagem = "Adicionado com sucesso !";
                    if (txtemail.Text != "")
                    {
                        Email();
                    }
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        private void pushMensage()
        {
            try
            {
                semaEntities ctx = new semaEntities();
                historico his = new historico();
                his.chamadoID = LastID;
                his.mensagem = "<p>Enviada em: " + data + "</p></br>" + descricao.Text;
                his.sequencia = 0;
                his.origem = "cidadao";
                his.data = data;
                ctx.historicoes.Add(his);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                mensagem = "Ocorreu o seguinte erro ao tentar gravar o texto: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
        }
        protected void cboxAssunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxAssunto.SelectedValue == "Selecione")
            {
                cboxTopico.Items.Clear();
            }
            else
            {
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.topicos where assuntoID=" + cboxAssunto.SelectedValue + " order by descricao asc", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                cboxTopico.Items.Clear();
                cboxTopico.DataSource = dt;
                cboxTopico.DataBind();
            }
        }
        protected void cboxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStatusColor();
        }
        private void Email()
        {
            try
            {
                int sec = int.Parse(Session["secretaria"].ToString());
                semaEntities ctx = new semaEntities();
                configuraco cfg = ctx.configuracoes.First(p => p.secretariaID == sec);
                string protocolo = txtProtocolo.Text.Trim();
                string nome = txtnome.Text.Trim();
                string telefone = txttelefone.Text.Trim();
                string email = txtemail.Text.Trim();
                string cpf = txtcpf.Text.Trim();
                string assunto = cboxAssunto.SelectedItem.ToString();
                string topico = cboxTopico.SelectedItem.ToString();
                string status = cboxStatus.SelectedItem.ToString();
                string body = cfg.bodyEmailAuto.ToString();
                if (body.Contains("[nome]"))
                {
                    body = body.Replace("[nome]", txtnome.Text);
                }
                if (body.Contains("[protocolo]"))
                {
                    body = body.Replace("[protocolo]", txtProtocolo.Text);
                }
                if (body.Contains("[telefone]"))
                {
                    body = body.Replace("[telefone]", txttelefone.Text);
                }
                if (body.Contains("[email]"))
                {
                    body = body.Replace("[email]", txtemail.Text);
                }
                if (body.Contains("[cpf]"))
                {
                    body = body.Replace("[cpf]", txtcpf.Text);
                }
                if (body.Contains("[assunto]"))
                {
                    body = body.Replace("[assunto]", cboxAssunto.SelectedItem.ToString());
                }
                if (body.Contains("[topico]"))
                {
                    body = body.Replace("[topico]", cboxTopico.SelectedItem.ToString());
                }
                if (body.Contains("[status]"))
                {
                    body = body.Replace("[status]", cboxStatus.SelectedItem.ToString());
                }
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(e_mail, cfg.nomeRemetente);
                mailMessage.To.Add(e_mail.ToLower());
                mailMessage.Subject = cfg.assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.High;
                SmtpClient smtpClient = new SmtpClient(cfg.smtp, int.Parse(cfg.porta));
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(cfg.email, cfg.senhaEmail);
                smtpClient.Send(mailMessage);
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao enviar e-mail: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }
        public static class ValidaCPF
        {
            public static bool IsCpf(string cpf)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
        }
        public static class ValidaEmail
        {
            public static bool ValidarEmail(string email)
            {
                bool validEmail = false;
                int indexArr = email.IndexOf('@');
                if (indexArr > 0)
                {
                    int indexDot = email.IndexOf('.', indexArr);
                    if (indexDot - 1 > indexArr)
                    {
                        if (indexDot + 1 < email.Length)
                        {
                            string indexDot2 = email.Substring(indexDot + 1, 1);
                            if (indexDot2 != ".")
                            {
                                validEmail = true;
                            }
                        }
                    }
                }
                return validEmail;
            }
        }
    }
}