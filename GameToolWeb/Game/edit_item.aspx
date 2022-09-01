<%@ page language="C#" autoeventwireup="true" inherits="Game_edit_item, App_Web_edit_item.aspx.3d0f6542" %><!DOCTYPE html><html xmlns="http://www.w3.org/1999/xhtml">
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
		<col style="width:70%" />
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
			<td><asp:Literal id="ltrItemName" runat="server"></asp:Literal></td>
			<td><asp:Literal id="ltrItemCnt" runat="server"></asp:Literal></td>
			<td><asp:Literal id="ltrItemType" runat="server"></asp:Literal></td>
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
				<p>Create Time :<asp:Literal ID="ltrCreateTime" runat="server"></asp:Literal></p>
                <asp:Literal id="ltrItemInfo" runat="server"></asp:Literal>
                <asp:Panel ID="pnlSummon" runat="server" Visible="False" >
					<asp:RadioButtonList ID="rblSummonList" runat="server"></asp:RadioButtonList>
					<asp:Button ID="btnChangeSummonSid" runat="server" Text="ChangeSummonSid" OnClick="btnChangeSummonSid_Click" />
                </asp:Panel>				
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


<%--// 아이템 관련 --%>
<%-- 펫 관련 --%>
<div id="divPetInfo" runat="server" visible="false">
	<h4>Pet Info</h4>
	<asp:Literal ID="ltrPetName" runat="server"></asp:Literal> -> <asp:TextBox ID="txtPetName" runat="server" CssClass="input-small"></asp:TextBox>
    <%--&nbsp;(<asp:Literal ID="ltrPetNameCanChangeText" runat="server"></asp:Literal>)--%>
    <asp:Button ID="btnPetNameChange" runat="server" Text="Change" Enabled="false" OnClick="btnPetNameChange_Click" />
</div>
<%--// 펫 관련 --%>
<br />
<%-- 아이템 각성 관련 --%>
<div id="divAwakenInfo" runat="server" visible="false">
    <table class="table table-bordered">
	    <thead>
		    <tr>
			    <th>Item Awaken Info</th>
		    </tr>
	    </thead>
	    <tbody>
		    <tr>
			    <td class="centerAlign">
				    <ul>
                        <asp:Repeater ID="rptAwakenInfo" runat="server">
		                    <ItemTemplate>
			                    <li><asp:Literal ID="ltrType" runat="server"></asp:Literal>
			                    : <asp:Literal ID="ltrValue" runat="server"></asp:Literal> (<asp:Literal ID="ltrData" runat="server"></asp:Literal>)</li>
		                    </ItemTemplate>
	                    </asp:Repeater>					    
				    </ul>
			    </td>
		    </tr>
	    </tbody>
    </table>	
</div>
<%--// 아이템 각성 관련 --%>
<br />
<%-- 크리처 강화 관련--%>
<div id="divSummon" runat="server" visible="false">
	<h4>Modify Summon Info</h4>
    <div class="itemModify">
	    Enhance : <asp:TextBox ID="txtEnhance2" runat="server" CssClass="input-small" Width="30px"></asp:TextBox> &nbsp;
	    <asp:CheckBox ID="chkLvinItialization" runat="server" Text="Level Itialization" />&nbsp;
	    <span style="color:Red; font-weight:bold">Max Summon Enhance : 5</span> &nbsp;
	    <asp:Button ID="btnChange2" runat="server" Text="Change"  Enabled="False" onclick="btnChange2_Click" OnClientClick="javascript:return enhanceChk();" />
    </div>
</div>
<%--// 크리처 강화 관련  --%>

<%-- 아이템 관련 --%>
<div id="divItem" runat="server">
    <h4>Modify Item Info</h4>
    <div class="itemModify">
	    Enhance : <asp:TextBox id="txtEnhance" runat="server" CssClass="input-small" Enabled="False">0</asp:TextBox>, &nbsp;Level : <asp:TextBox id="txtLevel" runat="server" CssClass="input-small" Enabled="False">0</asp:TextBox>, &nbsp;Count : <asp:TextBox id="txtCnt" runat="server" CssClass="input-small" Enabled="False">0</asp:TextBox>
        <asp:Literal ID="ltrBeltSlot" runat="server" Visible="false">Belt Slot : 1 + </asp:Literal><asp:TextBox ID="txtBeltSlot" runat="server" CssClass="input-small" Enabled="False" Visible="false"  >0</asp:TextBox>
        <label class="checkbox"><asp:CheckBox id="chkTaming" runat="server" Text="Taiming" Enabled="False"></asp:CheckBox></label> <br />
	    Remain Time : <asp:Literal ID="ltrRemainTime" runat="server"></asp:Literal> -> <asp:TextBox id="txtRemainTime" runat="server" Enabled="False"></asp:TextBox> &nbsp;<asp:Button id="btnChange" runat="server" Text="Change" Enabled="False" CssClass="btn" onclick="btnChange_Click"></asp:Button>
    </div>
</div>

<h4>Delete Item</h4>
<p><asp:Button id="btnDel" runat="server" Text="Del" Enabled="False" CssClass="btn" onclick="btnDel_Click"></asp:Button></p>

<p class="centerBtn"><input class="btn btn-primary" type="button" value="Close" onclick="window.close()" /></p>
<asp:HiddenField ID="hdnSummonCode" runat="server" />
<asp:HiddenField ID="hdnOldSummonEnhance" runat="server" />
</form>
</body>
</html>
