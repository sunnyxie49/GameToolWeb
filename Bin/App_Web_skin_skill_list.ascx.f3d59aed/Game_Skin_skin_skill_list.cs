using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_Skin_skin_skill_list : UserControl
{
	protected Repeater rptSkillList;

	public DataTable dtSkillList;

	public string SelectedServer = "";

	public int ResourceDbNo;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		rptSkillList.DataSource = dtSkillList;
		rptSkillList.ItemDataBound += rptSkillList_ItemDataBound;
		rptSkillList.DataBind();
	}

	private void rptSkillList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
		{
			return;
		}
		Literal literal = (Literal)e.Item.FindControl("ltrsid");
		Literal literal2 = (Literal)e.Item.FindControl("ltrIcon");
		Literal literal3 = (Literal)e.Item.FindControl("ltrSkillName");
		Literal literal4 = (Literal)e.Item.FindControl("ltrType");
		Literal literal5 = (Literal)e.Item.FindControl("ltrInfo");
		Literal literal6 = (Literal)e.Item.FindControl("ltrBlank");
		HtmlControl htmlControl = (HtmlControl)e.Item.FindControl("trLine");
		literal.Text = ((DataRowView)e.Item.DataItem)["sid"].ToString();
		literal6.Text = string.Format("<input class='btn btn-primary btn-small' type='button' value='Modify' onClick=\"OpenEditSkillPopup('{0}', '{1}')\">", SelectedServer, ((DataRowView)e.Item.DataItem)["sid"]);
		DataRow skillResource = resource.GetSkillResource(ResourceDbNo, (int)((DataRowView)e.Item.DataItem)["skill_id"]);
		if (skillResource == null)
		{
			htmlControl.Style.Add("background-color", "#ff9900");
			literal3.Text = string.Format("Skill Id : {0}", (int)((DataRowView)e.Item.DataItem)["skill_id"]);
			literal5.Text = "The skill code does not exist in SkillResource";
			return;
		}
		if (File.Exists(base.Server.MapPath(string.Format("../img/icon/{0}.jpg", skillResource["icon_file_name"]))))
		{
			literal2.Text = string.Format("<img src='../img/icon/{0}.jpg'>", skillResource["icon_file_name"]);
		}
		else
		{
			literal2.Text = "&nbsp;";
		}
		string stringResource = resource.GetStringResource(ResourceDbNo, (int)skillResource["text_id"]);
		if (stringResource.Length > 0)
		{
			literal3.Text = string.Format("{0} Lv {1}", stringResource, ((DataRowView)e.Item.DataItem)["skill_level"]);
		}
		else
		{
			try
			{
				literal3.Text = string.Format("(text_id : {0}) Lv {1}", skillResource["text_id"], ((DataRowView)e.Item.DataItem)["skill_level"]);
			}
			catch (Exception ex)
			{
				literal3.Text = ex.ToString();
			}
		}
		literal4.Text = ((skillResource["is_passive"].ToString() == "0") ? "Passive" : "<font color='blue'>Active</font>");
		string stringResource2 = resource.GetStringResource(ResourceDbNo, (int)skillResource["tooltip_id"]);
		if (stringResource2.Length > 0)
		{
			try
			{
				string text = stringResource2.Substring(stringResource2.IndexOf("<#ffffcc>") + 9);
				literal5.Text = text.Substring(0, text.IndexOf("<#ffffff>"));
			}
			catch (Exception)
			{
			}
		}
		else
		{
			literal5.Text = string.Format("(tooltip_id : {0})", skillResource["tooltip_id"]);
		}
	}
}
