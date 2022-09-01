using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_Skin_skin_character_list : UserControl
{
	protected Repeater rptCharacterList;

	public DataTable dtCharacterList;

	private int list_no = 1;

	public int character_id;

	public string selected_server;

	public int ResourceDbNo;

	public bool seeDeletedCharacter;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		DataView dataView = new DataView();
		dataView.Table = dtCharacterList;
		if (!seeDeletedCharacter)
		{
			dataView.RowFilter = " delete_time > '9999-01-01'";
		}
		dataView.Sort = "sid";
		rptCharacterList.DataSource = dataView;
		rptCharacterList.ItemDataBound += rptCharacterList_ItemDataBound;
		rptCharacterList.DataBind();
	}

	private void rptCharacterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		HtmlControl htmlControl = (HtmlControl)e.Item.FindControl("trLine");
		if ((e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item) && (int)((DataRowView)e.Item.DataItem)["sid"] == character_id)
		{
			htmlControl.Style.Add("background-color", "#f0f0f0");
		}
	}

	protected string BindNo()
	{
		return list_no++.ToString();
	}

	protected string BindAccount(object item)
	{
		if ((int)DataBinder.Eval(item, "auto_used") == 1)
		{
			return string.Format("<font color='blue'>[AUTO]</font> {0}({1})", (string)DataBinder.Eval(item, "account"), (int)DataBinder.Eval(item, "account_id"));
		}
		return string.Format("{0}({1})", (string)DataBinder.Eval(item, "account"), (int)DataBinder.Eval(item, "account_id"));
	}

	protected string BindPermission(object item)
	{
		if ((int)DataBinder.Eval(item, "permission") != 0)
		{
			string text = "";
			return string.Format("<img src='../img/icon_gm.gif' align='absmiddle'>{0}", (int)DataBinder.Eval(item, "permission") switch
			{
				100 => "[S]",
				80 => "[A]",
				70 => "[B]",
				60 => "[C]",
				_ => string.Format("[{0}]", (int)DataBinder.Eval(item, "permission")),
			});
		}
		return "";
	}

	protected string BindName(object item)
	{
		string text = (string)DataBinder.Eval(item, "name");
		if (text.Substring(0, 1) == "@")
		{
			try
			{
				text = string.Format("<span title='* name : {1}\n* delete_time : {2}'>{0}</span>", text.Substring(0, text.IndexOf(' ', 0)), text, (DateTime)DataBinder.Eval(item, "delete_time"));
			}
			catch (Exception)
			{
			}
		}
		return string.Format("<a href='{0}?server={1}&account_id={2}&character_id={3}&see_delete_character={4}'>{5}</a>", base.Request.Path, selected_server, (int)DataBinder.Eval(item, "account_id"), (int)DataBinder.Eval(item, "sid"), seeDeletedCharacter, text);
	}

	protected string BindJob(object item)
	{
		DataRow jobResource = resource.GetJobResource(ResourceDbNo, (int)DataBinder.Eval(item, "job"));
		string arg = "";
		string arg2 = "";
		if (jobResource != null)
		{
			arg = jobResource["icon_file_name"].ToString();
			arg = ((!File.Exists(base.Server.MapPath(string.Format("../img/icon/job/{0}.jpg", jobResource["icon_file_name"])))) ? "&nbsp;&nbsp;" : string.Format("<img src='../img/icon/job/{0}.jpg' align='absmiddle'>", jobResource["icon_file_name"]));
			arg2 = (string)jobResource["value"];
		}
		return $"{arg} {arg2}";
	}

	protected string BindRace(object item)
	{
		return resource.GetRaceName((int)DataBinder.Eval(item, "race"));
	}

	protected string BindSex(object item)
	{
		if ((int)DataBinder.Eval(item, "sex") != 2)
		{
			return "Female";
		}
		return "Male";
	}

	protected string BindExp(object item)
	{
		return ((long)DataBinder.Eval(item, "exp")).ToString("#,##0");
	}

	protected string BindTotalJP(object item)
	{
		return ((long)DataBinder.Eval(item, "total_jp")).ToString("#,##0");
	}

	protected string BindGold(object item)
	{
		return ((long)DataBinder.Eval(item, "gold")).ToString("#,##0");
	}

	protected string BindCreateTime(object item)
	{
		return ((DateTime)DataBinder.Eval(item, "create_time")).ToString("yyyy'-'MM'-'dd");
	}

	protected string BindLoginCheck(object item)
	{
		if (!((DateTime)DataBinder.Eval(item, "login_time") > (DateTime)DataBinder.Eval(item, "logout_time")))
		{
			return "X";
		}
		return "Login";
	}
}
