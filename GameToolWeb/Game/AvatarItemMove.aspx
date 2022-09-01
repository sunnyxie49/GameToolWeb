<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Game_AvatarItemMove, App_Web_avataritemmove.aspx.3d0f6542" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
//    function viewItemList(sid, accountid, server) {
//        //document.getElementById("<%=hdItemList.ClientID%>").click();
//        //$("#divItemList").attr("style", "display:block");
//        __doPostBack("hdItemList", sid + "," + accountid + "," + server);        
//        //AvatarItemList.location.href = url;
//    }

    function OpenOwnerChangeItemPopup(server, type, item_uid) {
        var win = window.open("ChangeItemOwner.aspx?server=" + server + "&type=" + type + "&sid=" + item_uid, "ChangeItemOwner", 'left=50,top=50,width=620,height=450,toolbar=no,menubar=no,status=no,scrollbars=yes,resizable=no');
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
				<h2>Game DB - Avatar Item</h2>
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
						<th>Search </th>
						<td>
							<label class="radio inline"><input type="radio" name="optionsRadios" runat="server" id="rdoAID" value="0" checked />Account ID</label>
							<label class="radio inline"><input type="radio" name="optionsRadios" runat="server" id="rdoSID" value="1" />Character SID</label> 
							<asp:textbox id="txtSearch" tabIndex="1" runat="server" Width="100px" BorderStyle="Groove"></asp:textbox>&nbsp;
                            <asp:button id="Button1" runat="server" Text="Search" CssClass="btn btn-primary" onclick="Button1_Click"></asp:button>
						</td>
					</tr>
				</tbody>
			</table>

			<h4>Character Info</h4>
			<table class="table table-bordered centerTable">
				<thead>
					<tr>
						<th>Character Name(sid)</th>
						<th>Account(Account Id)</th>
						<th>Item List</th>
						<th>Login Check</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Repeater ID="rptCharacterList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# BindCharacterName(Container.DataItem) %></td>
                                <td><%# BindAccount(Container.DataItem) %></td>
                                <td><%# BindItemList(Container.DataItem) %></td>
                                <td><%# BindLoginCheck(Container.DataItem) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>					
				</tbody>
			</table>

            <%--<iframe id="AvatarItemList" width="100%" height="100%" style="border:solid 0px black;"></iframe>--%>

			<!-- item list iframe -->
            <div style="display:none;">
            <%--<asp:button id="hdItemList" runat="server" onclick="btnItemList_Click"></asp:button>--%>
            <asp:LinkButton ID="hdItemList" runat="server"></asp:LinkButton>
            </div>
            <div id="divItemList" runat="server" style="display:none;">            
			<h4>AVATAR()</h4>
			<table class="table table-bordered centerTable">
				<colgroup>
					<col style="width:20%" />
					<col style="width:30%" />
					<col style="width:10%" />
					<col style="width:18%" />
					<col style="width:*" />
					<col style="width:10%" />
				</colgroup>
				<thead>                    
					<tr>
						<th>id</th>
						<th>name</th>
						<th>count</th>
						<th>Get</th>
						<th>time</th>
						<th>Modify</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Repeater ID="rptAvatar" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("sid") %></td>
                                <td><%#BindCharaterName(Container.DataItem)%></td>
                                <td><%#Eval("cnt")%></td>
                                <td><%#BindGet(Container.DataItem)%></td>
                                <td><%#BindFlag(Container.DataItem)%></td>
                                <td><%#BindModifyForAvater(Container.DataItem)%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>					
				</tbody>
			</table>

			<h4>BANK</h4>
			<table class="table table-bordered centerTable">
				<colgroup>
					<col style="width:20%" />
					<col style="width:30%" />
					<col style="width:10%" />
					<col style="width:18%" />
					<col style="width:*" />
					<col style="width:10%" />
				</colgroup>
				<thead>
					<tr>
						<th>id</th>
						<th>name</th>
						<th>count</th>
						<th>Get</th>
						<th>time</th>
						<th>Modify</th>
					</tr>
				</thead>
				<tbody>
                    <asp:Repeater ID="rptBank" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("sid") %></td>
                                <td><%#BindCharaterName(Container.DataItem)%></td>
                                <td><%#Eval("cnt")%></td>
                                <td><%#BindGet(Container.DataItem)%></td>
                                <td><%#BindFlag(Container.DataItem)%></td>
                                <td><%#BindModifyForBank(Container.DataItem)%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>					
					
				</tbody>
			</table>
            </div>
            
		</div>
	</div>
</div>
</asp:Content>

