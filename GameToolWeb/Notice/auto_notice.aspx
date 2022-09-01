<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Notice_auto_notice, App_Web_auto_notice.aspx.ad4434c7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script language="javascript" type="text/javascript">
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
        var AutoNotice = "";
        $(".chkid").each(function (index) {
            if ($(this).is(":checked")) {
                AutoNotice += $(this).val() + ",";
            }
        });

        $(".hdAutoNotice").val(AutoNotice);
    }

    $(document).ready(function () {
        $("#<%=txtStart.ClientID %>").datetimepicker({ showOn: "button", timeFormat: "HH:mm:ss", showSecond: true });
        $("#<%=txtEnd.ClientID %>").datetimepicker({ showOn: "button", timeFormat: "HH:mm:ss", showSecond: true });        
    });
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Game Auto Notice</h2>
			</div>

			<table class="table table-bordered table-condensed">
				<col style="width:10%" />
				<col style="width:90%" />
				<tbody>
					<tr>
						<th>Server</th>
						<td>
                            <asp:Repeater ID="rptAutoNotice" runat="server">
                                <ItemTemplate>
                                    <label class="checkbox inline"><input type="checkbox" class="chkid" value="<%# Eval("code") %>" /><%# Eval("name") %>(<%# Eval("code") %>)</label>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%--<asp:CheckBoxList ID="cblServer" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" meta:resourcekey="cblServerResource1"></asp:CheckBoxList>--%>							
							<input class="btn btn-primary" type="button" value="All" onclick="CheckServerAll()" />
						</td>
					</tr>
					<tr>
						<th>Repeat</th>
						<td>
							<label for="<%=txtStart.ClientID %>">begin_time :</label> <input type="text" id="txtStart" runat="server" />

							<label for="<%=txtEnd.ClientID %>">end_time :</label> <input type="text" id="txtEnd" runat="server" />

							<label for="<%=txtRepeatSec.ClientID %>">Repeat Cycle :</label> <asp:TextBox ID="txtRepeatSec" runat="server"></asp:TextBox> Sec
						</td>
					</tr>
				</tbody>
			</table>
			<p class="textArea"><asp:TextBox id="txtNotice" runat="server" BorderStyle="Groove" TextMode="MultiLine" Width="500px" height="80"></asp:TextBox>&nbsp;<asp:Button id="Button1" runat="server" Text="Send" class="btn btn-primary" OnClientClick="ChkFInd_Click()" onclick="Button1_Click"></asp:Button></p>
			
			<table class="table table-bordered">
				<colgroup>
					<col style="width:10%" />
					<col style="width:90%" />
				</colgroup>
				<tbody>
					<tr>
						<th>
							<asp:DropDownList ID="ddlServer" runat="server" ></asp:DropDownList>
							<asp:Button ID="Button2" runat="server" Text="Select Server" class="btn btn-primary mt4" onclick="Button2_Click" />
						</th>
						<td>
							<table class="table table-condensed centerTable leftBordernone">
								<colgroup>
									<col style="width:20%" />
									<col style="width:20%" />
									<col style="width:*" />
									<col style="width:10%" />
								</colgroup>
								<thead>
									<tr>
										<th>begin time</th>
										<th>end time</th>
										<th>notice body</th>
										<th>modify</th>
									</tr>
								</thead>
								<tbody>
                                    <asp:Repeater ID="rptNotice" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><asp:Literal ID="ltrBegin" runat="server"></asp:Literal></td>
                                                <td><asp:Literal ID="ltrEnd" runat="server"></asp:Literal></td>
                                                <td><asp:Literal ID="ltrCommand" runat="server"></asp:Literal></td>
                                                <td><asp:Literal ID="ltrModify" runat="server"></asp:Literal></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>									
								</tbody>
							</table>
						</td>
					</tr>
				</tbody>
			</table>
		</div>
	</div>
</div>
<input type="hidden" class="hdAutoNotice" id="hdAutoNotice" runat="server" />
</asp:Content>

