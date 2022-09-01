<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_game_lv, App_Web_game_lv.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script language="javascript" src="/Charts/FusionCharts.js"></script>
<script type="text/javascript">
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
        var GameLv = "";
        $(".chkid").each(function (index) {
            if ($(this).is(":checked")) {
                GameLv += $(this).val() + ",";
            }
        });

        $(".hdGameLv").val(GameLv);
    }    
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Statistics - Character Lv</h2>
			</div>

			<table class="table table-bordered">
				<tbody>
					<tr>
						<td>
                            <asp:Repeater ID="rptGameLV" runat="server">
                                <ItemTemplate>
                                    <label class="checkbox inline"><input type="checkbox" class="chkid" value="<%# Eval("code") %>" /><%# Eval("name") %>(<%# Eval("code") %>)</label>                                    
                                </ItemTemplate>
                            </asp:Repeater>
                            <%--<asp:CheckBoxList ID="cblServer" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>--%>
							<input class="btn btn-primary" type="button" value="All" onclick="CheckServerAll()" />&nbsp;
							<asp:Button ID="Button1" runat="server" Text="Get Data" class="btn btn-primary" OnClientClick="ChkFInd_Click()" OnClick="Button1_Click" />
						</td>
					</tr>
				</tbody>
			</table>
            <div style="padding-bottom:40px;"></div>
            <asp:Literal ID="Literal1" runat="server" EnableViewState="False"></asp:Literal>
		</div>
	</div>
</div>
<input type="hidden" class="hdGameLv" id="hdGameLv" runat="server" />
</asp:Content>

