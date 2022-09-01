<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Game_ItemCount, App_Web_itemcount.aspx.3d0f6542" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        $(".cServerNm strong").text($("[jid='ddlServerNm'] option:selected")[0].text);
        $("[jid='ddlServerNm'] option").click(function () {            
            if ($(this)[0].value == 'none') {
                $(".cServerNm strong").text('');
                $("[jid='stServerNm']").attr("style", "display:none;");
            } else {
                $(".cServerNm strong").text($(this)[0].text);
                $("[jid='stServerNm']").attr("style", "display:;");
            }
        });
    });
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Item Quantity</h2>
			</div>
			<table class="table table-bordered table-condensed">
				<colgroup>
					<col style="width:10%" />
					<col style="width:90%" />
				</colgroup>
				<tbody>
					<tr>
						<th>							
                            <asp:dropdownlist id="ddlServer" AppendDataBoundItems="true" jid="ddlServerNm" runat="server"></asp:dropdownlist>
						</th>
						<td class="cServerNm">Now <strong id="idServerNm" jid="stServerNm" runat="server"></strong> Server Selected</td>
					</tr>
					<tr>
						<th>Search</th>
						<td>
							<label for="<%=txtItemCode.ClientID %>">Item Code:</label>&nbsp;<asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
						</td>
					</tr>
				</tbody>
			</table>
			<p>Result : <asp:Literal ID="ltrItemCount" runat="server"></asp:Literal></p>
            <table class="table table-bordered centerTable">
                <colgroup>
					<col style="width:25%" />
					<col style="width:25%" />
                    <col style="width:25%" />
                    <col style="width:25%" />
				</colgroup>
                <thead>
                    <tr>
                        <th>Item Name</th>
						<th>Server</th>
						<th>Code</th>
						<th>Count</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptItemList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><asp:Literal ID="ltrItemNm" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrServerNm" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrCode" runat="server"></asp:Literal></td>
                                <td><asp:Literal ID="ltrCount" runat="server"></asp:Literal></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>                    
                </tbody>
            </table>
		</div>
	</div>
</div>
</asp:Content>

