using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;
using Rappelz.GMToolV3.BizDac;

public class Admin_Default : Page, IRequiresSessionState
{
	protected Repeater rptAdmin;

	protected Literal ltrPage;

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
			GetAdminList();
		}
	}

	private void GetAdminList()
	{
		DataSet dataSet = null;
		int page = common.ToInt(base.Request.QueryString["page"]);
		int totalCnt = 0;
		using (AdminBiz adminBiz = new AdminBiz())
		{
			totalCnt = adminBiz.CountAdminAccountInfo();
		}
		ltrPage.Text = paging.PageList(base.Request.Path, "", totalCnt, ref page, 30, 10);
		using (AdminBiz adminBiz2 = new AdminBiz())
		{
			dataSet = adminBiz2.GetAdminAccountInfoByPaging(30, 30 * (page - 1));
		}
		rptAdmin.DataSource = dataSet;
		rptAdmin.DataBind();
		if (dataSet != null)
		{
			dataSet = null;
		}
	}

	protected string BindAccount(object item)
	{
		int num = (int)DataBinder.Eval(item, "sid");
		string arg = (string)DataBinder.Eval(item, "account");
		return $"<a href='#' onclick=\"window.open('admin_modify.aspx?sid={num}', 'admin_modify','width=400,height=320,toolbar=no,menubar=no,status=yes,scrollbars=yes,resizable=no')\">{arg}</a>";
	}
}
