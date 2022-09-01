<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_game_summon_taming, App_Web_game_summon_taming.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script type="text/javascript">
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
				<h2>Game DB - Summon</h2>
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
						<th>Summon Type</th>
						<td>
                            <asp:RadioButtonList ID="rblSummonType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:RadioButtonList>
                            <%--<asp:Repeater ID="rptGameSummon" runat="server">
                                <ItemTemplate>
                                    <label class="radio inline"> <input type="radio" name="optionsRadios" value="<%# Eval("id")%>" id="optionsRadios"><%# BindGetSummonName(Container.DataItem)%></label>
                                </ItemTemplate>
                            </asp:Repeater>--%>
                            <asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-primary"  onclick="Button1_Click" />
                        </td>
					</tr>
				</tbody>
			</table>

			<p><asp:Label ID="lblSummonName" runat="server" Text="" Font-Bold="true"></asp:Label></p>
            <asp:Panel ID="pnSummon" runat="server" Visible="false">
			    <table class="table table-bordered centerTable smallTable">
				    <thead>
					    <tr>
						    <th>Enhance</th>
						    <th>Count</th>
					    </tr>
				    </thead>
				    <tbody>
                        <asp:Repeater ID="rptGameSummonTaming" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("enhance")%></td>
                                    <td><%# Eval("cnt")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>					
				    </tbody>
			    </table>
            </asp:Panel>
		</div>
	</div>
</div>
</asp:Content>

