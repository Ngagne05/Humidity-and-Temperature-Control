<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="ArduiboProjet.DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../favicon.ico">
    <title>DASHBOARD</title>
    <!-- Bootstrap core CSS -->
    <link href="Styles/bootstrap.min.css" rel="stylesheet">
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <link href="Styles/ie10-viewport-bug-workaround.css" rel="stylesheet">
    <!-- Custom styles for this template -->
    <link href="Styles/dashboard.css" rel="stylesheet">
    <!-- Just for debugging purposes. Don't actually copy these 2 lines! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <script src="Scripts/ie-emulation-modes-warning.js"></script>
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar"
                    aria-expanded="false" aria-controls="navbar">
                    <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                        class="icon-bar"></span><span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">TEMPERATURE AND HUMIDITY CONTROL</a>
            </div>
            <div id="navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="Home.aspx">HOME</a></li>
                    <li><a href="DashBoard.aspx">DASHBOARD</a></li>
                    <li>
                    <li><a href="startPage.aspx">Log out</a></li>
                    <li>
                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="White">Log out</asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-3 col-md-2 sidebar">
                <ul class="nav nav-sidebar">
                    <li class="active"><a href="DashBoard.aspx">DASHBOARD<span class="sr-only">(current)</span></a></li>
                    <li><a href="Settings.aspx">SETTINGS</a></li>
                </ul>
            </div>
            <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
                <h1 class="page-header">
                    Dashboard</h1>
                <div>
                    <div class="container">
                        <div class="row">
                            <!-- création d'une ligne -->
                            <div class="col-md-6">
                                <iframe width="450" height="260" style="border: 1px solid #cccccc;" src="https://thingspeak.com/channels/166643/charts/1?bgcolor=%23ffffff&color=%23d62020&dynamic=true&results=60&type=column">
                                </iframe>
                            </div>
                            <div class="col-md-6">
                                <iframe width="450" height="260" style="border: 1px solid #cccccc;" src="https://thingspeak.com/channels/166643/charts/2?bgcolor=%23ffffff&color=%23d62020&dynamic=true&results=60&type=column">
                                </iframe>
                            </div>
                        </div>
                    </div>
                    <div>
                        <form id="Form1" runat="server">
                        <asp:UpdatePanel ID="updatePanelConteneur" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:ToolkitScriptManager runat="server" ID="toolkitManagerPage">
                                </asp:ToolkitScriptManager>
                                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
                                </asp:Timer>
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div id="tempValue" runat="server" style="width: 300px; height: 150px;">
                                                <asp:Label ID="lblTemp" runat="server" Text="Label" Font-Bold="True" Font-Size="XX-Large"
                                                    ForeColor="#009933"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div id="humidValue" runat="server" style="width: 300px; height: 150px;">
                                                <asp:Label ID="lblHumid" runat="server" Text="Label" Font-Bold="True" Font-Size="XX-Large"
                                                    ForeColor="#339933"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div style="float: right; position: relative;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" Width="200px" Height="150px" ImageUrl="~/image/images.jpg"
                                                        OnClick="ImageButton1_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: middle; text-align: center">
                                                    <asp:Label ID="lblPush" runat="server" Text="STOP ALARM" Font-Names="Arial" Font-Bold="True"
                                                        ForeColor="#CC0000"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="float: left; position: relative;">
                                        <div style="float: right; position: relative;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <%--<asp:ImageButton ID="ImgStop" runat="server" ImageUrl="~/image/stop.jpg" Width="200px"
                                                            Height="150px" onclick="ImgStop_Click" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: center">
                                                        <asp:Label ID="lblStopInfo" runat="server" Text="STOP" Font-Bold="True" ForeColor="#CC0000"
                                                            Font-Names="Arial" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="float: left; position: relative;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <%--<asp:ImageButton ID="ImgStart" runat="server" ImageUrl="~/image/start.jpg" Width="200px"
                                                            Height="150px" onclick="ImgStart_Click" />--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; text-align: center">
                                                        <asp:Label ID="lblStartInfo" runat="server" Text="START" Font-Names="Arial" Font-Bold="True"
                                                            ForeColor="#CC0000" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script>        window.jQuery || document.write('<script src="Scripts/jquery-1.4.1.min.js"><\/script>')</script>
    <script src="Scripts/bootstrap.min.js"></script>
    <!-- Just to make our placeholder images work. Don't actually copy the next line! -->
    <script src="Scripts/holder.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="Scripts/ie10-viewport-bug-workaround.js"></script>
</body>
</html>
