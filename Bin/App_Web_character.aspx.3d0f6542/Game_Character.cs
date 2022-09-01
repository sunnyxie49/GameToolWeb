using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;
using Rappelz.GMToolV3.BizDac;

public class Game_Character : Page, IRequiresSessionState
{
	protected Label Label1;

	protected Repeater rptChracter;

	protected Label Label2;

	protected DropDownList ddlSearchType;

	protected TextBox txtSearchText;

	protected CheckBox cbDeleteCharacter;

	protected CheckBox chkLike;

	protected Button btnSearch;

	protected Literal ltrTotalCnt;

	protected Repeater rptAvaterInfo;

	protected HtmlInputHidden hdChracter;

	private DataTable dt;

	private int accountid_no = 2002060;

	private int character_no = 2009092;

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
			server.SetServer(ref rptChracter);
			ddlSearchType.SelectedValue = base.Request.QueryString["searchType"];
			txtSearchText.Text = base.Request.QueryString["searchText"];
		}
		dt = new DataTable();
		string[] array = hdChracter.Value.Split(',');
		for (int i = 0; i < array.Length - 1; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				GetCharacter(array[i]);
			}
		}
		rptAvaterInfo.DataSource = dt;
		rptAvaterInfo.DataBind();
		ltrTotalCnt.Text = dt.Rows.Count.ToString();
	}

	private void GetCharacter(string server)
	{
		string selectedValue = ddlSearchType.SelectedValue;
		string text = txtSearchText.Text.Trim();
		bool @checked = cbDeleteCharacter.Checked;
		if (chkLike.Checked)
		{
			text += "%";
		}
		new StringBuilder(2000);
		int result = 0;
		int.TryParse(text, out result);
		long result2 = 0L;
		long.TryParse(text, out result2);
		string connectionString = libs.server.GetConnectionString(server);
		DataSet dataSet = null;
		using (GameLogBiz gameLogBiz = new GameLogBiz())
		{
			dataSet = gameLogBiz.GetCharacterInfo(connectionString, server, selectedValue, text, @checked);
		}
		try
		{
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				dt.Merge(dataSet.Tables[0]);
			}
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
		}
	}

	protected string BindServer(object item)
	{
		return server.GetServerName((string)DataBinder.Eval(item, "server"));
	}

	protected string BindName(object item)
	{
		_ = (string)DataBinder.Eval(item, "account");
		string text = (string)DataBinder.Eval(item, "name");
		string text2 = (string)DataBinder.Eval(item, "server");
		int num = (int)DataBinder.Eval(item, "sid");
		int num2 = (int)DataBinder.Eval(item, "account_id");
		if (text.Substring(0, 1) == "@")
		{
			try
			{
				text = string.Format("<span title='* name : {1}\n* delete_time : {2}'>{0}</span>", text.Substring(0, text.IndexOf(' ', 0)), text, ((DateTime)DataBinder.Eval(item, "delete_time")).ToString("yyyy-MM-dd HH:mm:ss"));
			}
			catch (Exception)
			{
			}
		}
		text = string.Format("<a href=\"javascript:;\" onclick=\"window.open('character_info.aspx?server={0}&character_id={1}&account_id={3}','character_name_info_{3}','left=50,top=50,width=1250,height=770,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=yes')\">{2}</a>", text2, num, text, num2);
		resource.GetJobResource(server.GetResourceServerNo((string)DataBinder.Eval(item, "server")), (int)DataBinder.Eval(item, "job"), out var job_name, out var job_icon);
		bool flag = false;
		if (DataBinder.Eval(item, "immoral_point").GetType() == typeof(float))
		{
			float num3 = (float)DataBinder.Eval(item, "immoral_point");
			if (num3 >= 100f)
			{
				flag = true;
			}
		}
		else
		{
			decimal d = (decimal)DataBinder.Eval(item, "immoral_point");
			if (d >= 100m)
			{
				flag = true;
			}
		}
		if (job_icon != "")
		{
			if (flag)
			{
				return string.Format("<img src='../img/icon/{1}' align='absmiddle' title='{2}'> <font color='red'>{0}</font>", text, job_icon, job_name);
			}
			return string.Format("<img src='../img/icon/{1}' align='absmiddle' title='{2}'> {0}", text, job_icon, job_name);
		}
		return $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{text}";
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

	protected string BindAuto(object item)
	{
		if ((int)DataBinder.Eval(item, "auto_used") == 1)
		{
			return "<span>AUTO</span>";
		}
		return "";
	}

	protected string BindLogin(object item)
	{
		if ((DateTime)DataBinder.Eval(item, "login_time") > (DateTime)DataBinder.Eval(item, "logout_time"))
		{
			return "login";
		}
		TimeSpan timeSpan = DateTime.Now - (DateTime)DataBinder.Eval(item, "logout_time");
		if (timeSpan.Days > 0)
		{
			return $"logout {timeSpan.Days} days ago";
		}
		if (timeSpan.Hours > 0)
		{
			return $"logout {timeSpan.Hours} hours ago";
		}
		return $"logout {timeSpan.Minutes} minutes ago";
	}

	protected string BindPlayTime(object item)
	{
		int num = (int)DataBinder.Eval(item, "play_time");
		int num2 = num / 86400;
		int num3 = num % 86400 / 3600;
		int num4 = num % 86400 % 3600 / 60;
		string text = "";
		text = ((num2 > 0) ? (num2.ToString().PadLeft(3, '_') + "d ") : "");
		text += ((num2 > 0 || num3 > 0) ? (num3.ToString().PadLeft(2, '_') + "h ") : "");
		text += ((num2 > 0 || num3 > 0 || num4 > 0) ? (num4.ToString().PadLeft(2, '_') + "m") : "");
		return string.Format("<span title='{1} sec'>{0}<span>", text.Replace("_", "&nbsp;"), num.ToString("#,###"));
	}

	protected string BindGuildName(object item)
	{
		if ((int)DataBinder.Eval(item, "guild_id") > 0)
		{
			string text = "";
			if ((int)DataBinder.Eval(item, "sid") == (int)DataBinder.Eval(item, "guild_leader_id"))
			{
				text = "<img src='../img/icon_guild_master.gif' align='absmiddle'>";
			}
			int num = (int)DataBinder.Eval(item, "guild_icon_size");
			string text2 = link.GuildPopup((string)DataBinder.Eval(item, "server"), (int)DataBinder.Eval(item, "guild_id"), (string)DataBinder.Eval(item, "guild_name"));
			if (num > 0)
			{
				string a = (string)DataBinder.Eval(item, "server");
				if (a == "program" || a == "devel")
				{
					return string.Format("<img src='{0}{1}' align='absmiddle'> {2} {3}", ConfigurationManager.AppSettings["guild_icon_url_test"], (string)DataBinder.Eval(item, "guild_icon"), text2, text);
				}
				return string.Format("<img src='{0}{1}' align='absmiddle'> {2} {3}", ConfigurationManager.AppSettings["guild_icon_url"], (string)DataBinder.Eval(item, "guild_icon"), text2, text);
			}
			return $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{text2} {text}";
		}
		return "";
	}

	protected void btnSearch_Click(object sender, EventArgs e)
	{
	}
}
