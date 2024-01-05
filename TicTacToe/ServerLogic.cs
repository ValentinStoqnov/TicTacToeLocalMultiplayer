using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TicTacToe
{
    public class ServerLogic
    {
        public ServerLogic(string serverName)
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress hostIp = host.AddressList[1];
            IPEndPoint hostEndPoint = new IPEndPoint(hostIp, 12500);

            Socket hostSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            hostSocket.Bind(hostEndPoint);
            hostSocket.Listen();

            StartServer(hostSocket,serverName);
        }

        private async void StartServer(Socket hostSocket, string serverName) 
        {
            var handler = await hostSocket.AcceptAsync();

            PacketSender packetSender = new PacketSender();
            packetSender.WriteOperationFlag(OperationFlags.MessageString);
            packetSender.WriteString(serverName);

            await handler.SendAsync(packetSender.GetPacketBytes(),SocketFlags.None);
            Debug.WriteLine(Encoding.UTF8.GetString(packetSender.GetPacketBytes()));

        }

        private async void StartGame(Socket hostSocket)
        {
            while (true)
            {
                var handler = await hostSocket.AcceptAsync();
                byte[] buffer = new byte[1024];


                var receiverdMsgInBytes = await handler.ReceiveAsync(buffer, SocketFlags.None);
                string convertedMsgToString = Encoding.UTF8.GetString(buffer, 0, receiverdMsgInBytes);

                Debug.WriteLine(convertedMsgToString);
            }
        }
    }
}
