<%@ control language="C#" autoeventwireup="true" inherits="Game_Skin_skin_quest_list, App_Web_skin_quest_list.ascx.f3d59aed" %>
<table class="table table-bordered centerTable">
	<colgroup>
		<col width="4%" />
		<col width="7%" />
		<col width="*" />
		<col width="10%" />
		<col width="10%" />
		<col width="10%" />
		<col width="10%" />
	</colgroup>
	<thead>
		<tr>
			<th>id</th>
			<th>Quest ID</th>
			<th>Quest Name</th>
			<th>Status 1</th>
			<th>Status 2</th>
			<th>Status 3</th>
			<th>progress</th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptQuestList" runat="server">
            <ItemTemplate>
                <tr>
                    <td><asp:Literal ID="ltrid" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrQuestID" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrQuestName" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrStatus1" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrStatus2" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrStatus3" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrprogress" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>