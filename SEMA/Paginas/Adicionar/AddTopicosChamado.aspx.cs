using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace SEMA
{
    public partial class AddTopicosChamado : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        static string prevPage = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            prevPage = Request.UrlReferrer.ToString();

            if (Session["logado"] == null)
            {
                Response.Redirect("/login.aspx");
            }
            if (!Page.IsPostBack)
            {
                PreencherCbox();
            }
        }
        protected void cboxSecretaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            textAssunto.Text = "";
            textTopico.Text = "";
            if (cboxSecretaria.SelectedValue == "Selecione")
            {
                ListBox1.Items.Clear();
                ListBox2.Items.Clear();
                textAssunto.Text = "";
                textTopico.Text = "";
            }
            else
            {
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.assunto where secretariaID=" + cboxSecretaria.SelectedValue + " order by descricao asc", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                ListBox1.DataSource = ds;
                ListBox1.DataValueField = "id";
                ListBox1.DataTextField = "descricao";
                ListBox1.DataBind();
                con.Close();
            }
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
            cboxSecretaria.Items.Insert(0, new ListItem("Selecione", "Selecione"));
            foreach (var item in resultado)
            {
                cboxSecretaria.Items.Add(new ListItem(item.nome, item.id.ToString()));
            }
        }
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/home.aspx");
        }
        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox2.Items.Clear();
            string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from sema.topicos where assuntoID=" + ListBox1.SelectedValue + " order by descricao asc", con);
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            ListBox2.DataSource = ds;
            ListBox2.DataValueField = "id";
            ListBox2.DataTextField = "descricao";
            ListBox2.DataBind();
            textAssunto.Text = ListBox1.SelectedItem.Text.ToString();
            textTopico.Text = "";
            con.Close();
        }
        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textTopico.Text = ListBox2.SelectedItem.Text.ToString();
        }
        protected void btnAdicionarAssunto_Click(object sender, EventArgs e)
        {
            if (cboxSecretaria.SelectedValue == "Selecione")
            {
                mensagem = "Erro ! Favor Selecionar a Secretaria que deseja adicionar o Assunto";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else if (textAssunto.Text == "")
            {
                mensagem = "Favor Digitar a descrição do Assunto que deseja adicionar";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                textAssunto.Focus();
            }
            else
            {
                try
                {
                    string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                    MySqlConnection con = new MySqlConnection(conecLocal);
                    string sql;
                    MySqlCommand cmd;
                    con.Open();
                    sql = "INSERT INTO sema.assunto(descricao,secretariaID) VALUES(@descricao,@secretariaID)";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@descricao", textAssunto.Text);
                    cmd.Parameters.AddWithValue("@secretariaID", cboxSecretaria.SelectedValue);
                    cmd.ExecuteNonQuery();
                    textAssunto.Text = "";
                    RefreshListbox1();
                    mensagem = "Adicionado com Sucesso!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    con.Close();
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro ao tentar gravar: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        protected void btnEditarAssunto_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex == -1)
            {
                mensagem = "Erro ! Não há ou Não foi selecionado o item para Alteração";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            {
                try
                {
                    string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                    MySqlConnection con = new MySqlConnection(conecLocal);
                    string sql;
                    MySqlCommand cmd;
                    con.Open();
                    sql = "UPDATE sema.assunto SET descricao = @descricao WHERE(id = @id)";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", ListBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@descricao", textAssunto.Text);
                    cmd.ExecuteNonQuery();
                    textAssunto.Text = "";
                    RefreshListbox1();
                    mensagem = "Alterado com Sucesso!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    con.Close();
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro ao tentar gravar: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        protected void btnExcluirAssunto_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex == -1)
            {
                mensagem = "Erro ! Não há ou Não foi selecionado o item para Exclusão";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            {
                try
                {
                    string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                    MySqlConnection con = new MySqlConnection(conecLocal);
                    string sql;
                    MySqlCommand cmd;
                    con.Open();
                    sql = "DELETE FROM sema.assunto WHERE (`id` = @id)";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", ListBox1.SelectedValue);
                    cmd.ExecuteNonQuery();
                    textAssunto.Text = "";
                    RefreshListbox1();
                    mensagem = "Excluido com Sucesso!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    con.Close();
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro ao tentar Excluir: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        public void RefreshListbox1()
        {
            try
            {
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.assunto where secretariaID=" + cboxSecretaria.SelectedValue + " order by descricao asc", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                ListBox1.DataSource = ds;
                ListBox1.DataValueField = "id";
                ListBox1.DataTextField = "descricao";
                ListBox1.DataBind();
                con.Close();
            }
            catch (System.Exception ex)
            {
                mensagem = "Ocorreu o seguinte erro ao tentar Atualizar os dados: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }
        public void RefreshListbox2()
        {
            try
            {
                string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                MySqlConnection con = new MySqlConnection(conecLocal);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from sema.topicos where assuntoID=" + ListBox1.SelectedValue + " order by descricao asc", con);
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                ListBox2.DataSource = ds;
                ListBox2.DataValueField = "id";
                ListBox2.DataTextField = "descricao";
                ListBox2.DataBind();
                con.Close();
            }
            catch (System.Exception ex)
            {
                mensagem = "Ocorreu o seguinte erro ao tentar Atualizar os dados: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }
        protected void btnAdicionarTopico_Click(object sender, EventArgs e)
        {
            if (cboxSecretaria.SelectedValue == "Selecione")
            {
                mensagem = "Erro ! Favor Selecionar a Secretaria";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else if (ListBox1.SelectedIndex == -1)
            {
                mensagem = "Erro ! Favor Selecionar o Assunto na qual deseja adicionar um tópico";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else if (textTopico.Text == "")
            {
                mensagem = "Favor Digitar a descrição do Tópico que deseja adicionar";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                textTopico.Focus();
            }
            else if (ListBox1.SelectedValue == "")
            {
                mensagem = "Favor Selecionar o Assunto na qual deseja vincular o tópico";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            {
                try
                {
                    string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                    MySqlConnection con = new MySqlConnection(conecLocal);
                    string sql;
                    MySqlCommand cmd;
                    con.Open();
                    sql = "INSERT INTO sema.topicos(descricao,assuntoID) VALUES(@descricao,@assuntoID)";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@descricao", textTopico.Text);
                    cmd.Parameters.AddWithValue("@assuntoID", ListBox1.SelectedValue);
                    cmd.ExecuteNonQuery();
                    textTopico.Text = "";
                    RefreshListbox2();
                    mensagem = "Adicionado com Sucesso!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    con.Close();
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro ao tentar gravar: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        protected void btnEditarTopico_Click(object sender, EventArgs e)
        {
            if (ListBox2.SelectedIndex == -1)
            {
                mensagem = "Erro ! Não há ou Não foi selecionado o item para Alteração";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            {
                try
                {
                    string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                    MySqlConnection con = new MySqlConnection(conecLocal);
                    string sql;
                    MySqlCommand cmd;
                    con.Open();
                    sql = "UPDATE sema.topicos SET descricao = @descricao WHERE(id = @id)";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", ListBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@descricao", textTopico.Text);
                    cmd.ExecuteNonQuery();
                    textAssunto.Text = "";
                    RefreshListbox2();
                    mensagem = "Alterado com Sucesso!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    con.Close();
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro ao tentar gravar: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        protected void btnExcluirTopico_Click(object sender, EventArgs e)
        {
            if (ListBox2.SelectedIndex == -1)
            {
                mensagem = "Erro ! Não há ou Não foi selecionado o item para Exclusão";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            {
                try
                {
                    string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
                    MySqlConnection con = new MySqlConnection(conecLocal);
                    string sql;
                    MySqlCommand cmd;
                    con.Open();
                    sql = "DELETE FROM sema.topicos WHERE (`id` = @id)";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id", ListBox2.SelectedValue);
                    cmd.ExecuteNonQuery();
                    textAssunto.Text = "";
                    RefreshListbox2();
                    mensagem = "Excluido com Sucesso!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                    con.Close();
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro ao tentar Excluir: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
    }
}