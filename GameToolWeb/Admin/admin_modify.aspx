<%@ page language="C#" autoeventwireup="true" inherits="Admin_admin_modify, App_Web_admin_modify.aspx.fdf7a39c" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
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
<h4>Modify Admin Info</h4>
<table class="table table-bordered">
	<colgroup>
		<col style="width:30%" />
		<col style="width:*" />
	</colgroup>
	<tbody>
		<tr>
			<th>sid</th>
			<td><asp:Literal ID="ltrSid" runat="server"></asp:Literal></td>
		</tr>
		<tr>
			<th>Account</th>
			<td><asp:Literal ID="ltrAccount" runat="server"></asp:Literal></td>
		</tr>
		<tr>
			<th>Name</th>
			<td><asp:TextBox ID="txtName" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="***" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator></td>
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
<p class="centerBtn"><asp:Button ID="Button1" runat="server" Text="modify info" CssClass="btn" OnClick="Button1_Click" /></p>

<h4>Change Password</h4>
<table class="table table-bordered">
	<colgroup>
		<col style="width:30%" />
		<col style="width:*" />
	</colgroup>
	<tbody>
		<tr>
			<th>Password</th>
			<td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="***" ControlToValidate="txtPassword" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator></td>
		</tr>
	</tbody>
</table>
<p class="centerBtn"><asp:Button ID="Button2" runat="server" Text="Change Password" CssClass="btn" OnClick="Button2_Click" ValidationGroup="2" /></p>

<p class="centerBtn"><input class="btn btn-primary" type="button" value="Close" onclick="window.close()" /></p>
</form>
</body>
</html>
