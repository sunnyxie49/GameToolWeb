using System;
using System.Web;
using System.Web.UI;

namespace libs
{
    public class common
    {
		public static bool IsDebug()
		{
			return false;
		}

		public static void MsgBoxWindow(string msg)
		{
			HttpContext.Current.Response.Write("<script>\n");
			HttpContext.Current.Response.Write("\twindow.showModalDialog('/4th/popup/alert.aspx?msg=" + msg + "', 'alert', 'dialogHeight: 238px; dialogWidth: 280px;edge: Raised; center: Yes; help: No;resizable:No;status:No;');\n");
			HttpContext.Current.Response.Write("</script>\n");
		}

		public static void MsgBox(Page page, string sScriptKey, string sMsg)
		{
			sMsg = sMsg.Trim().Replace("\r", "").Replace("\n", "\\n");
			string script = "alert('" + sMsg + "')";
			page.ClientScript.RegisterStartupScript(page.GetType(), sScriptKey, script, addScriptTags: true);
		}

		public static void MsgBox(string msg)
		{
			msg = msg.Trim().Replace("\r", "").Replace("\n", "\\n");
			HttpContext.Current.Response.Write("<script>\n");
			HttpContext.Current.Response.Write("\talert(\"" + msg + "\");\n");
			HttpContext.Current.Response.Write("</script>\n");
		}

		public static void MsgBox(string msg, string url)
		{
			msg = msg.Trim().Replace("\r", "").Replace("\n", "\\n");
			url = url.Trim();
			HttpContext.Current.Response.Write("<script>\n");
			HttpContext.Current.Response.Write("\talert(\"" + msg + "\");\n");
			if (url == "back")
			{
				HttpContext.Current.Response.Write("history.go(-1);\n");
				HttpContext.Current.Response.Write("</script>\n");
			}
			else if (url == "close")
			{
				HttpContext.Current.Response.Write("window.close();\n");
				HttpContext.Current.Response.Write("</script>\n");
			}
			else
			{
				HttpContext.Current.Response.Write("</script>\n");
				HttpContext.Current.Response.Write("<meta http-equiv=refresh content='0; url=" + url + "'>\n");
			}
			HttpContext.Current.Response.End();
		}

		public static bool IsInt(string var)
		{
			bool flag = false;
			try
			{
				int.Parse(var);
				return true;
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return false;
			}
		}

		public static byte ToByte(string var)
		{
			if (var == null || var == "")
			{
				return 0;
			}
			try
			{
				return byte.Parse(var);
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return 0;
			}
		}

		public static byte ToByte(string var, byte iInit)
		{
			if (var == null || var == "")
			{
				return 0;
			}
			try
			{
				return byte.Parse(var);
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return iInit;
			}
		}

		public static int ToInt(string var)
		{
			if (var == null || var == "")
			{
				return 0;
			}
			try
			{
				return int.Parse(var);
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return 0;
			}
		}

		public static int ToInt(string var, int iInit)
		{
			if (var == null || var == "")
			{
				return iInit;
			}
			try
			{
				return int.Parse(var);
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return iInit;
			}
		}

		public static long ToLong(string var)
		{
			if (var == null || var == "")
			{
				return 0L;
			}
			try
			{
				return long.Parse(var);
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return 0L;
			}
		}

		public static long ToLong(string var, long iInit)
		{
			if (var == null || var == "")
			{
				return iInit;
			}
			try
			{
				return long.Parse(var);
			}
			catch (Exception ex)
			{
				_ = ex.Message;
				return iInit;
			}
		}

		public static string ToString(string var)
		{
			if (var == null || var == "")
			{
				return "";
			}
			string text = "";
			try
			{
				return var.ToString();
			}
			catch (Exception)
			{
				return "";
			}
		}

		public static string ToString(DateTime dtVar)
		{
			string text = "";
			try
			{
				return dtVar.ToString();
			}
			catch (Exception)
			{
				return "";
			}
		}

		public static bool IsDateTime(string var)
		{
			bool flag = false;
			try
			{
				DateTime.Parse(var);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public static string PageList(string link, string searchUrl, int totalCnt, ref int page, int pageSize, int listSize)
		{
			if (page < 1)
			{
				page = 1;
			}
			if (pageSize < 0)
			{
				pageSize = 15;
			}
			if (listSize < 0)
			{
				listSize = 10;
			}
			int num;
			int num3;
			int num4;
			int num5;
			int num6;
			if (totalCnt > 0)
			{
				num = ((totalCnt % pageSize <= 0) ? (totalCnt / pageSize) : (totalCnt / pageSize + 1));
				if (page > num)
				{
					page = num;
				}
				int num2 = ((page % listSize <= 0) ? (page / listSize) : (page / listSize + 1));
				num3 = (num2 - 1) * listSize + 1;
				num4 = num2 * listSize;
				if (num4 > num)
				{
					num4 = num;
				}
				num5 = num3 - 1;
				num6 = num4 + 1;
			}
			else
			{
				num = 1;
				page = 1;
				num3 = 1;
				num4 = 1;
				num5 = 1;
				num6 = 1;
			}
			string str = "";
			str = ((num5 <= 0 || num5 >= num3) ? (str + "<li class=\"disabled\"><a href=\"#\">«</a></li>") : (str + $"<ul><li><a href=\"{link}?page={num5}{searchUrl}\">&lt</a></li>"));
			for (int i = num3; i <= num4; i++)
			{
				if (page == i)
				{
					object obj = str;
					str = string.Concat(obj, "<li class='active'><a href=\"#\">", i, "</a></li>");
				}
				else
				{
					str += string.Format("<li><a href=\"{0}?page={1}{2}\">{1}</a></li>", link, i, searchUrl);
				}
			}
			if (num6 > num4 && num6 <= num)
			{
				return str + $"<li><a href=\"{link}?page={num6}{searchUrl}\"></a></li></ul>";
			}
			return str + "<li class=\"disabled\"><a href=\"#\">»</a></li>";
		}

		public static string DateFormat()
		{
			return "yyyy-MM-dd HH:mm:ss";
		}
	}
}
