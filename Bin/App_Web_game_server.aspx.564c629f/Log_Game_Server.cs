using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Log_Game_Server : Page, IRequiresSessionState
{
	protected DropDownList ddlServer;

	protected DropDownList ddlLogFile;

	protected HtmlInputText txtStart;

	protected HtmlInputText txtEnd;

	protected HtmlInputText txtItemCode;

	protected HtmlInputText txtItemUID;

	protected HtmlInputText txtAccount;

	protected HtmlInputText txtAccountNo;

	protected HtmlInputText txtAvatar;

	protected HtmlInputText txtAvatarNo;

	protected Button Button1;

	protected Button Button2;

	protected HtmlInputCheckBox cbUseIndex;

	protected CheckBoxList cblLogType1;

	protected CheckBoxList cblLogType2;

	protected CheckBoxList cblLogType3;

	protected CheckBoxList cblLogType4;

	protected CheckBoxList cblLogType6;

	protected CheckBoxList cblLogType10;

	protected CheckBoxList cblLogType7;

	protected CheckBoxList cblLogType8;

	protected CheckBoxList cblLogType5;

	protected CheckBoxList cblLogType9;

	protected Literal ltrTotalCnt;

	protected Repeater rptGameServer;

	private int resource_server_id = 3;

	private string server;

	private string itemnamecolor = "#38B44A";

	private string bluecolor = "#0061F3";

	protected string NullResult = string.Empty;

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
			ddlLogFile.Items.Clear();
			int num = 0;
			do
			{
				ddlLogFile.Items.Add(new ListItem(DateTime.Now.AddMonths(num).ToString("yyyy /  MM"), DateTime.Now.AddMonths(num).ToString("yyyyMM")));
				num--;
			}
			while (DateTime.Now.AddMonths(num).ToString("yyyyMM") != ConfigurationManager.AppSettings["ServiceStart"]);
			libs.server.SetServer(ref ddlServer, base.Request.QueryString["server"]);
			txtStart.Value = DateTime.Now.ToString(common.DateFormat());
			txtEnd.Value = DateTime.Now.AddDays(1.0).ToString(common.DateFormat());
			txtAccount.Value = string.Format("{0}", base.Request.QueryString["account"]);
			txtAccountNo.Value = string.Format("{0}", base.Request.QueryString["account_no"]);
			txtAvatar.Value = string.Format("{0}", base.Request.QueryString["avatar"]);
			txtAvatarNo.Value = string.Format("{0}", base.Request.QueryString["avatar_no"]);
		}
		server = ddlServer.SelectedValue;
	}

	public void Button1_Click(object sender, EventArgs e)
	{
		GetLogData();
	}

	private void GetLogData()
	{
		resource_server_id = libs.server.GetResourceServerNo(ddlServer.SelectedValue);
		SqlConnection connection = new SqlConnection(libs.server.GetConnectionString(ddlServer.SelectedValue, "log"));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = connection;
		string text = " and log_date between @start_date and @end_date ";
		string text2 = "";
		foreach (ListItem item in cblLogType1.Items)
		{
			if (item.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item.Value;
			}
		}
		foreach (ListItem item2 in cblLogType2.Items)
		{
			if (item2.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item2.Value;
			}
		}
		foreach (ListItem item3 in cblLogType3.Items)
		{
			if (item3.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item3.Value;
			}
		}
		foreach (ListItem item4 in cblLogType4.Items)
		{
			if (item4.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item4.Value;
			}
		}
		foreach (ListItem item5 in cblLogType5.Items)
		{
			if (item5.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item5.Value;
			}
		}
		foreach (ListItem item6 in cblLogType6.Items)
		{
			if (item6.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item6.Value;
			}
		}
		foreach (ListItem item7 in cblLogType7.Items)
		{
			if (item7.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item7.Value;
			}
		}
		foreach (ListItem item8 in cblLogType8.Items)
		{
			if (item8.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item8.Value;
			}
		}
		foreach (ListItem item9 in cblLogType9.Items)
		{
			if (item9.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item9.Value;
			}
		}
		foreach (ListItem item10 in cblLogType10.Items)
		{
			if (item10.Selected)
			{
				if (text2 != "")
				{
					text2 += ", ";
				}
				text2 += item10.Value;
			}
		}
		if (text2 != "")
		{
			text = text + " and log_id in " + $"( {text2} ) ";
		}
		if (txtAccount.Value != "")
		{
			text += " and s1 = @s1";
		}
		if (txtAccountNo.Value != "")
		{
			text += " and n1 = @n1";
		}
		if (txtAvatar.Value != "")
		{
			text += " and s2 = @s2";
		}
		if (txtAvatarNo.Value != "")
		{
			text += " and n2 = @n2";
		}
		if (txtItemCode.Value != "")
		{
			text += " and n4 = @n4";
		}
		if (txtItemUID.Value != "")
		{
			text += " and n11 = @n11 ";
		}
		string arg = ddlServer.SelectedValue + "_" + ddlLogFile.SelectedValue;
		string arg2 = "";
		if (cbUseIndex.Checked)
		{
			if (txtItemUID.Value != "")
			{
				arg2 = $",  index = IX_tb_log_{arg}_n11";
			}
			else if (txtAccount.Value != "")
			{
				arg2 = $",  index = IX_tb_log_{arg}_s1";
			}
			else if (txtAvatar.Value != "")
			{
				arg2 = $",  index = IX_tb_log_{arg}_s2";
			}
		}
		string text4 = (sqlCommand.CommandText = string.Format("select top 1000 * from Log_{0}.dbo.tb_log_{0} with (NOLOCK{2}) where 1=1 {1} order by log_date", arg, text, arg2));
		sqlCommand.CommandType = CommandType.Text;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@start_date", SqlDbType.DateTime).Value = txtStart.Value;
		sqlCommand.Parameters.Add("@end_date", SqlDbType.DateTime).Value = txtEnd.Value;
		if (txtAccount.Value != "")
		{
			sqlCommand.Parameters.Add("@s1", SqlDbType.NVarChar).Value = txtAccount.Value;
		}
		if (txtAccountNo.Value != "")
		{
			sqlCommand.Parameters.Add("@n1", SqlDbType.BigInt).Value = long.Parse(txtAccountNo.Value);
		}
		if (txtAvatar.Value != "")
		{
			sqlCommand.Parameters.Add("@s2", SqlDbType.NVarChar).Value = txtAvatar.Value;
		}
		if (txtAvatarNo.Value != "")
		{
			sqlCommand.Parameters.Add("@n2", SqlDbType.BigInt).Value = long.Parse(txtAvatarNo.Value);
		}
		if (txtItemCode.Value != "")
		{
			sqlCommand.Parameters.Add("@n4", SqlDbType.BigInt).Value = long.Parse(txtItemCode.Value);
		}
		if (txtItemUID.Value != "")
		{
			sqlCommand.Parameters.Add("@n11", SqlDbType.BigInt).Value = long.Parse(txtItemUID.Value);
		}
		sqlCommand.CommandTimeout = 180;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
		DataSet dataSet = new DataSet();
		try
		{
			sqlDataAdapter.Fill(dataSet);
			if (dataSet == null || dataSet.Tables[0].Rows.Count < 1)
			{
				NullResult = "<tr><td colspan='6' style='text-align:center;'>No Data</td></tr>";
			}
			ltrTotalCnt.Text = dataSet.Tables[0].Rows.Count.ToString("#,##0");
			rptGameServer.DataSource = dataSet;
			rptGameServer.ItemDataBound += rptGameServer_ItemDataBound;
			rptGameServer.DataBind();
		}
		catch (Exception)
		{
			NullResult = "<tr><td colspan='6' style='text-align:center;'>No Data</td></tr>";
		}
	}

	private void rptGameServer_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
		{
			return;
		}
		Literal literal = (Literal)e.Item.FindControl("ltrDate");
		Literal literal2 = (Literal)e.Item.FindControl("ltrAccount");
		Literal literal3 = (Literal)e.Item.FindControl("ltrAvatar");
		Literal literal4 = (Literal)e.Item.FindControl("ltrAction");
		Literal literal5 = (Literal)e.Item.FindControl("ltrMsg");
		Literal literal6 = (Literal)e.Item.FindControl("ltrSID");
		int num = (int)((DataRowView)e.Item.DataItem)["log_id"];
		long num2 = (long)((DataRowView)e.Item.DataItem)["n1"];
		long num3 = (long)((DataRowView)e.Item.DataItem)["n2"];
		long num4 = (long)((DataRowView)e.Item.DataItem)["n3"];
		long num5 = (long)((DataRowView)e.Item.DataItem)["n4"];
		long num6 = (long)((DataRowView)e.Item.DataItem)["n5"];
		long num7 = (long)((DataRowView)e.Item.DataItem)["n6"];
		long num8 = (long)((DataRowView)e.Item.DataItem)["n7"];
		long num9 = (long)((DataRowView)e.Item.DataItem)["n8"];
		long num10 = (long)((DataRowView)e.Item.DataItem)["n9"];
		long num11 = (long)((DataRowView)e.Item.DataItem)["n10"];
		long num12 = (long)((DataRowView)e.Item.DataItem)["n11"];
		string text = ((DataRowView)e.Item.DataItem)["s1"].ToString();
		string text2 = ((DataRowView)e.Item.DataItem)["s2"].ToString();
		string text3 = ((DataRowView)e.Item.DataItem)["s3"].ToString();
		string text4 = ((DataRowView)e.Item.DataItem)["s4"].ToString();
		literal.Text = ((DateTime)((DataRowView)e.Item.DataItem)["log_date"]).ToString("yyyy-MM-dd HH:mm:ss");
		if (num >= 1000)
		{
			text2 = link.CharacterPopup(server, (int)num3, text2, text);
			literal2.Text = $"<span title='{num2}'>{text}</span>";
			literal3.Text = text2;
		}
		if (num12 != 0 && num != 2306)
		{
			literal6.Text = string.Format("<a href=\"javascript:OpenEditItemPopup('{0}', '{1}')\">{1}</a>", ddlServer.SelectedValue, num12);
		}
		StringBuilder stringBuilder = new StringBuilder();
		string newValue;
		string text7;
		string text5;
		long num13;
		string text6;
		switch (num)
		{
			case 101:
				literal4.Text = "SERVER_INFO";
				stringBuilder.Append("#@msg@#");
				stringBuilder.Replace("#@msg@#", text4);
				break;
			case 102:
				literal4.Text = "SERVER_WARNING";
				stringBuilder.Append("#@msg@#");
				stringBuilder.Replace("#@msg@#", text4);
				break;
			case 103:
				literal4.Text = "SERVER_THREAD_INFO";
				stringBuilder.Append("#@thread_id@# : #@thread_name@#");
				stringBuilder.Replace("#@thread_id@#", num5.ToString());
				stringBuilder.Replace("#@thread_name@#", text4);
				break;
			case 104:
				literal4.Text = "SERVER_STATUS";
				stringBuilder.Append("Concurrent users : #@user_cnt@#");
				stringBuilder.Replace("#@user_cnt@#", num5.ToString());
				break;
			case 110:
				literal2.Text = $"<span title='{num2}'>{text}</span>";
				literal4.Text = "SERVER_REPORT";
				stringBuilder.Append($"MAC: {text2}<br>IP: {text3}<br>Client: {text4}");
				break;
			case 1001:
				literal4.Text = "Login";
				stringBuilder.Append("#@account@# Login - IP #@ip@# #@pcbang@#");
				stringBuilder.Replace("#@account@#", text);
				stringBuilder.Replace("#@ip@#", text3);
				num13 = num3;
				if (num13 <= 2 && num13 >= 1)
				{
					switch (num13 - 1)
					{
						case 0L:
							break;
						case 1L:
							goto IL_08c3;
						default:
							goto IL_08da;
					}
					stringBuilder.Replace("#@pcbang@#", "[PC CAFE]");
					break;
				}
				goto IL_08da;
			case 1002:
				{
					literal4.Text = "Logout #@pcbang@#";
					stringBuilder.Append("#@account@# Logout - IP #@ip@#");
					stringBuilder.Replace("#@account@#", text);
					stringBuilder.Replace("#@ip@#", text3);
					long num25 = num6;
					if (num25 <= 2 && num25 >= 1)
					{
						switch (num25 - 1)
						{
							case 0L:
								break;
							case 1L:
								goto IL_0963;
							default:
								goto IL_097a;
						}
						stringBuilder.Replace("#@pcbang@#", "[PC CAFE]");
						break;
					}
					goto IL_097a;
				}
			case 2001:
				literal4.Text = "Create Avatar";
				stringBuilder.Append("Avatar#@character_name@# Create");
				stringBuilder.Replace("#@character_name@#", text2);
				break;
			case 2002:
				literal4.Text = "Ask Avatar deletion";
				stringBuilder.Append("Avatar #@character_name@#ask to delete");
				stringBuilder.Replace("#@character_name@#", text2);
				break;
			case 2003:
				literal4.Text = "Avatar deletion cancel";
				stringBuilder.Append("Avatar #@character_name@# cancel the deletion");
				stringBuilder.Replace("#@character_name@#", text2);
				break;
			case 2004:
				literal4.Text = "Avatar deletion";
				stringBuilder.Append("Avatar #@character_name@# is deleted");
				stringBuilder.Replace("#@character_name@#", text2);
				break;
			case 2010:
				literal4.Text = "Avatar name change";
				stringBuilder.Append("Avatar name change #@old_character_name@#  -> #@new_character_name@# ");
				stringBuilder.Replace("#@old_character_name@#", text2);
				stringBuilder.Replace("#@new_character_name@#", text3);
				break;
			case 2101:
				{
					literal4.Text = "Game Login";
					stringBuilder.Append("#@character_name@# Game login(Stamina #@stamina@#) (Gold #@gold@#) - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)  #@pcbang@#");
					stringBuilder.Replace("#@character_name@#", text2);
					stringBuilder.Replace("#@stamina@#", num7.ToString());
					stringBuilder.Replace("#@gold@#", num8.ToString("###,0"));
					stringBuilder.Replace("#@layer@#", num9.ToString());
					stringBuilder.Replace("#@axis_x@#", num10.ToString());
					stringBuilder.Replace("#@axis_y@#", num11.ToString());
					long num17 = num6;
					if (num17 <= 2 && num17 >= 1)
					{
						switch (num17 - 1)
						{
							case 0L:
								break;
							case 1L:
								goto IL_0b48;
							default:
								goto IL_0b5f;
						}
						stringBuilder.Replace("#@pcbang@#", "[PC Internet Cafe]");
						break;
					}
					goto IL_0b5f;
				}
			case 2102:
				{
					literal4.Text = "Game Logout";
					stringBuilder.Append("#@character_name@# Lv #@lv@# Game Logout(Stamina #@stamina@#)  (Gold #@gold@#) - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#), PlayTime #@play_time@#Sec #@pcbang@#");
					stringBuilder.Replace("#@character_name@#", text2);
					stringBuilder.Replace("#@stamina@#", num7.ToString());
					stringBuilder.Replace("#@gold@#", num8.ToString("###,0"));
					stringBuilder.Replace("#@layer@#", num9.ToString());
					stringBuilder.Replace("#@axis_x@#", num10.ToString());
					stringBuilder.Replace("#@axis_y@#", num11.ToString());
					long num14 = num6;
					if (num14 > 2 || num14 < 1)
					{
						goto IL_0c53;
					}
					switch (num14 - 1)
					{
						case 0L:
							break;
						case 1L:
							goto IL_0c3f;
						default:
							goto IL_0c53;
					}
					stringBuilder.Replace("#@pcbang@#", "[PC Internet Cafe]");
					goto IL_0c65;
				}
			case 2103:
				literal4.Text = "Avatar Info";
				stringBuilder.Append("#@character_name@# Lv #@lv@# \r\n\t\t\t\t\t\t#@job_name@# JLv #@jlv@#\r\n\t\t\t\t\t\t, JP #@jp@#\r\n\t\t\t\t\t\t, Gold #@gold@#\r\n\t\t\t\t\t\t, Bank\t#@bank_gold@#\r\n\t\t\t\t\t\t, Lac #@lac@#\r\n\t\t\t\t\t\t, Immoral Point #@immoral_point@#\r\n\t\t\t\t\t\t, Exp #@exp@#\r\n\t\t\t\t\t");
				stringBuilder.Replace("#@character_name@#", text2);
				stringBuilder.Replace("#@lv@#", num4.ToString());
				stringBuilder.Replace("#@jlv@#", num5.ToString());
				stringBuilder.Replace("#@jp@#", num6.ToString("###,0"));
				if (resource.GetJobResource(resource_server_id, (int)num7) != null)
				{
					stringBuilder.Replace("#@job_name@#", (string)resource.GetJobResource(resource_server_id, (int)num7)["value"]);
				}
				else
				{
					stringBuilder.Replace("#@job_name@#", $"unkonw({num7})");
				}
				stringBuilder.Replace("#@gold@#", num8.ToString("###,0"));
				stringBuilder.Replace("#@bank_gold@#", num9.ToString("###,0"));
				stringBuilder.Replace("#@lac@#", num10.ToString("###,0"));
				stringBuilder.Replace("#@immoral_point@#", num11.ToString());
				stringBuilder.Replace("#@exp@#", num12.ToString("###,0"));
				break;
			case 2201:
				literal4.Text = "CHEAT";
				stringBuilder.Append("#@command@#");
				stringBuilder.Replace("#@command@#", text4);
				break;
			case 2301:
				literal4.Text = "Lv up";
				if (num6 >= num5)
				{
					stringBuilder.Append("[Up] Lv #@old_lv@# -> Lv #@now_lv@#");
				}
				else
				{
					stringBuilder.Append("[Down] Lv #@old_lv@# -> Lv #@now_lv@#");
				}
				stringBuilder.Replace("#@old_lv@#", num5.ToString());
				stringBuilder.Replace("#@now_lv@#", num6.ToString());
				break;
			case 2302:
				{
					literal4.Text = "Learn Skill";
					stringBuilder.Append("You have learned #@skill_name@# Lv #@skill_lv@#. #@result_jp@# JP (#@use_jp@# JP)");
					DataRow skillResource2 = resource.GetSkillResource(resource_server_id, (int)num5);
					if (skillResource2 != null)
					{
						stringBuilder.Replace("#@skill_name@#", (string)skillResource2["value"]);
					}
					else
					{
						stringBuilder.Replace("#@skill_name@#", $"[skill code : {num5}]");
					}
					stringBuilder.Replace("#@skill_lv@#", num6.ToString());
					stringBuilder.Replace("#@use_jp@#", $"<font color='red'>-{num7}</font>");
					stringBuilder.Replace("#@result_jp@#", num8.ToString());
					break;
				}
			case 2303:
				literal4.Text = "Job Lv up";
				stringBuilder.Append("#@job_name@# Lv #@job_lv@# - #@result_jp@# JP (#@use_jp@# JP)");
				if (resource.GetJobResource(resource_server_id, (int)num5) != null)
				{
					stringBuilder.Replace("#@job_name@#", (string)resource.GetJobResource(resource_server_id, (int)num5)["value"]);
				}
				else
				{
					stringBuilder.Replace("#@job_name@#", $"unkonw({num5})");
				}
				stringBuilder.Replace("#@job_lv@#", num6.ToString());
				stringBuilder.Replace("#@use_jp@#", $"<font color='red'>-{num7}</font>");
				stringBuilder.Replace("#@result_jp@#", num8.ToString());
				break;
			case 2304:
				literal4.Text = "Change of professoion";
				if (num5 != 0)
				{
					stringBuilder.Append("#@old_job_name@# Lv #@old_job_lv@# -> #@now_job_name@# Lv #@now_job_lv@#");
					if (resource.GetJobResource(resource_server_id, (int)num5) != null)
					{
						stringBuilder.Replace("#@old_job_name@#", (string)resource.GetJobResource(resource_server_id, (int)num5)["value"]);
					}
					else
					{
						stringBuilder.Replace("#@old_job_name@#", $"unkonw({num5})");
					}
					stringBuilder.Replace("#@old_job_lv@#", num7.ToString());
				}
				else
				{
					stringBuilder.Append("#@now_job_name@# Lv #@now_job_lv@#");
				}
				if (resource.GetJobResource(resource_server_id, (int)num6) != null)
				{
					stringBuilder.Replace("#@now_job_name@#", (string)resource.GetJobResource(resource_server_id, (int)num6)["value"]);
				}
				else
				{
					stringBuilder.Replace("#@now_job_name@#", $"unkonw({num6})");
				}
				stringBuilder.Replace("#@now_job_lv@#", num8.ToString());
				break;
			case 2305:
				literal4.Text = "PK";
				stringBuilder.Append("#@character_name@# (#@pk_type@#)  #@now_immoral_point@# IP(#@get_immoral_point@#), #@dkp@# dkp,  #@pkc@# pkc, #@lac@# Lac(#@get_lac@#)");
				stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num5, text3, num5.ToString()));
				stringBuilder.Replace("#@pk_type@#", text4);
				stringBuilder.Replace("#@now_immoral_point@#", num8.ToString());
				stringBuilder.Replace("#@get_immoral_point@#", string.Format("<font color='" + bluecolor + "'>+{0}</font>", num7));
				stringBuilder.Replace("#@dkp@#", num9.ToString());
				stringBuilder.Replace("#@pkc@#", num10.ToString());
				stringBuilder.Replace("#@lac@#", num11.ToString());
				stringBuilder.Replace("#@get_lac@#", string.Format("<font color='" + bluecolor + "'>+{0}</font>", num6));
				break;
			case 2306:
				literal4.Text = "Death";
				if (text4 == "PLYR")
				{
					stringBuilder.Append("Killed by Player '#@character_name@#'- #@exp@# Exp(#@lost_exp@#), #@lac@# Lac(#@lost_lac@#)");
					stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num5, text3, num5.ToString()));
				}
				else if (text4 == "SUMN")
				{
					stringBuilder.Append("Killed by <a href=\"javascript:OpenSummonPopup('" + server + "', '" + num5 + "')\">Summon</a> (owner - '#@character_name@#' ) - #@exp@# Exp(#@lost_exp@#), #@lac@# Lac(#@lost_lac@#)");
					stringBuilder.Replace("#@character_name@#", text3);
				}
				else
				{
					stringBuilder.Append("Killed by Monster '#@monster_name@#' - #@exp@# Exp(#@lost_exp@#),  #@lac@# Lac(#@lost_lac@#)");
					stringBuilder.Replace("#@monster_name@#", resource.GetMonsterName(resource_server_id, (int)num5));
				}
				stringBuilder.Replace("#@exp@#", num12.ToString());
				stringBuilder.Replace("#@lost_exp@#", $"<font color='red'>-{num6}</font>");
				stringBuilder.Replace("#@lost_lac@#", $"<font color='red'>-{num7}</font>");
				stringBuilder.Replace("#@lac@#", num8.ToString());
				break;
			case 2307:
				literal4.Text = "Taming";
				if (text4 == "SUCS")
				{
					stringBuilder.Append("#@creature_name@# Taming Success");
				}
				else
				{
					stringBuilder.Append("#@creature_name@# Taming Failure");
				}
				stringBuilder.Replace("#@creature_name@#", resource.GetSummonName(resource_server_id, (int)num6));
				break;
			case 2308:
				literal4.Text = "PKMode";
				if (num6 > 0)
				{
					stringBuilder.Append("PKMode#@on_off@# - #@now_immoral_point@# IP (#@get_immoral_point@# IP)");
				}
				else
				{
					stringBuilder.Append("PKMode#@on_off@#");
				}
				stringBuilder.Replace("#@on_off@#", text4);
				stringBuilder.Replace("#@now_immoral_point@#", num7.ToString());
				stringBuilder.Replace("#@get_immoral_point@#", string.Format("<font color='" + bluecolor + "'>+{0}</font>", num6));
				break;
			case 2309:
				{
					literal4.Text = "CHARACTER RESURRECTION";
					if (num5 == 0)
					{
						stringBuilder.Append("#@resurection_type@# #@character_name@# - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#) ");
					}
					else
					{
						stringBuilder.Append("#@resurection_type@# #@character_name@# - by #@helper_character_name@#- Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#) ");
					}
					stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num5, text2, text));
					stringBuilder.Replace("#@helper_character_name@#", link.CharacterPopup(server, (int)num6, text4, text3));
					long num22 = num4;
					if (num22 <= 7 && num22 >= 0)
					{
						switch (num22)
						{
							case 0L:
								stringBuilder.Replace("#@resurection_type@#", "");
								break;
							case 1L:
								stringBuilder.Replace("#@resurection_type@#", "[UI] ");
								break;
							case 2L:
								stringBuilder.Replace("#@resurection_type@#", "[UI in Areana]");
								break;
							case 3L:
								stringBuilder.Replace("#@resurection_type@#", "[Skill 504]");
								break;
							case 4L:
								stringBuilder.Replace("#@resurection_type@#", "[Skill 30501]");
								break;
							case 5L:
								stringBuilder.Replace("#@resurection_type@#", "[Item]");
								break;
							case 6L:
								stringBuilder.Replace("#@resurection_type@#", "[Buff]");
								break;
							case 7L:
								stringBuilder.Replace("#@resurection_type@#", "[Cash Item]");
								break;
						}
					}
					stringBuilder.Replace("#@layer@#", num10.ToString());
					stringBuilder.Replace("#@axis_x@#", num8.ToString());
					stringBuilder.Replace("#@axis_y@#", num9.ToString());
					if (num11 > 0)
					{
						stringBuilder.Append(" ....... #@result_exp@# Exp(#@restore_exp@# )");
					}
					else
					{
						stringBuilder.Append(" ....... #@result_exp@# Exp");
					}
					stringBuilder.Replace("#@restore_exp@#", string.Format("<font color='" + bluecolor + "'>+{0}</font>", num11));
					stringBuilder.Replace("#@result_exp@#", num12.ToString());
					break;
				}
			case 2351:
				{
					literal4.Text = "SKILL LOTTO";
					stringBuilder.Append("Creature #@creature_name@#(#@creature_id@#) (Skill NAME:#@skill_name@#, LV:#@skill_lv@#, ENHANCEMENT LV : #@enhancement@#, )");
					stringBuilder.Append("<br/>BEFORE GOLD : #@before_gold@#, AFTER GOLD : #@after_gold@#, USE GOLD : #@use_gold@#, ACQUISITION : #@acquisition_gold@#");
					stringBuilder.Replace("#@creature_id@#", num4.ToString());
					stringBuilder.Replace("#@creature_name@#", text3);
					stringBuilder.Replace("#@lv@#", num5.ToString());
					stringBuilder.Replace("#@result_lv@#", num6.ToString());
					DataRow skillResource = resource.GetSkillResource(resource_server_id, (int)num6);
					if (skillResource != null)
					{
						if (skillResource["value"].ToString() == "")
						{
							stringBuilder.Replace("#@skill_name@#", $"[skill code : {num6}]");
						}
						else
						{
							stringBuilder.Replace("#@skill_name@#", skillResource["value"].ToString());
						}
					}
					else
					{
						stringBuilder.Replace("#@skill_name@#", $"[skill code : {num6}]");
					}
					stringBuilder.Replace("#@skill_lv@#", num6.ToString());
					stringBuilder.Replace("#@enhancement@#", num8.ToString());
					stringBuilder.Replace(" #@before_gold@#", num9.ToString());
					stringBuilder.Replace(" #@after_gold@#", num12.ToString());
					stringBuilder.Replace(" #@use_gold@#", num10.ToString());
					stringBuilder.Replace(" #@acquisition_gold@#", num11.ToString());
					break;
				}
			case 2401:
				literal4.Text = "Item Acquisition";
				if (num5 != 0)
				{
					if (num6 == num7)
					{
						stringBuilder.Append("[#@take_type@#] #@item_name@# #@count@#unit(s) Acquisition");
					}
					else
					{
						stringBuilder.Append("[#@take_type@#] #@item_name@# #@count@#unit(s) Acquisition - Total #@result_count@#unit(s) possession");
					}
					stringBuilder.Replace("#@take_type@#", text4);
					stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
					stringBuilder.Replace("#@count@#", num6.ToString());
					stringBuilder.Replace("#@result_count@#", num7.ToString());
				}
				else
				{
					stringBuilder.Append("[#@take_type@#] #@count@#Rupee Acquisition - Total #@result_count@# Rupee possession");
					stringBuilder.Replace("#@take_type@#", text4);
					stringBuilder.Replace("#@count@#", num8.ToString());
					stringBuilder.Replace("#@result_count@#", num9.ToString());
				}
				break;
			case 2402:
				literal4.Text = "Item destruction";
				if (num6 == num7)
				{
					stringBuilder.Append("#@item_name@# #@count@#unit(s) destruction");
				}
				else
				{
					stringBuilder.Append("#@item_name@# #@count@#unit(s) destruction - #@reuslt_count@#unit(s) possession");
				}
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@count@#", num6.ToString());
				stringBuilder.Replace("#@result_count@#", num7.ToString());
				break;
			case 2403:
				literal4.Text = "Item throw away";
				if (num5 > 0)
				{
					if (num7 == 0)
					{
						stringBuilder.Append("#@item_name@# #@count@#unit(s) throw away - Coordinates (#@axis_x@#, #@axis_y@#)");
					}
					else
					{
						stringBuilder.Append("#@item_name@# #@count@#unit(s) throw away (#@result_count@#unit(s) left)- Coordinates (#@axis_x@#, #@axis_y@#)");
					}
					stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
					stringBuilder.Replace("#@count@#", num6.ToString());
					stringBuilder.Replace("#@result_count@#", num7.ToString());
				}
				else
				{
					stringBuilder.Append("#@gold@#Rupee throw away- Total #@result_gold@# Rupee possession - Coordinates (#@axis_x@#, #@axis_y@#)");
					stringBuilder.Replace("#@gold@#", num8.ToString());
					stringBuilder.Replace("#@result_gold@#", num9.ToString());
				}
				stringBuilder.Replace("#@axis_x@#", num10.ToString());
				stringBuilder.Replace("#@axis_y@#", num11.ToString());
				break;
			case 2404:
				literal4.Text = "Store buying";
				if (num7 == 0)
				{
					stringBuilder.Append("[NPC #@npc_name@#] #@item_name@# #@count@#unit(s) buying - #@result_gold@#Rupee(#@gold@#)");
				}
				else
				{
					stringBuilder.Append("[NPC #@npc_name@#] #@item_name@# #@count@#unit(s) buying - (#@result_count@#unit(s) possession) - #@result_gold@#Rupee(#@gold@#)");
				}
				stringBuilder.Replace("#@npc_name@#", resource.GetNpcName(resource_server_id, (int)num10));
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@count@#", num6.ToString());
				stringBuilder.Replace("#@result_count@#", num7.ToString());
				stringBuilder.Replace("#@result_gold@#", num9.ToString());
				stringBuilder.Replace("#@gold@#", $"<font color='red'>-{num8}</font>");
				break;
			case 2405:
				literal4.Text = "Store sale";
				if (num7 == 0)
				{
					stringBuilder.Append("[NPC #@npc_name@#] #@item_name@# #@count@#unit(s) sale - #@result_gold@#Rupee(#@gold@#)");
				}
				else
				{
					stringBuilder.Append("[NPC #@npc_name@#] #@item_name@# #@count@#unit(s) sale - (#@result_count@#unit(s) possession) - #@result_gold@#Rupee(#@gold@#)");
				}
				stringBuilder.Replace("#@npc_name@#", resource.GetNpcName(resource_server_id, (int)num10));
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@count@#", num6.ToString());
				stringBuilder.Replace("#@result_count@#", num7.ToString());
				stringBuilder.Replace("#@result_gold@#", num9.ToString());
				stringBuilder.Replace("#@gold@#", string.Format("<font color='" + bluecolor + "'>+{0}</font>", num8));
				break;
			case 2406:
				literal4.Text = "Item use";
				stringBuilder.Append("#@item_name@# #@count@#unit(s) use (#@result_count@#unit(s) possession)");
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@count@#", num6.ToString());
				stringBuilder.Replace("#@result_count@#", num7.ToString());
				break;
			case 2407:
				literal4.Text = "Giving by trade";
				if (num5 != 0)
				{
					if (num7 == 0)
					{
						stringBuilder.Append("[Give to #@target_name@#] #@item_name@# #@count@#unit(s)");
					}
					else
					{
						stringBuilder.Append("[Give to #@target_name@#] #@item_name@# #@count@#unit(s) - #@result_count@#unit(s) possession");
					}
					stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
					stringBuilder.Replace("#@count@#", num6.ToString());
					stringBuilder.Replace("#@result_count@#", num7.ToString());
				}
				else
				{
					stringBuilder.Append("[Give To #@target_name@#] #@gold@#Rupee - #@result_gold@# Rupee possession");
					stringBuilder.Replace("#@gold@#", num8.ToString());
					stringBuilder.Replace("#@result_gold@#", num9.ToString());
				}
				stringBuilder.Replace("#@target_name@#", link.CharacterPopup(ddlServer.SelectedValue, (int)num10, text3, ""));
				break;
			case 2408:
				literal4.Text = "Taking by trade";
				if (num5 != 0)
				{
					if (num7 == 0)
					{
						stringBuilder.Append("[Take from #@target_name@#] #@item_name@# #@count@#unit(s)");
					}
					else
					{
						stringBuilder.Append("[Take from #@target_name@#] #@item_name@# #@count@#unit(s) - #@result_count@#unit(s) possession");
					}
					stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
					stringBuilder.Replace("#@count@#", num6.ToString());
					stringBuilder.Replace("#@result_count@#", num7.ToString());
				}
				else
				{
					stringBuilder.Append("[Take from #@target_name@#] #@gold@#Rupee - #@result_gold@# Rupee possession");
					stringBuilder.Replace("#@gold@#", num8.ToString());
					stringBuilder.Replace("#@result_gold@#", num9.ToString());
				}
				stringBuilder.Replace("#@target_name@#", link.CharacterPopup(ddlServer.SelectedValue, (int)num10, text3, ""));
				break;
			case 2409:
				literal4.Text = "Item Combination";
				stringBuilder.Append("#@mix_type@# - #@item_name@# #@count@#unit(s) Combination trial (#@result_count@#unit(s) possession) <br>ethereal_durability(#@ethereal_durability@#)");
				stringBuilder.Replace("#@mix_type@#", text4);
				stringBuilder.Replace("#@ethereal_durability@#", num9.ToString());
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@count@#", num6.ToString());
				stringBuilder.Replace("#@result_count@#", num7.ToString());
				break;
			case 2410:
				literal4.Text = "Combination result";
				if (text4 == "SUCS")
				{
					stringBuilder.Append("#@item_name@# Combination Success");
				}
				else
				{
					stringBuilder.Append("#@item_name@# Combination Failure");
				}
				stringBuilder.Append("<br/>ethereal_durability(#@ethereal_durability@#)");
				stringBuilder.Replace("#@ethereal_durability@#", num8.ToString());
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				break;
			case 2411:
				literal4.Text = "Item enhancement";
				stringBuilder.Append("#@item_name@# Blacksimth enhancement (Lv #@item_lv@# -> Lv #@result_item_lv@#)");
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@item_lv@#", num6.ToString());
				stringBuilder.Replace("#@result_item_lv@#", num7.ToString());
				break;
			case 2412:
				literal4.Text = "Lak Acquisition";
				stringBuilder.Append("#@lac@# Lak Acquisition - #@result_lac@# Lak possession");
				stringBuilder.Replace("#@lac@#", num6.ToString());
				stringBuilder.Replace("#@result_lac@#", num7.ToString());
				break;
			case 2413:
				literal4.Text = "Item Deleted";
				stringBuilder.Append("#@item_name@#  #@del_count@#unit(s)  Deleted(#@result_count@# left) - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#) ");
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
				stringBuilder.Append(text4);
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@before_count@#", num6.ToString());
				stringBuilder.Replace("#@del_count@#", num7.ToString());
				stringBuilder.Replace("#@result_count@#", ((int)(num6 - num7)).ToString());
				stringBuilder.Replace("#@gold@#", num8.ToString());
				stringBuilder.Replace("#@layer@#", num9.ToString());
				stringBuilder.Replace("#@axis_x@#", num10.ToString());
				stringBuilder.Replace("#@axis_y@#", num11.ToString());
				break;
			case 2414:
				literal4.Text = "ITEM_SOCKET_INFO";
				stringBuilder.Append("#@item_name@# #@now@#/#@max@# #@socket1@# #@socket2@# #@socket3@# #@socket4@# #@socket5@# #@socket6@#");
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num12)));
				stringBuilder.Replace("#@now@#", num4.ToString());
				stringBuilder.Replace("#@max@#", num5.ToString());
				stringBuilder.Replace("#@socket1@#", (num6 > 0) ? resource.GetItemName(resource_server_id, (int)num6) : "");
				stringBuilder.Replace("#@socket2@#", (num7 > 0) ? resource.GetItemName(resource_server_id, (int)num7) : "");
				stringBuilder.Replace("#@socket3@#", (num8 > 0) ? resource.GetItemName(resource_server_id, (int)num8) : "");
				stringBuilder.Replace("#@socket4@#", (num9 > 0) ? resource.GetItemName(resource_server_id, (int)num9) : "");
				stringBuilder.Replace("#@socket5@#", (num10 > 0) ? resource.GetItemName(resource_server_id, (int)num10) : "");
				stringBuilder.Replace("#@socket6@#", (num11 > 0) ? resource.GetItemName(resource_server_id, (int)num11) : "");
				break;
			case 2415:
				literal4.Text = "ITEM_SOCKET_CRAFT";
				stringBuilder.Append("#@item_name@# [Lac #@before_lac@# -> #@now_lac@#] [endurance #@before_endurance@# -> #@now_endurance@#/#@max_endurance@#] ");
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num12)));
				stringBuilder.Replace("#@before_lac@#", num4.ToString());
				stringBuilder.Replace("#@now_lac@#", num5.ToString());
				stringBuilder.Replace("#@before_endurance@#", num6.ToString());
				stringBuilder.Replace("#@now_endurance@#", num7.ToString());
				stringBuilder.Replace("#@max_endurance@#", num8.ToString());
				break;
			case 2416:
				literal4.Text = "ITEM_SOCKET_REPAIR";
				break;
			case 2417:
				literal4.Text = "Item Donate";
				stringBuilder.Append("#@donate_result@# : #@item_name@# #@count@#unit(s) (#@before_count@# -&rt #@result_count@#unit(s)) Imoral Point #@ip@# (#@before_ip@# -&rt #@result_ip@#");
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num4)));
				stringBuilder.Replace("#@before_count@#", num5.ToString());
				stringBuilder.Replace("#@count@#", num6.ToString());
				stringBuilder.Replace("#@result_count@#", num7.ToString());
				stringBuilder.Replace("#@before_ip@#", num8.ToString());
				stringBuilder.Replace("#@ip@#", num9.ToString());
				stringBuilder.Replace("#@result_ip@#", num10.ToString());
				if (num11 == 1)
				{
					stringBuilder.Replace("#@donate_result@#", "sucess");
				}
				else
				{
					stringBuilder.Replace("#@donate_result@#", "fail");
				}
				break;
			case 2420:
				literal4.Text = "Item Expiration";
				stringBuilder.Append("CHARACTER - #@character_name@#, ITEM NAME -#@item_name@#");
				stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num3, text2.ToString(), text.ToString()));
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5)));
				break;
			case 2428:
			case 2429:
			case 2430:
			case 2431:
				{
					switch (num)
					{
						case 2428:
							literal4.Text = "Item Awaken";
							break;
						case 2429:
							literal4.Text = "Item Awaken Deleted";
							break;
						case 2430:
							literal4.Text = "Item Awaken by Script";
							break;
						case 2431:
							literal4.Text = "Item Awaken Deleted by Script";
							break;
					}
					string text9 = "";
					text9 = GetItemOptionType(num4);
					if (!string.IsNullOrEmpty(text9))
					{
						stringBuilder.Append(text9 + ":" + num5);
					}
					text9 = GetItemOptionType(num6);
					if (!string.IsNullOrEmpty(text9))
					{
						stringBuilder.Append(" " + text9 + ":" + num7);
					}
					text9 = GetItemOptionType(num8);
					if (!string.IsNullOrEmpty(text9))
					{
						stringBuilder.Append(" " + text9 + ":" + num9);
					}
					text9 = GetItemOptionType(num10);
					if (!string.IsNullOrEmpty(text9))
					{
						stringBuilder.Append(" " + text9 + ":" + num11);
					}
					if (long.TryParse(text3, out var result))
					{
						text9 = GetItemOptionType(result);
						stringBuilder.Append(" " + text9 + ":" + text4);
					}
					break;
				}
			case 2501:
				literal4.Text = "Stall sale";
				if (num7 == 0)
				{
					stringBuilder.Append("[To #@target_name@#] #@item_name@# #@count@#unit(s) buying - #@result_gold@#Rupee(#@gold@#)");
				}
				else
				{
					stringBuilder.Append("[To #@target_name@#] #@item_name@# #@count@#unit(s) buying - (#@result_count@#unit(s) possession) - #@result_gold@#Rupee(#@gold@#)");
				}
				stringBuilder.Replace("#@target_name@#", link.CharacterPopup(ddlServer.SelectedValue, (int)num10, text3, ""));
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@count@#", num6.ToString());
				stringBuilder.Replace("#@result_count@#", num7.ToString());
				stringBuilder.Replace("#@result_gold@#", num9.ToString());
				stringBuilder.Replace("#@gold@#", string.Format("<font color='" + bluecolor + "'>+{0}</font>", num8));
				break;
			case 2502:
				literal4.Text = "Stall buying";
				if (num7 == 0)
				{
					stringBuilder.Append("[From #@target_name@#] #@item_name@# #@count@#unit(s) buying - #@result_gold@#Rupee(#@gold@#)");
				}
				else
				{
					stringBuilder.Append("[From #@target_name@#] #@item_name@# #@count@#unit(s) buying - (#@result_count@#unit(s) possession) - #@result_gold@#Rupee(#@gold@#)");
				}
				stringBuilder.Replace("#@target_name@#", link.CharacterPopup(ddlServer.SelectedValue, (int)num10, text3, ""));
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
				stringBuilder.Replace("#@count@#", num6.ToString());
				stringBuilder.Replace("#@result_count@#", num7.ToString());
				stringBuilder.Replace("#@result_gold@#", num9.ToString());
				stringBuilder.Replace("#@gold@#", $"<font color='red'>-{num8}</font>");
				break;
			case 2503:
				literal4.Text = "Bank transaction";
				if (num5 != 0)
				{
					if (num6 > 0)
					{
						stringBuilder.Append("Withdraw #@item_name@# #@count@#unit(s) - Inventory #@result_count@#unit(s) / Bank #@bank_result_count@#unit(s)");
					}
					else
					{
						stringBuilder.Append("Deposit #@item_name@# #@count@#unit(s) - Inventory #@result_count@#unit(s) / Bank #@bank_result_count@#unit(s)");
					}
					stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5, (int)num4 % 100, (int)num4 / 100, 300)));
					stringBuilder.Replace("#@count@#", Math.Abs(num6).ToString());
					stringBuilder.Replace("#@result_count@#", num7.ToString());
					stringBuilder.Replace("#@bank_result_count@#", num10.ToString());
				}
				else
				{
					if (num8 > 0)
					{
						stringBuilder.Append("#@gold@# Rupee withdrawal -Inventory  #@result_gold@#Rupee / Bank #@bank_result_gold@#Rupee");
					}
					else
					{
						stringBuilder.Append("#@gold@# Rupee Depositing - Inventory #@result_gold@#Rupee / Bank #@bank_result_gold@#Rupee");
					}
					stringBuilder.Replace("#@gold@#", Math.Abs(num8).ToString());
					stringBuilder.Replace("#@result_gold@#", num9.ToString());
					stringBuilder.Replace("#@bank_result_gold@#", num11.ToString());
				}
				break;
			case 2601:
				literal4.Text = "Creature Lv up";
				stringBuilder.Append("Creature #@creature_name@#(#@creature_id@#) (Lv #@lv@# -> Lv #@result_lv@#)");
				stringBuilder.Replace("#@creature_id@#", num4.ToString());
				stringBuilder.Replace("#@creature_name@#", text3);
				stringBuilder.Replace("#@lv@#", num5.ToString());
				stringBuilder.Replace("#@result_lv@#", num6.ToString());
				break;
			case 2602:
				{
					literal4.Text = "Creature Learn Skill";
					stringBuilder.Append("Creature #@creature_name@#(#@creature_id@#) have learned#@skill_name@# Lv #@skill_lv@#. #@result_jp@# JP (#@use_jp@# JP)");
					stringBuilder.Replace("#@creature_id@#", num4.ToString());
					stringBuilder.Replace("#@creature_name@#", text3);
					DataRow skillResource = resource.GetSkillResource(resource_server_id, (int)num5);
					if (skillResource != null)
					{
						if (skillResource["value"].ToString() == "")
						{
							stringBuilder.Replace("#@skill_name@#", $"[skill code : {num5}]");
						}
						else
						{
							stringBuilder.Replace("#@skill_name@#", skillResource["value"].ToString());
						}
					}
					else
					{
						stringBuilder.Replace("#@skill_name@#", $"[skill code : {num5}]");
					}
					stringBuilder.Replace("#@skill_lv@#", num6.ToString());
					stringBuilder.Replace("#@use_jp@#", $"<font color='red'>-{num7}</font>");
					stringBuilder.Replace("#@result_jp@#", num8.ToString());
					break;
				}
			case 2603:
				literal4.Text = "Creature Death";
				if (text4 == "PLYR")
				{
					stringBuilder.Append("Creature #@creature_name@#(#@creature_id@#)  has been killed by Player '#@character_name@#' - #@exp@# Exp(#@lost_exp@#)");
					stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num5, text3.ToString(), num5.ToString()));
				}
				else
				{
					stringBuilder.Append("Creature #@creature_name@#(#@creature_id@#)  has been killed by Monster '#@monster_name@#' - #@exp@# Exp(#@lost_exp@#)");
					stringBuilder.Replace("#@monster_name@#", resource.GetMonsterName(resource_server_id, (int)num5));
				}
				stringBuilder.Replace("#@creature_id@#", num4.ToString());
				stringBuilder.Replace("#@creature_name@#", text3);
				stringBuilder.Replace("#@exp@#", num12.ToString());
				stringBuilder.Replace("#@lost_exp@#", $"<font color='red'>-{num6}</font>");
				break;
			case 2604:
				literal4.Text = "Creature Evolution";
				stringBuilder.Append("Creature #@creature_name@#(#@creature_id@#) Evolution #@creature_species@# Lv #@lv@# -> #@result_creature_species@# Lv #@result_lv@#");
				stringBuilder.Replace("#@creature_id@#", num4.ToString());
				stringBuilder.Replace("#@creature_name@#", text3);
				stringBuilder.Replace("#@creature_species@#", resource.GetSummonName(resource_server_id, (int)num5));
				stringBuilder.Replace("#@result_creature_species@#", resource.GetSummonName(resource_server_id, (int)num6));
				stringBuilder.Replace("#@lv@#", num7.ToString());
				stringBuilder.Replace("#@result_lv@#", num8.ToString());
				break;
			case 2605:
				literal4.Text = "Creature Resurrection";
				if (num6 > 0)
				{
					stringBuilder.Append("[cashitem] Creature ID (#@creature_id@#) Resurrection, Exp #@result_exp@#(+#@repair_exp@#) ");
				}
				else
				{
					stringBuilder.Append("Creature ID (#@creature_id@#) Resurrection, Exp #@result_exp@#(+#@repair_exp@#) ");
				}
				stringBuilder.Replace("#@creature_id@#", num4.ToString());
				stringBuilder.Replace("#@repair_exp@#", num7.ToString());
				stringBuilder.Replace("#@result_exp@#", num8.ToString());
				break;
			case 2606:
				literal4.Text = "Change Creature Name";
				stringBuilder.Append("[#@by@#] #@old_creature_name@#(#@creature_id@#) ->  #@new_creature_name@#");
				stringBuilder.Replace("#@creature_id@#", num4.ToString());
				stringBuilder.Replace("#@old_creature_name@#", text3);
				stringBuilder.Replace("#@new_creature_name@#", text4);
				if (num5 == 0)
				{
					stringBuilder.Replace("#@by@#", "by Item");
				}
				else
				{
					stringBuilder.Replace("#@by@#", "by NPC");
				}
				break;
			case 2701:
				literal4.Text = "Warp";
				stringBuilder.Append("Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#) ->Coordinates[#@result_layer@#](#@result_axis_x@#, #@result_axis_y@#");
				stringBuilder.Replace("#@layer@#", num6.ToString());
				stringBuilder.Replace("#@result_layer@#", num7.ToString());
				stringBuilder.Replace("#@axis_x@#", num8.ToString());
				stringBuilder.Replace("#@axis_y@#", num9.ToString());
				stringBuilder.Replace("#@result_axis_x@#", num10.ToString());
				stringBuilder.Replace("#@result_axis_y@#", num11.ToString());
				break;
			case 2702:
				literal4.Text = "NPC_CONTACT";
				stringBuilder.Append("[NPC #@npc_name@#] #@npc_script@#");
				stringBuilder.Replace("#@npc_name@#", resource.GetNpcName(resource_server_id, (int)num5));
				stringBuilder.Replace("#@npc_script@#", text4);
				break;
			case 2703:
				literal4.Text = "NPC_PROCESS";
				stringBuilder.Append("[NPC #@npc_name@#] #@npc_script@#");
				stringBuilder.Replace("#@npc_name@#", resource.GetNpcName(resource_server_id, (int)num5));
				stringBuilder.Replace("#@npc_script@#", text4);
				literal4.Text = "NPC_PROCESS";
				stringBuilder.Append("[NPC #@npc_name@#] #@npc_script@# - Gold: #@gold@#→#@result_gold@#, Lac:#@lac@#→#@result_lac@#");
				stringBuilder.Replace("#@npc_name@#", resource.GetNpcName(resource_server_id, (int)num5));
				stringBuilder.Replace("#@lac@#", num6.ToString());
				stringBuilder.Replace("#@gold@#", num7.ToString());
				stringBuilder.Replace("#@result_lac@#", num8.ToString());
				stringBuilder.Replace("#@result_gold@#", num9.ToString());
				stringBuilder.Replace("#@npc_script@#", text4);
				break;
			case 2706:
				literal4.Text = "State Expiration";
				stringBuilder.Append("CHARACTER - #@character_name@#, STATE -#@state@#, STATE LV - #@state_lv@#");
				stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num3, text2.ToString(), text.ToString()));
				stringBuilder.Replace("#@state@#", resource.GetStateName(resource_server_id, (int)num4));
				stringBuilder.Replace("#@state_lv@#", num5.ToString());
				if ((int)num6 > 0)
				{
					stringBuilder.Append("(creature - #@creature_name@#)");
					stringBuilder.Replace("#@creature_name@#", resource.GetSummonName(resource_server_id, (int)num6));
				}
				break;
			case 2801:
				literal4.Text = "Quest start";
				stringBuilder.Append("Quest '#@quest_name@#' start");
				stringBuilder.Replace("#@quest_name@#", text3);
				break;
			case 2802:
				literal4.Text = "Quest give up";
				stringBuilder.Append("Quest '#@quest_name@#' give up");
				stringBuilder.Replace("#@quest_name@#", text3);
				break;
			case 2803:
				literal4.Text = "Quest finish";
				stringBuilder.Append("Quest '#@quest_name@#' finish");
				stringBuilder.Replace("#@quest_name@#", text3);
				break;
			case 2901:
				literal4.Text = "Create Guild ";
				stringBuilder.Append("'#@guild_name@#' Create Guild ");
				stringBuilder.Replace("#@guild_name@#", text3);
				break;
			case 2902:
				{
					literal4.Text = "Join Guild";
					stringBuilder.Append("'#@guild_name@#' Join Guild");
					stringBuilder.Replace("#@guild_name@#", text3);
					string text8 = "";
					switch (num6)
					{
						case 1L:
							text8 = "acceptance";
							break;
						case 2L:
							text8 = "refusal";
							break;
						default:
							text8 = "no answer";
							break;
					}
					stringBuilder.Append(" answer : " + text8);
					break;
				}
			case 2903:
				literal4.Text = "Breakup guild";
				stringBuilder.Append("'#@guild_name@#' Breakup guild");
				stringBuilder.Replace("#@guild_name@#", text3);
				break;
			case 2904:
				literal4.Text = "Guild Deportation ";
				stringBuilder.Append("Deported form '#@guild_name@#' guild'#@character_name@#'");
				stringBuilder.Replace("#@guild_name@#", text3);
				stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num6, text4.ToString(), num6.ToString()));
				break;
			case 2905:
				literal4.Text = "Guild Withdrawal";
				stringBuilder.Append("Withdraw frm '#@guild_name@#' guild");
				stringBuilder.Replace("#@guild_name@#", text3);
				break;
			case 2906:
				literal4.Text = "Guild name change";
				stringBuilder.Append("Guild name change #@guild_name@# -> #@result_guild_name@#");
				stringBuilder.Replace("#@guild_name@#", text2);
				stringBuilder.Replace("#@result_guild_name@#", text3);
				break;
			case 2907:
				literal4.Text = "Guild Master Change";
				stringBuilder.Append("'#@guild_name@#' Guild Master : '#@character_name@#' -> '#@character_name2@#'");
				stringBuilder.Replace("#@guild_name@#", text3);
				stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num6, text4.ToString(), num6.ToString()));
				stringBuilder.Replace("#@character_name2@#", link.CharacterPopup(server, (int)num3, text2.ToString(), num3.ToString()));
				break;
			case 2911:
				literal4.Text = "GUILD INVITE";
				stringBuilder.Append("'#@guild_name@#' guild invite : host : #@host_name@#,  guest : #@guest_name@#");
				stringBuilder.Replace("#@guild_name@#", text3);
				stringBuilder.Replace("#@host_name@#", text2);
				stringBuilder.Replace("#@guest_name@#", text4);
				break;
			case 2951:
				literal4.Text = "Alliance Create";
				stringBuilder.Append("Alliance [#@alliance_name@#(#@alliance_id@#)], Guild [#@guild_name@#(#@guild_id@#)]");
				stringBuilder.Replace("#@alliance_name@#", text4);
				stringBuilder.Replace("#@alliance_id@#", num6.ToString());
				stringBuilder.Replace("#@guild_name@#", text3);
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				break;
			case 2952:
				literal4.Text = "Alliance Join";
				stringBuilder.Append("Alliance [#@alliance_name@#(#@alliance_id@#)], Guild [#@guild_name@#(#@guild_id@#)]");
				stringBuilder.Replace("#@alliance_name@#", text4);
				stringBuilder.Replace("#@alliance_id@#", num6.ToString());
				stringBuilder.Replace("#@guild_name@#", text3);
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				break;
			case 2953:
				literal4.Text = "Alliance Destroy";
				stringBuilder.Append("Alliance [#@alliance_name@#(#@alliance_id@#)], Guild [#@guild_name@#(#@guild_id@#)]");
				stringBuilder.Replace("#@alliance_name@#", text4);
				stringBuilder.Replace("#@alliance_id@#", num6.ToString());
				stringBuilder.Replace("#@guild_name@#", text3);
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				break;
			case 2954:
				literal4.Text = "Alliance Kick";
				stringBuilder.Append("Alliance [#@alliance_name@#(#@alliance_id@#)], Guild [#@guild_name@#(#@guild_id@#)]");
				stringBuilder.Replace("#@alliance_name@#", text4);
				stringBuilder.Replace("#@alliance_id@#", num6.ToString());
				stringBuilder.Replace("#@guild_name@#", text3);
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				break;
			case 2955:
				literal4.Text = "Alliance Leave";
				stringBuilder.Append("Alliance [#@alliance_name@#(#@alliance_id@#)], Guild [#@guild_name@#(#@guild_id@#)]");
				stringBuilder.Replace("#@alliance_name@#", text4);
				stringBuilder.Replace("#@alliance_id@#", num6.ToString());
				stringBuilder.Replace("#@guild_name@#", text3);
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				break;
			case 3001:
				literal4.Text = "Item Shop(Check)";
				stringBuilder.Append("Count of Items of ItemShop Bank  : #@cashitem_count@# ");
				stringBuilder.Replace("#@cashitem_count@#", num4.ToString());
				break;
			case 3002:
				literal4.Text = "Item Shop(Withdraw)";
				stringBuilder.Append("Withdraw #@item_name@# #@count@#unit(s) from ItemShop Bank - ItemShop ID : #@box_id@#");
				stringBuilder.Replace("#@box_id@#", num4.ToString());
				stringBuilder.Replace("#@item_name@#", string.Format("<font color='" + itemnamecolor + "'>{0}</font>", resource.GetItemName(resource_server_id, (int)num5)));
				stringBuilder.Replace("#@count@#", num6.ToString());
				break;
			case 3003:
				literal4.Text = "Item Shop(Temp)";
				stringBuilder.Append("ItemShop ID : #@box_id@#, count : #@count@#,temp ID #@item_temp_id@#");
				stringBuilder.Replace("#@box_id@#", num4.ToString());
				stringBuilder.Replace("#@count@#", num5.ToString());
				stringBuilder.Replace("#@item_temp_id@#", num12.ToString());
				break;
			case 3100:
				literal4.Text = "REQUEST_DUNGEON_RAID";
				if (text4 == "REQUEST")
				{
					stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - Raid Proposal ");
				}
				else
				{
					stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - Raid Cancel ");
				}
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				stringBuilder.Replace("#@dungeon_id@#", num12.ToString());
				break;
			case 3101:
				literal4.Text = "END_DUNGEON_RAID";
				if (text4 == "SUCS")
				{
					stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - Raid Success : the No. of Bossmonsters that you have killed #@kill_boss_count@#");
				}
				else
				{
					stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - Raid Failure : the No. of Bossmonsters that you have killed #@kill_boss_count@#");
				}
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				stringBuilder.Replace("#@dungeon_id@#", num12.ToString());
				stringBuilder.Replace("#@kill_boss_count@#", num11.ToString());
				break;
			case 3102:
				literal4.Text = "DUNGEON_CHANGE_OWNER";
				stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - Controller ID #@controller_id@#");
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				stringBuilder.Replace("#@dungeon_id@#", num12.ToString());
				stringBuilder.Replace("#@controller_id@#", num11.ToString());
				break;
			case 3103:
				literal4.Text = "END_DUNGEON_SIEGE";
				if (text4 == "TIMEOUT")
				{
					stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - Siege has been finished as Dungeon Siege time over");
				}
				else
				{
					stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - Siege has been finished because of Dungeon stone destruction");
				}
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				stringBuilder.Replace("#@dungeon_id@#", num12.ToString());
				break;
			case 3104:
				literal4.Text = "SET_DUNGEON_TAX_RATE";
				stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - New tax rate  #@tax@#");
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				stringBuilder.Replace("#@dungeon_id@#", num12.ToString());
				stringBuilder.Replace("#@tax@#", num11.ToString());
				break;
			case 3105:
				literal4.Text = "DRAW_DUNGEON_TAX";
				stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@# - User possession Rupee(including tax) #@user_gold@# Rupee,  Tax Rupee #@tax_gold@# Rupee, User possessionLak(including tax)  #@user_lac@# Lac, Tax Lak  #@tax_lac@#  Lac");
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				stringBuilder.Replace("#@dungeon_id@#", num12.ToString());
				stringBuilder.Replace("#@user_gold@#", num8.ToString());
				stringBuilder.Replace("#@tax_gold@#", num9.ToString());
				stringBuilder.Replace("#@user_lac@#", num10.ToString());
				stringBuilder.Replace("#@tax_lac@#", num11.ToString());
				break;
			case 3107:
				literal4.Text = "DROP_DUNGEON_OWNERSHIP";
				stringBuilder.Append("GuildID #@guild_id@#, Dungeon ID #@dungeon_id@#");
				stringBuilder.Replace("#@guild_id@#", num5.ToString());
				stringBuilder.Replace("#@dungeon_id@#", num12.ToString());
				break;
			case 3200:
				{
					literal4.Text = "AUTO_USER_CHECKED";
					stringBuilder.Append("(auto_code : #@code@#) #@msg@#");
					stringBuilder.Replace("#@code@#", num11.ToString());
					long num26 = num11;
					if (num26 <= 8 && num26 >= 1)
					{
						switch (num26 - 1)
						{
							case 0L:
								stringBuilder.Replace("#@msg@#", "Try Connect before Server UP");
								break;
							case 1L:
								stringBuilder.Replace("#@msg@#", "Already Auto User");
								break;
							case 2L:
								stringBuilder.Replace("#@msg@#", "Try drop 0 item");
								break;
							case 3L:
								stringBuilder.Replace("#@msg@#", "Hackshield : wrong response");
								break;
							case 4L:
								stringBuilder.Replace("#@msg@#", "Hackshield : wrong Hackshild File");
								break;
							case 5L:
								stringBuilder.Replace("#@msg@#", "Hackshield : not response Initial Value");
								break;
							case 6L:
								stringBuilder.Replace("#@msg@#", "GM did command [set_auto_user]");
								break;
							case 7L:
								stringBuilder.Replace("#@msg@#", "hackingtool : checked by client");
								break;
						}
					}
					break;
				}
			case 3300:
				literal4.Text = "[Auction] Begin";
				stringBuilder.Append("[AuctionID : #@auction_id@#] [Item : #@item_name@#] [Number of item: #@number_of_items@#]<br />");
				stringBuilder.Append("[Starting bid price: #@starting_bid_price@#] [Instant purchase Price: #@now_price@#] [Duration: #@duration@# hour]<br />");
				stringBuilder.Append("[RegistrationFee: #@registatioin_fee@#] [Rupees : #@gold_before_register@# -> #@gold_after_register@#]<br />");
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num4, "a"));
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num2));
				stringBuilder.Replace("#@starting_bid_price@#", num5.ToString("#,##0"));
				stringBuilder.Replace("#@now_price@#", num6.ToString("#,##0"));
				stringBuilder.Replace("#@duration@#", ((int)(num7 / 3600)).ToString());
				stringBuilder.Replace("#@gold_before_register@#", num8.ToString("#,##0"));
				stringBuilder.Replace("#@registatioin_fee@#", num9.ToString("#,##0"));
				stringBuilder.Replace("#@gold_after_register@#", num10.ToString("#,##0"));
				stringBuilder.Replace("#@number_of_items@#", num11.ToString("#,##0"));
				break;
			case 3301:
				literal4.Text = "[Auction] Begin item splited";
				stringBuilder.Append("[AuctionID : #@auction_id@#] [Item : #@item_name@#] [Total amount of owned item : #@total_amount@#] [Number of item: #@number_of_items@#] [Number of item user registers: #@registed_item_count@#]");
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num4, "a"));
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num2));
				stringBuilder.Replace("#@total_amount@#", num5.ToString("#,##0"));
				stringBuilder.Replace("#@number_of_items@#", num6.ToString("#,##0"));
				stringBuilder.Replace("#@registed_item_count@#", num7.ToString("#,##0"));
				break;
			case 3302:
				literal4.Text = "[Auction] Bid";
				stringBuilder.Append("[AuctionID : #@auction_id@#] [Item : #@item_name@#] [Number of item: #@number_of_items@#] <br />");
				stringBuilder.Append("[Avatar who bid highest price: #@before_character_name@#] [Highest bidding price: #@before_price@#]<br />");
				stringBuilder.Append("[New avatar who bid highest price: #@character_name@#] [New highest bidding price: #@price@#] ");
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num2));
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num4, "a"));
				stringBuilder.Replace("#@account@#", text);
				stringBuilder.Replace("#@before_character_name@#", link.CharacterPopup(server, (int)num5, text3, ""));
				stringBuilder.Replace("#@before_price@#", num6.ToString("#,##0"));
				stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num7, text2, text));
				stringBuilder.Replace("#@price@#", num8.ToString("#,##0"));
				stringBuilder.Replace("#@number_of_items@#", num11.ToString("#,##0"));
				break;
			case 3303:
				literal4.Text = "[Auction] Buy now";
				stringBuilder.Append("[AuctionID : #@auction_id@#] [Item : #@item_name@#]<br />");
				stringBuilder.Append("[Avatar who bid highest price: #@before_character_name@#] [Highest bidding price: #@before_price@#]<br />");
				stringBuilder.Append("[Aavatar who bought now: #@character_name@#] [Price: #@price@#] [Rest rupee(s): #@rest_rupee@#]<br />");
				stringBuilder.Append("[price for sales: #@price_for_sales@#] [Refund of registration fee: #@registration_fee@#]");
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num2));
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num4, "a"));
				stringBuilder.Replace("#@account@#", text);
				stringBuilder.Replace("#@before_character_name@#", link.CharacterPopup(server, (int)num7, text3, ""));
				stringBuilder.Replace("#@before_price@#", num8.ToString("#,##0"));
				stringBuilder.Replace("#@character_name@#", link.CharacterPopup(server, (int)num7, text2, text));
				stringBuilder.Replace("#@price@#", num5.ToString("#,##0"));
				stringBuilder.Replace("#@rest_rupee@#", num6.ToString("#,##0"));
				stringBuilder.Replace("#@price_for_sales@#", num10.ToString("#,##0"));
				stringBuilder.Replace("#@registration_fee@#", num11.ToString("#,##0"));
				break;
			case 3304:
				literal4.Text = "[Auction] Cancellation";
				stringBuilder.Append("[AuctionID : #@auction_id@#] [Item : #@item_name@#]<br />");
				stringBuilder.Append("[Avatar who bid highest price: #@before_character_name@#] [Highest bidding price: #@before_price@#]<br />");
				stringBuilder.Append("[Refund of registration fee: #@registration_fee@#]");
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num2));
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num4, "a"));
				stringBuilder.Replace("#@before_character_name@#", link.CharacterPopup(server, (int)num5, text3, ""));
				stringBuilder.Replace("#@before_price@#", num6.ToString("#,##0"));
				stringBuilder.Replace("#@registration_fee@#", num11.ToString("#,##0"));
				break;
			case 3305:
				literal4.Text = "[Auction] Sold";
				stringBuilder.Append("[AuctionID : #@auction_id@#] [Item : #@item_name@#] [Seller : #@seller_character_name@#]<br />");
				stringBuilder.Append("[Avatar who bid highest price: #@before_character_name@#] [Highest bidding price: #@before_price@#]<br />");
				stringBuilder.Append("[price for sales: #@price_for_sales@#] [Refund of registration fee: #@registration_fee@#]");
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num2));
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num4, "a"));
				stringBuilder.Replace("#@account@#", text);
				stringBuilder.Replace("#@before_character_name@#", link.CharacterPopup(server, (int)num3, text2, ""));
				stringBuilder.Replace("#@before_price@#", num6.ToString("#,##0"));
				stringBuilder.Replace("#@seller_character_name@#", link.CharacterPopup(server, (int)num9, text3, ""));
				stringBuilder.Replace("#@price_for_sales@#", num10.ToString("#,##0"));
				stringBuilder.Replace("#@registration_fee@#", num11.ToString("#,##0"));
				break;
			case 3306:
				literal4.Text = "[Auction] Not sold";
				stringBuilder.Append("[AuctionID : #@auction_id@#] [Item : #@item_name@#] [Seller : #@seller_character_name@#]<br />");
				stringBuilder.Append("[Refund of registration fee: #@registration_fee@#]");
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num2));
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num4, "a"));
				stringBuilder.Replace("#@account@#", text);
				stringBuilder.Replace("#@seller_character_name@#", link.CharacterPopup(server, (int)num3, text2, ""));
				stringBuilder.Replace("#@registration_fee@#", num11.ToString("#,##0"));
				break;
			case 3350:
				{
					literal4.Text = "KEEPING_BEGIN";
					newValue = "";
					long num24 = num6;
					if (num24 <= 5)
					{
						if (num24 < 2)
						{
							goto IL_4193;
						}
						switch (num24 - 2)
						{
							case 0L:
								goto IL_4142;
							case 1L:
								goto IL_414b;
							case 2L:
								goto IL_4154;
							case 3L:
								goto IL_415d;
						}
					}
					if (num24 > 35 || num24 < 31)
					{
						goto IL_4193;
					}
					switch (num24 - 31)
					{
						case 0L:
							break;
						case 1L:
							goto IL_416f;
						case 2L:
							goto IL_4178;
						case 3L:
							goto IL_4181;
						case 4L:
							goto IL_418a;
						default:
							goto IL_4193;
					}
					newValue = "Rupee-Cost";
					goto IL_419a;
				}
			case 3351:
				{
					literal4.Text = "KEEPING_TAKE";
					long num23 = num6;
					if (num23 <= 5)
					{
						if (num23 < 2)
						{
							goto IL_4368;
						}
						switch (num23 - 2)
						{
							case 0L:
								goto IL_4317;
							case 1L:
								goto IL_4320;
							case 2L:
								goto IL_4329;
							case 3L:
								goto IL_4332;
						}
					}
					if (num23 > 35 || num23 < 31)
					{
						goto IL_4368;
					}
					switch (num23 - 31)
					{
						case 0L:
							break;
						case 1L:
							goto IL_4344;
						case 2L:
							goto IL_434d;
						case 3L:
							goto IL_4356;
						case 4L:
							goto IL_435f;
						default:
							goto IL_4368;
					}
					newValue = "Rupee-Cost";
					goto IL_436f;
				}
			case 3352:
				{
					literal4.Text = "KEEPING_EXPIRED";
					long num21 = num6;
					if (num21 <= 5)
					{
						if (num21 < 2)
						{
							goto IL_44d7;
						}
						switch (num21 - 2)
						{
							case 0L:
								goto IL_4486;
							case 1L:
								goto IL_448f;
							case 2L:
								goto IL_4498;
							case 3L:
								goto IL_44a1;
						}
					}
					if (num21 > 35 || num21 < 31)
					{
						goto IL_44d7;
					}
					switch (num21 - 31)
					{
						case 0L:
							break;
						case 1L:
							goto IL_44b3;
						case 2L:
							goto IL_44bc;
						case 3L:
							goto IL_44c5;
						case 4L:
							goto IL_44ce;
						default:
							goto IL_44d7;
					}
					newValue = "Rupee-Cost";
					goto IL_44de;
				}
			case 3400:
				literal4.Text = "PARTY_CREATE";
				stringBuilder.AppendFormat("Party[#@party_name@#], #@party_type@# - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)");
				stringBuilder.Replace("#@party_name@#", $"{text3}({num4})");
				stringBuilder.Replace("#@party_type@#", GetPartyType(num8));
				stringBuilder.Replace("#@layer@#", num7.ToString());
				stringBuilder.Replace("#@axis_x@#", num5.ToString());
				stringBuilder.Replace("#@axis_y@#", num6.ToString());
				break;
			case 3401:
				literal4.Text = "PARTY_DESTROY";
				stringBuilder.AppendFormat("Party[#@party_name@#], #@detroy_reason@# - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)");
				stringBuilder.Replace("#@party_name@#", $"{text3}({num4})");
				stringBuilder.Replace("#@detroy_reason@#", GetPartyDestroyType(num8));
				stringBuilder.Replace("#@layer@#", num7.ToString());
				stringBuilder.Replace("#@axis_x@#", num5.ToString());
				stringBuilder.Replace("#@axis_y@#", num6.ToString());
				break;
			case 3402:
				literal4.Text = "PARTY_INVITE";
				stringBuilder.AppendFormat("Party[#@party_name@#] #@party_type@# - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)- #@target@# Coordinates[#@target_layer@#](#@target_axis_x@#, #@target_axis_y@#)");
				stringBuilder.Replace("#@party_name@#", $"(party_id:{num4})");
				stringBuilder.Replace("#@party_type@#", GetPartyType(num8));
				stringBuilder.Replace("#@layer@#", num7.ToString());
				stringBuilder.Replace("#@axis_x@#", num5.ToString());
				stringBuilder.Replace("#@axis_y@#", num6.ToString());
				stringBuilder.Replace("#@target@#", link.CharacterPopup(server, (int)num8, text4, text3));
				stringBuilder.Replace("#@target_layer@#", num12.ToString());
				stringBuilder.Replace("#@target_axis_x@#", num10.ToString());
				stringBuilder.Replace("#@target_axis_y@#", num11.ToString());
				break;
			case 3403:
				{
					literal4.Text = "PARTY_JOIN";
					stringBuilder.AppendFormat("Party[#@party_name@#]#@party_join_type@# - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)");
					stringBuilder.Replace("#@party_name@#", $"{text3}({num4})");
					stringBuilder.Replace("#@layer@#", num7.ToString());
					stringBuilder.Replace("#@axis_x@#", num5.ToString());
					stringBuilder.Replace("#@axis_y@#", num6.ToString());
					long num20 = num8;
					if (num20 <= 1 && num20 >= 0)
					{
						switch (num20)
						{
							case 0L:
								break;
							case 1L:
								goto IL_483b;
							default:
								goto IL_4852;
						}
						stringBuilder.Replace("#@party_join_type@#", "");
						break;
					}
					goto IL_4852;
				}
			case 3404:
				{
					literal4.Text = "PARTY_LEAVE";
					stringBuilder.AppendFormat("Party[#@party_name@#]#@party_out_type@# - Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)");
					stringBuilder.Replace("#@party_name@#", $"{text3}({num4})");
					stringBuilder.Replace("#@layer@#", num7.ToString());
					stringBuilder.Replace("#@axis_x@#", num5.ToString());
					stringBuilder.Replace("#@axis_y@#", num6.ToString());
					long num19 = num8;
					if (num19 <= 3 && num19 >= 0)
					{
						switch (num19)
						{
							case 0L:
								break;
							case 1L:
								goto IL_4924;
							case 2L:
								goto IL_493b;
							case 3L:
								goto IL_4952;
							default:
								goto IL_4969;
						}
						stringBuilder.Replace("#@party_out_type@#", "");
						break;
					}
					goto IL_4969;
				}
			case 3405:
				literal4.Text = "PARTY_KICK";
				stringBuilder.Append(string.Format("[Party ID: {0}] [Target : {1}] Coordinates[{4}]({2}, {3})", num4, link.CharacterPopup(server, (int)num9, text4, text3), num5, num6, num7));
				break;
			case 3406:
				literal4.Text = "PARTY_PROMOTE";
				stringBuilder.Append(string.Format("[Party ID: {0}] [Target : {1}] Coordinates[{4}]({2}, {3})", num4, link.CharacterPopup(server, (int)num9, text4, text3), num5, num6, num7));
				break;
			case 3500:
				{
					literal4.Text = "HUNTERHOLIC_CREATE";
					stringBuilder.Append("\r\n\t\t\t\t\t\t\t[roomname : #@room_name@# (#@instance_type@#)]\r\n\t\t\t\t\t\t\t[id : #@hunterholick_id@#]\r\n\t\t\t\t\t\t\t[room_number : #@room_number@#]\r\n\t\t\t\t\t\t\t[max_user : #@max_user_number@#]\r\n\t\t\t\t\t\t\t[#@lock_room@#]\r\n\t\t\t\t\t\t\t[total room cnt : #@total_room_cnt@#]\r\n\t\t\t\t\t\t\t[#@postion@#]\r\n\t\t\t\t\t");
					stringBuilder.Replace("#@hunterholick_id@#", num4.ToString());
					long num18 = num5;
					if (num18 > 9 || num18 < 0)
					{
						goto IL_4ba8;
					}
					switch (num18)
					{
						case 0L:
							break;
						case 1L:
							goto IL_4aeb;
						case 2L:
							goto IL_4b02;
						case 3L:
							goto IL_4b19;
						case 4L:
							goto IL_4b30;
						case 5L:
							goto IL_4b44;
						case 6L:
							goto IL_4b58;
						case 7L:
							goto IL_4b6c;
						case 8L:
							goto IL_4b80;
						case 9L:
							goto IL_4b94;
						default:
							goto IL_4ba8;
					}
					stringBuilder.Replace("#@instance_type@#", "~35Lv");
					goto IL_4bba;
				}
			case 3501:
				{
					literal4.Text = "HUNTERHOLIC_JOIN";
					stringBuilder.Append("\r\n\t\t\t\t\t\t\t[roomname : #@room_name@# (#@instance_type@#)]\r\n\t\t\t\t\t\t\t[id : #@hunterholick_id@#]\r\n\t\t\t\t\t\t\t[room_number : #@room_number@#]\r\n\t\t\t\t\t\t\t[max_user : #@max_user_number@#]\r\n\t\t\t\t\t\t\t[#@lock_room@#]\r\n\t\t\t\t\t\t\t[user cnt : #@total_user_cnt@#]\r\n\t\t\t\t\t\t\t[#@postion@#]\r\n\t\t\t\t\t");
					stringBuilder.Replace("#@hunterholick_id@#", num4.ToString());
					long num16 = num5;
					if (num16 > 9 || num16 < 0)
					{
						goto IL_4db0;
					}
					switch (num16)
					{
						case 0L:
							break;
						case 1L:
							goto IL_4cf3;
						case 2L:
							goto IL_4d0a;
						case 3L:
							goto IL_4d21;
						case 4L:
							goto IL_4d38;
						case 5L:
							goto IL_4d4c;
						case 6L:
							goto IL_4d60;
						case 7L:
							goto IL_4d74;
						case 8L:
							goto IL_4d88;
						case 9L:
							goto IL_4d9c;
						default:
							goto IL_4db0;
					}
					stringBuilder.Replace("#@instance_type@#", "~35Lv");
					goto IL_4dc2;
				}
			case 3502:
				{
					literal4.Text = "HUNTERHOLIC_LEAVE";
					stringBuilder.Append("\r\n\t\t\t\t\t\t\t[roomname : #@room_name@# (#@instance_type@#)]\r\n\t\t\t\t\t\t\t[id : #@hunterholick_id@#]\r\n\t\t\t\t\t\t\t[room_number : #@room_number@#]\r\n\t\t\t\t\t\t\t[max_user : #@max_user_number@#]\r\n\t\t\t\t\t\t\t[#@lock_room@#]\r\n\t\t\t\t\t\t\t[user cnt : #@total_user_cnt@#]\r\n\t\t\t\t\t\t\t[#@postion@#]\r\n\t\t\t\t\t");
					stringBuilder.Replace("#@hunterholick_id@#", num4.ToString());
					long num15 = num5;
					if (num15 > 9 || num15 < 0)
					{
						goto IL_4fb8;
					}
					switch (num15)
					{
						case 0L:
							break;
						case 1L:
							goto IL_4efb;
						case 2L:
							goto IL_4f12;
						case 3L:
							goto IL_4f29;
						case 4L:
							goto IL_4f40;
						case 5L:
							goto IL_4f54;
						case 6L:
							goto IL_4f68;
						case 7L:
							goto IL_4f7c;
						case 8L:
							goto IL_4f90;
						case 9L:
							goto IL_4fa4;
						default:
							goto IL_4fb8;
					}
					stringBuilder.Replace("#@instance_type@#", "~35Lv");
					goto IL_4fca;
				}
			case 3503:
				literal4.Text = "HUNTERHOLIC_DESTROY";
				stringBuilder.Append("\r\n\t\t\t\t\t\t\t[roomname : #@room_name@# (#@instance_type@#)]\r\n\t\t\t\t\t\t\t[id : #@hunterholick_id@#]\r\n\t\t\t\t\t\t\t[room_number : #@room_number@#]\r\n\t\t\t\t\t\t\t[max_user : #@max_user_number@#]\r\n\t\t\t\t\t\t\t[#@lock_room@#]\r\n\t\t\t\t\t\t\t[#@postion@#]\r\n\t\t\t\t\t");
				stringBuilder.Replace("#@hunterholick_id@#", num4.ToString());
				num13 = num5;
				if (num13 > 9 || num13 < 0)
				{
					goto IL_51c0;
				}
				switch (num13)
				{
					case 0L:
						break;
					case 1L:
						goto IL_5103;
					case 2L:
						goto IL_511a;
					case 3L:
						goto IL_5131;
					case 4L:
						goto IL_5148;
					case 5L:
						goto IL_515c;
					case 6L:
						goto IL_5170;
					case 7L:
						goto IL_5184;
					case 8L:
						goto IL_5198;
					case 9L:
						goto IL_51ac;
					default:
						goto IL_51c0;
				}
				stringBuilder.Replace("#@instance_type@#", "~35Lv");
				goto IL_51d2;
			case 3504:
				literal4.Text = "HUNTERHOLIC_BEGIN";
				stringBuilder.Append("\r\n\t\t\t\t\t\t\t[roomname : #@room_name@#]\r\n\t\t\t\t\t\t\t[id : #@hunterholick_id@#]\r\n\t\t\t\t\t\t\t[room_number : #@room_number@#]\r\n\t\t\t\t\t\t\t[max_user : #@max_user_number@#]\r\n\t\t\t\t\t\t\t[#@lock_room@#]\r\n\t\t\t\t\t\t\t[#@postion@#]\r\n\t\t\t\t\t");
				stringBuilder.Replace("#@hunterholick_id@#", num4.ToString());
				num13 = num5;
				if (num13 > 9 || num13 < 0)
				{
					goto IL_53b4;
				}
				switch (num13)
				{
					case 0L:
						break;
					case 1L:
						goto IL_52f7;
					case 2L:
						goto IL_530e;
					case 3L:
						goto IL_5325;
					case 4L:
						goto IL_533c;
					case 5L:
						goto IL_5350;
					case 6L:
						goto IL_5364;
					case 7L:
						goto IL_5378;
					case 8L:
						goto IL_538c;
					case 9L:
						goto IL_53a0;
					default:
						goto IL_53b4;
				}
				stringBuilder.Replace("#@instance_type@#", "~35Lv");
				goto IL_53c6;
			case 3505:
				literal4.Text = "HUNTERHOLIC_END";
				stringBuilder.Append("\r\n\t\t\t\t\t\t\t[roomname : #@room_name@# (#@instance_type@#)]\r\n\t\t\t\t\t\t\t[id : #@hunterholick_id@#]\r\n\t\t\t\t\t\t\t[room_number : #@room_number@#]\r\n\t\t\t\t\t\t\t[max_user : #@max_user_number@#]\r\n\t\t\t\t\t\t\t[#@lock_room@#]\r\n\t\t\t\t\t\t\t[#@postion@#]\r\n\t\t\t\t\t");
				stringBuilder.Replace("#@hunterholick_id@#", num4.ToString());
				num13 = num5;
				if (num13 > 9 || num13 < 0)
				{
					goto IL_557c;
				}
				switch (num13)
				{
					case 0L:
						break;
					case 1L:
						goto IL_54bf;
					case 2L:
						goto IL_54d6;
					case 3L:
						goto IL_54ed;
					case 4L:
						goto IL_5504;
					case 5L:
						goto IL_5518;
					case 6L:
						goto IL_552c;
					case 7L:
						goto IL_5540;
					case 8L:
						goto IL_5554;
					case 9L:
						goto IL_5568;
					default:
						goto IL_557c;
				}
				stringBuilder.Replace("#@instance_type@#", "~35Lv");
				goto IL_558e;
			case 3506:
				literal4.Text = "HUNTERHOLIC_QUIT";
				stringBuilder.Append("\r\n\r\n\t\t\t\t\t\t\t[id : #@hunterholick_id@#]\r\n\t\t\t\t\t\t\t[roomname : #@room_name@#]\r\n\t\t\t\t\t\t\t[room_number : #@room_number@#]\r\n\t\t\t\t\t\t\t[hollic_point : #@hollic_point@# (#@total_hollic_point@#)]\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t[exp : #@exp@# (#@total_exp@#)]\r\n\t\t\t\t\t\t\t[jp : #@jp@# (#@total_jp@#)]\r\n\r\n\t\t\t\t\t");
				stringBuilder.Replace("#@hunterholick_id@#", num4.ToString());
				stringBuilder.Replace("#@room_number@#", num5.ToString());
				num13 = num6;
				if (num13 <= 2 && num13 >= -1)
				{
					switch (num13 - -1)
					{
						case 0L:
							stringBuilder.Replace("#@result@#", "unknown");
							break;
						case 1L:
							stringBuilder.Replace("#@result@#", "success");
							break;
						case 2L:
							stringBuilder.Replace("#@result@#", "failure(death)");
							break;
						case 3L:
							stringBuilder.Replace("#@result@#", "give up or time over");
							break;
					}
				}
				stringBuilder.Replace("#@hollic_point@#", num7.ToString());
				stringBuilder.Replace("#@total_hollic_point@#", num8.ToString());
				stringBuilder.Replace("#@exp@#", num9.ToString());
				stringBuilder.Replace("#@total_exp@#", num10.ToString());
				stringBuilder.Replace("#@jp@#", num11.ToString());
				stringBuilder.Replace("#@total_jp@#", num12.ToString());
				stringBuilder.Replace("#@room_name@#", text3);
				break;
			case 3600:
				literal4.Text = "RANKING_SETTLE";
				stringBuilder.Append($"RANK_Type : {num4}");
				break;
			case 3601:
				literal4.Text = "RANKING_TOP_RECORD";
				stringBuilder.Append(string.Format("RANK_Type : {0}_RANK :{1}_POINT:{2}_AVATAR :{3}({4})", num4, num3, num5, num6, text2, num3));
				break;
			case 3700:
				literal4.Text = "Compete Request";
				stringBuilder.Append($"to {text4}");
				break;
			case 3701:
				text7 = "";
				literal4.Text = "Compete Answer";
				num13 = num11;
				if (num13 > 3 || num13 < 0)
				{
					goto IL_5855;
				}
				switch (num13)
				{
					case 0L:
						break;
					case 1L:
						goto IL_583a;
					case 2L:
						goto IL_5843;
					case 3L:
						goto IL_584c;
					default:
						goto IL_5855;
				}
				text7 = "accept";
				goto IL_585c;
			case 3702:
				literal4.Text = "Compete Begin";
				stringBuilder.Append($"with {text4}");
				break;
			case 3703:
				literal4.Text = "Compete End";
				text5 = "";
				text6 = "";
				num13 = num11;
				if (num13 > 5 || num13 < 0)
				{
					goto IL_591c;
				}
				switch (num13)
				{
					case 0L:
						break;
					case 1L:
						goto IL_58ef;
					case 2L:
						goto IL_58f8;
					case 3L:
						goto IL_5901;
					case 4L:
						goto IL_590a;
					case 5L:
						goto IL_5913;
					default:
						goto IL_591c;
				}
				text5 = "Dead";
				goto IL_5923;
			case 3800:
				literal4.Text = "Creature expired";
				stringBuilder.AppendFormat("expired Period for the {1}({2}) in the farm{0}<br/>", num5.ToString(), text3, resource.GetSummonName(resource_server_id, (int)num6), num9.ToString());
				stringBuilder.AppendFormat("the Point of expiration :{0}", text4);
				break;
			case 3801:
				literal4.Text = "Put the creature";
				stringBuilder.AppendFormat("Put the {1}({2}:EXP-{3}) in the farm({0})<br/>", num5.ToString(), text3, resource.GetSummonName(resource_server_id, (int)num6), num9.ToString());
				stringBuilder.AppendFormat("option:{0}-{1},{2}-{3}", "cracker", GetBool(num10), "premium", GetBool(num11));
				break;
			case 3802:
				literal4.Text = "Take the creature back";
				stringBuilder.AppendFormat("Take the {1}({2}) back from the farm({0})<br/>", num5.ToString(), text3, resource.GetSummonName(resource_server_id, (int)num6));
				stringBuilder.AppendFormat("EXP :{0}({1}) -->{2}({3})", num8.ToString(), num9.ToString(), num10.ToString(), num11.ToString());
				break;
			case 3803:
				literal4.Text = "Take care of the creature";
				stringBuilder.AppendFormat("Take care of the {1}({2}) in the farm({0})<br/>", num5.ToString(), text3, resource.GetSummonName(resource_server_id, (int)num6));
				break;
			default:
				{
					literal4.Text = num.ToString();
					stringBuilder.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}", num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, num12, text, text2, text3, text4);
					break;
				}
			IL_5913:
				text5 = "move to other area";
				goto IL_5923;
			IL_590a:
				text5 = "attacked from monster or pk";
				goto IL_5923;
			IL_5901:
				text5 = "Timeout";
				goto IL_5923;
			IL_58f8:
				text5 = "leave from compete area";
				goto IL_5923;
			IL_58ef:
				text5 = "Logout";
				goto IL_5923;
			IL_08da:
				stringBuilder.Replace("#@pcbang@#", "");
				break;
			IL_591c:
				text5 = "error";
				goto IL_5923;
			IL_5923:
				num13 = num10;
				if (num13 <= 1 && num13 >= 0)
				{
					switch (num13)
					{
						case 0L:
							break;
						case 1L:
							goto IL_59a5;
						default:
							goto IL_59fc;
					}
					text6 = "defeat";
					if (num11 == 0 || num11 == 1 || num11 == 2 || num11 == 5)
					{
						stringBuilder.Append($"{text2}{text5}, {text6}");
					}
					else
					{
						stringBuilder.Append($"{text5}, {text6}");
					}
					break;
				}
				goto IL_59fc;
			IL_59a5:
				text6 = "victory";
				if (num11 == 0 || num11 == 1 || num11 == 2 || num11 == 5)
				{
					stringBuilder.Append($"{text4}{text5}, {text6}");
				}
				else
				{
					stringBuilder.Append($"{text5}, {text6}");
				}
				break;
			IL_59fc:
				text6 = "error";
				stringBuilder.Append(text6);
				break;
			IL_584c:
				text7 = "reject(timeout)";
				goto IL_585c;
			IL_5843:
				text7 = "reject(auto)";
				goto IL_585c;
			IL_583a:
				text7 = "reject";
				goto IL_585c;
			IL_5855:
				text7 = "error";
				goto IL_585c;
			IL_585c:
				stringBuilder.Append($"{text4} - {text7}");
				break;
			IL_5568:
				stringBuilder.Replace("#@instance_type@#", "155Lv~");
				goto IL_558e;
			IL_5554:
				stringBuilder.Replace("#@instance_type@#", "140~155Lv");
				goto IL_558e;
			IL_5540:
				stringBuilder.Replace("#@instance_type@#", "125~140Lv");
				goto IL_558e;
			IL_552c:
				stringBuilder.Replace("#@instance_type@#", "110~125Lv");
				goto IL_558e;
			IL_5518:
				stringBuilder.Replace("#@instance_type@#", "95~110Lv");
				goto IL_558e;
			IL_5504:
				stringBuilder.Replace("#@instance_type@#", "80~95Lv");
				goto IL_558e;
			IL_54ed:
				stringBuilder.Replace("#@instance_type@#", "65~80Lv");
				goto IL_558e;
			IL_54d6:
				stringBuilder.Replace("#@instance_type@#", "50~65Lv");
				goto IL_558e;
			IL_54bf:
				stringBuilder.Replace("#@instance_type@#", "35~50Lv");
				goto IL_558e;
			IL_557c:
				stringBuilder.Replace("#@instance_type@#", "unknown");
				goto IL_558e;
			IL_558e:
				stringBuilder.Replace("#@room_number@#", num6.ToString());
				stringBuilder.Replace("#@max_user_number@#", num7.ToString());
				stringBuilder.Replace("#@lock_room@#", (num8 == 0) ? "open" : "locked");
				stringBuilder.Replace("#@room_name@#", text3);
				stringBuilder.Replace("#@room_password@#", text4);
				break;
			IL_53a0:
				stringBuilder.Replace("#@instance_type@#", "155Lv~");
				goto IL_53c6;
			IL_538c:
				stringBuilder.Replace("#@instance_type@#", "140~155Lv");
				goto IL_53c6;
			IL_5378:
				stringBuilder.Replace("#@instance_type@#", "125~140Lv");
				goto IL_53c6;
			IL_5364:
				stringBuilder.Replace("#@instance_type@#", "110~125Lv");
				goto IL_53c6;
			IL_5350:
				stringBuilder.Replace("#@instance_type@#", "95~110Lv");
				goto IL_53c6;
			IL_533c:
				stringBuilder.Replace("#@instance_type@#", "80~95Lv");
				goto IL_53c6;
			IL_5325:
				stringBuilder.Replace("#@instance_type@#", "65~80Lv");
				goto IL_53c6;
			IL_530e:
				stringBuilder.Replace("#@instance_type@#", "50~65Lv");
				goto IL_53c6;
			IL_52f7:
				stringBuilder.Replace("#@instance_type@#", "35~50Lv");
				goto IL_53c6;
			IL_53b4:
				stringBuilder.Replace("#@instance_type@#", "unknown");
				goto IL_53c6;
			IL_53c6:
				stringBuilder.Replace("#@room_number@#", num6.ToString());
				stringBuilder.Replace("#@max_user_number@#", num7.ToString());
				stringBuilder.Replace("#@lock_room@#", (num8 == 0) ? "open" : "locked");
				stringBuilder.Replace("#@room_name@#", text3);
				stringBuilder.Replace("#@room_password@#", text4);
				break;
			IL_0963:
				stringBuilder.Replace("#@pcbang@#", "[Double Plus PC CAFE]");
				break;
			IL_51ac:
				stringBuilder.Replace("#@instance_type@#", "155Lv~");
				goto IL_51d2;
			IL_5198:
				stringBuilder.Replace("#@instance_type@#", "140~155Lv");
				goto IL_51d2;
			IL_5184:
				stringBuilder.Replace("#@instance_type@#", "125~140Lv");
				goto IL_51d2;
			IL_5170:
				stringBuilder.Replace("#@instance_type@#", "110~125Lv");
				goto IL_51d2;
			IL_515c:
				stringBuilder.Replace("#@instance_type@#", "95~110Lv");
				goto IL_51d2;
			IL_5148:
				stringBuilder.Replace("#@instance_type@#", "80~95Lv");
				goto IL_51d2;
			IL_5131:
				stringBuilder.Replace("#@instance_type@#", "65~80Lv");
				goto IL_51d2;
			IL_511a:
				stringBuilder.Replace("#@instance_type@#", "50~65Lv");
				goto IL_51d2;
			IL_5103:
				stringBuilder.Replace("#@instance_type@#", "35~50Lv");
				goto IL_51d2;
			IL_51c0:
				stringBuilder.Replace("#@instance_type@#", "unknown");
				goto IL_51d2;
			IL_51d2:
				stringBuilder.Replace("#@room_number@#", num6.ToString());
				stringBuilder.Replace("#@max_user_number@#", num7.ToString());
				stringBuilder.Replace("#@lock_room@#", (num8 == 0) ? "open" : "locked");
				stringBuilder.Replace("#@postion@#", string.Format("Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)", num10, num11, num12));
				stringBuilder.Replace("#@room_name@#", text3);
				stringBuilder.Replace("#@room_password@#", text4);
				break;
			IL_0c65:
				stringBuilder.Replace("#@lv@#", num4.ToString());
				stringBuilder.Replace("#@play_time@#", num12.ToString());
				break;
			IL_097a:
				stringBuilder.Replace("#@pcbang@#", "");
				break;
			IL_4fa4:
				stringBuilder.Replace("#@instance_type@#", "155Lv~");
				goto IL_4fca;
			IL_4f90:
				stringBuilder.Replace("#@instance_type@#", "140~155Lv");
				goto IL_4fca;
			IL_4f7c:
				stringBuilder.Replace("#@instance_type@#", "125~140Lv");
				goto IL_4fca;
			IL_4f68:
				stringBuilder.Replace("#@instance_type@#", "110~125Lv");
				goto IL_4fca;
			IL_4f54:
				stringBuilder.Replace("#@instance_type@#", "95~110Lv");
				goto IL_4fca;
			IL_4f40:
				stringBuilder.Replace("#@instance_type@#", "80~95Lv");
				goto IL_4fca;
			IL_4f29:
				stringBuilder.Replace("#@instance_type@#", "65~80Lv");
				goto IL_4fca;
			IL_4f12:
				stringBuilder.Replace("#@instance_type@#", "50~65Lv");
				goto IL_4fca;
			IL_4efb:
				stringBuilder.Replace("#@instance_type@#", "35~50Lv");
				goto IL_4fca;
			IL_4fb8:
				stringBuilder.Replace("#@instance_type@#", "unknown");
				goto IL_4fca;
			IL_4fca:
				stringBuilder.Replace("#@room_number@#", num6.ToString());
				stringBuilder.Replace("#@max_user_number@#", num7.ToString());
				stringBuilder.Replace("#@lock_room@#", (num8 == 0) ? "open" : "locked");
				stringBuilder.Replace("#@total_user_cnt@#", num9.ToString());
				stringBuilder.Replace("#@postion@#", string.Format("Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)", num10, num11, num12));
				stringBuilder.Replace("#@room_name@#", text3);
				stringBuilder.Replace("#@room_password@#", text4);
				break;
			IL_0c3f:
				stringBuilder.Replace("#@pcbang@#", "[Double Internet Cafe]");
				goto IL_0c65;
			IL_4d9c:
				stringBuilder.Replace("#@instance_type@#", "155Lv~");
				goto IL_4dc2;
			IL_4d88:
				stringBuilder.Replace("#@instance_type@#", "140~155Lv");
				goto IL_4dc2;
			IL_4d74:
				stringBuilder.Replace("#@instance_type@#", "125~140Lv");
				goto IL_4dc2;
			IL_4d60:
				stringBuilder.Replace("#@instance_type@#", "110~125Lv");
				goto IL_4dc2;
			IL_4d4c:
				stringBuilder.Replace("#@instance_type@#", "95~110Lv");
				goto IL_4dc2;
			IL_4d38:
				stringBuilder.Replace("#@instance_type@#", "80~95Lv");
				goto IL_4dc2;
			IL_4d21:
				stringBuilder.Replace("#@instance_type@#", "65~80Lv");
				goto IL_4dc2;
			IL_4d0a:
				stringBuilder.Replace("#@instance_type@#", "50~65Lv");
				goto IL_4dc2;
			IL_4cf3:
				stringBuilder.Replace("#@instance_type@#", "35~50Lv");
				goto IL_4dc2;
			IL_4db0:
				stringBuilder.Replace("#@instance_type@#", "unknown");
				goto IL_4dc2;
			IL_4dc2:
				stringBuilder.Replace("#@room_number@#", num6.ToString());
				stringBuilder.Replace("#@max_user_number@#", num7.ToString());
				stringBuilder.Replace("#@lock_room@#", (num8 == 0) ? "open" : "locked");
				stringBuilder.Replace("#@total_user_cnt@#", num9.ToString());
				stringBuilder.Replace("#@postion@#", string.Format("Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)", num10, num11, num12));
				stringBuilder.Replace("#@room_name@#", text3);
				stringBuilder.Replace("#@room_password@#", text4);
				break;
			IL_4b94:
				stringBuilder.Replace("#@instance_type@#", "155Lv~");
				goto IL_4bba;
			IL_4b80:
				stringBuilder.Replace("#@instance_type@#", "140~155Lv");
				goto IL_4bba;
			IL_4b6c:
				stringBuilder.Replace("#@instance_type@#", "125~140Lv");
				goto IL_4bba;
			IL_4b58:
				stringBuilder.Replace("#@instance_type@#", "110~125Lv");
				goto IL_4bba;
			IL_4b44:
				stringBuilder.Replace("#@instance_type@#", "95~110Lv");
				goto IL_4bba;
			IL_4b30:
				stringBuilder.Replace("#@instance_type@#", "80~95Lv");
				goto IL_4bba;
			IL_4b19:
				stringBuilder.Replace("#@instance_type@#", "65~80Lv");
				goto IL_4bba;
			IL_4b02:
				stringBuilder.Replace("#@instance_type@#", "50~65Lv");
				goto IL_4bba;
			IL_4aeb:
				stringBuilder.Replace("#@instance_type@#", "35~50Lv");
				goto IL_4bba;
			IL_4ba8:
				stringBuilder.Replace("#@instance_type@#", "unknown");
				goto IL_4bba;
			IL_4bba:
				stringBuilder.Replace("#@room_number@#", num6.ToString());
				stringBuilder.Replace("#@max_user_number@#", num7.ToString());
				stringBuilder.Replace("#@lock_room@#", (num8 == 0) ? "open" : "locked");
				stringBuilder.Replace("#@now_created_rooms_cnt@#", num9.ToString());
				stringBuilder.Replace("#@postion@#", string.Format("Coordinates[#@layer@#](#@axis_x@#, #@axis_y@#)", num10, num11, num12));
				stringBuilder.Replace("#@room_name@#", text3);
				stringBuilder.Replace("#@room_password@#", text4);
				break;
			IL_08c3:
				stringBuilder.Replace("#@pcbang@#", "[Double Plus PC CAFE]");
				break;
			IL_4952:
				stringBuilder.Replace("#@party_out_type@#", "(HunterHolic-Force Kick)");
				break;
			IL_493b:
				stringBuilder.Replace("#@party_out_type@#", "(Character Deleted)");
				break;
			IL_4924:
				stringBuilder.Replace("#@party_out_type@#", "(HunterHolic)");
				break;
			IL_0b5f:
				stringBuilder.Replace("#@pcbang@#", "");
				break;
			IL_4969:
				stringBuilder.Replace("#@party_out_type@#", "Error");
				break;
			IL_483b:
				stringBuilder.Replace("#@party_join_type@#", "(HunterHolic)");
				break;
			IL_0c53:
				stringBuilder.Replace("#@pcbang@#", "");
				goto IL_0c65;
			IL_4852:
				stringBuilder.Replace("#@party_join_type@#", "Error");
				break;
			IL_44a1:
				newValue = "Item-Cancel";
				goto IL_44de;
			IL_4498:
				newValue = "Item-Not Sold";
				goto IL_44de;
			IL_448f:
				newValue = "Item-Now buy";
				goto IL_44de;
			IL_4486:
				newValue = "Item-Sold";
				goto IL_44de;
			IL_0b48:
				stringBuilder.Replace("#@pcbang@#", "[Double Internet Cafe]");
				break;
			IL_44ce:
				newValue = "Rupee-Refund by other's now buying";
				goto IL_44de;
			IL_44c5:
				newValue = "Rupee-Refund by Auction cancellation";
				goto IL_44de;
			IL_44bc:
				newValue = "Rupee-Refund by Someone summit higher price";
				goto IL_44de;
			IL_44b3:
				newValue = "Rupee-Refund of registration fee by Sold";
				goto IL_44de;
			IL_44d7:
				newValue = "error";
				goto IL_44de;
			IL_44de:
				stringBuilder.Append("[StoreID: #@store_id@#] [Item : #@item_name@#] , [Number of item: #@number_of_items@#] [Keeping Type : #@keeping_type@#] <br />");
				stringBuilder.Replace("#@store_id@#", GetAuctionInfoLink(server, num4, "k"));
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num5, "a"));
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num7));
				stringBuilder.Replace("#@number_of_items@#", num8.ToString("#,##0"));
				stringBuilder.Replace("#@keeping_type@#", newValue);
				break;
			IL_4332:
				newValue = "Item-Cancel";
				goto IL_436f;
			IL_4329:
				newValue = "Item-Not Sold";
				goto IL_436f;
			IL_4320:
				newValue = "Item-Now buy";
				goto IL_436f;
			IL_4317:
				newValue = "Item-Sold";
				goto IL_436f;
			IL_435f:
				newValue = "Rupee-Refund by other's now buying";
				goto IL_436f;
			IL_4356:
				newValue = "Rupee-Refund by Auction cancellation";
				goto IL_436f;
			IL_434d:
				newValue = "Rupee-Refund by Someone summit higher price";
				goto IL_436f;
			IL_4344:
				newValue = "Rupee-Refund of registration fee by Sold";
				goto IL_436f;
			IL_4368:
				newValue = "error";
				goto IL_436f;
			IL_436f:
				stringBuilder.Append("[StoreID: #@store_id@#] [Item : #@item_name@#] , [Number of item: #@number_of_items@#] [Keeping Type : #@keeping_type@#] [number of items After Takeing : #@result_cnt@#]<br />");
				stringBuilder.Replace("#@store_id@#", GetAuctionInfoLink(server, num4, "k"));
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num5, "a"));
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num7));
				stringBuilder.Replace("#@number_of_items@#", num8.ToString("#,##0"));
				stringBuilder.Replace("#@keeping_type@#", newValue);
				stringBuilder.Replace("#@result_cnt@#", num9.ToString("#,##0"));
				break;
			IL_415d:
				newValue = "Item-Cancel";
				goto IL_419a;
			IL_4154:
				newValue = "Item-Not Sold";
				goto IL_419a;
			IL_414b:
				newValue = "Item-Now buy";
				goto IL_419a;
			IL_4142:
				newValue = "Item-Sold";
				goto IL_419a;
			IL_418a:
				newValue = "Rupee-Refund by other's now buying";
				goto IL_419a;
			IL_4181:
				newValue = "Rupee-Refund by Auction cancellation";
				goto IL_419a;
			IL_4178:
				newValue = "Rupee-Refund by Someone summit higher price";
				goto IL_419a;
			IL_416f:
				newValue = "Rupee-Refund of registration fee by Sold";
				goto IL_419a;
			IL_4193:
				newValue = "error";
				goto IL_419a;
			IL_419a:
				stringBuilder.Append("[StoreID: #@store_id@#] [AuctionID : #@auction_id@#] [Item : #@item_name@#] , [Number of item: #@number_of_items@#] [Keeping Type : #@keeping_type@#] [Keeping Date: #@keeping_date@#]<br />");
				if (text3 != "")
				{
					stringBuilder.Append("<br />[Casue Account : #@cause_account@#] [Casue Character : #@cause_character_name@#] ");
				}
				stringBuilder.Replace("#@store_id@#", GetAuctionInfoLink(server, num4, "k"));
				stringBuilder.Replace("#@auction_id@#", GetAuctionInfoLink(server, num5, "a"));
				stringBuilder.Replace("#@item_name@#", resource.GetItemName(resource_server_id, (int)num7));
				stringBuilder.Replace("#@number_of_items@#", num8.ToString("#,##0"));
				stringBuilder.Replace("#@keeping_type@#", newValue);
				stringBuilder.Replace("#@keeping_date@#", ((DateTime)((DataRowView)e.Item.DataItem)["log_date"]).AddSeconds(num9).ToString("yyyy-MM-dd HH:mm:ss"));
				stringBuilder.Replace("#@cause_account@#", text3);
				stringBuilder.Replace("#@cause_character_name@#", text4);
				break;
		}
		literal5.Text = stringBuilder.ToString();
	}

	private string GetBool(long i)
	{
		if (i == 0)
		{
			return "False";
		}
		return "True";
	}

	private string GetPartyType(long code)
	{
		string text = "";
		long num = code;
		if (num <= 3 && num >= 0)
		{
			switch (num)
			{
				case 0L:
					return "normal party";
				case 1L:
					return "siege party";
				case 2L:
					return "siege sub party";
				case 3L:
					return "siege mercenary party";
			}
		}
		return code.ToString();
	}

	private string GetPartyDestroyType(long code)
	{
		string text = "";
		long num = code;
		if (num <= 9 && num >= 0)
		{
			switch (num)
			{
				case 0L:
					return "party leader";
				case 1L:
					return "[E5P2]hunter holic  - clear";
				case 2L:
					return "[E5P2]hunter holic - timer over";
				case 3L:
					return "party leader's character deleted";
				case 4L:
					return "raid cancled";
				case 5L:
					return "[E5P2]hunter holic ";
				case 6L:
					return "dungeon - lv limite ";
				case 7L:
					return "raid - period end";
				case 8L:
					return "siege - end";
				case 9L:
					return "raid timer attack - end  ";
			}
		}
		return code.ToString();
	}

	private string GetItemOptionType(long code)
	{
		string result = "";
		if (code <= 32768)
		{
			if (code <= 256)
			{
				if (code <= 16)
				{
					if (code <= 4)
					{
						if (code < 1)
						{
							goto IL_0317;
						}
						switch (code - 1)
						{
							case 0L:
								result = "STR";
								goto IL_0317;
							case 1L:
								result = "Stamina";
								goto IL_0317;
							case 3L:
								result = "DEX";
								goto IL_0317;
							case 2L:
								goto IL_0317;
						}
					}
					switch (code)
					{
						case 8L:
							result = "CON";
							break;
						case 16L:
							result = "INT";
							break;
					}
				}
				else
				{
					switch (code)
					{
						case 32L:
							result = "MEN";
							break;
						case 64L:
							result = "LUK";
							break;
						case 128L:
							result = "Physical Damage";
							break;
						case 256L:
							result = "Magic Damage";
							break;
					}
				}
			}
			else
			{
				switch (code)
				{
					case 512L:
						result = "Pysical Defence";
						break;
					case 1024L:
						result = "Magic Defence";
						break;
					case 2048L:
						result = "Attack Speed";
						break;
					case 4096L:
						result = "Casting Speed";
						break;
					case 8192L:
						result = "Movement Speed";
						break;
					case 16384L:
						result = "Hit";
						break;
					case 32768L:
						result = "Magic Hit";
						break;
				}
			}
		}
		else
		{
			switch (code)
			{
				case 65536L:
					result = "critical rate";
					break;
				case 131072L:
					result = "block rate";
					break;
				case 262144L:
					result = "block defence";
					break;
				case 524288L:
					result = "evasion";
					break;
				case 1048576L:
					result = "Magic regist";
					break;
				case 2097152L:
					result = "MAX HP";
					break;
				case 4194304L:
					result = "MAX MP";
					break;
				case 8388608L:
					result = "MAX SP";
					break;
				case 16777216L:
					result = "HP Regen";
					break;
				case 33554432L:
					result = "MP Regen";
					break;
				case 67108864L:
					result = "SP Regen";
					break;
				case 134217728L:
					result = "HP Regen rate";
					break;
				case 268435456L:
					result = "MP Regen rate";
					break;
				case 1073741824L:
					result = "Carry Weight";
					break;
			}
		}
		goto IL_0317;
	IL_0317:
		return result;
	}

	protected string GetAuctionInfoLink(string server, long id, string type)
	{
		return string.Format("<a href=\"javascript:;\" onclick=\"window.open('{0}?server={1}&file={2}&id={3}&type={4}','auction_info_{5}','left=50,top=50,width=1140,height=500,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{6}</a>", "bigint_auction_info.aspx", server, ddlLogFile.SelectedValue, id, type, id, id);
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		GetLogData();
		string text = string.Format("Rappelz_GameServerLog_{0}_{1}.xls", ddlServer.SelectedValue, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
		base.Response.Clear();
		base.Response.AddHeader("content-disposition", "attachment;filename=" + text);
		base.Response.ContentType = "application/vnd.xls";
		base.Response.ContentEncoding = Encoding.UTF8;
		StringWriter stringWriter = new StringWriter();
		HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
		rptGameServer.RenderControl(writer);
		byte[] buffer = new byte[3]
		{
			239,
			187,
			191
		};
		base.Response.BinaryWrite(buffer);
		base.Response.Write(stringWriter.ToString());
		base.Response.End();
	}
}
