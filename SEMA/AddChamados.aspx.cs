using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
namespace SEMA
{
    using ServiceCorreios;
    using System.Web.Services;

    public partial class AddChamados : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        string e_mail;
        string valido;
        DateTime data = DateTime.Now;
        int LastID;
        protected void Page_Load(object sender, EventArgs e)
        {
            LastID = 0;
            e_mail = txtemail.Text;
            cepNotFound.Visible = false;
            btnNovoChamado.Visible = false;
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            
            if (!Page.IsPostBack)
            {
                // chama a API para gerar o numero do protocolo
                string numProtocolo;
                var request = (HttpWebRequest)WebRequest.Create("http://10.0.2.15/ctdws/integracao/protocolo/geraProtocolo?login=ctd&senha=integraws&idchamada=0&numdea=33793300&tipomidia=ura");
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                numProtocolo = responseString.TrimStart('"').TrimEnd('"');
                txtProtocolo.Text = numProtocolo.TrimStart('"').TrimEnd('"');

                // Metodo para popular a combobox 
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
            catch (System.Exception ex)
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
            if(descricao.Text.Length < 200)
            {
                valido = "nao";
                mensagem = "A descrição do chamado esta muito curta, deve conter no mínimo 200 caracteres !";
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
                    ch.status = cboxStatus.SelectedValue;
                    ctx.chamadoes.Add(ch);
                    ctx.SaveChanges();
                    LastID = ch.id;
                    if (LastID != 0)
                    {
                        pushMensage();
                    }

                   // WhatsApp();

                    
                    if (txtemail.Text != "")
                    {
                        Email();
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "Click", "temporaryButtonClick();", true);


                }
                catch (System.Exception ex)
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
            catch (System.Exception ex)
            {
                mensagem = "Ocorreu o seguinte erro ao tentar gravar o texto: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
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
        private void WhatsApp()
        {
            int sec = int.Parse(Session["secretaria"].ToString());
            semaEntities ctx = new semaEntities();
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
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://10.0.2.9:3000/api/" + numCel + "/" + dadosMensagem);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
            }
            catch (System.Exception ex)
            {
                mensagem = "Não foi possivel enviar a mensagem de confirmação pelo WhatsApp: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "info();", true);
            }
            
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
            catch (System.Exception ex)
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

        public static class ValidaCNPJ
        {
            public static bool IsCnpj(string cnpj)
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;
                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                if (cnpj.Length != 14)
                    return false;
                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cnpj.EndsWith(digito);
            }
        }

        protected void btnNovoChamado_Click(object sender, EventArgs e)
        {

        }

        protected void txttelefone_TextChanged(object sender, EventArgs e)
        {
            int tamanho = txttelefone.Text.Length;
            if (tamanho < 16)
            {
                mensagem = "O Celular informado é inválido: ";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                txttelefone.Focus();
            }
        }

        protected void txtCEP_TextChanged(object sender, EventArgs e)
        {
            getLogradouro(txtCEP.Text.Trim());
        }
        
        public void getLogradouro(string cep)
        {
        using (var ws = new AtendeClienteClient())
        {
        try
        {
          var resposta = ws.consultaCEP(cep);
          txtRua.Text = resposta.end;
          txtBairro.Text = resposta.bairro;
          txtCidade.Text = resposta.cidade +" "+ resposta.uf;
          cepNotFound.Visible = false;
          txtNumero.Focus();
        }
        catch (System.Exception)
        {
          cepNotFound.Visible = true;
          cepNotFound.Text = "CEP NÃO ENCONTRADO";

        }


        }
        }


        protected void txtcpf_TextChanged(object sender, EventArgs e)
        {
            if (txtcpf.Text.Length <=14)
            {
                if (ValidaCPF.IsCpf(txtcpf.Text) == false)
                {
                    mensagem = "O CPF informado é inválido: ";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    txtcpf.Focus();
                }
            }
            if (txtcpf.Text.Length > 14 && txtcpf.Text.Length <= 18)
            {
                if (ValidaCNPJ.IsCnpj(txtcpf.Text) == false)
                {
                    mensagem = "O CNPJ informado é inválido: ";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    txtcpf.Focus();
                }
            }
        }
    }
}