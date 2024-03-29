﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddSecretarias.aspx.cs" Inherits="SEMA.AddSecretarias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="content-wrapper">
  <!-- Content Header (Page header) -->
  <section class="content-header">
    <h1>
      Cadastrar Secretaria
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
            <asp:Button Text="Salvar" CssClass="btn btn-sm btn-info" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" />
            <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click"/>
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fas fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fas fa-window-close"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-4">
              <div class="form-group">
                <label>Nome da Secretaria</label>
                <asp:TextBox runat="server" ID="nome" CssClass="form-control"  />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>

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
      toastr.success('Gravado com Sucesso!!!')        
    };
  </script>
  <script type="text/javascript">
    function erro() {
      toastr.error('Erro ao Gravar!!!')        
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

  <script type="text/javascript">
    function erroGeral() {
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
      toastr["error"]("<%=mensagem%>", "Erro")
    };
  </script>
</div>
</asp:Content>
