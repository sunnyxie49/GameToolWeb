<%@ page language="C#" autoeventwireup="true" inherits="Game_item_insert_useSid, App_Web_item_insert_usesid.aspx.3d0f6542" %><!DOCTYPE html>

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
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="table table-bordered centerTable">
	        <colgroup>
		        <col style="width:50%" />
		        <col style="width:50%" />
	        </colgroup>
	        <thead>
		        <tr>
			        <th>item Sid</th>
			        <th>item Code</th>
		        </tr>
	        </thead>
	        <tbody>
		        <tr>
			        <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
			        <td><asp:TextBox ID="txtCode" runat="server"></asp:TextBox></td>
		        </tr>
		        <tr>
			        <td colspan="2"><asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" onclick="Button1_Click" /> <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary disabled" Text="Give item" onclick="Button2_Click" Enabled="false" /></td>
		        </tr>
	        </tbody>
        </table>

        <h4>Owner Info</h4>
        <table class="table table-bordered centerTable">
	        <colgroup>
		        <col style="width:30%" />
		        <col style="width:*" />
	        </colgroup>
	        <thead>
		        <tr>
			        <th>Server</th>
			        <th>Owner</th>
		        </tr>
	        </thead>
	        <tbody>
		        <tr>
			        <td><asp:Literal id="ltrServer" runat="server"></asp:Literal></td>
			        <td><asp:Literal id="ltrOwner" runat="server"></asp:Literal></td>
		        </tr>
	        </tbody>
        </table>

        <h4>Item Info</h4>
        <table class="table table-bordered centerTable">
	        <colgroup>
		        <col style="width:9%" />
		        <col style="width:*" />
		        <col style="width:8%" />
		        <col style="width:16%" />
	        </colgroup>
	        <thead>
		        <tr>
			        <th>sid</th>
			        <th>Name</th>
			        <th>Count</th>
			        <th>Class</th>
		        </tr>
	        </thead>
	        <tbody>
		        <tr>
			        <td><asp:Literal id="ltrItemSid" runat="server"></asp:Literal></td>
			        <td><asp:Literal id="ltrItemName" runat="server"></asp:Literal></td>
			        <td><asp:Literal id="ltrItemCnt" runat="server"></asp:Literal></td>
			        <td><asp:Literal id="ltrItemType" runat="server"></asp:Literal></td>
		        </tr>
	        </tbody>
        </table>

        <table class="table table-bordered">
	        <thead>
		        <tr>
			        <th>*Info</th>
		        </tr>
	        </thead>
	        <tbody>
		        <tr>
			        <td>item code : <asp:Literal ID="ltrCode" runat="server"></asp:Literal> state : <asp:Literal ID="ltrState" runat="server"></asp:Literal><asp:HiddenField ID="hdnState" runat="server" /> Create Time : <asp:Literal ID="ltrCreateTime" runat="server"></asp:Literal></td>
                    <br />
                    <asp:Literal id="ltrItemInfo" runat="server"></asp:Literal>
		        </tr>
	        </tbody>
        </table>
    </form>
</body>
</html>
