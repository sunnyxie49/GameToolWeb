using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using Rappelz.GMToolV3.BizDac;

namespace libs
{
    public class admin
    {
		public static DataSet ds;

		public static DateTime updateTime;

		public static void UpdateAdminAuthorityInfo()
		{
			new SqlConnection(ConfigurationManager.ConnectionStrings["gmtool"].ConnectionString);
			if (ds == null)
			{
				ds = new DataSet();
			}
			else
			{
				ds.Dispose();
				ds = new DataSet();
			}
			AdminBiz adminBiz = new AdminBiz();
			ds = adminBiz.Biz_GetAdminAuthority();
			ds.Tables[0].TableName = "AdminAuthority";
			updateTime = DateTime.Now;
		}

		public static DataTable GetAdminAuthorityInfo()
		{
			try
			{
				if (ds == null || ds.Tables.Count == 0)
				{
					UpdateAdminAuthorityInfo();
				}
				return ds.Tables["AdminAuthority"];
			}
			catch (Exception)
			{
				UpdateAdminAuthorityInfo();
				return ds.Tables["AdminAuthority"];
			}
		}

		public static bool Login(string account, string password, out string msg)
		{
			bool result = false;
			SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["gmtool"].ConnectionString);
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "select *, IsNull( (select name from tb_admin_team where sid = tb_admin.team_id), '') as team, IsNull((select name from tb_admin_grade where sid = tb_admin.grade_id), '') as grade from tb_admin where account = @account";
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@account", SqlDbType.VarChar).Value = account;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			if (sqlDataReader.Read())
			{
				if (password == sqlDataReader["password"].ToString())
				{
					int num = (int)sqlDataReader["sid"];
					string str = (string)sqlDataReader["name"];
					int num2 = (int)sqlDataReader["team_id"];
					string str2 = (string)sqlDataReader["team"];
					int num3 = (int)sqlDataReader["grade_id"];
					string str3 = (string)sqlDataReader["grade"];
					HttpContext.Current.Response.Cookies["GmTool"]["admin_id"] = num.ToString();
					HttpContext.Current.Response.Cookies["GmTool"]["account"] = account;
					HttpContext.Current.Response.Cookies["GmTool"]["team_id"] = num2.ToString();
					HttpContext.Current.Response.Cookies["GmTool"]["grade_id"] = num3.ToString();
					HttpContext.Current.Response.Cookies["GmTool"]["name"] = HttpUtility.UrlEncode(str);
					HttpContext.Current.Response.Cookies["GmTool"]["team"] = HttpUtility.UrlEncode(str2);
					HttpContext.Current.Response.Cookies["GmTool"]["grade"] = HttpUtility.UrlEncode(str3);
					string password2 = string.Format(ConfigurationManager.AppSettings["login_key"], num, account, num2, num3, "d");
					string value = FormsAuthentication.HashPasswordForStoringInConfigFile(password2, "MD5");
					HttpContext.Current.Response.Cookies["GmTool"]["authkey"] = value;
					result = true;
					msg = "";
				}
				else
				{
					msg = "登录失败.";
				}
			}
			else
			{
				msg = "登录失败.";
			}
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
			return result;
		}

		public static void Logout()
		{
			HttpContext.Current.Response.Cookies["GmTool"]["admin_id"] = "";
			HttpContext.Current.Response.Cookies["GmTool"]["account"] = "";
			HttpContext.Current.Response.Cookies["GmTool"]["name"] = "";
			HttpContext.Current.Response.Cookies["GmTool"]["team_id"] = "";
			HttpContext.Current.Response.Cookies["GmTool"]["team"] = "";
			HttpContext.Current.Response.Cookies["GmTool"]["grade_id"] = "";
			HttpContext.Current.Response.Cookies["GmTool"]["grade"] = "";
			HttpContext.Current.Response.Cookies["GmTool"]["authkey"] = "";
			HttpContext.Current.Response.Cookies["GmTool"].Expires = DateTime.Now.AddDays(-1.0);
		}

		public static bool IsAuth()
		{
			if (HttpContext.Current.Request.Cookies["GmTool"] != null)
			{
				string a = HttpContext.Current.Request.Cookies["GmTool"]["authkey"];
				string text = HttpContext.Current.Request.Cookies["GmTool"]["admin_id"];
				string text2 = HttpContext.Current.Request.Cookies["GmTool"]["account"];
				HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["GmTool"]["name"]);
				string text3 = HttpContext.Current.Request.Cookies["GmTool"]["team_id"];
				string text4 = HttpContext.Current.Request.Cookies["GmTool"]["grade_id"];
				string password = string.Format(ConfigurationManager.AppSettings["login_key"], text, text2, text3, text4, "d");
				string b = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
				if (a != b)
				{
					return false;
				}
				return true;
			}
			return false;
		}

		public static bool Authority()
		{
			string arg = HttpContext.Current.Request.PhysicalPath.Substring(HttpContext.Current.Request.PhysicalApplicationPath.Length, HttpContext.Current.Request.PhysicalPath.Length - HttpContext.Current.Request.PhysicalApplicationPath.Length).ToLower().Replace("\\", "/");
			if (IsAuth())
			{
				/*if (GetAccount() == "suezou")
				{
					return true;
				}
				if (GetAccount() == "#suezou")
				{
					return true;
				}
				if (GetAccount() == "#miniggo")
				{
					return true;
				}*/
				if (GetAdminGradeID() == 1)
				{
					return true;
				}
				DataTable adminAuthorityInfo = GetAdminAuthorityInfo();
				string filterExpression = $" code = '{arg}' and (admin_id = {GetAdminID()} or admin_grade_id = {GetAdminGradeID()}) ";
				DataRow[] array = adminAuthorityInfo.Select(filterExpression, "authority_type desc");
				if (array.Length > 0)
				{
					if ((int)array[0]["authority_type"] > 0)
					{
						return true;
					}
					return false;
				}
				return false;
			}
			return false;
		}

		public static bool Authority(string code)
		{
			if (IsAuth())
			{
				if (GetAdminGradeID() == 1)
				{
					return true;
				}
				DataTable adminAuthorityInfo = GetAdminAuthorityInfo();
				string filterExpression = $" code = '{code}' and (admin_id = {GetAdminID()} or admin_grade_id = {GetAdminGradeID()}) ";
				DataRow[] array = adminAuthorityInfo.Select(filterExpression, "authority_type desc");
				if (array.Length > 0)
				{
					if ((int)array[0]["authority_type"] > 0)
					{
						return true;
					}
					return false;
				}
				return false;
			}
			return false;
		}

		public static bool Authority(string server, string function)
		{
			if (IsAuth())
			{
				return Authority(function);
			}
			return false;
		}

		public static int GetAdminID()
		{
			if (IsAuth())
			{
				return common.ToInt(HttpContext.Current.Request.Cookies["GmTool"]["admin_id"], 0);
			}
			return 0;
		}

		public static string GetAccount()
		{
			if (IsAuth())
			{
				return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["GmTool"]["account"]);
			}
			return "";
		}

		public static int GetAdminID(int gmnick_id)
		{
			AdminBiz adminBiz = new AdminBiz();
			DataSet dataSet = adminBiz.Biz_GetAdminID(gmnick_id);
			int result = 0;
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				result = int.Parse(dataSet.Tables[0].Rows[0]["admin_id"].ToString());
			}
			return result;
		}

		public static string GetAdminName()
		{
			if (IsAuth())
			{
				return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["GmTool"]["name"]);
			}
			return "";
		}

		public static int GetAdminTeamID()
		{
			if (IsAuth())
			{
				return common.ToInt(HttpContext.Current.Request.Cookies["GmTool"]["team_id"], -1);
			}
			return -1;
		}

		public static string GetAdminTeam()
		{
			if (IsAuth())
			{
				return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["GmTool"]["team"]);
			}
			return "";
		}

		public static int GetAdminGradeID()
		{
			if (IsAuth())
			{
				return common.ToInt(HttpContext.Current.Request.Cookies["GmTool"]["grade_id"], -1);
			}
			return -1;
		}

		public static string GetAdminGrade()
		{
			if (IsAuth())
			{
				return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["GmTool"]["grade"]);
			}
			return "";
		}
	}
}
