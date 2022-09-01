using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_ItemCount : Page, IRequiresSessionState
{
	protected DropDownList ddlServer;

	protected HtmlGenericControl idServerNm;

	protected TextBox txtItemCode;

	protected Button btnSearch;

	protected Literal ltrItemCount;

	protected Repeater rptItemList;

	private int itemCode;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		if (!admin.Authority())
		{
			common.MsgBox("没有权限", "返回");
		}
		if (!base.IsPostBack)
		{
			server.SetServer(ref ddlServer, base.Request.QueryString["server"]);
			ddlServer.Items.Insert(0, new ListItem("--Select Server--", "none"));
			ddlServer.Items.Insert(1, new ListItem("All Server", "all"));
			idServerNm.Style.Add("display", "none");
		}
	}

	protected void btnSearch_Click(object sender, EventArgs e)
	{
		if (ddlServer.SelectedValue == "none" || !int.TryParse(txtItemCode.Text, out itemCode))
		{
			return;
		}
		if (ddlServer.SelectedValue == "all")
		{
			ListItemCollection listItemCollection = new ListItemCollection();
			foreach (ListItem item in ddlServer.Items)
			{
				if (!(item.Value.ToLower() == "none") && !(item.Value.ToLower() == "all"))
				{
					listItemCollection.Add(item);
				}
			}
			rptItemList.DataSource = listItemCollection;
		}
		else
		{
			rptItemList.DataSource = new ListItem[1]
			{
				ddlServer.SelectedItem
			};
		}
		rptItemList.ItemDataBound += rptItemList_ItemDataBound;
		rptItemList.DataBind();
	}

	private void rptItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
		{
			return;
		}
		ListItem listItem = (ListItem)e.Item.DataItem;
		Literal literal = (Literal)e.Item.FindControl("ltrItemNm");
		Literal literal2 = (Literal)e.Item.FindControl("ltrServerNm");
		Literal literal3 = (Literal)e.Item.FindControl("ltrCode");
		Literal literal4 = (Literal)e.Item.FindControl("ltrCount");
		using SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(listItem.Value));
		DataTable dataTable = new DataTable();
		try
		{
			SqlCommand sqlCommand = new SqlCommand();
			int resourceServerNo = server.GetResourceServerNo(ddlServer.SelectedValue);
			DataRow itemResource = resource.GetItemResource(resourceServerNo, itemCode);
			literal2.Text = listItem.Value;
			literal.Text = (string)itemResource["value"];
			sqlConnection.Open();
			sqlCommand.Connection = sqlConnection;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandText = "gmtool_GetServer_ItemList";
			sqlCommand.Parameters.Add("@Code", SqlDbType.Int).Value = itemCode;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			sqlDataAdapter.Fill(dataTable);
			if (dataTable != null && dataTable.Rows.Count >= 1)
			{
				literal3.Text = dataTable.Rows[0]["code"].ToString();
				literal4.Text = dataTable.Rows[0]["cnt"].ToString();
			}
		}
		catch (Exception)
		{
			literal3.Text = "No Server";
			literal4.Text = "No Server";
		}
	}
}
