<%@ page language="C#" autoeventwireup="true" inherits="Game_QuestPopup, App_Web_questpopup.aspx.3d0f6542" %><!DOCTYPE html>
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
<table class="table table-bordered">
	<colgroup>
		<col style="width:20%" />
		<col style="width:80%" />
	</colgroup>
	<tbody>
		<tr>
			<th>Search</th>
			<td>
				<asp:dropdownlist id="ddlSearchType" runat="server">
					<asp:ListItem Value="code">Quest Code</asp:ListItem>
					<asp:ListItem Value="name" Selected="True">Quest Name</asp:ListItem>
				</asp:dropdownlist>&nbsp;
				<asp:TextBox id="txtSearch" runat="server"></asp:TextBox>&nbsp;
				<asp:Button id="Button1" runat="server" Text="Search" CssClass="btn btn-primary" onclick="Button1_Click"></asp:Button>
			</td>
		</tr>
	</tbody>
</table>
<asp:Repeater ID="rptQuestPopup" runat="server">
    <ItemTemplate>
        <h4>Quest Code : <%# DataBinder.Eval(Container.DataItem, "id")%></h4>

        <table class="table table-bordered">
	        <colgroup>
		        <col style="width:20%" />
		        <col style="width:80%" />
	        </colgroup>
	        <tbody>
		        <tr>
			        <th>Quest Name</th>
			        <td><%# DataBinder.Eval(Container.DataItem, "value") %> </td>
		        </tr>
		        <tr>
			        <td colspan="2"> <%# BindSummary(Container.DataItem) %>  </td>
		        </tr>
	        </tbody>
        </table>

        <table class="table table-bordered centerTable">
	        <colgroup>
		        <col style="width:33%" />
		        <col style="width:33%" />
		        <col style="width:34%" />
	        </colgroup>
	        <thead>
		        <tr>
			        <th>Exp</th>
			        <th>JP</th>
			        <th>Gold</th>
		        </tr>
	        </thead>
	        <tbody>
		        <tr>
			        <td><%# BindExp(Container.DataItem) %></td>
			        <td><%# BindJP(Container.DataItem) %></td>
			        <td><%# BindGold(Container.DataItem) %></td>
		        </tr>
	        </tbody>
        </table>

        <table class="table table-bordered">
	        <colgroup>
		        <col style="width:20%" />
		        <col style="width:80%" />
	        </colgroup>
	        <tbody>
		        <tr>
			        <th>Basic Reward</th>
			        <td><%# BindReward(Container.DataItem) %> </td>
		        </tr>
		        <tr>
			        <th rowspan="3">Choice Reward</th>
			        <td><%# BindReward(Container.DataItem, 1) %> </td>
		        </tr>
		        <tr>
			        <td><%# BindReward(Container.DataItem, 2) %> </td>
		        </tr>
		        <tr>
			        <td><%# BindReward(Container.DataItem, 3) %> </td>
		        </tr>
	        </tbody>
        </table>
    </ItemTemplate>
</asp:Repeater>
</form>
</body>
</html>
