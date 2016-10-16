<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="ArduiboProjet.Settings" %>

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
    <title>SETTINGS</title>
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
                <a class="navbar-brand" href="Home.aspx">TEMPERATURE AND HUMIDITY CONTROL</a>
            </div>
            <div id="navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="Home.aspx">HOME</a></li>
                    <li><a href="DashBoard.aspx">DASHBOARD</a></li>
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
                    <li><a href="DashBoard.aspx">DASHBOARD</a></li>
                    <li class="active"><a href="Settings.aspx">SETTINGS<span class="sr-only">(current)</span></a></li>
                </ul>
            </div>
            <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
                <h1 class="page-header">
                    SETTINGS</h1>
                <div>
                    <div>
                        <form id="Form1" runat="server">
                        <asp:UpdatePanel ID="updatePanelConteneur" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:ToolkitScriptManager runat="server" ID="toolkitManagerPage">
                                </asp:ToolkitScriptManager>
                                <div class="container">
                                    <div class="row" style="margin-bottom: 20px;">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label5" runat="server" Text="TEMPERATURE" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="Large" ForeColor="#0099CC"></asp:Label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="Label6" runat="server" Text="HUMIDITY" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="Large" ForeColor="#0099CC"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <asp:Label ID="Label1" runat="server" Text="T MAX" class="col-xs-2 col-form-label"></asp:Label>
                                                <div class="col-xs-10">
                                                    <asp:TextBox ID="txtMaxTemp" runat="server" class="form-control" Width="250px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <asp:Label ID="Label2" runat="server" Text="T MIN" class="col-xs-2 col-form-label"></asp:Label>
                                                <div class="col-xs-10">
                                                    <asp:TextBox ID="txtMinTemp" runat="server" class="form-control" Width="250px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <asp:Label ID="Label3" runat="server" Text="H MAX" class="col-xs-2 col-form-label"></asp:Label>
                                                <div class="col-xs-10">
                                                    <asp:TextBox ID="txtMaxHumd" runat="server" class="form-control" Width="250px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <asp:Label ID="Label4" runat="server" Text="H MIN" class="col-xs-2 col-form-label"></asp:Label>
                                                <div class="col-xs-10">
                                                    <asp:TextBox ID="txtMinHumd" runat="server" class="form-control" Width="250px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 20px">
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <asp:Label ID="Label7" runat="server" Text="ALARM STOP TIME" class="col-xs-2 col-form-label"></asp:Label>
                                                <div class="col-xs-10">
                                                    <asp:DropDownList ID="DropDlListeTime" runat="server" class="form-control" Width="150px">
                                                        <asp:ListItem Selected="True">30</asp:ListItem>
                                                        <asp:ListItem>60</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <asp:Label ID="Label8" runat="server" Text="RECEIVER MAIL" class="col-xs-2 col-form-label"></asp:Label>
                                                <div class="col-xs-10">
                                                    <asp:TextBox ID="txtMail" runat="server" class="form-control" Width="250px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 20px">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnApply" runat="server" Text="APPLY" Width="150px" CssClass="btn btn-info"
                                                OnClick="btnApply_Click" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblErrorMessage" runat="server" Text="Label" Visible="False" Font-Bold="True"
                                                ForeColor="#CC0000"></asp:Label>
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
