<%@ control language="C#" autoeventwireup="true" inherits="Game_Skin_skin_shop_list, App_Web_skin_shop_list.ascx.f3d59aed" %>
<p class="text-info">[ Left Item ]</p>
<table class="table table-bordered centerTable bgGray">
	<colgroup>
		<col width="3%" />
		<col width="*" />
		<col width="10%" />
		<col width="10%" />
		<col width="10%" />
		<col width="8%" />
		<col width="4%" />
		<col width="10%" />
		<col width="3%" />
	</colgroup>
	<thead>
		<tr>
			<th>sid</th>
			<th>Name</th>
			<th>Bougth Time</th>
			<th>Check Time</th>
			<th>Taken Time</th>
			<th>Bought Count</th>
			<th>Count</th>
			<th>Valid Time</th>
			<th></th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptLeftItem" runat="server">
            <ItemTemplate>
                <tr>
                    <td><asp:Literal ID="ltrsid" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrName" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrBougthTime" runat="server"></asp:Literal></td>
                    <td id="trLine" runat="server"><asp:Literal ID="ltrCheckTime" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrTakenTime" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrBoughtCount" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrCount" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrValidTime" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrBlank" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>

<p class="text-info">[ All Used Item ]</p>
<table class="table table-bordered centerTable bgGray">
	<colgroup>
		<col width="3%" />
		<col width="*" />
		<col width="10%" />
		<col width="10%" />
		<col width="10%" />
		<col width="8%" />
		<col width="4%" />
		<col width="10%" />
		<col width="3%" />
	</colgroup>
	<thead>
		<tr>
			<th>sid</th>
			<th>Name</th>
			<th>Bougth Time</th>
			<th>Check Time</th>
			<th>Taken Time</th>
			<th>Bought Count</th>
			<th>Count</th>
			<th>Valid Time</th>
			<th></th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptUsedItem" runat="server">
            <ItemTemplate>
                <tr>
                    <td><asp:Literal ID="ltrsid" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrName" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrBougthTime" runat="server"></asp:Literal></td>
                    <td id="trLine" runat="server"><asp:Literal ID="ltrCheckTime" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrTakenTime" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrBoughtCount" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrCount" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrValidTime" runat="server"></asp:Literal></td>
                    <td><asp:Literal ID="ltrBlank" runat="server"></asp:Literal></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>