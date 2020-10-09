<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="SEMA.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="content-wrapper">
  <section class="content-header">
    <h1>
      Dashboard
      <small>Estatisticas</small>
    </h1>
    <ol class="breadcrumb">
      <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
      <li class="active">Dashboard</li>
    </ol>
  </section>
  <style>
    .col-lg-3{
      width:20%!important;
    }
  </style>
  <section class="content">
    <div class="row">
      <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="10000"></asp:Timer>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
      <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer1" />
      </Triggers>
      <ContentTemplate>
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3><asp:Label ID="qtaAbertos" runat="server" /></h3>
              <p>Novos Chamados</p>
            </div>
            <div class="icon">
              <i class="fal fa-bullhorn" style="font-size:70px"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <div class="col-lg-3 col-xs-6">
          <div class="small-box bg-green">
            <div class="inner">
              <h3><asp:Label ID="qtaFinalizados" runat="server" /><sup style="font-size: 20px"></sup></h3>
              <p>Finalizados</p>
            </div>
            <div class="icon">
              <i class="ion ion-stats-bars"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <div class="col-lg-3 col-xs-6">
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3><asp:Label ID="qtaAtendimentos" runat="server" /></h3>
              <p>Atendimentos</p>
            </div>
            <div class="icon">
              <i class="far fa-construction" style="font-size:60px"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <div class="col-lg-3 col-xs-6">
          <div class="small-box bg-red">
            <div class="inner">
              <h3><asp:Label ID="qtaPendentes" runat="server" /></h3>
              <p>Pendentes</p>
            </div>
            <div class="icon">
              <i class="fad fa-siren-on" style="font-size:74px"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <div class="col-lg-3 col-xs-6">
          <div class="small-box " style="background-color:#9400D3;color:white;">
            <div class="inner">
              <h3><asp:Label ID="qtaRetornoCidadao" runat="server" /></h3>
              <p>Retorno Cidadão</p>
            </div>
            <div class="icon">
              <i class="fad fa-user" style="font-size:74px"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  <div class="row">
    <div class="col-md-6">
      <div class="box box-success">
        <div class="box-header with-border">
          <h3 class="box-title">Nº Chamados</h3>
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
            </button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="chart">
            <canvas id="barChart" style="min-height: 250px; height: 250px; max-height: 250px; max-width: 100%;"></canvas>
          </div>
        </div>
      </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
      <div class="col-md-6">
        <div class="box box-danger">
          <div class="box-header with-border">
            <h3 class="box-title">Status dos Chamados</h3>
            <div class="box-tools pull-right">
              <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
              </button>
              <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
            </div>
          </div>
          <div class="box-body">
            <canvas id="pieChart" style="height:250px"></canvas>
          </div>
        </div>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</div>
</div>
</section>
</div>
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
    toastr["info"]("<%=mensagem%> Acesso Permitido apenas a Usuários Administradores da Companhia de Tecnologia e Desenvolvimento de Londrina", "Informação")
  };
</script>
<script>
  var ctx = document.getElementById("barChart").getContext('2d');
  var myChart = new Chart(ctx, {
    type: 'bar',
    data: {
      labels  : ['DOM', 'SEG', 'TER', 'QUA', 'QUI', 'SEX', 'SAB'],
      datasets: [{
        label: 'Essa Semana',
        data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data3)%>,
        backgroundColor: '#007bff',
        borderColor: '#007bff',
        borderWidth: 1
      },
      {
        label: 'Semana Passada',
        data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(Data4)%>,
        backgroundColor: '#ced4da',
        borderColor: '#ced4da',
        borderWidth: 1
      }]
    },
    options: {
      scales: {
        yAxes: [{
          ticks: {
            beginAtZero:true
          }
        }]
      }
    }
  });
</script>
<script type="text/javascript">
  function graficoDonuts() {
    var ctx = document.getElementById("pieChart").getContext('2d');
    var myChart = new Chart(ctx, {
      type: 'doughnut',
      data: {
        labels  : ['Abertos', 'Em Atendimento', 'Finalizados','Pendentes','Retorno Cidadao'],
        datasets: [{
          label: 'Abertos',
          data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(DataChamados)%>,
          backgroundColor: ['#00c0ef','#f39c12','#00a65a','#f56954','#9400D3']
        }]
      },
      options: {
        segmentShowStroke    : true,
        segmentStrokeColor   : '#fff',
        segmentStrokeWidth   : 2,
        percentageInnerCutout: 50,
        animationSteps       : 100,
        animationEasing      : 'easeOutBounce',
        animateRotate        : true,
        animateScale         : false,
        responsive           : true,
        maintainAspectRatio  : true,
      }
    });
  }
</script>
</asp:Content>
