using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SEMA
{
    public partial class cidadao : System.Web.UI.Page
    {
        public string mensagem = string.Empty;
        DateTime data = DateTime.Now;
        int seq = 0;
        int chamadoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            chamadoID = Convert.ToInt32(Request.QueryString["chamadoID"]);
            GetChamados(chamadoID);
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
                                 assunto = b.descricao,
                                 topico = c.descricao,
                                 a.status,
                                 a.img,

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
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (editor1.Value =="")
            {
                mensagem = "Favor preencher o texto com sua pergunta!";
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                editor1.Focus();
            }
            else
            if (resp_cboxStatus.Text =="Aberto")
                {
                    mensagem = "Õ Chamado esta com status ABERTO aguardando analise!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            else
            {
                try
                {
                    semaEntities ctx = new semaEntities();
                    chamado cha = ctx.chamadoes.First(p => p.id == chamadoID);
                    cha.status = "Retorno Cidadao";
                    ctx.SaveChanges();
                    pushMensage();
                    gravaHistorico();
                    mensagem = "Gravado com Sucesso!";
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
                }
                catch (Exception ex)
                {
                    mensagem = "Ocorreu o Seguinte erro ao tentar gravar " + ex.Message;
                    ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
                }
            }
        }
        private void pushMensage()
        {

            try
            {
                semaEntities ctx = new semaEntities();
                var resultado = (from t in ctx.historicoes
                                 where t.chamadoID == chamadoID
                                 orderby t.sequencia descending
                                 select new
                                 {
                                     t.sequencia,
                                     t.mensagem,
                                 }).Take(1);
                foreach (var item in resultado)
                {
                    seq = item.sequencia.Value;
                }
            }
            catch (Exception ex)
            {
                mensagem = "Ocorreu o seguinte erro ao tentar consultar ultima mensagem: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }
        // grava a mensagem na tabela historico
        private void gravaHistorico()
        {
            try
            {
                seq = seq + 1;
                semaEntities ctx = new semaEntities();
                historico his = new historico();
                his.chamadoID = chamadoID;
                his.mensagem = "<p>Enviada em: " + data + "</p></br>" + editor1.Value;
                his.sequencia = seq;
                his.origem = "cidadao";
                his.data = data;
                ctx.historicoes.Add(his);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                mensagem = "Ocorreu o seguinte erro ao gravar mensagem: " + ex.Message;
                ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
            }
        }
    }
}