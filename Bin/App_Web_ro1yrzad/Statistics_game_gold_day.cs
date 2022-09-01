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

public class Statistics_game_gold_day : Page, IRequiresSessionState
{
	public int year = DateTime.Now.Year;

	public int month = DateTime.Now.Month;

	public int day = DateTime.Now.Day;

	protected Literal Literal1;

	protected LinkButton hdNowDate;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		if (!admin.Authority("statistics/game_gold.aspx"))
		{
			common.MsgBox("没有权限", "返回");
		}
		if (base.Request["__EVENTTARGET"] == hdNowDate.ClientID)
		{
			hdNowDate_Click(base.Request["__EVENTARGUMENT"]);
		}
		if (!base.IsPostBack)
		{
			int.TryParse(base.Request.QueryString["year"], out year);
			int.TryParse(base.Request.QueryString["month"], out month);
			int.TryParse(base.Request.QueryString["day"], out day);
			GetData();
		}
	}

	private void GetData()
	{
		StringBuilder stringBuilder = new StringBuilder();
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = "select * from tb_log_gold where DatePart(year,log_date) = @year and DatePart(month,log_date) = @month and DatePart(day,log_date) = @day order by server, log_date";
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@year", SqlDbType.Int).Value = year;
		sqlCommand.Parameters.Add("@month", SqlDbType.Int).Value = month;
		sqlCommand.Parameters.Add("@day", SqlDbType.Int).Value = day;
		sqlConnection.Open();
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		string text = "";
		long num = 0L;
		long num2 = 0L;
		while (sqlDataReader.Read())
		{
			DateTime dateTime = (DateTime)sqlDataReader["log_date"];
			string text2 = (string)sqlDataReader["server"];
			long num3 = (long)sqlDataReader["gold"];
			long num4 = (long)sqlDataReader["bank_gold"];
			if (text != text2)
			{
				stringBuilder.Append("<tr>");
				stringBuilder.AppendFormat("<td>{0}</td>", server.GetServerName(text2));
				stringBuilder.AppendFormat("<td>{0}</td>", dateTime.ToString("yyyy/MM/dd HH"));
				stringBuilder.AppendFormat("<td>{0}</td>", num3.ToString("#,##0"));
				stringBuilder.AppendFormat("<td>{0}</td>", num4.ToString("#,##0"));
				stringBuilder.Append("<td>&nbsp;</td>");
				stringBuilder.Append("<td>&nbsp;</td>");
				stringBuilder.Append("<td>&nbsp;</td>");
				stringBuilder.Append("</tr>");
			}
			else
			{
				stringBuilder.Append("<tr>");
				stringBuilder.AppendFormat("<td>{0}</td>", server.GetServerName(text2));
				stringBuilder.AppendFormat("<td>{0}</td>", dateTime.ToString("yyyy/MM/dd HH"));
				stringBuilder.AppendFormat("<td>{0}</td>", num3.ToString("#,##0"));
				stringBuilder.AppendFormat("<td>{0}</td>", num4.ToString("#,##0"));
				long num5 = 0L;
				num5 = num3 - num;
				if (num5 > 0)
				{
					stringBuilder.AppendFormat("<td><font color='red'>+{0}</font></td>", num5.ToString("#,##0"));
				}
				else
				{
					stringBuilder.AppendFormat("<td><font color='blue'>{0}</font></td>", num5.ToString("#,##0"));
				}
				num5 = num4 - num2;
				if (num5 > 0)
				{
					stringBuilder.AppendFormat("<td><font color='red'>+{0}</font></td>", num5.ToString("#,##0"));
				}
				else
				{
					stringBuilder.AppendFormat("<td><font color='blue'>{0}</font></td>", num5.ToString("#,##0"));
				}
				num5 = num3 - num + num4 - num2;
				if (num5 > 0)
				{
					stringBuilder.AppendFormat("<td><font color='red'>+{0}</font></td>", num5.ToString("#,##0"));
				}
				else
				{
					stringBuilder.AppendFormat("<td><font color='blue'>{0}</font></td'>", num5.ToString("#,##0"));
				}
				stringBuilder.Append("</tr>");
			}
			text = text2;
			num = num3;
			num2 = num4;
		}
		sqlDataReader.Close();
		sqlConnection.Close();
		Literal1.Text = stringBuilder.ToString();
		hdNowDate.Text = month + "/" + day + "/" + year;
	}

	protected void hdNowDate_Click(string args)
	{
		month = int.Parse(args.Split('/')[0].ToLower());
		day = int.Parse(args.Split('/')[1].ToLower());
		year = int.Parse(args.Split('/')[2].ToLower());
		GetData();
		hdNowDate.Text = args;
	}

	protected void Calendar1_SelectionChanged(object sender, EventArgs e)
	{
		GetData();
	}
}
