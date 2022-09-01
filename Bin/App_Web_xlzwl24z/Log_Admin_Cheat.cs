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

public class Log_Admin_Cheat : Page, IRequiresSessionState
{
	private string searchUrl = "";

	private string searchSql = "";

	private int page = 1;

	private string searchAccount = "";

	protected DropDownList ddlServer;

	protected TextBox txtSearch;

	protected Button Button1;

	protected Literal ltrTotalCnt;

	protected Literal ltrPageTop;

	protected Repeater rptAdminCheat;

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
			page = common.ToInt(base.Request.QueryString["page"], 1);
			server.SetServer(ref ddlServer, base.Request.QueryString["server"]);
			searchAccount = string.Format("{0}", base.Request.QueryString["search"]);
			txtSearch.Text = searchAccount;
			GetLog();
		}
	}

	private void GetLog()
	{
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["gmtool"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		if (txtSearch.Text.Length > 0)
		{
			searchSql = " and account = '" + txtSearch.Text + "' ";
			searchUrl = "&search=" + txtSearch.Text;
		}
		searchSql += " and server = @server ";
		sqlConnection.Open();
		sqlCommand.CommandText = $"select count(*) cnt  from tb_gmtool_game_cheat_log WITH (NOLOCK) where 1=1 {searchSql}";
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@server", SqlDbType.VarChar).Value = ddlServer.SelectedValue;
		int totalCnt = (int)sqlCommand.ExecuteScalar();
		ltrTotalCnt.Text = totalCnt.ToString();
		ltrPageTop.Text = paging.PageList(base.Request.Path, "&server=" + ddlServer.SelectedValue + searchUrl, totalCnt, ref page, 100, 10);
		sqlCommand.CommandText = string.Format("select top 100 * from tb_gmtool_game_cheat_log WITH (NOLOCK) where sid not in (select top {0} sid from tb_gmtool_game_cheat_log WITH (NOLOCK) where 1=1  {1} order by log_date desc) {1} order by log_date desc ", (page - 1) * 100, searchSql);
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@server", SqlDbType.VarChar).Value = ddlServer.SelectedValue;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		sqlDataAdapter.Fill(dataSet);
		rptAdminCheat.DataSource = dataSet;
		rptAdminCheat.DataBind();
		sqlCommand.Dispose();
		sqlConnection.Close();
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		GetLog();
	}

	protected string BindAccount(object item)
	{
		int num = 0;
		string arg = "";
		try
		{
			num = (int)DataBinder.Eval(item, "account_id");
			arg = (string)DataBinder.Eval(item, "account");
		}
		catch (Exception)
		{
		}
		return string.Format("<a href=\"javascript:;\" onclick=\"window.open('../Account/account_info.aspx?account_id={0}','account_info_{0}','left=50,top=50,width=640,height=640,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{1}</a>", num, arg);
	}

	protected string BindCharacter(object item)
	{
		int num = 0;
		string arg = "";
		try
		{
			num = (int)DataBinder.Eval(item, "account_id");
			arg = (string)DataBinder.Eval(item, "account");
		}
		catch (Exception)
		{
		}
		int num2 = (int)DataBinder.Eval(item, "character_id");
		string arg2 = (string)DataBinder.Eval(item, "character_name");
		return link.CharacterPopup(ddlServer.SelectedValue, num2, $"{arg2}({num2})", num, $"{arg}({num})");
	}

	protected string BindItemId(object item)
	{
		long num = (long)DataBinder.Eval(item, "item_id");
		if (num >= 9000000000000000000L)
		{
			return string.Format("<a href=\"javascript:OpenEditItemPopup('{2}', '{0}')\"><span title='{0}'>-{1}</span></a>", num, num - 9000000000000000000L, ddlServer.SelectedValue);
		}
		return string.Format("<a href=\"javascript:OpenEditItemPopup('{2}', '{0}')\"><span title='{0}'>{0}</span></a>", num, num, ddlServer.SelectedValue);
	}
}
