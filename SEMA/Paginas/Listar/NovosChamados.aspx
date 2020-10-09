<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NovosChamados.aspx.cs" Inherits="SEMA.NovosChamados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="wrapper">
  <div class="content-wrapper">
    <section class="content-header">
      <h1>
        Chamados disponíves para Atendimento  
      </h1>
      <ol class="breadcrumb">
        <li><a href="/home.aspx"><i class="fas fa-home"></i> Home</a></li>
        <li class="active">Chamados</li>
      </ol>
    </section>
    <section class="content">
      <div class="container-fluid">
        <div class="row">
          <div class="col-12">
            <div class="box">
              <div class="box-header with-border">
                <h3 class="box-title"></h3>
              </div>
              <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="100000"></asp:Timer>
              <div class="box-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="Timer1" />
                </Triggers>
                <ContentTemplate>
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" emptydatatext="Não existem dados Cadastrados!!" class="table table-bordered table-hover table-sm">
                  <Columns>
                    <asp:BoundField ItemStyle-Width="80px" DataField="data" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="protocolo" HeaderText="Protocolo" />
                    <asp:BoundField DataField="nome" HeaderText="Nome" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="cpf" HeaderText="CPF / CNPJ" />
                    <asp:BoundField ItemStyle-Width="110px" DataField="telefone" HeaderText="Telefone" />
                    <asp:BoundField DataField="assunto" HeaderText="Assunto" />
                    <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="false" />
                    <asp:TemplateField HeaderText="Ações" ItemStyle-Wrap="false">
                    <ItemTemplate>
                      <asp:LinkButton class="btn badge-secondary" Text="" data-toggle="tooltip" title="Visualizar" data-placement="auto" ID="btnVisualizar" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnVisualizar_Click" ><i class="far fa-eye"></i></asp:LinkButton>
                      <asp:LinkButton class="btn badge-info" Text="" data-toggle="tooltip" title="Responder Chamado" data-placement="auto" ID="btnResponder" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnResponder_Click" ><i class="fas fa-edit"></i></asp:LinkButton>
                      <asp:LinkButton class="btn badge-danger" Text="" data-toggle="tooltip" title="Excluir" data-placement="auto" ID="btnExcluir" runat="server" CommandName="Delete" CommandArgument='<%# Eval("id") %>' OnClick="btnExcluir_Click" OnClientClick="return sweetAlertConfirm(this);" ><i class="fas fa-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
              </asp:GridView>
            </ContentTemplate>
          </asp:UpdatePanel>
        </div>
      </div>
    </div>
  </div>
</div>
</section>
</div>
</div>
<script type="text/javascript">
  $(function () {
    $('[data-toggle="tooltip"]').tooltip()
  })
  function sucesso() {
    toastr.success('Deletado com Sucesso!!!')        
  };
</script>
<script type="text/javascript">
  function erro() {
    toastr.options = {
      "closeButton": false,
      "debug": false,
      "newestOnTop": true,
      "progressBar": true,
      "positionClass": "toast-top-full-width",
      "preventDuplicates": true,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "1000",
      "timeOut": "8000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "fadeIn",
      "hideMethod": "fadeOut"
    }
    toastr["error"]("Não é possível deletar esse registro, pois esta sendo utilizado no cadastro de um ou mais Usuarios. ", "Erro")
  };
</script>
<script type="text/javascript">
  function erroGeral() {
    toastr.options = {
      "closeButton": false,
      "debug": false,
      "newestOnTop": true,
      "progressBar": true,
      "positionClass": "toast-top-full-width",
      "preventDuplicates": true,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "1000",
      "timeOut": "8000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "fadeIn",
      "hideMethod": "fadeOut"
    }
    toastr["error"]("<%= mensagem %>", "Erro")
  };
</script>
<script type="text/javascript">
  function acessoNegado() {
    toastr.options = {
      "closeButton": false,
      "debug": false,
      "newestOnTop": true,
      "progressBar": true,
      "positionClass": "toast-top-full-width",
      "preventDuplicates": true,
      "onclick": null,
      "showDuration": "300",
      "hideDuration": "1000",
      "timeOut": "8000",
      "extendedTimeOut": "1000",
      "showEasing": "swing",
      "hideEasing": "linear",
      "showMethod": "fadeIn",
      "hideMethod": "fadeOut"
    }
    toastr["info"]("Acesso somente a usuarios Administradores. ", "Erro")
  };
</script>
<script>
  function pageLoad(){ 
    $('#<%= GridView1.ClientID%>').prepend($("<thead></thead>").append($("#<%= GridView1.ClientID%>").find("tr:first"))).DataTable({
      "bJQueryUI": true,
      "autoWidth": true,
      "order": [[ 2, "asc" ]],
      "oLanguage": {
        "sProcessing":   "Processando...",
        "sLengthMenu":   "Mostrar _MENU_ registros",
        "sZeroRecords":  "Não foram encontrados resultados",
        "sInfo":         "Mostrando de _START_ até _END_ de _TOTAL_ registros",
        "sInfoEmpty":    "Mostrando de 0 até 0 de 0 registros",
        "sInfoFiltered": "",
        "sInfoPostFix":  "",
        "sSearch":       "Pesquisar:",
        "sUrl":          "",
        "oPaginate": {
          "sFirst":    "Primeiro",
          "sPrevious": "Anterior",
          "sNext":     "Seguinte",
          "sLast":     "Último"
        }
      }
    }) 
  };
</script>
<script type="text/javascript">

    function sweetAlertConfirm(btnExcluir) {

        if (btnExcluir.dataset.confirmed) {
            // The action was already confirmed by the user, proceed with server event
            btnExcluir.dataset.confirmed = false;
            return true;
        } else {
            // Ask the user to confirm/cancel the action
            event.preventDefault();
            swal({
                title: 'Você tem certeza que deseja deletar esse Registro?',
                text: "Esta operação é irreversível",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            })
                .then(function () {
                    // Set data-confirmed attribute to indicate that the action was confirmed
                    btnExcluir.dataset.confirmed = true;
                    // Trigger button click programmatically
                    btnExcluir.click();
                    swal({ text: 'Deletado com Sucesso', type: 'success' });
                }).catch(function (reason) {
                    // The action was canceled by the user
                    return false
                });
        }
    }    
</script>
</asp:Content>
