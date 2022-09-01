using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;

public class Log_chat_log : Page, IRequiresSessionState
{
	protected DropDownList ddlServer;

	protected DropDownList ddlMonth;

	protected TextBox txtLogDate;

	protected TextBox txtAccount;

	protected TextBox txtCharacter;

	protected Button Button1;

	protected GridView GridView1;

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
			common.MsgBox("没有权限.", "返回");
		}
		if (!base.IsPostBack)
		{
			server.SetServer(ref ddlServer);
			int num = 0;
			do
			{
				ddlMonth.Items.Add(new ListItem(DateTime.Now.AddMonths(num).ToString("yyyy /  MM"), DateTime.Now.AddMonths(num).ToString("yyyyMM")));
				num--;
			}
			while (int.Parse(DateTime.Now.AddMonths(num).ToString("yyyyMM")) >= 200901);
			txtLogDate.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
		}
	}

	private void GetData()
	{
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["chat_log"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		string text = ddlServer.SelectedValue;
		if (text == "program")
		{
			text = "game001";
		}
		string selectedValue = ddlMonth.SelectedValue;
		string text2 = "";
		DateTime result = DateTime.Now;
		sqlCommand.Parameters.Clear();
		if (DateTime.TryParse(txtLogDate.Text, out result))
		{
			text2 += " and log_date >= @log_date";
			sqlCommand.Parameters.Add("@log_date", SqlDbType.DateTime).Value = txtLogDate.Text;
		}
		if (txtAccount.Text != string.Empty)
		{
			text2 += " and sender_account = @sender_account";
			sqlCommand.Parameters.Add("@sender_account", SqlDbType.VarChar, 60).Value = txtAccount.Text;
		}
		if (txtCharacter.Text != string.Empty)
		{
			text2 += " and sender_character = @sender_character";
			sqlCommand.Parameters.Add("@sender_character", SqlDbType.NVarChar).Value = txtCharacter.Text;
		}
		string text4 = (sqlCommand.CommandText = string.Format("SELECT\r\n\t\t\t\t\t\t\t\ttop 300\r\n\t\t\t\t\t\t\t\t\t   [log_date]\r\n\t\t\t\t\t\t\t\t\t  ,[sender_account_id]\r\n\t\t\t\t\t\t\t\t\t  ,[sender_character_id]\r\n\t\t\t\t\t\t\t\t\t  ,[chat_type]\r\n\t\t\t\t\t\t\t\t\t  ,[sender_pos_x]\r\n\t\t\t\t\t\t\t\t\t  ,[sender_pos_y]\r\n\t\t\t\t\t\t\t\t\t  ,[receiver_account_id]\r\n\t\t\t\t\t\t\t\t\t  ,[receiver_character_id]\r\n\t\t\t\t\t\t\t\t\t  ,[sender_account]\r\n\t\t\t\t\t\t\t\t\t  ,[sender_character]\r\n\t\t\t\t\t\t\t\t\t  ,[receiver_account]\r\n\t\t\t\t\t\t\t\t\t  ,[receiver_character]\r\n\t\t\t\t\t\t\t\t\t  ,[chat]\r\n\t\t\t\t\t\t\t\tFROM [Log_ChattingLog_{0}_{1}].[dbo].[tb_log_ChattingLog_{0}_{1}]\r\n\t\t\t\t\t\t\t\twhere msg_id = 0 {2}\r\n\t\t\t\t\t\t\t\torder by log_date", text, selectedValue, text2));
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataTable dataTable = new DataTable();
		sqlDataAdapter.Fill(dataTable);
		GridView1.DataSource = dataTable;
		GridView1.DataBind();
		sqlDataAdapter.Dispose();
		sqlCommand.Dispose();
		sqlConnection.Dispose();
	}

	protected string BindChatType(object item)
	{
		int num = (int)DataBinder.Eval(item, "chat_type");
		string text = "";
		return string.Format("({0}) {1}", num, num switch
		{
			0 => "Nomal",
			1 => "Yell",
			2 => "Advertize",
			3 => "Whisper",
			4 => "Global",
			6 => "[GM]Global",
			7 => "[GM]Whisper",
			10 => "Party",
			11 => "Guild",
			12 => "Attackteam",
			_ => "unknown",
		});
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		GetData();
	}
}
