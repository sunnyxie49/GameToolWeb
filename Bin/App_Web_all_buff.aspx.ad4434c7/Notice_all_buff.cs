using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Notice_all_buff : Page, IRequiresSessionState
{
	protected Repeater rptAllBuff;

	protected TextBox txtAdd;

	protected TextBox txtStateLv;

	protected Button Button1;

	protected TextBox txtRemove;

	protected Button Button2;

	protected Button Button3;

	protected Literal ltrList;

	protected HtmlInputHidden hdAllBuff;

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
			common.MsgBox("没有权限", "back");
		}
		if (!base.IsPostBack)
		{
			server.SetServer(ref rptAllBuff);
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		string[] array = hdAllBuff.Value.Split(',');
		for (int i = 0; i < array.Length - 1; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				AddEventState(array[i], Convert.ToInt32(txtAdd.Text), Convert.ToInt32(txtStateLv.Text));
			}
		}
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		string[] array = hdAllBuff.Value.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				RemoveEventState(array[i], Convert.ToInt32(txtRemove.Text));
			}
		}
	}

	protected void Button3_Click(object sender, EventArgs e)
	{
		ltrList.Text = "";
		string[] array = hdAllBuff.Value.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			if (!string.IsNullOrEmpty(array[i]))
			{
				GetStateList(array[i]);
			}
		}
	}

	protected void AddEventState(string server_name, int state_id, int state_lv)
	{
		server.GameServerInfo(server_name, out var address, out var port, out var password);
		string command = $"#return add_event_state({state_id},{state_lv})";
		libACWC.Execute(address, port, password, command, out var _);
		acm.WriteCheatLog("ADD_EVENT_STATE", server_name, 0, "", 0, "", 0L, 0, 0, 0, 0, 0, "STATE ID:" + state_id + "-LV:" + state_lv);
	}

	protected void RemoveEventState(string server_name, int state_id)
	{
		server.GameServerInfo(server_name, out var address, out var port, out var password);
		string command = $"#return remove_event_state({state_id})";
		libACWC.Execute(address, port, password, command, out var _);
		acm.WriteCheatLog("REMOVE_EVENT_STATE", server_name, 0, "", 0, "", 0L, 0, 0, 0, 0, 0, "STATE ID:" + state_id);
	}

	protected void GetStateList(string server_name)
	{
		server.GameServerInfo(server_name, out var address, out var port, out var password);
		string command = "#return get_event_state_list()";
		object o_result;
		string text = libACWC.ExecuteReturn(address, port, password, command, out o_result);
		string[] array = text.Split('\n');
		ltrList.Text = "<table class='table table-bordered centerTable smallTable'>";
		ltrList.Text += "<colgroup><col width='35%' /><col width='35%' /><col width='30%' /></colgroup><thead><tr><th>服务器名</th><th>状态 ID</th><th>状态等级</th></tr></thead><tbody>";
		for (int i = 1; i < array.Length - 1; i++)
		{
			Literal literal = ltrList;
			literal.Text = literal.Text + "<tr><td>" + server.GetServerName(server_name) + "</td>";
			ltrList.Text += $"<td>{resource.GetStateName(server.GetResourceServerNo(server_name), Convert.ToInt32(array[i].Split(' ')[0]))}({array[i].Split(' ')[0]})</td>";
			Literal literal2 = ltrList;
			literal2.Text = literal2.Text + "<td>" + array[i].Split(' ')[1] + "</td></tr>";
		}
		ltrList.Text += "</tbody></table>";
	}
}
