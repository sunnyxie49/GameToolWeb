using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class BillDac : BaseDac
	{
		private string connectionString;

		public BillDac()
		{
			connectionString = billing;
		}

		public int UpdatePaidItemForAddHour(int sid, int addHour)
		{
			string commandText = "smp_Update_Paiditem";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@sid", sid),
				new SqlParameter("@addHour", addHour)
			};
			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, commandText, commandParameters);
		}

		public DataSet SelectPaidItemList(int account_id, int character_id, string server_name)
		{
			string commandText = "select sid, item_code, confirmed, confirmed_time, bought_time, valid_time, taken_time, item_count, rest_item_count\r\n                from PaidItem with (nolock)\r\n                where taken_account_id=@account_id and isCancel=0\r\n                order by bought_time desc, sid desc";
			SqlParameter[] commandParameters = new SqlParameter[3]
			{
				new SqlParameter("@account_id", account_id),
				new SqlParameter("@character_id", character_id),
				new SqlParameter("@server_name", server_name)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
		}
	}
}
