using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_character_info_item : Page, IRequiresSessionState
{
	protected DropDownList ddlServer;

	protected PlaceHolder PlaceHolder1;

	protected CheckBox chkSeeDeletedCharacter;

	protected HyperLink hlInfo;

	protected HyperLink hlItem;

	protected HyperLink hlGiveItem;

	protected HyperLink hlSkill;

	protected HyperLink hlSummon;

	protected HyperLink hlQuest;

	protected HyperLink hlBank;

	protected HyperLink hlShop;

	protected HyperLink hlTitle;

	protected HyperLink hlArena;

	protected TextBox txtItemCode;

	protected TextBox txtItemEnhance;

	protected TextBox txtItemLevel;

	protected TextBox txtItemCnt;

	protected Button btnGiveItem;

	protected Panel pnlItemInsert;

	protected PlaceHolder PlaceHolder2;

	protected HtmlForm form1;

	private int account_id;

	private int character_id;

	private string selected_server;

	private int resource_db_no;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.Authority())
		{
			common.MsgBox("没有权限", "关闭");
		}
		account_id = common.ToInt(base.Request.Params["account_id"], 0);
		character_id = common.ToInt(base.Request.Params["character_id"], 0);
		if (!base.IsPostBack)
		{
			chkSeeDeletedCharacter.Checked = ((string.Format("{0}", base.Request.Params["see_delete_character"]) == "True") ? true : false);
			server.SetServer(ref ddlServer, base.Request.QueryString["server"]);
		}
		selected_server = ddlServer.SelectedValue;
		resource_db_no = server.GetResourceServerNo(selected_server);
		hlInfo.NavigateUrl = GetCharacterInfoNavigateUrl("character_info.aspx");
		hlSkill.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_skill.aspx");
		hlItem.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_item.aspx");
		hlGiveItem.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_give_item.aspx");
		hlSummon.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_summon.aspx");
		hlQuest.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_quest.aspx");
		hlBank.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_bank.aspx");
		hlTitle.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_title.aspx");
		hlArena.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_arena.aspx");
		if (ConfigurationManager.AppSettings["use_item_shop"] == "true")
		{
			hlShop.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_shop.aspx");
			return;
		}
		hlShop.Text = "";
		hlShop.NavigateUrl = "";
	}

	private string GetCharacterInfoNavigateUrl(string path)
	{
		return $"{path}?server={selected_server}&account_id={account_id}&character_id={character_id}&see_delete_character={chkSeeDeletedCharacter.Checked}";
	}

	protected void Page_PreRender(object sender, EventArgs e)
	{
		GetCharacterInfo();
	}

	private void GetCharacterInfo()
	{
		selected_server = ddlServer.SelectedValue;
		SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(ddlServer.SelectedValue));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "gmtool_v2_get_character_info_item";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
		sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		sqlDataAdapter.Fill(dataSet);
		sqlCommand.Dispose();
		sqlConnection.Close();
		bool flag = false;
		for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
		{
			if ((int)dataSet.Tables[0].Rows[i]["sid"] == character_id && (int)dataSet.Tables[0].Rows[i]["account_id"] == account_id)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			for (int j = 1; j < dataSet.Tables.Count; j++)
			{
				dataSet.Tables[j].Clear();
			}
			pnlItemInsert.Visible = false;
		}
		else
		{
			pnlItemInsert.Visible = true;
		}
		Game_Skin_skin_character_list game_Skin_skin_character_list = (Game_Skin_skin_character_list)LoadControl("skin/skin_character_list.ascx");
		game_Skin_skin_character_list.dtCharacterList = dataSet.Tables[0];
		game_Skin_skin_character_list.selected_server = selected_server;
		game_Skin_skin_character_list.ResourceDbNo = resource_db_no;
		game_Skin_skin_character_list.character_id = character_id;
		game_Skin_skin_character_list.seeDeletedCharacter = chkSeeDeletedCharacter.Checked;
		PlaceHolder1.Controls.Add(game_Skin_skin_character_list);
		Game_Skin_skin_item_list game_Skin_skin_item_list = (Game_Skin_skin_item_list)LoadControl("skin/skin_item_list.ascx");
		game_Skin_skin_item_list.dtItemList = dataSet.Tables[1];
		if (dataSet.Tables.Count >= 3)
		{
			game_Skin_skin_item_list.dtEquipItemList = dataSet.Tables[2];
		}
		game_Skin_skin_item_list.SelectedServer = selected_server;
		game_Skin_skin_item_list.ResourceDbNo = resource_db_no;
		PlaceHolder2.Controls.Add(game_Skin_skin_item_list);
	}

	protected void btnGiveItem_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_insert_item"))
		{
			int num = common.ToInt(txtItemCode.Text);
			int num2 = common.ToInt(txtItemEnhance.Text);
			int num3 = common.ToInt(txtItemLevel.Text);
			int num4 = common.ToInt(txtItemCnt.Text);
			int flag = 0;
			int num5 = 0;
			int resource_server_no = resource_db_no;
			DataRow itemResource = resource.GetItemResource(resource_server_no, num);
			bool flag2 = false;
			int num6 = 0;
			if (itemResource != null)
			{
				if (num4 > 0)
				{
					num5 = (int)itemResource["ethereal_durability"];
					int num7 = (int)itemResource["group"];
					num6 = (int)itemResource["available_period"];
					if ((byte)itemResource["decrease_type"] == 2)
					{
						int result = 0;
						int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
						num6 = (int)(DateTime.Now - new DateTime(1970, 1, 1, result, 0, 0)).TotalSeconds + num6;
					}
					if (num7 >= 1 && num7 <= 9)
					{
						if (num2 > 20)
						{
							common.MsgBox(this, "CItem", "Enhancement value should be smaller than 20");
							flag2 = true;
						}
						if ((int)itemResource["rank"] == 1)
						{
							if (num3 > 5)
							{
								common.MsgBox(this, "CItem", "Lv value should be smaller than 5");
								flag2 = true;
							}
						}
						else if (num3 > 10)
						{
							common.MsgBox(this, "CItem", "Lv value should be smaller than 10");
							flag2 = true;
						}
					}
					else if (num7 == 10)
					{
						if (num2 < 1)
						{
							common.MsgBox(this, "CItem", "Enhancement value should be bigger than 10");
							flag2 = true;
						}
						if (num2 > 10)
						{
							common.MsgBox(this, "CItem", "Enhancement value should be smaller than 10");
							flag2 = true;
						}
						if (num3 != 1)
						{
							common.MsgBox(this, "CItem", "Lv value shoud be 1");
							flag2 = true;
						}
					}
					else
					{
						if (num2 != 0)
						{
							common.MsgBox(this, "CItem", "Enhancement value should be 0");
							flag2 = true;
						}
						if (num3 != 1)
						{
							common.MsgBox(this, "CItem", "Lv value shoud be 1.");
							flag2 = true;
						}
					}
					if (num7 == 18)
					{
						flag = 32;
					}
					if (flag2)
					{
						return;
					}
					long num8 = Convert.ToInt32(itemResource["item_use_flag"].ToString());
					string text = "";
					while (num8 > 0)
					{
						text = ((num8 % 2 != 0) ? (text + "1") : (text + "0"));
						num8 /= 2;
					}
					string result_msg;
					if (text.Length > 5 && text[6].ToString() == "1")
					{
						if (acm.InsertItem(ddlServer.SelectedValue, character_id, num, num2, num3, num4, (int)itemResource["endurance"], num6, flag, out result_msg, num5))
						{
							common.MsgBox(this, "CItem", "Insert Item : Success \n\n" + result_msg);
						}
						else
						{
							common.MsgBox(this, "CItem", "Insert Item : Failure \n\n" + result_msg);
						}
						return;
					}
					for (int i = 0; i < num4; i++)
					{
						if (acm.InsertItem(ddlServer.SelectedValue, character_id, num, num2, num3, 1, (int)itemResource["endurance"], num6, flag, out result_msg, num5))
						{
							common.MsgBox(this, "CItem", "Insert Item : Success \n\n" + result_msg);
						}
						else
						{
							common.MsgBox(this, "CItem", "Insert Item : Failure \n\n " + result_msg);
						}
					}
				}
				else
				{
					common.MsgBox(this, "CItem", "Please enter the no. of item you need.");
				}
			}
			else
			{
				common.MsgBox(this, "CItem", "The item code has no item resource info");
			}
		}
		else
		{
			common.MsgBox(this, "CItem", "[Insert Item] No Permission.");
		}
	}
}
