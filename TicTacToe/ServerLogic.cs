using Microsoft.Windows.Themes;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.WPFPages;

namespace TicTacToe
{
    public class ServerLogic
    {
        Socket serverSocket;
        Socket connectedSocket;
        public event EventHandler<GameActionEventArgs> ReceivedGameActionEvent;

        public ServerLogic(string serverName)
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress hostIp = host.AddressList[1];
            IPEndPoint hostEndPoint = new IPEndPoint(hostIp, 12500);

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(hostEndPoint);
            serverSocket.Listen();
        }

        public async Task<bool> StartServer(string serverName)
        {
            bool isGameStarted = false;
            while (!isGameStarted)
            {
                var handler = await serverSocket.AcceptAsync();
                if (handler.Connected)
                {
                    byte[] buffer = new byte[1024];
                    var receivedMsgInBytes = await handler.ReceiveAsync(buffer, SocketFlags.None);
                    if (receivedMsgInBytes > 0)
                    {
                        PacketReader packetReader = new PacketReader(buffer);
                        PacketSender packetSender;
                        OperationFlags operationFlag = packetReader.GetOperationFlag();
                        if (operationFlag == OperationFlags.Request)
                        {
                            RequestTypes request = packetReader.GetRequestType();
                            if (request == RequestTypes.ServerName)
                            {
                                packetSender = new PacketSender(OperationFlags.MessageString, serverName);
                                await handler.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
                            }
                            else if (request == RequestTypes.StartGame)
                            {
                                packetSender = new PacketSender(OperationFlags.Request, RequestTypes.StartGame);
                                await handler.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
                                isGameStarted = true;
                                handler.Close();
                            }
                        }
                    }
                }
            }
            return isGameStarted;
        }

        public async void StartGame(string serverName)
        {
            var handler = await serverSocket.AcceptAsync();
            connectedSocket = handler;
            if (handler.Connected)
            {
                bool isGameRuning = true;
                while (isGameRuning)
                {
                    byte[] buffer = new byte[1024];
                    var receivedMsgInBytes = await handler.ReceiveAsync(buffer, SocketFlags.None);
                    if (receivedMsgInBytes > 0)
                    {
                        PacketReader packetReader = new PacketReader(buffer);
                        PacketSender packetSender;
                        OperationFlags operationFlag = packetReader.GetOperationFlag();
                        switch (operationFlag)
                        {
                            case OperationFlags.Null:
                                break;
                            case OperationFlags.MessageString:
                                break;
                            case OperationFlags.Request:
                                RequestTypes request = packetReader.GetRequestType();
                                switch (request)
                                {
                                    case RequestTypes.ServerName:
                                        packetSender = new PacketSender(OperationFlags.MessageString, serverName);
                                        await handler.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
                                        break;
                                    case RequestTypes.StartGame:
                                        packetSender = new PacketSender(OperationFlags.Request, RequestTypes.StartGame);
                                        await handler.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case OperationFlags.GameAction:
                                GameActions receivedAction = packetReader.GetGameAction();
                                OnReceivedGameActionEvent(new GameActionEventArgs(receivedAction));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        public async void SendBackAcceptedGameAction(GameActionEventArgs e)
        {
            PacketSender packetSender = new PacketSender(OperationFlags.GameAction, e.receivedGameAction);
            await connectedSocket.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
        }
        public async void SendButtonClicked(int buttonTag)
        {
            GameActions actionToSend = (GameActions)buttonTag;
            PacketSender packetSender = new PacketSender(OperationFlags.GameAction, actionToSend); // placeholder
            await connectedSocket.SendAsync(packetSender.GetPacketBytes(), SocketFlags.None);
        }
        protected virtual void OnReceivedGameActionEvent(GameActionEventArgs e)
        {
            EventHandler<GameActionEventArgs> raiseEvent = ReceivedGameActionEvent;
            if (raiseEvent != null)
            {
                raiseEvent(this, e);
            }
        }

    }
}
