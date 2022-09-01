using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_guild_info : Page, IRequiresSessionState
{
	protected Literal ltrAllianceName;

	protected Repeater rptGuildInfo;

	protected Literal ltrAllianceBlockTime;

	protected Button btnDeleteGuildIcon;

	protected TextBox txtIcon;

	protected TextBox txtIconSize;

	protected Button btnChangeGuildIcon;

	protected Literal ltrGuildNameBefore;

	protected TextBox txtGuildName;

	protected Button btnChangeGuildName;

	protected Literal ltrGuildLeaderBefore;

	protected TextBox txtGuildLeaderSid;

	protected Button btnChangeGuildLeader;

	protected Literal ltrGuildName;

	protected Repeater rptGuildmember;

	protected HtmlForm form1;

	private string selected_server;

	private int guild_id;

	private int lead_guild_id;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.Authority("game/guild.aspx"))
		{
			common.MsgBox("没有权限", "关闭");
		}
		if (!admin.Authority(selected_server, "gmtool_modify_guild_info"))
		{
			btnChangeGuildLeader.Enabled = false;
			btnChangeGuildName.Enabled = false;
			btnDeleteGuildIcon.Enabled = false;
			btnChangeGuildIcon.Enabled = false;
		}
		else
		{
			btnChangeGuildLeader.Enabled = true;
			btnChangeGuildName.Enabled = true;
			btnDeleteGuildIcon.Enabled = true;
			btnChangeGuildIcon.Enabled = true;
		}
		GetData();
	}

	private void GetData()
	{
		selected_server = base.Request.QueryString["server"];
		guild_id = common.ToInt(base.Request.QueryString["guild_id"]);
		server.GetResourceServerNo(selected_server);
		if (guild_id <= 0)
		{
			return;
		}
		SqlConnection connection = new SqlConnection(server.GetConnectionString(selected_server));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = connection;
		sqlCommand.CommandText = "gmtool_GetGuild_info";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Add("@sid", SqlDbType.Int).Value = guild_id;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		sqlDataAdapter.Fill(dataSet);
		if (dataSet.Tables[0].Rows.Count > 0)
		{
			lead_guild_id = (int)dataSet.Tables[0].Rows[0]["lead_guild_id"];
			ltrAllianceName.Text = string.Format("* {0}'s Alliance Guild <br \\>", dataSet.Tables[0].Rows[0]["name"].ToString());
		}
		if (dataSet.Tables[1].Rows.Count == 0)
		{
			common.MsgBox("No Guild Info", "close");
			return;
		}
		rptGuildInfo.DataSource = dataSet.Tables[1];
		rptGuildInfo.DataBind();
		rptGuildmember.DataSource = dataSet.Tables[2];
		rptGuildmember.DataBind();
		DataRow[] array = dataSet.Tables[1].Select("sid = " + guild_id);
		if (array.Length <= 0)
		{
			return;
		}
		if (!base.IsPostBack)
		{
			txtGuildName.Text = array[0]["name"].ToString();
			txtGuildLeaderSid.Text = array[0]["leader_id"].ToString();
		}
		ltrGuildNameBefore.Text = array[0]["name"].ToString();
		ltrGuildLeaderBefore.Text = array[0]["leader_id"].ToString();
		long num = (long)array[0]["alliance_block_time"];
		if (num > 0)
		{
			int result = 0;
			int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
			DateTime t = new DateTime(1970, 1, 1, 0, 0, 0).AddHours(result).AddSeconds(num);
			if (t > DateTime.Now)
			{
				ltrAllianceBlockTime.Text = string.Format("- Alliance Block Time :{0}", t.ToString("yyyy-MM-dd HH:mm:ss"));
			}
			else
			{
				ltrAllianceBlockTime.Text = "";
			}
		}
		else
		{
			ltrAllianceBlockTime.Text = "";
		}
	}

	protected string BindGuildName(object item)
	{
		string text = (string)DataBinder.Eval(item, "name");
		if (lead_guild_id == (int)DataBinder.Eval(item, "sid"))
		{
			text = "[L] " + text;
		}
		if (base.Request.QueryString["guild_id"] == ((int)DataBinder.Eval(item, "sid")).ToString())
		{
			ltrGuildName.Text = text;
		}
		string arg = (string)DataBinder.Eval(item, "icon");
		int num = (int)DataBinder.Eval(item, "icon_size");
		string text2 = link.GuildPopup(selected_server, (int)DataBinder.Eval(item, "sid"), text);
		if (num > 0)
		{
			string a = selected_server;
			if (a == "program" || a == "devel")
			{
				return string.Format("<img src='{0}{1}' align='absmiddle'> {2}", ConfigurationManager.AppSettings["guild_icon_url_test"], arg, text2);
			}
			return string.Format("<img src='{0}{1}' align='absmiddle'> {2}", ConfigurationManager.AppSettings["guild_icon_url"], arg, text2);
		}
		return $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{text2}";
	}

	protected string BindGuildLeader(object item)
	{
		_ = (string)DataBinder.Eval(item, "leader_account");
		string text = (string)DataBinder.Eval(item, "leader_name");
		string text2 = selected_server;
		int num = (int)DataBinder.Eval(item, "leader_id");
		int num2 = (int)DataBinder.Eval(item, "leader_account_id");
		int job_code = (int)DataBinder.Eval(item, "leader_job");
		int num3 = (int)DataBinder.Eval(item, "leader_lv");
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
		resource.GetJobResource(server.GetResourceServerNo(text2), job_code, out var job_name, out var job_icon);
		bool flag = false;
		if (DataBinder.Eval(item, "leader_immoral_point").GetType() == typeof(float))
		{
			float num4 = (float)DataBinder.Eval(item, "leader_immoral_point");
			if (num4 >= 100f)
			{
				flag = true;
			}
		}
		else
		{
			decimal d = (decimal)DataBinder.Eval(item, "leader_immoral_point");
			if (d >= 100m)
			{
				flag = true;
			}
		}
		if (job_icon != "")
		{
			if (flag)
			{
				return string.Format("<img src='../img/icon/{1}' align='absmiddle' title='{2}'> <font color='red'>{0}</font> Lv {3}", text, job_icon, job_name, num3);
			}
			return string.Format("<img src='../img/icon/{1}' align='absmiddle' title='{2}'> {0} Lv {3}", text, job_icon, job_name, num3);
		}
		return $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{text}";
	}

	protected string BindDungeon(object item)
	{
		int num = (int)DataBinder.Eval(item, "dungeon_id");
		return resource.GetStringResource(server.GetResourceServerNo(selected_server), num + 70000000);
	}

	protected string BindName(object item)
	{
		_ = (string)DataBinder.Eval(item, "account");
		string text = (string)DataBinder.Eval(item, "name");
		string text2 = selected_server;
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
		resource.GetJobResource(server.GetResourceServerNo(text2), (int)DataBinder.Eval(item, "job"), out var job_name, out var job_icon);
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
			return "<img src='../img/icon_gm.gif' align='absmiddle'> ";
		}
		return "";
	}

	protected string BindAuto(object item)
	{
		if ((int)DataBinder.Eval(item, "auto_used") == 1)
		{
			return "<font color='blue'>AUTO</font>";
		}
		return "";
	}

	protected string BindLogin(object item)
	{
		if ((DateTime)DataBinder.Eval(item, "login_time") > (DateTime)DataBinder.Eval(item, "logout_time"))
		{
			return "O";
		}
		TimeSpan timeSpan = DateTime.Now - (DateTime)DataBinder.Eval(item, "logout_time");
		if (timeSpan.Days > 0)
		{
			return $"<span title='logout {timeSpan.Days} days ago'>X</span>";
		}
		if (timeSpan.Hours > 0)
		{
			return $"<span title='logout {timeSpan.Hours} hours ago'>X</span>";
		}
		return $"<span title='logout {timeSpan.Minutes} minutes ago'>X</span>";
	}

	protected void btnDeleteGuildIcon_Click(object sender, EventArgs e)
	{
		if (admin.Authority(selected_server, "gmtool_modify_guild_info"))
		{
			if (acm.DeleteGuildIcon(selected_server, guild_id, out var result_msg))
			{
				common.MsgBox(this, "GuildInfo", "Delete Guild Icon :  Success \n\n" + result_msg);
			}
			else
			{
				common.MsgBox(this, "GuildInfo", "Delete Guild Icon :  Failure \n\n" + result_msg);
			}
		}
		GetData();
	}

	protected void btnChangeGuildIcon_Click(object sender, EventArgs e)
	{
		if (admin.Authority(selected_server, "gmtool_modify_guild_info"))
		{
			if (acm.ChangeGuildIcon(selected_server, guild_id, txtIcon.Text.Trim(), int.Parse(txtIconSize.Text.Trim()), out var result_msg))
			{
				common.MsgBox(this, "GuildInfo", "Change Guild Icon :  Success \n\n" + result_msg);
			}
			else
			{
				common.MsgBox(this, "GuildInfo", "Change Guild Icon :  Failure \n\n" + result_msg);
			}
		}
		GetData();
	}

	protected void btnChangeGuildName_Click(object sender, EventArgs e)
	{
		if (admin.Authority(selected_server, "gmtool_modify_guild_info"))
		{
			acm.ChangeGuildName(selected_server, guild_id, txtGuildName.Text.Trim());
		}
		GetData();
	}

	protected void btnChangeGuildLeader_Click(object sender, EventArgs e)
	{
		if (admin.Authority(selected_server, "gmtool_modify_guild_info"))
		{
			acm.ChangeGuildLeader(selected_server, guild_id, int.Parse(txtGuildLeaderSid.Text));
		}
		GetData();
	}
}
