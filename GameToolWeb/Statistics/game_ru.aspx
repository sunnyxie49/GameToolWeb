<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_game_ru, App_Web_game_ru.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Statistics - GAME UV, MCU, RU</h2>
			</div>

			<table class="table table-bordered">
				<tbody>
					<tr>
						<td>
                            <asp:Button ID="Button1" runat="server" Text="Get All Data of Month" class="btn btn-primary" OnClick="Button1_Click" />&nbsp;
                            <asp:Button ID="Button2" runat="server" Text="Get All Data of Date" class="btn btn-primary" OnClick="Button2_Click" />&nbsp;
							<asp:DropDownList ID="ddlMonth" runat="server">
	                        </asp:DropDownList>&nbsp;
                            <asp:Button ID="Button3" runat="server" Text="Get Data of Selected Month" class="btn btn-primary" OnClick="Button3_Click" />&nbsp;
							LV&nbsp;
							<asp:DropDownList ID="ddlLv" runat="server"></asp:DropDownList>&nbsp;
                            <asp:Button ID="btnDown" runat="server" OnClick="btnDown_Click" class="btn btn-primary" Text="Download(Account)" />	
						</td>
					</tr>
				</tbody>
			</table>
            <asp:Literal ID="ltrMsg" runat="server"></asp:Literal>
			<ul class="dataList">
				<li class="monthList">
					<table class="table table-bordered centerTable">
						<thead>                            
							<tr>
								<th>Avatar Created Date</th>
								<th>GAME RU</th>
								<th>GAME A.RU</th>
								<th>User Count of Selected LV</th>
								<th>Average User Count (Selected LV)</th>
								<th>RU -> Selected LV Percentage </th>
							</tr>
						</thead>
						<tbody>
                            <asp:Repeater ID="rptMonthList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("create_time")%></td>
                                        <td><%# ((int)Eval("cnt")).ToString("#,##0")%></td>
                                        <td><%# ((int)Eval("average_cnt")).ToString("#,##0")%></td>
                                        <td><%# ((int)Eval("lv_cnt")).ToString("#,##0")%></td>
                                        <td><%# ((int)Eval("lv_average_cnt")).ToString("#,##0")%></td>
                                        <td><%# ((int)Eval("lv_cnt") *100 / (int)Eval("cnt") ).ToString("#,##0")%>%</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
						</tbody>
					</table>
				</li>
				<li class="dateList">
					<table class="table table-bordered centerTable">
						<thead>
							<tr>
								<th>Avatar Created Date</th>
								<th>GAME RU</th>
								<th>User Count of Selected LV</th>								
								<th>RU -> Selected LV Percentage </th>
							</tr>
						</thead>
						<tbody>
                            <asp:Repeater ID="rptDateList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("create_time")%></td>
                                        <td><%# ((int)Eval("cnt")).ToString("#,##0")%></td>
                                        <td><%# ((int)Eval("lv_cnt")).ToString("#,##0")%></td>
                                        <td><%# ((int)Eval("lv_cnt") *100 / (int)Eval("cnt") ).ToString("#,##0")%>%</td>                                        
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>							
						</tbody>
					</table>
				</li>
			</ul>
		</div>
	</div>
</div>
</asp:Content>

