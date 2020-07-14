using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class RespChamado : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        string image;
        public string ImgPath;
        string prevPage;
        int chamadoID;
        string e_mail;
        string numProtocolo;
        string nome;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCaminhoImg.Visible = false;
            prevPage = Request.UrlReferrer.ToString();
            getStatusColor();
            chamadoID = Convert.ToInt32(Request.QueryString["chamadoID"]);
            GetChamados(chamadoID);
            e_mail = resp_email.Text;

            if (Session["logado"] != null)
            {
                if (Session["perfil"].ToString() != "Administrador")
                {
                    Response.Redirect("login.aspx");
                }

            }
            else
            {
                Response.Redirect("login.aspx");
            }

            if (!IsPostBack)
            {
                PreencherCbox();
            } 

            if (IsPostBack && img.PostedFile != null)
            {
                if (img.PostedFile.FileName.Length > 0)
                {
                    if (img.PostedFile.ContentLength < 8388608)
                    {
                        try
                        {
                            if (img.HasFile)
                            {
                                try
                                {
                                    //Aqui ele vai filtrar pelo tipo de arquivo
                                    if (img.PostedFile.ContentType == "image/jpeg" ||
                                        img.PostedFile.ContentType == "image/jpg" ||
                                        img.PostedFile.ContentType == "image/png" ||
                                        img.PostedFile.ContentType == "image/gif" ||
                                        img.PostedFile.ContentType == "image/bmp")
                                    {
                                        try
                                        {
                                            //Obtem o  HttpFileCollection
                                            HttpFileCollection hfc = Request.Files;
                                            for (int i = 0; i < hfc.Count; i++)
                                            {
                                                HttpPostedFile hpf = hfc[i];
                                                if (hpf.ContentLength > 0)
                                                {
                                                    //Pega o nome do arquivo
                                                    string nome = Path.GetFileName(hpf.FileName);
                                                    //Pega a extensão do arquivo
                                                    string extensao = Path.GetExtension(hpf.FileName);
                                                    //Gera nome novo do Arquivo numericamente
                                                    string filename = string.Format("{0:00000000000000}", GerarID());
                                                    //Caminho a onde será salvo
                                                    hpf.SaveAs(Server.MapPath("~/dist/img/chamados/") + filename + i
                                                    + extensao);

                                                    //Prefixo p/ img pequena
                                                    var prefixoP = "-80x80";
                                                    //Prefixo p/ img grande
                                                    var prefixoG = "-800x600";

                                                    //pega o arquivo já carregado
                                                    string pth = Server.MapPath("~/dist/img/chamados/")
                                                    + filename + i + extensao;

                                                    //Redefine altura e largura da imagem e Salva o arquivo + prefixo
                                                    ImageResize.resizeImageAndSave(pth, 80, 80, prefixoP);
                                                    ImageResize.resizeImageAndSave(pth, 800, 600, prefixoG);
                                                    image = filename + i + prefixoG + extensao;
                                                }

                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            mensagem = "Ocerreu o Seguinte erro: " + ex.Message;
                                            ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                                        }
                                        // Mensagem se tudo ocorreu bem
                                        //imgSel.ImageUrl = "dist/img/chamados/" + image;
                                        ImgPath = "dist/img/chamados/" + image;
                                        Image1.ImageUrl = ImgPath;
                                        imgSel.ImageUrl = ImgPath;
                                        lblCaminhoImg.Text = image;
                                        mensagem = "Upload da Imagem feito com Sucesso!!!";
                                        ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);


                                    }
                                    else
                                    {
                                        // Mensagem notifica que é permitido carregar apenas 
                                        // as imagens definida la em cima.
                                        mensagem = "É permitido carregar apenas imagens!";
                                        ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);

                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Mensagem notifica quando ocorre erros
                                    mensagem = "O arquivo não pôde ser carregado. O seguinte erro ocorreu: " + ex.Message;
                                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Mensagem notifica quando ocorre erros
                            mensagem = "O arquivo não pôde ser carregado. O seguinte erro ocorreu: " + ex.Message;
                            ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);

                        }
                    }
                    else
                    {
                        // Mensagem notifica quando imagem é superior a 8 MB
                        mensagem = "Não é permitido carregar mais do que 8 MB";
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);

                    }
                }
            }
            else
            {
                ImgPath = "dist/img/chamados/user-800x600.png";
                Image1.ImageUrl = ImgPath;
                imgSel.ImageUrl = ImgPath;
                lblCaminhoImg.Text = "user-800x600.png";
            }
        }

        private void PreencherCbox()
        {
            semaEntities ctx = new semaEntities();
            var resultado = (from t in ctx.usuarios
                             select new
                             {
                                 t.id,
                                 t.nome,
                             });
            cboxUsuario.Items.Insert(0, new ListItem("Selecione", "Selecione"));
            foreach (var item in resultado)
            {
                string valor = Convert.ToString(item.id);
                cboxUsuario.Items.Add(new ListItem(item.nome, valor));
            }

        }

        public Int64 GerarID()
        {
            try
            {
                DateTime data = new DateTime();
                data = DateTime.Now;
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
        public void GetChamados(int cod)
        {
            semaEntities ctx = new semaEntities();
            var resultado = (from a in ctx.chamadoes
                             join b in ctx.assuntoes on a.assunto equals b.id
                             join c in ctx.topicos on a.topico equals c.id
                             where a.id == cod
                             select new
                             {
                                 a.id,
                                 a.protocolo,
                                 a.nome,
                                 a.cpf,
                                 a.email,
                                 a.telefone,
                                 a.descricao,
                                 assunto = b.descricao,
                                 topico = c.descricao,
                                 a.status,
                                 a.img,
                                 a.resposta,
                                 
                             });
            foreach (var item in resultado)
            {
                resp_txtProtocolo.Text = item.protocolo;
                resp_nome.Text = item.nome;
                resp_email.Text = item.email;
                resp_cpf.Text = item.cpf;
                resp_telefone.Text = item.telefone;
                resp_cboxAssunto.Items.Add(new ListItem(item.assunto, item.assunto));
                resp_cboxTopico.Items.Add(new ListItem(item.topico, item.topico));
                resp_cboxStatus.Items.Add(new ListItem(item.status, item.status));
                resp_descricao.Text = item.descricao;
            }
        }


        

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cboxUsuario.SelectedIndex == 0)
            {
                mensagem = "Deve Selecionar o usuario Responsavel pelo atendimento do chamado";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                cboxUsuario.Focus();
            }
            else
            
                if (cboxStatus.SelectedIndex == 0)
                {
                    mensagem = "Favor atualizar o Status do Chamado";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    cboxStatus.Focus();
                }
            
            else
	        
                if (resp_descricao.Text == "")
                {
                    mensagem = "Favor preencher a descrição da resposta do chamado";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    resp_descricao.Focus();
                }

            else
            {
                try
                {
                    semaEntities ctx = new semaEntities();
                    chamado cha = ctx.chamadoes.First(p => p.id == chamadoID);
                    cha.usuario_responsavel = int.Parse(cboxUsuario.SelectedValue);
                    cha.status = cboxStatus.SelectedValue;
                    cha.resposta = descricao.Text;
                    cha.img = lblCaminhoImg.Text;
                    ctx.SaveChanges();
                    mensagem = "Gravado com Sucesso!";
                    if (resp_email.Text !="")
                    {
                        Email();
                    }
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);


                }
                catch (Exception ex)
                {
                    mensagem = "Ocorreu o Seguinte erro ao tentar gravar " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        private void Email()
        {
            try
            {
                string assunto = "Resposta ao Protocolo Nº " + resp_txtProtocolo.Text + " Secretaria do Meio Ambiente Londrina-PR";
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(e_mail, "Secretaria do Meio Ambiente Londrina-PR");
                mailMessage.To.Add(e_mail.ToLower());
                mailMessage.Subject = assunto;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body =
                mailMessage.Body = "<html><body><img src='https://i.ibb.co/L89Y9Yt/SEMA.png' /><br><br>" + "<b>Olá " + resp_nome.Text + "</b><br><br>" +
                            "Em Resposta a sua solicitação na qual foi registrada com protocolo Nº " + numProtocolo + "<br>" +
                           "<br>Sua Mensagem: <br>" + resp_descricao.Text + "<br><br>" +
                           "Segue abaixo Resposta:<br>" + descricao.Text + "<br><br><br>" +
                           "Caso ainda tenha dúvidas referente a esse protocolo, por favor <b><a href='http://10.0.2.135/faleconosco/faleconosco?idfaleconosco=" + chamadoID + "'>CLIQUE AQUI</a></b> para nos perguntar<br>" +
                           "Obrigado por entrar em contato.<br>" +
                           "A SEMA está a sua disposição, você também pode obter informações e serviços no site <a href='http://www1.londrina.pr.gov.br/index.php?option=com_content&view=frontpageplus&Itemid=163'> da Prefeitura </a>.<br>" +
                           "Nosso horário de atendimento presencial é das 12h às 18h de segunda a sexta - feira.<br></body><html>";

                mailMessage.Priority = MailPriority.High;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("sercomtelcontatcenter@gmail.com", "qtrlutilrbkfgwsf");
                smtpClient.Send(mailMessage);
                mensagem = "Email enviado com sucesso!";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao enviar o Email: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void cboxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStatusColor();
        }

        protected void btnVoltar_resp_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        
    }
}