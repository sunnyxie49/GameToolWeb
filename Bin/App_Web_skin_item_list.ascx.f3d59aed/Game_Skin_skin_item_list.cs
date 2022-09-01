using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;

public class Game_Skin_skin_item_list : UserControl
{
	protected Repeater rptItemList;

	protected Literal ltrItemLayer;

	public DataTable dtItemList;

	public string SelectedServer = "";

	public int ResourceDbNo;

	public DataTable dtEquipItemList;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		rptItemList.DataSource = dtItemList;
		rptItemList.ItemDataBound += rptCharacterList_ItemDataBound;
		rptItemList.DataBind();
	}

	private void rptCharacterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		Literal literal = (Literal)e.Item.FindControl("ltrsid");
		Literal literal2 = (Literal)e.Item.FindControl("ltrIcon");
		Literal literal3 = (Literal)e.Item.FindControl("ltrName");
		Literal literal4 = (Literal)e.Item.FindControl("ltrCount");
		Literal literal5 = (Literal)e.Item.FindControl("ltrEndurance");
		Literal literal6 = (Literal)e.Item.FindControl("ltrethereal");
		Literal literal7 = (Literal)e.Item.FindControl("ltrGet");
		Literal literal8 = (Literal)e.Item.FindControl("ltrClass");
		Literal literal9 = (Literal)e.Item.FindControl("ltrWear");
		Literal literal10 = (Literal)e.Item.FindControl("ltrTime");
		Literal literal11 = (Literal)e.Item.FindControl("ltrBlank");
		HtmlControl htmlControl = (HtmlControl)e.Item.FindControl("trLine");
		long num = (long)((DataRowView)e.Item.DataItem)["sid"];
		literal.Text = num.ToString();
		try
		{
			if ((int)((DataRowView)e.Item.DataItem)["wear_info"] != 255)
			{
				htmlControl.Attributes.Add("class", "bgGray");
			}
		}
		catch (Exception)
		{
		}
		bool flag = false;
		if (dtEquipItemList != null)
		{
			foreach (DataRow row in dtEquipItemList.Rows)
			{
				foreach (DataColumn column in dtEquipItemList.Columns)
				{
					if ((long)row[column] == num)
					{
						if (column.ColumnName.Contains("belt"))
						{
							htmlControl.Attributes.Add("class", "success");
						}
						else
						{
							htmlControl.Attributes.Add("class", "warning");
						}
						flag = true;
						break;
					}
					if (flag)
					{
						break;
					}
				}
			}
		}
		literal11.Text = string.Format("<input class='btn btn-primary btn-small' type='button' value='Modify' onClick=\"OpenEditItemPopup('{0}', '{1}')\">", SelectedServer, ((DataRowView)e.Item.DataItem)["sid"]);
		DataRow itemResource = resource.GetItemResource(ResourceDbNo, (int)((DataRowView)e.Item.DataItem)["code"]);
		if (itemResource == null)
		{
			htmlControl.Style.Add("background-color", "#ff9900");
			literal3.Text = string.Format("ItemCode : {0}", (int)((DataRowView)e.Item.DataItem)["code"]);
			literal4.Text = ((DataRowView)e.Item.DataItem)["cnt"].ToString();
			literal10.Text = "ItemResource";
			return;
		}
		if ((int)itemResource["group"] == 13)
		{
			literal11.Text += string.Format("<input class='btn btn-primary lastBtn btn-small' type='button' value='Summon' onClick=\"OpenSummonPopup('{0}', '{1}')\">", SelectedServer, ((DataRowView)e.Item.DataItem)["sid"]);
		}
		if (File.Exists(base.Server.MapPath(string.Format("../img/icon/{0}.jpg", itemResource["icon_file_name"]))))
		{
			literal2.Text = string.Format("<img src='../img/icon/{0}.jpg' align='absmiddle'>", itemResource["icon_file_name"]);
		}
		else if ((int)itemResource["type"] == 2)
		{
			literal2.Text = "<img src='../img/icon/icon_item_etc_0006.jpg' align='absmiddle'>";
		}
		else
		{
			literal2.Text = "&nbsp;";
		}
		StringBuilder stringBuilder = new StringBuilder(1000);
		if ((int)((DataRowView)e.Item.DataItem)["flag"] == 0)
		{
			for (int i = 0; i < 4; i++)
			{
				int num2 = (int)((DataRowView)e.Item.DataItem)[$"socket_{i}"];
				if (num2 > 0)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append("<br/ >");
					}
					stringBuilder.AppendFormat("#{0} : {1}", i + 1, resource.GetItemName(ResourceDbNo, num2));
				}
			}
		}
		if (stringBuilder.Length == 0)
		{
			literal3.Text = resource.GetItemName(ResourceDbNo, (int)((DataRowView)e.Item.DataItem)["code"], (int)((DataRowView)e.Item.DataItem)["level"], (int)((DataRowView)e.Item.DataItem)["enhance"], 300);
		}
		else
		{
			literal3.Text = resource.GetItemName(ResourceDbNo, (int)((DataRowView)e.Item.DataItem)["code"], (int)((DataRowView)e.Item.DataItem)["level"], (int)((DataRowView)e.Item.DataItem)["enhance"], 300) + "<br />" + stringBuilder.ToString();
		}
		literal4.Text = ((DataRowView)e.Item.DataItem)["cnt"].ToString();
		literal5.Text = string.Format("{0} / {1}", ((DataRowView)e.Item.DataItem)["now_endurance"], itemResource["endurance"]);
		literal7.Text = string.Format("<span title='create time : {1}\nupdate time : {2}'>{0}</span>", resource.GetItemGCode((int)((DataRowView)e.Item.DataItem)["gcode"]), ((DataRowView)e.Item.DataItem)["create_time"], ((DataRowView)e.Item.DataItem)["update_time"]);
		literal7.Text = resource.GetItemType((int)itemResource["type"]);
		literal8.Text = resource.GetItemGroup((int)itemResource["group"]);
		literal8.Text = resource.GetItemClass((int)itemResource["class"]);
		literal9.Text = resource.GetItemWearType((int)itemResource["wear_type"]);
		string itemFlagValue = resource.GetItemFlagValue((int)((DataRowView)e.Item.DataItem)["flag"]);
		if (itemFlagValue != "")
		{
			literal10.Text = itemFlagValue;
		}
		try
		{
			int num3 = (int)((DataRowView)e.Item.DataItem)["remain_time"];
			if (num3 > 0)
			{
				if ((byte)itemResource["decrease_type"] == 2)
				{
					int result = 0;
					int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
					DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddHours(result).AddSeconds(num3);
					if (literal10.Text != "")
					{
						literal10.Text = literal10.Text + "<br />" + dateTime.ToString("yyyy-MM-dd HH:mm:ss");
					}
					else
					{
						literal10.Text = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
					}
				}
				else
				{
					literal10.Text = GetLeftTime((int)((DataRowView)e.Item.DataItem)["remain_time"]);
				}
			}
		}
		catch (Exception)
		{
		}
		string text = itemResource["ethereal_durability"].ToString();
		literal6.Text = ((DataRowView)e.Item.DataItem)["ethereal_durability"].ToString() + "/" + text;
		string itemToolTip = resource.GetItemToolTip(ResourceDbNo, (int)((DataRowView)e.Item.DataItem)["code"], (int)((DataRowView)e.Item.DataItem)["level"], (int)((DataRowView)e.Item.DataItem)["enhance"], 300);
		literal3.Text = string.Format("<a herf='#' onmouseover=\"ItemEffectOpen('item_effect_{0}')\" onmouseleave=\"ItemEffectClose('item_effect_{0}')\">{1}</a>", num, literal3.Text);
		ltrItemLayer.Text += string.Format("<div id='item_effect_{0}' class='item_effect' >{1} {2}</div>", num, itemToolTip, resource.GetItemFlagValue((int)((DataRowView)e.Item.DataItem)["flag"]));
	}

	private string GetLeftTime(int sec)
	{
		try
		{
			int num = sec;
			int num2 = num / 86400;
			int num3 = num % 86400 / 3600;
			int num4 = num % 86400 % 3600 / 60;
			string text = "";
			text = ((num2 > 0) ? (num2.ToString().PadLeft(3, '_') + "D") : "");
			text += ((num2 > 0 || num3 > 0) ? (num3.ToString().PadLeft(3, '_') + "H ") : "");
			text += ((num2 > 0 || num3 > 0 || num4 > 0) ? (num4.ToString().PadLeft(3, '_') + "M") : "");
			return string.Format("<span title='{1} sec'>{0}<span>", text.Replace("_", "&nbsp;"), num.ToString("#,###"));
		}
		catch (Exception ex)
		{
			return ex.ToString();
		}
	}
}
