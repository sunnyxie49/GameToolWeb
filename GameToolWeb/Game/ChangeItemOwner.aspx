<%@ page language="C#" autoeventwireup="true" inherits="Game_ChangeItemOwner, App_Web_changeitemowner.aspx.3d0f6542" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=9">
<meta charset="utf-8" />
<link rel="stylesheet" href="/Common/css/bootstrap.css" />
<!-- <link rel="stylesheet" href="css/bootstrap-responsive.min.css" /> -->
<link rel="stylesheet" href="/Common/css/customize.css" />
<style type="text/css">
	body{padding:5px;}
</style>
<script type="text/javascript" src="/Common/js/jquery-1.8.3.js"></script>
<script type="text/javascript" src="/Common/js/bootstrap.min.js"></script>
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

<h4>Item Info</h4>
<table class="table table-bordered centerTable">
	<colgroup>
		<col style="width:9%" />
		<col style="width:*" />
		<col style="width:8%" />
		<col style="width:16%" />
	</colgroup>
	<thead>
		<tr>
			<th>sid</th>
			<th>Name</th>
			<th>Count</th>
			<th>Class</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td><asp:Literal id="ltrItemSid" runat="server"></asp:Literal></td>
			<td><asp:Literal id="ltrItemName" runat="server"></asp:Literal> / Remain Time : <asp:Literal ID="ltrRemainTime" runat="server"></asp:Literal></td>
			<td><asp:Literal id="ltrItemCnt" runat="server"></asp:Literal></td>
			<td><asp:Literal id="ltrItemType" runat="server"></asp:Literal></td>
		</tr>
	</tbody>
</table>

<table class="table table-bordered centerTable">
	<thead>
		<tr>
			<th>* Modify Item Owner</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<%--<td>Character SID : 852805 -> Character SID <input type="text" class="" /> &nbsp;<input class="btn" type="button" value="Change" /></td>--%>
            <td>
                <asp:Panel ID="pnAccount" runat="server" Visible="false" Width="100%">
					Account ID&nbsp; : &nbsp;<asp:Literal ID="ltrOldAccount" runat="server"></asp:Literal>
					&nbsp; &nbsp; -> Character SID : <asp:TextBox ID="txtNewAccount" runat="server"></asp:TextBox>&nbsp;
					<asp:Button id="btnChangeAccount" CssClass="btn" runat="server" Text="Change" Enabled="False" onclick="btnChangeAccount_Click"></asp:Button>
				</asp:Panel>
            </td>
		</tr>
		<tr>
            <td>
                <asp:Panel ID="pnCharacter" runat="server" Visible="false"  Width="100%">
					Character SID&nbsp; : &nbsp;<asp:Literal ID="ltrOldCharacter" runat="server"></asp:Literal>
					&nbsp; &nbsp; -> Character SID : <asp:TextBox ID="txtNewAvatarname" runat="server"></asp:TextBox>
					<asp:Button id="btnChangeCharacter" CssClass="btn" runat="server" Text="Change" Enabled="False" OnClick="btnChangeCharacter_Click"></asp:Button>
				</asp:Panel>
            </td>
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
			<td>
                <asp:Literal id="ltrItemInfo" runat="server"></asp:Literal>
                <asp:Panel ID="pnlSummon" runat="server" Visible="False" ></asp:Panel>				
			</td>
		</tr>
	</tbody>
</table>

<table class="table table-bordered">
	<thead>
		<tr>
			<th>*Admin Cheat Log</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td><asp:Literal id="ltrItemLog" runat="server"></asp:Literal></td>
		</tr>
	</tbody>
</table>

<p class="centerBtn"><input class="btn btn-primary" type="button" value="Close" onclick="window.close()" /></p>
</form>
</body>
</html>
