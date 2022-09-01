<%@ page title="" language="C#" masterpagefile="~/MasterPage/MasterPage.master" autoeventwireup="true" inherits="Log_chat_log, App_Web_chat_log.aspx.564c629f" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPMainPage" Runat="Server">
    <asp:DropDownList ID="ddlServer" runat="server">
	</asp:DropDownList>
	<asp:DropDownList ID="ddlMonth" runat="server">
	</asp:DropDownList>
	Time <asp:TextBox ID="txtLogDate" runat="server"></asp:TextBox>
	Account : <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
	Character : <asp:TextBox ID="txtCharacter" runat="server"></asp:TextBox>
	<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <hr />
	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField HeaderText="log_date" DataField="log_date"/> 
			<asp:TemplateField HeaderText="chat_type"><ItemTemplate><%# BindChatType(Container.DataItem)%></ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField HeaderText="sender_account" DataField="sender_account"/> 
			<asp:BoundField HeaderText="sender_character" DataField="sender_character"/> 
			<asp:BoundField HeaderText="receiver_account" DataField="receiver_account"/> 
			<asp:BoundField HeaderText="receiver_character" DataField="receiver_character"/> 
			<asp:BoundField HeaderText="chat" DataField="chat"/>			
		</Columns>	
	</asp:GridView>
</asp:Content>

