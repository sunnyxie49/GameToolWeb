using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Log_Game_Auth : Page, IRequiresSessionState
{
	protected DropDownList ddlLogFile;

	protected HtmlInputText txtStart;

	protected HtmlInputText txtEnd;

	protected HtmlInputText txtAccount;

	protected HtmlInputText txtAccountNo;

	protected HtmlInputText txtIP;

	protected Button Button1;

	protected Literal ltrTotalCnt;

	protected Repeater rptGameAuth;

	protected string NullResult = string.Empty;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		if (!admin.Authority("log/game_auth.aspx"))
		{
			common.MsgBox("没有权限", "返回");
		}
		if (!base.IsPostBack)
		{
			ddlLogFile.Items.Clear();
			int num = 0;
			do
			{
				ddlLogFile.Items.Add(new ListItem(DateTime.Now.AddMonths(num).ToString("yyyy /  MM"), DateTime.Now.AddMonths(num).ToString("yyyyMM")));
				num--;
			}
			while (DateTime.Now.AddMonths(num).ToString("yyyyMM") != ConfigurationManager.AppSettings["ServiceStart"]);
			txtStart.Value = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace('-', '/');
			txtEnd.Value = DateTime.Now.AddDays(1.0).ToString("MM/dd/yyyy HH:mm:ss").Replace('-', '/');
			txtAccount.Value = string.Format("{0}", base.Request.QueryString["account"]);
			txtAccountNo.Value = string.Format("{0}", base.Request.QueryString["account_no"]);
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		GetLogData();
	}

	private void GetLogData()
	{
		SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["auth_log"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = connection;
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		string text = " and log_date between @start_date and @end_date ";
		sqlCommand.Parameters.Add("@start_date", SqlDbType.DateTime).Value = txtStart.Value;
		sqlCommand.Parameters.Add("@end_date", SqlDbType.DateTime).Value = txtEnd.Value;
		string text2 = "";
		if (text2 != "")
		{
			text = text + " and log_id in " + $"( {text2} ) ";
		}
		if (txtAccount.Value != "")
		{
			text += " and s1 = @s1";
			sqlCommand.Parameters.Add("@s1", SqlDbType.VarChar).Value = txtAccount.Value;
		}
		if (txtAccountNo.Value != "")
		{
			text += " and n1 = @n1";
			sqlCommand.Parameters.Add("@n1", SqlDbType.Int).Value = int.Parse(txtAccountNo.Value);
		}
		if (txtIP.Value != "")
		{
			text += " and s2 like @s2";
			sqlCommand.Parameters.Add("@s2", SqlDbType.VarChar).Value = $"{txtIP.Value}%";
		}
		string text4 = (sqlCommand.CommandText = string.Format("select * from Log_auth_{1}.dbo.tb_log_auth_{1} where log_id in (1001, 1002) {0} order by log_date", text, ddlLogFile.SelectedValue));
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		try
		{
			sqlDataAdapter.Fill(dataSet);
			ltrTotalCnt.Text = dataSet.Tables[0].Rows.Count.ToString("#,##0");
			rptGameAuth.DataSource = dataSet;
			rptGameAuth.ItemDataBound += rptGameAuth_ItemDataBound;
			rptGameAuth.DataBind();
		}
		catch (Exception)
		{
			NullResult = "<tr><td colspan='4' style='text-align:center;'>No Data</td></tr>";
		}
	}

	private void rptGameAuth_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
		{
			return;
		}
		Literal literal = (Literal)e.Item.FindControl("ltrDate");
		Literal literal2 = (Literal)e.Item.FindControl("ltrAccount");
		Literal literal3 = (Literal)e.Item.FindControl("ltrMsg");
		Literal literal4 = (Literal)e.Item.FindControl("ltrIP");
		int num = (int)((DataRowView)e.Item.DataItem)["log_id"];
		int num2 = (int)((DataRowView)e.Item.DataItem)["n1"];
		int num3 = (int)((DataRowView)e.Item.DataItem)["n2"];
		_ = (int)((DataRowView)e.Item.DataItem)["n3"];
		_ = (int)((DataRowView)e.Item.DataItem)["n4"];
		int num4 = (int)((DataRowView)e.Item.DataItem)["n5"];
		_ = (int)((DataRowView)e.Item.DataItem)["n6"];
		_ = (int)((DataRowView)e.Item.DataItem)["n7"];
		_ = (int)((DataRowView)e.Item.DataItem)["n8"];
		_ = (int)((DataRowView)e.Item.DataItem)["n9"];
		_ = (int)((DataRowView)e.Item.DataItem)["n10"];
		_ = (long)((DataRowView)e.Item.DataItem)["n11"];
		string arg = ((DataRowView)e.Item.DataItem)["s1"].ToString();
		string text = ((DataRowView)e.Item.DataItem)["s2"].ToString();
		((DataRowView)e.Item.DataItem)["s3"].ToString();
		((DataRowView)e.Item.DataItem)["s4"].ToString();
		literal.Text = ((DateTime)((DataRowView)e.Item.DataItem)["log_date"]).ToString("yyyy-MM-dd HH:mm:ss");
		if (num >= 1000)
		{
			literal2.Text = $"<span title='{num2}'>{arg}</span>";
		}
		StringBuilder stringBuilder = new StringBuilder(100);
		switch (num)
		{
		case 1001:
			stringBuilder.Append("Login #@pcbang@#");
			switch (num3)
			{
			case 1:
				stringBuilder.Replace("#@pcbang@#", "[PC CAFE]");
				break;
			case 2:
				stringBuilder.Replace("#@pcbang@#", "[Double Plus PC CAFE]");
				break;
			default:
				stringBuilder.Replace("#@pcbang@#", "");
				break;
			}
			break;
		case 1002:
			stringBuilder.Append("Logout #@pcbang@#");
			switch (num4)
			{
			case 1:
				stringBuilder.Replace("#@pcbang@#", "[PC CAFE]");
				break;
			case 2:
				stringBuilder.Replace("#@pcbang@#", "[Double Plus PC CAFE]");
				break;
			default:
				stringBuilder.Replace("#@pcbang@#", "");
				break;
			}
			break;
		}
		literal3.Text = stringBuilder.ToString();
		literal4.Text = text;
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		GetLogData();
		base.Response.Clear();
		base.Response.AddHeader("content-disposition", "attachment;filename=DataGrid.xls");
		base.Response.ContentType = "application/vnd.xls";
		StringWriter stringWriter = new StringWriter();
		HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
		rptGameAuth.RenderControl(writer);
		base.Response.Write(stringWriter.ToString());
		base.Response.End();
	}
}
