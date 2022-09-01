using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;
using Rappelz.GMToolV3.BizDac;

public class Admin_authority_admin : Page, IRequiresSessionState
{
	private bool update;

	protected DropDownList ddlAdmin;

	protected Button btnGetAuthority;

	protected Button Button1;

	protected Button Button2;

	protected Button Button3;

	protected Repeater rptAdmin;

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
			dataSet = adminBiz.GetAdminAccountGradeInfo();
		}
		if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
		{
			for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
			{
				dataRow = dataSet.Tables[0].Rows[i];
				int num = (int)dataRow["sid"];
				string text = (string)dataRow["name"];
				string text2 = (string)dataRow["grade_name"];
				int num2 = (int)dataRow["grade_id"];
				if (text2 != "")
				{
					text = $"{text} ({text2})";
				}
				ListItem item = new ListItem(text, $"{num}|{num2}");
				ddlAdmin.Items.Add(item);
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
		string[] array = ddlAdmin.SelectedValue.Split('|');
		int admin_id = int.Parse(array[0]);
		int admin_grade_id = int.Parse(array[1]);
		DataSet dataSet = null;
		using (AdminBiz adminBiz = new AdminBiz())
		{
			dataSet = adminBiz.GetAdminAuthorityTotalListV3(admin_id, admin_grade_id);
		}
		rptAdmin.DataSource = dataSet;
		rptAdmin.ItemDataBound += rptAdmin_ItemDataBound;
		rptAdmin.DataBind();
		if (dataSet != null)
		{
			dataSet = null;
		}
	}

	private void rptAdmin_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		string[] array = ddlAdmin.SelectedValue.Split('|');
		int admin_id = int.Parse(array[0]);
		if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
		{
			return;
		}
		Literal literal = (Literal)e.Item.FindControl("ltrCode");
		Literal literal2 = (Literal)e.Item.FindControl("ltrCategory");
		Literal literal3 = (Literal)e.Item.FindControl("ltrInfo");
		Literal literal4 = (Literal)e.Item.FindControl("ltrGrade");
		Literal literal5 = (Literal)e.Item.FindControl("ltrPerson");
		literal.Text = ((DataRowView)e.Item.DataItem)["Code"].ToString();
		literal2.Text = ((DataRowView)e.Item.DataItem)["Category"].ToString();
		literal3.Text = ((DataRowView)e.Item.DataItem)["Info"].ToString();
		int num = (int)((DataRowView)e.Item.DataItem)["authority_id"];
		_ = (int)((DataRowView)e.Item.DataItem)["grade_authority_id"];
		string arg = ((DataRowView)e.Item.DataItem)["sid"].ToString();
		if (update)
		{
			if ((int)((DataRowView)e.Item.DataItem)["grade_authority_type"] > 0)
			{
				literal4.Text = string.Format("<input type='checkbox' name='chkGrade{0}' value='{0}' checked disabled='true'>", arg);
			}
			else
			{
				literal4.Text = string.Format("<input type='checkbox' name='chkGrade{0}' value='{0}' disabled='true'>", arg);
			}
			if (base.Request.Form[$"chkPerson{arg}"] != null)
			{
				if (num == -1)
				{
					UpdateAuthority(num, (int)((DataRowView)e.Item.DataItem)["sid"], admin_id, -1, 1);
				}
				literal5.Text = string.Format("<input type='checkbox' name='chkPerson{0}' value='{0}' checked>", arg);
			}
			else
			{
				if (num != -1)
				{
					UpdateAuthority(num, (int)((DataRowView)e.Item.DataItem)["sid"], admin_id, -1, 0);
				}
				literal5.Text = string.Format("<input type='checkbox' name='chkPerson{0}' value='{0}'>", arg);
			}
		}
		else
		{
			if ((int)((DataRowView)e.Item.DataItem)["grade_authority_type"] > 0)
			{
				literal4.Text = string.Format("<input type='checkbox' name='chkGrade{0}' value='{0}' checked disabled='true'>", arg);
			}
			else
			{
				literal4.Text = string.Format("<input type='checkbox' name='chkGrade{0}' value='{0}' disabled='true'>", arg);
			}
			if ((int)((DataRowView)e.Item.DataItem)["authority_type"] > 0)
			{
				literal5.Text = string.Format("<input type='checkbox' name='chkPerson{0}' value='{0}' checked>", arg);
			}
			else
			{
				literal5.Text = string.Format("<input type='checkbox' name='chkPerson{0}' value='{0}'>", arg);
			}
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
