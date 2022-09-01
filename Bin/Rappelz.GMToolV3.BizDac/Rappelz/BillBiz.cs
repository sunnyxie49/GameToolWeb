using System.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class BillBiz : BaseBiz
	{
		public int SetPaidItemForAddHour(int sid, int addHour)
		{
			using BillDac billDac = new BillDac();
			return billDac.UpdatePaidItemForAddHour(sid, addHour);
		}

		public DataSet GetPaidItemList(int account_id, int character_id, string server_name)
		{
			using BillDac billDac = new BillDac();
			return billDac.SelectPaidItemList(account_id, character_id, server_name);
		}
	}
}
