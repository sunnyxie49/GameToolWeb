using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;

public class Game_Skin_skin_quest_list : UserControl
{
	public DataTable dtQuestList;

	public string SelectedServer = "";

	public int ResourceDbNo;

	protected Repeater rptQuestList;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		rptQuestList.DataSource = dtQuestList;
		rptQuestList.ItemDataBound += rptQuestList_ItemDataBound;
		rptQuestList.DataBind();
	}

	private void rptQuestList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
		{
			Literal literal = (Literal)e.Item.FindControl("ltrid");
			Literal literal2 = (Literal)e.Item.FindControl("ltrQuestID");
			Literal literal3 = (Literal)e.Item.FindControl("ltrQuestName");
			Literal literal4 = (Literal)e.Item.FindControl("ltrStatus1");
			Literal literal5 = (Literal)e.Item.FindControl("ltrStatus2");
			Literal literal6 = (Literal)e.Item.FindControl("ltrStatus3");
			Literal literal7 = (Literal)e.Item.FindControl("ltrprogress");
			literal.Text = ((int)((DataRowView)e.Item.DataItem)["id"]).ToString();
			literal2.Text = link.QuestPopup(SelectedServer, (int)((DataRowView)e.Item.DataItem)["code"], ((int)((DataRowView)e.Item.DataItem)["code"]).ToString());
			literal4.Text = (((int)((DataRowView)e.Item.DataItem)["status1"] == 0) ? "-" : ((DataRowView)e.Item.DataItem)["status1"].ToString());
			literal5.Text = (((int)((DataRowView)e.Item.DataItem)["status2"] == 0) ? "-" : ((DataRowView)e.Item.DataItem)["status2"].ToString());
			literal6.Text = (((int)((DataRowView)e.Item.DataItem)["status3"] == 0) ? "-" : ((DataRowView)e.Item.DataItem)["status3"].ToString());
			literal7.Text = ((int)((DataRowView)e.Item.DataItem)["progress"]).ToString();
			DataRow questResource = resource.GetQuestResource(ResourceDbNo, (int)((DataRowView)e.Item.DataItem)["code"]);
			literal3.Text = link.QuestPopup(SelectedServer, (int)((DataRowView)e.Item.DataItem)["code"], (string)questResource["value"]);
		}
	}
}
