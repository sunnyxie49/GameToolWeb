using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Main : Page, IRequiresSessionState
{
	protected Label lbMsg;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request.QueryString["mode"] == "nopermission")
		{
			lbMsg.Text = "没有权限";
		}
	}
}
