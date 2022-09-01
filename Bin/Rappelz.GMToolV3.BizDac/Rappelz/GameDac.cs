using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class GameDac : BaseDac
	{
		private string connectionString;

		public GameDac()
		{
			connectionString = gmtool;
		}

		public DataTable Dac_Server_Info()
		{
			string commandText = "select * from tb_rappelz_server with (nolock) order by idx";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText).Tables[0];
		}

		public int Dac_GetCharacterListForCnt(string GConn, string vcAccount, string nvcName, DateTime dtStart, DateTime dtEnd, int nJob)
		{
			int num = 0;
			string commandText = "";
			SqlParameter[] array = new SqlParameter[6]
			{
				new SqlParameter("@vcAccount", SqlDbType.VarChar),
				null,
				null,
				null,
				null,
				null
			};
			array[0].Value = vcAccount;
			array[1] = new SqlParameter("@nvcName", SqlDbType.NVarChar);
			array[1].Value = nvcName;
			array[2] = new SqlParameter("@dtStart", SqlDbType.DateTime);
			array[2].Value = dtStart;
			array[3] = new SqlParameter("@dtEnd", SqlDbType.DateTime);
			array[3].Value = dtEnd;
			array[4] = new SqlParameter("@nJob", SqlDbType.Int);
			array[4].Value = nJob;
			array[5] = new SqlParameter("@iReturn", SqlDbType.Int);
			array[5].Direction = ParameterDirection.Output;
			SqlHelper.ExecuteNonQuery(GConn, CommandType.StoredProcedure, commandText, array);
			return Convert.ToInt32(array[5].Value.ToString());
		}

		public DataSet SelectDungeonDataInfo(string connString, string server, int gmt)
		{
			string commandText = "gmtool_GetDungeonData_foreign";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@server", server),
				new SqlParameter("@gmt", gmt)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectDungeonRaidInfo(string connString, string server, int gmt)
		{
			string commandText = "gmtool_GetDungeonRaid_foreign";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@server", server),
				new SqlParameter("@gmt", gmt)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectDungeonRaidInfoV2(string connString, string server, int dungeon_id, int gmt)
		{
			string commandText = "gmtool_GetDungeonRaid_temp_foreign_ver2";
			SqlParameter[] commandParameters = new SqlParameter[3]
			{
				new SqlParameter("@server", server),
				new SqlParameter("@dungeon_id", dungeon_id),
				new SqlParameter("@gmt", gmt)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectItemInfoByCode(string connString, int sid, int code)
		{
			string commandText = "gmtool_v2_get_item_info_use_code";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@sid", sid),
				new SqlParameter("@code", code)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public int SelectCheatGiveItemBySid(string connString, int sid, int code, int character_id)
		{
			string commandText = "gmtool_cheat_give_item_useSid";
			SqlParameter[] commandParameters = new SqlParameter[3]
			{
				new SqlParameter("@sid", sid),
				new SqlParameter("@code", code),
				new SqlParameter("@chatacter_id", character_id)
			};
			return (int)SqlHelper.ExecuteScalar(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectServerItemList(string connString, int code)
		{
			string commandText = "gmtool_GetServer_ItemList";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@code", code)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectMapInfo(string connString)
		{
			string commandText = "gmtool_GetMap_useDefault";
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText);
		}

		public DataSet SelectMapInfoByPK(string connString)
		{
			string commandText = "gmtool_GetMap_usePk";
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText);
		}

		public DataSet SelectMapInfoByPKAll(string connString)
		{
			string commandText = "gmtool_GetMap_usePk";
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText);
		}

		public DataSet SelectMapInfoByCharacter(string connString, string name)
		{
			string commandText = "gmtool_GetMap_useCharacter";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@name", name)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectMapInfoByGuild(string connString, string name)
		{
			string commandText = "gmtool_GetMap_useGuild";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@name", name)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectMapInfoByParty(string connString, string name)
		{
			string commandText = "gmtool_GetMap_useParty";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@name", name)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectMapInfoByGuildAll(string connString, string name)
		{
			string commandText = "gmtool_GetMap_useGuildall";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@name", name)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectPartyInfo(string connString, int sid)
		{
			string commandText = "gmtool_GetPartyInfo";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@sid", sid)
			};
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText, commandParameters);
		}
	}
}
