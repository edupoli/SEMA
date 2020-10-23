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
    public partial class login : System.Web.UI.Page
    {
        string conecLocal = "SERVER=10.0.2.9;UID=ura;PWD=ask123;Allow User Variables=True;Pooling=False";
        string user;
        string password;
        public string mensagem = string.Empty;
        int logado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = usuario.Text.Trim();
            password = senha.Text.Trim();
        }
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(conecLocal);
            con.Open();
            if (usuario.Text.Trim() == "" || senha.Text.Trim() == "" || senha.Text == string.Empty)
            {
                mensagem = "Favor informar Usuário e senha para login!!";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
            else
            {
                try
                {
                    string senhaCriptografada = Criptografia.CalculaHash(password);
                    string sql = "Select * from sema.usuario where login='" + user + "' and senha='" + senhaCriptografada + "'";
                    MySqlCommand cmd;
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    cmd = new MySqlCommand(sql, con);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        Session.Timeout = 10;
                        Session["logado"] = "SIM";
                        Session["nome"] = dt.Rows[0][1].ToString();
                        Session["perfil"] = dt.Rows[0][5].ToString();
                        Session["img"] = dt.Rows[0][7].ToString();
                        Session["cargo"] = dt.Rows[0][8].ToString();
                        Session["id"] = dt.Rows[0][0].ToString();
                        Session["secretaria"] = dt.Rows[0][6].ToString();
                        dt.Dispose();
                        logado = 1;
                    }
                    else
                    {
                        Session["logado"] = "NAO";
                        mensagem = "Usuário ou senha Inválidos!!";
                        logado = 0;
                        ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                    }
                }
                catch (System.Exception ex)
                {
                    mensagem = "Ocorreu o seguinte erro: " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
                finally
                {
                    con.Close();
                }
            }
            if (logado == 1)
            {
                Response.Redirect("home.aspx");
            }
        }
    }
}