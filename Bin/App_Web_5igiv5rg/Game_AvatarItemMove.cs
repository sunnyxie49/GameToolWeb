using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using libs;
using Rappelz.GMToolV3.BizDac;

public class Game_AvatarItemMove : Page, IRequiresSessionState
{
	private int account_id;

	private int avatar_id;

	private string hserver;

	private DateTime login_time;

	private DateTime logout_time;

	protected DropDownList ddlServer;

	protected HtmlInputRadioButton rdoAID;

	protected HtmlInputRadioButton rdoSID;

	protected TextBox txtSearch;

	protected Button Button1;

	protected Repeater rptCharacterList;

	protected LinkButton hdItemList;

	protected Repeater rptAvatar;

	protected Repeater rptBank;

	protected HtmlGenericControl divItemList;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["__EVENTTARGET"] == hdItemList.ClientID)
		{
			hdItemList_Click(base.Request["__EVENTARGUMENT"]);
		}
		if (!base.IsPostBack)
		{
			server.SetServer(ref ddlServer, base.Request.QueryString["server"]);
		}
	}

	protected void hdItemList_Click(string args)
	{
		string character_id = args.Split(',')[0].ToLower();
		string text = args.Split(',')[1].ToLower();
		hserver = args.Split(',')[2].ToLower();
		DataSet dataSet = null;
		string connectionString = server.GetConnectionString(hserver);
		using (GameLogBiz gameLogBiz = new GameLogBiz())
		{
			dataSet = gameLogBiz.GetItemList(connectionString, text, character_id);
		}
		DataTable dataTable = new DataTable();
		DataTable dataTable2 = new DataTable();
		dataTable = dataSet.Tables[0];
		dataTable2 = dataSet.Tables[1];
		login_time = (DateTime)dataSet.Tables[2].Rows[0]["login_time"];
		logout_time = (DateTime)dataSet.Tables[2].Rows[0]["logout_time"];
		rptAvatar.DataSource = dataTable;
		rptAvatar.DataBind();
		rptBank.DataSource = dataTable2;
		rptBank.DataBind();
		divItemList.Attributes.Add("style", "display:block");
		if (dataSet != null)
		{
			dataSet = null;
		}
	}

	protected string BindModifyForBank(object item)
	{
		long num = (long)DataBinder.Eval(item, "sid");
		if (login_time < logout_time)
		{
			return $"<input type='button' class=\"btn btn-inverse btn-small\" value='Modify' onClick=\"OpenOwnerChangeItemPopup('{hserver}', 'character','{num.ToString()}')\">";
		}
		return "";
	}

	protected string BindModifyForAvater(object item)
	{
		long num = (long)DataBinder.Eval(item, "sid");
		if (login_time < logout_time)
		{
			return $"<input type='button' class=\"btn btn-inverse btn-small\" value='Modify' onClick=\"OpenOwnerChangeItemPopup('{hserver}', 'character','{num.ToString()}')\">";
		}
		return "";
	}

	protected string BindFlag(object item)
	{
		string text = "";
		string itemFlagValue = resource.GetItemFlagValue((int)DataBinder.Eval(item, "flag"));
		int num = (int)DataBinder.Eval(item, "wear_info");
		if (itemFlagValue != "")
		{
			text = itemFlagValue;
		}
		int num2 = (int)DataBinder.Eval(item, "remain_time");
		if (num2 > 0)
		{
			if (num.ToString() != "22")
			{
				int result = 0;
				int.TryParse(ConfigurationManager.AppSettings["GMT"], out result);
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0).AddHours(result).AddSeconds(num2);
				text = ((!(text != "")) ? dateTime.ToString("yyyy-MM-dd HH:mm:ss") : (text + "<br />" + dateTime.ToString("yyyy-MM-dd HH:mm:ss")));
			}
			else
			{
				text = GetLeftTime(num2);
			}
		}
		return text;
	}

	protected string BindGet(object item)
	{
		int gCode = (int)DataBinder.Eval(item, "gcode");
		DateTime dateTime = (DateTime)DataBinder.Eval(item, "create_time");
		return string.Format(arg2: ((DateTime)DataBinder.Eval(item, "update_time")).ToString(), format: "<span title='create time : {1}\nupdate time : {2}'>{0}</span>", arg0: resource.GetItemGCode(gCode), arg1: dateTime.ToString());
	}

	protected string BindCharaterName(object item)
	{
		return resource.GetItemName(id: Convert.ToInt32(((int)DataBinder.Eval(item, "code")).ToString()), resource_server_no: server.GetResourceServerNo(hserver));
	}

	protected string BindCharacterName(object item)
	{
		int num = (int)DataBinder.Eval(item, "sid");
		string arg = (string)DataBinder.Eval(item, "name");
		if (num.ToString() == avatar_id.ToString())
		{
			return $"<b>{arg}({num})</b>";
		}
		return $"{arg}({num})</b>";
	}

	protected string BindAccount(object item)
	{
		string arg = (string)DataBinder.Eval(item, "account");
		return string.Format("{0}({1})", arg, ((int)DataBinder.Eval(item, "account_id")).ToString());
	}

	protected string BindItemList(object item)
	{
		int num = (int)DataBinder.Eval(item, "sid");
		return string.Format(arg1: ((int)DataBinder.Eval(item, "account_id")).ToString(), format: "<a id=avatar_{0} href=\"javascript:__doPostBack('" + hdItemList.ClientID + "','{0},{1},{2}')\" onFocus=\"this.blur();\" style=\"cursor:hand\">view Item List</a>", arg0: num.ToString(), arg2: ddlServer.SelectedValue);
	}

	protected string BindLoginCheck(object item)
	{
		DateTime t = (DateTime)DataBinder.Eval(item, "login_time");
		DateTime t2 = (DateTime)DataBinder.Eval(item, "logout_time");
		if (t > t2)
		{
			return "login";
		}
		return "logout";
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

	protected void Button1_Click(object sender, EventArgs e)
	{
		divItemList.Attributes.Add("style", "display:none;");
		try
		{
			if (txtSearch.Text == "")
			{
				if (rdoAID.Checked)
				{
					common.MsgBox(this, "Insertaccountidtobesearched", "Insert account id to be searched!");
				}
				else if (rdoSID.Checked)
				{
					common.MsgBox(this, "Insertcharactersidtobesearched", "Insert character sid to be searched!");
				}
				return;
			}
			if (rdoAID.Checked)
			{
				int.TryParse(txtSearch.Text.Trim(), out account_id);
			}
			else if (rdoSID.Checked)
			{
				int.TryParse(txtSearch.Text.Trim(), out avatar_id);
			}
			if (account_id + avatar_id < 1)
			{
				common.MsgBox(this, "Onlynumber", "Only number.");
			}
			DataSet dataSet = null;
			string connectionString = server.GetConnectionString(ddlServer.SelectedValue);
			using (GameLogBiz gameLogBiz = new GameLogBiz())
			{
				gameLogBiz.GetAvatarList(connectionString, account_id, avatar_id);
			}
			rptCharacterList.DataSource = dataSet;
			rptCharacterList.DataBind();
			if (dataSet != null)
			{
				dataSet = null;
			}
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
		}
	}
}
