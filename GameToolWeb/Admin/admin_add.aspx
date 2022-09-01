<%@ page language="C#" autoeventwireup="true" inherits="Admin_admin_add, App_Web_admin_add.aspx.fdf7a39c" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <meta charset="utf-8" />
    <link rel="stylesheet" href="/Common/css/bootstrap.css" />
    <!-- <link rel="stylesheet" href="css/bootstrap-responsive.min.css" /> -->
    <link rel="stylesheet" href="/Common/css/customize.css" />
    <style>
	    body{padding:5px;}
    </style>
    <script src="http://code.jquery.com/jquery-1.7.1.js"></script>
    <script src="/Common/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <h4>Admin Add</h4>
    <table class="table table-bordered">
	    <colgroup>
		    <col style="width:30%" />
		    <col style="width:*" />
	    </colgroup>
	    <tbody>
		    <tr>
			    <th>Account</th>
			    <td>
                    <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="***" ControlToValidate="txtAccount"></asp:RequiredFieldValidator>
                </td>
		    </tr>
		    <tr>
			    <th>Password</th>
			    <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="***" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
		    </tr>
		    <tr>
			    <th>Name</th>
			    <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="***" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
		    </tr>
		    <tr>
			    <th>Team</th>
			    <td>
                    <asp:DropDownList ID="ddlTeam" runat="server">
                        <asp:ListItem Value="0" Text="none"></asp:ListItem>
                    </asp:DropDownList>				    
			    </td>
		    </tr>
		    <tr>
			    <th>Grade</th>
			    <td>
                    <asp:DropDownList ID="ddlGrade" runat="server">
                        <asp:ListItem Value="0" Text="none"></asp:ListItem>
                    </asp:DropDownList>				    
			    </td>
		    </tr>
	    </tbody>
    </table>
    <p class="centerBtn"><asp:Button ID="Button1" runat="server" Text="add" class="btn" OnClick="Button1_Click" /> <input class="btn btn-primary" type="button" value="Close" onclick="window.close()" /></p>
    </form>
</body>
</html>