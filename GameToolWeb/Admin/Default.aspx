<%@ page title="国服GM工具" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Admin_Default, App_Web_default.aspx.fdf7a39c" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
<div class="container-fluid">
	<div class="row-fluid">
		<div class="span12">
			<div class="page-header">
				<h2>Admin Account List</h2>
			</div>

			<div class="halfWidth">
				<table class="table table-bordered centerTable">
					<colgroup>
						<col style="width:10%" />
						<col style="width:20%" />
						<col style="width:20%" />
						<col style="width:30%" />
						<col style="width:20%" />
					</colgroup>
					<thead>
						<tr>
							<th>sid</th>
							<th>account</th>
							<th>Name</th>
							<th>Group</th>
							<th>Authority</th>
						</tr>
					</thead>
					<tbody>
                        <asp:Repeater ID="rptAdmin" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("sid")%></td>
                                    <td><%#BindAccount(Container.DataItem)%></td>
                                    <td><%#Eval("name")%></td>
                                    <td><%#Eval("team")%></td>
                                    <td><%#Eval("grade")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>						
					</tbody>
				</table>
			</div>
            <div class="pagination pagination-centered">                    
				<ul>
                    <asp:Literal ID="ltrPage" runat="server"></asp:Literal>					
				</ul>
			</div>            
		</div>
	</div>
</div>
</asp:Content>

