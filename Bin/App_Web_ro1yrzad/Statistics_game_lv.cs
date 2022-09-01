using System;
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

public class Statistics_game_lv : Page, IRequiresSessionState
{
	private DataTable dt;

	private int max_lv = 170;

	protected Repeater rptGameLV;

	protected Button Button1;

	protected Literal Literal1;

	protected HtmlInputHidden hdGameLv;

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
		if (!int.TryParse(base.Request.QueryString["max_lv"], out max_lv))
		{
			max_lv = 170;
		}
		dt = new DataTable("tb_character");
		dt.Columns.Add("server", typeof(string));
		dt.Columns.Add("lv", typeof(int));
		dt.Columns.Add("all", typeof(int));
		dt.Columns.Add("highest", typeof(int));
		dt.Columns.Add("30days", typeof(int));
		dt.Columns.Add("7days", typeof(int));
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		if (!base.IsPostBack)
		{
			server.SetServer(ref rptGameLV);
		}
	}

	private void InitData(string server)
	{
		dt.Clear();
		for (int i = 1; i <= max_lv; i++)
		{
			DataRow dataRow = dt.NewRow();
			dataRow["server"] = server;
			dataRow["lv"] = i;
			dataRow["all"] = 0;
			dataRow["highest"] = 0;
			dataRow["30days"] = 0;
			dataRow["7days"] = 0;
			dt.Rows.Add(dataRow);
		}
	}

	private void GetData(string server)
	{
		InitData(server);
		SqlConnection sqlConnection = new SqlConnection(libs.server.GetConnectionString(server));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "gmtool_GetLvCnt";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			if ((int)sqlDataReader["lv"] > 0 && (int)sqlDataReader["lv"] <= max_lv)
			{
				dt.Rows[(int)sqlDataReader["lv"] - 1]["all"] = (int)sqlDataReader["cnt"];
			}
		}
		sqlDataReader.NextResult();
		while (sqlDataReader.Read())
		{
			if ((int)sqlDataReader["lv"] > 0 && (int)sqlDataReader["lv"] <= max_lv)
			{
				dt.Rows[(int)sqlDataReader["lv"] - 1]["highest"] = (int)sqlDataReader["cnt"];
			}
		}
		sqlDataReader.NextResult();
		while (sqlDataReader.Read())
		{
			if ((int)sqlDataReader["lv"] > 0 && (int)sqlDataReader["lv"] <= max_lv)
			{
				dt.Rows[(int)sqlDataReader["lv"] - 1]["30days"] = (int)sqlDataReader["cnt"];
			}
		}
		sqlDataReader.NextResult();
		while (sqlDataReader.Read())
		{
			if ((int)sqlDataReader["lv"] > 0 && (int)sqlDataReader["lv"] <= max_lv)
			{
				dt.Rows[(int)sqlDataReader["lv"] - 1]["7days"] = (int)sqlDataReader["cnt"];
			}
		}
		StringBuilder stringBuilder = new StringBuilder(1000);
		if (dt.Rows.Count > 0)
		{
			stringBuilder.AppendFormat("<chart animation='0' baseFontSize = '12' palette='2' caption='[{0}] Rappelz Character LV Stastics' subCaption='' showValues='0' divLineDecimalPrecision='1' limitsDecimalPrecision='1' PYAxisName='Total Character Count' SYAxisName='Character Count' numberPrefix='' formatNumberScale='0'>", GetServerName(server));
			stringBuilder.Append("<categories>");
			for (int i = 1; i <= max_lv; i++)
			{
				stringBuilder.AppendFormat("<category label='{0}' />", i);
			}
			stringBuilder.Append("</categories>");
			stringBuilder.Append("<dataset seriesName='All Characters'>");
			for (int j = 1; j <= max_lv; j++)
			{
				stringBuilder.AppendFormat("<set value='{0}' />", dt.Rows[j - 1]["all"]);
			}
			stringBuilder.Append("</dataset>");
			stringBuilder.Append("<dataset seriesName='highest lv of each account'  renderAs='Area' parentYAxis='P'>");
			for (int k = 1; k <= max_lv; k++)
			{
				stringBuilder.AppendFormat("<set value='{0}' />", dt.Rows[k - 1]["highest"]);
			}
			stringBuilder.Append("</dataset>");
			stringBuilder.Append("<dataset lineThickness='1' seriesName='all characters who login the game within 30days lately' parentYAxis='S'>");
			for (int l = 1; l <= max_lv; l++)
			{
				stringBuilder.AppendFormat("<set value='{0}' />", dt.Rows[l - 1]["30days"]);
			}
			stringBuilder.Append("</dataset>");
			stringBuilder.Append("<dataset lineThickness='1' seriesName='all characters who login the game within 7days lately' parentYAxis='S'>");
			for (int m = 1; m <= max_lv; m++)
			{
				stringBuilder.AppendFormat("<set value='{0}' />", dt.Rows[m - 1]["7days"]);
			}
			stringBuilder.Append("</dataset>");
			stringBuilder.Append("</chart>");
			Literal1.Text += string.Format("\r\n                 <div id=\"chartdiv_{0}\"> FusionCharts. </div>\r\n                 <script type=\"text/javascript\">\r\n            \t     var chart = new FusionCharts(\"../Charts/MSCombiDY2D.swf\", \"{0}\", \"3200\", \"400\", \"0\", \"0\");\r\n\t\t             chart.setDataXML(\"{1}\");\r\n\t\t             chart.render(\"chartdiv_{0}\");\r\n\t\t        </script><br />", server, stringBuilder.ToString());
		}
	}

	private string GetServerName(string server)
	{
		return libs.server.GetServerName(server);
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		string[] array = hdGameLv.Value.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				GetData(array[i]);
			}
		}
	}
}
