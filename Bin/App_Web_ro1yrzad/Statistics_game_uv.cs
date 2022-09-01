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

public class Statistics_game_uv : Page, IRequiresSessionState
{
	protected DropDownList ddlYear;

	protected DropDownList ddlMonth;

	protected Button Button1;

	protected Button btnDown;

	protected Literal Literal1;

	protected Literal Literal2;

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
			ddlYear.Items.Clear();
			int num = 0;
			do
			{
				ddlYear.Items.Add(new ListItem(DateTime.Now.AddYears(num).ToString("yyyy"), DateTime.Now.AddYears(num).ToString("yyyy")));
				num--;
			}
			while (DateTime.Now.AddYears(num).ToString("yyyy") != "2004");
			ddlYear.SelectedValue = DateTime.Now.Year.ToString();
			ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		GetData();
	}

	private void GetData()
	{
		SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = connection;
		sqlCommand.CommandText = "gmtool_game_uv";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@y", SqlDbType.Int).Value = ddlYear.SelectedValue;
		sqlCommand.Parameters.Add("@m", SqlDbType.Int).Value = ddlMonth.SelectedValue;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		sqlDataAdapter.Fill(dataSet);
		sqlCommand.CommandText = "gmtool_game_mcu";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@y", SqlDbType.Int).Value = ddlYear.SelectedValue;
		sqlCommand.Parameters.Add("@m", SqlDbType.Int).Value = ddlMonth.SelectedValue;
		sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlDataAdapter.Fill(dataSet, "mcu");
		sqlCommand.CommandText = "select create_time, cnt from Rappelz_game_log.dbo.tb_game_ru where create_time like @create_time order by create_time";
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@create_time", SqlDbType.VarChar, 10).Value = $"{ddlYear.SelectedValue}-{ddlMonth.SelectedValue.PadLeft(2, '0')}%";
		sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		sqlDataAdapter.Fill(dataSet, "ru");
		try
		{
			if (ConfigurationManager.AppSettings["Region"] == "Taiwan")
			{
				SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["auth_log"].ConnectionString);
				SqlCommand sqlCommand2 = new SqlCommand();
				sqlCommand2.Connection = sqlConnection;
				sqlCommand2.CommandText = string.Format("select count(*) as uv from (select distinct n1 from Log_Auth_{0}.dbo.tb_log_auth_{0} with (nolock) where log_id = 1001 ) as a;", ddlYear.SelectedValue + ddlMonth.SelectedValue.PadLeft(2, '0'));
				sqlCommand2.CommandType = CommandType.Text;
				sqlCommand2.Parameters.Clear();
				SqlDataAdapter sqlDataAdapter2 = new SqlDataAdapter(sqlCommand2);
				sqlDataAdapter2.Fill(dataSet, "month_uv");
				Literal2.Text = string.Format("Month UV = {0}", ((int)dataSet.Tables["month_uv"].Rows[0][0]).ToString("#,##0"));
				sqlDataAdapter2.Dispose();
				sqlConnection.Dispose();
				sqlCommand2.Dispose();
			}
			else
			{
				sqlCommand.CommandText = string.Format("select count(*) as uv from (select distinct n1 from Log_Auth_{0}.dbo.tb_log_auth_{0} with (nolock) where log_id = 1001 ) as a;", ddlYear.SelectedValue + ddlMonth.SelectedValue.PadLeft(2, '0'));
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.Parameters.Clear();
				sqlDataAdapter = new SqlDataAdapter(sqlCommand);
				sqlDataAdapter.Fill(dataSet, "month_uv");
				Literal2.Text = string.Format("Month UV = {0}", ((int)dataSet.Tables["month_uv"].Rows[0][0]).ToString("#,##0"));
			}
		}
		catch (Exception ex)
		{
			Literal2.Text = ex.Message;
		}
		StringBuilder stringBuilder = new StringBuilder(100000);
		StringBuilder stringBuilder2 = new StringBuilder(100000);
		stringBuilder2.Append("<table class='table table-bordered centerTable'><thead>");
		stringBuilder2.Append("<tr><th></th>");
		for (int i = 1; i <= 31; i++)
		{
			stringBuilder2.AppendFormat("<th>{0}</th>", i);
		}
		stringBuilder2.Append("</tr></thead>");
		stringBuilder2.Append("<tbody><tr><th class='leftAlign'>UV</th>");
		for (int j = 1; j <= 31; j++)
		{
			DataRow[] array = dataSet.Tables[0].Select("d=" + j);
			if (array.Length > 0)
			{
				stringBuilder2.AppendFormat("<td>{0}</td>", (int)array[0]["cnt"]);
			}
			else
			{
				stringBuilder2.AppendFormat("<td>{0}</td>", 0);
			}
		}
		stringBuilder2.Append("</tr>");
		stringBuilder2.Append("<tr><th class='leftAlign'>MCU</th>");
		for (int k = 1; k <= 31; k++)
		{
			DataRow[] array2 = dataSet.Tables[1].Select("d=" + k);
			if (array2.Length > 0)
			{
				stringBuilder2.AppendFormat("<td>{0}</td>", (int)array2[0]["max_cnt"]);
			}
			else
			{
				stringBuilder2.AppendFormat("<td>{0}</td>", 0);
			}
		}
		stringBuilder2.Append("</tr>");
		stringBuilder2.Append("<tr><th class='leftAlign'>RU</th>");
		for (int l = 1; l <= 31; l++)
		{
			DataRow[] array3 = dataSet.Tables["ru"].Select($"create_time='{ddlYear.SelectedValue}-{ddlMonth.SelectedValue.PadLeft(2, '0')}-{l.ToString().PadLeft(2, '0')}'");
			if (array3.Length > 0)
			{
				stringBuilder2.AppendFormat("<td>{0}</td>", (int)array3[0]["cnt"]);
			}
			else
			{
				stringBuilder2.AppendFormat("<td>{0}</td>", 0);
			}
		}
		stringBuilder2.Append("</tr></tbody>");
		stringBuilder2.Append("</table>");
		stringBuilder.Append("<chart animation='0' baseFontSize = '12' palette='2' caption='GAME UV, MCU, RU' subCaption='' showValues='0' divLineDecimalPrecision='1' limitsDecimalPrecision='1' PYAxisName='UV/MCU' SYAxisName='RU' numberPrefix='' formatNumberScale='0'>");
		stringBuilder.Append("<categories>");
		for (int m = 1; m <= 31; m++)
		{
			stringBuilder.AppendFormat("<category label='{0}' />", m);
		}
		stringBuilder.Append("</categories>");
		stringBuilder.Append("<dataset seriesName='UV'  lineThickness='3' parentYAxis='P'>");
		for (int n = 1; n <= 31; n++)
		{
			DataRow[] array4 = dataSet.Tables[0].Select("d=" + n);
			if (array4.Length > 0)
			{
				stringBuilder.AppendFormat("<set value='{0}' />", (int)array4[0]["cnt"]);
			}
			else
			{
				stringBuilder.AppendFormat("<set  />", 0);
			}
		}
		stringBuilder.Append("</dataset>");
		stringBuilder.Append("<dataset seriesName='MCU'  lineThickness='3' parentYAxis='P'>");
		for (int num = 1; num <= 31; num++)
		{
			DataRow[] array5 = dataSet.Tables["mcu"].Select("d=" + num);
			if (array5.Length > 0)
			{
				stringBuilder.AppendFormat("<set value='{0}' />", (int)array5[0]["max_cnt"]);
			}
			else
			{
				stringBuilder.AppendFormat("<set  />", 0);
			}
		}
		stringBuilder.Append("</dataset>");
		stringBuilder.Append("<dataset seriesName='RU'  lineThickness='3' parentYAxis='S'>");
		for (int num2 = 1; num2 <= 31; num2++)
		{
			DataRow[] array6 = dataSet.Tables["ru"].Select($"create_time='{ddlYear.SelectedValue}-{ddlMonth.SelectedValue.PadLeft(2, '0')}-{num2.ToString().PadLeft(2, '0')}'");
			if (array6.Length > 0)
			{
				stringBuilder.AppendFormat("<set value='{0}' />", (int)array6[0]["cnt"]);
			}
			else
			{
				stringBuilder.AppendFormat("<set  />", 0);
			}
		}
		stringBuilder.Append("</dataset>");
		stringBuilder.Append("</chart>");
		Literal1.Text = string.Format("\r\n             {2}<br />\r\n             <div id=\"chartdiv_{0}\"> FusionCharts. </div>\r\n             <script type=\"text/javascript\">\r\n        \t     var chart = new FusionCharts(\"../Charts/MSCombiDY2D.swf\", \"{0}\", \"1100\", \"400\", \"0\", \"0\");\r\n\t             chart.setDataXML(\"{1}\");\r\n\t             chart.render(\"chartdiv_{0}\");\r\n\t        </script>", "Korea", stringBuilder.ToString(), stringBuilder2.ToString());
	}

	protected void btnDown_Click(object sender, EventArgs e)
	{
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = string.Format("select distinct s1 from Log_Auth_{0}.dbo.tb_log_auth_{0} with (nolock) where log_id = 1001", ddlYear.SelectedValue + ddlMonth.SelectedValue.PadLeft(2, '0'));
		sqlCommand.CommandType = CommandType.Text;
		sqlConnection.Open();
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		StringBuilder stringBuilder = new StringBuilder(1000);
		while (sqlDataReader.Read())
		{
			stringBuilder.AppendLine(sqlDataReader["s1"].ToString());
		}
		sqlDataReader.Close();
		sqlConnection.Close();
		sqlDataReader.Dispose();
		sqlCommand.Dispose();
		sqlConnection.Dispose();
		base.Response.Clear();
		base.Response.AddHeader("content-disposition", $"attachment;filename=UV_{ddlYear.SelectedValue + ddlMonth.SelectedValue.PadLeft(2, '0')}.txt");
		base.Response.ContentEncoding = Encoding.UTF8;
		base.Response.ContentType = "text/txt";
		base.Response.Write(stringBuilder.ToString().Trim());
		base.Response.End();
	}
}
