<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUsuarios.aspx.cs" Inherits="SEMA.AddUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="wrapper">
  <div class="content-wrapper">
    <section class="content-header">
      <h1>Cadastrar Usuário</h1>
      <ol class="breadcrumb">
        <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
        <li class="active">Usuários</li>
      </ol>
    </section>
    <section class="content">
      <div class="container-fluid">
        <div class="box box-default">
          <div class="box-header with-border" >
            <h3 class="box-title"><i class="fas fa-user"></i></h3>
            <div class="box-tools">
              <asp:Button Text="Salvar" CssClass="btn btn-sm btn-primary" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" />
              <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click" />
              <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fas fa-minus"></i></button>
              <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fas fa-window-close"></i></button>
            </div>
          </div>
          <div class="box-body">
            <div class="row">
              <div class="col-md-5">
                <div class="form-group">
                  <label>Nome</label>
                  <asp:TextBox runat="server" ID="nome" CssClass="form-control"  />
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>E-Mail</label>
                  <asp:TextBox runat="server" ID="email" CssClass="form-control"  />
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>Login</label>
                  <asp:TextBox runat="server" ID="login" CssClass="form-control"  />
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>Senha</label>
                  <asp:TextBox runat="server" ID="senha" CssClass="form-control" type="password" />
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-4">
                <div class="form-group">
                  <label>Perfil</label>
                  <asp:DropDownList runat="server" ID="cboxPerfil" CssClass="form-control">
                  <asp:ListItem Text="Comum"  Value="comum"/>
                  <asp:ListItem Text="Administrador" Value="Administrador" />
                </asp:DropDownList>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-group">
                <label>Secretaria</label>
                <asp:DropDownList runat="server" ID="cboxSecretaria" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="nome" DataValueField="id">
              </asp:DropDownList>
              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="server=10.0.2.9;user id=ura;database=sema;password=ask123;persistsecurityinfo=True" ProviderName="MySql.Data.MySqlClient" SelectCommand="SELECT id, nome FROM secretaria ORDER BY nome"></asp:SqlDataSource>
            </div>
          </div>
          <div class="col-md-4">
            <div class="form-group">
              <label>Cargo</label>
              <asp:TextBox runat="server" ID="cargo" CssClass="form-control"   />
            </div>
          </div>
        </div>
        <div class="col">
          <div class="form-group">
            <asp:Label Text="" runat="server" ID="lblCaminhoImg" />
            <asp:Image runat="server" ID="imgSel" Width="160px" Height="160px" />
            <asp:FileUpload runat="server" ID="img" ToolTip="Selecione uma Imagem" CssClass="btn" /><br />
            <asp:Button runat="server" ID="btnUpload" type="submit" Text="Upload" class="btn btn-primary" OnClick="btnUpload_Click" />
            <asp:Label runat="server" id="StatusLabel" text="" ForeColor="Red" />
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
</div>
</div>
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
