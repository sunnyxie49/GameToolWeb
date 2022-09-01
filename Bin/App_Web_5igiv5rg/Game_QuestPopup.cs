using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_QuestPopup : Page, IRequiresSessionState
{
	private int resource_id;

	protected DropDownList ddlSearchType;

	protected TextBox txtSearch;

	protected Button Button1;

	protected Repeater rptQuestPopup;

	protected HtmlForm form1;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			ddlSearchType.SelectedValue = "code";
			txtSearch.Text = base.Request.QueryString["quest_code"];
			SearchQuest();
		}
	}

	private void SearchQuest()
	{
		DataView dataView = new DataView(resource.GetResourceTable(resource_id, "QuestResource"));
		if (txtSearch.Text != "")
		{
			if (ddlSearchType.SelectedValue == "code")
			{
				dataView.RowFilter = $"id = {txtSearch.Text}";
			}
			else if (ddlSearchType.SelectedValue == "name")
			{
				dataView.RowFilter = $"value like '{txtSearch.Text}%'";
			}
		}
		dataView.Sort = "id";
		rptQuestPopup.DataSource = dataView;
		rptQuestPopup.DataBind();
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		SearchQuest();
	}

	protected string BindSummary(object item)
	{
		int code = (int)DataBinder.Eval(item, "text_id_summary");
		return resource.GetStringResource(3, code);
	}

	protected string BindStatus(object item)
	{
		int code = (int)DataBinder.Eval(item, "text_id_status");
		return resource.GetStringResource(3, code);
	}

	protected string BindExp(object item)
	{
		return ((long)DataBinder.Eval(item, "exp")).ToString("#,##0");
	}

	protected string BindJP(object item)
	{
		return ((int)DataBinder.Eval(item, "jp")).ToString("#,##0");
	}

	protected string BindGold(object item)
	{
		return ((int)DataBinder.Eval(item, "gold")).ToString("#,##0");
	}

	protected string BindReward(object item)
	{
		int code = (int)DataBinder.Eval(item, "default_reward_id");
		int level = (int)DataBinder.Eval(item, "default_reward_level");
		int cnt = (int)DataBinder.Eval(item, "default_reward_quantity");
		return GetItemInfo(code, level, cnt);
	}

	protected string BindReward(object item, int no)
	{
		int code = (int)DataBinder.Eval(item, $"optional_reward_id{no}");
		int level = (int)DataBinder.Eval(item, $"optional_reward_level{no}");
		int cnt = (int)DataBinder.Eval(item, $"optional_reward_quantity{no}");
		return GetItemInfo(code, level, cnt);
	}

	private string GetItemInfo(int code, int level, int cnt)
	{
		if (code != 0)
		{
			DataRow itemResource = resource.GetItemResource(resource_id, code);
			if (itemResource == null)
			{
				return $"No Resource [{code}] LV {level}, Count : {cnt} ";
			}
			if ((int)itemResource["group"] >= 1 && (int)itemResource["group"] <= 7)
			{
				return string.Format("{0} Lv {1}, Count : {2}  ", itemResource["value"], level, cnt);
			}
			return string.Format("{0}, Count : {2}", itemResource["value"], level, cnt);
		}
		return "&nbsp;";
	}
}
