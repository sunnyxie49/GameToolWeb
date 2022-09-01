using System;
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

public class Game_summon_info : Page, IRequiresSessionState
{
	protected Literal Literal1;

	protected HtmlForm form1;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		if (base.Request.QueryString["server"] == null || base.Request.QueryString["server"] == "")
		{
			common.MsgBox("You need the server value");
		}
		else if (base.Request.QueryString["item_id"] == null || base.Request.QueryString["item_id"] == "")
		{
			common.MsgBox("You need Item sid value");
		}
		long result = 0L;
		if (long.TryParse(base.Request.QueryString["item_id"], out result))
		{
			GetData(base.Request.QueryString["server"], result);
		}
	}

	private void GetData(string server, long item_id)
	{
		int resourceServerNo = libs.server.GetResourceServerNo(server);
		SqlConnection sqlConnection = new SqlConnection(libs.server.GetConnectionString(server));
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = "gmtool_GetSummon_info";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlCommand.Parameters.Clear();
		sqlCommand.Parameters.Add("@item_id", SqlDbType.BigInt).Value = item_id;
		sqlConnection.Open();
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		StringBuilder stringBuilder = new StringBuilder(1000);
		stringBuilder.Append("<h4>Owner Info</h4>");
		stringBuilder.Append("<table class='table table-bordered table-condensed'>");
		stringBuilder.Append("<colgroup><col width='30%' /><col width='70%' /></colgroup>");
		stringBuilder.Append("<tbody>");
		if (sqlDataReader.Read())
		{
			stringBuilder.AppendFormat("<tr><th>Account</th><td>{0} ({1})</td></tr>", sqlDataReader["account"], sqlDataReader["account_id"]);
			string job_name = "";
			string job_icon = "";
			resource.GetJobResource(resourceServerNo, (int)sqlDataReader["job"], out job_name, out job_icon);
			bool flag = false;
			if (sqlDataReader["immoral_point"].GetType() == typeof(float))
			{
				float num = (float)sqlDataReader["immoral_point"];
				if (num >= 100f)
				{
					flag = true;
				}
			}
			else
			{
				decimal num2 = (decimal)sqlDataReader["immoral_point"];
				if (num2 >= 100m)
				{
					flag = true;
				}
			}
			if (job_icon != "")
			{
				if (flag)
				{
					stringBuilder.AppendFormat("<tr><th>Character</th><td><img src='../img/icon/{1}' align='absmiddle' title='{2}'> <font color='red'>{0}</font> ({3})</td></tr>", sqlDataReader["name"], job_icon, job_name, sqlDataReader["sid"]);
				}
				else
				{
					stringBuilder.AppendFormat("<tr><th>Character</th><td><img src='../img/icon/{1}' align='absmiddle' title='{2}'> {0} ({3})</td></tr>", sqlDataReader["name"], job_icon, job_name, sqlDataReader["sid"]);
				}
			}
			else
			{
				stringBuilder.AppendFormat("<tr><th>Character</th><td>{0} ({5})</td></tr>", sqlDataReader["name"], sqlDataReader["lv"]);
			}
			stringBuilder.AppendFormat("<tr><th>Lv</th><td> {0} </td></tr>", sqlDataReader["lv"]);
			stringBuilder.AppendFormat("<tr><th>JLv</th><td> {0} </td></tr>", sqlDataReader["jlv"]);
		}
		else
		{
			stringBuilder.Append("* Item SID Error.");
		}
		stringBuilder.Append("</tbody></table>");
		sqlDataReader.NextResult();
		stringBuilder.Append("<h4>Item Info</h4>");
		stringBuilder.Append("<table class='table table-bordered table-condensed'>");
		stringBuilder.Append("<colgroup><col width='30%' /><col width='70%' /></colgroup>");
		stringBuilder.Append("<tbody>");
		if (sqlDataReader.Read())
		{
			stringBuilder.AppendFormat("<tr><th>character_id</th><td>{0}</td></tr>", sqlDataReader["character_id"]);
			stringBuilder.AppendFormat("<tr><th>account_id</th><td>{0}</td></tr>", sqlDataReader["account_id"]);
			stringBuilder.AppendFormat("<tr><th>item_code</th><td>{0}</td></tr>", resource.GetItemName(resourceServerNo, common.ToInt(sqlDataReader["item_code"].ToString(), 0)));
			stringBuilder.AppendFormat("<tr><th>flag</th><td></td>{0}</tr>", resource.GetItemFlagValue(common.ToInt(sqlDataReader["flag"].ToString(), 0)));
			stringBuilder.AppendFormat("<tr><th>summon_id</th><td>{0}</td></tr>", sqlDataReader["summon_id"]);
		}
		stringBuilder.Append("</tbody></table>");
		sqlDataReader.NextResult();
		stringBuilder.Append("<h4>Summon Info</h4>");
		stringBuilder.Append("<table class='table table-bordered table-condensed'>");
		stringBuilder.Append("<colgroup><col width='30%' /><col width='70%' /></colgroup>");
		stringBuilder.Append("<tbody>");
		if (sqlDataReader.Read())
		{
			stringBuilder.AppendFormat("<tr><th>sid</th><td>{0}</td></tr>", sqlDataReader["sid"]);
			stringBuilder.AppendFormat("<tr><th>account_id</th><td>{0}</td></tr>", sqlDataReader["account_id"]);
			stringBuilder.AppendFormat("<tr><th>owner_id</th><td>{0}</td></tr>", sqlDataReader["owner_id"]);
			stringBuilder.AppendFormat("<tr><th>code</th><td>{0}</td></tr>", resource.GetSummonName(resourceServerNo, common.ToInt(sqlDataReader["code"].ToString(), 0)));
			stringBuilder.AppendFormat("<tr><th>card_uid</th><td>{0}</td></tr>", sqlDataReader["card_uid"]);
			stringBuilder.AppendFormat("<tr><th>exp</th><td>{0}</td></tr>", sqlDataReader["exp"]);
			stringBuilder.AppendFormat("<tr><th>jp</th><td>{0}</td></tr>", sqlDataReader["jp"]);
			stringBuilder.AppendFormat("<tr><th>last_decreased_exp</th><td>{0}</td></tr>", sqlDataReader["last_decreased_exp"]);
			stringBuilder.AppendFormat("<tr><th>name</th><td>{0}</td></tr>", sqlDataReader["name"]);
			stringBuilder.AppendFormat("<tr><th>transform</th><td>{0}</td></tr>", sqlDataReader["transform"]);
			stringBuilder.AppendFormat("<tr><th>lv</th><td>{0}</td></tr>", sqlDataReader["lv"]);
			stringBuilder.AppendFormat("<tr><th>jlv</th><td>{0}</td></tr>", sqlDataReader["jlv"]);
			stringBuilder.AppendFormat("<tr><th>max_level</th><td>{0}</td></tr>", sqlDataReader["max_level"]);
			stringBuilder.AppendFormat("<tr><th>fp</th><td>{0}</td></tr>", sqlDataReader["fp"]);
			stringBuilder.AppendFormat("<tr><th>prev_level_01</th><td>{0}</td></tr>", sqlDataReader["prev_level_01"]);
			stringBuilder.AppendFormat("<tr><th>prev_level_02</th><td>{0}</td></tr>", sqlDataReader["prev_level_02"]);
			stringBuilder.AppendFormat("<tr><th>prev_id_01</th><td>{0}</td></tr>", resource.GetSummonName(resourceServerNo, (int)sqlDataReader["prev_id_01"]));
			stringBuilder.AppendFormat("<tr><th>prev_id_02</th><td>{0}</td></tr>", resource.GetSummonName(resourceServerNo, (int)sqlDataReader["prev_id_02"]));
			stringBuilder.AppendFormat("<tr><th>sp</th><td>{0}</td></tr>", sqlDataReader["sp"]);
			stringBuilder.AppendFormat("<tr><th>hp</th><td>{0}</td></tr>", sqlDataReader["hp"]);
			stringBuilder.AppendFormat("<tr><th>mp</th><td>{0}</td></tr>", sqlDataReader["mp"]);
		}
		stringBuilder.Append("</tbody></table>");
		sqlDataReader.NextResult();
		stringBuilder.Append("<h4>Summon Skill Info</h4>");
		stringBuilder.Append("<table class='table table-bordered table-condensed'>");
		stringBuilder.Append("<colgroup><col width='30%' /><col width='70%' /></colgroup>");
		stringBuilder.Append("<tbody>");
		while (sqlDataReader.Read())
		{
			DataRow skillResource = resource.GetSkillResource(resourceServerNo, (int)sqlDataReader["skill_id"]);
			string text = ((!File.Exists(base.Server.MapPath(string.Format("../img/icon/{0}.jpg", skillResource["icon_file_name"])))) ? "&nbsp;" : string.Format("<img src='../img/icon/{0}.jpg'>", skillResource["icon_file_name"]));
			resource.GetStringResource(resourceServerNo, (int)skillResource["text_id"]);
			stringBuilder.AppendFormat("<tr><th></th><td>{0} {1} Lv {2} {3}</td></tr>", text, resource.GetStringResource(resourceServerNo, (int)skillResource["text_id"]), sqlDataReader["skill_level"], (skillResource["is_passive"].ToString() == "0") ? "Passive" : "<font color='blue'>Active</font>");
		}
		stringBuilder.Append("</tbody></table>");
		Literal1.Text = stringBuilder.ToString();
		sqlConnection.Close();
		sqlCommand.Dispose();
		sqlConnection.Dispose();
	}
}
