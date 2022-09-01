using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;

public class Game_Skin_skin_title_list : UserControl
{
	protected Repeater rptTitleList;

	public DataTable dtTitleList;

	public string SelectedServer = "";

	public int ResourceDbNo;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		rptTitleList.DataSource = dtTitleList;
		rptTitleList.ItemDataBound += rptTitleList_ItemDataBound;
		rptTitleList.DataBind();
	}

	private void rptTitleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
		{
			Literal literal = (Literal)e.Item.FindControl("ltrcode");
			Literal literal2 = (Literal)e.Item.FindControl("ltrstatus");
			literal.Text = resource.GetTitleResource(server.GetResourceServerNo(base.Request.QueryString["server"]), (int)((DataRowView)e.Item.DataItem)["code"]);
			literal2.Text = ((DataRowView)e.Item.DataItem)[3].ToString();
		}
	}
}
