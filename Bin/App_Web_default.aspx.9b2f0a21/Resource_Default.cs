using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;

public class Resource_Default : Page, IRequiresSessionState
{
	protected Literal ltrServerList;

	protected Button btnServerList;

	protected Literal ltrResource0;

	protected Button btnUpdate0;

	protected Literal ltrResource1;

	protected Button btnUpdate1;

	protected Literal ltrResource2;

	protected Button btnUpdate2;

	protected Literal ltrResource3;

	protected Button btnUpdate3;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.IsAuth())
		{
			base.Response.Redirect("../default.aspx", true);
		}
		if (!admin.Authority())
		{
			common.MsgBox("没有权限.", "back");
		}
		ltrServerList.Text = server.updateTime.ToString();
		ltrResource0.Text = resource.updateTime[0].ToString();
		ltrResource1.Text = resource.updateTime[1].ToString();
		ltrResource2.Text = resource.updateTime[2].ToString();
		ltrResource3.Text = resource.updateTime[3].ToString();
	}

	protected void btnServerList_Click(object sender, EventArgs e)
	{
		server.UpdateServerInfo();
		ltrServerList.Text = server.updateTime.ToString();
	}

	protected void btnUpdate0_Click(object sender, EventArgs e)
	{
		resource.UpdateGameResource(0);
		ltrResource0.Text = resource.updateTime[0].ToString();
	}

	protected void btnUpdate1_Click(object sender, EventArgs e)
	{
		resource.UpdateGameResource(1);
		ltrResource1.Text = resource.updateTime[1].ToString();
	}

	protected void btnUpdate2_Click(object sender, EventArgs e)
	{
		resource.UpdateGameResource(2);
		ltrResource2.Text = resource.updateTime[2].ToString();
	}

	protected void btnUpdate3_Click(object sender, EventArgs e)
	{
		resource.UpdateGameResource(3);
		ltrResource3.Text = resource.updateTime[3].ToString();
	}
}
