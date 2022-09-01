<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Game_Character, App_Web_character.aspx.3d0f6542" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
    var allServerChecked = false;
    var isAllChecked = false;
    function CheckServerAll() {
        if (isAllChecked) {
            $(".chkid").each(function (index) {
                $(this).attr('checked', false);
            });
            isAllChecked = false;
        } else {
            $(".chkid").each(function (index) {
                $(this).attr('checked', true);
            });
            isAllChecked = true;
        }
    }

    function ChkFInd_Click() {
        var Chracter = "";
        $(".chkid").each(function (index) {
            if ($(this).is(":checked")) {
                Chracter += $(this).val() + ",";
            }
        });

        $(".hdChracter").val(Chracter);
    }

    function chkValue()
	{
		if(document.getElementById("<%=chkLike.ClientID%>").checked == true && document.getElementById("<%=txtSearchText.ClientID%>").value.length < 2)
		{
			alert("Please enter the search words more then 2 characters");
			return false;
		}
		else
		{
			if(document.getElementById("<%=txtSearchText.ClientID%>").value.match(/[%]/)) 
			{ 
				alert('NO %!!');
				return false;
			}
			else
			{
			    return true;
			}
		}        
	}
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
						<th><asp:Label ID="Label1" runat="server" Text="Server" meta:resourcekey="Label1Resource1"></asp:Label></th>
						<td class="cblServer">
                            <asp:Repeater ID="rptChracter" runat="server">
                                <ItemTemplate>
                                    <label class="checkbox inline"><input type="checkbox" class="chkid" value="<%# Eval("code") %>" /><%# Eval("name") %>(<%# Eval("code") %>)</label>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%--<asp:CheckBoxList ID="cblServer" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" meta:resourcekey="cblServerResource1"></asp:CheckBoxList>--%>
							<input class="btn btn-primary" type="button" value="All" onclick="CheckServerAll()" />
						</td>
					</tr>
					<tr>
						<th><asp:Label ID="Label2" runat="server" Text="Search Option" meta:resourcekey="Label2Resource1"></asp:Label></th>
						<td>							
                            <asp:DropDownList ID="ddlSearchType" runat="server" meta:resourcekey="ddlSearchTypeResource1">	
					            <asp:ListItem Value="account" meta:resourcekey="ListItemResource2">Account</asp:ListItem>
					            <asp:ListItem Value="character_name" Selected="True" meta:resourcekey="ListItemResource4">Character Name</asp:ListItem>
					            <asp:ListItem Value="guild_name" meta:resourcekey="ListItemResource8">Guild Name</asp:ListItem>
					            <%--<asp:ListItem Value="party_name" meta:resourcekey="ListItemResource6">Party Name</asp:ListItem>--%>		
					            <asp:ListItem Value="item_code" meta:resourcekey="ListItemResource11">* Item Code(character)</asp:ListItem>
					            <asp:ListItem Value="item_code_bank" meta:resourcekey="ListItemResource12">* Item Code(bank)</asp:ListItem>
					            <asp:ListItem Value="gold" meta:resourcekey="ListItemResource9">Gold</asp:ListItem>
					            <%--<asp:ListItem Value="bank_gold" meta:resourcekey="ListItemResource10">* Bank Gold</asp:ListItem>--%>
					            <asp:ListItem Value="account_id" meta:resourcekey="ListItemResource1">Account SID</asp:ListItem>
					            <asp:ListItem Value="character_id" meta:resourcekey="ListItemResource3">Character SID</asp:ListItem>
					            <asp:ListItem Value="guild_id" meta:resourcekey="ListItemResource7">Guild SID</asp:ListItem>
					            <asp:ListItem Value="party_id" meta:resourcekey="ListItemResource5">Party SID</asp:ListItem>
				            </asp:DropDownList> &nbsp;
							<asp:TextBox ID="txtSearchText" runat="server" meta:resourcekey="txtSearchTextResource1"></asp:TextBox>
							<label class="checkbox inline"><asp:CheckBox ID="cbDeleteCharacter" runat="server" Text="Include Deleted Character" meta:resourcekey="cbDeleteCharacterResource1" /></label>
							<label class="checkbox inline"><asp:checkbox id="chkLike" runat="server" Text="Like Search"></asp:checkbox></label>
                            <asp:Button ID="btnSearch" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" OnClientClick="javascript:ChkFInd_Click(); return chkValue()" />							
						</td>
					</tr>
				</tbody>
			</table>
			
			<p>Search Result : <asp:Literal ID="ltrTotalCnt" runat="server" meta:resourcekey="ltrTotalCntResource1"></asp:Literal><br /></p>

			<table class="table table-bordered centerTable">
				<colgroup>
					<col style="width:8%" />
					<col style="width:10%" />
					<col style="width:*" />
					<col style="width:5%" />
					<col style="width:5%" />
					<col style="width:5%" />
					<col style="width:8%" />
					<col style="width:8%" />
					<col style="width:8%" />
					<col style="width:10%" />
					<col style="width:8%" />
					<col style="width:10%" />
				</colgroup>
				<thead>
					<tr>
						<th>Server</th>
						<th>Account</th>
						<th>Character</th>
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
                                <td><%# BindServer(Container.DataItem) %></td>
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
                                <td class="leftAlign"><%# BindGuildName(Container.DataItem) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>					
				</tbody>
			</table>
		</div>
	</div>
</div>
<input type="hidden" class="hdChracter" id="hdChracter" runat="server" />
</asp:Content>

