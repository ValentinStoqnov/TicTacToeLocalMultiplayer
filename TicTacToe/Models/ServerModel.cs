using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class ServerModel
    {
		private string name = "";
        private string ipAddress = "";

		public ServerModel(string ipAddress, string serverName)
		{
			ServerIp = ipAddress;
			ServerName = serverName;
		}

		public string ServerName
		{
			get { return name; }
			set { name = value; }
		}
		
		public string ServerIp
		{
			get { return ipAddress; }
			set { ipAddress = value; }
		}


	}
}
