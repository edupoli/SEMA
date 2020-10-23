<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewChamados.aspx.cs" Inherits="SEMA.ViewChamados" validateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<style>
    .nav-tabs>li.active>a {
        background-color: #478978 !important;
        color: #ffffff !important;
    }
    .modal-static-backdrop {
        -ms-filter:"progid:DXImageTransform.Microsoft.Alpha(Opacity=50)";
        filter: alpha(opacity=50);
        --moz-opacity:0.5;
        --khtml-opacity: 0.5;
        opacity: 0.5;
        background:#000;
        height:100%;
        left:0;
        position:fixed;
        top:0;
        width:100%;
        z-index:2000;
    }
    .modal-static {
        bottom: auto;
        display:block !important;
        position:absolute;
        z-index:2001;
    }
</style>
<div class="wrapper">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Visualizar Chamado</h1>
            <ol class="breadcrumb">
                <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
                <li class="active">Chamados</li>
            </ol>
        </section>
        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab_1" data-toggle="tab">Informações do Chamado</a></li>
                <li><a href="#tab_2" data-toggle="tab">Histórico de Mensagens</a></li>
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
                                        <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-4">
                <div class="form-group">
                  <label>É denuncia anonima ?</label>
                  <asp:CheckBox Text="" runat="server" ID="checkDenuncia" Enabled="false"/>
                </div>
              </div>
              <div class="col-md-4">
                <div class="form-group">
                  <label>Enviar Notificações pelo WhatsApp ?</label>
                  <asp:CheckBox Text="" runat="server" ID="resp_checkWhatsapp" Enabled="false" />
                </div>
              </div>
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Nº Protocolo</label>
                                                <asp:TextBox runat="server" ID="txtProtocolo" CssClass="form-control"  ReadOnly="true"/>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Nome</label>
                                                <asp:TextBox runat="server" ID="txtnome" CssClass="form-control" ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>E-Mail</label>
                                                <asp:TextBox runat="server" ID="txtemail" CssClass="form-control" ReadOnly="true" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Telefone</label>
                                                <asp:TextBox runat="server" ID="txttelefone" CssClass="form-control" ReadOnly="true"  />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>CPF ou CNPJ</label>
                                                <asp:TextBox runat="server" ID="txtcpf" CssClass="form-control" data-inputmask='"mask": "999.999.999-99"' data-mask ReadOnly="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Assunto</label>
                                                <asp:DropDownList runat="server" ID="cboxAssunto" CssClass="form-control" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Tópico</label>
                                            <asp:DropDownList runat="server" ID="cboxTopico" CssClass="form-control" Enabled="false">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Status</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="cboxStatus" Enabled="false" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>CEP</label>
                                    <asp:TextBox runat="server" ID="txtCEP" CssClass="form-control" Enabled="false"/>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Rua</label>
                                    <asp:TextBox runat="server" ID="txtRua" CssClass="form-control" Enabled="false"/>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <label>Número</label>
                                    <asp:TextBox runat="server" ID="txtNumero" CssClass="form-control" Enabled="false"/>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bairro</label>
                                    <asp:TextBox runat="server" ID="txtBairro" CssClass="form-control" Enabled="false"/>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Cidade</label>
                                    <asp:TextBox runat="server" ID="txtCidade" CssClass="form-control" Enabled="false"/>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="descricao" TextMode="MultiLine" Enabled="false"/>
                                </div>
                            </div>
                            <div class="col-md-4" style="height:306px;">
                                <div class="form-group">
                                    <asp:Label Text="" runat="server" ID="lblCaminhoImg" />
                                    <asp:ImageButton runat="server" ID="Image1" Width="294.33px" Height="280px"  OnClick="Image1_Click" />
                                    <asp:FileUpload runat="server" ID="img" ToolTip="Selecione uma Imagem" CssClass="btn" ClientIDMode="Static" onchange="this.form.submit()" Enabled="false"   />
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
                <asp:PlaceHolder ID="ModalPlaceHolder" runat="server" Visible="false">
                <div class="modal-static-backdrop"></div>
                <div class="modal modal-static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:LinkButton ID="btnModalCloseHeader" runat="server" CssClass="close" OnClick="btnModalCloseHeader_Click" ><span aria-hidden="true">×</span></asp:LinkButton>
                            </div>
                            <div class="modal-body">
                                <asp:Image runat="server" ID="imgSel" CssClass="img-responsive"/>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
        </ContentTemplate>
    </asp:UpdatePanel>
</section>
</div>
<div class="tab-pane" id="tab_2">
    <style>          
        .container1 {
            border: 2px solid #dedede;
            background-color: #f1f1f1;
            border-radius: 5px;
            padding: 5px;
            margin: 10px auto!important;
            width:80%!important;
        }
        .darker {
            border-color: #ccc;
            background-color: #ddd;
        }
        .container1::after {
            content: "";
            clear: both;
            display: table;
        }
        .container1 img {
            float: left;
            max-width: 60px;
            width: 100%;
            margin-right: 20px;
            border-radius: 50%;
        }
        .container1 img.right {
            float: right;
            margin-left: 20px;
            margin-right:0;
        }
        .time-right {
            float: right;
            color: #aaa;
        }
        .time-left {
            float: left;
            color: #999;
        }
    </style>                 
    <asp:Literal ID="historicoMsg" runat="server" />
</div>
</div>
</div>
</div>
</div>
<script>
    CKEDITOR.plugins.addExternal('wordcount', '/bower_components/ckeditor/plugins/wordcount/', 'plugin.js');
    CKEDITOR.plugins.addExternal( 'autosave', '/bower_components/ckeditor/plugins/autosave/', 'plugin.js' );
	CKEDITOR.replace( '<%=descricao.ClientID%>',{ customConfig : '/bower_components/ckeditor/config.js' } );

	CKEDITOR.on( 'instanceReady', function( ev ) {
		ev.editor.setData('<p style="text-align:justify;"></p>');
	});
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
