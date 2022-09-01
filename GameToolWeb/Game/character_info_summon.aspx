<%@ Reference Control="~/Game/skin/skin_summon_list.ascx" %>
<%@ Reference Control="~/Game/skin/skin_item_list.ascx" %>
<%@ Reference Control="~/Game/skin/skin_skill_list.ascx" %>
<%@ Reference Control="~/Game/skin/skin_character_list.ascx" %>
<%@ page language="C#" autoeventwireup="true" inherits="Game_character_info_summon, App_Web_character_info_summon.aspx.3d0f6542" %><!DOCTYPE html>
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
<div class="well well-small alert">
	Server : 
	<asp:dropdownlist id="ddlServer" runat="server" AutoPostBack="True"></asp:dropdownlist>
</div>

<h4>Character List</h4>
<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
<p><label class="checkbox"><asp:CheckBox ID="chkSeeDeletedCharacter" runat="server" Text="Include Deleted Character" AutoPostBack="True"/> </label></p>
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

<h4> Summon Formation</h4>
<ul>
	<li><i class="icon-chevron-right"></i>&nbsp;<asp:Literal ID="ltrFormation" runat="server"></asp:Literal></li>	
</ul>

<h4>Summon List</h4>
<asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>

<asp:Panel id="Panel1" runat="server">
    <table class="table table-bordered">
	    <colgroup>
		    <col style="width:20%" />
		    <col style="width:80%" />
	    </colgroup>
	    <tbody>
		    <tr>
			    <th>Selected Creature Name</th>
			    <td><asp:textbox id="txtSummonName" tabIndex="1" runat="server" ></asp:textbox> <asp:Button ID="btnCreatureNameModify" runat="server" Text="Modify" CssClass="btn" OnClick="btnCreatureNameModify_Click" /></td>
		    </tr>
		    <tr>
			    <th>Selected Creature</th>
			    <td>
				    <label for="<%=txtSumonExp.ClientID %>">EXP :</label> <asp:textbox id="txtSumonExp" tabIndex="1" runat="server" ></asp:textbox>, 
				    <label for="<%=txtSumonJp.ClientID %>">JP :</label> <asp:textbox id="txtSumonJp" tabIndex="1" runat="server" ></asp:textbox>, 
				    <label for="<%=txtSumonSp.ClientID %>">SP :</label> <asp:textbox id="txtSumonSp" tabIndex="1" runat="server" ></asp:textbox> 
				    <asp:Button id="Button2" runat="server" Text="Modify" CssClass="btn" onclick="Button2_Click"></asp:Button>
			    </td>
		    </tr>
	    </tbody>
    </table>
</asp:Panel>

<h4>Skill List</h4>
<asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>

<h4>Item List</h4>
<asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
</form>
</body>
</html>
