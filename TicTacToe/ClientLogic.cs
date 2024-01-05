using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.WPFPages;
using System.Collections.ObjectModel;

namespace TicTacToe
{
    public class ClientLogic
    {
        public ClientLogic()
        {


        }

        private static List<IPEndPoint> GetActiveTcpServerListeners()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = properties.GetActiveTcpListeners();
            List<IPEndPoint> listeningServersList = new List<IPEndPoint>();

            foreach (IPEndPoint e in endPoints)
            {
                if (e.Port == 12500)
                    listeningServersList.Add(e);
            }
            return listeningServersList;
        }
        public async static Task<ObservableCollection<string>> GetAllServerNames()
        {
            ObservableCollection<string> serverNames = new ObservableCollection<string>();
            List<IPEndPoint> serverList = GetActiveTcpServerListeners();
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            foreach (IPEndPoint e in serverList)
            {
                try
                {
                     clientSocket.Connect(e); ////////////////////////////// needs to be async
                }
                catch
                {
                    Debug.WriteLine($"{e.Address} ServerUnreachable");
                }
                finally
                {
                    if (clientSocket.Connected == true)
                    {
                        byte[] buffer = new byte[1024];
                        clientSocket.Receive(buffer, SocketFlags.None); ///////////////////////////////////needs to be async
                        PacketReader packetReader = new PacketReader(buffer);
                        serverNames.Add(packetReader.GetMessageString());
                    }
                }
            }
            return serverNames;
        }
    }
}

