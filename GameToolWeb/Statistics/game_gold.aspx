<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_game_gold, App_Web_game_gold.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Statistics - Gold</h2>
			</div>

			<p>
				<asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
                <asp:Button ID="Button1" runat="server" Text="List View" class="btn btn-primary" OnClick="Button1_Click" />				
			</p>

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
		</div>
	</div>
</div>
</asp:Content>

