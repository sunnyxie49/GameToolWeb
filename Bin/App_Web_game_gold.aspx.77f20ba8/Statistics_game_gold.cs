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

public class Statistics_game_gold : Page, IRequiresSessionState
{
	protected DropDownList ddlMonth;

	protected Button Button1;

	protected Literal Literal1;

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
			common.MsgBox("没有权限", "back");
		}
		if (base.IsPostBack)
		{
			return;
		}
		int result = 2006;
		int result2 = 1;
		int year = DateTime.Now.Year;
		int month = DateTime.Now.Month;
		int.TryParse(ConfigurationManager.AppSettings["ServiceStart"].Substring(0, 4), out result);
		int.TryParse(ConfigurationManager.AppSettings["ServiceStart"].Substring(4, 2), out result2);
		for (int i = result; i <= DateTime.Now.Year; i++)
		{
			for (int j = 1; j <= 12; j++)
			{
				if (i == result)
				{
					if (j >= result2)
					{
						ddlMonth.Items.Insert(0, new ListItem(string.Format("{0}-{1}", i.ToString("####"), j.ToString("00")), string.Format("{0}{1}", i.ToString("####"), j.ToString("00"))));
					}
				}
				else if (i == year)
				{
					if (j <= month)
					{
						ddlMonth.Items.Insert(0, new ListItem(string.Format("{0}-{1}", i.ToString("####"), j.ToString("00")), string.Format("{0}{1}", i.ToString("####"), j.ToString("00"))));
					}
				}
				else
				{
					ddlMonth.Items.Insert(0, new ListItem(string.Format("{0}-{1}", i.ToString("####"), j.ToString("00")), string.Format("{0}{1}", i.ToString("####"), j.ToString("00"))));
				}
			}
		}
	}

	private void GetData()
	{
		StringBuilder stringBuilder = new StringBuilder();
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = "select a.*\r\n\t\t\t\t\t\t\tfrom tb_log_gold as a with(nolock)\r\n\t\t\t\t\t\t\tjoin (select max(DatePart(hh,log_date)) maxdate, convert(varchar(10), log_date, 120) as log_date2  from  tb_log_gold  with(nolock) group by convert(varchar(10), log_date, 120)) b\r\n\t\t\t\t\t\t\ton convert(varchar(10), log_date, 120) = log_date2\r\n\t\t\t\t\t\t\twhere YEAR(log_date) = @year and MONTH(log_date) = @month and convert(varchar(2), log_date, 108) = b.maxdate\r\n\t\t\t\t\t\t\torder by server, log_date\r\n\t\t\t\t\t\t\t";
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@year", SqlDbType.Int).Value = ddlMonth.SelectedValue.Substring(0, 4);
		sqlCommand.Parameters.Add("@month", SqlDbType.Int).Value = ddlMonth.SelectedValue.Substring(4, 2);
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
				stringBuilder.Append("<tr class='sectionLine'>");
				stringBuilder.AppendFormat("<td>{0}</td>", server.GetServerName(text2));
				stringBuilder.AppendFormat("<td><a href='game_gold_day.aspx?year={1}&month={2}&day={3}'>{0}</a></td>", dateTime.ToString("yyyy/MM/dd HH"), dateTime.Year, dateTime.Month, dateTime.Day);
				stringBuilder.AppendFormat("<td>{0}</td>", num3.ToString("#,##0"));
				stringBuilder.AppendFormat("<td>{0}</td>", num4.ToString("#,##0"));
				stringBuilder.Append("<td></td>");
				stringBuilder.Append("<td></td>");
				stringBuilder.Append("<td></td>");
				stringBuilder.Append("</tr>");
			}
			else
			{
				stringBuilder.Append("<tr>");
				stringBuilder.AppendFormat("<td>{0}</td>", server.GetServerName(text2));
				stringBuilder.AppendFormat("<td><a href='game_gold_day.aspx?year={1}&month={2}&day={3}'>{0}</a></td>", dateTime.ToString("yyyy/MM/dd HH"), dateTime.Year, dateTime.Month, dateTime.Day);
				stringBuilder.AppendFormat("<td>{0}</td>", num3.ToString("#,##0"));
				stringBuilder.AppendFormat("<td>{0}</td>", num4.ToString("#,##0"));
				long num5 = 0L;
				num5 = num3 - num;
				if (num5 > 0)
				{
					stringBuilder.AppendFormat("<td class='redText'>+{0}</td>", num5.ToString("#,##0"));
				}
				else
				{
					stringBuilder.AppendFormat("<td class='blueText'>{0}</td>", num5.ToString("#,##0"));
				}
				num5 = num4 - num2;
				if (num5 > 0)
				{
					stringBuilder.AppendFormat("<td class='redText'>+{0}</td>", num5.ToString("#,##0"));
				}
				else
				{
					stringBuilder.AppendFormat("<td class='blueText'>{0}</td>", num5.ToString("#,##0"));
				}
				num5 = num3 - num + num4 - num2;
				if (num5 > 0)
				{
					stringBuilder.AppendFormat("<td class='redText'>+{0}</td>", num5.ToString("#,##0"));
				}
				else
				{
					stringBuilder.AppendFormat("<td class='blueText'>{0}</td'>", num5.ToString("#,##0"));
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
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		GetData();
	}
}
