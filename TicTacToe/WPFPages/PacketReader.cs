using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace TicTacToe.WPFPages
{
    public class PacketReader
    {
        MemoryStream ms;
        public PacketReader(byte[] BytesToRead)
        {
            ms = new MemoryStream(BytesToRead); 
        }
        public OperationFlags GetOperationFlag()
        {
            int operationFlagInt = ms.ReadByte();
            OperationFlags operationFlag = (OperationFlags)operationFlagInt;
            return operationFlag;
        }
        public string GetMessageString()
        {
            byte[] messageLengthBuffer = new byte[4];
            ms.Read(messageLengthBuffer, 0, 4);
            int MessageLength = BitConverter.ToInt32(messageLengthBuffer, 0);
            byte[] MessageDataBytes = new byte[MessageLength];
            ms.Read(MessageDataBytes, 0, MessageLength);
            return Encoding.UTF8.GetString(MessageDataBytes, 0, MessageLength);
        }
        public RequestTypes GetRequestType()
        {
            int type = ms.ReadByte();
            RequestTypes RequestTypes = (RequestTypes)type;
            return RequestTypes;
        }
        public GameActions GetGameAction()
        {
            int action = ms.ReadByte();
            GameActions GameActions = (GameActions)action;
            return GameActions;
        }
        public PlayerMark GetPlayerMark()
        {
            int mark = ms.ReadByte();
            PlayerMark PlayerMark = (PlayerMark)mark;
            return PlayerMark;
        }
    }
}
