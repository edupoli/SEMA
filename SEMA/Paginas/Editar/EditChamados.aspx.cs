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
    public partial class EditChamados : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        string valido;
        string numProtocolo;
        DateTime data = DateTime.Now;
        int chamadoID;

        protected void Page_Load(object sender, EventArgs e)
        {
            chamadoID = Convert.ToInt32(Request.QueryString["chamadoID"]);
            
            if (!Page.IsPostBack)
            {
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.assunto where sema.assunto.secretariaID="+ Session["secretaria"].ToString() +" order by descricao asc", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                cboxAssunto.DataSource = dt;
                cboxAssunto.DataBind();
                txtProtocolo.Text = numProtocolo;

                getStatusColor();
                getTopicos();
                GetChamados(chamadoID,int.Parse(Session["secretaria"].ToString()));
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
        public void GetChamados(int cod, int secretaria)
        {
            semaEntities ctx = new semaEntities();
            var resultado = (from a in ctx.chamadoes
                             join b in ctx.assuntoes on a.assunto equals b.id
                             join c in ctx.topicos on a.topico equals c.id
                             join d in ctx.historicoes on a.id equals d.chamadoID
                             where a.id == cod
                             where a.secretariaID == secretaria
                             select new
                             {
                                 a.id,
                                 a.secretariaID,
                                 a.protocolo,
                                 a.nome,
                                 a.cpf,
                                 a.email,
                                 a.telefone,
                                 a.assunto,
                                 a.topico,
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
                cboxAssunto.SelectedValue = Convert.ToString(item.assunto);
                getTopicos();
                cboxTopico.SelectedValue = Convert.ToString(item.topico);
                cboxStatus.SelectedValue = item.status;
                descricao.Text = item.mensagem;
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

            if (valido == "sim")
            {
                try
                {
                    semaEntities ctx = new semaEntities();
                    chamado ch = ctx.chamadoes.First(p => p.id == chamadoID);
                    ch.protocolo = txtProtocolo.Text;
                    ch.nome = nome.Text;
                    ch.telefone = telefone.Text;
                    ch.email = email.Text;
                    ch.cpf = cpf.Text;
                    ch.img = "user-800x600.png";
                    ch.assunto = int.Parse(cboxAssunto.SelectedValue);
                    ch.topico = int.Parse(cboxTopico.SelectedValue);
                    ch.status = cboxStatus.SelectedValue;
                    ctx.SaveChanges();
                    mensagem = "Alterado com sucesso !";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);

                }
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/home.aspx");
        }

        private void getTopicos()
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

        protected void cboxAssunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTopicos();
        }

        protected void cboxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStatusColor();
        }
    }
}