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
    public partial class home : System.Web.UI.Page
    {
        string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Database=sema;Allow User Variables=True;Pooling=False";
        
        public string[] Labels { get; set; }
        public int[] DataChamados = new int[4];
        public int[] Data3 { get; set; }
        public int[] Data4 { get; set; }
        string aberto;
        string finalizado;
        string pendente;
        string atendimento;
        protected void Page_Load(object sender, EventArgs e)
        {
            Timer1.Enabled = true;
            aberto = qtaAbertos.Text;
            finalizado = qtaFinalizados.Text;
            pendente = qtaPendentes.Text;
            atendimento = qtaAtendimentos.Text;
            if (!Page.IsPostBack)
            {
                abertos();
                Pendentes();
                Em_Atendimento();
                finalizados();
                getQtdaStatus();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "graficoDonuts();", true);
            }

            Labels = new string[] { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
            Data3 = new int[] { 22, 39, 63, 45, 32, 53,25 };
            Data4 = new int[] { 32, 59, 43, 65, 22, 73,45 };
           

        }
        private void getQtdaStatus()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status,count(*) as total FROM chamado group by status", con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            DataTable dt = new DataTable();
            dr = cmd.ExecuteReader();
            dt.Load(dr);
            int linhas = dt.Rows.Count;
            dr = cmd.ExecuteReader();
            for (int i = 0; i < linhas; i++)
            {
                dr.Read();
                int dd = Convert.ToInt32(dr["total"]);
                DataChamados[i] = dd;
            }
            
        }
        private void abertos()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Aberto' group by status ", con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                string qtda = Convert.ToString(dr["total"]);
                qtaAbertos.Text = qtda;
            }
            else
            {
                qtaAbertos.Text = "0";
            }
            if (aberto != qtaAbertos.Text)
            {
                getQtdaStatus();
                UpdatePanel2.Update();
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "Show_Font", "graficoDonuts();", true);
            }
        }
        private void finalizados()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Finalizado' group by status ", con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                string qtda = Convert.ToString(dr["total"]);
                qtaFinalizados.Text = qtda;
            }
            else
            {
                qtaFinalizados.Text = "0";
            }
            if (finalizado != qtaFinalizados.Text)
            {
                getQtdaStatus();
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "Show_Font", "graficoDonuts();", true);
            }
        }
        private void Pendentes()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Pendente' group by status ", con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                string qtda = Convert.ToString(dr["total"]);
                qtaPendentes.Text = qtda;
            }
            else
            {
                qtaPendentes.Text = "0";
            }
            if (pendente != qtaPendentes.Text)
            {
                getQtdaStatus();
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "Show_Font", "graficoDonuts();", true);
            }
            
        }
        private void Em_Atendimento()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Em Atendimento' group by status ", con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                string qtda = Convert.ToString(dr["total"]);
                qtaAtendimentos.Text = qtda;
            }
            else
            {
                qtaAtendimentos.Text = "0";
            }
            if (atendimento != qtaAtendimentos.Text)
            {
                getQtdaStatus();
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "Show_Font", "graficoDonuts();", true);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            abertos();
            Pendentes();
            Em_Atendimento();
            finalizados();
            
        }
    }
}