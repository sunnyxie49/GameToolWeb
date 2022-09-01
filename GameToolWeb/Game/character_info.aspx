<%@ Reference Control="~/Game/skin/skin_summon_list.ascx" %>
<%@ Reference Control="~/Game/skin/skin_item_list.ascx" %>
<%@ Reference Control="~/Game/skin/skin_character_info.ascx" %>
<%@ Reference Control="~/Game/skin/skin_character_list.ascx" %>
<%@ page language="C#" autoeventwireup="true" inherits="Game_character_info, App_Web_character_info.aspx.3d0f6542" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="X-UA-Compatible" content="IE=9" />
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

<h4>Character Info</h4>
<asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>

<asp:Panel id="pnlCharacterModify" runat="server" Width="100%">
<h4>Character Data Modify</h4>
<table class="table table-bordered centerTable chaData">
	<tr>
		<th><label for="<%=txtName.ClientID %>">Name</label></th>
		<th><label for="<%=txtExp.ClientID %>">Exp</label></th>
		<th><label for="<%=txtJP.ClientID %>">JP</label></th>
		<th><label for="<%=txtGold.ClientID %>">Gold</label></th>
		<th><label for="<%=txtLac.ClientID %>">Lac</label></th>
		<th><label for="<%=txtStamina.ClientID %>">Stamina</label></th>
		<th><label for="<%=txtImmoralPoint.ClientID %>">IP</label></th>
		<th><label for="<%=txtCha.ClientID %>">cha</label></th>
		<th><label for="<%=txtPkc.ClientID %>">pkc</label></th>
		<th><label for="<%=txtDkc.ClientID %>">dkc</label></th>
		<th><label for="<%=txtChatBlockTime.ClientID %>">Chat Block</label></th>
		<th><label for="<%=txtHuntaHolicPoint.ClientID %>">Bearlord Point</label></th>
		<th><label for="<%=txtEtherealStoneDurability.ClientID %>">Ethereal Stone Durability</label></th>
		<th><label for="<%=txtAP.ClientID %>">ap</label></th>
	</tr>
	<tr>		
        <td><asp:TextBox id="txtName" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtExp" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtJP" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtGold" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtLac" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtStamina" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtImmoralPoint" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtCha" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtPkc" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtDkc" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtChatBlockTime" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox id="txtHuntaHolicPoint" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox ID="txtEtherealStoneDurability" runat="server" CssClass="input-small"></asp:TextBox></td>
        <td><asp:TextBox ID="txtAP" runat="server" CssClass="input-small"></asp:TextBox></td>
	</tr>
	<tr>
        <td>
            <%--<button id="btnChangeName" runat="server" class="btn btn-primary" onserverclick="btnChangeName_Click" type="button">Modify</button>--%>
            <asp:Button id="btnChangeName" runat="server" Text="Modify" onclick="btnChangeName_Click" CssClass="btn btn-primary w100"></asp:Button>
        </td> 
        <td><asp:Button id="btnChangeExp" runat="server" Text="Modify" OnClick="btnChangeExp_Click" CssClass="btn btn-primary w100"></asp:Button></td> 
        <td><asp:Button id="btnChangeJP" runat="server" Text="Modify" OnClick="btnChangeJP_Click" CssClass="btn btn-primary w100"></asp:Button></td> 
        <td><asp:Button id="btnChangeGold" runat="server" Text="Modify" OnClick="btnChangeGold_Click" CssClass="btn btn-primary w100"></asp:Button></td> 
        <td><asp:Button id="btnChangeLac" runat="server" Text="Modify" OnClick="btnChangeLac_Click" CssClass="btn btn-primary w100"></asp:Button></td> 
        <td><asp:Button id="btnChangeStamina" runat="server" Text="Modify" OnClick="btnChangeStamina_Click" CssClass="btn btn-primary w100"></asp:Button></td> 
        <td><asp:Button id="btnImmoralPoint" runat="server" Text="Modify" OnClick="btnImmoralPoint_Click" CssClass="btn btn-primary w100"></asp:Button></td>
        <td><asp:Button id="btnCha" runat="server" Text="Modify" OnClick="btnCha_Click" CssClass="btn btn-primary w100"></asp:Button></td>
        <td><asp:Button id="btnPkc" runat="server" Text="Modify" OnClick="btnPkc_Click" CssClass="btn btn-primary w100"></asp:Button></td>
        <td><asp:Button id="btnDkc" runat="server" Text="Modify" OnClick="btnDkc_Click" CssClass="btn btn-primary w100"></asp:Button></td>
        <td><asp:Button id="btnChangeChatBlockTime" runat="server" Text="Modify" OnClick="btnChangeChatBlockTime_Click" CssClass="btn btn-primary w100"></asp:Button></td> 
        <td><asp:Button id="btnChangeHuntaHolicPoint" runat="server" Text="Modify" OnClick="btnChangeHuntaHolicPoint_Click" CssClass="btn btn-primary w100"></asp:Button></td>
		<td><asp:Button ID="btnEtherealStoneDurability" runat="server" Text="Modify" onclick="btnEtherealStoneDurability_Click" CssClass="btn btn-primary w100" /></td>
		<td><asp:Button ID="btnAP" runat="server" Text="Modify" onclick="btnAP_Click" CssClass="btn btn-primary w100" /></td>
	</tr>
</table>

<h4>Character Data Modify</h4>
<table class="table table-bordered">
	<colgroup>
		<col style="width:20%" />
		<col style="width:80%" />
	</colgroup>
	<tbody>
		<tr>
			<th>Set SecurityNumber</th>
			<td><input class="btn" type="button" value="Initialize" /></td>
		</tr>
		<tr>
			<th>Select Permission</th>
			<td>
                <asp:DropDownList ID="ddlGmPermission" runat="server">
					<asp:ListItem Value="0">Player</asp:ListItem>					
					<asp:ListItem Value="100">[S](All)</asp:ListItem>
				</asp:DropDownList>				
			</td>
		</tr>
		<tr>
			<th>Give GM Permission ( Selected Character )</th>
			<td>
                <asp:Button id="btnGiveGM" runat="server" CssClass="btn" Text="Give GM Permission" onclick="btnGiveGM_Click"></asp:Button>&nbsp;
		        <asp:Button id="btnDelGM" runat="server" CssClass="btn" Text="Remove GM Permission" onclick="btnDelGM_Click"></asp:Button>				
			</td>
		</tr>
		<tr>
			<th>Give GM Permission ( All Character )</th>
			<td>
				<asp:Button id="btnGiveAllGM" runat="server" CssClass="btn" Text="Give GM Permission" onclick="btnGiveAllGM_Click"></asp:Button>&nbsp;
		        <asp:Button id="btnDelAllGM" runat="server" CssClass="btn" Text="Remove GM Permission"  onclick="btnDelAllGM_Click"></asp:Button>
			</td>
		</tr>
		<tr>
			<th>Warp Character to</th>
			<td>
				<strong>x</strong> <asp:TextBox id="txtPositionX" runat="server" BorderStyle="Groove"></asp:TextBox>, &nbsp;<strong>y</strong> <asp:TextBox id="txtPositionY" runat="server" BorderStyle="Groove"></asp:TextBox> &nbsp;
                <asp:Button id="btnWarp" runat="server" Text="Warp" CssClass="btn" onclick="btnWarp_Click"></asp:Button>&nbsp;
		        <asp:Button id="btnWarp2" runat="server" Text="Warp" CssClass="btn" onclick="btnWarp2_Click"></asp:Button>                
			</td>
		</tr>
		<tr>
			<th>Kick Character from Game</th>
			<td><asp:Button id="btnKick" runat="server" Text="Kick" CssClass="btn" onclick="btnKick_Click"></asp:Button></td>
		</tr>
		<tr>
			<th>Auto Set</th>
			<td>
				<asp:Button ID="btnAutoSetGame" runat="server" CssClass="btn" OnClick="btnAutoSetGame_Click" Text="Set Auto" />&nbsp;
				<asp:Button ID="btnAutoDelGame" runat="server" CssClass="btn" OnClick="btnAutoDelGame_Click" Text="Set Nomal" />
				<br />(Must be select Logined character if character login )
			</td>
		</tr>
	</tbody>
</table>

<h4>Deleted Character Restore</h4>
<p><asp:TextBox id="txtRestoredName" runat="server" BorderStyle="Groove" Width="246px"></asp:TextBox> <asp:Button id="btnDeletedCharactedRestore" runat="server" Text="Restore" CssClass="btn" OnClick="btnDeletedCharactedRestore_Click"></asp:Button></p>
<input id="hdnAvatarNo" type="hidden" name="Hidden1" runat="server"><input id="hdnName" type="hidden" name="Hidden1" runat="server">
</asp:Panel>

<h4>Item List</h4>
<asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
<!-- 캐릭터 아이템리스트(아이템 페이지에서 가져다 쓰는 듯함) -->
<h4>Summon List</h4>
<asp:PlaceHolder ID="PlaceHolder4" runat="server"></asp:PlaceHolder>
<!-- 캐릭터 크리쳐리스트 -->
</form>
</body>
</html>
