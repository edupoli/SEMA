<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SEMA.WebForm1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <asp:TextBox runat="server" ID="testeText" Height="271" Width="985" />
          <ajaxToolkit:HtmlEditorExtender ID="testeText_HtmlEditorExtender" runat="server" 
          EnableSanitization="false" BehaviorID="testeText_HtmlEditorExtender" TargetControlID="testeText">
        </ajaxToolkit:HtmlEditorExtender>
      </div>
    </div>
  </div>
</section>
</div>
</asp:Content>
