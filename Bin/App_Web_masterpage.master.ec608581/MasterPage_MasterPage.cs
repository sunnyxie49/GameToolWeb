using System;
using System.Configuration;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class MasterPage_MasterPage : MasterPage
{
	protected Label lbGame;

	protected HyperLink hlGame1;

	protected HyperLink hlGame2;

	protected HyperLink hlGame3;

	protected HyperLink hlGame4;

	protected Label lbLog;

	protected HyperLink hlLog4;

	protected HyperLink hlLog5;

	protected HyperLink hlLog2;

	protected HyperLink hlLog3;

	protected Label lbStatistics;

	protected HyperLink hlStatistics1;

	protected HyperLink hlStatistics2;

	protected HyperLink hlStatistics3;

	protected HyperLink hlStatistics4;

	protected HyperLink hlStatistics5;

	protected HyperLink hlStatistics6;

	protected Label lbNotice;

	protected HyperLink hlResource;

	protected Label lbAdmin;

	protected HyperLink hlAdmin1;

	protected HyperLink hlAdmin2;

	protected HyperLink hlAdmin3;

	protected Label lbLogin;

	protected HtmlGenericControl spanLogout;

	protected HyperLink hlLogout;

	protected ContentPlaceHolder CPMainPage;

	protected HtmlForm form1;

	protected string sAccount = string.Empty;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		hlResource.NavigateUrl = "/Resource/Default.aspx";
		if (ConfigurationManager.AppSettings["Region"] == "China")
		{
			hlLog5.Visible = true;
		}
		sAccount = admin.GetAccount();
	}
}
