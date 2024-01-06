﻿using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.WPFPages;
using System.Collections.ObjectModel;
using TicTacToe.Models;

namespace TicTacToe
{
    public class ClientLogic
    {
        string _playerName;
        IPEndPoint joinedServerEndpoint;
        Socket gameSocket;
        
        public ClientLogic(string playerName)
        {
            _playerName = playerName;
        }

        private List<IPEndPoint> GetActiveTcpServerListeners()
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

        public async Task<ObservableCollection<ServerModel>> RequestServerNames()
        {
            ObservableCollection<ServerModel> serverCollection = new ObservableCollection<ServerModel>();
            List<IPEndPoint> serverList = GetActiveTcpServerListeners();
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            foreach (IPEndPoint e in serverList)
            {
                try
                {
                    clientSocket.Connect(e);
                }
                catch
                {
                    Debug.WriteLine($"{e.Address} Server is unreachable");
                }
                finally
                {
                    if (clientSocket.Connected == true)
                    {
                        PacketSender packetSender = new PacketSender(OperationFlags.Request, RequestTypes.ServerName);
                        await clientSocket.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
                        byte[] buffer = new byte[1024];
                        await clientSocket.ReceiveAsync(buffer, SocketFlags.None);
                        PacketReader packetReader = new PacketReader(buffer);
                        if (packetReader.GetOperationFlag() == OperationFlags.MessageString)
                        {
                            ServerModel server = new(e.Address.ToString(), packetReader.GetMessageString());
                            serverCollection.Add(server);
                        }
                        clientSocket.Close();
                    }
                }
            }
            return serverCollection;
        }

        public async Task<bool> JoinServer(ServerModel server)
        {
            Socket joinedSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = IPAddress.Parse(server.ServerIp);
            joinedServerEndpoint = new(iPAddress, 12500);
            bool canStartGame = false;
            try
            {
                joinedSocket.Connect(joinedServerEndpoint);
            }
            catch
            {
                Debug.WriteLine($"{joinedServerEndpoint.Address} Server is unreachable");
            }
            finally
            {
                if (joinedSocket.Connected == true)
                {
                    PacketSender packetSender = new PacketSender(OperationFlags.Request, RequestTypes.StartGame);
                    await joinedSocket.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
                    byte[] buffer = new byte[1024];
                    await joinedSocket.ReceiveAsync(buffer, SocketFlags.None);
                    PacketReader packetReader = new PacketReader(buffer);
                    if (packetReader.GetOperationFlag() == OperationFlags.Request)
                        if(packetReader.GetRequestType() == RequestTypes.StartGame)
                            canStartGame = true;
                    joinedSocket.Close();
                }
            }
            return canStartGame;
        }

        public async void StartGame()
        {
            Socket GameSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            GameSocket.Connect(joinedServerEndpoint);
            gameSocket = GameSocket;
            
        }
        public async void SendButtonClicked(int buttonTag)
        {
            GameActions actionToSend = (GameActions)buttonTag;
            PacketSender packetSender = new PacketSender(OperationFlags.GameAction, actionToSend); // placeholder
            await gameSocket.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
        }
    }
}

