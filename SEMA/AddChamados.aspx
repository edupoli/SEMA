<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddChamados.aspx.cs" Inherits="SEMA.AddChamados" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="wrapper">
  <div class="content-wrapper">
    <section class="content-header">
      <h1>Cadastrar Novo Chamado</h1> <div id="painel"></div>
      <ol class="breadcrumb">
        <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
        <li class="active">Chamados</li>
      </ol>
    </section>
    <section class="content">
      <div class="container-fluid">
        <div class="box box-default">
          <div class="box-header with-border" >
            <h3 class="box-title"><i class="far fa-bullhorn"></i></h3>
            <div class="box-tools">
              <asp:Button Text="Salvar" CssClass="btn btn-sm btn-primary" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" />
              <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click" />
            </div>
          </div>
          <!-- /.box-header -->
          <div class="box-body">
            <div class="row">
              <div class="col-md-2">
                <div class="form-group">
                  <label>Nº Protocolo</label>
                  <asp:TextBox runat="server" ID="txtProtocolo" CssClass="form-control"  ReadOnly="true"/>
                </div>
              </div>
              <div class="col-md-4">
                <div class="form-group">
                  <label>Nome</label>
                  <asp:TextBox runat="server" ID="txtnome" CssClass="form-control"  />
                </div>
              </div>
              <!-- /.form-group -->
              <div class="col-md-2">
                <div class="form-group">
                  <label>E-Mail</label>
                  <asp:TextBox runat="server" ID="txtemail" CssClass="form-control"  />
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>Telefone</label>
                  <asp:textbox  runat="server" id="txttelefone" class="form-control" onkeypress="$(this).mask('(00) 000000009')" /> 
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>CPF</label>
                  <asp:TextBox runat="server" ID="txtcpf" CssClass="form-control" data-inputmask='"mask": "999.999.999-99"' data-mask />
                </div>
              </div>
              <!-- /.form-group -->
            </div>
            <div class="row">
              <div class="col-md-4">
                <div class="form-group">
                  <label>Assunto</label>
                  <asp:DropDownList runat="server" ID="cboxAssunto" CssClass="form-control" DataTextField="descricao" DataValueField="id" AppendDataBoundItems="true" OnSelectedIndexChanged="cboxAssunto_SelectedIndexChanged" AutoPostBack="true">
                  <asp:ListItem Text="Selecione"  Value="Selecione"/>
                </asp:DropDownList>
              </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="cboxAssunto" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
              <div class="col-md-6">
                <div class="form-group">
                  <label>Tópico</label>
                  <asp:DropDownList runat="server" ID="cboxTopico" CssClass="form-control" DataTextField="descricao" DataValueField="id" AppendDataBoundItems="true">
                  <asp:ListItem Text="Selecione"  Value="Selecione"/>
                </asp:DropDownList>
              </div>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="cboxStatus" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
          <div class="col-md-2">
            <div class="form-group">
              <label>Status</label>
              <asp:DropDownList runat="server" CssClass="form-control" ID="cboxStatus" OnSelectedIndexChanged="cboxStatus_SelectedIndexChanged" AutoPostBack="true">
              <asp:ListItem Text="Aberto" Value="Aberto"  />
              <asp:ListItem Text="Em Atendimento" Value="Em Atendimento" />
              <asp:ListItem Text="Finalizado" Value="Finalizado" />
              <asp:ListItem Text="Pendente" Value="Pendente" />
            </asp:DropDownList>
          </div>
        </div>
      </ContentTemplate>
    </asp:UpdatePanel>
  </div>
  <div class="row">
    <div class="col-md-12">
      <div class="form-group">
        <CKEditor:CKEditorControl ID="descricao" runat="server" Visible="true" EntitiesLatin="False"></CKEditor:CKEditorControl>
      </div>
    </div>
  </div>
</div>
</div>
</div>
</section>
</div>
</div>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.15/jquery.mask.min.js"></script>
<script>
  $('[data-mask]').inputmask()
</script>
<script type="text/javascript">
  function sucesso() {
    toastr.options = {
      "closeButton": false,
      "debug": false,
      "newestOnTop": false,
      "progressBar": true,
      "positionClass": "toast-top-full-width",
      "preventDuplicates": false,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "1000",
      "timeOut": "5000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "slideDown",
      "hideMethod": "slideUp"
    }
    toastr["success"]("<%= mensagem %>")
  };

</script>
<script type="text/javascript">
  function erro() {
    toastr.options = {
      "closeButton": false,
      "debug": false,
      "newestOnTop": false,
      "progressBar": true,
      "positionClass": "toast-top-full-width",
      "preventDuplicates": false,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "1000",
      "timeOut": "5000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "slideDown",
      "hideMethod": "slideUp"
    }
    toastr["error"]("<%= mensagem %>")
  };
</script>
<script type="text/javascript">
  function acessoNegado() {
    toastr.options = {
      "closeButton": false,
      "debug": false,
      "newestOnTop": false,
      "progressBar": true,
      "positionClass": "toast-top-full-width",
      "preventDuplicates": false,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "1000",
      "timeOut": "5000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "slideDown",
      "hideMethod": "slideUp"
    }
    toastr["info"]("Acesso restrito a usuarios Administradores. ", "Erro")
  };
</script>

    <!--
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax ({
            type : "POST",
            url : "http://localhost:3000/getProtocolo",
            data : '{}',
            contentType : "application/json: charset=utf-8",
            dataType : "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
            function OnSuccess(jsonResult) {
                $(jsonResult.d).each(function () {
                    var htmltext = this.d;
                    $("#painel").append(htmltext);
                    alert("oi");
                })
            }
    });
    </script>
  -->

  <script type="text/javascript">
    $(document).ready(function () {
      var url = "http://localhost:3000/getProtocolo";
          //var url = "http://10.0.2.15/ctdws/integracao/protocolo/geraProtocolo?login=ctd&senha=integraws&idchamada=0&numdea=33793300&tipomidia=ura";
          $.ajax({
            url: url,
            type: "GET",
            success: function (data) {
              var returnedData = data;
              console.log("Ajax call succeeded! :D");
              console.log(returnedData);
              $("input[id$='txtProtocolo']").val(returnedData);
            },
            error: function (xhr) {
              console.log("Ajax call failed :'(");
              console.log(xhr);
            }
          });
        });
      </script>
      <script language="C#" type="text/C#" runat="server">

        /*
        public void Email()
        {
          try
          {
            string assunto = "Protocolo Nº " + numProtocolo + " Secretaria do Meio Ambiente Londrina-PR";
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(e_mail, "Secretaria do Meio Ambiente Londrina-PR");
            mailMessage.To.Add(e_mail.ToLower());
            mailMessage.Subject = assunto;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = "<img src='https://i.ibb.co/L89Y9Yt/SEMA.png' /><br>RECEBEMOS A SUA SOLICITAÇÃO E EM BREVE SERÁ RESPONDIDA." + "<br>" +
            "Protocolo: " + numProtocolo + "<br>" + "Nome: " + nome.Text + "<br>" +
            " CPF: " + cpf.Text + "<br>" +
            " Telefone para Contato: " + telefone.Text + "<br>" +
            " Assunto: " + cboxAssunto.SelectedItem + "<br>" + cboxTopico.SelectedItem +"<br>"+
            " Sua Mensagem:<br> " + descricao.Text + "<br><br>" +
            " <strong>SECRETARIA MUNICIPAL DO AMBIENTE</strong><br>" +
            " <strong>Localização:</strong> Rua da Natureza, 155 Jardim Piza" +
            " <strong>CEP:</strong> 86041-050 Londrina-Paraná" +
            " <strong>Telefone:</strong> Geral(43) 3372-4750  ou(43) 3372-4751" +
            " <strong>E-mail:</strong> sema@londrina.pr.gov.br" +
            " <strong>Horário de Atendimento:</strong> de segunda a sexta-feira, das 12h às 18h<br><br>" +

            "*** E-mail automático, não há necessidade de respondê-lo. ***";

            mailMessage.Priority = MailPriority.High;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("sercomtelcontatcenter@gmail.com", "qtrlutilrbkfgwsf");
            smtpClient.Send(mailMessage);
            ClientScript.RegisterStartupScript(GetType(), "Popup", "sucesso();", true);
          }
          catch (Exception ex)
          {
            mensagem = "Erro ao enviar e-mail: " + ex.Message;
            ClientScript.RegisterStartupScript(GetType(), "Popup", "erro();", true);
          }
        }
        */
      </script>

    </asp:Content>
