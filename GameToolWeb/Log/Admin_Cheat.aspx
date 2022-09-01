<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Log_Admin_Cheat, App_Web_admin_cheat.aspx.564c629f" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
    function OpenEditItemPopup(server, item_uid) {
        var win = window.open("../Game/edit_item.aspx?server=" + server + "&sid=" + item_uid, "EditItemPopup", 'left=50,top=50,width=590,height=350,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=yes');
        win.focus();
    }

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
				<h2>Game DB - Admin Cheat Log</h2>
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
						<td  class="cServerNm">
							<p>Now <strong></strong> Server Selected</p>
							<div>
								<label for="<%=txtSearch.ClientID %>" class="">Accout :</label> 
								<asp:TextBox ID="txtSearch" CssClass="input-medium" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="Search" />
							</div>
						</td>
					</tr>
				</tbody>
			</table>

			<div class="resultArea">
				<span class="searchResult">Result : <asp:literal id="ltrTotalCnt" runat="server" Text="0"></asp:literal></span>
				<div class="pagination pagination-centered">
					<ul>
						<%--<li class="disabled"><a href="#">«</a></li>
						<li class="active"><a href="#">1</a></li>
						<li><a href="#">2</a></li>
						<li><a href="#">3</a></li>
						<li><a href="#">4</a></li>
						<li><a href="#">»</a></li>--%>
                        <asp:literal id="ltrPageTop" runat="server"></asp:literal>
					</ul>
				</div>
			</div>

			<table class="table table-bordered centerTable">
				<colgroup>
					<col style="width:4%" />
					<col style="width:10%" />
					<col style="width:11%" />
					<col style="width:6%" />
					<col style="width:11%" />
					<col style="width:11%" />
					<col style="width:6%" />
					<col style="width:4%" />
					<col style="width:4%" />
					<col style="width:4%" />
					<col style="width:4%" />
					<col style="width:4%" />
					<col style="width:*" />
				</colgroup>
				<thead>
					<tr>
						<th>sid</th>
						<th>log_date</th>
						<th>cheat_type</th>
						<th>admin</th>
						<th>account</th>
						<th>character</th>
						<th>item_id</th>
						<th>skill_id</th>
						<th>summon_id</th>
						<th>quest_id</th>
						<th>guild_id</th>
						<th>party_id</th>
						<th>result_msg</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Repeater ID="rptAdminCheat" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("sid")%></td>
                                <td><%#Eval("log_date")%></td>
                                <td class="leftAlign"><%#Eval("cheat_type")%></td>
                                <td><%#Eval("admin_name")%></td>
                                <td class="leftAlign"><%#Eval("account").ToString() %>(<%#Eval("account_id").ToString() %>)</td>
                                <td class="leftAlign"><%#BindCharacter(Container.DataItem) %></td>
                                <td><%#BindItemId(Container.DataItem) %></td>
                                <td><%#Eval("skill_id")%></td>
                                <td><%#Eval("summon_id")%></td>
                                <td><%#Eval("quest_id")%></td>
                                <td><%#Eval("guild_id")%></td>
                                <td><%#Eval("party_id")%></td>
                                <td class="leftAlign"><%#Eval("result_msg")%></td>                                
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>					
				</tbody>
			</table>
		</div>
	</div>
</div>
</asp:Content>

