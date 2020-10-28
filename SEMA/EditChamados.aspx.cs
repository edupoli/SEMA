using MySql.Data.MySqlClient;
using SEMA.ServiceCorreios;
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
        static string prevPage = string.Empty;
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
                prevPage = Request.UrlReferrer.ToString();
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
                                 a.rua,
                                 a.numero,
                                 a.bairro,
                                 a.cidade,
                                 a.cep,
                                 a.assunto,
                                 a.topico,
                                 a.status,
                                 a.img,
                                 d.mensagem
                                 

                             });
            foreach (var item in resultado)
            {
                txtProtocolo.Text = item.protocolo;
                txtnome.Text = item.nome;
                txtemail.Text = item.email;
                txtcpf.Text = item.cpf;
                txttelefone.Text = item.telefone;
                txtCEP.Text = item.cep;
                txtRua.Text = item.rua;
                txtNumero.Text = item.numero;
                txtBairro.Text = item.bairro;
                txtCidade.Text = item.cidade;
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
            /*
            else
                if ((txtcpf.Text != "") && (ValidaCPF.IsCpf(txtcpf.Text) == false))
            {
                valido = "nao";
                mensagem = "O CPF informado é inválido !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            */
            else

                    if ((txtemail.Text != "") && (ValidaEmail.ValidarEmail(txtemail.Text) == false))
            {
                valido = "nao";
                mensagem = "O e-mail digitado esta incorreto !";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                txtemail.Focus();
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
                    if (checkDenuncia.Checked == true)
                    {
                        ch.anonimo = "SIM";
                    }
                    if (checkDenuncia.Checked == false)
                    {
                        ch.anonimo = "NAO";
                    }
                    if (checkWhatsapp.Checked == true)
                    {
                        ch.envia_whatsapp = "SIM";
                    }
                    if (checkWhatsapp.Checked == false)
                    {
                        ch.envia_whatsapp = "NAO";
                    }
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
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
                GetChamados(chamadoID, int.Parse(Session["secretaria"].ToString()));
            }
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
    }
}