<%@ page language="C#" autoeventwireup="true" inherits="Game_dungeon_log, App_Web_dungeon_log.aspx.3d0f6542" %><!DOCTYPE html>
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
<table class="table table-bordered">
	<colgroup>
		<col style="width:20%" />
		<col style="width:80%" />
	</colgroup>
	<tbody>        
		<tr>
			<th>server</th>
			<td>game015</td>
		</tr>
		<tr>
			<asp:Literal ID="ltrInfo" runat="server"></asp:Literal>
		</tr>
	</tbody>
</table>

<table class="table table-bordered centerTable">
	<colgroup>
		<col style="width:7%" />
		<col style="width:*" />
		<col style="width:30%" />
		<col style="width:20%" />
		<col style="width:10%" />
	</colgroup>
	<thead>
		<tr>
			<th>Alliance</th>
			<th>Guild</th>
			<th>Guild Leader</th>
			<th>Raid Time</th>
			<th>Record</th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptDugeonLog" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%# Eval("alliance_name")%></td>
                    <td class="leftAlign"><%#BindGuildName(Container.DataItem, "")%></td>
                    <td class="leftAlign"><%#BindGuildLeader(Container.DataItem, "") %></td>
                    <td><%# Eval("raid_time")%></td>
                    <td><%#BindRecord(Container.DataItem)%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>
</body>
</form>
</html>
