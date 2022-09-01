<%@ page language="C#" autoeventwireup="true" inherits="_Default, App_Web_default.aspx.cdcab7d2" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>国服GM工具</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <meta charset="utf-8" />
    <link rel="stylesheet" href="/Common/css/bootstrap.css" />
    <!-- <link rel="stylesheet" href="css/bootstrap-responsive.min.css" /> -->
    <link rel="stylesheet" href="/Common/css/customize.css" />
    <style type="text/css">
	    body {padding-top:300px;}
	
    </style>
    <script type="text/javascript" src="/Common/js/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/Common/js/bootstrap.min.js"></script>
</head>
<body>
    <div class="navbar navbar-fixed-top">
	    <div class="navbar-inner">
		    <div class="container-fluid">
			    <span class="brand"><img src="/Common/img/logo_rappelz.png" alt="RAPPELZ" /></span>
		    </div>
	    </div>
    </div>
    <div class="container">
	    <form class="form-signin" id="form1" runat="server">
		    <h2 class="form-signin-heading"><img src="/Common/img/logo_rappelz_login.png" alt="RAPPELZ" /></h2>
		    <input type="text" id="txtAccount" runat="server" class="input-block-level" placeholder="ID" />
		    <input type="password" id="txtPassword" runat="server" class="input-block-level" placeholder="Password" />
		    <label class="checkbox">
			    <input type="checkbox" value="remember-me" id="chkRemember" runat="server" /> Remember me
		    </label>		    
            <asp:Button id="btnLogin" CssClass="btn btn-large btn-block btn-primary" runat="server" Text="Login" onclick="btnLogin_Click"></asp:Button>
	    </form>
    </div>
    <div class="footer">
	    <div class="innerBox">
		    <p class="footer_logo">Gala Lab</p>
		    <p class="copyright">Copyright © Gala Lab Corp. All Rights Reserved.</p>
	    </div>
    </div>    
</body>
</html>
