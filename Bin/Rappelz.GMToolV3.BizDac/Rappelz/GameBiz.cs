using System;
using System.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class GameBiz : BaseBiz
	{
		public DataTable Biz_Server_Info()
		{
			using GameDac gameDac = new GameDac();
			return gameDac.Dac_Server_Info();
		}

		public int Biz_Dac_GetCharacterListForCnt(string GConn, string vcAccount, string nvcName, DateTime dtStart, DateTime dtEnd, int nJob)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.Dac_GetCharacterListForCnt(GConn, vcAccount, nvcName, dtStart, dtEnd, nJob);
		}

		public DataSet GetDungeonDataInfo(string connString, string server, int gmt)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectDungeonDataInfo(connString, server, gmt);
		}

		public DataSet GetDungeonRaidInfo(string connString, string server, int gmt)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectDungeonRaidInfo(connString, server, gmt);
		}

		public DataSet GetDungeonRaidInfoV2(string connString, string server, int dungeon_id, int gmt)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectDungeonRaidInfoV2(connString, server, dungeon_id, gmt);
		}

		public DataSet GetItemInfoByCode(string connString, int sid, int code)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectItemInfoByCode(connString, sid, code);
		}

		public int GetCheatGiveItemBySid(string connString, int sid, int code, int character_id)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectCheatGiveItemBySid(connString, sid, code, character_id);
		}

		public DataSet GetServerItemList(string connString, int code)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectServerItemList(connString, code);
		}

		public DataSet GetMapInfo(string connString)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectMapInfo(connString);
		}

		public DataSet GetMapInfoByPK(string connString)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectMapInfoByPK(connString);
		}

		public DataSet GetMapInfoByPKAll(string connString)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectMapInfoByPKAll(connString);
		}

		public DataSet GetMapInfoByCharacter(string connString, string name)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectMapInfoByCharacter(connString, name);
		}

		public DataSet GetMapInfoByGuild(string connString, string name)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectMapInfoByGuild(connString, name);
		}

		public DataSet GetMapInfoByParty(string connString, string name)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectMapInfoByParty(connString, name);
		}

		public DataSet GetMapInfoByGuildAll(string connString, string name)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectMapInfoByGuildAll(connString, name);
		}

		public DataSet GetPartyInfo(string connString, int sid)
		{
			using GameDac gameDac = new GameDac();
			return gameDac.SelectPartyInfo(connString, sid);
		}
	}
}
