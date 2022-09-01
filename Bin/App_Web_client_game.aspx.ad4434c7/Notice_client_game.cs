using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Notice_client_game : Page, IRequiresSessionState
{
	protected Repeater rptClientGame;

	protected DropDownList ddlHour;

	protected DropDownList ddlMin;

	protected DropDownList ddlSec;

	protected CheckBox cbRepeat;

	protected HtmlTextArea TextBox1;

	protected Button Button1;

	protected HtmlGenericControl ifrMsg;

	protected Literal ltrScript;

	protected HtmlInputHidden hdClientGame;

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
			server.SetServer(ref rptClientGame);
			int num = 0;
			for (num = 0; num < 25; num++)
			{
				ddlHour.Items.Add(new ListItem(num.ToString()));
			}
			for (num = 0; num < 61; num++)
			{
				ddlMin.Items.Add(new ListItem(num.ToString()));
			}
			for (num = 0; num < 61; num++)
			{
				ddlSec.Items.Add(new ListItem(num.ToString()));
			}
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		string value = TextBox1.Value;
		ltrScript.Text = "";
		value = value.Replace("\n", "{n}");
		int count = rptClientGame.Items.Count;
		string value2 = hdClientGame.Value;
		for (int i = 0; i < count; i++)
		{
		}
		ifrMsg.Attributes.Add("src", $"send_msg.aspx?server={value2}&msg={value}");
		if (cbRepeat.Checked)
		{
			int num = int.Parse(ddlHour.SelectedValue) * 60 * 60 + int.Parse(ddlMin.SelectedValue) * 60 + int.Parse(ddlSec.SelectedValue);
			if (num > 0)
			{
				ltrScript.Text = $"\r\n\t\t\t\t\t\t<script>\r\n\t\t\t\t\t\ttime = {num * 1000};\r\n\t\t\t\t\t\tAutoStart();\r\n\t\t\t\t\t\t</script>";
			}
		}
	}
}
