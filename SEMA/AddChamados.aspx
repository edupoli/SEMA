<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddChamados.aspx.cs" Inherits="SEMA.AddChamados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="wrapper">
  <div class="content-wrapper">
    <section class="content-header">
      <h1>Cadastrar Novo Chamado</h1> <div id="painel"></div>
      <ol class="breadcrumb">
        <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
        <li class="active">Chamados</li>
      </ol>
    </section>
    <section class="content">
      <div class="container-fluid">
        <div class="box box-default">
          <div class="box-header with-border" >
            <h3 class="box-title"><i class="far fa-bullhorn"></i></h3>
            <div class="box-tools">
              <asp:Button Text="Salvar" CssClass="btn btn-sm btn-primary" runat="server" ID="btnSalvar" OnClick="btnSalvar_Click" />
              <asp:Button Text="Voltar" CssClass="btn btn-sm btn-secondary" runat="server" ID="btnVoltar" OnClick="btnVoltar_Click" />
              <asp:Button Text="" runat="server" ID="btnNovoChamado" ClientIDMode="Static" OnClick="btnNovoChamado_Click" OnClientClick="return sweetAlertConfirm(this);" />
            </div>
          </div>
          <div class="box-body">
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
                  <asp:TextBox runat="server" ID="txtnome" CssClass="form-control"  style="text-transform:capitalize;"  />
                    <a href="#" onclick="document.getElementById('<%=btnNovoChamado.ClientID %>').click()">click me</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="form-group">
                  <label>E-Mail</label>
                  <asp:TextBox runat="server" ID="txtemail" CssClass="form-control"  />
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>Celular</label>
                  <asp:textbox  runat="server" id="txttelefone" class="form-control" OnTextChanged="txttelefone_TextChanged" AutoPostBack="true" onkeypress="$(this).mask('(00) 0 0000-0000')" /> 
                </div>
              </div>
              <div class="col-md-2">
                <div class="form-group">
                  <label>CPF ou CNPJ</label>
                  <asp:TextBox runat="server" ID="txtcpf" data-mask-for-cpf-cnpj CssClass="form-control"  OnTextChanged="txtcpf_TextChanged" AutoPostBack="true" /> <!-- onkeypress="$(this).mask('000.000.000-00')" /> -->
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-4">
                <div class="form-group">
                  <label>Assunto</label>
                  <asp:DropDownList runat="server" ID="cboxAssunto" CssClass="form-control" DataTextField="descricao" DataValueField="id" AppendDataBoundItems="true" OnSelectedIndexChanged="cboxAssunto_SelectedIndexChanged" AutoPostBack="true">
                  <asp:ListItem Text="Selecione"  Value="Selecione"/>
                </asp:DropDownList>
              </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="cboxAssunto" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
              <div class="col-md-6">
                <div class="form-group">
                  <label>Tópico</label>
                  <asp:DropDownList runat="server" ID="cboxTopico" CssClass="form-control" DataTextField="descricao" DataValueField="id" AppendDataBoundItems="true">
                  <asp:ListItem Text="Selecione"  Value="Selecione"/>
                </asp:DropDownList>
              </div>
            </div>
          </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="cboxStatus" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
          <div class="col-md-2">
            <div class="form-group">
              <label>Status</label>
              <asp:DropDownList runat="server" CssClass="form-control" ID="cboxStatus" OnSelectedIndexChanged="cboxStatus_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
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
    <div class="col-md-2">
      <div class="form-group">
        <label>CEP</label>
        <asp:TextBox runat="server" ID="txtCEP" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCEP_TextChanged"/>
        <asp:Label Text="" runat="server" ID="cepNotFound" Font-Bold="True" ForeColor="#CC0000" />
      </div>
    </div>
    <asp:UpdatePanel runat="server" ID="painellEndereco" UpdateMode="Conditional">
    <Triggers>
      <asp:AsyncPostBackTrigger ControlID="txtCEP" EventName="TextChanged" />
    </Triggers>
    <ContentTemplate>
      <div class="col-md-4">
        <div class="form-group">
          <label>Rua</label>
          <asp:TextBox runat="server" ID="txtRua" CssClass="form-control" Enabled="false"/>
        </div>
      </div>
      <div class="col-md-1">
        <div class="form-group">
          <label>Número</label>
          <asp:TextBox runat="server" ID="txtNumero" CssClass="form-control" Enabled="true"/>
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
    </ContentTemplate>
  </asp:UpdatePanel>
</div>
<div class="row">
  <div class="col-md-12">
    <div class="form-group">
      <asp:TextBox runat="server" ID="descricao" TextMode="MultiLine" />
    </div>
  </div>
</div>
</div>
</div>
</div>
</section>
</div>
</div>
<script>
   function temporaryButtonClick() { document.getElementById("<%= btnNovoChamado.ClientID %>").click();}
</script>
<script>
	CKEDITOR.replace( '<%=descricao.ClientID%>' );
	extraPlugins: 'wordcount,notification,exportpdf'

	CKEDITOR.on( 'instanceReady', function( ev ) {
		ev.editor.setData('<p style="text-align:justify;"></p>');
	});
</script>
<script type="text/javascript">
  function sweetAlertConfirm(btnNovoChamado) {
    if (btnNovoChamado.dataset.confirmed) {
      btnNovoChamado.dataset.confirmed = false;
      return true;
    } else {
      event.preventDefault();
      swal({
        title: 'Cadastrado com Sucesso!',
        text: "Você deseja Abrir um Novo Chamado ?",
        type: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'SIM',
        cancelButtonText: 'NÃO'
      })
      .then(function () {
        btnNovoChamado.dataset.confirmed = true;
        btnNovoChamado.click();
        swal({ text: 'Deletado com Sucesso', type: 'success' });
      }).catch(function (reason) {
        return false
      });
    }
  }    
</script>

</asp:Content>
