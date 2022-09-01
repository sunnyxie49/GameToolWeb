using System;
using System.Collections;
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

public class Game_edit_item : Page, IRequiresSessionState
{
	private SqlConnection con;

	private SqlCommand cmd;

	private string server;

	private long sid;

	protected int oldSummonEnhance;

	private int SummonCode;

	protected Literal ltrServer;

	protected Literal ltrOwner;

	protected Literal ltrItemSid;

	protected Literal ltrItemName;

	protected Literal ltrItemCnt;

	protected Literal ltrItemType;

	protected Literal ltrCreateTime;

	protected Literal ltrItemInfo;

	protected RadioButtonList rblSummonList;

	protected Button btnChangeSummonSid;

	protected Panel pnlSummon;

	protected Literal ltrItemLog;

	protected Literal ltrPetName;

	protected TextBox txtPetName;

	protected Button btnPetNameChange;

	protected HtmlGenericControl divPetInfo;

	protected Repeater rptAwakenInfo;

	protected HtmlGenericControl divAwakenInfo;

	protected TextBox txtEnhance2;

	protected CheckBox chkLvinItialization;

	protected Button btnChange2;

	protected HtmlGenericControl divSummon;

	protected TextBox txtEnhance;

	protected TextBox txtLevel;

	protected TextBox txtCnt;

	protected Literal ltrBeltSlot;

	protected TextBox txtBeltSlot;

	protected CheckBox chkTaming;

	protected Literal ltrRemainTime;

	protected TextBox txtRemainTime;

	protected Button btnChange;

	protected HtmlGenericControl divItem;

	protected Button btnDel;

	protected HiddenField hdnSummonCode;

	protected HiddenField hdnOldSummonEnhance;

	protected HtmlForm form1;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.Authority())
		{
			common.MsgBox("没有权限", "关闭");
		}
		if (base.Request.QueryString["server"] == null || base.Request.QueryString["server"] == "")
		{
			common.MsgBox(this, "CEditItem", "You need the server value");
		}
		else if (base.Request.QueryString["sid"] == null || base.Request.QueryString["sid"] == "")
		{
			common.MsgBox(this, "CEditItem", "You need Item sid value");
		}
		else
		{
			server = base.Request.QueryString["server"];
			sid = long.Parse(base.Request.QueryString["sid"]);
			ltrServer.Text = server;
			ltrItemSid.Text = sid.ToString();
			con = new SqlConnection(libs.server.GetConnectionString(server));
			cmd = new SqlCommand();
			cmd.Connection = con;
			if (!base.IsPostBack)
			{
				GetItemInfo();
			}
		}
		if (admin.Authority(server, "gmtool_edit_item"))
		{
			btnChange.Enabled = true;
			btnDel.Enabled = true;
			btnPetNameChange.Enabled = true;
		}
	}

	private void GetItemInfo()
	{
		int resource_server_no = (int)libs.server.GetServerInfo(server)["resource_id"];
		con.Open();
		cmd.CommandText = "gmtool_v2_get_item_info";
		cmd.CommandType = CommandType.StoredProcedure;
		cmd.Parameters.Clear();
		cmd.Parameters.Add("@sid", SqlDbType.BigInt).Value = sid;
		SqlDataReader sqlDataReader = cmd.ExecuteReader();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		long num4 = 0L;
		int item_level = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		DateTime now = DateTime.Now;
		_ = DateTime.Now;
		string arg = "";
		string arg2 = "";
		StringBuilder stringBuilder = new StringBuilder(1000);
		if (sqlDataReader.Read())
		{
			num = (int)sqlDataReader["owner_id"];
			num2 = (int)sqlDataReader["account_id"];
			_ = (int)sqlDataReader["summon_id"];
			num3 = (int)sqlDataReader["code"];
			num4 = (long)sqlDataReader["cnt"];
			item_level = (int)sqlDataReader["level"];
			num5 = (int)sqlDataReader["enhance"];
			num6 = (int)sqlDataReader["flag"];
			_ = (int)sqlDataReader["gcode"];
			num7 = (int)sqlDataReader["socket_0"];
			_ = (int)sqlDataReader["socket_1"];
			_ = (int)sqlDataReader["socket_2"];
			_ = (int)sqlDataReader["socket_3"];
			num8 = (int)sqlDataReader["remain_time"];
			now = (DateTime)sqlDataReader["create_time"];
			_ = (DateTime)sqlDataReader["update_time"];
			arg = (string)sqlDataReader["character_name"];
			arg2 = (string)sqlDataReader["account"];
			ltrCreateTime.Text = now.ToString();
			oldSummonEnhance = (int)sqlDataReader["enhance"];
			if (num6 == 0)
			{
				for (int i = 0; i < 4; i++)
				{
					int num9 = (int)sqlDataReader[$"socket_{i}"];
					if (num9 > 0)
					{
						stringBuilder.AppendFormat("# Socket {0} : {1}<br />", i + 1, resource.GetItemToolTip(resource_server_no, num9, 0, 0, 0));
					}
				}
			}
		}
		sqlDataReader.NextResult();
		if (sqlDataReader.Read() && sqlDataReader.HasRows && sqlDataReader["name"] != null)
		{
			divPetInfo.Visible = true;
			ltrPetName.Text = sqlDataReader["name"].ToString();
		}
		sqlDataReader.NextResult();
		if (sqlDataReader.Read() && sqlDataReader.HasRows && sqlDataReader["sid"] != null)
		{
			divAwakenInfo.Visible = true;
			ArrayList arrayList = new ArrayList();
			for (int j = 1; j <= 5; j++)
			{
				if (sqlDataReader["type_0" + j] != null)
				{
					arrayList.Add(new string[3]
					{
						sqlDataReader["type_0" + j].ToString(),
						sqlDataReader["value_0" + j].ToString(),
						sqlDataReader["data_0" + j].ToString()
					});
				}
			}
			rptAwakenInfo.DataSource = arrayList;
			rptAwakenInfo.ItemDataBound += rptAwakenInfo_ItemDataBound;
			rptAwakenInfo.DataBind();
		}
		if (!sqlDataReader.IsClosed)
		{
			sqlDataReader.Close();
		}
		TextBox textBox = txtEnhance;
		TextBox textBox2 = txtEnhance2;
		string text2 = (hdnOldSummonEnhance.Value = num5.ToString());
		string text5 = (textBox.Text = (textBox2.Text = text2));
		txtLevel.Text = item_level.ToString();
		txtCnt.Text = num4.ToString();
		txtBeltSlot.Text = num7.ToString();
		DataRow itemResource = resource.GetItemResource(resource_server_no, num3);
		if (itemResource == null)
		{
			ltrItemInfo.Text = "The item code does not exist in ItemResource";
		}
		else
		{
			resource.GetStringResource(resource_server_no, (int)itemResource["name_id"]);
			ltrItemName.Text = resource.GetItemName(libs.server.GetResourceServerNo(server), num3, item_level, num5, 300);
			ltrItemCnt.Text = num4.ToString("#,##0");
			ltrItemType.Text = "";
			ltrItemType.Text = resource.GetItemType((int)itemResource["type"]);
			string itemGroup = resource.GetItemGroup((int)itemResource["group"]);
			string itemClass = resource.GetItemClass((int)itemResource["class"]);
			if (itemGroup != "기타")
			{
				ltrItemType.Text = resource.GetItemGroup((int)itemResource["group"]);
			}
			if (itemClass != "기타")
			{
				ltrItemType.Text = resource.GetItemClass((int)itemResource["class"]);
			}
			int num10 = (int)itemResource["group"];
			if (num10 == 7)
			{
				ltrItemInfo.Text = resource.GetItemToolTip(libs.server.GetResourceServerNo(server), num3, item_level, num5, 300);
			}
			else
			{
				ltrItemInfo.Text = resource.GetItemToolTip(libs.server.GetResourceServerNo(server), num3, item_level, num5, 300) + stringBuilder.ToString();
			}
			if (num10 >= 1 && num10 <= 9)
			{
				txtEnhance.Enabled = true;
				txtLevel.Enabled = true;
			}
			if (num10 == 10)
			{
				txtEnhance.Enabled = true;
			}
			long num11 = Convert.ToInt32(string.Format("{0}", itemResource["item_use_flag"].ToString()));
			string text6 = "";
			while (num11 > 0)
			{
				text6 = ((num11 % 2 != 0) ? (text6 + "1") : (text6 + "0"));
				num11 /= 2;
			}
			if (text6[6].ToString() == "1")
			{
				txtCnt.Enabled = true;
			}
			if (itemResource["decrease_type"].ToString() == "0")
			{
				ltrRemainTime.Text = "0";
				txtRemainTime.Text = "0";
				txtRemainTime.Enabled = false;
			}
			else if (itemResource["decrease_type"].ToString() == "1")
			{
				ltrRemainTime.Text = num8.ToString();
				txtRemainTime.Text = num8.ToString();
				txtRemainTime.Enabled = true;
			}
			else if (itemResource["decrease_type"].ToString() == "2")
			{
				int result = 0;
				int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddHours(result).AddSeconds(num8);
				ltrRemainTime.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
				txtRemainTime.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
				txtRemainTime.Enabled = true;
			}
			if (num10 == 13)
			{
				if ((num6 & int.MinValue) == int.MinValue)
				{
					divItem.Visible = false;
					divSummon.Visible = true;
					chkTaming.Checked = true;
					txtEnhance2.Enabled = true;
					chkLvinItialization.Enabled = true;
					if (num5 < 5)
					{
						btnChange2.Enabled = true;
					}
					else
					{
						btnChange2.Enabled = false;
					}
					if (num7 != 0)
					{
						GetSummonInfo(num7);
					}
					if ((num6 & 0x8000000) == 134217728)
					{
						ltrItemInfo.Text += "<br/>In the farm<br/>";
					}
				}
				else
				{
					chkTaming.Enabled = true;
					txtEnhance2.Enabled = false;
					chkLvinItialization.Enabled = false;
				}
			}
			if (num10 == 7)
			{
				Literal literal = ltrItemInfo;
				literal.Text = literal.Text + "Belt Slot : 1 + " + num7;
				ltrBeltSlot.Visible = true;
				txtBeltSlot.Visible = true;
				txtBeltSlot.Enabled = true;
			}
		}
		if (num != 0)
		{
			ltrOwner.Text = $"character_id : {num}, name : {arg}";
		}
		if (num2 != 0)
		{
			ltrOwner.Text = $"account_id : {num2}, account : {arg2}";
		}
		if (num == 0 && num2 == 0)
		{
			ltrOwner.Text = $"No Owner - Deleteted Item";
		}
		con.Close();
		SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["gmtool"].ConnectionString);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = "select log_date, admin_name, cheat_type, result_msg from dbo.tb_gmtool_game_cheat_log with (nolock) where server=@server and item_id = @item_id order by sid";
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@server", SqlDbType.VarChar).Value = server;
		sqlCommand.Parameters.Add("@item_id", SqlDbType.BigInt).Value = sid;
		sqlConnection.Open();
		SqlDataReader sqlDataReader2 = sqlCommand.ExecuteReader();
		ltrItemLog.Text = "";
		while (sqlDataReader2.Read())
		{
			ltrItemLog.Text += string.Format("[{0}] {1} : <br> - {2} [{3}]<br>", ((DateTime)sqlDataReader2["log_date"]).ToString("yyyy-MM-dd HH:mm:ss"), (string)sqlDataReader2["admin_name"], (string)sqlDataReader2["cheat_type"], (string)sqlDataReader2["result_msg"]);
		}
		sqlDataReader2.Close();
		sqlConnection.Close();
	}

	private void rptAwakenInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
		{
			string[] array = (string[])e.Item.DataItem;
			Literal literal = (Literal)e.Item.FindControl("ltrType");
			Literal literal2 = (Literal)e.Item.FindControl("ltrValue");
			Literal literal3 = (Literal)e.Item.FindControl("ltrData");
			literal.Text = array[0];
			literal2.Text = array[1];
			literal3.Text = array[2];
		}
	}

	private void GetSummonInfo(int socket_0)
	{
		SqlConnection sqlConnection = new SqlConnection(libs.server.GetConnectionString(server));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "gmtool_GetSummonInfo_2";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = sid;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		int num = 0;
		while (sqlDataReader.Read())
		{
			num++;
			if (socket_0 == (int)sqlDataReader["sid"])
			{
				ltrItemInfo.Text += string.Format("<br><b>SUMMON sid : {0} / Trans : {1} / Lv : {2} /  prev1 Lv : {3} / prev2 Lv : {4}</b>", sqlDataReader["sid"], sqlDataReader["transform"], sqlDataReader["lv"], sqlDataReader["prev_level_01"], sqlDataReader["prev_level_02"]);
			}
			else
			{
				ltrItemInfo.Text += string.Format("<br>SUMMON sid : {0} / Trans : {1} / Lv : {2} /  prev1 Lv : {3} / prev2 Lv : {4}", sqlDataReader["sid"], sqlDataReader["transform"], sqlDataReader["lv"], sqlDataReader["prev_level_01"], sqlDataReader["prev_level_02"]);
			}
			hdnSummonCode.Value = sqlDataReader["code"].ToString();
			if (!base.IsPostBack)
			{
				rblSummonList.Items.Add(new ListItem(string.Format("SUMMON sid : {0} / Trans : {1} / Lv : {2} /  prev1 Lv : {3} / prev2 Lv : {4}", sqlDataReader["sid"], sqlDataReader["transform"], sqlDataReader["lv"], sqlDataReader["prev_level_01"], sqlDataReader["prev_level_02"]), sqlDataReader["sid"].ToString()));
			}
		}
		if (!base.IsPostBack && num > 0)
		{
			rblSummonList.Items.FindByValue(socket_0.ToString()).Selected = true;
		}
		if (num > 1)
		{
			pnlSummon.Visible = true;
		}
		else
		{
			pnlSummon.Visible = false;
		}
		sqlDataReader.Close();
		sqlCommand.Clone();
	}

	protected void btnChange_Click(object sender, EventArgs e)
	{
		int enhance = int.Parse(txtEnhance.Text);
		int level = int.Parse(txtLevel.Text);
		long cnt = long.Parse(txtCnt.Text);
		bool @checked = chkTaming.Checked;
		int socket_ = int.Parse(txtBeltSlot.Text);
		int result = 0;
		string result_msg2;
		if (txtRemainTime.Enabled)
		{
			if (!int.TryParse(txtRemainTime.Text, out result))
			{
				DateTime d = DateTime.Parse(txtRemainTime.Text);
				int result2 = 0;
				int.TryParse(ConfigurationManager.AppSettings["GMT"], out result2);
				result = int.Parse((d - new DateTime(1970, 1, 1).AddHours(result2)).TotalSeconds.ToString());
			}
			if (acm.EditItem(server, sid, enhance, level, cnt, @checked, result, socket_, out var result_msg))
			{
				common.MsgBox(this, "CEditItem", "Modify :  Success \n\n" + result_msg);
			}
			else
			{
				common.MsgBox(this, "CEditItem", "Modify :  Failure \n\n" + result_msg);
			}
		}
		else if (acm.EditItem(server, sid, enhance, level, cnt, @checked, socket_, out result_msg2))
		{
			common.MsgBox(this, "CEditItem", "Modify :  Success \n\n" + result_msg2);
		}
		else
		{
			common.MsgBox(this, "CEditItem", "Modify :  Failure \n\n" + result_msg2);
		}
		GetItemInfo();
	}

	protected void btnPetNameChange_Click(object sender, EventArgs e)
	{
		string text = txtPetName.Text;
		con.Open();
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = con;
		sqlCommand.CommandText = "gmtool_v3_cheat_change_pet_name";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@IN_SID", SqlDbType.BigInt).Value = sid;
		sqlCommand.Parameters.Add("@IN_NAME", SqlDbType.NVarChar, 36).Value = text;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		if (sqlDataReader.Read())
		{
			bool flag = (bool)sqlDataReader["result"];
			string str = (string)sqlDataReader["result_msg"];
			if (flag)
			{
				common.MsgBox(this, "CEditItem", "Modify :  Success \n\n" + str);
			}
			else
			{
				common.MsgBox(this, "CEditItem", "Modify :  Failure \n\n" + str);
			}
		}
		con.Close();
		GetItemInfo();
	}

	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (acm.DeleteItem(server, sid, out var result_msg))
		{
			common.MsgBox(this, "CEditItem", "Modify :  Success \n\n" + result_msg);
		}
		else
		{
			common.MsgBox(this, "CEditItem", "Modify :  Failure \n\n" + result_msg);
		}
		GetItemInfo();
	}

	protected void btnChangeSummonSid_Click(object sender, EventArgs e)
	{
		int summon_id = int.Parse(rblSummonList.SelectedValue);
		if (acm.SetCreaturedSid(server, sid, summon_id, out var result_msg))
		{
			common.MsgBox(this, "CEditItem", "Modify :  Success \n\n" + result_msg);
		}
		else
		{
			common.MsgBox(this, "CEditItem", "Modify :  Failure \n\n" + result_msg);
		}
		GetItemInfo();
	}

	protected void btnChange2_Click(object sender, EventArgs e)
	{
		if (admin.Authority("gmtool_summon_enhance_change"))
		{
			int enhance = int.Parse(txtEnhance2.Text);
			int result = 0;
			int result2 = 0;
			int.TryParse(hdnSummonCode.Value, out result);
			int.TryParse(hdnOldSummonEnhance.Value, out result2);
			bool @checked = chkLvinItialization.Checked;
			_ = chkTaming.Checked;
			if (acm.EditSummon(server, sid, enhance, @checked, result, result2, out var result_msg))
			{
				common.MsgBox(this, "CEditItem", "Modify :  Success \n\n" + result_msg);
			}
			else
			{
				common.MsgBox(this, "CEditItem", "Modify :  Failure \n\n" + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CEditItem", "No Permission");
		}
		GetItemInfo();
	}
}
