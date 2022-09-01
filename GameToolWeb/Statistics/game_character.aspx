<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Statistics_game_character, App_Web_game_character.aspx.77f20ba8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Statistics - Character CSV</h2>
			</div>

			<p>
                <asp:Button ID="Button1" runat="server" class="btn btn-success btn-large" Text="Sever" OnClick="Button1_Click" />
	            <asp:Button ID="Button2" runat="server" class="btn btn-success btn-large" Text="Each Server" OnClick="Button2_Click" />				
			</p>

		</div>
	</div>
</div>
</asp:Content>

