<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Notice_client_game, App_Web_client_game.aspx.ad4434c7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script language="javascript" type="text/javascript">
	<!-- 

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
                GameLv += $(this).val() + "_";
            }
        });

        $(".hdClientGame").val(GameLv);
    }  

	var time = 3000;
	var auto_submit = 1;

	function AutoExcute()
	{
		ifrMsg.location.reload();
	}

	function AutoStart(){
	   auto_submit = setInterval('AutoExcute()', time);
	}

	function AutoStop(){
		clearTimeout(auto_submit);
	}
	-->
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Game Notice</h2>
			</div>

			<table class="table table-bordered table-condensed">
				<col style="width:10%" />
				<col style="width:90%" />
				<tbody>
					<tr>
						<th>Server</th>
						<td>
                            <asp:Repeater ID="rptClientGame" runat="server">
                                <ItemTemplate>
                                    <label class="checkbox inline"><input type="checkbox" class="chkid" value="<%# Eval("code") %>" /><%# Eval("name") %>(<%# Eval("code") %>)</label>
                                </ItemTemplate>                            
                            </asp:Repeater>
                            <%--<asp:CheckBoxList ID="cblServer" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" meta:resourcekey="cblServerResource1">                               
                            </asp:CheckBoxList>--%>
							<input type="button" value="All" class="btn btn-primary" onclick="CheckServerAll()"/>
						</td>
					</tr>
					<tr>
						<th>Repeat</th>
						<td>
							<asp:DropDownList ID="ddlHour" runat="server"></asp:DropDownList>
							Hour&nbsp;
							<asp:DropDownList ID="ddlMin" runat="server"></asp:DropDownList>
							Min&nbsp;
							<asp:DropDownList ID="ddlSec" runat="server"></asp:DropDownList>
							Sec &nbsp;
							<label class="checkbox inline"><asp:CheckBox ID="cbRepeat" runat="server" /> repeat</label>
							<input id="Button5" type="button" value="Stop Repeat" class="btn btn-primary" onclick="AutoStop()" />
						</td>
					</tr>
				</tbody>
			</table>
			<p class="textArea">                
                <textarea rows="5" id="TextBox1" runat="server"></textarea>&nbsp;
                <asp:Button id="Button1" runat="server" Text="Send" class="btn btn-primary" OnClientClick="ChkFInd_Click()" onclick="Button1_Click"></asp:Button>
            </p>
			<!-- iframe -->
            <IFRAME id="ifrMsg" name="ifrMsg" marginWidth="0" marginHeight="0" src="" frameBorder="1" width="500" height="500" runat="server"></IFRAME>
	        <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
		</div>
	</div>
</div>
<input type="hidden" class="hdClientGame" id="hdClientGame" runat="server" />
</asp:Content>

