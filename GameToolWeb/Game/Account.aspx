<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Game_Account, App_Web_account.aspx.3d0f6542" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Account List</h2>
			</div>
			<p><asp:FileUpload ID="FileUpload1" CssClass="btn" runat="server" /> <asp:Button ID="Button1" runat="server" Text="List View" onclick="Button1_Click" CssClass="btn btn-primary" /></p>
			<p class="text-error">Only text file. </p>
		</div>
	</div>
</div>
</asp:Content>

