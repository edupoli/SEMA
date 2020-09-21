<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SEMA.login" %>
<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>SEMA</title>
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css">
  <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css">
  <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css">
  <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
  <link rel="stylesheet" href="plugins/iCheck/square/blue.css">
  <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet" />
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
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
  <style>
    body{
      font-family: Arial, Helvetica, sans-serif!important;
    }
  </style>
</head>
<body class="hold-transition login-page">
  <div class="login-box">
    <div class="login-logo">
      <img src="imagens/logo1.png" alt="SEMA" />
    </div>
    <div class="login-box-body">
      <p class="login-box-msg">Faça login para iniciar sua sessão</p>
      <form method="post" runat="server">
        <div class="form-group has-feedback">
          <asp:TextBox runat="server" ID="usuario" CssClass="form-control" placeholder="Usuário"/>
          <span class="glyphicon glyphicon-user form-control-feedback"></span>
        </div>
        <div class="form-group has-feedback">
          <asp:TextBox runat="server"  ID="senha" TextMode="Password" CssClass="form-control"/>
          <span class="glyphicon glyphicon-lock form-control-feedback"></span>
        </div>
        <div class="row">
          <div class="col-xs-8">
            <div class="checkbox icheck">
              <label>
                <input type="checkbox"> Lembrar-Me
              </label>
            </div>
          </div>
          <div class="col-xs-4">
            <asp:Button Text="Entrar" runat="server" ID="btnEntrar" CssClass="btn btn-primary btn-block" OnClick="btnEntrar_Click"/>
          </div>
        </div>
      </form>
      <a href="RecuperSenha.aspx">Esqueci a minha senha</a>
    </div>
  </div>
  <script src="bower_components/jquery/dist/jquery.min.js"></script>
  <script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
  <script src="plugins/iCheck/icheck.min.js"></script>
  <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
  <script src="Scripts/toastr.min.js"></script>
  <script>
    $(function erro() {
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
      toastr.error('<%=mensagem%>')
    });
  </script>
  <script>
    $(function () {
      $('input').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
        increaseArea: '20%'
      });
    });
  </script>
</body>
</html>
