<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_game_gold_day, App_Web_game_gold_day.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<%--<script type="text/javascript" src="/Common/js/Calendar.js"></script>--%>
<script type="text/javascript">
    $(function () {
        var nowDate = "<%=hdNowDate.Text %>";

        $("#datepicker").datepicker({
            inline: true,
            onSelect: function (date) {                
                __doPostBack('<%=hdNowDate.ClientID %>', date);
            }
        }).datepicker("setDate", new Date(nowDate));
    });
</script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Statistics - Gold</h2>
			</div>
			<ul class="statisticsBox">
				<li class="calArea">
                    <div id="datepicker"></div>
				</li>
				<li class="statisticsArea">
					<table class="table table-bordered centerTable">
						<thead>
							<tr>
								<th>Server</th>
								<th>Date</th>
								<th>Gold</th>
								<th>Bank</th>
								<th>Gold variation</th>
								<th>Bank variation</th>
								<th>Total</th>
							</tr>
						</thead>
						<tbody>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>                            					
						</tbody>
					</table>
				</li>
			</ul>
		</div>
	</div>
</div>
<div style="display:none;">
<asp:LinkButton ID="hdNowDate" runat="server"></asp:LinkButton>
</div>
</asp:Content>

