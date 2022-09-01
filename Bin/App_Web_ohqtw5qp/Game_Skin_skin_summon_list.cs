using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;

public class Game_Skin_skin_summon_list : UserControl
{
	public DataTable dtSummonList;

	public string SelectedServer = "";

	public int ResourceDbNo;

	public int account_id;

	protected Repeater rptSummonList;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		rptSummonList.DataSource = dtSummonList;
		rptSummonList.ItemDataBound += rptCharacterList_ItemDataBound;
		rptSummonList.DataBind();
	}

	private void rptCharacterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
		{
			Literal literal = (Literal)e.Item.FindControl("ltrsid");
			Literal literal2 = (Literal)e.Item.FindControl("ltrType");
			Literal literal3 = (Literal)e.Item.FindControl("ltrName");
			Literal literal4 = (Literal)e.Item.FindControl("ltrTrans");
			Literal literal5 = (Literal)e.Item.FindControl("ltrCard");
			Literal literal6 = (Literal)e.Item.FindControl("ltrLV");
			Literal literal7 = (Literal)e.Item.FindControl("ltrJLV");
			Literal literal8 = (Literal)e.Item.FindControl("ltrfp");
			Literal literal9 = (Literal)e.Item.FindControl("ltrExp");
			Literal literal10 = (Literal)e.Item.FindControl("ltrJP");
			Literal literal11 = (Literal)e.Item.FindControl("ltrSP");
			Literal literal12 = (Literal)e.Item.FindControl("ltrHP");
			Literal literal13 = (Literal)e.Item.FindControl("ltrMP");
			Literal literal14 = (Literal)e.Item.FindControl("ltrRemain");
			int num = (int)((DataRowView)e.Item.DataItem)["sid"];
			int num2 = (int)((DataRowView)e.Item.DataItem)["code"];
			literal.Text = num.ToString();
			DataRow summonResource = resource.GetSummonResource(ResourceDbNo, num2);
			if (summonResource == null)
			{
				literal2.Text = $"SummonCode : {num2}";
				return;
			}
			literal2.Text = summonResource["value"].ToString();
			literal3.Text = string.Format("<a href='character_info_summon.aspx?server={0}&account_id={1}&character_id={2}&summon_id={3}'>{4}</a>", SelectedServer, account_id, ((DataRowView)e.Item.DataItem)["owner_id"], ((DataRowView)e.Item.DataItem)["sid"], ((DataRowView)e.Item.DataItem)["name"]);
			literal4.Text = ((DataRowView)e.Item.DataItem)["transform"].ToString();
			literal5.Text = ((DataRowView)e.Item.DataItem)["card_uid"].ToString();
			literal6.Text = ((DataRowView)e.Item.DataItem)["lv"].ToString();
			literal7.Text = ((DataRowView)e.Item.DataItem)["jlv"].ToString();
			literal8.Text = ((DataRowView)e.Item.DataItem)["fp"].ToString();
			literal9.Text = ((long)((DataRowView)e.Item.DataItem)["exp"]).ToString("#,##0");
			literal10.Text = ((int)((DataRowView)e.Item.DataItem)["jp"]).ToString("#,##0");
			literal11.Text = ((int)((DataRowView)e.Item.DataItem)["SP"]).ToString("#,##0");
			literal12.Text = ((int)((DataRowView)e.Item.DataItem)["HP"]).ToString("#,##0");
			literal13.Text = ((int)((DataRowView)e.Item.DataItem)["MP"]).ToString("#,##0");
			literal14.Text = ((int)((DataRowView)e.Item.DataItem)["remain_time"]).ToString("#,##0");
		}
	}
}
