<%@ page language="C#" autoeventwireup="true" inherits="Game_character_info_item, App_Web_character_info_item.aspx.3d0f6542" %>
<%@ Reference Control="~/Game/skin/skin_item_list.ascx" %>
<%@ Reference Control="~/Game/skin/skin_character_list.ascx" %><!DOCTYPE html>
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
<script type="text/javascript">
    function OpenItemPopup(where) {
        server = document.form1.ddlServer.value; ;
        item_code = document.form1.txtItemCode.value;
        var ItemPopup = window.open("ItemPopup.aspx?server=" + server + "&item_code=" + item_code + "&where=" + where, "ItemPopup", "left=50,top=50,width=680,height=380,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no");
        ItemPopup.focus();
    }

    function OpenPopup() {
        var server = document.form1.ddlServer.value; ;
        var account_id = getParameter("account_id");
        var character_id = getParameter("character_id");

        var ItemPopup = window.open("item_insert_useSid.aspx?server=" + server + "&account_id=" + account_id + "&character_id=" + character_id, "ItemPopup2", "left=50,top=50,width=680,height=380,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no");
        ItemPopup.focus();
    }

    function getParameter(name) {
        var rtnval = '';
        var nowAddress = unescape(location.href);
        var parameters = (nowAddress.slice(nowAddress.indexOf('?') + 1, nowAddress.length)).split('&');

        for (var i = 0; i < parameters.length; i++) {
            var varName = parameters[i].split('=')[0];
            if (varName.toUpperCase() == name.toUpperCase()) {
                rtnval = parameters[i].split('=')[1];
                break;
            }
        }
        return rtnval;


    }

    function SetItemCode(where, code) {
        document.form1.txtItemCode.value = code;
    }
</script>
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
		<li><asp:HyperLink id="hlGiveItem" runat="server" Text="GiveItem"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlSkill" runat="server" Text="Skill"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlSummon" runat="server" Text="Summon"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlQuest" runat="server" Text="Quest"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlBank" runat="server" Text="Bank"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlShop" runat="server" Text="Shop"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlTitle" runat="server" Text="Title"></asp:HyperLink></li>
		<li><asp:HyperLink id="hlArena" runat="server" Text="Arena"></asp:HyperLink></li>
	</ul>
</div>
<asp:Panel ID="pnlItemInsert" runat="server" Width="100%">
<h4>Item Insert</h4>
<p>Send Item ( Item Code : <asp:textbox id="txtItemCode" tabIndex="1" runat="server" CssClass="input-small"></asp:textbox>&nbsp;<input class="btn" type="button" value="Search" onclick="OpenItemPopup('give')" /> ) Enhance : <asp:textbox id="txtItemEnhance" tabIndex="1" runat="server" CssClass="input-small">0</asp:textbox>&nbsp;Level : <asp:textbox id="txtItemLevel" tabIndex="1" runat="server" CssClass="input-small">1</asp:textbox>&nbsp;Count : <asp:textbox id="txtItemCnt" tabIndex="1" runat="server" CssClass="input-small"></asp:textbox>&nbsp;<asp:button id="btnGiveItem" runat="server" Text="Give Item" onclick="btnGiveItem_Click" CssClass="btn"></asp:button>&nbsp;<input class="btn" type="button" onclick="OpenPopup()" value="Give Item(SID)" /></p>
</asp:Panel>

<h4>Item List</h4>
<asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
</form>
</body>
</html>
