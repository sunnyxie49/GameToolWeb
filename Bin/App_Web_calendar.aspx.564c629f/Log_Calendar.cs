using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;

public class Log_Calendar : Page, IRequiresSessionState
{
	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
	}
}
