using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;

public class Statistics_game_character : Page, IRequiresSessionState
{
	protected Button Button1;

	protected Button Button2;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		if (!admin.Authority("statistics/game_lv.aspx"))
		{
			common.MsgBox("没有权限", "返回");
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		try
		{
			SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = connection;
			sqlCommand.CommandText = "\r\n\t\t\t\tselect \r\n\t\t\t\t\tserver,\r\n\t\t\t\t\taccount_id,\r\n\t\t\t\t\tmax(lv) as max_lv,\r\n\t\t\t\t\tavg(lv) as avg_lv,\r\n\t\t\t\t\tcount(*) as character_cnt,\r\n\t\t\t\t\tmax(play_time) as max_play_time,\r\n\t\t\t\t\tsum(convert(bigint, play_time)) as total_play_time,\r\n\t\t\t\t\tmin(create_time) as min_create_time,\r\n\t\t\t\t\tmax(login_time) as last_login_time\r\n\t\t\t\tfrom tb_character_info wtih (nolock)\r\n\t\t\t\tgroup by server, account_id\r\n\t\t\t\torder by server, max_lv desc\r\n\t\t\t";
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataTable dataTable = new DataTable();
			sqlDataAdapter.Fill(dataTable);
			sqlCommand.CommandTimeout = 300;
			base.Response.Clear();
			base.Response.AddHeader("content-disposition", string.Format("attachment;filename=RappelzCharacterStatistics_{0}.csv", DateTime.Now.ToString("yyyyMMdd")));
			base.Response.ContentEncoding = Encoding.Default;
			base.Response.ContentType = "text/txt";
			base.Response.Write(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}\r\n", "server", "account_id", "max_lv", "avg_lv", "character_count", "max_play_time", "total_play_time", "min_create_time", "last_login_time"));
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				base.Response.Write(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}\r\n", dataTable.Rows[i]["server"], dataTable.Rows[i]["account_id"], dataTable.Rows[i]["max_lv"], dataTable.Rows[i]["avg_lv"], dataTable.Rows[i]["character_cnt"], dataTable.Rows[i]["max_play_time"], dataTable.Rows[i]["total_play_time"], Convert.ToDateTime(dataTable.Rows[i]["min_create_time"]).ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToDateTime(dataTable.Rows[i]["last_login_time"]).ToString("yyyy-MM-dd HH:mm:ss")));
			}
			base.Response.End();
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.ToString());
		}
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = connection;
		sqlCommand.CommandText = "\r\n\t\t\t\tselect \r\n\t\t\t\t\taccount_id,\r\n\t\t\t\t\tmax(lv) as max_lv,\r\n\t\t\t\t\tavg(lv) as avg_lv,\r\n\t\t\t\t\tcount(*) as character_cnt,\r\n\t\t\t\t\tmax(play_time) as max_play_time,\r\n\t\t\t\t\tsum(convert(bigint, play_time)) as total_play_time,\r\n\t\t\t\t\tmin(create_time) as min_create_time,\r\n\t\t\t\t\tmax(login_time) as last_login_time\r\n\t\t\t\tfrom tb_character_info wtih (nolock)\r\n\t\t\t\tgroup by account_id\r\n\t\t\t\torder by max_lv desc\r\n\t\t\t";
		sqlCommand.CommandTimeout = 300;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataTable dataTable = new DataTable();
		sqlDataAdapter.Fill(dataTable);
		base.Response.Clear();
		base.Response.AddHeader("content-disposition", string.Format("attachment;filename=RappelzCharacterStatistics_{0}.csv", DateTime.Now.ToString("yyyyMMdd")));
		base.Response.ContentEncoding = Encoding.Default;
		base.Response.ContentType = "text/txt";
		base.Response.Write(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}\r\n", "account_id", "max_lv", "avg_lv", "character_count", "max_play_time", "total_play_time", "min_create_time", "last_login_time"));
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			base.Response.Write(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}\r\n", dataTable.Rows[i]["account_id"], dataTable.Rows[i]["max_lv"], dataTable.Rows[i]["avg_lv"], dataTable.Rows[i]["character_cnt"], dataTable.Rows[i]["max_play_time"], dataTable.Rows[i]["total_play_time"], Convert.ToDateTime(dataTable.Rows[i]["min_create_time"]).ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToDateTime(dataTable.Rows[i]["last_login_time"]).ToString("yyyy-MM-dd HH:mm:ss")));
		}
		base.Response.End();
	}
}
