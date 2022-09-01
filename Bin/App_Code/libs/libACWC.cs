using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Admin_Channel;

namespace libs
{
    public class libACWC : Password
    {
		public static bool Connnect(Socket socket, string server_password)
		{
			try
			{
				Receive(socket);
				socket.Send(Encoding.Default.GetBytes(server_password + "\r\n"));
				Receive(socket);
				return socket.Connected;
			}
			catch (Exception)
			{
				socket.Close();
				return false;
			}
		}

		public static void Disconnect(Socket socket)
		{
			try
			{
				Action(socket, "quit");
				Receive(socket);
				socket.Disconnect(reuseSocket: false);
				socket.Close();
			}
			catch (Exception)
			{
			}
		}

		public static bool Action(Socket socket, string sendstring, out string result_msg)
		{
			bool flag = false;
			new StringBuilder(2048);
			try
			{
				socket.Send(Encoding.Default.GetBytes(sendstring + "\r\n"), SocketFlags.None);
				result_msg = Receive(socket);
				return true;
			}
			catch (Exception ex)
			{
				result_msg = "例外 : " + ex.Message;
				return false;
			}
		}

		public static string Action(Socket socket, string sendstring)
		{
			string text = "";
			new StringBuilder(2048);
			try
			{
				socket.Send(Encoding.Default.GetBytes(sendstring + "\r\n"), SocketFlags.None);
				return Receive(socket);
			}
			catch (Exception ex)
			{
				return "例外 : " + ex.Message;
			}
		}

		public static string Receive(Socket socket)
		{
			byte[] array = new byte[2048];
			if (socket.Connected)
			{
				StringBuilder stringBuilder = new StringBuilder(2048);
				try
				{
					do
					{
						int num = socket.Receive(array);
						if (num != 0)
						{
							stringBuilder.Append(Encoding.Default.GetString(array, 0, num));
							continue;
						}
						break;
					}
					while (stringBuilder.ToString().Length > 0 && !(stringBuilder.ToString().Substring(stringBuilder.ToString().Length - 1, 1) == "\0"));
				}
				catch (Exception)
				{
				}
				return stringBuilder.ToString();
			}
			return "";
		}

		public static bool Execute(string server_ip, object server_port, string password, string command, out object o_result)
		{
			/*if (server_ip == "program")
			{
				server_ip = "192.168.0.29";
			}*/

			IPAddress address = IPAddress.Parse(server_ip);
			IPEndPoint remoteEP = new IPEndPoint(address, int.Parse(server_port.ToString()));
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(remoteEP);
			o_result = null;
			if (Connnect(socket, Password.ADMIN_CHANNEL_PASSWORD))
			{
				if (Action(socket, command, out var _))
				{
					Disconnect(socket);
					return true;
				}
				Disconnect(socket);
				return false;
			}
			return false;
		}

		public static string ExecuteReturn(string server_ip, object server_port, string password, string command, out object o_result)
		{
			/*if (server_ip == "program")
			{
				server_ip = "192.168.0.29";
			}*/
			IPAddress address = IPAddress.Parse(server_ip);
			IPEndPoint remoteEP = new IPEndPoint(address, int.Parse(server_port.ToString()));
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(remoteEP);
			o_result = null;
			if (Connnect(socket, Password.ADMIN_CHANNEL_PASSWORD))
			{
				if (Action(socket, command, out var result_msg))
				{
					Disconnect(socket);
					return result_msg;
				}
				Disconnect(socket);
				return "";
			}
			return "";
		}
	}
}
