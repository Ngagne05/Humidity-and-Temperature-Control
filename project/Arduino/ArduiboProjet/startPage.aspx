<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="ArduiboProjet.startPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../favicon.ico">
    <title>TEMPERATURE AND HUMIDITY CONTROL</title>
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
    <div>
        <div class="container" style="margin-top: 5%;">
            <div class="col-md-4 col-md-offset-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Login</div>
                    <div class="panel-body">
                        <!-- Login Form -->
                        <form id="form1" runat="server">
                        <!-- Username Field -->
                        <div class="row">
                            <div class="form-group col-xs-12">
                                <label for="username">
                                    <span class="text-danger" style="margin-right: 5px;">*</span>Username:</label>
                                <div class="input-group">
                                    <asp:TextBox ID="username" runat="server" placeholder="Username" class="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <label class="btn btn-primary">
                                            <span class="glyphicon glyphicon-user" aria-hidden="true"></span>
                                        </label>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <!-- Content Field -->
                        <div class="row">
                            <div class="form-group col-xs-12">
                                <label for="password">
                                    <span class="text-danger" style="margin-right: 5px;">*</span>Password:</label>
                                <div class="input-group">
                                    <asp:TextBox ID="password" runat="server" TextMode="Password" class="form-control"
                                        placeholder="Password"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <label class="btn btn-primary">
                                            <span class="glyphicon glyphicon-lock" aria-hidden="true"></span>
                                        </label>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <!-- Login Button -->
                        <div class="row">
                            <div class="form-group col-xs-4">
                                <asp:Button runat="server" Text="Submit" class="btn btn-primary" ID="Submit" 
                                    onclick="Submit_Click" />
                            </div>
                        </div>
                        <div class="row">
                            <asp:Label ID="lblErreurMessage" runat="server" Text="" Visible ="false" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
                        </div>
                        </form>
                        <!-- End of Login Form -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
