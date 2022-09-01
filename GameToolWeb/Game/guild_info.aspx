<%@ page language="C#" autoeventwireup="true" inherits="Game_guild_info, App_Web_guild_info.aspx.3d0f6542" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=9" />
<meta charset="utf-8" />
<link rel="stylesheet" href="/Common/css/bootstrap.css" />
<!-- <link rel="stylesheet" href="css/bootstrap-responsive.min.css" /> -->
<link rel="stylesheet" href="/Common/css/customize.css" />
<style>
	body{padding:5px;}
</style>
<script type="text/javascript" src="/Common/js/jquery-1.8.3.js"></script>
<script type="text/javascript" src="/Common/js/bootstrap.min.js"></script>
</head>

<body>
<form id="form1" runat="server">
<asp:Literal ID="ltrAllianceName" runat="server"></asp:Literal>
<table class="table table-bordered centerTable">
	<colgroup>
		<col style="width:5%" />
		<col style="width:20%" />
		<col style="width:5%" />
		<col style="width:*" />
		<col style="width:14%" />
		<col style="width:10%" />
		<col style="width:10%" />
		<col style="width:10%" />
	</colgroup>
	<thead>
		<tr>
			<th>sid</th>
			<th>Name</th>
			<th>Member</th>
			<th>Leader</th>
			<th>Dungeon</th>
			<th>Gold</th>
			<th>Chaos</th>
			<th>Donation Point</th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptGuildInfo" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("sid") %></td>
                    <td class="leftAlign"><%# BindGuildName(Container.DataItem)%></td>
                    <td><%# Eval("cnt") %></td>
                    <td class="leftAlign"><%# BindGuildLeader(Container.DataItem) %></td>
                    <td><%# BindDungeon(Container.DataItem) %></td>
                    <td><%# Eval("gold")%></td>
                    <td><%# Eval("chaos")%></td>
                    <td><%# Eval("donation_point")%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>
<asp:Literal ID="ltrAllianceBlockTime" runat="server"></asp:Literal>

<h4>Modify Guild Info</h4>
<table class="table table-bordered">
	<colgroup>
		<col style="width:30%" />
		<col style="width:70%" />
	</colgroup>
	<tbody>
		<tr>
			<th>Delete Guild Icon</th>
			<td><asp:Button ID="btnDeleteGuildIcon" runat="server" Text="Delete" OnClick="btnDeleteGuildIcon_Click" CssClass="btn" /></td>
		</tr>
		<tr>
			<th>Change Guild Icon</th>
			<td>FileName <asp:TextBox ID="txtIcon" runat="server" CssClass="input-small"></asp:TextBox>,&nbsp;FileSize <asp:TextBox ID="txtIconSize" runat="server" CssClass="input-small"></asp:TextBox> <asp:Button ID="btnChangeGuildIcon" runat="server" Text="Change" CssClass="btn" OnClick="btnChangeGuildIcon_Click" /></td>
		</tr>
		<tr>
			<th>Change Guild Name</th>
			<td>(<asp:Literal ID="ltrGuildNameBefore" runat="server"></asp:Literal>) <asp:TextBox ID="txtGuildName" runat="server" CssClass="input-small"></asp:TextBox>&nbsp;<asp:Button ID="btnChangeGuildName" runat="server" Text="Change" OnClick="btnChangeGuildName_Click" CssClass="btn" /></td>
		</tr>
		<tr>
			<th>Change Guild Leader</th>
			<td>(<asp:Literal ID="ltrGuildLeaderBefore" runat="server"></asp:Literal>)- Character Sid <asp:TextBox ID="txtGuildLeaderSid" runat="server" CssClass="input-small"></asp:TextBox> &nbsp;<asp:Button ID="btnChangeGuildLeader" runat="server" Text="Change" OnClick="btnChangeGuildLeader_Click" CssClass="btn" /></td>
		</tr>
	</tbody>
</table>

<h4><asp:Literal ID="ltrGuildName" runat="server"></asp:Literal>'s Guild Member</h4>
<table class="table table-bordered centerTable">
	<colgroup>
		<col style="width:20%" />
		<col style="width:*%" />
		<col style="width:5%" />
		<col style="width:5%" />
		<col style="width:5%" />
		<col style="width:12%" />
		<col style="width:12%" />
		<col style="width:5%" />
	</colgroup>
	<thead>
		<tr>
			<th>Account</th>
			<th>Character Name</th>
			<th>Lv</th>
			<th>JLv</th>
			<th>Lac</th>
			<th>Gold</th>
			<th>Bank Gold</th>
			<th>Login</th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptGuildmember" runat="server">
            <ItemTemplate>
                <tr>
                    <td class="leftAlign"><%# ((string)Eval("account")).ToLower() %>(<%# Eval("account_id") %>)</td>
                    <td class="leftAlign"><%# BindName(Container.DataItem) %>(<%# Eval("sid") %>)<%# BindPermission(Container.DataItem) %><%# BindAuto(Container.DataItem) %></td>
                    <td><span title='Exp : <%# ((Int64)Eval("exp")).ToString("#,##0") %>'><%# Eval("lv") %></span></td>
                    <td><span title='Total JP : <%# ((Int64)Eval("total_jp")).ToString("#,##0") %>'><%# Eval("jlv") %></span></td>
                    <td><%# Eval("chaos")%></td>
                    <td><%# ((Int64)Eval("gold")).ToString("#,##0")%></td>
                    <td><%# ((Int64)Eval("bank_gold")).ToString("#,##0") %></td>
                    <td><%# BindLogin(Container.DataItem) %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
		<%--<tr>
			<td>gmldud9999 (300221)</td>
			<td><img src="img/temp/common_mark_job_0005.jpg" alt="" /> <a href="#">BLESSofGODDESS (979978)</a></td>
			<td>67</td>
			<td>30</td>
			<td>500</td>
			<td>11,762,132</td>
			<td>890,000,000 </td>
			<td>X</td>
		</tr>--%>
	</tbody>
</table>
</form>
</body>
</html>