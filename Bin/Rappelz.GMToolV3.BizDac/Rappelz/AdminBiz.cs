using System.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class AdminBiz : BaseBiz
	{
		public DataSet Biz_Admin_Login(string vcAccount)
		{
            using AdminDac adminDac = new AdminDac();
			return adminDac.Dac_Admin_Login(vcAccount);
		}

		public DataSet Biz_GetAdminID(int ngmnick_id)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.Dac_GetAdminID(ngmnick_id);
		}

		public DataSet Biz_GetAdminAuthority()
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.Dac_GetAdminAuthority();
		}

		public DataSet GetAdminTeamInfo()
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.SelectAdminTeamInfo();
		}

		public DataSet GetAdminGradeInfo()
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.SelectAdminGradeInfo();
		}

		public DataSet GetAdminAccountGradeInfo()
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.SelectAdminAccountGradeInfo();
		}

		public DataSet GetAdminAccountInfo(int sid)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.SelectAdminAccountInfo(sid);
		}

		public DataSet GetGameCheatLog(string server, long sid)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.SelectGameCheatLog(server, sid);
		}

		public int CountAdminAccountInfo()
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.CountAdminAccountInfo();
		}

		public DataSet GetAdminAccountInfoByPaging(int pageSize, int pageIndex)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.SelectAdminAccountInfoByPaging(pageSize, pageIndex);
		}

		public int AddAdminAccountInfo(string account, string password, string name, string team_id, string grade_id)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.InsertAdminAccountInfo(account, password, name, team_id, grade_id);
		}

		public int SetAdminAccountInfo(int sid, string name, string team_id, string grade_id)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.UpdateAdminAccountInfo(sid, name, team_id, grade_id);
		}

		public int SetAdminPassword(int sid, string password)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.UpdateAdminPassword(sid, password);
		}

		public DataSet GetAdminAuthorityTotalListV3(int admin_id, int admin_grade_id)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.SelectAdminAuthorityTotalListV3(admin_id, admin_grade_id);
		}

		public int AddAdminAuthority(int sid, int authority_code_id, int admin_id, int admin_grade_id, int authority_type)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.InsertAdminAuthority(sid, authority_code_id, admin_id, admin_grade_id, authority_type);
		}

		public int RemoveAdminAuthority(int sid)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.DeleteAdminAuthority(sid);
		}

		public int AddAdminAuthorityCode(int sid, string code, int sort_a, int sort_b, string category, int info)
		{
			using AdminDac adminDac = new AdminDac();
			return adminDac.InsertAdminAuthorityCode(sid, code, sort_a, sort_b, category, info);
		}
	}
}
