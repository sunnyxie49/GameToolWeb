<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Notice_all_buff, App_Web_all_buff.aspx.ad4434c7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
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
        var AllBuff = "";
        $(".chkid").each(function (index) {
            if ($(this).is(":checked")) {
                AllBuff += $(this).val() + ",";
            }
        });

        $(".hdAllBuff").val(AllBuff);
    }

    function AddBtnChk() {
        var stState = document.getElementById("<%=txtAdd.ClientID%>").value;
        var stLv = document.getElementById("<%=txtStateLv.ClientID%>").value;

        if (stState == "") {
            alert("Check STATE ID");
            return false;
        }

        if (stLv == "") {
            alert("Check STATE LV");
            return false;
        }

        if (stState.match(/[^0-9]/)) {
            alert("only numbers");
            return false;
        }

        if (stLv.match(/[^0-9]/)) {
            alert("only numbers");
            return false;
        }

        var AllBuff = "";
        $(".chkid").each(function (index) {
            if ($(this).is(":checked")) {
                AllBuff += $(this).val() + ",";
            }
        });

        $(".hdAllBuff").val(AllBuff);

        return true;
    }

    function RemoveBtnChk() {
        var stState = document.getElementById("<%=txtRemove.ClientID%>").value;

        if (stState == "") {
            alert("Check STATE ID");
            return false;
        }

        if (stState.match(/[^0-9]/)) {
            alert("only numbers");
            return false;
        }

        var AllBuff = "";
        $(".chkid").each(function (index) {
            if ($(this).is(":checked")) {
                AllBuff += $(this).val() + ",";
            }
        });

        $(".hdAllBuff").val(AllBuff);

        return true;
    }
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Game State</h2>
			</div>

			<table class="table table-bordered table-condensed">
				<col style="width:10%" />
				<col style="width:90%" />
				<tbody>
					<tr>
						<th>Server</th>
						<td>
                            <%--<asp:CheckBoxList ID="cblServer" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" meta:resourcekey="cblServerResource1"></asp:CheckBoxList>--%>
                            <asp:Repeater ID="rptAllBuff" runat="server">
                                <ItemTemplate>
                                    <label class="checkbox inline"><input type="checkbox" class="chkid" value="<%# Eval("code") %>" /><%# Eval("name") %>(<%# Eval("code") %>)</label>
                                </ItemTemplate>
                            </asp:Repeater>
							&nbsp;<input class="btn btn-primary" type="button" value="All" onclick="CheckServerAll()" />
						</td>
					</tr>
				</tbody>
			</table>

			<table class="table table-bordered table-condensed">
				<col style="width:10%" />
				<col style="width:90%" />
				<tbody>
					<tr>
						<th>Add event state</th>
						<td>
							<label for="<%=txtAdd.ClientID%>">STATE ID :</label> <asp:TextBox ID="txtAdd" runat="server"></asp:TextBox>&nbsp;
							<label for="<%=txtStateLv.ClientID%>">STATE LV :</label> <asp:TextBox ID="txtStateLv" runat="server"></asp:TextBox>
							<asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="OK" onclick="Button1_Click" OnClientClick="return AddBtnChk(); ChkFInd_Click();" />
						</td>
					</tr>
					<tr>
						<th>Remove event state</th>
						<td>
							<label for="<%=txtRemove.ClientID %>">STATE ID :</label> <asp:TextBox ID="txtRemove" runat="server"></asp:TextBox>&nbsp;
							<asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="OK" onclick="Button2_Click" OnClientClick="return RemoveBtnChk(); ChkFInd_Click();" />
						</td>
					</tr>
				</tbody>
			</table>
			<p>Event State List&nbsp;<asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Text="View List"  onclick="Button3_Click" /></p>            
            <asp:Literal ID="ltrList" runat="server"></asp:Literal>
		</div>
	</div>
</div>
<input type="hidden" class="hdAllBuff" id="hdAllBuff" runat="server" />
</asp:Content>

