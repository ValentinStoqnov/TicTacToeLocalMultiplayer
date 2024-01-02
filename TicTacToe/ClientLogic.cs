using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TicTacToe
{
    public class ClientLogic
    {
        public ClientLogic()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress hostIp = host.AddressList[1];
            IPEndPoint hostEndPoint = new IPEndPoint(hostIp, 12500);

            Socket hostSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            hostSocket.Connect(hostEndPoint);
            if (hostSocket.Connected)
            {
                Debug.WriteLine("WE HAVE CONNECTION");
            }

        }
    }
}
