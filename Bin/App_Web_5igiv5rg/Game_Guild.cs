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

public class Game_Guild : Page, IRequiresSessionState
{
	protected int grid_no = 1;

	protected DropDownList ddlServer;

	protected TextBox txtGuildName;

	protected Button Button1;

	protected HtmlInputRadioButton optionsRadios1;

	protected HtmlInputRadioButton optionsRadios2;

	protected Repeater rptGuild;

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
			common.MsgBox("No Permission", "back");
		}
		if (!base.IsPostBack)
		{
			server.SetServer(ref ddlServer, "");
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		string commandText = "";
		if (optionsRadios1.Checked)
		{
			commandText = "gmtool_GetGuild_orderbyPoint";
		}
		else if (optionsRadios2.Checked)
		{
			commandText = "gmtool_GetGuild_orderbyMemberCnt";
		}
		SqlConnection connection = new SqlConnection(server.GetConnectionString(ddlServer.SelectedValue));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = connection;
		sqlCommand.CommandText = commandText;
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@guild_name", SqlDbType.NVarChar).Value = $"{txtGuildName.Text.Trim()}%";
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataTable dataTable = new DataTable();
		sqlDataAdapter.Fill(dataTable);
		rptGuild.DataSource = dataTable;
		rptGuild.DataBind();
	}

	protected string BindNo()
	{
		return grid_no++.ToString();
	}

	protected string BindGuildName(object item)
	{
		_ = (string)DataBinder.Eval(item, "name");
		string arg = (string)DataBinder.Eval(item, "icon");
		int num = (int)DataBinder.Eval(item, "icon_size");
		string text = link.GuildPopup(ddlServer.SelectedValue, (int)DataBinder.Eval(item, "sid"), (string)DataBinder.Eval(item, "name"));
		if (num > 0)
		{
			string selectedValue = ddlServer.SelectedValue;
			if (selectedValue == "program" || selectedValue == "devel")
			{
				return string.Format("<img src='{0}{1}' align='absmiddle'> {2}", ConfigurationManager.AppSettings["guild_icon_url_test"], arg, text);
			}
			return string.Format("<img src='{0}{1}' align='absmiddle'> {2}", ConfigurationManager.AppSettings["guild_icon_url"], arg, text);
		}
		return $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{text}";
	}

	protected string BindGuildLeader(object item)
	{
		_ = (string)DataBinder.Eval(item, "leader_account");
		string text = (string)DataBinder.Eval(item, "leader_name");
		string selectedValue = ddlServer.SelectedValue;
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
		text = string.Format("<a href=\"javascript:;\" onclick=\"window.open('character_info.aspx?server={0}&character_id={1}&account_id={3}','character_name_info_{3}','left=50,top=50,width=1250,height=770,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{2}</a>", selectedValue, num, text, num2);
		resource.GetJobResource(server.GetResourceServerNo(selectedValue), job_code, out var job_name, out var job_icon);
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

	protected string BindGuildNotice(object item)
	{
		return base.Server.HtmlEncode((string)DataBinder.Eval(item, "notice")).Replace("\r\n", "<br />");
	}

	protected string BindDungeon(object item)
	{
		int num = (int)DataBinder.Eval(item, "dungeon_id");
		return resource.GetStringResource(server.GetResourceServerNo(ddlServer.SelectedValue), num + 70000000);
	}

	protected string BindAllianceBlockTime(object item)
	{
		long num = (long)DataBinder.Eval(item, "alliance_block_time");
		if (num > 0)
		{
			int result = 0;
			int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
			DateTime t = new DateTime(1970, 1, 1, 0, 0, 0).AddHours(result).AddSeconds(num);
			if (t > DateTime.Now)
			{
				return t.ToString("yyyy-MM-dd HH:mm:ss");
			}
			return "";
		}
		return "";
	}
}
