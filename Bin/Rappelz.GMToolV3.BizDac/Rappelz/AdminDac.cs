using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace Rappelz.GMToolV3.BizDac
{
	public class AdminDac : BaseDac
	{
		private string connectionString;

		public AdminDac()
		{
			connectionString = gmtool;
		}

		public DataSet Dac_Admin_Login(string vcAccount)
		{
			string commandText = "select *, IsNull( (select name from tb_admin_team where sid = tb_admin.team_id), '') as team, IsNull((select name from tb_admin_grade where sid = tb_admin.grade_id), '') as grade from tb_admin where account = @vcAccount";
			SqlParameter[] array = new SqlParameter[1]
			{
				new SqlParameter("@vcAccount", SqlDbType.VarChar, 32)
			};
			array[0].Value = vcAccount;
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, array);
		}

		public DataSet Dac_GetAdminID(int ngmnick_id)
		{
			string commandText = "select admin_id from tb_gmnick_authority where gmnick_id=@vcgmnick_id";
			SqlParameter[] array = new SqlParameter[1]
			{
				new SqlParameter("@vcgmnick_id", SqlDbType.Int)
			};
			array[0].Value = ngmnick_id;
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, array);
		}

		public DataSet Dac_GetAdminAuthority()
		{
			string commandText = "select a.code, b.admin_id, b.admin_grade_id, authority_type from tb_authority_code_v3 a, tb_admin_authority_v3 b where a.sid = b.authority_code_id";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
		}

		public DataSet SelectAdminTeamInfo()
		{
			string commandText = "select * from tb_admin_team with (nolock)";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
		}

		public DataSet SelectAdminGradeInfo()
		{
			string commandText = "select * from tb_admin_grade with (nolock)";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
		}

		public DataSet SelectAdminAccountGradeInfo()
		{
			string commandText = "select a.*, IsNull(b.name, '') as grade_name from tb_admin a with (nolock) left join tb_admin_grade b with (nolock) on a.grade_id = b.sid";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
		}

		public DataSet SelectAdminAccountInfo(int sid)
		{
			string commandText = "select * from tb_admin with (nolock) where sid = @sid";
			SqlParameter[] array = new SqlParameter[1]
			{
				new SqlParameter("@sid", SqlDbType.Int)
			};
			array[0].Value = sid;
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, array);
		}

		public DataSet SelectGameCheatLog(string server, long sid)
		{
			string commandText = "select log_date, admin_name, cheat_type, result_msg\r\n                from dbo.tb_gmtool_game_cheat_log with (nolock)\r\n                where server=@server and item_id = @item_id\r\n                order by sid";
			SqlParameter[] commandParameters = new SqlParameter[2]
			{
				new SqlParameter("@server", server),
				new SqlParameter("@item_id", sid)
			};
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText, commandParameters);
		}

		public int CountAdminAccountInfo()
		{
			string commandText = "select count(*) cnt from tb_admin with (nolock)";
			return (int)SqlHelper.ExecuteScalar(connectionString, CommandType.Text, commandText);
		}

		public DataSet SelectAdminAccountInfoByPaging(int pageSize, int pageIndex)
		{
			if (pageSize <= 0)
			{
				pageSize = 30;
			}
			if (pageIndex <= 0)
			{
				pageIndex = 0;
			}
			string commandText = $"select top {pageSize} *, IsNull(\r\n                (select name from tb_admin_team where sid = tb_admin.team_id), '') as team\r\n                , IsNull((select name from tb_admin_grade where sid = tb_admin.grade_id), '') as grade\r\n                from tb_admin with (nolock) where sid not in (\r\n                select top {pageIndex} sid from tb_admin order by sid) order by sid";
			return SqlHelper.ExecuteDataset(connectionString, CommandType.Text, commandText);
		}

		public int InsertAdminAccountInfo(string account, string password, string name, string team_id, string grade_id)
		{
			string commandText = "sp_admin_insert";
			SqlParameter[] array = new SqlParameter[5]
			{
				new SqlParameter("@account", SqlDbType.VarChar, 20),
				new SqlParameter("@password", SqlDbType.VarChar, 20),
				new SqlParameter("@name", SqlDbType.NVarChar),
				new SqlParameter("@team_id", SqlDbType.Int),
				new SqlParameter("@grade_id", SqlDbType.Int)
			};
			array[0].Value = account;
			array[1].Value = password;
			array[2].Value = name;
			array[3].Value = team_id;
			array[4].Value = grade_id;
			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, commandText, array);
		}

		public int UpdateAdminAccountInfo(int sid, string name, string team_id, string grade_id)
		{
			string commandText = "sp_admin_update";
			SqlParameter[] array = new SqlParameter[4]
			{
				new SqlParameter("@sid", SqlDbType.Int),
				new SqlParameter("@name", SqlDbType.NVarChar, 20),
				new SqlParameter("@team_id", SqlDbType.Int),
				new SqlParameter("@grade_id", SqlDbType.Int)
			};
			array[0].Value = sid;
			array[1].Value = name;
			array[2].Value = team_id;
			array[3].Value = grade_id;
			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, commandText, array);
		}

		public int UpdateAdminPassword(int sid, string password)
		{
			string commandText = "sp_admin_password_change";
			SqlParameter[] array = new SqlParameter[2]
			{
				new SqlParameter("@sid", SqlDbType.Int),
				new SqlParameter("@password", SqlDbType.NVarChar, 20)
			};
			array[0].Value = sid;
			array[1].Value = password;
			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, commandText, array);
		}

		public DataSet SelectAdminAuthorityTotalListV3(int admin_id, int admin_grade_id)
		{
			string commandText = "sp_admin_authority_total_list_v3";
			SqlParameter[] array = new SqlParameter[2]
			{
				new SqlParameter("@admin_id", SqlDbType.Int),
				new SqlParameter("@admin_grade_id", SqlDbType.Int)
			};
			array[0].Value = admin_id;
			array[1].Value = admin_grade_id;
			return SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, commandText, array);
		}

		public int InsertAdminAuthority(int sid, int authority_code_id, int admin_id, int admin_grade_id, int authority_type)
		{
			string commandText = "insert into tb_admin_authority_v3 (authority_code_id, admin_id, admin_grade_id, authority_type) values (@authority_code_id, @admin_id, @admin_grade_id, @authority_type)";
			SqlParameter[] array = new SqlParameter[5]
			{
				new SqlParameter("@sid", SqlDbType.Int),
				new SqlParameter("@authority_code_id", SqlDbType.Int),
				new SqlParameter("@admin_id", SqlDbType.Int),
				new SqlParameter("@admin_grade_id", SqlDbType.Int),
				new SqlParameter("@authority_type", SqlDbType.Int)
			};
			array[0].Value = sid;
			array[1].Value = authority_code_id;
			array[2].Value = admin_id;
			array[3].Value = admin_grade_id;
			array[4].Value = authority_type;
			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, commandText, array);
		}

		public int DeleteAdminAuthority(int sid)
		{
			string commandText = "delete from tb_admin_authority_v3 where sid = @sid";
			SqlParameter[] array = new SqlParameter[1]
			{
				new SqlParameter("@sid", SqlDbType.Int)
			};
			array[0].Value = sid;
			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, commandText, array);
		}

		public int InsertAdminAuthorityCode(int sid, string code, int sort_a, int sort_b, string category, int info)
		{
			string commandText = "insert into dbo.tb_authority_code_v3 values (@sid, @code, @sort_a, @sort_b, @category, @info)";
			SqlParameter[] array = new SqlParameter[6]
			{
				new SqlParameter("@sid", SqlDbType.Int),
				new SqlParameter("@code", SqlDbType.VarChar),
				new SqlParameter("@sort_a", SqlDbType.Int),
				new SqlParameter("@sort_b", SqlDbType.Int),
				new SqlParameter("@category", SqlDbType.VarChar),
				new SqlParameter("@info", SqlDbType.VarChar)
			};
			array[0].Value = sid;
			array[1].Value = code;
			array[2].Value = sort_a;
			array[3].Value = sort_b;
			array[4].Value = category;
			array[5].Value = info;
			return SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, commandText, array);
		}
	}
}
