<%@ control language="C#" autoeventwireup="true" inherits="Game_Skin_skin_character_list, App_Web_skin_character_list.ascx.f3d59aed" %>
<table class="table table-bordered centerTable">
	<colgroup>
		<col width="2%" />
		<col width="4%" />
		<col width="10%" />
		<col width="13%" />
		<col width="2%" />
		<col width="7%" />
		<col width="2%" />
		<col width="6%" />
		<col width="4%" />
		<col width="*%" />
		<col width="*%" />
		<col width="*%" />
		<col width="8%" />
		<col width="4%" />
	</colgroup>
	<thead>
		<tr>
			<th>No</th>
			<th>sid</th>
			<th>Account</th>
			<th>Name</th>
			<th>Lv</th>
			<th>Job</th>
			<th>JLv</th>
			<th>Race</th>
			<th>Sex</th>
			<th>Exp</th>
			<th>Total JP</th>
			<th>Money</th>
			<th>Create Time</th>
			<th>LogIn</th>
		</tr>
	</thead>
	<tbody>
        <asp:Repeater ID="rptCharacterList" runat="server">
            <ItemTemplate>
                <tr id="trLine" runat="server">
                    <td><%# BindNo()%></td>
			        <td><%# Eval("sid")%></td>
			        <td><%# BindAccount(Container.DataItem)%></td>
			        <td class="leftAlign"><%# BindPermission(Container.DataItem) %><%# BindName(Container.DataItem) %></td>
			        <td><%# Eval("lv")%></td>
			        <td><%# BindJob(Container.DataItem) %></td>
			        <td><%# Eval("jlv")%></td>
			        <td><%# BindRace(Container.DataItem) %></td>
			        <td><%# BindSex(Container.DataItem) %></td>
			        <td><%# BindExp(Container.DataItem) %></td>
			        <td><%# BindTotalJP(Container.DataItem) %></td>
			        <td><%# BindGold(Container.DataItem) %></td>
			        <td><%# BindCreateTime(Container.DataItem) %></td>
			        <td><%# BindLoginCheck(Container.DataItem)%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>		
	</tbody>
</table>