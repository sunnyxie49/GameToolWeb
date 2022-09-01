using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Notice_auto_notice : Page, IRequiresSessionState
{
	protected Repeater rptAutoNotice;

	protected HtmlInputText txtStart;

	protected HtmlInputText txtEnd;

	protected TextBox txtRepeatSec;

	protected TextBox txtNotice;

	protected Button Button1;

	protected DropDownList ddlServer;

	protected Button Button2;

	protected Repeater rptNotice;

	protected HtmlInputHidden hdAutoNotice;

	private int writeType;

	private int no;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			server.SetServer(ref rptAutoNotice);
			server.SetServer(ref ddlServer);
			txtStart.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			txtEnd.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}
		if (base.Request.QueryString["type"] != "" && base.Request.QueryString["no"] != "" && base.Request.QueryString["server"] != "")
		{
			writeType = 1;
			int.TryParse(base.Request.QueryString["no"], out no);
		}
	}

	protected void getList()
	{
		SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(ddlServer.SelectedValue));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "dbo.gmtool_get_ScheduledCommand_list";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		sqlDataAdapter.Fill(dataSet);
		rptNotice.DataSource = dataSet;
		rptNotice.ItemDataBound += rptNotice_ItemDataBound;
		rptNotice.DataBind();
		sqlConnection.Close();
	}

	private void rptNotice_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
		{
			Literal literal = (Literal)e.Item.FindControl("ltrBegin");
			Literal literal2 = (Literal)e.Item.FindControl("ltrEnd");
			Literal literal3 = (Literal)e.Item.FindControl("ltrCommand");
			Literal literal4 = (Literal)e.Item.FindControl("ltrModify");
			string text = ((DataRowView)e.Item.DataItem)["begin_time"].ToString();
			string text2 = ((DataRowView)e.Item.DataItem)["end_time"].ToString();
			string text3 = ((DataRowView)e.Item.DataItem)["command"].ToString();
			literal.Text = text;
			literal2.Text = text2;
			literal3.Text = text3;
			literal4.Text = string.Format("<input class=\"btn btn-small btn-inverse\" type=\"button\" value=\"modify\" onclick=\"location.href='auto_notice.aspx?no={0}&type=modify&server={1}'\" />", ((DataRowView)e.Item.DataItem)["sid"].ToString(), ddlServer.SelectedValue);
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		string text = "";
		string[] array = hdAutoNotice.Value.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				string text2 = "";
				text2 = array[i];
				SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(text2));
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				if (writeType == 1 && no != 0)
				{
					text = "dbo.gmtool_update_ScheduledCommand";
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.Add("@begin_time", SqlDbType.DateTime).Value = txtStart.Value;
					sqlCommand.Parameters.Add("@end_time", SqlDbType.DateTime).Value = txtEnd.Value;
					sqlCommand.Parameters.Add("@interval", SqlDbType.Int).Value = txtRepeatSec.Text;
					sqlCommand.Parameters.Add("@command", SqlDbType.NVarChar, 255).Value = txtNotice.Text;
					sqlCommand.Parameters.Add("@sid", SqlDbType.Int).Value = no;
					acm.WriteCheatLog("AUTO_NOTICE_MODIFY", text2, 0, "", 0, "", 0L, 0, 0, 0, 0, 0, $"seq:{no}__command:{txtNotice.Text}");
				}
				else
				{
					text = "dbo.gmtool_insert_ScheduledCommand";
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.Add("@begin_time", SqlDbType.DateTime).Value = txtStart.Value;
					sqlCommand.Parameters.Add("@end_time", SqlDbType.DateTime).Value = txtEnd.Value;
					sqlCommand.Parameters.Add("@interval", SqlDbType.Int).Value = txtRepeatSec.Text;
					sqlCommand.Parameters.Add("@command", SqlDbType.NVarChar, 255).Value = txtNotice.Text;
					acm.WriteCheatLog("AUTO_NOTICE_INSERT", text2, 0, "", 0, "", 0L, 0, 0, 0, 0, 0, $"seq:{no}__command:{txtNotice.Text}");
				}
				sqlConnection.Open();
				sqlCommand.CommandText = text;
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlConnection.Close();
				string address;
				short port;
				string password;
				server.GameServerInfo(text2, out address, out port, out password);
				string command = "#refresh( \"scheduled_command\" )";
				object o_result;
				libACWC.Execute(address, port, password, command, out o_result);
			}
		}
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		getList();
	}
}
