<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuracoes.aspx.cs" Inherits="SEMA.Configuracoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="content-wrapper">
  <!-- Content Header (Page header) -->
  <section class="content-header">
    <h1>
      Personalizacão do Tema
    </h1>
    <ol class="breadcrumb">
      <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
      <li class="active">Configurações</li>
    </ol>
  </section>
  <!-- /.content-header -->
  <!-- Main content -->
  <section class="content">
    <div class="container-fluid">
      <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title"><i class="fas fa-cog"></i></h3>
          <div class="box-tools">
            <asp:Button Text="Salvar" CssClass="btn btn-sm btn-primary" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" />
            <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click"/>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-6">
              <label>Selecione a Secretaria</label>
              <asp:DropDownList runat="server" ID="cboxSecretaria" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cboxSecretaria_SelectedIndexChanged">
            </asp:DropDownList>
          </div>
        </div>
        <div class="row">
          <div class="col-md-4">
            <h3>Menu</h3>
            <table>
              <tr>
                <td><div class="pull-left"><label>Background Color:</label></div></td>
                <td><div class="pull-right"><input type="color" runat="server" id="bckColorMenu" /></div></td>
              </tr>
              <tr>
                <td><div class="pull-left"><label>Background Color on hover:</label></div></td>
                <td><div class="pull-right"><input type="color" runat="server" id="onHoverbckColorMenu" /></div></td>
              </tr>
              <tr>
                <td><div class="pull-left"><label>Text Color:</label></div></td>
                <td><div class="pull-right"><input type="color"  runat="server" id="textColorMenu" /></div></td>
              </tr>
              <tr>
                <td><div class="pull-left"><label>Text Color on hover:</label></div></td>
                <td><div class="pull-right"><input type="color"  runat="server" id="onHovertextColorMenu" /></div></td>
              </tr>
            </table>
          </div>
          <div class="col-md-4">
            <h3>SubMenu</h3>
            <table>
              <tr>
                <td><div class="pull-left"><label>Background color:</label></div></td>
                <td><div class="pull-right"><input type="color" runat=server id="bckColorSbMenu" /></div></td>
              </tr>
              <tr>
                <td><div class="pull-left"><label>Background color on hover:</label></div></td>
                <td><div class="pull-right"><input type="color" runat=server id="onHoverbckColorSbMenu" /></div></td>
              </tr>
              <tr>
                <td><div class="pull-left"><label>Text color:</label></div></td>
                <td><div class="pull-right"><input type="color" runat=server id="textColorSbMenu" /></div></td>
              </tr>
              <tr>
                <td><div class="pull-left"><label>Text color On hover:</label></div></td>
                <td><div class="pull-right"><input type="color" runat=server id="onHovertextColorSbMenu" /></div></td>
              </tr>
            </table>
          </div>
          <div class="col-md-4">
            <h3>Barra Superior</h3>
            <table>
              <tr>
                <td><div class="pull-left"><label>Background color:</label></div></td>
                <td><div class="pull-right"><input type="color" runat=server id="bckColorNavbar" /></div></td>
              </tr>
            </table>
          </div>
        </div>
        <div class="row">
          <div class="col-md-4">
            <h3>Logo</h3>
              <h4><p class="text-muted small">Para melhor qualidade a imagem deve ter fundo transparente e nas dimensões de 160px por 160px</p></h4>
            <div class="form-group">
              <asp:Label Text="" runat="server" ID="lblCaminhoImg" />
              <asp:Image runat="server" ID="imgSel" Width="160px" Height="160px" />
              <asp:FileUpload runat="server" ID="img" ToolTip="Selecione uma Imagem" CssClass="btn" ClientIDMode="Static" onchange="this.form.submit()"    />
              <asp:Label runat="server" id="StatusLabel" text="" ForeColor="Red" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
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
      toastr["info"]("Acesso Permitido apenas a Usuários Administradores da Companhia de Tecnologia e Desenvolvimento de Londrina", "Informação")
    };
  </script>
</asp:Content>
