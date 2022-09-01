using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;
using Rappelz.GMToolV3.BizDac;

public class Game_ChangeItemOwner : Page, IRequiresSessionState
{
	private string server;

	private int sid;

	private int owner_id;

	private int account_id;

	private int summon_id;

	private string account = "";

	private string character_name = "";

	private int code;

	private long cnt;

	private int level;

	private int enhance;

	private int flag;

	private int gcode;

	private int socket_0;

	private int socket_1;

	private int socket_2;

	private int socket_3;

	protected Literal ltrServer;

	protected Literal ltrOwner;

	protected Literal ltrItemSid;

	protected Literal ltrItemName;

	protected Literal ltrRemainTime;

	protected Literal ltrItemCnt;

	protected Literal ltrItemType;

	protected Literal ltrOldAccount;

	protected TextBox txtNewAccount;

	protected Button btnChangeAccount;

	protected Panel pnAccount;

	protected Literal ltrOldCharacter;

	protected TextBox txtNewAvatarname;

	protected Button btnChangeCharacter;

	protected Panel pnCharacter;

	protected Literal ltrItemInfo;

	protected Panel pnlSummon;

	protected Literal ltrItemLog;

	protected HtmlForm form1;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request.QueryString["server"] == null || base.Request.QueryString["server"] == "")
		{
			common.MsgBox(this, "Youneedtheservervalue", "您需要服务器值");
		}
		else if (base.Request.QueryString["sid"] == null || base.Request.QueryString["sid"] == "")
		{
			common.MsgBox(this, "YouneedItemsidvalue", "您需要Item sid值");
		}
		else
		{
			server = base.Request.QueryString["server"];
			sid = int.Parse(base.Request.QueryString["sid"]);
			ltrServer.Text = server;
			ltrItemSid.Text = sid.ToString();
			if (admin.Authority(server, "gmtool_edit_item"))
			{
				if (base.Request.QueryString["type"] == "account")
				{
					btnChangeAccount.Enabled = true;
				}
				else if (base.Request.QueryString["type"] == "character")
				{
					btnChangeCharacter.Enabled = true;
				}
			}
			GetItemInfo();
		}
		if (base.Request.QueryString["type"] == "account")
		{
			pnAccount.Visible = true;
			pnCharacter.Visible = false;
		}
		else if (base.Request.QueryString["type"] == "character")
		{
			pnCharacter.Visible = true;
			pnAccount.Visible = false;
		}
	}

	private void GetItemInfo()
	{
		int resource_server_no = (int)libs.server.GetServerInfo(server)["resource_id"];
		DataSet dataSet = null;
		IDataReader dataReader = null;
		string connectionString = libs.server.GetConnectionString(server);
		using (GameLogBiz gameLogBiz = new GameLogBiz())
		{
			dataSet = gameLogBiz.GetItemInfo(connectionString, sid);
		}
		dataReader = dataSet.CreateDataReader();
		int num = 0;
		_ = DateTime.Now;
		_ = DateTime.Now;
		character_name = "";
		account = "";
		_ = DateTime.Now;
		_ = DateTime.Now;
		string arg = "";
		StringBuilder stringBuilder = new StringBuilder(1000);
		if (dataReader.Read())
		{
			owner_id = (int)dataReader["owner_id"];
			account_id = (int)dataReader["account_id"];
			summon_id = (int)dataReader["summon_id"];
			code = (int)dataReader["code"];
			cnt = (long)dataReader["cnt"];
			level = (int)dataReader["level"];
			enhance = (int)dataReader["enhance"];
			flag = (int)dataReader["flag"];
			gcode = (int)dataReader["gcode"];
			socket_0 = (int)dataReader["socket_0"];
			socket_1 = (int)dataReader["socket_1"];
			socket_2 = (int)dataReader["socket_2"];
			socket_3 = (int)dataReader["socket_3"];
			num = (int)dataReader["remain_time"];
			_ = (DateTime)dataReader["create_time"];
			_ = (DateTime)dataReader["update_time"];
			character_name = (string)dataReader["character_name"];
			account = (string)dataReader["account"];
			if (flag == 0)
			{
				for (int i = 0; i < 4; i++)
				{
					int num2 = (int)dataReader[$"socket_{i}"];
					if (num2 > 0)
					{
						stringBuilder.AppendFormat("# Socket {0} : {1}<br />", i + 1, resource.GetItemToolTip(resource_server_no, num2, 0, 0, 0));
					}
				}
			}
			dataReader.Close();
		}
		DataRow itemResource = resource.GetItemResource(resource_server_no, code);
		if (itemResource == null)
		{
			ltrItemInfo.Text = "Item code 在ItemResource中不存在";
		}
		else
		{
			resource.GetStringResource(resource_server_no, (int)itemResource["name_id"]);
			ltrItemName.Text = resource.GetItemName(libs.server.GetResourceServerNo(server), code, level, enhance, 300);
			ltrItemCnt.Text = cnt.ToString("#,##0");
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
			ltrItemInfo.Text = resource.GetItemToolTip(libs.server.GetResourceServerNo(server), code, level, enhance, 300) + stringBuilder.ToString();
			int num3 = (int)itemResource["group"];
			if (itemResource["decrease_type"].ToString() == "0")
			{
				ltrRemainTime.Text = "0";
			}
			else if (itemResource["decrease_type"].ToString() == "1")
			{
				ltrRemainTime.Text = num.ToString();
			}
			else if (itemResource["decrease_type"].ToString() == "2")
			{
				int result = 0;
				int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddHours(result).AddSeconds(num);
				ltrRemainTime.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
			}
			if (num3 == 13 && socket_0 != 0)
			{
				GetSummonInfo(socket_0);
			}
		}
		if (owner_id != 0)
		{
			ltrOwner.Text = $"character_id : {owner_id}, name : {character_name} loginChk : {arg}";
			ltrOldCharacter.Text = owner_id.ToString();
		}
		if (account_id != 0)
		{
			ltrOwner.Text = $"account_id : {account_id}, account : {account}";
			ltrOldAccount.Text = account_id.ToString();
		}
		if (owner_id == 0 && account_id == 0)
		{
			ltrOwner.Text = $"No Owner - Deleteted Item";
		}
		using (AdminBiz adminBiz = new AdminBiz())
		{
			dataSet = adminBiz.GetGameCheatLog(base.Request.QueryString["server"], sid);
		}
		dataReader = dataSet.CreateDataReader();
		ltrItemLog.Text = "";
		while (dataReader.Read())
		{
			ltrItemLog.Text += string.Format("[{0}] {1} : <br> - {2} [{3}]<br>", ((DateTime)dataReader["log_date"]).ToString("yyyy-MM-dd HH:mm:ss"), (string)dataReader["admin_name"], (string)dataReader["cheat_type"], (string)dataReader["result_msg"]);
		}
		dataReader.Close();
		if (dataSet != null)
		{
			dataSet = null;
		}
	}

	private void GetSummonInfo(int socket_0)
	{
		int num = 0;
		DataSet dataSet = null;
		IDataReader dataReader = null;
		string connectionString = libs.server.GetConnectionString(server);
		using (GameLogBiz gameLogBiz = new GameLogBiz())
		{
			dataSet = gameLogBiz.GetSummonInfo(connectionString, sid);
			dataReader = dataSet.CreateDataReader();
		}
		while (dataReader.Read())
		{
			num++;
			if (socket_0 == (int)dataReader["sid"])
			{
				ltrItemInfo.Text += string.Format("<br><b>SUMMON sid : {0} / Trans : {1} / Lv : {2} /  prev1 Lv : {3} / prev2 Lv : {4}</b>", dataReader["sid"], dataReader["transform"], dataReader["lv"], dataReader["prev_level_01"], dataReader["prev_level_02"]);
			}
			else
			{
				ltrItemInfo.Text += string.Format("<br>SUMMON sid : {0} / Trans : {1} / Lv : {2} /  prev1 Lv : {3} / prev2 Lv : {4}", dataReader["sid"], dataReader["transform"], dataReader["lv"], dataReader["prev_level_01"], dataReader["prev_level_02"]);
			}
		}
		if (num > 1)
		{
			pnlSummon.Visible = true;
		}
		else
		{
			pnlSummon.Visible = false;
		}
		dataReader.Close();
		if (dataSet != null)
		{
			dataSet = null;
		}
	}

	protected void btnChangeAccount_Click(object sender, EventArgs e)
	{
		if (admin.Authority("gmtool_item_owner_change"))
		{
			int result = 0;
			int.TryParse(txtNewAccount.Text.Trim(), out result);
			if (result > 0)
			{
				string server_name = base.Request.QueryString["server"];
				string connectionString = libs.server.GetConnectionString(server);
				int num;
				using (GameLogBiz gameLogBiz = new GameLogBiz())
				{
					num = gameLogBiz.SetChagneItemOwnerByAccount(connectionString, result, account_id, sid);
				}
				switch (num)
				{
				case 0:
					acm.WriteCheatLog("ITEM_CHANGE_OWNER", server_name, account_id, account, owner_id, character_name, sid, 0, 0, 0, 0, 0, $"Item Owner(BANK) : {account} → character ID :{txtNewAccount.Text}");
					base.Response.Write("<script>opener.location.reload();</script>");
					common.MsgBox("success", base.Request.Url.ToString());
					break;
				case -1:
					common.MsgBox(this, "Thecharacterislogged", "The character is logged in or its name is incorrect. " + txtNewAccount.Text + ":" + account_id + ":" + sid);
					break;
				case -2:
					common.MsgBox(this, "Owner", "角色名称不正确.");
					break;
				case -3:
					common.MsgBox(this, "Owner", "该道具不正确.");
					break;
				case -4:
					common.MsgBox(this, "Owner", "游戏中有一个帐户已关联.");
					break;
				case -5:
					common.MsgBox(this, "Owner", "帐号不正确.");
					break;
				default:
					common.MsgBox(this, "Owner", "失败");
					break;
				}
			}
			else
			{
				common.MsgBox("Only Number.", base.Request.Url.ToString());
			}
		}
		else
		{
			common.MsgBox("No Permission", base.Request.Url.ToString());
		}
	}

	protected void btnChangeCharacter_Click(object sender, EventArgs e)
	{
		if (admin.Authority("gmtool_item_owner_change"))
		{
			int result = 0;
			int.TryParse(txtNewAvatarname.Text.Trim(), out result);
			if (result > 0)
			{
				string server_name = base.Request.QueryString["server"];
				string connectionString = libs.server.GetConnectionString(server);
				int num;
				using (GameLogBiz gameLogBiz = new GameLogBiz())
				{
					num = gameLogBiz.SetChagneItemOwnerByCharacter(connectionString, result, owner_id, sid);
				}
				if (num == 0)
				{
					acm.WriteCheatLog("ITEM_CHANGE_OWNER", server_name, account_id, account, owner_id, character_name, sid, 0, 0, 0, 0, 0, $"Item Owner(inventory) : {character_name} → {txtNewAvatarname.Text}");
					base.Response.Write("<script>opener.location.reload();</script>");
					common.MsgBox("success", base.Request.Url.ToString());
				}
				switch (num)
				{
				case -1:
					common.MsgBox(this, "Owner", "角色已登录或名称不正确. " + txtNewAvatarname.Text + ":" + character_name + ":" + owner_id + ":" + sid);
					break;
				case -2:
					common.MsgBox(this, "Owner", "角色名称不正确.");
					break;
				case -3:
					common.MsgBox(this, "Owner", "该道具不正确.");
					break;
				default:
					common.MsgBox("fail");
					break;
				}
			}
			else
			{
				common.MsgBox("Only Number.", base.Request.Url.ToString());
			}
		}
		else
		{
			common.MsgBox("No Permission", base.Request.Url.ToString());
		}
	}
}
