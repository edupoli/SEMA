using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    using MySql.Data.MySqlClient;
    using ServiceCorreios;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;

    public partial class WebForm1 : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        string e_mail;
        string valido;
        int LastID;
        DateTime data = DateTime.Now;


        protected void Page_Load(object sender, EventArgs e)
        {
            LastID = 0;
            e_mail = txtemail.Text;
            cepNotFound.Visible = false;
            if (Session["logado"] == null)
            {
                Response.Redirect("../../login.aspx");
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
            if (descricao.Text.Length < 200)
            {
                valido = "nao";
                mensagem = "A descrição do chamado esta muito curta, deve conter no mínimo 200 caracteres !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                descricao.Focus();
            }
            else
            if (txtnome.Text == "" && checkDenuncia.Checked == false)
            {
                valido = "nao";
                mensagem = "Favor informe o Nome !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                txtnome.Focus();
            }
            else
            if (txttelefone.Text == "" && checkDenuncia.Checked == false)
            {
                valido = "nao";
                mensagem = "É necessáro informar um Telefone para contato !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                txttelefone.Focus();
            }
            else /*
            if ((txtcpf.Text != "") && (ValidaCPF.IsCpf(txtcpf.Text) == false))
            {
                valido = "nao";
                mensagem = "O CPF informado é inválido !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else */
            if ((txtemail.Text != "") && (ValidaEmail.ValidarEmail(txtemail.Text) == false) && (checkDenuncia.Checked == false))
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
                    ch.data = data;
                    ch.status = cboxStatus.SelectedValue;
                    ch.secretariaID = int.Parse(Session["secretaria"].ToString());
                    ch.protocolo = txtProtocolo.Text;
                    ch.nome = txtnome.Text;
                    ch.telefone = txttelefone.Text;
                    ch.email = txtemail.Text;
                    ch.cpf = txtcpf.Text;
                    ch.assunto = int.Parse(cboxAssunto.SelectedValue);
                    ch.topico = int.Parse(cboxTopico.SelectedValue);
                    ch.img = "user-800x600.png";
                    ch.cep = txtCEP.Text;
                    ch.rua = txtRua.Text;
                    ch.numero = txtNumero.Text;
                    ch.bairro = txtBairro.Text;
                    ch.cidade = txtCidade.Text;
                    ch.user_cadastrou = int.Parse(Session["id"].ToString());
                    if (checkDenuncia.Checked == true)
                    {
                        ch.anonimo = "SIM";
                    }
                    if (checkDenuncia.Checked == false)
                    {
                        ch.anonimo = "NAO";
                    }
                    ctx.chamadoes.Add(ch);
                    ctx.SaveChanges();
                    LastID = ch.id;
                    if (LastID != 0)
                    {
                        pushMensage();
                    }
                    WhatsApp();

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

        protected void txtCEP_TextChanged(object sender, EventArgs e)
        {
            getLogradouro(txtCEP.Text.Trim());
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

        protected void txtcpf_TextChanged(object sender, EventArgs e)
        {
            if (txtcpf.Text.Length <= 11)
            {
                if (ValidaCPF(txtcpf.Text) == false)
                {
                    mensagem = "O CPF informado é inválido: ";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    txtcpf.Text = string.Empty;
                    txtcpf.Focus();
                }
            }
            if (txtcpf.Text.Length > 11 && txtcpf.Text.Length <= 14)
            {
                if (ValidaCnpj(txtcpf.Text) == false)
                {
                    mensagem = "O CNPJ informado é inválido: ";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    txtcpf.Text = string.Empty;
                    txtcpf.Focus();
                }
            }
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
                    txtCidade.Text = resposta.cidade + " " + resposta.uf;
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

        private void WhatsApp()
        {
            int sec = int.Parse(Session["secretaria"].ToString());
            semaEntities ctx = new semaEntities();
            secretaria sr = ctx.secretarias.First(p => p.id == sec);
            string nomeSec = sr.nome;
            string numCel = txttelefone.Text.Trim();
            if (numCel.Length == 16)
            {
                numCel = numCel.Remove(0, 1);
                numCel = numCel.Remove(2, 4);
                numCel = numCel.Remove(6, 1);
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
        //Valida CPF
        private bool ValidaCPF(String cpf)
        {
            Boolean valida = true;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            String tempCpf;
            String digito;
            String verifica;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace("-", "").Replace(".", "");
            if (cpf.Length == 11)
            {
                verifica = cpf.Substring(9);
                tempCpf = cpf.Substring(0, 9);
                soma = 0;
                for (int i = 0; i < 9; i++)
                {
                    soma = soma + int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                }
                resto = soma % 11;
                resto = resto < 2 ? resto = 0 : resto = 11 - resto; ;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                {
                    soma = soma + int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                }
                resto = soma % 11;
                resto = resto < 2 ? resto = 0 : resto = 11 - resto; ;
                digito = digito + resto.ToString();
                if (digito != verifica)
                {
                    valida = false;
                }
                switch (cpf)
                {
                    case "00000000000":
                        {
                            valida = false;
                            break;
                        }
                    case "11111111111":
                        {
                            valida = false;
                            break;
                        }
                    case "  22222222222 ":
                        {
                            valida = false;
                            break;
                        }
                    case "33333333333":
                        {
                            valida = false;
                            break;
                        }
                    case "44444444444":
                        {
                            valida = false;
                            break;
                        }
                    case "55555555555":
                        {
                            valida = false;
                            break;
                        }
                    case "66666666666":
                        {
                            valida = false;
                            break;
                        }
                    case "77777777777":
                        {
                            valida = false;
                            break;
                        }
                    case "88888888888":
                        {
                            valida = false;
                            break;
                        }
                    case "99999999999":
                        {
                            valida = false;
                            break;
                        }
                }
            }
            else
            {
                valida = false;
            }
            return valida;
        }
        //Valida  CNPJ
        private bool ValidaCnpj(String cnpj)
        {
            Boolean valida = true;
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            String tempCnpj;
            String digito;
            String verifica;
            int soma;
            int resto;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace("/", "").Replace(".", "").Replace("-", "");
            if (cnpj.Length == 14)
            {
                verifica = cnpj.Substring(12);
                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;
                for (int i = 0; i < 12; i++)
                {
                    soma = soma + int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                }
                resto = soma % 11;
                resto = resto < 2 ? resto = 0 : resto = 11 - resto; ;
                digito = resto.ToString();
                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                {
                    soma = soma + int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                }
                resto = soma % 11;
                resto = resto < 2 ? resto = 0 : resto = 11 - resto; ;
                digito = digito + resto.ToString();
                if (digito != verifica)
                {
                    valida = false;
                }
                switch (cnpj)
                {
                    case "00000000000000":
                        {
                            valida = false;
                            break;
                        }
                    case "11111111111111":
                        {
                            valida = false;
                            break;
                        }
                    case "22222222222222":
                        {
                            valida = false;
                            break;
                        }
                    case "33333333333333":
                        {
                            valida = false;
                            break;
                        }
                    case "44444444444444":
                        {
                            valida = false;
                            break;
                        }
                    case "55555555555555":
                        {
                            valida = false;
                            break;
                        }
                    case "66666666666666":
                        {
                            valida = false;
                            break;
                        }
                    case "77777777777777":
                        {
                            valida = false;
                            break;
                        }
                    case "88888888888888":
                        {
                            valida = false;
                            break;
                        }
                    case "99999999999999":
                        {
                            valida = false;
                            break;
                        }
                }
            }
            else
            {
                valida = false;
            }
            return valida;
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

        protected void btnConfirmNovoChamado_Click(object sender, EventArgs e)
        {
            // chama a API para gerar o numero do protocolo
            string numProtocolo;
            var request = (HttpWebRequest)WebRequest.Create("http://10.0.2.15/ctdws/integracao/protocolo/geraProtocolo?login=ctd&senha=integraws&idchamada=0&numdea=33793300&tipomidia=ura");
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            response.Close();
            numProtocolo = responseString.TrimStart('"').TrimEnd('"');
            txtProtocolo.Text = numProtocolo.TrimStart('"').TrimEnd('"');

            txtnome.Text = string.Empty;
            txtemail.Text = string.Empty;
            txttelefone.Text = string.Empty;
            txtcpf.Text = string.Empty;
            cboxAssunto.SelectedIndex = 0;
            cboxTopico.Items.Clear();
            txtCEP.Text = string.Empty;
            txtRua.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtNumero.Text = string.Empty;
            descricao.Text = string.Empty;

        }

        protected void btnCancelNovoChamado_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void checkDenuncia_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDenuncia.Checked == true)
            {
                txtnome.Text = string.Empty;
                txtnome.Enabled = false;
                txttelefone.Text = string.Empty;
                txttelefone.Enabled = false;
                txtemail.Text = string.Empty;
                txtemail.Enabled = false;
                txtcpf.Text = string.Empty;
                txtcpf.Enabled = false;
                txtNumero.Text = string.Empty;
                txtNumero.Enabled = false;
                txtCEP.Text = string.Empty;
                txtCEP.Enabled = false;
            }
            else
            {
                txtnome.Enabled = true;
                txttelefone.Enabled = true;
                txtemail.Enabled = true;
                txtcpf.Enabled = true;
                txtNumero.Enabled = true;
                txtCEP.Enabled = true;
            }
        }
    }
}