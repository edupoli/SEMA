<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RespChamado.aspx.cs" Inherits="SEMA.RespChamado" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    

    <style>
        .nav-tabs>li.active>a {
            background-color: #478978 !important;
            color: #ffffff !important;
        }
    </style>
    <div class="wrapper">
  <div class="content-wrapper">
    <section class="content-header">
      <h1>Responder Novo Chamado</h1>
        <br />
      <ol class="breadcrumb">
        <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
        <li class="active">Chamados</li>
      </ol>
    </section>
   <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
              <li class="active"><a href="#tab_1" data-toggle="tab">Resposta ao Chamado</a></li>
              <li><a href="#tab_2" data-toggle="tab">Informações do Chamado</a></li>
              <li class="pull-right"><a href="#" class="text-muted"><i class="far fa-bullhorn"></i></a></li>
            </ul>
            <div class="tab-content">
      <div class="tab-pane active" id="tab_1">
                
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
                <div class="col-md-8">
                    <div class="form-group">
                      <label>Responsável Pelo Chamado</label>
                        <asp:DropDownList runat="server" ID="cboxUsuario" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="cboxStatus" EventName="SelectedIndexChanged" />
                      </Triggers>
                      <ContentTemplate>
                  <div class="col-md-4">
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
                  <div class="col-md-8">
                    <div class="form-group">
                        <CKEditor:CKEditorControl ID="descricao" runat="server"></CKEditor:CKEditorControl>
                    </div>
                </div>
                      <div class="col-md-4" style="height:306px;">
                    <div class="form-group">
                        <asp:Label Text="" runat="server" ID="lblCaminhoImg" />
                        <asp:ImageButton runat="server" ID="Image1" Width="294.33px" Height="280px" data-toggle="modal" data-target="#modal-default"  />
                        <asp:FileUpload runat="server" ID="img" ToolTip="Selecione uma Imagem" CssClass="btn btn-secondary" ClientIDMode="Static" onchange="this.form.submit()"    />
                        <asp:Label runat="server" id="StatusLabel" text="" ForeColor="Red" />
                    </div>
                </div>
            </div>
        </div>
      </div>
    </div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Image1" />
    </Triggers>
    <ContentTemplate>
        <div id="modal-default" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image runat="server" ID="imgSel" CssClass="img-responsive"/>
                </div>
            </div>
          </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


     </section>

              </div>
              <!-- /.tab-pane -->
              <div class="tab-pane" id="tab_2">
                
    <section class="content">
      <div class="container-fluid">
        <div class="box box-default">
          <div class="box-header with-border" >
            <h3 class="box-title"><i class="far fa-bullhorn"></i></h3>
            <div class="box-tools">
              <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar_resp" OnClick="btnVoltar_resp_Click" />
            </div>
          </div>
          <!-- /.box-header -->
          <div class="box-body">
            <div class="row">
                <div class="col-md-2">
                    <div class="form-group">
                      <label>Nº Protocolo</label>
                        <asp:TextBox runat="server" ID="resp_txtProtocolo" CssClass="form-control"  ReadOnly="true"/>
                    </div>
                </div>
                  <div class="col-md-4">
                    <div class="form-group">
                      <label>Nome</label>
                        <asp:TextBox runat="server" ID="resp_nome" CssClass="form-control"  />
                    </div>
                  </div>
                <!-- /.form-group -->
                <div class="col-md-2">
                    <div class="form-group">
                      <label>E-Mail</label>
                        <asp:TextBox runat="server" ID="resp_email" CssClass="form-control"  />
                    </div>
                </div>
               <div class="col-md-2">
                    <div class="form-group">
                      <label>Telefone</label>
                        <asp:TextBox runat="server" ID="resp_telefone" CssClass="form-control"  />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                      <label>CPF</label>
                        <asp:TextBox runat="server" ID="resp_cpf" CssClass="form-control" data-inputmask='"mask": "999.999.999-99"' data-mask />
                    </div>
                </div>
                <!-- /.form-group -->
            </div>
              <div class="row">
                <div class="col-md-4">
                <div class="form-group">
                  <label>Assunto</label>
                    <asp:DropDownList runat="server" ID="resp_cboxAssunto" CssClass="form-control" >
                    </asp:DropDownList>
                </div>
                </div>
                  <asp:UpdatePanel ID="resp_UpdatePanel1" runat="server" UpdateMode="Conditional">
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="resp_cboxAssunto" EventName="SelectedIndexChanged" />
                      </Triggers>
                      <ContentTemplate>
                            <div class="col-md-6">
                                <div class="form-group">
                                  <label>Tópico</label>
                                    <asp:DropDownList runat="server" ID="resp_cboxTopico" CssClass="form-control" >
                                    <asp:ListItem Text="Selecione"  Value="Selecione"/>
                                    </asp:DropDownList>
                                </div>
                            </div>
                      </ContentTemplate>
                  </asp:UpdatePanel>
                  <asp:UpdatePanel ID="resp_UpdatePanel2" runat="server" UpdateMode="Conditional">
                      <Triggers>
                          <asp:AsyncPostBackTrigger ControlID="resp_cboxStatus" EventName="SelectedIndexChanged" />
                      </Triggers>
                      <ContentTemplate>
                  <div class="col-md-2">
                    <div class="form-group">
                      <label>Status</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="resp_cboxStatus" >
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
                
                        <CKEditor:CKEditorControl ID="resp_descricao" runat="server"></CKEditor:CKEditorControl>

                    </div>
                </div>
            </div>
        </div>
      </div>
    </div>
     </section>

              </div>
              <!-- /.tab-pane -->
              
              <!-- /.tab-pane -->
            </div>
            <!-- /.tab-content -->
          </div>
          <!-- nav-tabs-custom -->
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
