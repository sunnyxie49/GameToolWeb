<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Admin_authority_admin, App_Web_authority_admin.aspx.fdf7a39c" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>관리자 권한</h2>
			</div>
			<div class="halfWidth">
				<p>Admin : 					
                    <asp:DropDownList ID="ddlAdmin" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnGetAuthority" runat="server" class="btn btn-primary" Text="Load" OnClick="btnGetAuthority_Click" />
	                <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save" OnClick="Button1_Click" />
	                <asp:Button ID="Button2" runat="server" class="btn btn-primary" OnClick="Button2_Click" Text="Apply" />					
					<asp:Button ID="Button3" runat="server" class="btn btn-danger" OnClick="Button3_Click" Text="Update Authority" meta:resourcekey="btnAuthorityResource1" />
				</p>
				<table class="table table-bordered lastCenter">
					<colgroup>
						<col style="width:25%" />
						<col style="width:15%" />
						<col style="width:30%" />
						<col style="width:15%" />
						<col style="width:15%" />
					</colgroup>
					<thead>
						<tr>
							<th>Code</th>
							<th>Category</th>
							<th>Info</th>
							<th>Grade Authority</th>
							<th>Person Authority</th>
						</tr>
					</thead>
					<tbody>
                        <asp:Repeater ID="rptAdmin" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><asp:Literal ID="ltrCode" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltrCategory" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltrInfo" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltrGrade" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltrPerson" runat="server"></asp:Literal></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
						<%--<tr>
							<td>game/default.aspx</td>
							<td>페이지접근권한</td>
							<td>아바타검색페이지</td>
							<td class="centerAlign"><input type="checkbox" /></td>
							<td class="centerAlign"><input type="checkbox" /></td>
						</tr>--%>
					</tbody>
				</table>
			</div>
		</div>
	</div>
</div>
</asp:Content>

