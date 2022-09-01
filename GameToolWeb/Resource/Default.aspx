<%@ page title="国服GM工具" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Resource_Default, App_Web_default.aspx.9b2f0a21" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Game DB - Resource</h2>
			</div>

			<div class="halfWidth">
				<table class="table table-bordered centerTable">
					<colgroup>
						<col style="width:45%" />
						<col style="width:45%" />
						<col style="width:10%" />
					</colgroup>
					<thead>
						<tr>
							<th>Title</th>
							<th>Last Update Date</th>
							<th>Update</th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<td>Server List</td>
							<td><asp:Literal ID="ltrServerList" runat="server"></asp:Literal></td>
							<td><asp:Button id="btnServerList" runat="server" Text="Update" class="btn btn-primary" OnClick="btnServerList_Click"></asp:Button></td>
						</tr>
						<tr>
			                <td>Dev Server Resource</td>
			                <td><asp:Literal ID="ltrResource0" runat="server"></asp:Literal></td>
			                <td><asp:Button id="btnUpdate0" runat="server" Text="Update" class="btn btn-primary" onclick="btnUpdate0_Click"></asp:Button></td>
		                </tr>
		                <tr>
			                <td>QA Server Resource</td>
			                <td><asp:Literal ID="ltrResource1" runat="server"></asp:Literal></td>
			                <td><asp:Button id="btnUpdate1" runat="server" Text="Update" class="btn btn-primary" onclick="btnUpdate1_Click"></asp:Button></td>
		                </tr>
		                <tr>
			                <td>Test Server Resource</td>
			                <td><asp:Literal ID="ltrResource2" runat="server"></asp:Literal></td>
			                <td><asp:Button id="btnUpdate2" runat="server" Text="Update" class="btn btn-primary" onclick="btnUpdate2_Click"></asp:Button></td>
		                </tr>
		                <tr>
			                <td>Service Server Resource</td>
			                <td><asp:Literal ID="ltrResource3" runat="server"></asp:Literal></td>
			                <td><asp:Button id="btnUpdate3" runat="server" Text="Update" class="btn btn-primary" onclick="btnUpdate3_Click"></asp:Button></td>
		                </tr>
					</tbody>
				</table>
			</div>
		</div>
	</div>
</div>
</asp:Content>

