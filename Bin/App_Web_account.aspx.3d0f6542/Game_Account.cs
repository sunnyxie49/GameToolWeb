using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using libs;
using Rappelz.GMToolV3.BizDac;

public class Game_Account : Page, IRequiresSessionState
{
	protected FileUpload FileUpload1;

	protected Button Button1;

	private DateTime nowT;

	protected DefaultProfile Profile => (DefaultProfile)Context.Profile;

	protected HttpApplication ApplicationInstance => Context.ApplicationInstance;

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected void Button1_Click(object sender, EventArgs e)
	{
		nowT = DateTime.Now;
		string text = "";
		if (FileUpload1.PostedFile.ContentType == "text/plain")
		{
			if (FileUpload1.HasFile && FileUpload1.PostedFile.ContentLength > 0)
			{
				DataSet dataSet = null;
				StreamReader streamReader = new StreamReader(FileUpload1.FileContent, Encoding.Default);
				text = streamReader.ReadToEnd();
				string[] array = text.Split('\n');
				DataTable dataTable = new DataTable("tb_account_log");
				dataTable.Columns.Add("seq");
				dataTable.Columns.Add("account");
				dataTable.Columns.Add("log_date");
				for (int i = 0; i < array.Length; i++)
				{
					dataTable.Rows.Add(i + 1, array[i].Trim(), nowT);
				}
				using (GameLogBiz gameLogBiz = new GameLogBiz())
				{
					gameLogBiz.BulkCopyAccountLog(dataTable);
					dataSet = gameLogBiz.GetAccountList();
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("seq");
				stringBuilder.Append(",");
				stringBuilder.Append("Account");
				stringBuilder.Append(",");
				stringBuilder.Append("Account_id");
				stringBuilder.AppendLine();
				for (int j = 0; j < dataSet.Tables[0].Rows.Count; j++)
				{
					stringBuilder.AppendFormat("{0},{1},{2}", dataSet.Tables[0].Rows[j]["seq"], dataSet.Tables[0].Rows[j]["account"], dataSet.Tables[0].Rows[j]["account_id"]);
					stringBuilder.AppendLine();
				}
				stringBuilder.AppendLine();
				string str = base.Server.UrlPathEncode("account_list_" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv");
				base.Response.AddHeader("Content-Disposition", "attachment;filename=" + str);
				base.Response.AddHeader("Expires", "0");
				base.Response.ContentType = "text/txt";
				StringWriter stringWriter = new StringWriter();
				base.Response.Charset = Encoding.Default.ToString();
				base.Response.ContentEncoding = Encoding.Default;
				base.Response.Write(stringBuilder.ToString().Trim());
				base.Response.End();
				if (dataSet != null)
				{
					dataSet = null;
				}
				streamReader?.Close();
				stringWriter?.Close();
			}
		}
		else
		{
			common.MsgBox(this, "Onlytextfile", "仅文本文件.");
		}
	}
}
