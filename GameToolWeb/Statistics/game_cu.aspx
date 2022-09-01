<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_game_cu, App_Web_game_cu.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script language="javascript" src="/Charts/FusionCharts.js"></script>
<script type="text/javascript">
    $(function () {
        var nowDate = new Date(<%=year %>, <%=month %>-1, <%=day %>);

        $("div#picker").datepicker({
            inline: true,
            onSelect: function (date) {
                var arrDate = date.split("-");

                if (arrDate.length >= 3) {
                    date = arrDate[1] + "/" + arrDate[2] + "/" + arrDate[0];
                }
                __doPostBack('<%=hdNowDate.ClientID %>', date);
            }
        }).datepicker("setDate", nowDate);
    });
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Statistics - GAME CU</h2>
			</div>
			<ul class="statisticsBox">
				<li class="calArea">					
                    <div id="picker"></div>
                    <asp:RadioButtonList ID="rblValue" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblValue_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
					    <asp:ListItem Text="Min" Value="Min"></asp:ListItem> 
					    <asp:ListItem Text="Avg" Value="Avg"></asp:ListItem> 
					    <asp:ListItem Text="Max" Value="Max" Selected="True">
                    </asp:ListItem>  
			        </asp:RadioButtonList>
                    <%--<label class="radio"><input type="radio" name="optionsRadios" id="optionsRadios1" value="Min">Min</label>
					<label class="radio"><input type="radio" name="optionsRadios" id="optionsRadios2" value="Avg">Avg</label>
					<label class="radio"><input type="radio" name="optionsRadios" id="optionsRadios3" value="Max" checked>Max</label>--%>
				</li>
				<li class="statisticsArea">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>					
					<p class="alert alert-block" id="pacu" runat="server"><asp:Literal ID="Literal2" runat="server"></asp:Literal></p>
				</li>
			</ul>
		</div>
	</div>
</div>
<div style="display:none;">
<asp:LinkButton ID="hdNowDate" runat="server"></asp:LinkButton>
</div>
<input type="hidden" id="hdRadioCheck" runat="server" />
</asp:Content>

