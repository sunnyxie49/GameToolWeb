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

public class Game_dungeon_log : Page, IRequiresSessionState
{
	protected Literal ltrInfo;

	protected Repeater rptDugeonLog;

	protected HtmlForm form1;

	private string server = "";

	private int dungeon_id;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		server = string.Format("{0}", base.Request.QueryString["server"]);
		int.TryParse(base.Request.QueryString["dungeonid"], out dungeon_id);
		GetList();
	}

	protected void GetList()
	{
		if (dungeon_id >= 0)
		{
			ltrInfo.Text = $"<th>server : {server}</th><td>dungeon : {SetDungeonName(dungeon_id)}</td>";
			SqlConnection sqlConnection = new SqlConnection(libs.server.GetConnectionString(server));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlCommand.CommandText = "dbo.gmtool_GetDungeonRaid_temp_foreign_ver2";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@server", SqlDbType.VarChar).Value = server;
			sqlCommand.Parameters.Add("@dungeon_id", SqlDbType.Int).Value = dungeon_id;
			sqlCommand.Parameters.Add("@gmt", SqlDbType.Int).Value = ConfigurationManager.AppSettings["gmt"];
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataTable dataTable = new DataTable();
			sqlDataAdapter.Fill(dataTable);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				dataTable.Rows[i]["dungeon_name"] = SetDungeonName((int)dataTable.Rows[i]["dungeon_id"]);
			}
			rptDugeonLog.DataSource = dataTable;
			rptDugeonLog.DataBind();
			sqlCommand.Dispose();
			sqlConnection.Close();
			sqlConnection.Dispose();
		}
	}

	private string SetDungeonName(int dungeon_id)
	{
		return $"{resource.GetStringResource(libs.server.GetResourceServerNo(server), dungeon_id + 70000000)}";
	}

	protected string BindGuildName(object item, string target)
	{
		string text = (string)DataBinder.Eval(item, "server");
		if ((int)DataBinder.Eval(item, target + "guild_id") > 0)
		{
			int num = (int)DataBinder.Eval(item, target + "guild_icon_size");
			string text2 = link.GuildPopup(text, (int)DataBinder.Eval(item, target + "guild_id"), (string)DataBinder.Eval(item, target + "guild_name"));
			if (num > 0)
			{
				if (text == "program" || text == "devel")
				{
					return string.Format("<img src='{0}{1}' align='absmiddle'> {2}", ConfigurationManager.AppSettings["guild_icon_url_test"], (string)DataBinder.Eval(item, target + "guild_icon"), text2);
				}
				return string.Format("<img src='{0}{1}' align='absmiddle'> {2}", ConfigurationManager.AppSettings["guild_icon_url"], (string)DataBinder.Eval(item, target + "guild_icon"), text2);
			}
			return $"{text2}";
		}
		return "";
	}

	protected string BindGuildLeader(object item, string target)
	{
		if ((int)DataBinder.Eval(item, target + "guild_id") > 0)
		{
			_ = (int)DataBinder.Eval(item, target + "guild_icon_size");
			string.Format("<a href='{0}?server={1}&searchType=guild_name&searchText={2}' target='_blank'>{2}</a>", "character.aspx", (string)DataBinder.Eval(item, "server"), (string)DataBinder.Eval(item, target + "guild_name"));
			resource.GetJobResource(libs.server.GetResourceServerNo((string)DataBinder.Eval(item, "server")), (int)DataBinder.Eval(item, target + "guild_leader_job"), out var job_name, out var job_icon);
			if (job_icon != "")
			{
				return string.Format("<img src='../img/icon/{1}' align='absmiddle' title='{2}'> {0} Lv {3}", DataBinder.Eval(item, target + "guild_leader_name"), job_icon, job_name, DataBinder.Eval(item, target + "guild_leader_lv"));
			}
			return string.Format("{0} Lv {1}", DataBinder.Eval(item, target + "guild_leader_name"), DataBinder.Eval(item, target + "guild_leader_lv"));
		}
		return "";
	}

	protected string BindRecord(object item)
	{
		int num = (int)DataBinder.Eval(item, "record");
		int num2 = num / 100;
		int num3 = num2 / 86400;
		int num4 = num2 % 86400 / 3600;
		int num5 = num2 % 86400 % 3600 / 60;
		int num6 = num2 % 86400 % 3600 % 60;
		string text = "";
		text = ((num3 > 0) ? (num3 + "d ") : "");
		text += ((num4 > 0) ? (num4 + "h ") : "");
		text += ((num5 > 0) ? (num5 + "m") : "");
		text += ((num6 > 0) ? (num6 + "s") : "");
		return string.Format("<span title='{1} sec'>{0}<span>", text, num2);
	}
}
