using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.ApplicationBlocks.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class GameLogDac : BaseDac
	{
		private string connectionString;

		public GameLogDac()
		{
			connectionString = auth_log;
		}

		public DataSet SelectAccountList()
		{
			string commandText = "gmtool_get_account_list";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, commandText);
		}

		public DataSet SelectItemList(string connString, string account_id, string character_id)
		{
			string commandText = "gmtool_GetItemList_All";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectAvatarList(string connString, int account_id, int avatar_id)
		{
			string commandText = "gmtool_GetAvatarList";
			SqlParameter[] array = new SqlParameter[2]
			{
				new SqlParameter("@in_account_id", SqlDbType.Int),
				new SqlParameter("@in_name_id", SqlDbType.Int)
			};
			array[0].Value = account_id;
			array[1].Value = avatar_id;
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, array);
		}

		public DataSet SelectItemInfo(string connString, long sid)
		{
			string commandText = "gmtool_v2_get_item_info";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@sid", sid)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectSummonInfo(string connString, long sid)
		{
			string commandText = "gmtool_GetSummonInfo_2";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@sid", sid)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectSummonInfo(string connString, string summon_name)
		{
			string commandText = "gmtool_GetSummon_Search";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@summon_name", summon_name)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectSummonInfo(string connString, int code, int page, int count_row)
		{
			string commandText = $"\r\n\t\t\t    select count(*) as cnt\r\n                from Summon with (nolock)\r\n                where code between @code and @code + 3;\r\n\t\t\t    select top 1000 sid, account_id, owner_id, code, card_uid, exp, jp, last_decreased_exp, name, transform, lv, jlv, max_level, fp, prev_level_01, prev_level_02, prev_id_01, prev_id_02, sp, hp, mp\r\n                from Summon with (nolock)\r\n\t\t\t    where code between @code and @code + 3\r\n\t\t\t    and sid not in (\r\n                    select top {(page - 1) * 1000} sid\r\n                    from Summon with (nolock)\r\n                    where code between @code and @code + 3\r\n                    order by exp desc\r\n                )\r\n\t\t\t    order by exp desc\r\n\t\t\t    ";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@code", code),
				new SqlParameter("@count_row", count_row)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectSummonInfoBySid(string connString, int summon_id)
		{
			string commandText = "gmtool_GetSummon_info_useSid";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@summon_id", summon_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectSummonInfoList(string connString, string codeArray)
		{
			string commandText = "gmtool_get_summon_enhance_list";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@codeArray", codeArray)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterInfo(string connString, int account_id, int character_id)
		{
			string commandText = "gmtool_v2_get_character_info";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterInfo(string connString, string server, string searchType, string searchText, bool isDeletedCharacter)
		{
			int num = 1000;
			StringBuilder stringBuilder = new StringBuilder(2000);
			stringBuilder.AppendFormat("select top {0}\r\n\t\t\t\t@server as server, gmtool_view_check_searchType_main.sid + 2009092 as sid, gmtool_view_check_searchType_main.name, account, account_id + 2002060 as account_id,\r\n\t\t\t\tparty_id, \r\n\t\t\t--\tIsNull((select name from Party with (nolock) where sid = gmtool_view_check_searchType_main.party_id), '') as party_name,\r\n\t\t\t\tguild_id, \r\n\t\t\t\tIsNull(gmtool_view_check_searchType_Guild.name, '') as guild_name,\r\n\t\t\t\tIsNull(gmtool_view_check_searchType_Guild.icon_size, 0) as guild_icon_size,\r\n\t\t\t\tIsNull(gmtool_view_check_searchType_Guild.icon, '') as guild_icon,\r\n\t\t\t\tIsNull(gmtool_view_check_searchType_Guild.leader_id, 0) as guild_leader_id,\r\n\t\t\t\tpermission,\r\n\t\t\t--\tx, y, z, layer,\r\n\t\t\t\trace, sex, lv, exp, last_decreased_exp,\r\n\t\t\t--\thp, mp, stamina,\r\n\t\t\t\tjob, job_depth, jlv, jp, total_jp,\r\n\t\t\t--\tjob_0, job_1, job_2, jlv_0, jlv_1, jlv_2, \r\n\t\t\t\timmoral_point, cha, pkc, dkc, gold, \r\n\t\t\t--\tbank_gold,\r\n\t\t\t\tchaos,\r\n\t\t\t--\tbelt_00, belt_01, belt_02, belt_03, belt_04, belt_05, summon_0, summon_1, summon_2, summon_3, summon_4, summon_5, main_summon, sub_summon, remain_summon_time, \r\n\t\t\t\tcreate_time, delete_time, login_time, login_count, logout_time, play_time, chat_block_time, adv_chat_count, name_changed, auto_used,huntaholic_point  \r\n\t\t\t\tfrom gmtool_view_check_searchType_main with (nolock)\r\n\t\t\t\tleft join gmtool_view_check_searchType_Guild with (nolock) on gmtool_view_check_searchType_main.guild_id = gmtool_view_check_searchType_Guild.sid\r\n\t\t\t\twhere 1=1 {1}", num, isDeletedCharacter ? "" : " and gmtool_view_check_searchType_main.name not like '@%'");
			switch (searchType)
			{
			case "account_id":
				stringBuilder.Append(" and account_id = @searchNumber");
				break;
			case "account":
				stringBuilder.Append(" and account like @searchText");
				break;
			case "character_id":
				stringBuilder.Append(" and gmtool_view_check_searchType_main.sid = @searchNumber");
				break;
			case "character_name":
				stringBuilder.Append(" and gmtool_view_check_searchType_main.name like @searchText");
				break;
			case "party_id":
				stringBuilder.Append(" and party_id = @searchNumber");
				break;
			case "party_name":
				stringBuilder.Append(" and party_id in (select sid from gmtool_view_check_searchType_party with (nolock) where name like @searchText)");
				break;
			case "guild_id":
				stringBuilder.Append(" and guild_id = @searchNumber");
				break;
			case "guild_name":
				stringBuilder.Append(" and guild_id in (select sid from gmtool_view_check_searchType_guild with (nolock) where name like @searchText)");
				break;
			case "gold":
				stringBuilder.Append(" and gmtool_view_check_searchType_main.gold >= @searchNumber");
				break;
			case "item_code":
				stringBuilder.Append(" and gmtool_view_check_searchType_main.sid in (select  top 1000 owner_id from gmtool_view_check_searchType_item with (nolock) where owner_id  >  -2009092 and code = @searchNumber)");
				break;
			case "item_code_bank":
				stringBuilder.Append(" and gmtool_view_check_searchType_main.account_id in (select  top 1000 account_id from gmtool_view_check_searchType_item with (nolock) where account_id  > -2002060 and code = @searchNumber)");
				break;
			}
			if (searchType == "gold")
			{
				stringBuilder.Append(" order by gmtool_view_check_searchType_main.gold desc, exp desc, total_jp desc");
			}
			else
			{
				stringBuilder.Append(" order by exp desc, total_jp desc");
			}
			int num2 = 2002060;
			int num3 = 2009092;
			int result = 0;
			int.TryParse(searchText, out result);
			long result2 = 0L;
			long.TryParse(searchText, out result2);
			if (searchType == "account_id")
			{
				result -= num2;
			}
			else if (searchType == "character_id")
			{
				result -= num3;
			}
			string commandText = stringBuilder.ToString();
			SqlParameter[] commandParameters = new SqlParameter[4]
			{
				new SqlParameter("@server", server),
				new SqlParameter("@searchText", searchText),
				new SqlParameter("@searchNumber", result),
				new SqlParameter("@gold", result2)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.Text, commandText, commandParameters);
		}

		public DataSet SelectCharacterInfoList(string connString, int account_id, int character_id)
		{
			string commandText = "gmtool_v2_get_character_list";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterArenaInfo(string connString, int account_id, int character_id)
		{
			string commandText = "gmtool_v2_get_character_info_Arena";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterBankInfo(string connString, int account_id, int character_id)
		{
			string commandText = "gmtool_v2_get_character_info_bank";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterItemInfo(string connString, int account_id, int character_id)
		{
			string commandText = "gmtool_v2_get_character_info_item";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterQuestInfo(string connString, int account_id, int character_id)
		{
			string commandText = "gmtool_v2_get_character_info_quest";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterSkillInfo(string connString, int account_id, int character_id)
		{
			string commandText = "gmtool_v2_get_character_info_skill";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterSummonInfo(string connString, int account_id, int character_id, int summon_id)
		{
			string commandText = "gmtool_v2_get_character_info_summon";
			SqlParameter[] commandParameters = new SqlParameter[3]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id),
				new SqlParameter("@summon_id", summon_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectCharacterTitleInfo(string connString, int account_id, int character_id)
		{
			string commandText = "gmtool_v2_get_character_info_Title";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectSkillInfo(string connString, int sid)
		{
			string commandText = "gmtool_v2_get_skill_info";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@sid", sid)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectGuildInfoList(string connString, int sid)
		{
			string commandText = "gmtool_GetGuild_info";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@sid", sid)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectGuildInfoSortByPoint(string connString, string guild_name)
		{
			string commandText = "gmtool_GetGuild_orderbyPoint";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@guild_name", guild_name)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectGuildInfoSortByMemberCount(string connString, string guild_name)
		{
			string commandText = "gmtool_GetGuild_orderbyMemberCnt";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@guild_name", guild_name)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public int UpdateChagneItemOwnerByAccount(string connString, int newName_id, int oldAccount_id, long sid)
		{
			string commandText = "gmtool_ChangeItemOwner_useAccount";
			SqlParameter[] array = new SqlParameter[4]
			{
				new SqlParameter("@newname_id", newName_id),
				new SqlParameter("@oldaccount_id", oldAccount_id),
				new SqlParameter("@ItemSid", sid),
				new SqlParameter("@returnCode", SqlDbType.Int)
			};
			array[3].Direction = ParameterDirection.Output;
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, commandText, array);
			return (int)array[3].Value;
		}

		public int UpdateChagneItemOwnerByCharacter(string connString, int newName_id, int oldAccount_id, long sid)
		{
			string commandText = "gmtool_ChangeItemOwner_useCharater";
			SqlParameter[] array = new SqlParameter[4]
			{
				new SqlParameter("@newname_id", newName_id),
				new SqlParameter("@oldname_id", oldAccount_id),
				new SqlParameter("@ItemSid", sid),
				new SqlParameter("@returnCode", SqlDbType.Int)
			};
			array[3].Direction = ParameterDirection.Output;
			SqlHelper.ExecuteNonQuery(connString, CommandType.StoredProcedure, commandText, array);
			return (int)array[3].Value;
		}

		public void BulkCopyAccountLog(DataTable table)
		{
			using SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString);
			sqlBulkCopy.DestinationTableName = "tb_account_log";
			sqlBulkCopy.WriteToServer(table);
			sqlBulkCopy.Close();
		}
	}
}
