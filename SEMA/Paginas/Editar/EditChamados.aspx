<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditChamados.aspx.cs" Inherits="SEMA.EditChamados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="wrapper">
  <div class="content-wrapper">
    <section class="content-header">
      <h1>Editar Chamado</h1>
      <ol class="breadcrumb">
        <li><a href="/home.aspx"><i class="fas fa-home"></i> Home</a></li>
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
                  <asp:TextBox runat="server" ID="nome" CssClass="form-control"  />
                </div>
              </div>
              <!-- /.form-group -->
              <div class="col-md-2">
                <div class="form-group">
                  <label>E-Mail</label>
                  <asp:TextBox runat="server" ID="email" CssClass="form-control"  />
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>Telefone</label>
                  <asp:TextBox runat="server" ID="telefone" CssClass="form-control"  />
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>CPF</label>
                  <asp:TextBox runat="server" ID="cpf" CssClass="form-control" data-inputmask='"mask": "999.999.999-99"' data-mask />
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
        <asp:TextBox runat="server" ID="descricao" TextMode="MultiLine" />
      </div>
    </div>
  </div>
</div>
</div>
</div>
</section>
</div>
</div>
<script>
	CKEDITOR.replace( '<%=descricao.ClientID%>' );
	extraPlugins: 'wordcount,notification,exportpdf'

	CKEDITOR.on( 'instanceReady', function( ev ) {
		ev.editor.setData('<p style="text-align:justify;"></p>');
	});
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
<script>
  $('[data-mask]').inputmask()
</script>
</asp:Content>
