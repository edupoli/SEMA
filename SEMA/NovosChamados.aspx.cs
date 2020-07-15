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
    public partial class NovosChamados : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        int chamadoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            Timer1.Enabled = true;
            Timer1.Interval = 10000;
            Listar();
        }

        private void Listar()
        {
            string connectionString = "SERVER=10.0.2.9;UID=ura;PWD=ask123;DATABASE=sema;Allow User Variables=True;Pooling=False";
            MySqlConnection con = new MySqlConnection(connectionString);
            string sql;
            MySqlCommand cmd;
            con.Open();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter();
            sql = "SELECT chamado.id, chamado.protocolo,chamado.nome, chamado.email," +
                "chamado.cpf,chamado.telefone,assunto.descricao as assunto," +
                "topicos.descricao as topico, chamado.img,chamado.status" +
                 " FROM chamado " +
                 "inner join assunto on chamado.assunto = assunto.id " +
                 "inner join topicos on chamado.topico = topicos.id " +
                 "where chamado.status='Aberto'";

            cmd = new MySqlCommand(sql, con);
            da.SelectCommand = cmd;
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "status").ToString();
                if (status == "Aberto")
                {
                    string azul = "#3C8DBC";
                    e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml(azul);
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                }
                if (status == "Finalizado")
                {
                    string verde = "#478978";
                    e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml(verde);
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                }
                if (status == "Em Atendimento")
                {
                    string laranja = "#F39C12";
                    e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml(laranja);
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                }
                if (status == "Pendente")
                {
                    string vermelho = "#DD4B39";
                    e.Row.Cells[5].BackColor = System.Drawing.ColorTranslator.FromHtml(vermelho);
                    e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                }

            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Listar();
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {

        }

        protected void btnResponder_Click(object sender, EventArgs e)
        {
            if (Session["perfil"].ToString() != "Administrador")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "acessoNegado();", true);
            }
            else
            {
                chamadoID = int.Parse((sender as LinkButton).CommandArgument);
                Response.Redirect("RespChamado.aspx?chamadoID=" + chamadoID);
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {

        }
    }
}