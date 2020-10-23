<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TemplateEmail.aspx.cs" Inherits="SEMA.TemplateEmail" validateRequest="false"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="content-wrapper">
  <section class="content-header">
    <h1>
      Configurações Template de Email
    </h1>
    <ol class="breadcrumb">
      <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
      <li class="active">Configurações</li>
    </ol>
  </section>
  <section class="content">
    <div class="container-fluid">
      <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title"><i class="fas fa-cog"></i></h3>
          <div class="box-tools">
            <asp:Button Text="Salvar" CssClass="btn btn-sm btn-info" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" />
            <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click"/>
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fas fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fas fa-window-close"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="cboxSecretaria" />
            </Triggers>
            <ContentTemplate>
              <div class="form-row">
                <div class="col-md-6">
                  <label>Selecione a Secretaria</label>
                  <asp:DropDownList runat="server" ID="cboxSecretaria" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cboxSecretaria_SelectedIndexChanged">
                </asp:DropDownList>
              </div>
            </div>
            <div class="form-row">
              <div class="form-group col-md-4">
                <label for="smtp">Servidor SMTP</label>
                <asp:TextBox runat="server" class="form-control"  ID="textSMTP" placeholder="SMTP" />
              </div>
              <div class="form-group col-md-2">
                <label for="porta">Porta</label>
                <asp:TextBox runat="server" class="form-control" ID="textPorta" placeholder="Porta" />
              </div>
              <div class="form-group col-md-6">
                <label for="email">E-mail</label>
                <asp:TextBox runat="server" class="form-control" id="textEmail" placeholder="Email" />
              </div>
            </div>
            <div class="form-row">
              <div class="form-group col-md-6">
                <label for="textSenha">Senha do Email</label>
                <asp:TextBox runat="server" class="form-control" ID="textSenha" placeholder="Senha do email"/>
              </div>
              <div class="form-group col-md-6">
                <label for="nomeRementente">Nome do Remetente</label>
                <asp:TextBox runat="server" class="form-control" ID="textNomeRemetente" placeholder="Nome do Remetente"/>
              </div>
              <div class="form-group col-md-6">
                <label for="assunto">Assunto</label>
                <asp:TextBox runat="server" class="form-control" ID="textAssunto" placeholder="Assunto do Email"/>
              </div>
            </div>
            <div class="form-row">
              <div class="form-group col-md-12">
                <h3>Orientações para Preenchimento</h3>
                <p>Use <kbd>[variável]</kbd> Para adicionar valores variaveis no corpo to texto do email.</p>
                <p><strong>Exemplo:</strong> Digitando na caixa de texto <code>Olá [nome] o seu Número de protocolo é [protocolo]</code> </p>
                <p>Terá como exemplo o Resultado: <code>Olá Eduardo o seu Número de protocolo é 202009160001</code></p>
                <p>Variáveis Disponíveis para uso:</p>
                <p><kbd>[protocolo]</kbd> <kbd>[nome]</kbd> <kbd>[email]</kbd> <kbd>[telefone]</kbd> <kbd>[cpf]</kbd> <kbd>[assunto]</kbd> <kbd>[topico]</kbd> <kbd>[status]</kbd></p>
                <p>Para inserir Imagens no <mark>cabeçalho</mark> ou <mark>rodape</mark> do email, a imagem deve estar hospedada em um provedor externo e adicionar o link da imagem.</p>
                <p> Exemplos de serviços de hospedagens que podem ser utilizados: <a href="https://pt-br.imgbb.com/">ImageBB.com</a> ou <a href="https://uploaddeimagens.com.br/">uploaddeimagens.com</a> ou <a href="https://imagensbrasil.org/?lang=pt-BR">imagensbrasil.org</a></p>
              </div>
            </div>
            <div class="form-row">
              <div class="form-group col-md-12">
                <textarea class="textarea" runat="server" id="textBodyEmailAuto" placeholder="Digite aqui o Texto que será enviado como Mensagem Automática de confirmação de Abertura de Chamado"
                style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"></textarea>         
              </div>
              <div class="form-group col-md-12">
                <textarea class="textarea" runat="server" id="textBodyEmailResposta" placeholder="Digite aqui o Texto que será enviado na Resposta do Chamado"
                style="width: 100%; height: 200px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;"></textarea>         
              </div>
            </div>
          </div> 
        </ContentTemplate>
      </asp:UpdatePanel>        
    </div>
  </div>
</div>
</section>
</div>
<link rel="stylesheet" href="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css">
<script src="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
<script type="text/javascript">
  $(function () {
    var myCustomTemplates = {
      html : function(locale) {
        return "<li>" +
        "<div class='btn-group'>" +
        "<a class='btn btn-default' data-wysihtml5-action='change_view' title='" + locale.html.edit + "'>HTML</a>" +
        "</div>" +
        "</li>";
      }
    }
    $('.textarea').wysihtml5({
      toolbar: {
        "font-styles": true,
        "color": true,
        "emphasis": true,
        "textAlign": true,
        "lists": true,
        "blockquote": true,
        "link": true,
        "table": true,
        "image": true,
        "video": true,
        "html": true, 
        "customTemplates": myCustomTemplates
      }
    });
  })
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
  function info() {
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
    toastr["info"]("<%= mensagem %>")
  };
</script>
<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
  function RunThisAfterEachAsyncPostback() {
    var myCustomTemplates = {
      html : function(locale) {
        return "<li>" +
        "<div class='btn-group'>" +
        "<a class='btn btn-default' data-wysihtml5-action='change_view' title='" + locale.html.edit + "'>HTML</a>" +
        "</div>" +
        "</li>";
      }
    }
    $('.textarea').wysihtml5({
      toolbar: {
        "font-styles": true,
        "color": true,
        "emphasis": true,
        "textAlign": true,
        "lists": true,
        "blockquote": true,
        "link": true,
        "table": true,
        "image": true,
        "video": true,
        "html": true,
        "customTemplates": myCustomTemplates
      }
    });
  }
</script>
</asp:Content>
