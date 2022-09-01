<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Game_Dungeon, App_Web_dungeon.aspx.3d0f6542" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        $(".cServerNm strong").text($("[jid='ddlServerNm'] option:selected")[0].text);
        $("[jid='ddlServerNm'] option").click(function () {
            $(".cServerNm strong").text($(this)[0].text);
        });
    });
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Game DB - Dungeon</h2>
			</div>
			<table class="table table-bordered table-condensed">
				<colgroup>
					<col style="width:10%" />
					<col style="width:90%" />
				</colgroup>
				<tbody>
					<tr>
						<th>							
                            <asp:dropdownlist id="ddlServer" jid="ddlServerNm" runat="server"></asp:dropdownlist>
						</th>
						<td class="cServerNm">Now <strong></strong> Server Selected  <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search"/></td>						
					</tr>
				</tbody>
			</table>

			<table class="table table-bordered centerTable">
				<colgroup>
					<col style="width:5%" />
					<col style="width:*" />
					<col style="width:5%" />
					<col style="width:9%" />
					<col style="width:4%" />
					<col style="width:4%" />
					<col style="width:12%" />
					<col style="width:5%" />
					<col style="width:8%" />
					<col style="width:8%" />
					<col style="width:10%" />
					<col style="width:10%" />
					<col style="width:5%" />
					<col style="width:3%" />
				</colgroup>
				<thead>
					<tr>
						<th>Server</th>
						<th>Dungeon View</th>
						<th>Alliance</th>
						<th>Owner Guild</th>
						<th>Gold</th>
						<th>Lac</th>
						<th>Owner Guild Leader</th>
						<th>[R]Alliance</th>
						<th>Raid Guild</th>
						<th>Raid Guild Leader</th>
						<th>Siege</th>
						<th>Raid</th>
						<th>Best Raid Time</th>
						<th>Tax Rate</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Repeater ID="rptDungeon" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("server")%></td>
                                <td class="leftAlign"><%# BindDungeon(Container.DataItem)%></td>
                                <td><%# Eval("owner_guild_alliance_name")%></td>
                                <td class="leftAlign"><%# BindGuildName(Container.DataItem, "owner_")%></td>
                                <td><%# Eval("owner_guild_gold")%></td>
                                <td><%# Eval("owner_guild_chaos")%></td>
                                <td class="leftAlign"><%# BindGuildLeader(Container.DataItem, "owner_") %></td>
                                <td><%# Eval("raid_guild_alliance_name")%></td>
                                <td><%# BindGuildName(Container.DataItem, "raid_")%></td>
                                <td><%# BindGuildLeader(Container.DataItem, "raid_") %></td>
                                <td><%# Eval("last_dungeon_siege_finish_time")%></td>
                                <td><%# Eval("last_dungeon_raid_wrap_up_time")%></td>
                                <td><%# Eval("best_raid_time")%></td>
                                <td><%# Eval("tax_rate")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>					
				</tbody>
			</table>
			<hr />
			<table class="table table-bordered centerTable">
				<colgroup>
					<col style="width:5%" />
					<col style="width:*" />
					<col style="width:5%" />
					<col style="width:13%" />
					<col style="width:13%" />
					<col style="width:20%" />
					<col style="width:5%" />
				</colgroup>
				<thead>
					<tr>
						<th>Server</th>
						<th>Dungeon</th>
						<th>Alliance</th>
						<th>Guild</th>
						<th>Guild Leader</th>
						<th>Raid Time</th>
						<th>Record</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Repeater ID="rptDungeonRaid" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("server")%></td>
                                <td class="leftAlign"><%# Eval("dungeon_name")%></td>
                                <td><%# Eval("alliance_name")%></td>
                                <td class="leftAlign"><%#BindGuildName(Container.DataItem, "")%></td>
                                <td class="leftAlign"><%#BindGuildLeader(Container.DataItem, "") %></td>
                                <td><%# Eval("raid_time")%></td>
                                <td><%#BindRecord(Container.DataItem)%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>					
				</tbody>
			</table>
		</div>
	</div>
</div>
</asp:Content>

