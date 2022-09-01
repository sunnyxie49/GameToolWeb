<%@ Reference Control="~/Game/skin/skin_quest_list.ascx" %>
<%@ Reference Control="~/Game/skin/skin_character_list.ascx" %>
<%@ page language="C#" autoeventwireup="true" inherits="Game_character_info_quest, App_Web_character_info_quest.aspx.3d0f6542" %><!DOCTYPE html>
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
<div class="well well-small alert">
	Server : 
	<asp:dropdownlist id="ddlServer" runat="server" AutoPostBack="True"></asp:dropdownlist>
</div>

<h4>Character List</h4>
<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
<p><label class="checkbox"><asp:CheckBox ID="chkSeeDeletedCharacter" runat="server" Text="Include Deleted Character" AutoPostBack="True"/></label></p>
<div class="well well-small alert">
	<ul class="characterInfo_btnArea">
		<li><asp:HyperLink id="hlInfo" runat="server" Text="Character"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlItem" runat="server" Text="Item"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlSkill" runat="server" Text="Skill"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlSummon" runat="server" Text="Summon"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlQuest" runat="server" Text="Quest"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlBank" runat="server" Text="Bank"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlShop" runat="server" Text="Shop"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlTitle" runat="server" Text="Title"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlArena" runat="server" Text="Arena"></asp:HyperLink></li>
	</ul>
</div>

<h4>Quest Insert</h4>
<table class="table table-bordered">
	<colgroup>
		<col style="width:20%" />
		<col style="width:80%" />
	</colgroup>
	<tbody>
		<tr>
			<th>Quest Insert </th>
			<td><asp:TextBox ID="txtQuestCode" runat="server"></asp:TextBox> <asp:Button ID="btnQuestInsert" runat="server" Text="QuestInsert" CssClass="btn" onclick="btnQuestInsert_Click" /></td>
		</tr>
	</tbody>
</table>

<h4>Quest List (Doing)</h4>
<asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>

<h4>Quest List (Done)</h4>
<asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
</form>
</body>
</html>
