<%@ control language="C#" autoeventwireup="true" inherits="Game_Skin_skin_title_list, App_Web_skin_title_list.ascx.f3d59aed" %>
<table class="table table-bordered centerTable">
	<colgroup>
		<col width="40%" />
		<col width="60%" />
	</colgroup>
	<thead>
		<tr>
			<th>code</th>
			<th>status</th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptTitleList" runat="server">
            <ItemTemplate>
                <tr>
                    <%--<td><asp:Literal ID="ltrsid" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrownerid" runat="server"></asp:Literal></td>--%>
                    <td><asp:Literal ID="ltrcode" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrstatus" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>