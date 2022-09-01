<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Game_Guild, App_Web_guild.aspx.3d0f6542" %>

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
				<h2>Game DB - 길드 정보</h2>
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
						<td class="cServerNm">Now <strong></strong> Server Selected</td>
					</tr>
					<tr>
						<th>Guild Name</th>
						<td>
							<asp:TextBox ID="txtGuildName" runat="server"></asp:TextBox> <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="Search" />
							<label class="radio inline"><input type="radio" name="optionsRadios" id="optionsRadios1" runat="server" value="cnt" checked />Guild Member</label>
							<label class="radio inline"><input type="radio" name="optionsRadios" id="optionsRadios2" runat="server" value="donation_point" />Donation Point</label>
						</td>
					</tr>
				</tbody>
			</table>

			<table class="table table-bordered centerTable">
				<colgroup>
					<col style="width:3%" />
					<col style="width:3%" />
					<col style="width:12%" />
					<col style="width:3%" />
					<col style="width:6%" />
					<col style="width:5%" />
					<col style="width:14%" />
					<col style="width:10%" />
					<col style="width:5%" />
					<col style="width:5%" />
					<col style="width:7%" />
					<col style="width:*" />
				</colgroup>
				<thead>
					<tr>
						<th>Server</th>
						<th>sid</th>
						<th>Name</th>
						<th>Member</th>
						<th>Alliance</th>
						<th>AllianceBlockTime</th>
						<th>Leader</th>
						<th>Dungeon</th>
						<th>Gold</th>
						<th>Chaos</th>
						<th>Donation Point</th>
						<th>Notice</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Repeater ID="rptGuild" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# BindNo() %></td>
                                <td><%# Eval("sid")%></td>
                                <td class="leftAlign"><%# BindGuildName(Container.DataItem)%></td>
                                <td><%# Eval("cnt")%></td>
                                <td><%# Eval("alliance_name")%></td>
                                <td><%# BindAllianceBlockTime(Container.DataItem)%></td>
                                <td class="leftAlign"><%# BindGuildLeader(Container.DataItem) %></td>
                                <td><%# BindDungeon(Container.DataItem) %></td>
                                <td><%# Eval("gold")%></td>
                                <td><%# Eval("chaos")%></td>
                                <td><%# Eval("donation_point")%></td>
                                <td class="leftAlign"><%# BindGuildNotice(Container.DataItem) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>					
				</tbody>
			</table>
		</div>
	</div>
</div>
</asp:Content>

