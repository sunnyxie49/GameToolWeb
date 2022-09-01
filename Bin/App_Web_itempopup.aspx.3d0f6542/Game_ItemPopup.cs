using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_ItemPopup : Page, IRequiresSessionState
{
	protected DropDownList ddlItemGroup;

	protected TextBox txtItemName;

	protected Button Button1;

	protected Repeater rptitempopup;

	protected HtmlInputHidden hdnWhere;

	protected HtmlForm form1;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			common.MsgBox("没有权限", "关闭");
		}
		else if (!base.IsPostBack)
		{
			int num = common.ToInt(base.Request.QueryString["item_code"]);
			if (num != 0)
			{
				int resource_server_no = (int)server.GetServerInfo(base.Request.QueryString["server"])["resource_id"];
				DataView dataView = new DataView(resource.GetResourceTable(resource_server_no, "ItemResource"));
				dataView.RowFilter = $"id = {num}";
				rptitempopup.DataSource = dataView;
				rptitempopup.DataBind();
			}
		}
	}

	protected string BindItemType(object item)
	{
		string result = resource.GetItemType((int)DataBinder.Eval(item, "type"));
		string itemGroup = resource.GetItemGroup((int)DataBinder.Eval(item, "group"));
		string itemClass = resource.GetItemClass((int)DataBinder.Eval(item, "class"));
		if (itemGroup != "Others")
		{
			result = resource.GetItemGroup((int)DataBinder.Eval(item, "group"));
		}
		if (itemClass != "Others")
		{
			result = resource.GetItemClass((int)DataBinder.Eval(item, "class"));
		}
		return result;
	}

	protected string BindItemName(object item)
	{
		int num = (int)DataBinder.Eval(item, "group");
		if (num >= 1 && num <= 7)
		{
			return string.Format(" [{1}] {0}", (string)DataBinder.Eval(item, "value"), resource.GetItemRank((int)DataBinder.Eval(item, "rank")));
		}
		return (string)DataBinder.Eval(item, "value");
	}

	protected string BindItemInfo(object item)
	{
		int item_code = (int)DataBinder.Eval(item, "id");
		int item_level = (int)DataBinder.Eval(item, "level");
		int item_enhance = (int)DataBinder.Eval(item, "enhance");
		return resource.GetItemToolTip(0, item_code, item_level, item_enhance, 300);
	}

	private void SearchItem()
	{
		int resource_server_no = (int)server.GetServerInfo(base.Request.QueryString["server"])["resource_id"];
		DataView dataView = new DataView(resource.GetResourceTable(resource_server_no, "ItemResource"));
		if (ddlItemGroup.SelectedValue != "all")
		{
			dataView.RowFilter = $"group = {ddlItemGroup.SelectedValue} and value like '%{txtItemName.Text}%'";
		}
		else
		{
			dataView.RowFilter = $"value like '%{txtItemName.Text}%'";
		}
		dataView.Sort = "id";
		rptitempopup.DataSource = dataView;
		rptitempopup.DataBind();
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		SearchItem();
	}
}
