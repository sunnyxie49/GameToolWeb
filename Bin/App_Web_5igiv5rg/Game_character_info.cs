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

public class Game_character_info : Page, IRequiresSessionState
{
	private int account_id;

	private int character_id;

	private string selected_server;

	private int resource_db_no;

	private bool seeDeletedCharacter;

	protected DropDownList ddlServer;

	protected PlaceHolder PlaceHolder1;

	protected CheckBox chkSeeDeletedCharacter;

	protected HyperLink hlInfo;

	protected HyperLink hlItem;

	protected HyperLink hlSkill;

	protected HyperLink hlSummon;

	protected HyperLink hlQuest;

	protected HyperLink hlBank;

	protected HyperLink hlShop;

	protected HyperLink hlTitle;

	protected HyperLink hlArena;

	protected PlaceHolder PlaceHolder2;

	protected TextBox txtName;

	protected TextBox txtExp;

	protected TextBox txtJP;

	protected TextBox txtGold;

	protected TextBox txtLac;

	protected TextBox txtStamina;

	protected TextBox txtImmoralPoint;

	protected TextBox txtCha;

	protected TextBox txtPkc;

	protected TextBox txtDkc;

	protected TextBox txtChatBlockTime;

	protected TextBox txtHuntaHolicPoint;

	protected TextBox txtEtherealStoneDurability;

	protected TextBox txtAP;

	protected Button btnChangeName;

	protected Button btnChangeExp;

	protected Button btnChangeJP;

	protected Button btnChangeGold;

	protected Button btnChangeLac;

	protected Button btnChangeStamina;

	protected Button btnImmoralPoint;

	protected Button btnCha;

	protected Button btnPkc;

	protected Button btnDkc;

	protected Button btnChangeChatBlockTime;

	protected Button btnChangeHuntaHolicPoint;

	protected Button btnEtherealStoneDurability;

	protected Button btnAP;

	protected DropDownList ddlGmPermission;

	protected Button btnGiveGM;

	protected Button btnDelGM;

	protected Button btnGiveAllGM;

	protected Button btnDelAllGM;

	protected TextBox txtPositionX;

	protected TextBox txtPositionY;

	protected Button btnWarp;

	protected Button btnWarp2;

	protected Button btnKick;

	protected Button btnAutoSetGame;

	protected Button btnAutoDelGame;

	protected TextBox txtRestoredName;

	protected Button btnDeletedCharactedRestore;

	protected HtmlInputHidden hdnAvatarNo;

	protected HtmlInputHidden hdnName;

	protected Panel pnlCharacterModify;

	protected PlaceHolder PlaceHolder3;

	protected PlaceHolder PlaceHolder4;

	protected HtmlForm form1;

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
		hlSummon.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_summon.aspx");
		hlQuest.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_quest.aspx");
		hlBank.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_bank.aspx");
		hlTitle.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_title.aspx");
		hlArena.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_arena.aspx");
		if (ConfigurationManager.AppSettings["use_item_shop"] == "true")
		{
			hlShop.NavigateUrl = GetCharacterInfoNavigateUrl("character_info_shop.aspx");
		}
		else
		{
			hlShop.Text = "";
			hlShop.NavigateUrl = "";
		}
		if (ConfigurationManager.AppSettings["gmPermission"] == "true")
		{
			ddlGmPermission.Items.Add(new ListItem("[D] Chat Block", "50"));
		}
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
		SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(ddlServer.SelectedValue));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "gmtool_v2_get_character_info";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
		sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		sqlDataAdapter.Fill(dataSet);
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
			pnlCharacterModify.Visible = false;
		}
		else
		{
			pnlCharacterModify.Visible = true;
		}
		Game_Skin_skin_character_list game_Skin_skin_character_list = (Game_Skin_skin_character_list)LoadControl("skin/skin_character_list.ascx");
		game_Skin_skin_character_list.dtCharacterList = dataSet.Tables[0];
		game_Skin_skin_character_list.selected_server = selected_server;
		game_Skin_skin_character_list.ResourceDbNo = resource_db_no;
		game_Skin_skin_character_list.character_id = character_id;
		game_Skin_skin_character_list.seeDeletedCharacter = chkSeeDeletedCharacter.Checked;
		PlaceHolder1.Controls.Add(game_Skin_skin_character_list);
		if (character_id == 0)
		{
			return;
		}
		if (dataSet.Tables[1].Rows.Count != 0)
		{
			DataRow dataRow = dataSet.Tables[1].Rows[0];
			Game_Skin_skin_character_info game_Skin_skin_character_info = (Game_Skin_skin_character_info)LoadControl("skin/skin_character_info.ascx");
			game_Skin_skin_character_info.RowCharacter = dataRow;
			game_Skin_skin_character_info.RowParty = ((dataSet.Tables[2].Rows.Count != 0) ? dataSet.Tables[2].Rows[0] : null);
			game_Skin_skin_character_info.RowGuild = ((dataSet.Tables[3].Rows.Count != 0) ? dataSet.Tables[3].Rows[0] : null);
			game_Skin_skin_character_info.SelectedServer = selected_server;
			game_Skin_skin_character_info.ResourceDbNo = resource_db_no;
			PlaceHolder2.Controls.Add(game_Skin_skin_character_info);
			hdnAvatarNo.Value = dataRow["sid"].ToString();
			hdnName.Value = dataRow["name"].ToString();
			txtPositionX.Text = dataRow["x"].ToString();
			txtPositionY.Text = dataRow["y"].ToString();
			txtName.Text = dataRow["name"].ToString();
			txtExp.Text = dataRow["exp"].ToString();
			txtJP.Text = dataRow["jp"].ToString();
			txtGold.Text = dataRow["gold"].ToString();
			txtImmoralPoint.Text = dataRow["immoral_point"].ToString();
			txtCha.Text = dataRow["cha"].ToString();
			txtPkc.Text = dataRow["pkc"].ToString();
			txtDkc.Text = dataRow["dkc"].ToString();
			txtRestoredName.Text = dataRow["name"].ToString();
			txtLac.Text = dataRow["chaos"].ToString();
			txtStamina.Text = dataRow["stamina"].ToString();
			txtChatBlockTime.Text = dataRow["chat_block_time"].ToString();
			txtHuntaHolicPoint.Text = dataRow["huntaholic_point"].ToString();
			txtEtherealStoneDurability.Text = dataRow["ethereal_stone_durability"].ToString();
			if (dataRow.Table.Columns.Contains("arena_point"))
			{
				txtAP.Text = dataRow["arena_point"].ToString();
			}
			ddlGmPermission.SelectedValue = dataRow["permission"].ToString();
			if ((DateTime)dataRow["login_time"] > (DateTime)dataRow["logout_time"])
			{
				btnGiveGM.Enabled = false;
				btnDelGM.Enabled = false;
				btnGiveAllGM.Enabled = false;
				btnDelAllGM.Enabled = false;
				btnWarp2.Enabled = false;
				btnChangeExp.Enabled = false;
				btnChangeGold.Enabled = false;
				btnChangeJP.Enabled = false;
				btnChangeLac.Enabled = false;
				btnChangeName.Enabled = false;
				btnChangeStamina.Enabled = false;
				btnDeletedCharactedRestore.Enabled = false;
				btnImmoralPoint.Enabled = false;
				btnCha.Enabled = false;
				btnPkc.Enabled = false;
				btnDkc.Enabled = false;
				btnChangeChatBlockTime.Enabled = false;
				btnChangeHuntaHolicPoint.Enabled = false;
			}
			else
			{
				btnKick.Enabled = false;
				btnWarp.Enabled = false;
				DateTime t = new DateTime(9999, 1, 1);
				if ((DateTime)dataRow["delete_time"] > t)
				{
					btnDeletedCharactedRestore.Enabled = false;
				}
			}
		}
		Game_Skin_skin_item_list game_Skin_skin_item_list = (Game_Skin_skin_item_list)LoadControl("skin/skin_item_list.ascx");
		game_Skin_skin_item_list.SelectedServer = selected_server;
		game_Skin_skin_item_list.ResourceDbNo = resource_db_no;
		PlaceHolder3.Controls.Add(game_Skin_skin_item_list);
		Game_Skin_skin_summon_list game_Skin_skin_summon_list = (Game_Skin_skin_summon_list)LoadControl("skin/skin_summon_list.ascx");
		game_Skin_skin_summon_list.SelectedServer = selected_server;
		game_Skin_skin_summon_list.ResourceDbNo = resource_db_no;
		game_Skin_skin_summon_list.account_id = account_id;
		PlaceHolder4.Controls.Add(game_Skin_skin_summon_list);
	}

	protected void ddlServer_SelectedIndexChanged(object sender, EventArgs e)
	{
		character_id = 0;
	}

	protected void btnGiveGM_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_give_gm_authority"))
		{
			acm.SetPermission(ddlServer.SelectedValue, character_id, int.Parse(ddlGmPermission.SelectedValue), all: false);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[授予 GM 权限] 没有权限.");
		}
	}

	protected void btnDelGM_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_del_gm_authority"))
		{
			acm.SetPermission(ddlServer.SelectedValue, character_id, 0, all: false);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[移除 GM 权限] 没有权限.");
		}
	}

	protected void btnGiveAllGM_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_give_gm_authority"))
		{
			acm.SetPermission(ddlServer.SelectedValue, character_id, int.Parse(ddlGmPermission.SelectedValue), all: true);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[授予 GM 权限] 没有权限.");
		}
	}

	protected void btnDelAllGM_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_del_gm_authority"))
		{
			acm.SetPermission(ddlServer.SelectedValue, character_id, 0, all: true);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[移除 GM 权限] 没有权限.");
		}
	}

	protected void btnWarp_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_warp"))
		{
			int x = common.ToInt(txtPositionX.Text);
			int y = common.ToInt(txtPositionY.Text);
			acm.Warp(ddlServer.SelectedValue, hdnName.Value, x, y);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Warp] 没有权限.");
		}
	}

	protected void btnKick_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_kick"))
		{
			acm.Kick(ddlServer.SelectedValue, hdnName.Value);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Kick] 没有权限.");
		}
	}

	protected void btnGiveGold_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_insert_gold"))
		{
			long result = 0L;
			long.TryParse(txtGold.Text, out result);
			if (result > 0)
			{
				acm.InsertGold(ddlServer.SelectedValue, hdnName.Value, result);
			}
			else
			{
				common.MsgBox(this, "CInfo", "金币 是 0");
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[给与 金币] 没有权限.");
		}
	}

	protected void btnTaiming_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_taiming_all"))
		{
			acm.SetCreatureTaming(ddlServer.SelectedValue, character_id, hdnName.Value);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[驯服] 没有权限.");
		}
	}

	protected void btnWarp2_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_warp"))
		{
			int x = common.ToInt(txtPositionX.Text);
			int y = common.ToInt(txtPositionY.Text);
			if (acm.SetPosition(ddlServer.SelectedValue, character_id, x, y, 0, 0, out var result_msg))
			{
				common.MsgBox(this, "CInfo", "Warp 成功 \n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "Warp 失败 \n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Warp] 没有权限.");
		}
	}

	protected void btnChangeName_Click(object sender, EventArgs e)
	{
		base.Response.Write("asdfasd fdasf");
		base.Response.End();
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_avatar_name"))
		{
			if (acm.SetCharacterName(ddlServer.SelectedValue, character_id, txtName.Text, out var result_msg))
			{
				common.MsgBox(this, "CInfo", "名称修改 : 成功 \n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "名称修改 : 失败 \n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[名称修改] 没有权限.");
		}
	}

	protected void btnChangeExp_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_exp"))
		{
			if (acm.SetCharacterExp(ddlServer.SelectedValue, character_id, long.Parse(txtExp.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "Exp 修改 : 成功 \n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "Exp 修改 : 失败 \n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[EXP 修改] 没有权限.");
		}
	}

	protected void btnChangeJP_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_exp"))
		{
			if (acm.SetCharacterJP(ddlServer.SelectedValue, character_id, long.Parse(txtJP.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "JP 修改 : 成功 \n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "JP 修改 : 失败 \n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[JP 修改] 没有权限.");
		}
	}

	protected void btnChangeGold_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_gold"))
		{
			if (acm.SetCharacterGold(ddlServer.SelectedValue, character_id, long.Parse(txtGold.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "Gold 修改 : 成功 \n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "Gold 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Gold 修改] 没有权限.");
		}
	}

	protected void btnChangeLac_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_lac"))
		{
			if (acm.SetCharacterChaos(ddlServer.SelectedValue, character_id, int.Parse(txtLac.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "Lak 修改 : 成功 \n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "Lak 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Lak 修改] 没有权限.");
		}
	}

	protected void btnChangeStamina_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_stamina"))
		{
			if (acm.SetCharacterStamina(ddlServer.SelectedValue, character_id, int.Parse(txtStamina.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "Stamina 修改 : 成功\n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "Stamina 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Stamina 修改] 没有权限.");
		}
	}

	protected void btnDeletedCharactedRestore_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_restore_deleted_character"))
		{
			if (acm.RestoreDeletedCharacter(ddlServer.SelectedValue, character_id, txtRestoredName.Text))
			{
				common.MsgBox(this, "CInfo", "恢复完成");
			}
			else
			{
				common.MsgBox(this, "CInfo", "恢复失败，请检查角色名称。 如果已经存在相同名称，则无法恢复");
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[restore_deleted_character 修改] 没有权限.");
		}
	}

	protected void btnChangeChatBlockTime_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_chat_block_time"))
		{
			if (acm.SetCharacterChatBlockTime(ddlServer.SelectedValue, character_id, int.Parse(txtChatBlockTime.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "chat_block_time 修改 : 成功\n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "chat_block_time 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[chat_block_time 修改] 没有权限.");
		}
	}

	protected void btnChangeHuntaHolicPoint_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_huntaholic_point"))
		{
			if (acm.SetCharacterHuntaHolicPoint(ddlServer.SelectedValue, character_id, int.Parse(txtHuntaHolicPoint.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "HuntaHolic Point 修改 : 成功\n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "HuntaHolic Point 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[HuntaHolic Point Modify] 没有权限.");
		}
	}

	protected void btnImmoralPoint_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_ip"))
		{
			if (acm.SetCharacterIP(ddlServer.SelectedValue, character_id, float.Parse(txtImmoralPoint.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "Immoral Point 修改 : 成功 \n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "Immoral Point 修改 : 失败 \n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Immoral Point 修改] 没有权限.");
		}
	}

	protected void btnCha_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_cha"))
		{
			if (acm.SetCharacterCha(ddlServer.SelectedValue, character_id, int.Parse(txtCha.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "CHA 修改 : 成功\n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "CHA 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[CHA 修改] 没有权限.");
		}
	}

	protected void btnPkc_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_pkc"))
		{
			if (acm.SetCharacterPkc(ddlServer.SelectedValue, character_id, int.Parse(txtPkc.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "PKC 修改 : 成功\n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "PKC 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[PKC 修改] 没有权限.");
		}
	}

	protected void btnDkc_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_dkc"))
		{
			if (acm.SetCharacterDkc(ddlServer.SelectedValue, character_id, int.Parse(txtDkc.Text), out var result_msg))
			{
				common.MsgBox(this, "CInfo", "DKC 修改 : 成功\n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "DKC 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[DKC 修改] 没有权限.");
		}
	}

	protected void btnAutoSetGame_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_auto"))
		{
			acm.SetAutoUser(ddlServer.SelectedValue, hdnName.Value, 1);
			acm.SetAutoAccount(ddlServer.SelectedValue, hdnName.Value, 1);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Set Auto User] 没有权限.");
		}
	}

	protected void btnAutoDelGame_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_auto"))
		{
			acm.SetAutoUser(ddlServer.SelectedValue, hdnName.Value, 0);
			acm.SetAutoAccount(ddlServer.SelectedValue, hdnName.Value, 0);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Set No Auto User] 没有权限.");
		}
	}

	protected void btnSetSecurityNumber_Click(object sender, EventArgs e)
	{
		if (admin.Authority(ddlServer.SelectedValue, "gmtool_set_security_number"))
		{
			acm.SetSecurityNumber(ddlServer.SelectedValue, account_id);
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Set Security Number] 没有权限.");
		}
	}

	protected void btnEtherealStoneDurability_Click(object sender, EventArgs e)
	{
		int.TryParse(txtEtherealStoneDurability.Text, out var result);
		if (result > 100000000)
		{
			common.MsgBox(this, "CInfo", "max 100,000,000");
		}
		else if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_durability"))
		{
			if (acm.SetEtherealStoneDurability(ddlServer.SelectedValue, character_id, result, out var result_msg))
			{
				common.MsgBox(this, "CInfo", "EtherealStoneDurability 修改 : 成功\n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "EtherealStoneDurability 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Set Ethereal Stone Durability] 没有权限.");
		}
	}

	protected void btnAP_Click(object sender, EventArgs e)
	{
		int.TryParse(txtAP.Text, out var result);
		if (result > 100000000)
		{
			common.MsgBox(this, "CInfo", "max 100,000,000");
		}
		else if (admin.Authority(ddlServer.SelectedValue, "gmtool_change_character_ap"))
		{
			if (acm.SetArenaPoint(ddlServer.SelectedValue, character_id, result, out var result_msg))
			{
				common.MsgBox(this, "CInfo", "AP 修改 : 成功\n\n " + result_msg);
			}
			else
			{
				common.MsgBox(this, "CInfo", "AP 修改 : 失败\n\n " + result_msg);
			}
		}
		else
		{
			common.MsgBox(this, "CInfo", "[Set AP] 没有权限.");
		}
	}
}
