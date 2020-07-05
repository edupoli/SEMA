using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        string numProtocolo;


        protected void Page_Load(object sender, EventArgs e)
        {
            numProtocolo = string.Format("{0:00000000000000}", GerarProtocolo());
            e_mail = email.Text;
            if (!Page.IsPostBack)
            {
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.assunto order by descricao asc", con);
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
            
                if ((cpf.Text != "") && (ValidaCPF.IsCpf(cpf.Text) == false))
                {
                    valido = "nao";
                    mensagem = "O CPF informado é inválido !";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
                else

                    if ((email.Text != "") && (ValidaEmail.ValidarEmail(email.Text) == false))
                    {
                        valido = "nao";
                        mensagem = "O e-mail digitado esta incorreto !";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                        email.Focus();
                    }
                    else
                        {
                            valido = "sim";
                        }

            if (valido =="sim")
            {
                try
                {
                    semaEntities ctx = new semaEntities();
                    chamado ch = new chamado();
                    ch.protocolo = txtProtocolo.Text;
                    ch.nome = nome.Text;
                    ch.telefone = telefone.Text;
                    ch.email = email.Text;
                    ch.cpf = cpf.Text;
                    ch.assunto = cboxAssunto.SelectedValue;
                    ch.topico = cboxTopico.SelectedValue;
                    ch.descricao = descricao.Text;
                    ch.status = cboxStatus.SelectedValue;
                    ctx.chamadoes.Add(ch);
                    ctx.SaveChanges();
                    mensagem = "Adicionado com sucesso !";
                    if (email.Text != "")
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void cboxAssunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxAssunto.SelectedValue=="Selecione")
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
                string assunto = "Protocolo Nº " + numProtocolo + " Secretaria do Meio Ambiente Londrina-PR";
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(e_mail, "Secretaria do Meio Ambiente Londrina-PR");
                mailMessage.To.Add(e_mail.ToLower());
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<img src='https://i.ibb.co/L89Y9Yt/SEMA.png' /><br>RECEBEMOS A SUA SOLICITAÇÃO E EM BREVE SERÁ RESPONDIDA." + "<br>" +
                       "Protocolo: " + numProtocolo + "<br>" + "Nome: " + nome.Text + "<br>" +
                       " CPF: " + cpf.Text + "<br>" +
                       " Telefone para Contato: " + telefone.Text + "<br>" +
                       " Assunto: " + cboxAssunto.SelectedItem + "<br>" + cboxTopico.SelectedItem +"<br>"+
                       " Sua Mensagem:<br> " + descricao.Text + "<br><br>" +
                       " <strong>SECRETARIA MUNICIPAL DO AMBIENTE</strong><br>" +
                       " <strong>Localização:</strong> Rua da Natureza, 155 Jardim Piza" +
                       " <strong>CEP:</strong> 86041-050 Londrina-Paraná" +
                       " <strong>Telefone:</strong> Geral(43) 3372-4750  ou(43) 3372-4751" +
                       " <strong>E-mail:</strong> sema@londrina.pr.gov.br" +
                       " <strong>Horário de Atendimento:</strong> de segunda a sexta-feira, das 12h às 18h<br><br>" +

                       "*** E-mail automático, não há necessidade de respondê-lo. ***";

                mailMessage.Priority = MailPriority.High;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("sercomtelcontatcenter@gmail.com", "qtrlutilrbkfgwsf");
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