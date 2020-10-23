<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="SEMA.WebForm3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="wrapper">
  <div class="content-wrapper">
    <section class="content-header">
      <h1>Responder Chamado</h1>
      <br />
      <ol class="breadcrumb">
        <li><a href="home.aspx"><i class="fas fa-home"></i> Home</a></li>
        <li class="active">Chamados</li>
      </ol>
    </section>

    <div class="col-md-4" style="height:306px;">
                <div class="form-group">
                  <asp:Label Text="" runat="server" ID="lblCaminhoImg" />
                    
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                  <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="img" />
                  </Triggers>
                  <ContentTemplate>
                       <asp:Image runat="server" ID="Image1" Width="294.33px" Height="280px"   />
                      </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    
                    <ajaxToolkit:AsyncFileUpload ID="img" OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError"
                    CompleteBackColor="White" Width="350px" runat="server" UploaderStyle="Traditional" UploadingBackColor="#CCFFFF"
                    ThrobberID="imgLoad" OnUploadedComplete="fileUploadComplete"  /><br />
                    <asp:Image ID="imgLoad" runat="server" ImageUrl="/dist/img/loading.gif" />
                    <br />
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                    
                  

           </div>         


                </div>
      </div>
        </div>
    
</asp:Content>
