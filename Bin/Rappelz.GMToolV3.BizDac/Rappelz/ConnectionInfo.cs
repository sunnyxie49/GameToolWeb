using System.Configuration;

namespace Rappelz.GMToolV3.BizDac
{
	public class ConnectionInfo
	{
		protected string gmtool;

		protected string auth_log;

		protected string billing;

		protected string stat;

		public ConnectionInfo()
		{
			gmtool = ConfigurationManager.ConnectionStrings["gmtool"].ToString();
			auth_log = ConfigurationManager.ConnectionStrings["auth_log"].ToString();
			billing = ConfigurationManager.ConnectionStrings["billing"].ToString();
			stat = ConfigurationManager.ConnectionStrings["statistics"].ToString();
		}
	}
}
