using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using libs;

public class Notice_send_msg : Page, IRequiresSessionState
{
	private string server;

	private string msg;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", endResponse: true);
		}
		server = base.Request.QueryString["server"];
		msg = base.Request.QueryString["msg"].Replace("{n}", "\n");
		string[] array = server.Split('_');
		for (int i = 0; i < array.Length - 1; i++)
		{
			acm.Notice(array[i], msg);
		}
	}
}
