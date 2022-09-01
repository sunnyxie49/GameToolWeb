using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Game_Skin_skin_arena_list : UserControl
{
	protected Repeater rptAreanList;

	public DataTable dtArenaList;

	public string SelectedServer = "";

	public int ResourceDbNo;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		rptAreanList.DataSource = dtArenaList;
		rptAreanList.ItemDataBound += rptAreanList_ItemDataBound;
		rptAreanList.DataBind();
	}

	private void rptAreanList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
		{
			Literal literal = (Literal)e.Item.FindControl("ltraccount");
			Literal literal2 = (Literal)e.Item.FindControl("ltrname");
			Literal literal3 = (Literal)e.Item.FindControl("ltrarenapoint");
			Literal literal4 = (Literal)e.Item.FindControl("ltrarena_block_time");
			Literal literal5 = (Literal)e.Item.FindControl("ltrarena_penalty_count");
			Literal literal6 = (Literal)e.Item.FindControl("ltrarena_penalty_dec_time");
			Literal literal7 = (Literal)e.Item.FindControl("ltrarena_mvp_count");
			Literal literal8 = (Literal)e.Item.FindControl("ltrarena_record_0_0");
			Literal literal9 = (Literal)e.Item.FindControl("ltrarena_record_0_1");
			Literal literal10 = (Literal)e.Item.FindControl("ltrarena_record_1_0");
			Literal literal11 = (Literal)e.Item.FindControl("ltrarena_record_1_1");
			Literal literal12 = (Literal)e.Item.FindControl("ltrarena_record_2_0");
			Literal literal13 = (Literal)e.Item.FindControl("ltrarena_record_2_1");
			Literal literal14 = (Literal)e.Item.FindControl("ltralias");
			literal.Text = ((DataRowView)e.Item.DataItem)["account"].ToString();
			literal2.Text = ((DataRowView)e.Item.DataItem)["name"].ToString();
			literal3.Text = ((DataRowView)e.Item.DataItem)["arena_point"].ToString();
			literal4.Text = ((DataRowView)e.Item.DataItem)["arena_block_time"].ToString();
			literal5.Text = ((DataRowView)e.Item.DataItem)["arena_penalty_count"].ToString();
			literal6.Text = ((DataRowView)e.Item.DataItem)["arena_penalty_dec_time"].ToString();
			literal7.Text = ((DataRowView)e.Item.DataItem)["arena_mvp_count"].ToString();
			literal8.Text = ((DataRowView)e.Item.DataItem)["arena_record_0_0"].ToString();
			literal9.Text = ((DataRowView)e.Item.DataItem)["arena_record_0_1"].ToString();
			literal10.Text = ((DataRowView)e.Item.DataItem)["arena_record_1_0"].ToString();
			literal11.Text = ((DataRowView)e.Item.DataItem)["arena_record_1_1"].ToString();
			literal12.Text = ((DataRowView)e.Item.DataItem)["arena_record_2_0"].ToString();
			literal13.Text = ((DataRowView)e.Item.DataItem)["arena_record_2_1"].ToString();
			literal14.Text = ((DataRowView)e.Item.DataItem)["alias"].ToString();
		}
	}
}
