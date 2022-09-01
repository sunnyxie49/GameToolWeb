using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;
using Rappelz.GMToolV3.BizDac;

public class Admin_admin_add : Page, IRequiresSessionState
{
	protected TextBox txtAccount;

	protected RequiredFieldValidator RequiredFieldValidator1;

	protected TextBox txtPassword;

	protected RequiredFieldValidator RequiredFieldValidator2;

	protected TextBox txtName;

	protected RequiredFieldValidator RequiredFieldValidator3;

	protected DropDownList ddlTeam;

	protected DropDownList ddlGrade;

	protected Button Button1;

	protected HtmlForm form1;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!admin.Authority())
		{
			common.MsgBox("没有权限", "关闭");
		}
		if (base.IsPostBack)
		{
			return;
		}
		DataSet dataSet = null;
		DataRow dataRow = null;
		using (AdminBiz adminBiz = new AdminBiz())
		{
			dataSet = adminBiz.GetAdminTeamInfo();
		}
		if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
		{
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				dataRow = dataSet.Tables[0].Rows[i];
				int num = (int)dataRow["sid"];
				string text = (string)dataRow["name"];
				ListItem item = new ListItem(text, num.ToString());
				ddlTeam.Items.Add(item);
			}
		}
		using (AdminBiz adminBiz2 = new AdminBiz())
		{
			dataSet = adminBiz2.GetAdminGradeInfo();
		}
		if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
		{
			for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
			{
				dataRow = dataSet.Tables[0].Rows[j];
				int num2 = (int)dataRow["sid"];
				string text2 = (string)dataRow["name"];
				ListItem item2 = new ListItem(text2, num2.ToString());
				ddlGrade.Items.Add(item2);
			}
		}
		if (dataRow != null)
		{
			dataRow = null;
		}
		if (dataSet != null)
		{
			dataSet = null;
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			text = ((!(ConfigurationManager.AppSettings["password_encode"] == "True")) ? txtPassword.Text : FormsAuthentication.HashPasswordForStoringInConfigFile($"bmw|{txtPassword.Text}|z4", "MD5").Substring(0, 20));
			using (AdminBiz adminBiz = new AdminBiz())
			{
				adminBiz.AddAdminAccountInfo(txtAccount.Text, text, txtName.Text, ddlTeam.SelectedValue, ddlGrade.SelectedValue);
			}
			txtAccount.Text = "";
			txtPassword.Text = "";
			txtName.Text = "";
			common.MsgBox("已插入管理员帐户");
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
		}
	}
}
