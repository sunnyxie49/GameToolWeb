<%@ page title="国服GM工具" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Game_Default, App_Web_default.aspx.3d0f6542" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=txtStart.ClientID %>").datetimepicker({ showOn: "button", timeFormat: "HH:mm:ss", showSecond: true });
        $("#<%=txtEnd.ClientID %>").datetimepicker({ showOn: "button", timeFormat: "HH:mm:ss", showSecond: true });

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
				<h2>Game DB - Character</h2>
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
						<th>Search</th>
						<td>                            
                            <label class="checkbox inline"><input type="checkbox" id="chkUser" runat="server" />User</label>                            
							<label class="checkbox inline"><input type="checkbox" id="chkGM" runat="server" />GM</label>
							<label class="checkbox inline"><input type="checkbox" id="chkGameAccount" runat="server" />Game Only Account</label>
							<label class="checkbox inline"><input type="checkbox" id="chkConnect" runat="server" />Only Login User</label>                            
							<label class="checkbox inline"><input type="checkbox" id="chkAuto" runat="server" />Only Auto User</label>								
							<div class="control-group">
								<label for="<%=txtAccount.ClientID %>" class="control-label">Account</label>
                                <input id="txtAccount" runat="server" class="input-medium" type="text" />                                
							</div>
							<div class="control-group">
								<label for="<%=txtCharacter.ClientID %>" class="control-label">Character Name</label>
                                <input id="txtCharacter" runat="server" class="input-medium" type="text" />                                
							</div>
							<label class="checkbox inline"><asp:checkbox id="chkLike" runat="server" Text="Like Search"></asp:checkbox></label>
                            <%--<asp:button id="Button1" runat="server" Text="Search" class="btn btn-primary" onclick="Button1_Click"></asp:button>--%>
                            <button type="submit" runat="server" onserverclick="Button1_Click" class="btn btn-primary">Search</button>
                            <button id="Button1" type="submit" runat="server" onserverclick="Button2_Click" class="btn btn-primary">CSV</button>
                            <%--<asp:Button id="Button2" runat="server" Text="CSV" class="btn btn-primary" onclick="Button2_Click"></asp:Button>--%>
						</td>
					</tr>
					<tr>
						<th>Sort</th>
						<td>							
                            <asp:dropdownlist id="ddlSort1" runat="server">
					            <asp:ListItem Selected="True">Sort</asp:ListItem>
					            <asp:ListItem Value="account">Account</asp:ListItem>
					            <asp:ListItem Value="gmtool_view_check_searchType_main.name">Character Name</asp:ListItem>
					            <asp:ListItem Value="exp">EXP</asp:ListItem>
					            <asp:ListItem Value="total_jp">Total JP</asp:ListItem>
					            <asp:ListItem Value="gmtool_view_check_searchType_main.gold">GOLD</asp:ListItem>
					            <asp:ListItem Value="play_time">Play Time</asp:ListItem>
					            <asp:ListItem Value="login_time">Login Time</asp:ListItem>
					            <asp:ListItem Value="logout_time">Logout Time</asp:ListItem>
					            <asp:ListItem Value="create_time">Create Time</asp:ListItem>
					            <asp:ListItem Value="huntaholic_point">Huntaholic Point</asp:ListItem>
				            </asp:dropdownlist>
							<label class="checkbox inline"><asp:checkbox id="chkSort1" runat="server" Text="desc"></asp:checkbox></label>
							<asp:DropDownList id="ddlViewCnt" runat="server">
					            <asp:ListItem Value="10">10</asp:ListItem>
					            <asp:ListItem Value="20">20</asp:ListItem>
					            <asp:ListItem Value="30">30</asp:ListItem>
					            <asp:ListItem Value="50">50</asp:ListItem>
					            <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
					            <asp:ListItem Value="250">250</asp:ListItem>
					            <asp:ListItem Value="500">500</asp:ListItem>
					            <asp:ListItem Value="1000">1000</asp:ListItem>
				            </asp:DropDownList>
							<span>/page</span>
							<strong>Job</strong> :
                            <asp:DropDownList id="ddlJob" runat="server">
					            <asp:ListItem Value="all" Selected="True">All Job</asp:ListItem>
				            </asp:DropDownList>							
						</td>
					</tr>
					<tr>
						<th>Optional</th>
						<td>
							<label class="checkbox inline"><asp:checkbox id="chkCreateTime" runat="server" Text="Create Time"></asp:checkbox></label>							
                            <asp:TextBox id="txtStart" runat="server" placeholder="Text input"></asp:TextBox>
							~
							<asp:TextBox id="txtEnd" runat="server" placeholder="Text input"></asp:TextBox>
							<label class="checkbox inline"><asp:checkbox id="chkSeeDeletedCharacter" runat="server" Text="Include Deleted Character"></asp:checkbox></label>
						</td>
					</tr>
				</tbody>
			</table>
			<div class="resultArea">
				<span class="searchResult">Result : <asp:literal id="ltrTotalCnt" runat="server" Text="0"></asp:literal></span>
				<div class="pagination pagination-centered">                    
					<ul>
                        <asp:literal id="ltrPageTop" runat="server"></asp:literal>						
					</ul>
				</div>
			</div>
			<table class="table table-bordered table-striped centerTable">
				<colgroup>
					<col style="width:6%" />
					<col style="width:13%" />
					<col style="width:25%" />
					<col style="width:2%" />
					<col style="width:3%" />
					<col style="width:4%" />
					<col style="width:8%" />
					<col style="width:7%" />
					<col style="width:9%" />
					<col style="width:11%" />
					<col style="width:*" />
					<col style="width:6%" />
				</colgroup>
				<thead>
					<tr>
						<th>No</th>
						<th>Account</th>
						<th>Character Name</th>
						<th>Lv</th>
						<th>JLv</th>
						<th>Lac</th>
						<th>Gold</th>
						<th>Immoral</th>
						<th>huntaholic_point</th>
						<th>Login</th>
						<th>Play Time</th>
						<th>Guild Id</th>
					</tr>
				</thead>
                <tbody>
                <asp:Repeater ID="rptAvaterInfo" runat="server">                    
                    <ItemTemplate>
                        <tr>
                            <td><%# BindNo() %></td>
                            <td class="leftAlign"><%# ((string)Eval("account")).ToLower() %>(<%# Eval("account_id") %>)</td>
                            <td class="leftAlign"><%# BindName(Container.DataItem) %>(<%# Eval("sid") %>)<%# BindPermission(Container.DataItem) %><%# BindAuto(Container.DataItem) %></td>
                            <td><span title='Exp : <%# ((Int64)Eval("exp")).ToString("#,##0") %>'><%# Eval("lv") %></span></td>
                            <td><span title='Total JP : <%# ((Int64)Eval("total_jp")).ToString("#,##0") %>'><%# Eval("jlv") %></span></td>
                            <td><%# Eval("chaos")%></td>
                            <td><%# ((Int64)Eval("gold")).ToString("#,##0")%></td>
                            <td><span title='PKC : <%# ((int)Eval("pkc")).ToString("#,##0") %>, DKC : <%# ((int)Eval("dkc")).ToString("#,##0") %>'><%# Eval("immoral_point") %></span></td>
                            <td><%# Eval("huntaholic_point")%></td>
                            <td><%# BindLogin(Container.DataItem) %></td>
                            <td><%# BindPlayTime(Container.DataItem) %></td>                            
                            <td><%# BindGuildName(Container.DataItem) %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </tbody>
			</table>
			<div class="pagination pagination-centered">                
				<ul>
                    <asp:literal id="ltrPageBottom" runat="server"></asp:literal>					
				</ul>
			</div>
		</div>
	</div>
</div>
</asp:Content>

