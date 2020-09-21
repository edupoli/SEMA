using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class Configuracoes : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        string image;
        public string ImgPath;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblCaminhoImg.Visible = false;
            if (!Page.IsPostBack)
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
                                                    hpf.SaveAs(Server.MapPath("~/dist/img/logos/") + filename + i
                                                        + extensao);
                                                    //Prefixo p/ img pequena
                                                    var prefixoP = "-80x80";
                                                    //Prefixo p/ img grande
                                                    var prefixoG = "-160x160";
                                                    //pega o arquivo já carregado
                                                    string pth = Server.MapPath("~/dist/img/logos/")
                                                    + filename + i + extensao;
                                                    //Redefine altura e largura da imagem e Salva o arquivo + prefixo
                                                    ImageResize.resizeImageAndSave(pth, 80, 80, prefixoP);
                                                    ImageResize.resizeImageAndSave(pth, 160, 160, prefixoG);
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
                                        //imgSel.ImageUrl = "dist/img/logos/" + image;
                                        ImgPath = "dist/img/logos/" + image;
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
            if (lblCaminhoImg.Text == "")
            {
                ImgPath = "dist/img/logos/sem-logo.jpg";
                imgSel.ImageUrl = ImgPath;
                lblCaminhoImg.Text = "sem-logo.jpg";
            }

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            
            if (cboxSecretaria.SelectedValue == "selecione")
            {
                mensagem = "Favor Selecione a Secretaria!";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                cboxSecretaria.Focus();
            }
            else
            {
                int cod = int.Parse(cboxSecretaria.SelectedValue);
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.configuracoes where secretariaID="+ cod, con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.Fill(dt);
                int result = dt.Rows.Count;

                if (result == 0)
                {
                    semaEntities ctx = new semaEntities();
                    configuraco cfg = new configuraco();
                    cfg.secretariaID = int.Parse(cboxSecretaria.SelectedValue);
                    cfg.logo = lblCaminhoImg.Text;
                    cfg.bckColorMenu = bckColorMenu.Value;
                    cfg.onHoverbckColorMenu = onHoverbckColorMenu.Value;
                    cfg.textColorMenu = textColorMenu.Value;
                    cfg.onHovertexColorMenu = onHoverbckColorMenu.Value;
                    cfg.bckColorSbMenu = bckColorSbMenu.Value;
                    cfg.onHoverbckColorSbMenu = onHoverbckColorSbMenu.Value;
                    cfg.textColorSbMenu = textColorSbMenu.Value;
                    cfg.onHovertextColorSbMenu = onHovertextColorSbMenu.Value;
                    cfg.bckColorNavbar = bckColorNavbar.Value;
                    ctx.configuracoes.Add(cfg);
                    ctx.SaveChanges();
                    mensagem = "Adicionado com sucesso !";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                if(result == 1)
                {
                    semaEntities ctx = new semaEntities();
                    configuraco cfg = ctx.configuracoes.First(p => p.secretariaID == cod);
                    cfg.secretariaID = int.Parse(cboxSecretaria.SelectedValue);
                    if (lblCaminhoImg.Text != "sem-logo.jpg")
                    {
                        cfg.logo = lblCaminhoImg.Text;
                    }
                    cfg.bckColorMenu = bckColorMenu.Value;
                    cfg.onHoverbckColorMenu = onHoverbckColorMenu.Value;
                    cfg.textColorMenu = textColorMenu.Value;
                    cfg.onHovertexColorMenu = onHoverbckColorMenu.Value;
                    cfg.bckColorSbMenu = bckColorSbMenu.Value;
                    cfg.onHoverbckColorSbMenu = onHoverbckColorSbMenu.Value;
                    cfg.textColorSbMenu = textColorSbMenu.Value;
                    cfg.onHovertextColorSbMenu = onHovertextColorSbMenu.Value;
                    cfg.bckColorNavbar = bckColorNavbar.Value;
                    ctx.SaveChanges();
                    mensagem = "Alterado com sucesso !";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        private void PreencherCbox()
        {
            semaEntities ctx = new semaEntities();
            var resultado = (from t in ctx.secretarias
                             orderby t.nome
                             select new
                             {
                                 t.id,
                                 t.nome,
                             });
            cboxSecretaria.Items.Insert(0, new ListItem("Selecione", "selecione"));
            foreach (var item in resultado)
            {
                cboxSecretaria.Items.Add(new ListItem(item.nome, item.id.ToString()));
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
        public void getConfiguracao()
        {
            try
            {
                int cod = int.Parse(cboxSecretaria.SelectedValue);
                semaEntities ctx = new semaEntities();
                configuraco conf = ctx.configuracoes.First(p => p.secretariaID == cod);
                imgSel.ImageUrl = "dist/img/logos/" + conf.logo;
                bckColorMenu.Value = conf.bckColorMenu;
                onHoverbckColorMenu.Value = conf.onHoverbckColorMenu;
                textColorMenu.Value = conf.textColorMenu;
                onHovertextColorMenu.Value = conf.onHovertexColorMenu;
                bckColorSbMenu.Value = conf.bckColorSbMenu;
                onHoverbckColorSbMenu.Value = conf.onHoverbckColorSbMenu;
                textColorSbMenu.Value = conf.textColorSbMenu;
                onHovertextColorSbMenu.Value = conf.onHovertextColorSbMenu;
                bckColorNavbar.Value = conf.bckColorNavbar;
            }
            catch (Exception)
            {
                imgSel.ImageUrl = "dist/img/logos/sem-logo.jpg";
                lblCaminhoImg.Text = "sem-logo.jpg";
                bckColorMenu.Value = "#f9fafc";
                onHoverbckColorMenu.Value = "#f4f4f5";
                textColorMenu.Value = "#444444";
                onHovertextColorMenu.Value = "#000000";
                bckColorSbMenu.Value = "#f4f4f5";
                onHoverbckColorSbMenu.Value = "#2c3b41";
                textColorSbMenu.Value = "#777777";
                onHovertextColorSbMenu.Value = "#000000";
                bckColorNavbar.Value = "#3c8dbc";
                mensagem = "A Secretaria Selecionada ainda não tem Configurações Definidas";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "info();", true);
                
            }
            
        }

        protected void cboxSecretaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            getConfiguracao();
        }
    }
}