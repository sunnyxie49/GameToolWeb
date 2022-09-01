<%@ page language="C#" autoeventwireup="true" inherits="Game_edit_skill, App_Web_edit_skill.aspx.3d0f6542" %><!DOCTYPE html>
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
<script src="/Common/js/jquery-1.8.3.js"></script>
<script src="/Common/js/bootstrap.min.js"></script>
</head>

<body>
<form id="form1" runat="server">
<h4>Owner Info</h4>
<table class="table table-bordered centerTable">
	<colgroup>
		<col style="width:30%" />
		<col style="width:*" />
	</colgroup>
	<thead>
		<tr>
			<th>Server</th>
			<th>Owner</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td><asp:Literal id="ltrServer" runat="server"></asp:Literal></td>
			<td><asp:Literal id="ltrOwner" runat="server"></asp:Literal></td>
		</tr>
	</tbody>
</table>

<h4>Skill Info</h4>
<table class="table table-bordered centerTable">
	<colgroup>
		<col style="width:9%" />
		<col style="width:5%" />
		<col style="width:*" />
		<col style="width:8%" />
		<col style="width:16%" />
	</colgroup>
	<thead>
		<tr>
			<th>sid</th>
			<th>Icon</th>
			<th>Name</th>
			<th>Lv</th>
			<th>Type</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td><asp:Literal id="ltrSkillSid" runat="server"></asp:Literal></td>
			<td><asp:Image id="imgSkillIcon" runat="server" ImageUrl="../img/icon/0.jpg"></asp:Image></td>
			<td><asp:Literal id="ltrSkillName" runat="server"></asp:Literal></td>
			<td><asp:Literal id="ltrSkillLevel" runat="server"></asp:Literal></td>
			<td><asp:Literal id="ltrSkillType" runat="server"></asp:Literal></td>
		</tr>
	</tbody>
</table>

<table class="table table-bordered">
	<thead>
		<tr>
			<th>*Info</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td><asp:Literal id="ltrSkillInfo" runat="server"></asp:Literal></td>
		</tr>
	</tbody>
</table>

<h4>Modify Skill Data</h4>
<div>
	Level : <asp:TextBox id="txtLevel" runat="server" CssClass="input-small" Enabled="False">0</asp:TextBox> &nbsp;<asp:Button id="btnChange" runat="server" Text="Modify" Enabled="False" CssClass="btn" onclick="btnChange_Click"></asp:Button>
</div>

<h4>Delete Skill Data</h4>
<p><asp:Button id="btnDel" runat="server" Text="Delete" CssClass="btn" Enabled="False" onclick="btnDel_Click"></asp:Button></p>

<p class="centerBtn"><input class="btn btn-primary" type="button" onclick="window.close()" value="Close" /></p>
</form>
</body>
</html>
