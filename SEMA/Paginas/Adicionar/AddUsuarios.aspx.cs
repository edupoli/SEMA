using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class AddUsuarios : System.Web.UI.Page
    {
        string password;
        string image;
        public string mensagem = string.Empty;
        static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["logado"] == null)
                {
                    Response.Redirect("../../login.aspx");
                }
                else
            if (Session["perfil"].ToString() != "Administrador")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "acessoNegado();", true);
                    Response.Redirect("../../login.aspx");
                }
                imgSel.ImageUrl = "dist/img/users/user-160x160.png";
                lblCaminhoImg.Text = "user-160x160.png";
                prevPage = Request.UrlReferrer.ToString();
            }
            lblCaminhoImg.Visible = false;
            password = senha.Text;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (nome.Text == "")
            {
                mensagem = "O Campo Nome é obrigatório";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                nome.Focus();
            }
            else

            {
                try
                {
                    string senhaCriptografada = Criptografia.CalculaHash(password);
                    semaEntities ctx = new semaEntities();
                    usuario user = new usuario();
                    user.nome = nome.Text.Trim();
                    user.email = email.Text.Trim();
                    user.login = login.Text.Trim();
                    user.senha = senhaCriptografada;
                    user.perfil = cboxPerfil.SelectedValue;
                    user.secretariaID = int.Parse(cboxSecretaria.SelectedValue);
                    user.img = lblCaminhoImg.Text.Trim();
                    user.cargo = cargo.Text.Trim();
                    ctx.usuarios.Add(user);
                    ctx.SaveChanges();
                    mensagem = "Cadastrado com Sucesso !!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    nome.Text = string.Empty;
                    email.Text = string.Empty;
                    login.Text = string.Empty;
                    senha.Text = string.Empty;
                    cboxPerfil.SelectedIndex = -1;
                    cboxSecretaria.SelectedIndex = -1;
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    throw;
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
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
                                            hpf.SaveAs(Server.MapPath("~/dist/img/users/") + filename + i
                                            + extensao);

                                            //Prefixo p/ img pequena
                                            var prefixoP = "-80x80";
                                            //Prefixo p/ img grande
                                            var prefixoG = "-160x160";

                                            //pega o arquivo já carregado
                                            string pth = Server.MapPath("~/dist/img/users/")
                                            + filename + i + extensao;

                                            //Redefine altura e largura da imagem e Salva o arquivo + prefixo
                                            ImageResize.resizeImageAndSave(pth, 80, 80, prefixoP);
                                            ImageResize.resizeImageAndSave(pth, 160, 160, prefixoG);
                                            image = filename + i + prefixoG + extensao;
                                        }

                                    }
                                }
                                catch (System.Exception ex)
                                {
                                    mensagem = "Ocerreu o Seguinte erro: " + ex.Message;
                                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                                }
                                // Mensagem se tudo ocorreu bem
                                imgSel.ImageUrl = "dist/img/users/" + image;
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
                        catch (System.Exception ex)
                        {
                            // Mensagem notifica quando ocorre erros
                            mensagem = "O arquivo não pôde ser carregado. O seguinte erro ocorreu: " + ex.Message;
                            ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);

                        }
                    }
                }
                catch (System.Exception ex)
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
        public Int64 GerarID()
        {
            try
            {
                DateTime data = new DateTime();
                data = DateTime.Now;
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
    }
}