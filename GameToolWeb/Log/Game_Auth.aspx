<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Log_Game_Auth, App_Web_game_auth.aspx.564c629f" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtStart.ClientID %>").datetimepicker({ showOn: "button", timeFormat: "HH:mm:ss", showSecond: true });
        $("#<%=txtEnd.ClientID %>").datetimepicker({ showOn: "button", timeFormat: "HH:mm:ss", showSecond: true });        
    });
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Game DB - Auth Server Log</h2>
			</div>
			<table class="table table-bordered table-condensed">
				<colgroup>
					<col style="width:10%" />
					<col style="width:90%" />
				</colgroup>
				<tbody>
					<tr>
						<th>Search</th>
						<td>
							<table class="logSearchTable">
								<tbody>
									<tr>
										<th>Month</th>
										<td colspan="5">
											<asp:DropDownList ID="ddlLogFile" runat="server"></asp:DropDownList><br /> 
										</td>
									</tr>
									<tr>
										<th>Date</th>
										<td colspan="5">
											<input type="text" id="txtStart" runat="server" /> ~ <input type="text" id="txtEnd" runat="server" />
										</td>
									</tr>
									<tr>
										<th>Account</th>
										<td><input type="text" id="txtAccount" runat="server" /></td>
										<th>account_id</th>
										<td><input type="text" id="txtAccountNo" runat="server" /></td>
										<th>IP</th>
										<td><input type="text" id="txtIP" runat="server" />&nbsp;<asp:button id="Button1" runat="server" Text="Search" onclick="Button1_Click" CssClass="btn btn-primary"></asp:button></td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
				</tbody>
			</table>
			<p>Result : <asp:literal id="ltrTotalCnt" runat="server" Text="0"></asp:literal> </p>
			<table class="table table-bordered">
				<colgroup>
					<col style="width:15%" />
					<col style="width:*" />
					<col style="width:25%" />
					<col style="width:20%" />
				</colgroup>
				<thead>
					<tr>
						<th>Date</th>
						<th>Account</th>
						<th>Msg</th>
						<th>IP</th>
					</tr>
				</thead>
				<tbody>                
                    <%=NullResult %>
                    <asp:Repeater ID="rptGameAuth" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal ID="ltrDate" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrAccount" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrMsg" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrIP" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
				</tbody>
			</table>
		</div>
	</div>
</div>
</asp:Content>

