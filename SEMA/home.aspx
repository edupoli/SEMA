<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="SEMA.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
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

    <!-- Main content -->
    <section class="content">
      <!-- Small boxes (Stat box) -->
      <div class="row">
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-aqua">
            <div class="inner">
              <h3>150</h3>

              <p>Novos Chamados</p>
            </div>
            <div class="icon">
              <i class="fal fa-bullhorn" style="font-size:70px"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-green">
            <div class="inner">
              <h3>53<sup style="font-size: 20px"></sup></h3>

              <p>Finalizados</p>
            </div>
            <div class="icon">
              <i class="ion ion-stats-bars"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-yellow">
            <div class="inner">
              <h3>44</h3>

              <p>Atendimentos</p>
            </div>
            <div class="icon">
              <i class="far fa-construction" style="font-size:60px"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
          <!-- small box -->
          <div class="small-box bg-red">
            <div class="inner">
              <h3>65</h3>

              <p>Chamados Pendentes</p>
            </div>
            <div class="icon">
              <i class="fad fa-siren-on" style="font-size:74px"></i>
            </div>
            <a href="#" class="small-box-footer">Consultar <i class="fa fa-arrow-circle-right"></i></a>
          </div>
        </div>
        <!-- ./col -->
      </div>
      <!-- /.row -->
      <!-- Main row -->
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
            <!-- /.box-body -->
          </div>
        </div>
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
            <!-- /.box-body -->
          </div>
          </div>
        <!-- right col -->
      </div>
      <!-- /.row (main row) -->

    </section>
    <!-- /.content -->
  </div>
    
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
  <script>
    var ctx = document.getElementById("pieChart").getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels  : ['Abertos', 'Em Atendimento', 'Pendentes', 'Finalizados'],
            datasets: [{
                label: 'Abertos',
                data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(DataChamados)%>,
                backgroundColor: ['#00c0ef','#f39c12','#f56954','#00a65a']
            }]
        },
        options: {
            segmentShowStroke    : true,
          //String - The colour of each segment stroke
          segmentStrokeColor   : '#fff',
          //Number - The width of each segment stroke
          segmentStrokeWidth   : 2,
          //Number - The percentage of the chart that we cut out of the middle
          percentageInnerCutout: 50, // This is 0 for Pie charts
          //Number - Amount of animation steps
          animationSteps       : 100,
          //String - Animation easing effect
          animationEasing      : 'easeOutBounce',
          //Boolean - Whether we animate the rotation of the Doughnut
          animateRotate        : true,
          //Boolean - Whether we animate scaling the Doughnut from the centre
          animateScale         : false,
          //Boolean - whether to make the chart responsive to window resizing
          responsive           : true,
          // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
          maintainAspectRatio  : true,
        }
    });
    </script>
</asp:Content>
