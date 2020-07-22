<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cidadao.aspx.cs" Inherits="SEMA.cidadao" validateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
  <!-- Bootstrap 3.3.7 -->
  <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css"/>
  <!-- Font Awesome -->
  <link rel="stylesheet" href="bower_components/fontawesome-pro/css/all.min.css"/>
  <!-- Ionicons -->
  <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css"/>
  <!-- Theme style -->
  <link rel="stylesheet" href="dist/css/AdminLTE.min.css"/>
  <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css"/>
  <!-- bootstrap wysihtml5 - text editor -->
  <link rel="stylesheet" href="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>
  <!-- Google Font -->
  <link rel="stylesheet"href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"/>
  <link rel="stylesheet" href="plugins/toastr/toastr.min.css">
    <style>
        body{
            font-family: Arial, Helvetica, sans-serif!important;
        }
        label {
            font-weight: normal !important;
        }

    </style>
    <style>
          #toast-container > div {
              -moz-box-shadow: 0 0 12px #000000;
              -webkit-box-shadow: 0 0 12px #000000;
              box-shadow: 0 0 12px #000000;
              opacity: 1;
              -ms-filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=100);
              filter: alpha(opacity=100);
              cursor: pointer;
            }
      </style>
  
    <title>Dúvida Cidadão</title>
</head>
<body>
    <h3 style="text-align:center" class="lead"><b>Caso ainda tenha ficado alguma dúvida faça aqui sua pergunta</b></h3>
    <form runat="server">
    <section class="content">
      <div class="container-fluid">
        <div class="box box-default">
          <div class="box-header with-border" >
            <h3 class="box-title"><i class="far fa-bullhorn"></i></h3>
            <div class="box-tools">
              <asp:Button Text="Enviar Pergunta" CssClass="btn btn-sm btn-primary" runat="server" ID="btnEnviar" OnClick="btnEnviar_Click" />
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
                        <asp:TextBox runat="server" ID="resp_nome" CssClass="form-control" ReadOnly="true"  />
                    </div>
                  </div>
                <!-- /.form-group -->
                <div class="col-md-2">
                    <div class="form-group">
                      <label>E-Mail</label>
                        <asp:TextBox runat="server" ID="resp_email" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
               <div class="col-md-2">
                    <div class="form-group">
                      <label>Telefone</label>
                        <asp:TextBox runat="server" ID="resp_telefone" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                      <label>CPF</label>
                        <asp:TextBox runat="server" ID="resp_cpf" CssClass="form-control" data-inputmask='"mask": "999.999.999-99"' data-mask  ReadOnly="true"/>
                    </div>
                </div>
                <!-- /.form-group -->
            </div>
              <div class="row">
                <div class="col-md-4">
                <div class="form-group">
                  <label>Assunto</label>
                    <asp:DropDownList runat="server" ID="resp_cboxAssunto" CssClass="form-control" Enabled="false" >
                    </asp:DropDownList>
                </div>
                </div>
                  <div class="col-md-6">
                     <div class="form-group">
                       <label>Tópico</label>
                          <asp:DropDownList runat="server" ID="resp_cboxTopico" CssClass="form-control" Enabled="false">
                          </asp:DropDownList>
                     </div>
                  </div>
                  <div class="col-md-2">
                    <div class="form-group">
                      <label>Status</label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="resp_cboxStatus" Enabled="false">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
                  
            <div class="box-body pad">
              <form>
                    <textarea id="editor1" name="editor1" rows="10" cols="80" runat="server">
                    </textarea>
              </form>
            </div>
        </div>
      </div>        
    </div>
   </section>  
 </form>
 <!-- jQuery 3 -->
<script src="bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- FastClick -->
<script src="bower_components/fastclick/lib/fastclick.js"></script>
<!-- AdminLTE App -->
<script src="dist/js/adminlte.min.js"></script>
<!-- AdminLTE for demo purposes -->
<script src="dist/js/demo.js"></script>
<!-- CK Editor -->
<script src="bower_components/ckeditor/ckeditor.js"></script>
<!-- Bootstrap WYSIHTML5 -->
<script src="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
<script src="plugins/toastr/toastr.min.js"></script>
    
    <script type="text/javascript">
        $(function sucesso() {
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
        });
        
    </script>
        <script type="text/javascript">
        (function erro() {
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
        });
    </script>
    
<script>
  $(function () {
    // Replace the <textarea id="editor1"> with a CKEditor
    // instance, using default configuration.
    CKEDITOR.replace('editor1')
    //bootstrap WYSIHTML5 - text editor
    $('.textarea').wysihtml5()
  })
</script>
</body>
</html>
