using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Rappelz.GMToolV3.BizDac;

namespace libs
{
    public class server
    {
		private static bool update = false;

		public static DataTable dtServer = null;

		public static DateTime updateTime = default(DateTime);

		public static void UpdateServerInfo()
		{
			if (!update)
			{
				update = true;
				GameBiz gameBiz = new GameBiz();
				dtServer = gameBiz.Biz_Server_Info();
				update = false;
				updateTime = DateTime.Now;
			}
		}

		public static DataRow GetServerInfo(string server)
		{
			if (dtServer == null)
			{
				UpdateServerInfo();
			}
			if (dtServer.Rows.Count > 0)
			{
				DataRow[] array = dtServer.Select($"code = '{server}'");
				if (array.Length > 0)
				{
					return array[0];
				}
			}
			return null;
		}

		public static string GetServerName(string server)
		{
			DataRow serverInfo = GetServerInfo(server);
			if (serverInfo != null)
			{
				return (string)serverInfo["name"];
			}
			return server;
		}

		public static int GetServerListCount()
		{
			if (dtServer == null)
			{
				UpdateServerInfo();
			}
			return dtServer.Rows.Count;
		}

		public static bool IsLog(string server)
		{
			DataRow serverInfo = GetServerInfo(server);
			if (serverInfo == null)
			{
				return false;
			}
			if ((string)serverInfo["log_db_ip"] != "")
			{
				return true;
			}
			return false;
		}

		public static bool IsService(string server, DateTime date)
		{
			DataRow serverInfo = GetServerInfo(server);
			if (serverInfo == null)
			{
				return false;
			}
			if ((DateTime)serverInfo["open_date"] <= date && date <= (DateTime)serverInfo["close_date"])
			{
				return true;
			}
			return false;
		}

		public static void SetServer(ref DropDownList ddlServer)
		{
			ddlServer.Items.Clear();
			if (dtServer == null)
			{
				UpdateServerInfo();
			}
			for (int i = 0; i < dtServer.Rows.Count; i++)
			{
				try
				{
					string arg = dtServer.Rows[i]["name"].ToString();
					string text = dtServer.Rows[i]["code"].ToString();
					if (IsService(text, DateTime.Now))
					{
						ListItem item = new ListItem($"{arg}({text})", text);
						ddlServer.Items.Add(item);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		public static void SetServer(ref DropDownList ddlServer, string selectedServer)
		{
			ddlServer.Items.Clear();
			if (dtServer == null)
			{
				UpdateServerInfo();
			}
			for (int i = 0; i < dtServer.Rows.Count; i++)
			{
				try
				{
					string arg = dtServer.Rows[i]["name"].ToString();
					string text = dtServer.Rows[i]["code"].ToString();
					if (IsService(text, DateTime.Now))
					{
						ListItem item = new ListItem($"{arg}({text})", text);
						ddlServer.Items.Add(item);
					}
				}
				catch (Exception)
				{
				}
			}
			try
			{
				ddlServer.SelectedValue = selectedServer;
			}
			catch (Exception)
			{
			}
		}

		public static void SetAllServer(ref DropDownList ddlServer, string selectedServer)
		{
			ddlServer.Items.Clear();
			if (dtServer == null)
			{
				UpdateServerInfo();
			}
			for (int i = 0; i < dtServer.Rows.Count; i++)
			{
				try
				{
					string arg = dtServer.Rows[i]["name"].ToString();
					string text = dtServer.Rows[i]["code"].ToString();
					ListItem item = new ListItem($"{arg}({text})", text);
					ddlServer.Items.Add(item);
				}
				catch (Exception)
				{
				}
			}
			try
			{
				ddlServer.SelectedValue = selectedServer;
			}
			catch (Exception)
			{
			}
		}

		public static void SetServer(ref Repeater rptServer)
		{
			if (dtServer == null)
			{
				UpdateServerInfo();
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("code");
			dataTable.Columns.Add("name");
			dataTable.Columns.Add("count");
			for (int i = 0; i < dtServer.Rows.Count; i++)
			{
				string server = dtServer.Rows[i]["code"].ToString();
				dtServer.Rows[i]["name"].ToString();
				string value = i.ToString();
				if (IsService(server, DateTime.Now))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow[0] = dtServer.Rows[i]["code"];
					dataRow[1] = dtServer.Rows[i]["name"];
					dataRow[2] = value;
					dataTable.Rows.Add(dataRow);
				}
			}
			rptServer.DataSource = dataTable;
			rptServer.DataBind();
		}

		public static void SetServer(ref CheckBoxList cbServer)
		{
			cbServer.Items.Clear();
			if (dtServer == null)
			{
				UpdateServerInfo();
			}
			for (int i = 0; i < dtServer.Rows.Count; i++)
			{
				try
				{
					string arg = dtServer.Rows[i]["name"].ToString();
					string text = dtServer.Rows[i]["code"].ToString();
					if (IsService(text, DateTime.Now))
					{
						ListItem item = new ListItem($"{arg}({text})", text);
						cbServer.Items.Add(item);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		public static void SetServer(ref RadioButtonList rbServer)
		{
			rbServer.Items.Clear();
			if (dtServer == null)
			{
				UpdateServerInfo();
			}
			for (int i = 0; i < dtServer.Rows.Count; i++)
			{
				try
				{
					string arg = dtServer.Rows[i]["name"].ToString();
					string text = dtServer.Rows[i]["code"].ToString();
					if (IsService(text, DateTime.Now))
					{
						ListItem item = new ListItem($"{arg}({text})", text);
						rbServer.Items.Add(item);
					}
				}
				catch (Exception)
				{
				}
			}
		}

		public static string GetConnectionString(string server)
		{
			DataRow serverInfo = GetServerInfo(server);
			if (serverInfo == null)
			{
				return "";
			}
			return ConfigurationManager.ConnectionStrings["game_db"].ConnectionString.Replace("#@db_ip@#", (string)serverInfo["game_db_ip"]).Replace("#@database@#", (string)serverInfo["game_database"]);
		}

		public static string GetConnectionString(string server, string type)
		{
			DataRow serverInfo = GetServerInfo(server);
			if (serverInfo == null)
			{
				return "";
			}
			if (type == "log")
			{
				return ConfigurationManager.ConnectionStrings["game_log"].ConnectionString.Replace("#@db_ip@#", (string)serverInfo["log_db_ip"]);
			}
			return ConfigurationManager.ConnectionStrings["game_db"].ConnectionString.Replace("#@db_ip@#", (string)serverInfo["game_db_ip"]).Replace("#@database@#", (string)serverInfo["game_database"]);
		}

		public static int GetResourceServerNo(string server_name)
		{
			DataRow serverInfo = GetServerInfo(server_name);
			if (serverInfo != null)
			{
				return (int)serverInfo["resource_id"];
			}
			return 0;
		}

		public static void GameServerInfo(string server_name, out string address, out short port, out string password)
		{
			port = 0;
			address = "";
			password = "";
			DataRow serverInfo = GetServerInfo(server_name);
			if (serverInfo != null)
			{
				address = (string)serverInfo["game_server_ip"];
				port = (short)serverInfo["game_port"];
				password = "xxzx!@#123";
			}
		}
	}
}
