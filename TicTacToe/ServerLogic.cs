using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TicTacToe
{
    public class ServerLogic
    {
        public ServerLogic()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress hostIp = host.AddressList[1];
            IPEndPoint hostEndPoint = new IPEndPoint(hostIp, 12500);

            Socket hostSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            hostSocket.Bind(hostEndPoint);

            hostSocket.AcceptAsync();
            
        }
    }
}
