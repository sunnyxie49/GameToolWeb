<%@ control language="C#" autoeventwireup="true" inherits="Game_Skin_skin_summon_list, App_Web_skin_summon_list.ascx.f3d59aed" %>
<table class="table table-bordered centerTable">
	<colgroup>
		<col width="7%" />
		<col width="10%" />
		<col width="*%" />
		<col width="4%" />
		<col width="8%" />
		<col width="4%" />
		<col width="4%" />
		<col width="4%" />
		<col width="8%" />
		<col width="5%" />
		<col width="7%" />
		<col width="7%" />
		<col width="7%" />
		<col width="8%" />
	</colgroup>
	<thead>
		<tr>
			<th>sid</th>
			<th>Type</th>
			<th>Name</th>
			<th>Trans</th>
			<th>Card ID</th>
			<th>LV</th>
			<th>JLV</th>
			<th>fp</th>
			<th>Exp</th>
			<th>JP</th>
			<th>SP</th>
			<th>HP</th>
			<th>MP</th>
			<th>RemainTime</th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptSummonList" runat="server">
            <ItemTemplate>
                <tr>
                    <td><asp:Literal ID="ltrsid" runat="server"></asp:Literal></td>
			        <td><asp:Literal ID="ltrType" runat="server"></asp:Literal></td>
			        <td><asp:Literal ID="ltrName" runat="server"></asp:Literal></td>
			        <td><asp:Literal ID="ltrTrans" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrCard" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrLV" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrJLV" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrfp" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrExp" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrJP" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrSP" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrHP" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrMP" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrRemain" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>