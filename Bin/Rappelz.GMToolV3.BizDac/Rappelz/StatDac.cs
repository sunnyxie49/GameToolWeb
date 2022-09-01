using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class StatDac : BaseDac
	{
		private string connectionString;

		public StatDac()
		{
			connectionString = stat;
		}

		public DataSet SelectCharacterInfoListGroupByAccount()
		{
			string commandText = "\r\n\t\t\t\tselect \r\n\t\t\t\t\taccount_id,\r\n\t\t\t\t\tmax(lv) as max_lv,\r\n\t\t\t\t\tavg(lv) as avg_lv,\r\n\t\t\t\t\tcount(*) as character_cnt,\r\n\t\t\t\t\tmax(play_time) as max_play_time,\r\n\t\t\t\t\tsum(convert(bigint, play_time)) as total_play_time,\r\n\t\t\t\t\tmin(create_time) as min_create_time,\r\n\t\t\t\t\tmax(login_time) as last_login_time\r\n\t\t\t\tfrom tb_character_info wtih (nolock)\r\n\t\t\t\tgroup by account_id\r\n\t\t\t\torder by max_lv desc";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
		}

		public DataSet SelectCharacterInfoListGroupByServer()
		{
			string commandText = "\r\n\t\t\t\tselect \r\n\t\t\t\t\tserver,\r\n\t\t\t\t\taccount_id,\r\n\t\t\t\t\tmax(lv) as max_lv,\r\n\t\t\t\t\tavg(lv) as avg_lv,\r\n\t\t\t\t\tcount(*) as character_cnt,\r\n\t\t\t\t\tmax(play_time) as max_play_time,\r\n\t\t\t\t\tsum(convert(bigint, play_time)) as total_play_time,\r\n\t\t\t\t\tmin(create_time) as min_create_time,\r\n\t\t\t\t\tmax(login_time) as last_login_time\r\n\t\t\t\tfrom tb_character_info wtih (nolock)\r\n\t\t\t\tgroup by server, account_id\r\n\t\t\t\torder by server, max_lv desc";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
		}

		public DataSet SelectGameCUInfo(DateTime start_date, DateTime end_date)
		{
			string commandText = "gmtool_GetMap_useGuildall";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@start_date", start_date),
				new SqlParameter("@end_date", start_date.AddDays(1.0).AddSeconds(-1.0))
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectGameGoldInfoByMonth(int year, int month)
		{
			string commandText = "select a.*\r\n\t\t\t\tfrom tb_log_gold as a with (nolock)\r\n\t\t\t\tjoin (select max(DatePart(hh,log_date)) maxdate, convert(varchar(10), log_date, 120) as log_date2\r\n                from tb_log_gold with (nolock)\r\n                group by convert(varchar(10), log_date, 120)) b\r\n\t\t\t\ton convert(varchar(10), log_date, 120) = log_date2\r\n\t\t\t\twhere YEAR(log_date) = @year and MONTH(log_date) = @month and convert(varchar(2), log_date, 108) = b.maxdate\r\n\t\t\t\torder by server, log_date";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@year", year),
				new SqlParameter("@month", month)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
		}

		public DataSet SelectGameGoldInfoByDay(int year, int month, int day)
		{
			string commandText = "select *\r\n                from tb_log_gold with (nolock)\r\n                where DatePart(year,log_date) = @year and DatePart(month,log_date) = @month and DatePart(day,log_date) = @day\r\n                order by server, log_date";
			SqlParameter[] commandParameters = new SqlParameter[3]
			{
				new SqlParameter("@year", year),
				new SqlParameter("@month", month),
				new SqlParameter("@day", month)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
		}

		public DataSet SelectGameJobInfoByServer(string server)
		{
			string commandText = "\r\n\t\t\t\tselect job, count(*) as cnt\r\n\t\t\t\tfrom tb_character_info with (nolock)\r\n\t\t\t\twhere server = @server and job >= 100 and login_time > (GetDate() - 30)\r\n\t\t\t\tgroup by job\r\n\t\t\t\torder by cnt desc\r\n\t\t\t\tselect job, count(*) as cnt\r\n\t\t\t\tfrom tb_character_info with (nolock)\r\n\t\t\t\twhere server = @server and job >= 100\r\n\t\t\t\tgroup by job\r\n\t\t\t\torder by cnt desc";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@server", server)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
		}

		public DataSet SelectGameJobInfoByJob(string server, int job)
		{
			string commandText = "\r\n\t\t\t    select a.job, a.lv, a.cnt as total_cnt, IsNull(b.cnt, 0) as recent_play_cnt\r\n\t\t\t    from (\r\n\t\t\t        select job, lv, count(*) as cnt\r\n\t\t\t        from tb_character_info with (nolock)\r\n\t\t\t        where server = @server and job = @job\r\n\t\t\t        group by job, lv\r\n\t\t\t    ) as a \r\n\t\t\t    left join (\r\n\t\t\t        select job, lv, count(*) as cnt\r\n\t\t\t        from tb_character_info with (nolock)\r\n\t\t\t        where server = @server and job = @job and login_time > (GetDate() - 30)\r\n\t\t\t        group by job, lv\r\n\t\t\t    ) as b\r\n\t\t\t    on a.lv = b.lv\r\n\t\t\t    order by a.lv";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@server", server),
				new SqlParameter("@job", job)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
		}

		public DataSet SelectGameLevelInfo(string connString)
		{
			string commandText = "gmtool_GetLvCnt";
			return SqlHelper.ExecuteDataset(connString, CommandType.StoredProcedure, commandText);
		}

		public DataSet SelectGameRUInfo(int level)
		{
			string commandText = "\r\n\t\t\t    select\r\n\t\t\t\t\tsubstring(create_time, 1, 7) as create_time,\r\n\t\t\t\t\tsum(cnt) as cnt,\r\n\t\t\t\t\tavg(cnt) as average_cnt,\r\n\t\t\t\t\tsum(lv_cnt) as lv_cnt,\r\n\t\t\t\t\tavg(lv_cnt) as lv_average_cnt\r\n\t\t\t\tfrom\r\n\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taa.create_time,\r\n\t\t\t\t\t\tIsNull(aa.cnt, 0) as cnt,\r\n\t\t\t\t\t\tIsNull(bb.cnt, 0) as lv_cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) \r\n\t\t\t\t\tgroup by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as aa\r\n\t\t\t\t\tleft join\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect\r\n\t\t\t\t\t\tconvert(varchar(10), create_time, 120) as create_time,\r\n\t\t\t\t\t\tcount(*) as cnt\r\n\t\t\t\t\tfrom\r\n\t\t\t\t\t(\r\n\t\t\t\t\tselect \r\n\t\t\t\t\t\taccount_id, \r\n\t\t\t\t\t\tmin(create_time) as create_time,\r\n\t\t\t\t\t\tmax(lv) as lv \r\n\t\t\t\t\tfrom dbo.tb_character_info with (nolock) group by account_id\r\n\t\t\t\t\t) as a\r\n\t\t\t\t\twhere lv >= @lv\r\n\t\t\t\t\tgroup by convert(varchar(10), create_time, 120)\r\n\t\t\t\t\t) as bb\r\n\t\t\t\t\ton aa.create_time = bb.create_time\r\n\t\t\t\t) as cc\r\n\t\t\t\tgroup by  substring(create_time, 1, 7)\r\n\t\t\t\torder by substring(create_time, 1, 7)";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@lv", level)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
		}

		public DataSet SelectSummonEnhanceInfoList(string codeArray, string server)
		{
			string commandText = "gmtool_get_summon_enhance_list";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@codeArray", codeArray),
				new SqlParameter("@server", server)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectGameUVInfo(int year, int month)
		{
			string commandText = "gmtool_game_uv";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@y", year),
				new SqlParameter("@m", month)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectGameMCUInfo(int year, int month)
		{
			string commandText = "gmtool_game_mcu";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@y", year),
				new SqlParameter("@m", month)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectGameRUInfo(string create_time)
		{
			string commandText = "select create_time, cnt\r\n                from Rappelz_game_log.dbo.tb_game_ru with (nolock)\r\n                where create_time like @create_time\r\n                order by create_time";
			SqlParameter[] commandParameters = new SqlParameter[1]
			{
				new SqlParameter("@create_time", create_time)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, commandText, commandParameters);
		}
	}
}
