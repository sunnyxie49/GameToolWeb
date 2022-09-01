using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace libs
{
    public class acm
    {
		public static void GuildUpdate(string server_name, int guild_id)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			if (!(address == ""))
			{
				string command = $"#update_guild_info({guild_id})";
				libACWC.Execute(address, port, password, command, out var _);
			}
		}

		public static void Notice(string server_name, string notice)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = string.Format("#notice(\"{0}\")", notice.Trim().Replace("\r", "").Replace("\n", "<BR>       ")
				.Replace("\\", "\\\\")
				.Replace("\"", "\\\""));
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void Warp(string server_name, string character_name, int x, int y)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = string.Format("#warp({1}, {2}, \"{0}\")", character_name, x, y);
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void Kick(string server_name, string character_name)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = $"#save(\"{character_name}\")";
			string command2 = $"#kick(\"{character_name}\")";
			libACWC.Execute(address, port, password, command, out var o_result);
			libACWC.Execute(address, port, password, command2, out o_result);
		}

		public static void Whisper(string server_name, string character_name, string msg)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = string.Format("#whisper(\"{0}\", \"{1}\")", character_name, msg.Trim().Replace("\r", "").Replace("\n", "<BR>      ")
				.Replace("\\", "\\\\")
				.Replace("\"", "\\\""));
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void SetAutoUser(string server_name, string character_name, int auto_flag)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = $"#set_auto_user({auto_flag}, \"{character_name}\")";
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void SetAutoAccount(string server_name, string character_name, int auto_flag)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_GetAccountid_UseName";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_name", SqlDbType.NVarChar).Value = character_name;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			if (sqlDataReader.Read())
			{
				string command = string.Format("#set_auto_account({0}, {1})", auto_flag, (int)sqlDataReader["account_id"]);
				libACWC.Execute(address, port, password, command, out var _);
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			sqlDataReader.Dispose();
			sqlCommand.Dispose();
			sqlConnection.Dispose();
		}

		public static void InsertGold(string server_name, string character_name, long gold)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = string.Format("#insert_gold({1}, \"{0}\")", character_name, gold);
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void SaveCharacter(string server_name, string character_name)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = $"#save(\"{character_name}\")";
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void InsertItem(string server_name, string character_name, int code)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = string.Format("#insert_item({1}, \"{0}\")", character_name, code);
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void ChangeDungeonOwner(string server_name, int dungeon_id, int guild_id)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = $"#change_dungeon_owner({dungeon_id}, {guild_id})";
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void Destroyguild(string server_name, int guild_id)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = $"#destroy_guild({guild_id})";
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void ChangeGuildLeader(string server_name, int guild_id, int character_id)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = $"#force_promote_guild_leader({guild_id}, {character_id})";
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void ChangeGuildName(string server_name, int guild_id, string guild_name)
		{
			server.GameServerInfo(server_name, out var address, out var port, out var password);
			string command = $"#force_change_guild_name({guild_id}, \"{guild_name}\")";
			libACWC.Execute(address, port, password, command, out var _);
		}

		public static void SetPermission(string server_name, int character_id, int permission, bool all)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_set_permission";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@permission", SqlDbType.Int).Value = permission;
			sqlCommand.Parameters.Add("@all", SqlDbType.Bit).Value = all;
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}

		public static bool SetPosition(string server_name, int character_id, int x, int y, int z, int layer, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_position";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@x", SqlDbType.Int).Value = x;
			sqlCommand.Parameters.Add("@y", SqlDbType.Int).Value = y;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_NAME", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static void SetCreatureTaming(string server_name, int character_id, string character_name)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_cheat_creature_taming";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			WriteCheatLog("CREATURE_TAIMING_ALL", server_name, character_id, character_name, 0L, 0, 0, "All Creature Card - Creature is inside");
		}

		public static bool SetCharacterName(string server_name, int character_id, string new_name, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_name";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = new_name.Trim();
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_NAME", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterExp(string server_name, int character_id, long exp, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_exp";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@exp", SqlDbType.BigInt).Value = exp;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_EXP", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterJP(string server_name, int character_id, long jp, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_jp";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@jp", SqlDbType.BigInt).Value = jp;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_JP", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterGold(string server_name, int character_id, int gold, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_gold";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@gold", SqlDbType.Int).Value = gold;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_GOLD", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterGold(string server_name, int character_id, long gold, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_gold";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@gold", SqlDbType.BigInt).Value = gold;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_GOLD", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterGoldAdd(string server_name, int character_id, int gold, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_gold_add";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@gold", SqlDbType.Int).Value = gold;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_GOLD", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterGoldAdd(string server_name, int character_id, long gold, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_gold_add";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@gold", SqlDbType.BigInt).Value = gold;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_GOLD", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetBankGold(string server_name, int character_id, long gold, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_bank_gold";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@gold", SqlDbType.BigInt).Value = gold;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				long item_id = (long)sqlDataReader["item_id"];
				WriteCheatLog("BANK_GOLD", server_name, account_id, account, character_id, character_name, item_id, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterChaos(string server_name, int character_id, int chaos, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_chaos";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@chaos", SqlDbType.BigInt).Value = chaos;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_CHAOS", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterStamina(string server_name, int character_id, int stamina, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_stamina";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@stamina", SqlDbType.BigInt).Value = stamina;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_STAMINA", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterIP(string server_name, int character_id, float ip, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_ip";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@ip", SqlDbType.Real).Value = ip;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_IP", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterIP(string server_name, int character_id, decimal ip, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_ip";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@ip", SqlDbType.Decimal).Value = ip;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_IP", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterCha(string server_name, int character_id, int cha, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_cha";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@cha", SqlDbType.BigInt).Value = cha;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_CHA", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterPkc(string server_name, int character_id, int pkc, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_pkc";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@pkc", SqlDbType.BigInt).Value = pkc;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_PKC", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterDkc(string server_name, int character_id, int dkc, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_dkc";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@dkc", SqlDbType.BigInt).Value = dkc;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_DKC", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterChatBlockTime(string server_name, int character_id, int chat_block_time, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_chat_block_time";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@chat_block_time", SqlDbType.BigInt).Value = chat_block_time;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_CHAT_BLOCK", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败: 无法获得结果 ";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCharacterHuntaHolicPoint(string server_name, int character_id, int huntaholic_point, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_huntaholic_point";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@huntaholic_point", SqlDbType.Int).Value = huntaholic_point;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_HUNTAHOLICK_POINT", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败: 无法获得结果 ";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool InsertItem(string server_name, int character_id, int item_code, int enhance, int level, int cnt, int endurance, int remain_time, int flag, out string result_msg, int ethereal_durability)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_insert_item";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@item_code", SqlDbType.Int).Value = item_code;
			sqlCommand.Parameters.Add("@enhance", SqlDbType.Int).Value = enhance;
			sqlCommand.Parameters.Add("@level", SqlDbType.Int).Value = level;
			sqlCommand.Parameters.Add("@cnt", SqlDbType.Int).Value = cnt;
			sqlCommand.Parameters.Add("@endurance", SqlDbType.Int).Value = endurance;
			sqlCommand.Parameters.Add("@remain_time", SqlDbType.Int).Value = remain_time;
			sqlCommand.Parameters.Add("@flag", SqlDbType.Int).Value = flag;
			sqlCommand.Parameters.Add("@ethereal_durability", SqlDbType.Int).Value = ethereal_durability;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				long item_id = (long)sqlDataReader["item_id"];
				WriteCheatLog("ITEM_INSERT", server_name, account_id, account, character_id, character_name, item_id, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool EditItem(string server_name, long item_id, int enhance, int level, long cnt, bool taiming, int socket_0, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_edit_item";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = item_id;
			sqlCommand.Parameters.Add("@enhance", SqlDbType.Int).Value = enhance;
			sqlCommand.Parameters.Add("@level", SqlDbType.Int).Value = level;
			sqlCommand.Parameters.Add("@cnt", SqlDbType.BigInt).Value = cnt;
			sqlCommand.Parameters.Add("@taiming", SqlDbType.Bit).Value = taiming;
			sqlCommand.Parameters.Add("@socket_0", SqlDbType.Int).Value = socket_0;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				object obj = null;
				obj = sqlDataReader["account_id"];
				int account_id = (int)((obj == DBNull.Value) ? ((object)0) : obj);
				obj = sqlDataReader["account"];
				string account = (string)((obj == DBNull.Value) ? string.Empty : obj);
				obj = sqlDataReader["character_id"];
				int character_id = (int)((obj == DBNull.Value) ? ((object)0) : obj);
				obj = sqlDataReader["character_name"];
				string character_name = (string)((obj == DBNull.Value) ? string.Empty : obj);
				WriteCheatLog("ITEM_EDIT", server_name, account_id, account, character_id, character_name, item_id, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool EditItem(string server_name, long item_id, int enhance, int level, long cnt, bool taiming, int remain_time, int socket_0, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_edit_item2";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = item_id;
			sqlCommand.Parameters.Add("@enhance", SqlDbType.Int).Value = enhance;
			sqlCommand.Parameters.Add("@level", SqlDbType.Int).Value = level;
			sqlCommand.Parameters.Add("@cnt", SqlDbType.BigInt).Value = cnt;
			sqlCommand.Parameters.Add("@taiming", SqlDbType.Bit).Value = taiming;
			sqlCommand.Parameters.Add("@remain_time", SqlDbType.Int).Value = remain_time;
			sqlCommand.Parameters.Add("@socket_0", SqlDbType.Int).Value = socket_0;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = string.Format("{0}", sqlDataReader["account"]);
				int character_id = (int)sqlDataReader["character_id"];
				string character_name = string.Format("{0}", sqlDataReader["character_name"]);
				WriteCheatLog("ITEM_EDIT", server_name, account_id, account, character_id, character_name, item_id, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool EditSummon(string server_name, long item_id, int enhance, bool inItialization, int SummonCode, int OldSummonEnhance, out string result_msg)
		{
			bool flag = false;
			result_msg = "";
			GetSummonEnhanceInfo(enhance, SummonCode, OldSummonEnhance, out var card_durability, out var jp_addition, out var new_code, out var old_jp_addition);
			if (GetSummonEnhanceInfo(enhance, SummonCode, OldSummonEnhance, out card_durability, out jp_addition, out new_code, out old_jp_addition))
			{
				SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlConnection.Open();
				if (inItialization)
				{
					sqlCommand.CommandText = "gmtool_cheat_edit_summon";
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = item_id;
					sqlCommand.Parameters.Add("@enhance", SqlDbType.Int).Value = enhance;
					sqlCommand.Parameters.Add("@card_durability", SqlDbType.Int).Value = card_durability;
					sqlCommand.Parameters.Add("@jp_addition", SqlDbType.Int).Value = jp_addition;
					sqlCommand.Parameters.Add("@new_code", SqlDbType.Int).Value = new_code;
				}
				else
				{
					sqlCommand.CommandText = "gmtool_cheat_edit_summon2";
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = item_id;
					sqlCommand.Parameters.Add("@enhance", SqlDbType.Int).Value = enhance;
					sqlCommand.Parameters.Add("@card_durability", SqlDbType.Int).Value = card_durability;
					sqlCommand.Parameters.Add("@jp_addition", SqlDbType.Int).Value = jp_addition - old_jp_addition;
					sqlCommand.Parameters.Add("@new_code", SqlDbType.Int).Value = new_code;
				}
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.Read())
				{
					result_msg = (string)sqlDataReader["result_msg"];
					int account_id = (int)sqlDataReader["account_id"];
					string account = string.Format("{0}", sqlDataReader["account"]);
					int character_id = (int)sqlDataReader["character_id"];
					string character_name = string.Format("{0}", sqlDataReader["character_name"]);
					WriteCheatLog("SUMMON_EDIT", server_name, account_id, account, character_id, character_name, item_id, 0, 0, 0, 0, 0, result_msg);
					flag = true;
				}
				else
				{
					flag = false;
					result_msg = "失败";
				}
				sqlDataReader.Close();
				sqlConnection.Close();
			}
			else
			{
				flag = false;
				result_msg = "资源故障";
			}
			return flag;
		}

		public static bool GetSummonEnhanceInfo(int enhance, int old_code, int OldSummonEnhance, out int card_durability, out int jp_addition, out int new_code, out int old_jp_addition)
		{
			card_durability = 0;
			jp_addition = 0;
			new_code = 0;
			old_jp_addition = 0;
			SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["resource0"].ConnectionString);
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "dbo.gmtool_get_CreatureEnhance";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@enhance", SqlDbType.Int).Value = enhance;
			sqlCommand.Parameters.Add("@old_enhance", SqlDbType.Int).Value = OldSummonEnhance;
			sqlCommand.Parameters.Add("@code", SqlDbType.Int).Value = old_code;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				card_durability = (int)sqlDataReader["card_durability"];
				jp_addition = (int)sqlDataReader["jp_addition"];
				new_code = (int)sqlDataReader["code"];
				old_jp_addition = (int)sqlDataReader["old_jp_addition"];
				result = true;
			}
			else
			{
				result = false;
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool DeleteItem(string server_name, long item_id, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_del_item";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = item_id;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				object obj = null;
				obj = sqlDataReader["account_id"];
				int account_id = (int)((obj == DBNull.Value) ? ((object)0) : obj);
				obj = sqlDataReader["account"];
				string account = (string)((obj == DBNull.Value) ? string.Empty : obj);
				obj = sqlDataReader["character_id"];
				int character_id = (int)((obj == DBNull.Value) ? ((object)0) : obj);
				obj = sqlDataReader["character_name"];
				string character_name = (string)((obj == DBNull.Value) ? string.Empty : obj);
				WriteCheatLog("ITEM_DEL", server_name, account_id, account, character_id, character_name, item_id, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool InsertItemToBank(string server_name, int account_id, int item_code, int enhance, int level, int cnt, int endurance, out string result_msg, int ethereal_durability)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_insert_item";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
			sqlCommand.Parameters.Add("@item_code", SqlDbType.Int).Value = item_code;
			sqlCommand.Parameters.Add("@enhance", SqlDbType.Int).Value = enhance;
			sqlCommand.Parameters.Add("@level", SqlDbType.Int).Value = level;
			sqlCommand.Parameters.Add("@cnt", SqlDbType.Int).Value = cnt;
			sqlCommand.Parameters.Add("@endurance", SqlDbType.Int).Value = endurance;
			sqlCommand.Parameters.Add("@ethereal_durability", SqlDbType.Int).Value = ethereal_durability;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				string account = (string)sqlDataReader["account"];
				long item_id = (long)sqlDataReader["item_id"];
				WriteCheatLog("ITEM_INSERT", server_name, account_id, account, 0, "", item_id, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool InsertSkill(string server_name, int character_id, int summon_id, int skill_code, int level, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_insert_skill";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@summon_id", SqlDbType.Int).Value = summon_id;
			sqlCommand.Parameters.Add("@skill_code", SqlDbType.Int).Value = skill_code;
			sqlCommand.Parameters.Add("@level", SqlDbType.Int).Value = level;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int skill_id = (int)sqlDataReader["skill_id"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("SKILL_INSERT", server_name, account_id, account, character_id, character_name, 0L, skill_id, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool EditSkill(string server_name, int skill_id, int level, int cool_time, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_edit_skill";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = skill_id;
			sqlCommand.Parameters.Add("@level", SqlDbType.Int).Value = level;
			sqlCommand.Parameters.Add("@cool_time", SqlDbType.Int).Value = cool_time;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				int character_id = (int)sqlDataReader["character_id"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("SKILL_EDIT", server_name, account_id, account, character_id, character_name, 0L, skill_id, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool DeleteSkill(string server_name, int skill_id, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_del_skill";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@sid", SqlDbType.Int).Value = skill_id;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				int character_id = (int)sqlDataReader["character_id"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("SKILL_DEL", server_name, account_id, account, character_id, character_name, 0L, skill_id, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCreatureName(string server_name, int summon_id, string summon_name, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_creature_name";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@summon_id", SqlDbType.Int).Value = summon_id;
			sqlCommand.Parameters.Add("@summon_name", SqlDbType.NVarChar).Value = summon_name;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				int character_id = (int)sqlDataReader["character_id"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CREATURE_EDIT", server_name, account_id, account, character_id, character_name, 0L, 0, summon_id, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetCreatureExp(string server_name, int summon_id, long exp, int jp, int sp, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_creature_exp";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@summon_id", SqlDbType.Int).Value = summon_id;
			sqlCommand.Parameters.Add("@exp", SqlDbType.BigInt).Value = exp;
			sqlCommand.Parameters.Add("@jp", SqlDbType.Int).Value = jp;
			sqlCommand.Parameters.Add("@sp", SqlDbType.Int).Value = sp;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				int character_id = (int)sqlDataReader["character_id"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CREATURE_EDIT", server_name, account_id, account, character_id, character_name, 0L, 0, summon_id, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool RestoreDeletedCharacter(string server_name, int character_id, string name)
		{
			bool result = false;
			try
			{
				name = name.Split(' ')[0].Replace("@", "");
				SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlConnection.Open();
				sqlCommand.CommandText = "gmtool_GetAccount_useSid";
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.Clear();
				sqlCommand.Parameters.Add("@sid", SqlDbType.Int).Value = character_id;
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				int account_id = 0;
				string text = "";
				if (sqlDataReader.Read())
				{
					account_id = (int)sqlDataReader["account_id"];
					text = (string)sqlDataReader["account"];
				}
				sqlDataReader.Close();
				if (text != "")
				{
					sqlCommand.CommandText = "gmtool_GetCnt_useName";
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.Parameters.Clear();
					sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = name.Trim();
					int result2 = 0;
					int.TryParse(sqlCommand.ExecuteScalar().ToString(), out result2);
					if (result2 == 0)
					{
						sqlCommand.CommandText = "gmtool_v2_cheat_character_restore";
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.Parameters.Clear();
						sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = name.Trim();
						sqlCommand.Parameters.Add("@delete_time", SqlDbType.DateTime).Value = new DateTime(9999, 12, 31);
						sqlCommand.Parameters.Add("@sid", SqlDbType.Int).Value = character_id;
						common.MsgBox($"name : {name.Trim()}, account : {text}, charcter_id : {character_id}");
						int result3 = 0;
						object obj = sqlCommand.ExecuteScalar();
						if (obj != null)
						{
							int.TryParse(obj.ToString(), out result3);
						}
						if (result3 > 0)
						{
							result = true;
							WriteCheatLog("CharacterRestore", server_name, account_id, text, character_id, name.Trim(), 0L, 0, 0, 0, 0, 0, "character restored");
						}
						else
						{
							WriteCheatLog("CharacterRestore_False", server_name, account_id, text, character_id, name.Trim(), 0L, 0, 0, 0, 0, 0, "character restored_false _" + result3);
						}
					}
					else
					{
						result = false;
						WriteCheatLog("CharacterRestore_False", server_name, account_id, text, character_id, name.Trim(), 0L, 0, 0, 0, 0, 0, "character restored_false _Cnt");
					}
				}
				sqlConnection.Close();
				return result;
			}
			catch (Exception ex)
			{
				common.MsgBox(ex.Message);
				return result;
			}
		}

		public static bool SetCreaturedSid(string server_name, long item_id, int summon_id, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_creature_Changed";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@sid", SqlDbType.BigInt).Value = item_id;
			sqlCommand.Parameters.Add("@summon_id", SqlDbType.Int).Value = summon_id;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				int character_id = (int)sqlDataReader["character_id"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CREATURRECARD_EDIT", server_name, account_id, account, character_id, character_name, item_id, 0, summon_id, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static void WriteCheatLog(string cheat_type, string server_name, int character_id, string character_name, long item_id, int skill_id, int summon_id, string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["gmtool"].ConnectionString);
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_write_cheat_log";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@cheat_type", SqlDbType.VarChar, 20).Value = cheat_type;
			sqlCommand.Parameters.Add("@admin_id", SqlDbType.Int).Value = admin.GetAdminID();
			sqlCommand.Parameters.Add("@admin_name", SqlDbType.NVarChar).Value = admin.GetAdminName();
			sqlCommand.Parameters.Add("@server", SqlDbType.VarChar, 20).Value = server_name;
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@character_name", SqlDbType.NVarChar).Value = character_name;
			sqlCommand.Parameters.Add("@item_id", SqlDbType.BigInt).Value = item_id;
			sqlCommand.Parameters.Add("@skill_id", SqlDbType.Int).Value = skill_id;
			sqlCommand.Parameters.Add("@summon_id", SqlDbType.Int).Value = summon_id;
			sqlCommand.Parameters.Add("@result_msg", SqlDbType.VarChar, 100).Value = result_msg;
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}

		public static void WriteCheatLog(string cheat_type, string server_name, int account_id, string account, int character_id, string character_name, long item_id, int skill_id, int summon_id, int quest_id, int guild_id, int party_id, string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["gmtool"].ConnectionString);
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_write_cheat_log";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@cheat_type", SqlDbType.VarChar, 20).Value = cheat_type;
			sqlCommand.Parameters.Add("@admin_id", SqlDbType.Int).Value = admin.GetAdminID();
			sqlCommand.Parameters.Add("@admin_name", SqlDbType.NVarChar).Value = admin.GetAdminName();
			sqlCommand.Parameters.Add("@server", SqlDbType.VarChar, 20).Value = server_name;
			sqlCommand.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
			sqlCommand.Parameters.Add("@account", SqlDbType.VarChar, 50).Value = account;
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@character_name", SqlDbType.NVarChar).Value = character_name;
			sqlCommand.Parameters.Add("@item_id", SqlDbType.BigInt).Value = item_id;
			sqlCommand.Parameters.Add("@skill_id", SqlDbType.Int).Value = skill_id;
			sqlCommand.Parameters.Add("@summon_id", SqlDbType.Int).Value = summon_id;
			sqlCommand.Parameters.Add("@quest_id", SqlDbType.Int).Value = quest_id;
			sqlCommand.Parameters.Add("@guild_id", SqlDbType.Int).Value = guild_id;
			sqlCommand.Parameters.Add("@party_id", SqlDbType.Int).Value = party_id;
			sqlCommand.Parameters.Add("@result_msg", SqlDbType.VarChar, 255).Value = result_msg;
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}

		public static bool ChangeGuildIcon(string server_name, int guild_id, string icon, int icon_size, out string result_msg)
		{
			bool result = false;
			result_msg = "";
			try
			{
				SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlConnection.Open();
				sqlCommand.CommandText = "gmtool_v2_set_guild_icon";
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.Clear();
				sqlCommand.Parameters.Add("@guild_id", SqlDbType.Int).Value = guild_id;
				sqlCommand.Parameters.Add("@icon", SqlDbType.VarChar).Value = icon;
				sqlCommand.Parameters.Add("@icon_size", SqlDbType.Int).Value = icon_size;
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
				result = true;
				GuildUpdate(server_name, guild_id);
				WriteCheatLog("ChangeGuildIcon", server_name, 0, "", 0L, 0, 0, $"guild_id : {guild_id}");
			}
			catch (Exception ex)
			{
				result_msg = ex.Message;
			}
			result_msg = "";
			return result;
		}

		public static bool DeleteGuildIcon(string server_name, int guild_id, out string result_msg)
		{
			bool result = false;
			result_msg = "";
			try
			{
				SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlConnection.Open();
				sqlCommand.CommandText = "gmtool_v2_set_guild_icon";
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.Clear();
				sqlCommand.Parameters.Add("@guild_id", SqlDbType.Int).Value = guild_id;
				sqlCommand.Parameters.Add("@icon", SqlDbType.Int).Value = 0;
				sqlCommand.Parameters.Add("@icon_size", SqlDbType.NVarChar).Value = "";
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
				result = true;
				GuildUpdate(server_name, guild_id);
				WriteCheatLog("DeleteGuildIcon", server_name, 0, "", 0L, 0, 0, $"guild_id : {guild_id}");
			}
			catch (Exception ex)
			{
				result_msg = ex.Message;
			}
			result_msg = "";
			return result;
		}

		public static bool ChangeGuildName(string server_name, int guild_id, string guild_name_before, string guild_name, out string result_msg)
		{
			bool result = false;
			result_msg = "";
			try
			{
				SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlConnection.Open();
				sqlCommand.CommandText = "gmtool_v2_set_guild_name";
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.Clear();
				sqlCommand.Parameters.Add("@guild_id", SqlDbType.Int).Value = guild_id;
				sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = guild_name;
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
				result = true;
				GuildUpdate(server_name, guild_id);
				WriteCheatLog("ChangeGuildName", server_name, 0, "", 0L, 0, 0, $"guild_id : {guild_id}, name : {guild_name_before} -> {guild_name}");
			}
			catch (Exception ex)
			{
				result_msg = ex.Message;
			}
			result_msg = "";
			return result;
		}

		public static bool ChangeGuildLeader(string server_name, int guild_id, int leader_id_before, int leader_id, out string result_msg)
		{
			bool result = false;
			result_msg = "";
			try
			{
				SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlConnection.Open();
				sqlCommand.CommandText = "\r\n\t\t\t\t\t\tdeclare @cnt int;\r\n\t\t\t\t\t\tselect @cnt = count(*) from Character with (nolock) where sid = @leader_id and guild_id = @guild_id\r\n\t\t\t\t\t\tif @cnt < 0\r\n\t\t\t\t\t\t\tupdate Guild set leader_id= @name where sid = @guild_id;\r\n\t\t\t\t\t";
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.Parameters.Clear();
				sqlCommand.Parameters.Add("@guild_id", SqlDbType.Int).Value = guild_id;
				sqlCommand.Parameters.Add("@leader_id", SqlDbType.NVarChar).Value = leader_id;
				int num = sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
				if (num > 0)
				{
					result = true;
					GuildUpdate(server_name, guild_id);
					WriteCheatLog("ChangeGuildLeader", server_name, 0, "", 0L, 0, 0, $"guild_id : {guild_id}, leader: {leader_id_before} -> {leader_id}");
				}
			}
			catch (Exception ex)
			{
				result_msg = ex.Message;
			}
			result_msg = "";
			return result;
		}

		public static void SetSecurityNumber(string server_name, int account_id)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_SetSecurityNumber_Clear";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@account_id", SqlDbType.Int).Value = account_id;
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			sqlConnection.Dispose();
			sqlCommand.Dispose();
		}

		public static void RemoveFromDenyList(string ip)
		{
			string text = string.Format("{0}", ConfigurationManager.AppSettings["auth_ip"]);
			if (!(text == ""))
			{
				short num = short.Parse(ConfigurationManager.AppSettings["auth_port"]);
				string password = ConfigurationManager.AppSettings["auth_password"];
				string command = $"remove_from_deny_list {ip}";
				libACWC.Execute(text, num, password, command, out var _);
			}
		}

		public static void AddToDenyList(string ip)
		{
			string text = string.Format("{0}", ConfigurationManager.AppSettings["auth_ip"]);
			if (!(text == ""))
			{
				short num = short.Parse(ConfigurationManager.AppSettings["auth_port"]);
				string password = ConfigurationManager.AppSettings["auth_password"];
				string command = $"add_to_deny_list {ip}";
				libACWC.Execute(text, num, password, command, out var _);
			}
		}

		public static void AddToAllowList(string ip)
		{
			string text = string.Format("{0}", ConfigurationManager.AppSettings["auth_ip"]);
			if (!(text == ""))
			{
				short num = short.Parse(ConfigurationManager.AppSettings["auth_port"]);
				string password = ConfigurationManager.AppSettings["auth_password"];
				string command = $"add_to_allow_list {ip}";
				libACWC.Execute(text, num, password, command, out var _);
			}
		}

		public static void RemoveFromAllowList(string ip)
		{
			string text = string.Format("{0}", ConfigurationManager.AppSettings["auth_ip"]);
			if (!(text == ""))
			{
				short num = short.Parse(ConfigurationManager.AppSettings["auth_port"]);
				string password = ConfigurationManager.AppSettings["auth_password"];
				string command = $"remove_from_allow_list {ip}";
				libACWC.Execute(text, num, password, command, out var _);
			}
		}

		public static bool SetEtherealStoneDurability(string server_name, int character_id, int durability, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_set_ethereal_stone_durability";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@durability", SqlDbType.Int).Value = durability;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_ethereal_stone_durability_POINT", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败：无法获得结果 ";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetArenaPoint(string server_name, int character_id, int ap, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_ap";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@ap", SqlDbType.Int).Value = ap;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_ARENA_POINT", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败：无法获得结果 ";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}

		public static bool SetTalentPoint(string server_name, int character_id, int tp, out string result_msg)
		{
			SqlConnection sqlConnection = new SqlConnection(server.GetConnectionString(server_name));
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = sqlConnection;
			sqlConnection.Open();
			sqlCommand.CommandText = "gmtool_v2_cheat_character_tp";
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.Parameters.Clear();
			sqlCommand.Parameters.Add("@character_id", SqlDbType.Int).Value = character_id;
			sqlCommand.Parameters.Add("@tp", SqlDbType.Int).Value = tp;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			bool result;
			if (sqlDataReader.Read())
			{
				result = (bool)sqlDataReader["result"];
				result_msg = (string)sqlDataReader["result_msg"];
				int account_id = (int)sqlDataReader["account_id"];
				string account = (string)sqlDataReader["account"];
				string character_name = (string)sqlDataReader["character_name"];
				WriteCheatLog("CHARACTER_TALENT_POINT", server_name, account_id, account, character_id, character_name, 0L, 0, 0, 0, 0, 0, result_msg);
			}
			else
			{
				result = false;
				result_msg = "失败：无法获得结果 ";
			}
			sqlDataReader.Close();
			sqlConnection.Close();
			return result;
		}
	}
}
