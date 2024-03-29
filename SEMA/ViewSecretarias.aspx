﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewSecretarias.aspx.cs" Inherits="SEMA.ViewSecretarias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="content-wrapper">
  <section class="content-header">
    <h1>
      Visualizar Secretaria
    </h1>
    <ol class="breadcrumb">
      <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
      <li class="active">Secretarias</li>
    </ol>
  </section>
  <section class="content">
    <div class="container-fluid">
      <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title"><i class="fas fa-building"></i></h3>
          <div class="box-tools">
            <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click"/>
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fas fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fas fa-window-close"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
              <div class="form-group">
                <label>Nome da Secretaria</label>
                <asp:TextBox runat="server" ID="nome" CssClass="form-control" ReadOnly="true" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</div>
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
