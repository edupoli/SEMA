<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUsuarios.aspx.cs" Inherits="SEMA.ViewUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="wrapper">
  <div class="content-wrapper">
    <section class="content-header">
      <h1>Visualizar Usuário</h1>
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
                  <asp:TextBox runat="server" ID="nome" CssClass="form-control" ReadOnly="true" />
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>E-Mail</label>
                  <asp:TextBox runat="server" ID="email" CssClass="form-control" ReadOnly="true" />
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>Login</label>
                  <asp:TextBox runat="server" ID="login" CssClass="form-control" ReadOnly="true" />
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>Senha</label>
                  <asp:TextBox runat="server" ID="senha" CssClass="form-control" type="password" ReadOnly="true" />
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-4">
                <div class="form-group">
                  <label>Perfil</label>
                  <asp:DropDownList runat="server" ID="cboxPerfil" CssClass="form-control" Enabled="false">
                  <asp:ListItem Text="Comum"  Value="comum"/>
                  <asp:ListItem Text="Administrador" Value="Administrador" />
                </asp:DropDownList>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-group">
                <label>Secretaria</label>
                <asp:DropDownList runat="server" ID="cboxSecretaria" CssClass="form-control" Enabled="false">
              </asp:DropDownList>
            </div>
          </div>
          <div class="col-md-4">
            <div class="form-group">
              <label>Cargo</label>
              <asp:TextBox runat="server" ID="cargo" CssClass="form-control" ReadOnly="true" />
            </div>
          </div>
        </div>
        <div class="col">
          <div class="form-group">
            <asp:Label Text="" runat="server" ID="lblCaminhoImg" />
            <asp:Image runat="server" ID="imgSel" Width="160px" Height="160px" />
            <asp:FileUpload runat="server" ID="img" ToolTip="Selecione uma Imagem" CssClass="btn btn-secondary" Enabled="false" /><br />
            <asp:Button runat="server" ID="btnUpload" type="submit" Text="Upload" class="btn btn-primary" Enabled="false"/>
            <asp:Label runat="server" id="StatusLabel" text="" ForeColor="Red" />
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
</div>
</div>
<script>
  $('[data-mask]').inputmask()
</script>
</asp:Content>
