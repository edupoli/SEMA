<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chamados.aspx.cs" Inherits="SEMA.Chamados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="wrapper">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Chamados
      </h1>
      <ol class="breadcrumb">
        <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
        <li class="active">Chamados</li>
      </ol>
    </section>
    <!-- /.content-header -->
    <!-- Main content -->
    

    <section class="content">
     <div class="container-fluid">
      <div class="row">
        <div class="col-12">
          <div class="box">
            <div class="box-header with-border">
              <h3 class="box-title">Chamados Cadastrados</h3>
            </div>
              <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="10000"></asp:Timer>
            <!-- /.box-header -->
              <div class="box-body">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                  <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" />
                      </Triggers>
                      <ContentTemplate>
                                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" emptydatatext="Não existem dados Cadastrados!!" class="table table-bordered table-hover table-sm">
                                  <Columns>
                                      <asp:BoundField DataField="protocolo" HeaderText="Protocolo" />
                                      <asp:BoundField DataField="nome" HeaderText="Nome" />
                                      <asp:BoundField DataField="cpf" HeaderText="CPF" />
                                      <asp:BoundField DataField="telefone" HeaderText="Telefone" />
                                      <asp:BoundField DataField="assunto" HeaderText="Assunto" />
                                      <asp:BoundField DataField="status" HeaderText="Status" />
            
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <asp:LinkButton class="btn badge-secondary" Text="" data-toggle="tooltip" title="Visualizar" data-placement="auto" ID="btnVisualizar" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnVisualizar_Click" ><i class="far fa-eye"></i></asp:LinkButton>
                                        <asp:LinkButton class="btn badge-info" Text="" data-toggle="tooltip" title="Editar" data-placement="auto" ID="btnEditar" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnEditar_Click" ><i class="fas fa-edit"></i></asp:LinkButton>
                                        <asp:LinkButton class="btn badge-danger" Text="" data-toggle="tooltip" title="Excluir" data-placement="auto" ID="btnExcluir" runat="server" CommandArgument='<%# Eval("id") %>' OnClick="btnExcluir_Click" OnClientClick="return confirm('Tem Certeza que deseja Excluir ?')" ><i class="fas fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                              </asp:GridView>
                          </ContentTemplate>
                  </asp:UpdatePanel>
            </div>
            <!-- /.box-body -->

          </div>
          <!-- /.box -->

        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </div>
    </section>

    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->

 
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
            toastr["error"]("<%=mensagem%>", "Erro")
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
</asp:Content>
