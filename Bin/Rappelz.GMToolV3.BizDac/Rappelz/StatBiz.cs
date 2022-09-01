using System;
using System.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class StatBiz : BaseBiz
	{
		public DataSet GetCharacterInfoListGroupByAccount()
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectCharacterInfoListGroupByAccount();
		}

		public DataSet GetCharacterInfoListGroupByServer()
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectCharacterInfoListGroupByServer();
		}

		public DataSet GetGameCUInfo(DateTime start_date, DateTime end_date)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameCUInfo(start_date, end_date);
		}

		public DataSet GetGameGoldInfoByMonth(int year, int month)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameGoldInfoByMonth(year, month);
		}

		public DataSet GetGameGoldInfoByDay(int year, int month, int day)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameGoldInfoByDay(year, month, day);
		}

		public DataSet GetGameJobInfoByServer(string server)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameJobInfoByServer(server);
		}

		public DataSet GetGameJobInfoByJob(string server, int job)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameJobInfoByJob(server, job);
		}

		public DataSet GetGameLevelInfo(string connString)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameLevelInfo(connString);
		}

		public DataSet GetGameRUInfo(int level)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameRUInfo(level);
		}

		public DataSet GetSummonEnhanceInfoList(string codeArray, string server)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectSummonEnhanceInfoList(codeArray, server);
		}

		public DataSet GetGameUVInfo(int year, int month)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameUVInfo(year, month);
		}

		public DataSet GetGameMCUInfo(int year, int month)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameMCUInfo(year, month);
		}

		public DataSet GetGameRUInfo(string create_time)
		{
			using StatDac statDac = new StatDac();
			return statDac.SelectGameRUInfo(create_time);
		}
	}
}
