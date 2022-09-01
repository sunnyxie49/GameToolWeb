using System.Data;
using System.Transactions;

namespace Rappelz.GMToolV3.BizDac
{
	public class GameLogBiz : BaseBiz
	{
		public DataSet GetAccountList()
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectAccountList();
		}

		public DataSet GetItemList(string connString, string account_id, string character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectItemList(connString, account_id, character_id);
		}

		public DataSet GetAvatarList(string connString, int account_id, int avatar_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectAvatarList(connString, account_id, avatar_id);
		}

		public DataSet GetItemInfo(string connString, long sid)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectItemInfo(connString, sid);
		}

		public DataSet GetSummonInfo(string connString, long sid)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectSummonInfo(connString, sid);
		}

		public DataSet GetSummonInfo(string connString, string summon_name)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectSummonInfo(connString, summon_name);
		}

		public DataSet GetSummonInfo(string connString, int code, int page, int count_row)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectSummonInfo(connString, code, page, count_row);
		}

		public DataSet GetSummonInfoBySid(string connString, int summon_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectSummonInfoBySid(connString, summon_id);
		}

		public DataSet GetSummonInfoList(string connString, string codeArray)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectSummonInfoList(connString, codeArray);
		}

		public DataSet GetCharacterInfo(string connString, int account_id, int character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterInfo(connString, account_id, character_id);
		}

		public DataSet GetCharacterInfo(string connString, string server, string searchType, string searchText, bool isDeletedCharacter)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterInfo(connString, server, searchType, searchText, isDeletedCharacter);
		}

		public DataSet GetCharacterInfoList(string connString, int account_id, int character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterInfoList(connString, account_id, character_id);
		}

		public DataSet GetCharacterArenaInfo(string connString, int account_id, int character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterArenaInfo(connString, account_id, character_id);
		}

		public DataSet GetCharacterBankInfo(string connString, int account_id, int character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterBankInfo(connString, account_id, character_id);
		}

		public DataSet GetCharacterItemInfo(string connString, int account_id, int character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterItemInfo(connString, account_id, character_id);
		}

		public DataSet GetCharacterQuestInfo(string connString, int account_id, int character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterQuestInfo(connString, account_id, character_id);
		}

		public DataSet GetCharacterSkillInfo(string connString, int account_id, int character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterSkillInfo(connString, account_id, character_id);
		}

		public DataSet GetCharacterSummonInfo(string connString, int account_id, int character_id, int summon_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterSummonInfo(connString, account_id, character_id, summon_id);
		}

		public DataSet GetCharacterTitleInfo(string connString, int account_id, int character_id)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectCharacterTitleInfo(connString, account_id, character_id);
		}

		public DataSet GetSkillInfo(string connString, int sid)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectSkillInfo(connString, sid);
		}

		public DataSet GetGuildInfoList(string connString, int sid)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectGuildInfoList(connString, sid);
		}

		public DataSet GetGuildInfoSortByPoint(string connString, string guild_name)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectGuildInfoSortByPoint(connString, guild_name);
		}

		public DataSet GetGuildInfoSortByMemberCount(string connString, string guild_name)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			return gameLogDac.SelectGuildInfoSortByMemberCount(connString, guild_name);
		}

		public int SetChagneItemOwnerByAccount(string connString, int newName_id, int oldAccount_id, long sid)
		{
			int num = -9;
			using (GameLogDac gameLogDac = new GameLogDac())
			{
				using TransactionScope transactionScope = new TransactionScope();
				num = gameLogDac.UpdateChagneItemOwnerByAccount(connString, newName_id, oldAccount_id, sid);
				if (num == 0)
				{
					transactionScope.Complete();
				}
			}
			return num;
		}

		public int SetChagneItemOwnerByCharacter(string connString, int newName_id, int oldAccount_id, long sid)
		{
			int num = -9;
			using (GameLogDac gameLogDac = new GameLogDac())
			{
				using TransactionScope transactionScope = new TransactionScope();
				num = gameLogDac.UpdateChagneItemOwnerByCharacter(connString, newName_id, oldAccount_id, sid);
				if (num == 0)
				{
					transactionScope.Complete();
				}
			}
			return num;
		}

		public void BulkCopyAccountLog(DataTable table)
		{
			using GameLogDac gameLogDac = new GameLogDac();
			gameLogDac.BulkCopyAccountLog(table);
		}
	}
}
