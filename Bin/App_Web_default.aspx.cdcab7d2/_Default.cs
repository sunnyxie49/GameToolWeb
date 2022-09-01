using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class _Default : Page, IRequiresSessionState
    {
		protected HtmlInputText txtAccount;

		protected HtmlInputPassword txtPassword;

		protected HtmlInputCheckBox chkRemember;

		protected Button btnLogin;

		protected HtmlForm form1;

		protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

		protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (admin.IsAuth())
			{
				base.Response.Redirect("/main.aspx");
			}
		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			string text = txtAccount.Value.Trim();
			string text2 = txtPassword.Value.Trim();
			if (string.IsNullOrEmpty(text))
			{
				common.MsgBox(this, "IDCheck", "ID 检查");
				return;
			}
			if (string.IsNullOrEmpty(text2))
			{
				common.MsgBox(this, "PASSWORDCheck", "密码 检查");
				return;
			}
			string msg = "";
			if (!admin.Login(text, text2, out msg))
			{
				common.MsgBox(this, "LoginCheck", msg);
			}
			else
			{
				base.Response.Redirect("/main.aspx");
			}
		}
	}

