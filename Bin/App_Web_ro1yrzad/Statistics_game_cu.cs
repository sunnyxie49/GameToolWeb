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

public class Statistics_game_cu : Page, IRequiresSessionState
{
	protected RadioButtonList rblValue;

	protected Literal Literal1;

	protected Literal Literal2;

	protected HtmlGenericControl pacu;

	protected LinkButton hdNowDate;

	protected HtmlInputHidden hdRadioCheck;

	public int year = DateTime.Now.Year;

	public int month = DateTime.Now.Month;

	public int day = DateTime.Now.Day;

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
		if (base.Request["__EVENTTARGET"] == hdNowDate.ClientID)
		{
			hdNowDate_Click(base.Request["__EVENTARGUMENT"]);
		}
		pacu.Visible = false;
		_ = base.IsPostBack;
	}

	protected void GetData()
	{
		string.IsNullOrEmpty(hdNowDate.Text);
		DateTime dateTime = default(DateTime).AddYears(year - 1).AddMonths(month).AddDays(day - 1);
		DateTime dateTime2 = default(DateTime).AddYears(year - 1).AddMonths(month).AddDays(day)
			.AddSeconds(-1.0);
		SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = connection;
		sqlCommand.CommandText = "gmtool_game_cu";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@start_date", SqlDbType.DateTime).Value = dateTime;
		sqlCommand.Parameters.Add("@end_date", SqlDbType.DateTime).Value = dateTime2;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataTable dataTable = new DataTable();
		sqlDataAdapter.Fill(dataTable);
		StringBuilder stringBuilder = new StringBuilder(10000);
		StringBuilder stringBuilder2 = new StringBuilder(10000);
		StringBuilder stringBuilder3 = new StringBuilder(10000);
		stringBuilder.Append("<chart animation='0' baseFontSize = '12' palette='2' caption='GAME CU' subCaption='' showValues='0' divLineDecimalPrecision='1' limitsDecimalPrecision='1' PYAxisName='Total' SYAxisName='Each' numberPrefix='' formatNumberScale='0'>");
		stringBuilder.Append("<categories>");
		for (int i = 0; i < 24; i++)
		{
			stringBuilder.AppendFormat("<category label='{0}' />", i);
		}
		stringBuilder.Append("</categories>");
		stringBuilder2.Append("<table class=\"table table-bordered centerTable\">");
		stringBuilder2.Append("<thead><tr><th></th>");
		for (int j = 0; j < 24; j++)
		{
			stringBuilder2.AppendFormat("<th>{0}</th>", j);
		}
		stringBuilder2.Append("</tr></thead>");
		stringBuilder.AppendFormat("<dataset seriesName='Total  {0} CU'  lineThickness='3' parentYAxis='P'>", rblValue.SelectedValue);
		stringBuilder3.Append("<tr class='success'><td class='leftAlign'>Total</td>");
		int num = 0;
		for (int k = 0; k < 24; k++)
		{
			DataRow[] array = dataTable.Select($"h = {k}");
			int num2 = 0;
			for (int l = 0; l < array.Length; l++)
			{
				switch (rblValue.SelectedValue.ToLower())
				{
				case "min":
					num2 += (int)array[l]["min_cnt"];
					num += (int)array[l]["min_cnt"];
					break;
				case "avg":
					num2 += (int)array[l]["avg_cnt"];
					num += (int)array[l]["avg_cnt"];
					break;
				case "max":
					num2 += (int)array[l]["max_cnt"];
					num += (int)array[l]["max_cnt"];
					break;
				}
			}
			if (num2 > 0)
			{
				stringBuilder.AppendFormat("<set value='{0}' />", num2);
			}
			else
			{
				stringBuilder.AppendFormat("<set  />", 0);
			}
			stringBuilder3.AppendFormat("<td>{0}</td>", num2);
		}
		stringBuilder.Append("</dataset>");
		stringBuilder3.Append("</tr>");
		pacu.Visible = true;
		Literal2.Text = "ACCU : " + num / 24;
		for (int m = 0; m < server.GetServerListCount(); m++)
		{
			string text = (string)server.dtServer.Rows[m]["code"];
			if (!server.IsService(text, dateTime))
			{
				continue;
			}
			stringBuilder.AppendFormat("<dataset seriesName='{0}'  lineThickness='3' parentYAxis='S'>", server.GetServerName(text));
			stringBuilder2.AppendFormat("<tr><td class='leftAlign'>{0}</td>", server.GetServerName(text));
			for (int n = 0; n < 24; n++)
			{
				DataRow[] array2 = dataTable.Select($"h = {n} and server='{text}'");
				int num3 = 0;
				for (int num4 = 0; num4 < array2.Length; num4++)
				{
					switch (rblValue.SelectedValue.ToLower())
					{
					case "min":
						num3 += (int)array2[num4]["min_cnt"];
						break;
					case "avg":
						num3 += (int)array2[num4]["avg_cnt"];
						break;
					case "max":
						num3 += (int)array2[num4]["max_cnt"];
						break;
					}
				}
				if (num3 > 0)
				{
					stringBuilder.AppendFormat("<set value='{0}' />", num3);
				}
				else
				{
					stringBuilder.AppendFormat("<set  />", 0);
				}
				stringBuilder2.AppendFormat("<td align='right'>{0}</td>", num3);
			}
			stringBuilder.Append("</dataset>");
			stringBuilder2.Append("</tr>");
		}
		stringBuilder.Append("</chart>");
		stringBuilder2.Append(stringBuilder3.ToString());
		stringBuilder2.Append("</table>");
		Literal1.Text = string.Format("\r\n            {2}<br />  \r\n             <div id=\"chartdiv_{0}\"> FusionCharts. </div>              \r\n             <script type=\"text/javascript\">\r\n        \t     var chart = new FusionCharts(\"../Charts/MSCombiDY2D.swf\", \"{0}\", \"1100\", \"400\", \"0\", \"0\");\r\n\t             chart.setDataXML(\"{1}\");\r\n\t             chart.render(\"chartdiv_{0}\");\r\n\t        </script>", "Korea", stringBuilder.ToString(), stringBuilder2.ToString());
	}

	protected void hdNowDate_Click(string args)
	{
		month = int.Parse(args.Split('/')[0].ToLower());
		day = int.Parse(args.Split('/')[1].ToLower());
		year = int.Parse(args.Split('/')[2].ToLower());
		GetData();
		hdRadioCheck.Value = args;
		hdNowDate.Text = args;
	}

	protected void Calendar1_SelectionChanged(object sender, EventArgs e)
	{
		GetData();
	}

	protected void rblValue_SelectedIndexChanged(object sender, EventArgs e)
	{
		month = int.Parse(hdRadioCheck.Value.Split('/')[0].ToLower());
		day = int.Parse(hdRadioCheck.Value.Split('/')[1].ToLower());
		year = int.Parse(hdRadioCheck.Value.Split('/')[2].ToLower());
		GetData();
	}
}
