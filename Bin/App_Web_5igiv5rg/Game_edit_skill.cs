using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_edit_skill : Page, IRequiresSessionState
{
	protected Literal ltrServer;

	protected Literal ltrOwner;

	protected Literal ltrSkillSid;

	protected Image imgSkillIcon;

	protected Literal ltrSkillName;

	protected Literal ltrSkillLevel;

	protected Literal ltrSkillType;

	protected Literal ltrSkillInfo;

	protected TextBox txtLevel;

	protected Button btnChange;

	protected Button btnDel;

	protected HtmlForm form1;

	private SqlConnection con;

	private SqlCommand cmd;

	private string server;

	private int sid;

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
			common.MsgBox(this, "CEditSkill", "You need the server value");
		}
		else if (base.Request.QueryString["sid"] == null || base.Request.QueryString["sid"] == "")
		{
			common.MsgBox(this, "CEditSkill", "You need skill sid value");
		}
		else
		{
			server = base.Request.QueryString["server"];
			sid = common.ToInt(base.Request.QueryString["sid"]);
			ltrServer.Text = server;
			ltrSkillSid.Text = sid.ToString();
			con = new SqlConnection(libs.server.GetConnectionString(server));
			cmd = new SqlCommand();
			cmd.Connection = con;
			if (!base.IsPostBack)
			{
				GetSkillInfo();
			}
		}
		if (admin.Authority(server, "gmtool_edit_skill"))
		{
			txtLevel.Enabled = true;
			btnChange.Enabled = true;
			btnDel.Enabled = true;
		}
	}

	private void GetSkillInfo()
	{
		con.Open();
		cmd.CommandText = "gmtool_v2_get_skill_info";
		cmd.CommandType = CommandType.StoredProcedure;
		cmd.Parameters.Clear();
		cmd.Parameters.Add("@sid", SqlDbType.Int).Value = sid;
		SqlDataReader sqlDataReader = cmd.ExecuteReader();
		if (sqlDataReader.Read())
		{
			int num = (int)sqlDataReader["character_id"];
			string arg = (string)sqlDataReader["character_name"];
			int num2 = (int)sqlDataReader["summon_id"];
			int id = (int)sqlDataReader["skill_id"];
			int num3 = (int)sqlDataReader["skill_level"];
			_ = (int)sqlDataReader["cool_time"];
			if (num == 0 && num2 == 0)
			{
				common.MsgBox(this, "CEditSkill", $"There is no skill which sid value is {sid}");
			}
			else
			{
				txtLevel.Text = num3.ToString();
				if (num != 0)
				{
					ltrOwner.Text = $"{arg}({num})";
				}
				if (num2 != 0)
				{
					ltrOwner.Text += $"summon_id  : {num2}  Creature";
				}
				if (num == 0 && num2 == 0)
				{
					ltrOwner.Text = $"No Owner";
				}
				int resource_server_no = (int)libs.server.GetServerInfo(server)["resource_id"];
				DataRow skillResource = resource.GetSkillResource(resource_server_no, id);
				ltrSkillSid.Text = sid.ToString();
				ltrSkillName.Text = skillResource["value"].ToString();
				ltrSkillLevel.Text = num3.ToString();
				if (File.Exists(base.Server.MapPath(string.Format("../img/icon/{0}.jpg", skillResource["icon_file_name"]))))
				{
					imgSkillIcon.ImageUrl = string.Format("../img/icon/{0}.jpg", skillResource["icon_file_name"]);
				}
				else
				{
					imgSkillIcon.Visible = false;
				}
				ltrSkillType.Text = ((skillResource["is_passive"].ToString() == "0") ? "Passive" : "<font color='blue'>Active</font>");
				string stringResource = resource.GetStringResource(resource_server_no, (int)skillResource["tooltip_id"]);
				if (stringResource.Length > 0)
				{
					try
					{
						string text = stringResource.Substring(stringResource.IndexOf("<#ffffcc>") + 9);
						ltrSkillInfo.Text = text.Substring(0, text.IndexOf("<#ffffff>"));
					}
					catch (Exception)
					{
					}
				}
				else
				{
					ltrSkillInfo.Text = string.Format("(tooltip_id : {0})", skillResource["tooltip_id"]);
				}
			}
		}
		else
		{
			common.MsgBox(this, "CEditSkill", string.Format("There is no skill which sid value is A", sid));
		}
		con.Close();
	}

	protected void btnChange_Click(object sender, EventArgs e)
	{
		try
		{
			int level = int.Parse(txtLevel.Text);
			if (acm.EditSkill(server, sid, level, 0, out var result_msg))
			{
				common.MsgBox(this, "CEditSkill", "Change the skill info : Success\n\n" + result_msg);
			}
			else
			{
				common.MsgBox(this, "CEditSkill", "Change the skill info : Failure \n\n" + result_msg);
			}
		}
		catch (Exception ex)
		{
			common.MsgBox(this, "CEditSkill", ex.Message);
		}
		GetSkillInfo();
	}

	protected void btnDel_Click(object sender, EventArgs e)
	{
		try
		{
			if (acm.DeleteSkill(server, sid, out var result_msg))
			{
				common.MsgBox(this, "CEditSkill", "Delete the skill : Success \n\n" + result_msg);
			}
			else
			{
				common.MsgBox(this, "CEditSkill", "Delete the skill : Failure \n\n" + result_msg);
			}
		}
		catch (Exception ex)
		{
			common.MsgBox(this, "CEditSkill", ex.Message);
		}
		GetSkillInfo();
	}
}
