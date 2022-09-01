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

public class Game_character_info_title : Page, IRequiresSessionState
{
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
		sqlCommand.CommandText = "gmtool_v2_get_character_info_Title";
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
		}
		Game_Skin_skin_character_list game_Skin_skin_character_list = (Game_Skin_skin_character_list)LoadControl("skin/skin_character_list.ascx");
		game_Skin_skin_character_list.dtCharacterList = dataSet.Tables[0];
		game_Skin_skin_character_list.selected_server = selected_server;
		game_Skin_skin_character_list.ResourceDbNo = resource_db_no;
		game_Skin_skin_character_list.character_id = character_id;
		game_Skin_skin_character_list.seeDeletedCharacter = chkSeeDeletedCharacter.Checked;
		PlaceHolder1.Controls.Add(game_Skin_skin_character_list);
		Game_Skin_skin_title_list game_Skin_skin_title_list = (Game_Skin_skin_title_list)LoadControl("skin/skin_title_list.ascx");
		game_Skin_skin_title_list.dtTitleList = dataSet.Tables[1];
		game_Skin_skin_title_list.SelectedServer = selected_server;
		game_Skin_skin_title_list.ResourceDbNo = resource_db_no;
		PlaceHolder2.Controls.Add(game_Skin_skin_title_list);
	}
}
