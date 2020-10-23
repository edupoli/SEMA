<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="SEMA.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="bower_components/jquery/dist/jquery.min.js"></script>
    <script src="/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
    <script src="bower_components/ckeditor/ckeditor.js"></script>
    <link rel="stylesheet" href="/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />
    
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="novaLinha">
    <div>
        <label for="txtVoce">Faleme sobre você:</label>
    </div>
    <div>
        <textarea id="txtVoce" name="txtVoce" cols="40" rows="10"></textarea>
        <p><span id="carResTxtVoce" style="font-weight: bold;">400</span> caracteres restantes</p>
        <p class="aviso" id="avisoTxtVoce" style="display:none;">Este campo não pode ficar vazio!</p>
    </div>
</div>

        </div>
    </form>
</body>
</html>
<script>
    $(function () {
        CKEDITOR.replace('txtVoce',
            {
                toolbar: 'toolbar_full', /* this does the magic */
                uiColor: '#9AB8F3'
            });
    });
</script>

