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

public class Game_item_insert_useSid : Page, IRequiresSessionState
{
	protected TextBox TextBox1;

	protected TextBox txtCode;

	protected Button Button1;

	protected Button Button2;

	protected Literal ltrServer;

	protected Literal ltrOwner;

	protected Literal ltrItemSid;

	protected Literal ltrItemName;

	protected Literal ltrItemCnt;

	protected Literal ltrItemType;

	protected Literal ltrCode;

	protected Literal ltrState;

	protected HiddenField hdnState;

	protected Literal ltrCreateTime;

	protected Literal ltrItemInfo;

	protected HtmlForm form1;

	private string server;

	private int character_id;

	private int account_id;

	private bool item_state;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		server = base.Request.QueryString["server"];
		int.TryParse(base.Request.QueryString["character_id"], out character_id);
		int.TryParse(base.Request.QueryString["account_id"], out account_id);
		ltrServer.Text = server;
	}

	private void GetSummonInfo(int socket_0, long sid)
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
		}
		sqlDataReader.Close();
		sqlCommand.Dispose();
	}

	private void GetItemInfo(long sid, int itemcode)
	{
		int resource_server_no = (int)libs.server.GetServerInfo(server)["resource_id"];
		SqlConnection sqlConnection = new SqlConnection(libs.server.GetConnectionString(server));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "dbo.gmtool_v2_get_item_info_use_code";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = sid;
		sqlCommand.Parameters.Add("@code", SqlDbType.Int).Value = itemcode;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		long num6 = 0L;
		int item_level = 0;
		int item_enhance = 0;
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
			num3 = (int)sqlDataReader["auction_id"];
			num4 = (int)sqlDataReader["keeping_id"];
			num5 = (int)sqlDataReader["code"];
			num6 = (long)sqlDataReader["cnt"];
			item_level = (int)sqlDataReader["level"];
			item_enhance = (int)sqlDataReader["enhance"];
			num7 = (int)sqlDataReader["flag"];
			_ = (int)sqlDataReader["gcode"];
			num8 = (int)sqlDataReader["socket_0"];
			_ = (int)sqlDataReader["socket_1"];
			_ = (int)sqlDataReader["socket_2"];
			_ = (int)sqlDataReader["socket_3"];
			_ = (int)sqlDataReader["remain_time"];
			now = (DateTime)sqlDataReader["create_time"];
			_ = (DateTime)sqlDataReader["update_time"];
			arg = (string)sqlDataReader["character_name"];
			arg2 = (string)sqlDataReader["account"];
			ltrCreateTime.Text = now.ToString();
			if (num7 == 0)
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
			sqlDataReader.Close();
			item_state = true;
			ltrCode.Text = num5.ToString();
			hdnState.Value = "t";
			ltrState.Text = "아이템 테이블";
		}
		else
		{
			sqlDataReader.Close();
			sqlCommand.CommandText = "dbo.gmtool_v2_get_item_info_delete";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = sid;
			sqlCommand.Parameters.Add("@code", SqlDbType.Int).Value = itemcode;
			SqlDataReader sqlDataReader2 = sqlCommand.ExecuteReader();
			if (sqlDataReader2.Read())
			{
				num = (int)sqlDataReader2["owner_id"];
				num2 = (int)sqlDataReader2["account_id"];
				_ = (int)sqlDataReader2["summon_id"];
				num5 = (int)sqlDataReader2["code"];
				num6 = (long)sqlDataReader2["cnt"];
				item_level = (int)sqlDataReader2["level"];
				item_enhance = (int)sqlDataReader2["enhance"];
				num7 = (int)sqlDataReader2["flag"];
				_ = (int)sqlDataReader2["gcode"];
				num8 = (int)sqlDataReader2["socket_0"];
				_ = (int)sqlDataReader2["socket_1"];
				_ = (int)sqlDataReader2["socket_2"];
				_ = (int)sqlDataReader2["socket_3"];
				_ = (int)sqlDataReader2["remain_time"];
				now = (DateTime)sqlDataReader2["create_time"];
				_ = (DateTime)sqlDataReader2["update_time"];
				arg = (string)sqlDataReader2["character_name"];
				arg2 = (string)sqlDataReader2["account"];
				ltrCreateTime.Text = now.ToString();
				if (num7 == 0)
				{
					for (int j = 0; j < 4; j++)
					{
						int num10 = (int)sqlDataReader2[$"socket_{j}"];
						if (num10 > 0)
						{
							stringBuilder.AppendFormat("# Socket {0} : {1}<br />", j + 1, resource.GetItemToolTip(resource_server_no, num10, 0, 0, 0));
						}
					}
				}
				sqlDataReader2.Close();
				item_state = false;
				ltrCode.Text = num5.ToString();
				hdnState.Value = "f";
				ltrState.Text = "삭제 테이블";
			}
			else
			{
				sqlDataReader2.Close();
				item_state = false;
				hdnState.Value = "f";
				common.MsgBox("no item", base.Request.Url.ToString());
			}
		}
		DataRow itemResource = resource.GetItemResource(resource_server_no, num5);
		if (itemResource == null)
		{
			ltrItemInfo.Text = "The item code does not exist in ItemResource";
			Button2.Enabled = false;
		}
		else
		{
			ltrItemSid.Text = TextBox1.Text;
			resource.GetStringResource(resource_server_no, (int)itemResource["name_id"]);
			ltrItemName.Text = resource.GetItemName(libs.server.GetResourceServerNo(server), num5, item_level, item_enhance, 300);
			ltrItemCnt.Text = num6.ToString("#,##0");
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
			ltrItemInfo.Text = resource.GetItemToolTip(libs.server.GetResourceServerNo(server), num5, item_level, item_enhance, 300) + stringBuilder.ToString();
			int num11 = (int)itemResource["group"];
			long num12 = Convert.ToInt32(string.Format("{0}", itemResource["item_use_flag"].ToString()));
			string text = "";
			while (num12 > 0)
			{
				text = ((num12 % 2 != 0) ? (text + "1") : (text + "0"));
				num12 /= 2;
			}
			if (num11 == 13 && num8 != 0)
			{
				GetSummonInfo(num8, sid);
			}
			if (num != 0)
			{
				ltrOwner.Text = $"character_id : {num}, name : {arg}";
			}
			if (num2 != 0)
			{
				ltrOwner.Text = $"account_id : {num2}, account : {arg2}";
			}
			if (num3 != 0)
			{
				ltrOwner.Text = "Auction id : " + num3;
			}
			if (num4 != 0)
			{
				ltrOwner.Text = "Keeping id : " + num4;
			}
			if (num == 0 && num2 == 0 && num3 == 0 && num4 == 0)
			{
				ltrOwner.Text = $"No Owner - Deleteted Item";
				Button2.Enabled = true;
			}
		}
		sqlConnection.Close();
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		long.TryParse(TextBox1.Text, out var result);
		int.TryParse(txtCode.Text, out var result2);
		GetItemInfo(result, result2);
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		string server_name = server;
		long result = 0L;
		int result2 = 0;
		long.TryParse(ltrItemSid.Text.Trim(), out result);
		int.TryParse(ltrCode.Text.Trim(), out result2);
		if (admin.Authority("gmtool_item_insert_usesid"))
		{
			SqlConnection sqlConnection = new SqlConnection(libs.server.GetConnectionString(server));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			if (result > 0)
			{
				if (hdnState.Value == "t")
				{
					sqlConnection.Open();
					try
					{
						sqlCommand.Connection = sqlConnection;
						sqlCommand.CommandText = "dbo.gmtool_cheat_give_item_useSid";
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.Clear();
						sqlCommand.Parameters.AddWithValue("@sid", result);
						sqlCommand.Parameters.AddWithValue("@code", result2);
						sqlCommand.Parameters.AddWithValue("@chatacter_id", character_id);
						int num = (int)sqlCommand.ExecuteScalar();
						sqlCommand.Dispose();
						sqlConnection.Close();
						switch (num)
						{
							case -1:
								common.MsgBox("owner!");
								break;
							case -2:
								common.MsgBox("account!");
								break;
							case -3:
								common.MsgBox("summon!");
								break;
							case -4:
								common.MsgBox("auction");
								break;
							case -5:
								common.MsgBox("keeping box");
								break;
							case -10:
								common.MsgBox("error1");
								break;
							case -100:
								common.MsgBox("error2");
								break;
						}
						if (num == 0)
						{
							acm.WriteCheatLog("GIVE_ITEM_USE_SID", server_name, account_id, account_id.ToString(), character_id, character_id.ToString(), result, 0, 0, 0, 0, 0, "give item (inventory) Success");
							common.MsgBox("success");
						}
						else
						{
							acm.WriteCheatLog("GIVE_ITEM_USE_SID", server_name, account_id, account_id.ToString(), character_id, character_id.ToString(), result, 0, 0, 0, 0, 0, "give item (inventory) Fail" + num);
						}
					}
					catch (Exception ex)
					{
						common.MsgBox("fail" + ex.Message);
					}
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
				else
				{
					common.MsgBox("삭제 테이블" + result + "," + character_id);
					sqlConnection.Open();
					sqlCommand.Connection = sqlConnection;
					sqlCommand.CommandText = "dbo.gmtool_cheat_give_item_useSid_deletetable";
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.AddWithValue("@sid", result);
					sqlCommand.Parameters.AddWithValue("@code", result2);
					sqlCommand.Parameters.AddWithValue("@chatacter_id", character_id);
					string text = sqlCommand.ExecuteScalar().ToString();
					sqlCommand.Dispose();
					sqlConnection.Close();
					switch (text)
					{
						case "-1":
							common.MsgBox("max sid");
							break;
						case "-2":
							common.MsgBox("item sid error");
							break;
						case "-3":
							common.MsgBox("item sid error2");
							break;
						case "-100":
							common.MsgBox("error_deleteitem");
							break;
					}
					if (text == "0")
					{
						acm.WriteCheatLog("GIVE_ITEM_USE_SID", server_name, account_id, account_id.ToString(), character_id, character_id.ToString(), result, 0, 0, 0, 0, 0, "give item (inventory) Success_delete_item");
						common.MsgBox("success");
					}
					else
					{
						acm.WriteCheatLog("GIVE_ITEM_USE_SID", server_name, account_id, account_id.ToString(), character_id, character_id.ToString(), result, 0, 0, 0, 0, 0, "give item (inventory) Fail_delete_item" + text);
					}
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
			else
			{
				common.MsgBox("item sid error");
			}
		}
		else
		{
			common.MsgBox("No Permission");
		}
		GetItemInfo(result, result2);
	}
}
