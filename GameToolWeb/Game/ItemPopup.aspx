<%@ page language="C#" autoeventwireup="true" inherits="Game_ItemPopup, App_Web_itempopup.aspx.3d0f6542" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <meta charset="utf-8" />
    <link rel="stylesheet" href="/common/css/bootstrap.css" />
    <!-- <link rel="stylesheet" href="css/bootstrap-responsive.min.css" /> -->
    <link rel="stylesheet" href="/common/css/customize.css" />
    <style>
	    body{padding:5px;}
    </style>
    <script type="text/javascript" src="/Common/js/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/common/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function SetItemCode(code) {
            window.opener.SetItemCode(document.form1.hdnWhere.value, code);
            self.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="table table-bordered">
	    <colgroup>
		    <col style="width:30%" />
		    <col style="width:70%" />
	    </colgroup>
	    <tr>
		    <th>Server</th>
		    <td>
			    <asp:dropdownlist id="ddlItemGroup" runat="server">
					<asp:ListItem Value="all" Selected="True">All</asp:ListItem>
					<asp:ListItem Value="0">Others</asp:ListItem>
					<asp:ListItem Value="1">Weapon</asp:ListItem>
					<asp:ListItem Value="2">Costume</asp:ListItem>
					<asp:ListItem Value="3">Shield</asp:ListItem>
					<asp:ListItem Value="4">Helmet</asp:ListItem>
					<asp:ListItem Value="5">Gloves</asp:ListItem>
					<asp:ListItem Value="6">Boots</asp:ListItem>
					<asp:ListItem Value="7">Belt</asp:ListItem>
					<asp:ListItem Value="8">Mantle</asp:ListItem>
					<asp:ListItem Value="9">Accessory</asp:ListItem>
					<asp:ListItem Value="10">Skill Card</asp:ListItem>
					<asp:ListItem Value="11">Unit Card</asp:ListItem>
					<asp:ListItem Value="12">Spell Card</asp:ListItem>
					<asp:ListItem Value="13">Creature Card</asp:ListItem>
					<asp:ListItem Value="14">Hair</asp:ListItem>
					<asp:ListItem Value="15">Face</asp:ListItem>
					<asp:ListItem Value="16">Underwear</asp:ListItem>
					<asp:ListItem Value="21">Strike Cube</asp:ListItem>
					<asp:ListItem Value="22">Defense Cube</asp:ListItem>
					<asp:ListItem Value="23">Skill Cube</asp:ListItem>
					<asp:ListItem Value="80">Cash Item</asp:ListItem>
					<asp:ListItem Value="93">Soul Stone</asp:ListItem>
					<asp:ListItem Value="94">Item Package</asp:ListItem>
					<asp:ListItem Value="95">Generic Stone</asp:ListItem>
					<asp:ListItem Value="96">Skill Material</asp:ListItem>
					<asp:ListItem Value="97">Combine Material</asp:ListItem>
					<asp:ListItem Value="98">Bullet</asp:ListItem>
					<asp:ListItem Value="99">Consumption</asp:ListItem>
					<asp:ListItem Value="110">Costume</asp:ListItem>
					<asp:ListItem Value="120">Riding</asp:ListItem>
				</asp:dropdownlist>
			    <asp:TextBox id="txtItemName" runat="server"></asp:TextBox> <asp:Button id="Button1" runat="server" Text="Search" CssClass="btn btn-primary" onclick="Button1_Click"></asp:Button>
		    </td>
	    </tr>
    </table>

    <table class="table table-bordered centerTable">
	    <colgroup>
		    <col style="width:9%" />
		    <col style="width:9%" />
		    <col style="width:9%" />
		    <col style="width:*" />
		    <col style="width:16%" />
	    </colgroup>
	    <thead>
		    <tr>
			    <th>Code</th>
			    <th>Type</th>
			    <th>Name</th>
			    <th>Info</th>
			    <th>Choice</th>
		    </tr>
	    </thead>
	    <tbody>
            <asp:Repeater ID="rptitempopup" runat="server">
                <ItemTemplate>
                    <tr>
			            <td><%# Eval("id") %></td>
			            <td><%# BindItemType(Container.DataItem) %></td>
			            <td><%# BindItemName(Container.DataItem) %></td>
			            <td class="leftAlign">
                            <%# BindItemInfo(Container.DataItem) %>				            
			            </td>
			            <td><input class="btn btn-primary" type="button" onclick="SetItemCode(<%# Eval("id")%>)" value="Choice" /></td>
		            </tr>
                </ItemTemplate>
            </asp:Repeater>		    
	    </tbody>
    </table>
    <input id="hdnWhere" type="hidden" name="Hidden1" runat="server" />
    </form>
</body>
</html>
