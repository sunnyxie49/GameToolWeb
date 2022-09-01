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

public class Statistics_game_summon_taming : Page, IRequiresSessionState
{
	private int resource_server_no;

	protected DropDownList ddlServer;

	protected RadioButtonList rblSummonType;

	protected Button Button1;

	protected Label lblSummonName;

	protected Repeater rptGameSummonTaming;

	protected Panel pnSummon;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		if (!base.IsPostBack)
		{
			server.SetServer(ref ddlServer, "");
			resource_server_no = server.GetResourceServerNo(ddlServer.SelectedValue);
			DataRow[] array = resource.GetResourceTable(resource_server_no, "SummonResource").Select("form = 1 and is_riding_only = 0", "rate, id");
			rblSummonType.Items.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				ListItem item = new ListItem(resource.GetSummonName(resource_server_no, (int)array[i]["id"]), array[i]["id"].ToString());
				rblSummonType.Items.Add(item);
			}
		}
	}

	protected string BindGetSummonName(object item)
	{
		int summon_id = (int)DataBinder.Eval(item, "id");
		return resource.GetSummonName(resource_server_no, summon_id);
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		lblSummonName.Text = rblSummonType.SelectedItem.Text;
		pnSummon.Visible = true;
		int resourceServerNo = server.GetResourceServerNo(ddlServer.SelectedValue);
		DataRow[] array = resource.GetResourceTable(resourceServerNo, "ItemResource").Select($"summon_id = {rblSummonType.SelectedValue}");
		string text = "";
		for (int i = 0; i < array.Length; i++)
		{
			text = ((i + 1 != array.Length) ? (text + array[i]["id"].ToString() + ",") : (text + array[i]["id"].ToString()));
		}
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["statistics"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = "dbo.gmtool_get_summon_enhance_list";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@codeArray", SqlDbType.VarChar).Value = text;
		sqlCommand.Parameters.Add("@server", SqlDbType.VarChar, 10).Value = ddlServer.SelectedValue;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataTable dataTable = new DataTable();
		sqlDataAdapter.Fill(dataTable);
		rptGameSummonTaming.DataSource = dataTable;
		rptGameSummonTaming.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			Label label = lblSummonName;
			label.Text = label.Text + " - log date : " + dataTable.Rows[0]["log_date"].ToString();
		}
		sqlCommand.Dispose();
		sqlConnection.Close();
	}
}
