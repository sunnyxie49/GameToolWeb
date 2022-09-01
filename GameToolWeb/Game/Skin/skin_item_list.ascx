<%@ control language="C#" autoeventwireup="true" inherits="Game_Skin_skin_item_list, App_Web_skin_item_list.ascx.f3d59aed" %>
<script language="javascript">
<!--
    function OpenEditItemPopup(server, item_uid) {
        var win = window.open("edit_item.aspx?server=" + server + "&sid=" + item_uid, "EditItemPopup", 'left=50,top=50,width=590,height=350,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no');
        win.focus();
    }

    function OpenSummonPopup(server, item_uid) {
        var win = window.open("summon_info.aspx?server=" + server + "&item_id=" + item_uid, "SumonPopup", 'left=50,top=50,width=530,height=450,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no');
        win.focus();
    }

    function ItemEffectOpen(DivId) {
        var obj;
        eval("obj = document.all." + DivId + ";");
        obj.style.visibility = "visible";
        obj.style.display = 'block';


        obj.style.left = event.x + document.documentElement.scrollLeft + 10;
        obj.style.top = event.y + document.documentElement.scrollTop - 40;

    }
    function ItemEffectClose(DivId) {
        var obj;
        eval("obj = document.all." + DivId + ";");
        obj.style.visibility = "hidden";
        obj.style.display = 'none';
    }
-->
</script>
<table class="table table-bordered centerTable">
	<colgroup>
		<col width="6%" />
		<col width="3%" />
		<col width="*" />
		<col width="3%" />
		<col width="6%" />
		<col width="10%" />
		<col width="5%" />
		<col width="6%" />
		<col width="5%" />
		<col width="10%" />
		<col width="4%" />
	</colgroup>
	<thead>
		<tr>
			<th>sid</th>
			<th>Icon</th>
			<th>Name</th>
			<th>Count</th>
			<th>Endurance</th>
			<th>ethereal_durability</th>
			<th>Get</th>
			<th>Class</th>
			<th>Wear</th>
			<th>Time</th>
			<th></th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptItemList" runat="server">
            <ItemTemplate>
                <tr id="trLine" runat="server">
                    <td><asp:Literal ID="ltrsid" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrIcon" runat="server"></asp:Literal></td>
                    <td class="leftAlign"><asp:Literal ID="ltrName" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrCount" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrEndurance" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrethereal" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrGet" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrClass" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrWear" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrTime" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrBlank" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>    
</table>
<asp:Literal ID="ltrItemLayer" runat="server" EnableViewState="False"></asp:Literal>