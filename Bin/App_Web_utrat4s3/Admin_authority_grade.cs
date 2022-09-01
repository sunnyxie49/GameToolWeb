using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;
using Rappelz.GMToolV3.BizDac;

public class Admin_authority_grade : Page, IRequiresSessionState
{
	protected DropDownList ddlGrade;

	protected Button btnGetAuthority;

	protected Button Button1;

	protected Button Button2;

	protected Button Button3;

	protected Repeater rptAuthGrade;

	private bool update;

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
			common.MsgBox("没有权限", "返回");
		}
		if (base.IsPostBack)
		{
			return;
		}
		DataSet dataSet = null;
		DataRow dataRow = null;
		using (AdminBiz adminBiz = new AdminBiz())
		{
			dataSet = adminBiz.GetAdminGradeInfo();
		}
		if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
		{
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				dataRow = dataSet.Tables[0].Rows[i];
				int num = (int)dataRow["sid"];
				string text = (string)dataRow["name"];
				ListItem item = new ListItem(text, num.ToString());
				ddlGrade.Items.Add(item);
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

	private void GetAdminAuthority()
	{
		int admin_grade_id = int.Parse(ddlGrade.SelectedValue);
		DataSet dataSet = null;
		using (AdminBiz adminBiz = new AdminBiz())
		{
			dataSet = adminBiz.GetAdminAuthorityTotalListV3(0, admin_grade_id);
		}
		rptAuthGrade.DataSource = dataSet;
		rptAuthGrade.ItemDataBound += rptAuthGrade_ItemDataBound;
		rptAuthGrade.DataBind();
		if (dataSet != null)
		{
			dataSet = null;
		}
	}

	private void rptAuthGrade_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
		{
			return;
		}
		Literal literal = (Literal)e.Item.FindControl("ltrCode");
		Literal literal2 = (Literal)e.Item.FindControl("ltrCategory");
		Literal literal3 = (Literal)e.Item.FindControl("ltrInfo");
		Literal literal4 = (Literal)e.Item.FindControl("ltrAuthority");
		literal.Text = ((DataRowView)e.Item.DataItem)["Code"].ToString();
		literal2.Text = ((DataRowView)e.Item.DataItem)["Category"].ToString();
		literal3.Text = ((DataRowView)e.Item.DataItem)["Info"].ToString();
		_ = (int)((DataRowView)e.Item.DataItem)["authority_id"];
		int num = (int)((DataRowView)e.Item.DataItem)["grade_authority_id"];
		string arg = ((DataRowView)e.Item.DataItem)["sid"].ToString();
		if (update)
		{
			if (base.Request.Form[$"chkGrade{arg}"] != null)
			{
				if (num == -1)
				{
					UpdateAuthority(num, (int)((DataRowView)e.Item.DataItem)["sid"], -1, int.Parse(ddlGrade.SelectedValue), 1);
				}
				literal4.Text = string.Format("<input type='checkbox' name='chkGrade{0}' value='{0}' checked>", arg);
			}
			else
			{
				if (num != -1)
				{
					UpdateAuthority(num, (int)((DataRowView)e.Item.DataItem)["sid"], -1, int.Parse(ddlGrade.SelectedValue), 0);
				}
				literal4.Text = string.Format("<input type='checkbox' name='chkGrade{0}' value='{0}'>", arg);
			}
		}
		else if ((int)((DataRowView)e.Item.DataItem)["grade_authority_type"] > 0)
		{
			literal4.Text = string.Format("<input type='checkbox' name='chkGrade{0}' value='{0}' checked>", arg);
		}
		else
		{
			literal4.Text = string.Format("<input type='checkbox' name='chkGrade{0}' value='{0}'>", arg);
		}
	}

	private void UpdateAuthority(int sid, int authority_code_id, int admin_id, int admin_grade_id, int authority_type)
	{
		if (!update)
		{
			return;
		}
		using AdminBiz adminBiz = new AdminBiz();
		if (authority_type == 0)
		{
			adminBiz.RemoveAdminAuthority(sid);
		}
		else
		{
			adminBiz.AddAdminAuthority(sid, authority_code_id, admin_id, admin_grade_id, authority_type);
		}
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		update = true;
		GetAdminAuthority();
		update = false;
	}

	protected void btnGetAuthority_Click(object sender, EventArgs e)
	{
		GetAdminAuthority();
	}

	protected void Button2_Click(object sender, EventArgs e)
	{
		admin.UpdateAdminAuthorityInfo();
	}

	protected void Button3_Click(object sender, EventArgs e)
	{
		admin.UpdateAdminAuthorityInfo();
	}
}
