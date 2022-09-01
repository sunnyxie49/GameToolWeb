using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;

public class Game_Skin_skin_character_info : UserControl
{
	protected Label lblSid;

	protected Literal ltrSid;

	protected Label lblGuild;

	protected Literal ltrGuild;

	protected Label lblImmoralPoint;

	protected Literal ltrImmoralPoint;

	protected Label lblCreateDate;

	protected Literal ltrCreateDate;

	protected Label lblAccount;

	protected Literal ltrAccount;

	protected Label lblParty;

	protected Literal ltrParty;

	protected Label lblCha;

	protected Literal ltrCha;

	protected Label lblDeleteDate;

	protected Literal ltrDeleteDate;

	protected Label lblName;

	protected Literal ltrName;

	protected Label lblExp;

	protected Literal ltrExp;

	protected Label lblPkc;

	protected Literal ltrPkc;

	protected Label lblLoginDate;

	protected Literal ltrLoginDate;

	protected Label lblJob;

	protected Literal ltrJob;

	protected Label lblJP;

	protected Literal ltrJP;

	protected Literal ltrTotalJP;

	protected Label lblDkc;

	protected Literal ltrDkc;

	protected Label lblLoginCnt;

	protected Literal ltrLoginCnt;

	protected Label lblRace;

	protected Label lblSex;

	protected Literal ltrRace;

	protected Literal ltrSex;

	protected Label lblGold;

	protected Literal ltrGold;

	protected Label lblChat_block_time;

	protected Literal ltrChat_block_time;

	protected Label lblLogoutDate;

	protected Literal ltrLogoutDate;

	protected Label lblPosion;

	protected Literal ltrPositionX;

	protected Literal ltrPositionY;

	protected Literal ltrPositionZ;

	protected Label lblChaos;

	protected Literal ltrChaos;

	protected Label lblAdv_chat_count;

	protected Literal ltrAdv_chat_count;

	protected Label lblPlayTime;

	protected Literal ltrPlayTime;

	protected Label lblPositionLayer;

	protected Literal ltrPositionLayer;

	protected Label lblStamina;

	protected Literal ltrStamina;

	protected Label lbHuntaholicPoint;

	protected Literal ltrHuntaholicPoint;

	protected Label lblTalentPoint;

	protected Literal ltrTalentPoint;

	protected Label lblSkillReset;

	protected Literal ltrSkillReset;

	protected Label lblArenaPoint;

	protected Literal ltrArenaPoint;

	public string SelectedServer;

	public int ResourceDbNo;

	public DataRow RowCharacter;

	public DataRow RowParty;

	public DataRow RowGuild;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (RowCharacter != null)
		{
			GetCharacterInfo(RowCharacter);
		}
	}

	private void GetCharacterInfo(DataRow row)
	{
		ltrSid.Text = row["sid"].ToString();
		ltrAccount.Text = row["account"].ToString();
		string text = row["name"].ToString();
		if (text.Substring(0, 1) == "@")
		{
			try
			{
				text = string.Format("<span title='* name : {1}\n* delete_time : {2}'>{0}</span>", text.Substring(0, text.IndexOf(' ', 0)), text, row["delete_time"]);
			}
			catch (Exception)
			{
			}
		}
		if ((int)row["permission"] != 0)
		{
			string text2 = "";
			text2 = (int)row["permission"] switch
			{
				100 => "[S]",
				80 => "[A]",
				70 => "[B]",
				60 => "[C]",
				_ => string.Format("[{0}]", (int)row["permission"]),
			};
			ltrName.Text = string.Format("<img src='../img/icon_gm.gif' align='absmiddle'>{0} <strong>{1}</strong> Lv {2}", text2, text, row["lv"]);
		}
		else
		{
			ltrName.Text = string.Format("<strong>{0}</strong> Lv {1}", row["name"], row["lv"]);
		}
		ltrRace.Text = resource.GetRaceName((int)row["race"]);
		ltrSex.Text = (((int)row["sex"] == 2) ? "Male" : "Female");
		DataRow jobResource = resource.GetJobResource(ResourceDbNo, (int)row["job"]);
		string arg = "";
		string arg2 = "";
		if (jobResource != null)
		{
			arg = string.Format("{0}", jobResource["icon_file_name"]);
			if (File.Exists(base.Server.MapPath(string.Format("../img/icon/{0}.jpg", jobResource["icon_file_name"]))))
			{
				arg = string.Format("<img src='../img/icon/{0}.jpg'>", jobResource["icon_file_name"]);
			}
			arg2 = (string)jobResource["value"];
		}
		ltrJob.Text = string.Format("<span>{0}</span> <strong>{1}</strong> Lv {2}", arg, arg2, row["jlv"]);
		ltrExp.Text = ((long)row["exp"]).ToString("#,##0");
		try
		{
			ltrTotalJP.Text = ((long)row["total_jp"]).ToString("#,##0");
		}
		catch (Exception)
		{
		}
		ltrJP.Text = ((long)row["jp"]).ToString("#,##0");
		ltrGold.Text = ((long)row["gold"]).ToString("#,##0");
		ltrChaos.Text = ((int)row["chaos"]).ToString("#,##0");
		ltrStamina.Text = ((int)row["stamina"]).ToString("#,##0");
		ltrParty.Text = ((RowParty != null) ? link.PartyPopup(SelectedServer, (int)RowParty["sid"], (string)RowParty["name"]) : "&nbsp;");
		if (RowGuild != null)
		{
			ltrGuild.Text = link.GuildPopup(SelectedServer, (int)RowGuild["sid"], (string)RowGuild["name"]);
		}
		else
		{
			ltrGuild.Text = "&nbsp;";
		}
		ltrImmoralPoint.Text = row["immoral_point"].ToString();
		ltrCha.Text = ((int)row["cha"]).ToString("#,##0");
		ltrPkc.Text = ((int)row["pkc"]).ToString("#,##0");
		ltrDkc.Text = ((int)row["dkc"]).ToString("#,##0");
		ltrPositionX.Text = ((int)row["x"]).ToString();
		ltrPositionY.Text = ((int)row["y"]).ToString();
		ltrPositionZ.Text = ((int)row["z"]).ToString();
		ltrPositionLayer.Text = row["layer"].ToString();
		ltrChat_block_time.Text = ((int)row["chat_block_time"]).ToString("#,##0");
		ltrAdv_chat_count.Text = ((int)row["adv_chat_count"]).ToString("#,##0");
		ltrCreateDate.Text = ((DateTime)row["create_time"]).ToString("yyyy'-'MM'-'dd HH:mm:ss");
		new DateTime(9999, 1, 1, 0, 0, 0);
		if ((DateTime)row["delete_time"] < (DateTime)row["delete_time"])
		{
			ltrDeleteDate.Text = ((DateTime)row["delete_time"]).ToString("yyyy'-'MM'-'dd HH:mm:ss");
		}
		else
		{
			ltrDeleteDate.Text = "";
		}
		ltrLoginCnt.Text = row["login_count"].ToString();
		if ((DateTime)row["login_time"] > (DateTime)row["logout_time"])
		{
			ltrLoginDate.Text = string.Format("<b>{0}</b>", ((DateTime)row["login_time"]).ToString("yyyy'-'MM'-'dd HH:mm:ss"));
			ltrLogoutDate.Text = ((DateTime)row["logout_time"]).ToString("yyyy'-'MM'-'dd HH:mm:ss");
		}
		else
		{
			ltrLoginDate.Text = ((DateTime)row["login_time"]).ToString("yyyy'-'MM'-'dd HH:mm:ss");
			ltrLogoutDate.Text = string.Format("<b>{0}</b>", ((DateTime)row["logout_time"]).ToString("yyyy'-'MM'-'dd HH:mm:ss"));
		}
		int num = (int)row["play_time"];
		int num2 = num / 86400;
		int num3 = num % 86400 / 3600;
		int num4 = num % 86400 % 3600 / 60;
		string text3 = "";
		text3 = ((num2 > 0) ? (num2 + "d ") : "");
		text3 += ((num3 > 0) ? (num3 + "h ") : "");
		text3 += ((num4 > 0) ? (num4 + "m") : "");
		ltrPlayTime.Text = string.Format("<span title='{1} sec'>{0}<span>", text3, ((int)row["play_time"]).ToString("#,###"));
		if (row.Table.Columns.Contains("talent_point"))
		{
			ltrTalentPoint.Text = ((int)row["talent_point"]).ToString("#,##0");
		}
		if (row.Table.Columns.Contains("skill_reset"))
		{
			string text4 = row["Skill_Reset"].ToString();
			ltrSkillReset.Text = (text4.Contains("reset_count:") ? text4.Split(':')[1] : "0");
		}
		if (row.Table.Columns.Contains("arena_point"))
		{
			ltrArenaPoint.Text = ((int)row["arena_point"]).ToString("#,##0");
		}
		try
		{
			if (row.Table.Columns.Contains("huntaholic_point"))
			{
				ltrHuntaholicPoint.Text = ((int)row["huntaholic_point"]).ToString("#,##0");
				lbHuntaholicPoint.Text = "HuntaholicPoint";
			}
		}
		catch (Exception)
		{
		}
	}
}
