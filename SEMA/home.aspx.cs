﻿using MySql.Data.MySqlClient;
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
        public int[] DataChamados = new int[5];
        public int[] Data3 { get; set; }
        public int[] Data4 { get; set; }
        string aberto;
        string finalizado;
        string pendente;
        string atendimento;
        string retornoCidadao;
        public string mensagem = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logado"] == null)
            {
                Response.Redirect("login.aspx");
            }
            
            Timer1.Enabled = true;
            aberto = qtaAbertos.Text;
            finalizado = qtaFinalizados.Text;
            pendente = qtaPendentes.Text;
            atendimento = qtaAtendimentos.Text;
            retornoCidadao = qtaRetornoCidadao.Text;
            if (!Page.IsPostBack)
            {
                abertos();
                Pendentes();
                Em_Atendimento();
                finalizados();
                retorno_cidadao();
                getQtdaStatus();
                ClientScript.RegisterStartupScript(GetType(), "Popup", "graficoDonuts();", true);
            }
            
            Labels = new string[] { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
            Data3 = new int[] { 22, 39, 63, 45, 32, 53, 25 };
            Data4 = new int[] { 32, 59, 43, 65, 22, 73, 45 };
        }
        private void getQtdaStatus()
        {
            int cod = int.Parse(Session["secretaria"].ToString());
            semaEntities ctx = new semaEntities();
            var resultado = from p in ctx.chamadoes
                where p.secretariaID == cod
                group p by p.status into g
                select new {
                    Status = g.Key,
                    Total = g.Count()
                };
            int result = resultado.Count();
            int index = 0;
            foreach (var item in resultado)
            {
                switch (item.Status)
                {
                    case "Aberto":
                        DataChamados[index] = item.Total;
                        break;
                    case "Em Atendimento":
                        DataChamados[index] = item.Total;
                        break;
                    case "Finalizado":
                        DataChamados[index] = item.Total;
                        break;
                    case "Pendente":
                        DataChamados[index] = item.Total;
                        break;
                    case "Retorno Cidadao":
                        DataChamados[index] = item.Total;
                        break;
                    default:
                        DataChamados[index] = 0;
                        break;
                }
                index++;
            }
            /*
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status,count(*) as total FROM chamado where secretariaID=" + Session["secretaria"].ToString() + " group by status", con);
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
            */
        }
        private void abertos()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Aberto' and secretariaID=" + Session["secretaria"].ToString() + " group by status ", con);
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
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Finalizado' and secretariaID=" + Session["secretaria"].ToString() + " group by status ", con);
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
                UpdatePanel2.Update();
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "Show_Font", "graficoDonuts();", true);
            }
        }
        private void Pendentes()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Pendente' and secretariaID=" + Session["secretaria"].ToString() + " group by status ", con);
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
                UpdatePanel2.Update();
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "Show_Font", "graficoDonuts();", true);
            }
        }
        private void Em_Atendimento()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Em Atendimento' and secretariaID=" + Session["secretaria"].ToString() + " group by status ", con);
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
                UpdatePanel2.Update();
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, this.GetType(), "Show_Font", "graficoDonuts();", true);
            }
        }
        private void retorno_cidadao()
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT status, count(*) as total FROM chamado where status = 'Retorno Cidadao' and secretariaID=" + Session["secretaria"].ToString() + " group by status ", con);
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                string qtda = Convert.ToString(dr["total"]);
                qtaRetornoCidadao.Text = qtda;
            }
            else
            {
                qtaRetornoCidadao.Text = "0";
            }
            if (retornoCidadao != qtaRetornoCidadao.Text)
            {
                getQtdaStatus();
                UpdatePanel2.Update();
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