using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_Skin_skin_shop_list : UserControl
{
	protected Repeater rptLeftItem;

	protected Repeater rptUsedItem;

	public DataTable dtShopList;

	public string SelectedServer = "";

	public int ResourceDbNo;

	public int account_id;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		Init_Page();
	}

	protected void Init_Page()
	{
		DataView dataView = new DataView(dtShopList);
		dataView.RowFilter = $"rest_item_count > 0 and valid_time >= '{DateTime.Now}'";
		rptLeftItem.DataSource = dataView;
		rptLeftItem.ItemDataBound += rptCommon_ItemDataBound;
		rptLeftItem.DataBind();
		dataView.RowFilter = $"rest_item_count = 0 or valid_time < '{DateTime.Now}'";
		rptUsedItem.DataSource = dataView;
		rptUsedItem.ItemDataBound += rptCommon_ItemDataBound;
		rptUsedItem.DataBind();
	}

	private void rptCommon_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
		{
			return;
		}
		Literal literal = (Literal)e.Item.FindControl("ltrsid");
		Literal literal2 = (Literal)e.Item.FindControl("ltrName");
		Literal literal3 = (Literal)e.Item.FindControl("ltrBougthTime");
		Literal literal4 = (Literal)e.Item.FindControl("ltrCheckTime");
		Literal literal5 = (Literal)e.Item.FindControl("ltrTakenTime");
		Literal literal6 = (Literal)e.Item.FindControl("ltrBoughtCount");
		Literal literal7 = (Literal)e.Item.FindControl("ltrCount");
		Literal literal8 = (Literal)e.Item.FindControl("ltrValidTime");
		Literal literal9 = (Literal)e.Item.FindControl("ltrBlank");
		HtmlControl htmlControl = (HtmlControl)e.Item.FindControl("trLine");
		DataRowView dataRowView = (DataRowView)e.Item.DataItem;
		literal.Text = dataRowView["sid"].ToString();
		literal2.Text = GetItemName((int)dataRowView["item_code"]);
		literal3.Text = ((DateTime)dataRowView["bought_time"]).ToString("yyyy-MM-dd HH:mm:ss");
		if (dataRowView["taken_time"] != null && ((DateTime)dataRowView["taken_time"]).Year != 9999)
		{
			literal5.Text = ((DateTime)dataRowView["taken_time"]).ToString("yyyy-MM-dd HH:mm:ss");
		}
		else
		{
			literal5.Text = "not received";
		}
		int result = 0;
		int result2 = 0;
		if (int.TryParse(dataRowView["item_count"].ToString(), out result))
		{
			literal6.Text = $"{result}";
		}
		int.TryParse(dataRowView["rest_item_count"].ToString(), out result2);
		if (result > 0)
		{
			literal7.Text = "<b>" + $"{result2}" + "</b>";
		}
		else
		{
			literal7.Text = $"{result2}";
		}
		if (dataRowView["valid_time"] != null && ((DateTime)dataRowView["valid_time"]).Year < 2050)
		{
			if ((DateTime)dataRowView["valid_time"] >= DateTime.Now)
			{
				literal8.Text = "<b>" + ((DateTime)dataRowView["valid_time"]).ToString("yyyy-MM-dd HH:mm:ss") + "</b>";
			}
			else
			{
				literal8.Text = ((DateTime)dataRowView["valid_time"]).ToString("yyyy-MM-dd HH:mm:ss");
			}
		}
		else
		{
			literal8.Text = "";
			literal9.Text = "";
		}
		try
		{
			if ((bool)dataRowView["confirmed"])
			{
				htmlControl.Style.Add("background-color", "#eeeeee");
				literal4.Text = ((DateTime)dataRowView["confirmed_time"]).ToString("yyyy-MM-dd HH:mm:ss");
			}
			else
			{
				literal4.Text = "not confirmed";
			}
		}
		catch (Exception)
		{
		}
	}

	protected string GetItemName(int item_code)
	{
		return resource.GetItemName(ResourceDbNo, item_code, 1, 0, 300);
	}
}
