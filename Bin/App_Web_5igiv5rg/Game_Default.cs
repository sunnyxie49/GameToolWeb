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

public class Game_Default : Page, IRequiresSessionState
{
	private int no;

	private int grid_no;

	private int page;

	protected DropDownList ddlServer;

	protected HtmlInputCheckBox chkUser;

	protected HtmlInputCheckBox chkGM;

	protected HtmlInputCheckBox chkGameAccount;

	protected HtmlInputCheckBox chkConnect;

	protected HtmlInputCheckBox chkAuto;

	protected HtmlInputText txtAccount;

	protected HtmlInputText txtCharacter;

	protected CheckBox chkLike;

	protected HtmlButton Button1;

	protected DropDownList ddlSort1;

	protected CheckBox chkSort1;

	protected DropDownList ddlViewCnt;

	protected DropDownList ddlJob;

	protected CheckBox chkCreateTime;

	protected TextBox txtStart;

	protected TextBox txtEnd;

	protected CheckBox chkSeeDeletedCharacter;

	protected Literal ltrTotalCnt;

	protected Literal ltrPageTop;

	protected Repeater rptAvaterInfo;

	protected Literal ltrPageBottom;

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
			common.MsgBox("No Permission", "back");
		}
		if (!base.IsPostBack)
		{
			if (!int.TryParse(base.Request.QueryString["page"], out page))
			{
				page = 1;
			}
			server.SetServer(ref ddlServer, base.Request.QueryString["server"]);
			txtAccount.Value = string.Format("{0}", base.Request.QueryString["account"]);
			ddlViewCnt.SelectedValue = ((base.Request.QueryString["pageSize"] == null) ? "100" : base.Request.QueryString["pageSize"]);
			txtStart.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss").Replace('-', '/');
			txtEnd.Text = DateTime.Now.AddDays(1.0).ToString("MM/dd/yyyy HH:mm:ss").Replace('-', '/');
			DataView dataView = new DataView(resource.GetResourceTable(3, "JobResource"));
			dataView.Sort = "id";
			dataView.RowFilter = "up_lv > 0 and value is not null";
			for (int i = 0; i < dataView.Count; i++)
			{
				ddlJob.Items.Add(new ListItem(dataView[i]["value"].ToString(), dataView[i]["id"].ToString()));
			}
			try
			{
				ddlJob.SelectedValue = base.Request.QueryString["job"];
			}
			catch (Exception)
			{
				ddlJob.SelectedValue = "all";
			}
			if (string.Format("{0}", base.Request.QueryString["page"]) != "")
			{
				GetCharacterList();
				return;
			}
			rptAvaterInfo.DataSource = null;
			rptAvaterInfo.DataBind();
		}
	}

	private void GetCharacterList()
	{
		SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(ddlServer.SelectedValue));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		string text = "";
		string text2 = "";
		string str = "";
		if (!int.TryParse(ddlViewCnt.SelectedItem.Value, out var result))
		{
			result = 100;
		}
		int listSize = 10;
		str += $"&pageSize={result}&server={ddlServer.SelectedValue}";
		if (txtAccount.Value != "")
		{
			text = ((!chkLike.Checked) ? (text + " and gmtool_view_check_searchType_main.account = @account") : (text + " and gmtool_view_check_searchType_main.account like @account"));
			str += $"&account={txtAccount.Value.Trim()}";
		}
		if (txtCharacter.Value != "")
		{
			text = ((!chkLike.Checked) ? (text + " and gmtool_view_check_searchType_main.name = @name") : (text + " and gmtool_view_check_searchType_main.name like @name"));
			str += $"&name={txtCharacter.Value.Trim()}";
		}
		if (chkUser.Checked)
		{
			str += "&user=1";
		}
		if (chkGM.Checked)
		{
			str += "&gm=1";
		}
		if (chkGameAccount.Checked)
		{
			str += "&game=1";
		}
		else
		{
			text += " and gmtool_view_check_searchType_main.account not like '@%' ";
		}
		if (chkAuto.Checked)
		{
			str += "&auto_used=1";
			text += " and auto_used = 1";
		}
		if (chkLike.Checked)
		{
			str += "&like=1";
		}
		if (chkCreateTime.Checked)
		{
			str += $"&create_time=1&start={txtStart.Text}&end={txtEnd.Text}";
			text = " and create_time between @start and @end ";
		}
		if (!chkSeeDeletedCharacter.Checked)
		{
			text += " and delete_time > '9999-01-01'  ";
		}
		else
		{
			str += "&see_delete_character=True";
		}
		if (chkUser.Checked && !chkGM.Checked)
		{
			text += " and permission = 0";
		}
		if (!chkUser.Checked && chkGM.Checked)
		{
			text += " and permission != 0";
		}
		if (chkConnect.Checked)
		{
			text += " and login_time > logout_time";
			str += "&connect=1";
		}
		if (!chkGameAccount.Checked)
		{
			text += " and account not like '@%'";
		}
		if (ddlSort1.SelectedIndex > 0)
		{
			if (chkSort1.Checked)
			{
				text2 += $"order by {ddlSort1.SelectedValue} desc";
				str += $"&sort1={ddlSort1.SelectedIndex}&desc1=1";
			}
			else
			{
				text2 += $"order by {ddlSort1.SelectedValue}";
				str += $"&sort1={ddlSort1.SelectedIndex}";
			}
		}
		if (ddlJob.SelectedValue != "all")
		{
			str += $"&job={ddlJob.SelectedValue}";
			text += " and job = @job";
		}
		if (text2 == "")
		{
			text2 = "order by exp desc";
		}
		sqlConnection.Open();
		sqlCommand.CommandText = $"select count(*) cnt from gmtool_view_check_searchType_main WITH (NOLOCK) where 1=1 {text}";
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		if (txtAccount.Value != "")
		{
			if (chkLike.Checked)
			{
				sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = $"%{txtAccount.Value.Trim()}%";
			}
			else
			{
				sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = $"{txtAccount.Value.Trim()}";
			}
		}
		if (txtCharacter.Value != "")
		{
			if (chkLike.Checked)
			{
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"%{txtCharacter.Value.Trim()}%";
			}
			else
			{
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"{txtCharacter.Value.Trim()}";
			}
		}
		if (chkCreateTime.Checked)
		{
			sqlCommand.Parameters.Add("@start", SqlDbType.DateTime).Value = txtStart.Text;
			sqlCommand.Parameters.Add("@end", SqlDbType.DateTime).Value = txtEnd.Text;
		}
		if (ddlJob.SelectedValue != "all")
		{
			sqlCommand.Parameters.Add("@job", SqlDbType.Int).Value = ddlJob.SelectedValue;
		}
		int totalCnt = (int)sqlCommand.ExecuteScalar();
		ltrTotalCnt.Text = totalCnt.ToString();
		ltrPageTop.Text = paging.PageList(base.Request.Path, str, totalCnt, ref page, result, listSize);
		ltrPageBottom.Text = ltrPageTop.Text;
		StringBuilder stringBuilder = new StringBuilder(2000);
		stringBuilder.AppendFormat("\r\n\t\t\t\t\t\t\tselect top {0}\r\n\t\t\t\t\t\t\t\t@server as server,\r\n\t\t\t\t\t\t\t\tgmtool_view_check_searchType_main.sid+2009092 as sid, gmtool_view_check_searchType_main.name, account, account_id+2002060 as account_id, \r\n\t\t\t\t\t\t\t\tparty_id, \r\n\t\t\t\t\t\t\t\t-- IsNull((select name from Party with (nolock) where sid = gmtool_view_check_searchType_main.party_id), '') as party_name,\r\n\t\t\t\t\t\t\t\tguild_id, \r\n\t\t\t\t\t\t\t--\tIsNull(gmtool_view_check_searchType_Guild.name, '') as guild_name,\r\n\t\t\t\t\t\t\t--\tIsNull(gmtool_view_check_searchType_Guild.icon_size, 0) as guild_icon_size,\r\n\t\t\t\t\t\t\t--\tIsNull(gmtool_view_check_searchType_Guild.icon, '') as guild_icon,\r\n\t\t\t\t\t\t\t--\tIsNull(gmtool_view_check_searchType_Guild.leader_id, 0) as guild_leader_id,\r\n\t\t\t\t\t\t\t--\tslot,\r\n\t\t\t\t\t\t\t\tpermission, \r\n\t\t\t\t\t\t\t--\tx, y, z, layer, \r\n\t\t\t\t\t\t\t\trace, sex, \r\n\t\t\t\t\t\t\t\tlv, exp, last_decreased_exp, \r\n\t\t\t\t\t\t\t--\thp, mp, stamina, \r\n\t\t\t\t\t\t\t\tjob, job_depth, jlv, jp, total_jp,\r\n\t\t\t\t\t\t\t--\tjob_0, job_1, job_2, jlv_0, jlv_1, jlv_2, \r\n\t\t\t\t\t\t\t\timmoral_point, cha, pkc, dkc, \r\n\t\t\t\t\t\t\t\tgmtool_view_check_searchType_main.gold,\r\n\t\t\t\t\t\t\t--\tIsNull((select sum(cnt) from gmtool_view_check_searchType_item with (nolock) where account_id = gmtool_view_check_searchType_main.account_id and account_id > 0 and code = 0), 0) as bank_gold,\r\n\t\t\t\t\t\t\t\tgmtool_view_check_searchType_main.chaos, \r\n\t\t\t\t\t\t\t--\tmodel_00, model_01, model_02, model_03, model_04, \r\n\t\t\t\t\t\t\t--\tbelt_00, belt_01, belt_02, belt_03, belt_04, belt_05, \r\n\t\t\t\t\t\t\t--\tsummon_0, summon_1, summon_2, summon_3, summon_4, summon_5, \r\n\t\t\t\t\t\t\t--\tmain_summon, sub_summon, remain_summon_time, \r\n\t\t\t\t\t\t\t\tcreate_time, delete_time, login_time, login_count, logout_time, play_time, chat_block_time, adv_chat_count, \r\n\t\t\t\t\t\t\t\tgmtool_view_check_searchType_main.name_changed,\r\n\t\t\t\t\t\t\t\tauto_used,\r\n\t\t\t\t\t\t\t\thuntaholic_point\r\n\t\t\t\t\t\t\t--\t,otp_value, otp_date, flag_list, client_info \r\n\t\t\t\t\t\t\tfrom gmtool_view_check_searchType_main with (nolock) \r\n\t\t\t\t\t\t\t--left join gmtool_view_check_searchType_Guild with(nolock) on gmtool_view_check_searchType_main.guild_id = gmtool_view_check_searchType_Guild.sid \r\n\t\t\t\t\t\t\twhere gmtool_view_check_searchType_main.sid not in (select top {1} sid from gmtool_view_check_searchType_main WITH (NOLOCK) where 1=1 {2} {3}) {2} {3}", result, result * (page - 1), text, text2);
		sqlCommand.CommandText = stringBuilder.ToString();
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		if (txtAccount.Value != "")
		{
			if (chkLike.Checked)
			{
				sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = $"%{txtAccount.Value.Trim()}%";
			}
			else
			{
				sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = $"{txtAccount.Value.Trim()}";
			}
		}
		if (txtCharacter.Value != "")
		{
			if (chkLike.Checked)
			{
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"%{txtCharacter.Value.Trim()}%";
			}
			else
			{
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"{txtCharacter.Value.Trim()}";
			}
		}
		if (chkCreateTime.Checked)
		{
			sqlCommand.Parameters.Add("@start", SqlDbType.DateTime).Value = txtStart.Text;
			sqlCommand.Parameters.Add("@end", SqlDbType.DateTime).Value = txtEnd.Text;
		}
		if (ddlJob.SelectedValue != "all")
		{
			sqlCommand.Parameters.Add("@job", SqlDbType.Int).Value = ddlJob.SelectedValue;
		}
		sqlCommand.Parameters.Add("@server", SqlDbType.VarChar).Value = ddlServer.SelectedValue;
		no = 1 + (page - 1) * result;
		grid_no = 1 + (page - 1) * result;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		sqlDataAdapter.Fill(dataSet);
		rptAvaterInfo.DataSource = dataSet;
		rptAvaterInfo.DataBind();
		sqlCommand.Dispose();
		sqlConnection.Close();
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		page = 1;
		GetCharacterList();
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		page = 1;
		if (base.Request.QueryString["page"] == null)
		{
			page = 1;
		}
		else
		{
			page = Convert.ToInt16(base.Request.QueryString["page"]);
		}
		SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(ddlServer.SelectedValue));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		string text = "";
		string text2 = "";
		string str = "";
		if (!int.TryParse(ddlViewCnt.SelectedItem.Value, out var result))
		{
			result = 100;
		}
		int listSize = 10;
		str += $"&pageSize={result}&server={ddlServer.SelectedValue}";
		if (txtAccount.Value != "")
		{
			text = ((!chkLike.Checked) ? (text + " and gmtool_view_check_searchType_main.account = @account") : (text + " and gmtool_view_check_searchType_main.account like @account"));
			str += $"&account={txtAccount.Value.Trim()}";
		}
		if (txtCharacter.Value != "")
		{
			text = ((!chkLike.Checked) ? (text + " and gmtool_view_check_searchType_main.name = @name") : (text + " and gmtool_view_check_searchType_main.name like @name"));
			str += $"&name={txtCharacter.Value.Trim()}";
		}
		if (chkUser.Checked)
		{
			str += "&user=1";
		}
		if (chkGM.Checked)
		{
			str += "&gm=1";
		}
		if (chkGameAccount.Checked)
		{
			str += "&game=1";
		}
		else
		{
			text += " and gmtool_view_check_searchType_main.account not like '@%' ";
		}
		if (chkAuto.Checked)
		{
			str += "&auto_used=1";
			text += " and auto_used = 1";
		}
		if (chkLike.Checked)
		{
			str += "&like=1";
		}
		if (chkCreateTime.Checked)
		{
			str += $"&create_time=1&start={txtStart.Text}&end={txtEnd.Text}";
			text = " and create_time between @start and @end ";
		}
		if (!chkSeeDeletedCharacter.Checked)
		{
			text += " and delete_time > '9999-01-01'  ";
		}
		else
		{
			str += "&see_delete_character=True";
		}
		if (chkUser.Checked && !chkGM.Checked)
		{
			text += " and permission = 0";
		}
		if (!chkUser.Checked && chkGM.Checked)
		{
			text += " and permission != 0";
		}
		if (chkConnect.Checked)
		{
			text += " and login_time > logout_time";
			str += "&connect=1";
		}
		if (!chkGameAccount.Checked)
		{
			text += " and account not like '@%'";
		}
		if (ddlSort1.SelectedIndex > 0)
		{
			if (chkSort1.Checked)
			{
				text2 += $"order by {ddlSort1.SelectedValue} desc";
				str += $"&sort1={ddlSort1.SelectedIndex}&desc1=1";
			}
			else
			{
				text2 += $"order by {ddlSort1.SelectedValue}";
				str += $"&sort1={ddlSort1.SelectedIndex}";
			}
		}
		if (ddlJob.SelectedValue != "all")
		{
			str += $"&job={ddlJob.SelectedValue}";
			text += " and job = @job";
		}
		if (text2 == "")
		{
			text2 = "order by exp desc";
		}
		sqlConnection.Open();
		sqlCommand.CommandText = $"select count(*) cnt from gmtool_view_check_searchType_main WITH (NOLOCK) where 1=1 {text}";
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		if (txtAccount.Value != "")
		{
			if (chkLike.Checked)
			{
				sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = $"%{txtAccount.Value.Trim()}%";
			}
			else
			{
				sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = $"{txtAccount.Value.Trim()}";
			}
		}
		if (txtCharacter.Value != "")
		{
			if (chkLike.Checked)
			{
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"%{txtCharacter.Value.Trim()}%";
			}
			else
			{
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"{txtCharacter.Value.Trim()}";
			}
		}
		if (chkCreateTime.Checked)
		{
			sqlCommand.Parameters.Add("@start", SqlDbType.DateTime).Value = txtStart.Text;
			sqlCommand.Parameters.Add("@end", SqlDbType.DateTime).Value = txtEnd.Text;
		}
		if (ddlJob.SelectedValue != "all")
		{
			sqlCommand.Parameters.Add("@job", SqlDbType.Int).Value = ddlJob.SelectedValue;
		}
		int totalCnt = (int)sqlCommand.ExecuteScalar();
		ltrTotalCnt.Text = totalCnt.ToString();
		ltrPageTop.Text = common.PageList(base.Request.Path, str, totalCnt, ref page, result, listSize);
		ltrPageBottom.Text = ltrPageTop.Text;
		StringBuilder stringBuilder = new StringBuilder(2000);
		stringBuilder.AppendFormat("\r\n\t\t\t\t\t\t\tselect top {0}\r\n\t\t\t\t\t\t\t\t@server as server,\r\n\t\t\t\t\t\t\t\tgmtool_view_check_searchType_main.sid+2009092 as sid, gmtool_view_check_searchType_main.name, account, account_id+2002060 as account_id, \r\n\t\t\t\t\t\t\t\tparty_id, \r\n\t\t\t\t\t\t\t\t-- IsNull((select name from gmtool_view_check_searchType_Party with (nolock) where sid = gmtool_view_check_searchType_main.party_id), '') as party_name,\r\n\t\t\t\t\t\t\t\tguild_id, \r\n\t\t\t\t\t\t\t\t--IsNull(gmtool_view_check_searchType_Guild.name, '') as guild_name,\r\n\t\t\t\t\t\t\t\t--IsNull(gmtool_view_check_searchType_Guild.icon_size, 0) as guild_icon_size,\r\n\t\t\t\t\t\t\t\t--IsNull(gmtool_view_check_searchType_Guild.icon, '') as guild_icon,\r\n\t\t\t\t\t\t\t\t--IsNull(gmtool_view_check_searchType_Guild.leader_id, 0) as guild_leader_id,\r\n\t\t\t\t\t\t\t--\tslot,\r\n\t\t\t\t\t\t\t\tpermission, \r\n\t\t\t\t\t\t\t--\tx, y, z, layer, \r\n\t\t\t\t\t\t\t\trace, sex, \r\n\t\t\t\t\t\t\t\tlv, exp, last_decreased_exp, \r\n\t\t\t\t\t\t\t--\thp, mp, stamina, \r\n\t\t\t\t\t\t\t\tjob, job_depth, jlv, jp, total_jp, \r\n\t\t\t\t\t\t\t--\tjob_0, job_1, job_2, jlv_0, jlv_1, jlv_2, \r\n\t\t\t\t\t\t\t\timmoral_point, cha, pkc, dkc, \r\n\t\t\t\t\t\t\t\tgmtool_view_check_searchType_main.gold,\r\n\t\t\t\t\t\t\t--\tIsNull((select sum(cnt) from gmtool_view_check_searchType_item with (nolock) where account_id = gmtool_view_check_searchType_main.account_id and account_id > 0 and code = 0), 0) as bank_gold,\r\n\t\t\t\t\t\t\t\tgmtool_view_check_searchType_main.chaos, \r\n\t\t\t\t\t\t\t--\tmodel_00, model_01, model_02, model_03, model_04, \r\n\t\t\t\t\t\t\t--\tbelt_00, belt_01, belt_02, belt_03, belt_04, belt_05, \r\n\t\t\t\t\t\t\t--\tsummon_0, summon_1, summon_2, summon_3, summon_4, summon_5, \r\n\t\t\t\t\t\t\t--\tmain_summon, sub_summon, remain_summon_time, \r\n\t\t\t\t\t\t\t\tcreate_time, delete_time, login_time, login_count, logout_time, play_time, chat_block_time, adv_chat_count, \r\n\t\t\t\t\t\t\t\tgmtool_view_check_searchType_main.name_changed,\r\n\t\t\t\t\t\t\t\tauto_used,\r\n\t\t\t\t\t\t\t\thuntaholic_point\r\n\t\t\t\t\t\t\t--\t,otp_value, otp_date, flag_list, client_info \r\n\t\t\t\t\t\t\tfrom gmtool_view_check_searchType_main with (nolock) left join gmtool_view_check_searchType_Guild with(nolock) on gmtool_view_check_searchType_main.guild_id = gmtool_view_check_searchType_Guild.sid \r\n\t\t\t\t\t\t\twhere gmtool_view_check_searchType_main.sid not in (select top {1} sid from gmtool_view_check_searchType_main WITH (NOLOCK) where 1=1 {2} {3}) {2} {3}", result, result * (page - 1), text, text2);
		sqlCommand.CommandText = stringBuilder.ToString();
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		if (txtAccount.Value != "")
		{
			if (chkLike.Checked)
			{
				sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = $"%{txtAccount.Value.Trim()}%";
			}
			else
			{
				sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = $"{txtAccount.Value.Trim()}";
			}
		}
		if (txtCharacter.Value != "")
		{
			if (chkLike.Checked)
			{
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"%{txtCharacter.Value.Trim()}%";
			}
			else
			{
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = $"{txtCharacter.Value.Trim()}";
			}
		}
		if (chkCreateTime.Checked)
		{
			sqlCommand.Parameters.Add("@start", SqlDbType.DateTime).Value = txtStart.Text;
			sqlCommand.Parameters.Add("@end", SqlDbType.DateTime).Value = txtEnd.Text;
		}
		if (ddlJob.SelectedValue != "all")
		{
			sqlCommand.Parameters.Add("@job", SqlDbType.Int).Value = ddlJob.SelectedValue;
		}
		sqlCommand.Parameters.Add("@server", SqlDbType.VarChar).Value = ddlServer.SelectedValue;
		no = 1 + (page - 1) * result;
		grid_no = 1 + (page - 1) * result;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		sqlDataAdapter.Fill(dataSet);
		sqlCommand.Dispose();
		sqlConnection.Close();
		StringBuilder stringBuilder2 = new StringBuilder(1024000);
		stringBuilder2.Append("account_id");
		stringBuilder2.Append(",");
		stringBuilder2.Append("account");
		stringBuilder2.Append(",");
		stringBuilder2.Append("sid");
		stringBuilder2.Append(",");
		stringBuilder2.Append("name");
		stringBuilder2.Append(",");
		stringBuilder2.Append("lv");
		stringBuilder2.Append(",");
		stringBuilder2.Append("exp");
		stringBuilder2.Append(",");
		stringBuilder2.Append("jlv");
		stringBuilder2.Append(",");
		stringBuilder2.Append("total_jp");
		stringBuilder2.Append(",");
		stringBuilder2.Append("jp");
		stringBuilder2.Append(",");
		stringBuilder2.Append("chaos");
		stringBuilder2.Append(",");
		stringBuilder2.Append("gold");
		stringBuilder2.Append(",");
		stringBuilder2.Append("guild_id");
		stringBuilder2.Append(",");
		stringBuilder2.Append("create_time");
		stringBuilder2.Append(",");
		stringBuilder2.Append("delete_time");
		stringBuilder2.Append(",");
		stringBuilder2.Append("login_time");
		stringBuilder2.Append(",");
		stringBuilder2.Append("login_count");
		stringBuilder2.Append(",");
		stringBuilder2.Append("logout_time");
		stringBuilder2.Append(",");
		stringBuilder2.Append("play_time");
		stringBuilder2.Append(",");
		stringBuilder2.Append("huntaholic_point");
		stringBuilder2.AppendLine();
		for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[i];
			stringBuilder2.Append(dataRow["account_id"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["account"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["sid"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["name"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["lv"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["exp"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["jlv"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["total_jp"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["jp"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["chaos"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["gold"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["guild_id"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["create_time"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["delete_time"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["login_time"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["login_count"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["logout_time"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["play_time"].ToString());
			stringBuilder2.Append(",");
			stringBuilder2.Append(dataRow["huntaholic_point"].ToString());
			stringBuilder2.AppendLine();
		}
		base.Response.Clear();
		base.Response.AddHeader("content-disposition", string.Format("attachment;filename=RappelzCharacter_{0}.csv", DateTime.Now.ToString("yyyyMMdd")));
		base.Response.ContentEncoding = Encoding.Default;
		base.Response.ContentType = "text/txt";
		base.Response.Write(stringBuilder2.ToString().Trim());
		base.Response.End();
	}

	protected string BindNo()
	{
		return grid_no++.ToString();
	}

	protected string BindName(object item)
	{
		_ = (string)DataBinder.Eval(item, "account");
		string text = (string)DataBinder.Eval(item, "name");
		string text2 = (string)DataBinder.Eval(item, "server");
		int num = (int)DataBinder.Eval(item, "sid");
		int num2 = (int)DataBinder.Eval(item, "account_id");
		if (text.Substring(0, 1) == "@")
		{
			try
			{
				text = string.Format("<span title='* name : {1}\n* delete_time : {2}'>{0}</span>", text.Substring(0, text.IndexOf(' ', 0)), text, ((DateTime)DataBinder.Eval(item, "delete_time")).ToString("yyyy-MM-dd HH:mm:ss"));
			}
			catch (Exception)
			{
			}
		}
		text = string.Format("<a href=\"javascript:;\" onclick=\"window.open('character_info.aspx?server={0}&character_id={1}&account_id={3}','character_name_info_{3}','left=50,top=50,width=1250,height=770,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=yes')\">{2}</a>", text2, num, text, num2);
		resource.GetJobResource(server.GetResourceServerNo((string)DataBinder.Eval(item, "server")), (int)DataBinder.Eval(item, "job"), out var job_name, out var job_icon);
		bool flag = false;
		if (DataBinder.Eval(item, "immoral_point").GetType() == typeof(float))
		{
			float num3 = (float)DataBinder.Eval(item, "immoral_point");
			if (num3 >= 100f)
			{
				flag = true;
			}
		}
		else
		{
			decimal d = (decimal)DataBinder.Eval(item, "immoral_point");
			if (d >= 100m)
			{
				flag = true;
			}
		}
		if (job_icon != "")
		{
			if (flag)
			{
				return string.Format("<img src='../img/icon/{1}' align='absmiddle' title='{2}'> <font color='red'>{0}</font>", text, job_icon, job_name);
			}
			return string.Format("<img src='../img/icon/{1}' align='absmiddle' title='{2}'> {0}", text, job_icon, job_name);
		}
		return $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{text}";
	}

	protected string BindPermission(object item)
	{
		if ((int)DataBinder.Eval(item, "permission") != 0)
		{
			string text = "";
			return string.Format("<img src='../img/icon_gm.gif' align='absmiddle'>{0}", (int)DataBinder.Eval(item, "permission") switch
			{
				100 => "[S]", 
				80 => "[A]", 
				70 => "[B]", 
				60 => "[C]", 
				_ => string.Format("[{0}]", (int)DataBinder.Eval(item, "permission")), 
			});
		}
		return "";
	}

	protected string BindAuto(object item)
	{
		if ((int)DataBinder.Eval(item, "auto_used") == 1)
		{
			return "<font color='blue'>AUTO</font>";
		}
		return "";
	}

	protected string BindLogin(object item)
	{
		if ((DateTime)DataBinder.Eval(item, "login_time") > (DateTime)DataBinder.Eval(item, "logout_time"))
		{
			return "Login";
		}
		TimeSpan timeSpan = DateTime.Now - (DateTime)DataBinder.Eval(item, "logout_time");
		if (timeSpan.Days > 0)
		{
			return $"logout {timeSpan.Days} days ago";
		}
		if (timeSpan.Hours > 0)
		{
			return $"logout {timeSpan.Hours} hours ago";
		}
		return $"logout {timeSpan.Minutes} minutes ago";
	}

	protected string BindPlayTime(object item)
	{
		int num = (int)DataBinder.Eval(item, "play_time");
		int num2 = num / 86400;
		int num3 = num % 86400 / 3600;
		int num4 = num % 86400 % 3600 / 60;
		string text = "";
		text = ((num2 > 0) ? (num2.ToString().PadLeft(3, '_') + "d ") : "");
		text += ((num2 > 0 || num3 > 0) ? (num3.ToString().PadLeft(2, '_') + "h ") : "");
		text += ((num2 > 0 || num3 > 0 || num4 > 0) ? (num4.ToString().PadLeft(2, '_') + "m") : "");
		return string.Format("<span title='{1} sec'>{0}<span>", text.Replace("_", "&nbsp;"), num.ToString("#,###"));
	}

	protected string BindGuildName(object item)
	{
		if ((int)DataBinder.Eval(item, "guild_id") > 0)
		{
			return link.GuildPopup((string)DataBinder.Eval(item, "server"), (int)DataBinder.Eval(item, "guild_id"), DataBinder.Eval(item, "guild_id").ToString());
		}
		return "";
	}
}
