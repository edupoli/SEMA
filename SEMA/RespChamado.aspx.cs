using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCaminhoImg.Visible = false;
            prevPage = Request.UrlReferrer.ToString();
            getStatusColor();

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

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

        }

        protected void cboxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStatusColor();
        }

        protected void btnSalvar_resp_Click(object sender, EventArgs e)
        {

        }

        protected void btnVoltar_resp_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }

        
    }
}