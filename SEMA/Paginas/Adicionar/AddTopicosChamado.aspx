<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTopicosChamado.aspx.cs" Inherits="SEMA.AddTopicosChamado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="content-wrapper">
    <section class="content-header">
        <h1>
            Tópicos em Chamados
        </h1>
        <ol class="breadcrumb">
            <li><a href="/home.aspx"><i class="fas fa-home"></i> Home</a></li>
            <li class="active">Tópicos em Chamados</li>
        </ol>
    </section>
    <section class="content">
        <div class="container-fluid">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title"><i class="fad fa-comments-alt"></i></h3>
                    <div class="box-tools">
                        <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click"/>
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fas fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fas fa-window-close"></i></button>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Selecione a Secretaria</label>
                                <asp:DropDownList runat="server" ID="cboxSecretaria" CssClass="form-control" DataTextField="descricao" DataValueField="id" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="cboxSecretaria_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <h3>Assunto</h3>
                            <asp:ListBox ID="ListBox1" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" Height="220px" Width="100%"></asp:ListBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="textAssunto" CssClass="form-control form-group" runat="server"/>
                            <asp:LinkButton ID="btnAdicionarAssunto" CssClass="btn btn-twitter" runat="server" data-toggle="tooltip" data-placement="top" title="Adicionar" OnClick="btnAdicionarAssunto_Click"><i class="fas fa-plus"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnEditarAssunto" CssClass="btn btn-warning" runat="server" data-toggle="tooltip" data-placement="top" title="Editar" OnClick="btnEditarAssunto_Click"><i class="fas fa-edit"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnExcluirAssunto" CssClass="btn btn-danger" runat="server" data-toggle="tooltip" data-placement="top" title="Excluir" OnClick="btnExcluirAssunto_Click" OnClientClick="return sweetAlertConfirm(this);" ><i class="fas fa-trash"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <h3>Tópico</h3>
                            <asp:ListBox ID="ListBox2" CssClass="form-control" runat="server" Height="220px" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ListBox2_SelectedIndexChanged"></asp:ListBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="textTopico" CssClass="form-control form-group" runat="server"/>
                            <asp:LinkButton ID="btnAdicionarTopico" CssClass="btn btn-twitter" runat="server" data-toggle="tooltip" data-placement="top" title="Adicionar" OnClick="btnAdicionarTopico_Click"><i class="fas fa-plus"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnEditarTopico" CssClass="btn btn-warning" runat="server" data-toggle="tooltip" data-placement="top" title="Editar" OnClick="btnEditarTopico_Click"><i class="fas fa-edit"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnExcluirTopico" CssClass="btn btn-danger" runat="server" data-toggle="tooltip" data-placement="top" title="Excluir" OnClick="btnExcluirTopico_Click" OnClientClick="return sweetAlertConfirm2(this);"><i class="fas fa-trash"></i></asp:LinkButton>
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
    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })
</script>
<script type="text/javascript">
  function sweetAlertConfirm(btnExcluirAssunto) {
    if (btnExcluirAssunto.dataset.confirmed) {
        btnExcluirAssunto.dataset.confirmed = false;
      return true;
    } else {
      event.preventDefault();
      swal({
        title: 'Você tem Certeza que deseja Deletar ?',
        text: "Essa Ação é Irreversível, e tambem afetará chamados que estejam utilizando esse item",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'SIM',
        cancelButtonText: 'NÃO'
      })
      .then(function () {
          btnExcluirAssunto.dataset.confirmed = true;
        btnExcluirAssunto.click();
      }).catch(function (reason) {
        return false
      });
    }
  }    
</script>
<script type="text/javascript">
  function sweetAlertConfirm2(btnExcluirTopico) {
    if (btnExcluirTopico.dataset.confirmed) {
        btnExcluirTopico.dataset.confirmed = false;
      return true;
    } else {
      event.preventDefault();
      swal({
        title: 'Você tem Certeza que deseja Deletar ?',
        text: "Essa Ação é Irreversível, e tambem afetará chamados que estejam utilizando esse item",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'SIM',
        cancelButtonText: 'NÃO'
      })
      .then(function () {
          btnExcluirTopico.dataset.confirmed = true;
        btnExcluirTopico.click();
      }).catch(function (reason) {
        return false
      });
    }
  }    
</script>
</asp:Content>
