<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_server_down, App_Web_server_down.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
    function ChkFInd_Click() {
        var ServerDown = "";
        $(".chkServerDown").each(function (index) {
            if ($(this).is(":checked")) {
                ServerDown += $(this).val() + ",";
            }
        });

        $(".hdServerDown").val(ServerDown);
    }
</script>
<div class="container-fluid">
	<div class="page-header">
		<h2>Statistics - Server Down</h2>
	</div>
	<div class="row-fluid">
		<div class="span2">
			<div class="well">
				<p><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" OnClientClick="javascript:ChkFInd_Click();" Text="Get Data" CssClass="btn btn-primary btn-block" /><%--<input class="btn btn-primary btn-block" type="button" value="Get Data" />--%></p>
                <asp:Repeater ID="rptServerDown" runat="server">
                    <ItemTemplate>
                        <label class="checkbox"><input type="checkbox" class="chkServerDown" value="<%#BindDateValue(Container.DataItem) %>"><%# BindDate(Container.DataItem) %></label>
                    </ItemTemplate>
                </asp:Repeater>				
			</div>
		</div>
		<div class="span10">
            <asp:Literal ID="ltrResult" runat="server"></asp:Literal>			
		</div>
	</div>
</div>
<input type="hidden" class="hdServerDown" id="hdServerDown" runat="server" />
</asp:Content>

