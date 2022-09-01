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

public class Statistics_game_ru : Page, IRequiresSessionState
{
	protected Button Button1;

	protected Button Button2;

	protected DropDownList ddlMonth;

	protected Button Button3;

	protected DropDownList ddlLv;

	protected Button btnDown;

	protected Literal ltrMsg;

	protected Repeater rptMonthList;

	protected Repeater rptDateList;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		if (!base.IsPostBack)
		{
			DateTime dateTime = new DateTime(2006, 1, 1);
			while (dateTime <= DateTime.Now)
			{
				ddlMonth.Items.Insert(0, dateTime.ToString("yyyy-MM"));
				dateTime = dateTime.AddMonths(1);
			}
			for (int i = 1; i < 150; i++)
			{
				ddlLv.Items.Add(i.ToString());
			}
			ddlLv.SelectedValue = "30";
		}
		ltrMsg.Text = "";
	}

	private void GetData()
	{
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		try
		{
			sqlCommand.CommandText = $"\r\n\t\t\t\tselect\r\n\t\t\t\t\tsubstring(create_time, 1, 7) as create_time,\r\n\t\t\t\t\tsum(cnt) as cnt,\r\n\t\t\t\t\tavg(cnt) as average_cnt,\r\n\t\t\t\t\tsum(lv_cnt) as lv_cnt,\r\n\t\t\t\t\tavg(lv_cnt) as lv_average_cnt\r\n\t\t\t\tfrom\r\n\t\t\t\t(\r\n\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taa.create_time,\r\n\t\t\t\t\t\tIsNull(aa.cnt, 0) as cnt,\r\n\t\t\t\t\t\tIsNull(bb.cnt, 0) as lv_cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) \r\n\t\t\t\t\tgroup by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as aa\r\n\t\t\t\t\tleft join\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) group by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\twhere lv >= @lv\r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as bb\r\n\t\t\t\t\ton aa.create_time = bb.create_time\r\n\t\t\t\t) as cc\r\n\t\t\t\tgroup by  substring(create_time, 1, 7)\r\n\t\t\t\torder by substring(create_time, 1, 7)\r\n\r\n\t\t\t";
			sqlCommand.CommandType = CommandType.Text;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@lv", SqlDbType.Int).Value = int.Parse(ddlLv.SelectedValue);
			sqlCommand.CommandTimeout = 180;
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);
			rptMonthList.DataSource = dataSet.Tables[0];
			rptMonthList.DataBind();
		}
		catch (Exception ex)
		{
			ltrMsg.Text = ex.Message;
		}
		sqlCommand.Dispose();
		sqlConnection.Dispose();
	}

	private void GetData2()
	{
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		try
		{
			sqlCommand.CommandText = $"\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taa.create_time,\r\n\t\t\t\t\t\tIsNull(aa.cnt, 0) as cnt,\r\n\t\t\t\t\t\tIsNull(bb.cnt, 0) as lv_cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) \r\n\t\t\t\t\tgroup by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as aa\r\n\t\t\t\t\tleft join\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) group by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\twhere lv >= @lv\r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as bb\r\n\t\t\t\t\ton aa.create_time = bb.create_time\r\n\t\t\t\t\torder by aa.create_time\r\n\t\t\t";
			sqlCommand.CommandType = CommandType.Text;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@lv", SqlDbType.Int).Value = int.Parse(ddlLv.SelectedValue);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);
			rptDateList.DataSource = dataSet.Tables[0];
			rptDateList.DataBind();
		}
		catch (Exception ex)
		{
			ltrMsg.Text = ex.Message;
		}
		sqlCommand.Dispose();
		sqlConnection.Dispose();
	}

	private void GetData3()
	{
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		try
		{
			sqlCommand.CommandText = $"\r\n\t\t\t\tselect\r\n\t\t\t\t\tsubstring(create_time, 1, 7) as create_time,\r\n\t\t\t\t\tsum(cnt) as cnt,\r\n\t\t\t\t\tavg(cnt) as average_cnt,\r\n\t\t\t\t\tsum(lv_cnt) as lv_cnt,\r\n\t\t\t\t\tavg(lv_cnt) as lv_average_cnt\r\n\t\t\t\tfrom\r\n\t\t\t\t(\r\n\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taa.create_time,\r\n\t\t\t\t\t\tIsNull(aa.cnt, 0) as cnt,\r\n\t\t\t\t\t\tIsNull(bb.cnt, 0) as lv_cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) \r\n\t\t\t\t\tgroup by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\twhere YEAR(create_time) = YEAR(@dt) and MONTH(create_time) = MONTH(@dt) \r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as aa\r\n\t\t\t\t\tleft join\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) group by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\twhere YEAR(create_time) = YEAR(@dt) and MONTH(create_time) = MONTH(@dt)  and lv >= @lv\r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as bb\r\n\t\t\t\t\ton aa.create_time = bb.create_time\r\n\t\t\t\t) as cc\r\n\t\t\t\tgroup by  substring(create_time, 1, 7)\r\n\t\t\t\torder by substring(create_time, 1, 7);\r\n\r\n\t\t\t\tselect \r\n\t\t\t\t\t\taa.create_time,\r\n\t\t\t\t\t\tIsNull(aa.cnt, 0) as cnt,\r\n\t\t\t\t\t\tIsNull(bb.cnt, 0) as lv_cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) \r\n\t\t\t\t\tgroup by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\twhere YEAR(create_time) = YEAR(@dt) and MONTH(create_time) = MONTH(@dt) \r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as aa\r\n\t\t\t\t\tleft join\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) group by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\twhere YEAR(create_time) = YEAR(@dt) and MONTH(create_time) = MONTH(@dt) and lv >= @lv\r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as bb\r\n\t\t\t\t\ton aa.create_time = bb.create_time\r\n\t\t\t\t\torder by aa.create_time\r\n\r\n\t\t\t";
			sqlCommand.CommandType = CommandType.Text;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@dt", SqlDbType.DateTime).Value = ddlMonth.SelectedValue + "-01";
			sqlCommand.Parameters.Add("@lv", SqlDbType.Int).Value = int.Parse(ddlLv.SelectedValue);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);
			rptMonthList.DataSource = dataSet.Tables[0];
			rptMonthList.DataBind();
			rptDateList.DataSource = dataSet.Tables[1];
			rptDateList.DataBind();
		}
		catch (Exception ex)
		{
			ltrMsg.Text = ex.Message;
		}
		sqlCommand.Dispose();
		sqlConnection.Dispose();
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		GetData();
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		GetData2();
	}

	protected void Button3_Click(object sender, EventArgs e)
	{
		GetData3();
	}

	protected void btnDown_Click(object sender, EventArgs e)
	{
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = "\r\n\t\t\tselect\t\r\n\t\t\t\tcreate_time,\r\n\t\t\t\taccount_id,\r\n\t\t\t\taccount\r\n\t\t\tfrom\r\n\t\t\t(\r\n\t\t\t\tselect\r\n\t\t\t\t\taccount_id, \r\n\t\t\t\t\taccount, \r\n\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\tfrom dbo.tb_character_info with (nolock) \r\n\t\t\t\tgroup by account_id, account\r\n\t\t\t) as a\r\n\t\t\twhere convert(varchar(7), create_time, 120) = convert(varchar(7), @dt, 120) \r\n\t\t\torder by create_time, account_id, account\r\n\t\t";
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@dt", SqlDbType.DateTime).Value = ddlMonth.SelectedValue + "-01";
		sqlConnection.Open();
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		StringBuilder stringBuilder = new StringBuilder(1000);
		while (sqlDataReader.Read())
		{
			stringBuilder.AppendFormat("{0:yyyy-MM-dd HH:mm:ss}\t{1}\t{2}\r\n", sqlDataReader["create_time"], sqlDataReader["account_id"], sqlDataReader["account"]);
		}
		sqlDataReader.Close();
		sqlConnection.Close();
		sqlDataReader.Dispose();
		sqlCommand.Dispose();
		sqlConnection.Dispose();
		base.Response.Clear();
		base.Response.AddHeader("content-disposition", $"attachment;filename=RU_{ddlMonth.SelectedValue}.txt");
		base.Response.ContentEncoding = Encoding.UTF8;
		base.Response.ContentType = "text/txt";
		base.Response.Write(stringBuilder.ToString().Trim());
		base.Response.End();
	}
}
