using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;

public class Game_Dungeon : Page, IRequiresSessionState
{
	protected DropDownList ddlServer;

	protected Button Button1;

	protected Repeater rptDungeon;

	protected Repeater rptDungeonRaid;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			server.SetServer(ref ddlServer, base.Request.QueryString["server"]);
		}
		if (ddlServer.SelectedValue != "")
		{
			GetDungeonData(ddlServer.SelectedValue);
			GetDungeonRaidData(ddlServer.SelectedValue);
		}
	}

	private void GetDungeonData(string server)
	{
		SqlConnection sqlConnection = new SqlConnection(libs.server.GetConnectionString(server));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		int result = 0;
		int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
		sqlCommand.CommandText = "gmtool_GetDungeonData_foreign";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@server", SqlDbType.VarChar).Value = server;
		sqlCommand.Parameters.Add("@gmt", SqlDbType.Int).Value = result;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataTable dataTable = new DataTable();
		sqlDataAdapter.Fill(dataTable);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			dataTable.Rows[i]["dungeon_name"] = SetDungeonName((int)dataTable.Rows[i]["dungeon_id"]);
		}
		DataView dataView = new DataView(dataTable);
		dataView.Sort = "dungeon_name, dungeon_id";
		rptDungeon.DataSource = dataView;
		rptDungeon.DataBind();
		sqlCommand.Dispose();
		sqlConnection.Close();
		sqlConnection.Dispose();
	}

	private void GetDungeonRaidData(string server)
	{
		SqlConnection sqlConnection = new SqlConnection(libs.server.GetConnectionString(server));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		int result = 0;
		int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
		sqlCommand.CommandText = "gmtool_GetDungeonRaid_foreign";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@server", SqlDbType.VarChar).Value = server;
		sqlCommand.Parameters.Add("@gmt", SqlDbType.Int).Value = result;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataTable dataTable = new DataTable();
		sqlDataAdapter.Fill(dataTable);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			dataTable.Rows[i]["dungeon_name"] = SetDungeonName((int)dataTable.Rows[i]["dungeon_id"]);
		}
		DataView dataView = new DataView(dataTable);
		dataView.Sort = "dungeon_name, dungeon_id, raid_time, record ";
		rptDungeonRaid.DataSource = dataView;
		rptDungeonRaid.DataBind();
		sqlCommand.Dispose();
		sqlConnection.Dispose();
	}

	private string SetDungeonName(int dungeon_id)
	{
		string text = "";
		return string.Format("{0} {1}", dungeon_id switch
		{
			130400 => "01.", 
			130700 => "02.", 
			130000 => "03.", 
			130600 => "04.", 
			130300 => "05.", 
			130500 => "06.", 
			130800 => "07.", 
			130900 => "08.", 
			121000 => "09.", 
			_ => "99.", 
		}, resource.GetStringResource(server.GetResourceServerNo(ddlServer.SelectedValue), dungeon_id + 70000000));
	}

	protected string BindDungeon(object item)
	{
		int num = (int)DataBinder.Eval(item, "dungeon_id");
		return $"<a href='dungeon_log.aspx?server={ddlServer.SelectedValue}&dungeonid={num}' target='_blank'>{SetDungeonName(num)}</a>";
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
			resource.GetJobResource(server.GetResourceServerNo((string)DataBinder.Eval(item, "server")), (int)DataBinder.Eval(item, target + "guild_leader_job"), out var job_name, out var job_icon);
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
