<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Admin_authority_grade, App_Web_authority_grade.aspx.fdf7a39c" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>관리자 등급 권한</h2>
			</div>
			<div class="halfWidth">
				<p>Admin Group : 
                    <asp:DropDownList ID="ddlGrade" runat="server"></asp:DropDownList>					
                    <asp:Button ID="btnGetAuthority" runat="server" class="btn btn-primary" Text="Load" OnClick="btnGetAuthority_Click" />
	                <asp:Button ID="Button1" runat="server" Text="Save" class="btn btn-primary" OnClick="Button1_Click" />
	                <asp:Button ID="Button2" runat="server" class="btn btn-primary" OnClick="Button2_Click" Text="Apply" />					
					<asp:Button ID="Button3" runat="server" class="btn btn-danger" OnClick="Button3_Click" Text="Update Authority" meta:resourcekey="btnAuthorityResource1" />
				</p>
				<table class="table table-bordered lastCenter">
					<colgroup>
						<col style="width:30%" />
						<col style="width:20%" />
						<col style="width:35%" />
						<col style="width:15%" />
					</colgroup>
					<thead>
						<tr>
							<th>Code</th>
							<th>Category</th>
							<th>Info</th>
							<th>Grade Authority</th>
						</tr>
					</thead>
					<tbody>
                        <asp:Repeater ID="rptAuthGrade" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><asp:Literal ID="ltrCode" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltrCategory" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltrInfo" runat="server"></asp:Literal></td>
                                    <td class="centerAlign"><asp:Literal ID="ltrAuthority" runat="server"></asp:Literal></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>						
					</tbody>
				</table>
			</div>
		</div>
	</div>
</div>
</asp:Content>

