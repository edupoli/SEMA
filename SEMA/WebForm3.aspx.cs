using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        public string ImgPath = "";
        public string mensagem = string.Empty;
        string image;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                                        catch (System.Exception ex)
                                        {
                                            mensagem = "Ocerreu o Seguinte erro: " + ex.Message;
                                            ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                                        }
                                        // Mensagem se tudo ocorreu bem
                                        //imgSel.ImageUrl = "dist/img/chamados/" + image;
                                        ImgPath = "/dist/img/chamados/" + image;
                                        Image1.ImageUrl = ImgPath;
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
            }
            else
            if (lblCaminhoImg.Text == "user-800x600.png")
            {
                ImgPath = "/dist/img/chamados/user-800x600.png";
                Image1.ImageUrl = ImgPath;
                lblCaminhoImg.Text = "user-800x600.png";
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
        protected void fileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            //string filename = System.IO.Path.GetFileName(img.FileName);
            //img.SaveAs(Server.MapPath("~/dist/img/chamados/") + filename);
            //Image2.ImageUrl = (Server.MapPath("~/dist/img/chamados/") + filename);
            // ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(Image1);

        }
    }
}