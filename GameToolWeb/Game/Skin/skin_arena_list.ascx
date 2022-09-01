<%@ control language="C#" autoeventwireup="true" inherits="Game_Skin_skin_arena_list, App_Web_skin_arena_list.ascx.f3d59aed" %>
<script language="javascript">
<!--
    function OpenEditSkillPopup(server, skill_uid) {
        var win = window.open("edit_skill.aspx?server=" + server + "&sid=" + skill_uid, "EditItemPopup", 'left=50,top=50,width=530,height=350,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no');
        win.focus();
    }
-->
</script>
<table class="table table-bordered listTable centerTable">
	<colgroup>
		<col width="7%" />
		<col width="*" />
		<col width="6%" />
		<col width="9%" />
		<col width="9%" />
		<col width="10%" />
		<col width="7%" />
		<col width="5%" />
		<col width="5%" />
		<col width="5%" />
		<col width="5%" />
		<col width="5%" />
		<col width="5%" />
		<col width="5%" />
	</colgroup>
	<thead>
		<tr>
			<th>account</th>
			<th>name</th>
			<th>arena_point</th>
			<th>arena_block_time</th>
			<th>arena_penalty_count</th>
			<th>arena_penalty_dec_time	</th>
			<th>arena_mvp_count</th>
			<th>classic win</th>
			<th>class defeat</th>
			<th>bingo win</th>
			<th>bingo defeat</th>
			<th>slater win</th>
			<th>slater defeat</th>
			<th>alias</th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptAreanList" runat="server">
            <ItemTemplate>
                <tr>
                    <td><asp:Literal ID="ltraccount" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrname" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarenapoint" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_block_time" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_penalty_count" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_penalty_dec_time" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_mvp_count" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_record_0_0" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_record_0_1" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_record_1_0" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_record_1_1" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_record_2_0" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrarena_record_2_1" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltralias" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>