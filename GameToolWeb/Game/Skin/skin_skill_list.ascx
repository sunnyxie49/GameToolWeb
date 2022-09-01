<%@ control language="C#" autoeventwireup="true" inherits="Game_Skin_skin_skill_list, App_Web_skin_skill_list.ascx.f3d59aed" %>
<script language="javascript">
    function OpenEditSkillPopup(server, skill_uid) {
        var win = window.open("edit_skill.aspx?server=" + server + "&sid=" + skill_uid, "EditItemPopup", 'left=50,top=50,width=530,height=350,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no');
        win.focus();
    }
</script>
<table class="table table-bordered listTable centerTable">
	<colgroup>
		<col width="6%" />
		<col width="3%" />
		<col width="25%" />
		<col width="3%" />
		<col width="*" />
		<col width="6%" />
	</colgroup>
	<thead>
		<tr>
			<th>sid</th>
			<th>Icon</th>
			<th>Skill Name</th>
			<th>Type</th>
			<th>Info</th>
			<th></th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptSkillList" runat="server">
            <ItemTemplate>
                <tr id="trLine" runat="server">
                    <td><asp:Literal ID="ltrsid" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrIcon" runat="server"></asp:Literal></td>
                    <td class="leftAlign"><asp:Literal ID="ltrSkillName" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrType" runat="server"></asp:Literal></td>
                    <td class="leftAlign"><asp:Literal ID="ltrInfo" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrBlank" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>