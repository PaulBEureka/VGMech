<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSite.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="VisualMech.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <!-- Page Heading -->
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">Dashboard</h1>
    </div>

    <!-- Information Cards -->
    <div class="row">
        <asp:Literal runat="server" ID="InformationCardsPanel"></asp:Literal>

    </div>


    <div class="row text-center">
        <div class="col m-0 d-block text-center">
            <canvas id="learnChart" width="400" height="200" class="shadow"></canvas>
        </div>
    </div>


    
    <script src="/Scripts/chart.min.js"></script>


    <script>
        var chartData = <%= GetChartData() %>;

        var ctx = document.getElementById('learnChart').getContext('2d');
        var learnChart = new Chart(ctx, {
            type: 'bar',
            data: chartData,
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            stepSize: 1, 
                            precision: 0
                        }
                    }
                },
                responsive: true, 
                plugins: {
                    legend: {
                        labels: {
                            boxWidth: 0,
                            usePointStyle: false,
                            font: {
                                weight: 'bold',
                                size: 16
                            }
                        }
                    }
                },
                layout: {
                    padding: {
                        left: 10,
                        right: 10,
                        top: 10,
                        bottom: 10
                    }
                },
                aspectRatio: 4 
            }
        });


        

    </script>


    
    
</asp:Content>
