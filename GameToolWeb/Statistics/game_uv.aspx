<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_game_uv, App_Web_game_uv.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<script language="javascript" src="/Charts/FusionCharts.js"></script>
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Statistics - GAME UV, MCU, RU</h2>
			</div>

			<table class="table table-bordered table-condensed">
				<colgroup>
					<col style="width:10%" />
					<col style="width:90%" />
				</colgroup>
				<tbody>
					<tr>
						<th>Search</th>
						<td>
							<asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
							Year&nbsp;
							<asp:DropDownList ID="ddlMonth" runat="server">
				                <asp:ListItem Value="1"></asp:ListItem>
				                <asp:ListItem Value="2"></asp:ListItem>
				                <asp:ListItem Value="3"></asp:ListItem>
				                <asp:ListItem Value="4"></asp:ListItem>
				                <asp:ListItem Value="5"></asp:ListItem>
				                <asp:ListItem Value="6"></asp:ListItem>
				                <asp:ListItem Value="7"></asp:ListItem>
				                <asp:ListItem Value="8"></asp:ListItem>
				                <asp:ListItem Value="9"></asp:ListItem>
				                <asp:ListItem Value="10"></asp:ListItem>
				                <asp:ListItem Value="11"></asp:ListItem>
				                <asp:ListItem Value="12"></asp:ListItem>
			                </asp:DropDownList> 
							Month&nbsp;
							<asp:Button id="Button1" runat="server" class="btn btn-primary" Text="Search" OnClick="Button1_Click"></asp:Button>&nbsp;<asp:Button ID="btnDown" runat="server" OnClick="btnDown_Click" class="btn btn-primary" Text="Download(Account)" />
						</td>
					</tr>
				</tbody>
			</table>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>			
			<p><asp:Literal ID="Literal2" runat="server"></asp:Literal></p>
		</div>
	</div>
</div>
</asp:Content>

