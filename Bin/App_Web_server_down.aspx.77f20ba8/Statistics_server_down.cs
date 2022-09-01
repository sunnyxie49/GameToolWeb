using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Statistics_server_down : Page, IRequiresSessionState
{
	protected Button Button1;

	protected Repeater rptServerDown;

	protected Literal ltrResult;

	protected HtmlInputHidden hdServerDown;

	private StringBuilder sb;

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
			SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["auth_log"].ConnectionString);
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlCommand.CommandText = "select name from master.dbo.sysdatabases where name like 'Log_Auth%' order by name";
			sqlCommand.CommandType = CommandType.Text;
			sqlConnection.Open();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);
			rptServerDown.DataSource = dataSet;
			rptServerDown.DataBind();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		sb = new StringBuilder();
		string[] array = hdServerDown.Value.Split(',');
		for (int i = 0; i < array.Length - 1; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				GetData(array[i]);
			}
		}
		ltrResult.Text = sb.ToString();
	}

	private void GetData(string check)
	{
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["auth_log"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = string.Format("\r\n\t\t\t\t\t\tselect log_date, s4 as server\r\n\t\t\t\t\t\t from Log_Auth_{0}.dbo.tb_Log_Auth_{0}  with (NOLOCK) \r\n\t\t\t\t\t\twhere\r\n\t\t\t\t\t\tlog_id = 1102\r\n\t\t\t\t\t\torder by log_date\r\n\t\t\t\t\t\t", check);
		sqlCommand.CommandType = CommandType.Text;
		sqlConnection.Open();
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		sb.Append("<div class='month'>");
		sb.AppendFormat("<h4>{0}年 {1}月</h4>", check.Substring(0, 4), check.Substring(4, 2));
		sb.Append("<table class='table table-striped table-bordered'><tbody>");
		while (sqlDataReader.Read())
		{
			sb.Append("<tr>");
			DateTime dateTime = (DateTime)sqlDataReader["log_date"];
			string value = string.Format("<td class='redText'>{0}</td><td class='redText'>{1}</td><td class='redText'>#定期检查</td>", dateTime.ToString("yyyy-MM-dd HH:mm:ss"), (string)sqlDataReader["server"]);
			string value2 = string.Format("<td>{0}</td><td>{1}</td><td></td>", dateTime.ToString("yyyy-MM-dd HH:mm:ss"), (string)sqlDataReader["server"]);
			if (ConfigurationManager.AppSettings["Region"] == "Korea")
			{
				if (dateTime.DayOfWeek == DayOfWeek.Thursday && dateTime.Hour > 6 && dateTime.Hour < 11)
				{
					sb.Append(value);
				}
				else
				{
					sb.Append(value2);
				}
			}
			else if (ConfigurationManager.AppSettings["Region"] == "USA")
			{
				if (dateTime.DayOfWeek == DayOfWeek.Tuesday && dateTime.Hour >= 21 && dateTime.Hour <= 23)
				{
					sb.Append(value);
				}
				else
				{
					sb.Append(value2);
				}
			}
			else if (ConfigurationManager.AppSettings["Region"] == "Japan")
			{
				if (dateTime.DayOfWeek == DayOfWeek.Tuesday && dateTime.Hour >= 11 && dateTime.Hour <= 15)
				{
					sb.Append(value);
				}
				else
				{
					sb.Append(value2);
				}
			}
			else if (ConfigurationManager.AppSettings["Region"] == "China")
			{
				if (dateTime.DayOfWeek == DayOfWeek.Monday && dateTime.Hour >= 10 && dateTime.Hour <= 14)
				{
					sb.Append(value);
				}
				else
				{
					sb.Append(value2);
				}
			}
			else if (ConfigurationManager.AppSettings["Region"] == "Taiwan")
			{
				if (dateTime.DayOfWeek == DayOfWeek.Wednesday && dateTime.Hour >= 10 && dateTime.Hour <= 14)
				{
					sb.Append(value);
				}
				else
				{
					sb.Append(value2);
				}
			}
			else
			{
				sb.Append(value2);
			}
			sb.Append("</tr>");
		}
		sb.Append("</tbody></table>");
		sb.Append("</div>");
		sqlDataReader.Close();
		sqlDataReader.Dispose();
		sqlCommand.Dispose();
		sqlConnection.Close();
	}

	protected string BindDate(object item)
	{
		string text = ((string)DataBinder.Eval(item, "name")).Replace("Log_Auth_", "");
		return $"{text.Substring(0, 4)}年 {text.Substring(4, 2)}月";
	}

	protected string BindDateValue(object item)
	{
		return ((string)DataBinder.Eval(item, "name")).Replace("Log_Auth_", "");
	}
}
