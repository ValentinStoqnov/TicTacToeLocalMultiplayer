using System;
using System.IO;
using System.Text;

namespace TicTacToe
{
    public class PacketSender
    {
        MemoryStream memoryStream;
        private PacketSender(OperationFlags operationFlags)
        {
            memoryStream = new MemoryStream();
            WriteOperationFlag(operationFlags);
        }
        public PacketSender(OperationFlags operationFlags, string messageToSend) : this(operationFlags)
        {
            WriteString(messageToSend);
        }
        public PacketSender(OperationFlags operationFlags,RequestTypes request) : this(operationFlags)
        {
            WriteRequest(request);
        }
        public PacketSender(OperationFlags operationFlags, GameActions gameAction) : this(operationFlags)
        {
            WriteGameAction(gameAction);
        }
        private void WriteOperationFlag(OperationFlags operationFlags)
        {
            memoryStream.WriteByte((byte)operationFlags);
        }
        private void WriteString(string msg)
        {
            memoryStream.Write(BitConverter.GetBytes(msg.Length));
            memoryStream.Write(Encoding.UTF8.GetBytes(msg));
        }
        private void WriteRequest(RequestTypes request)
        {
            memoryStream.WriteByte((byte)request);
        }
        private void WriteGameAction(GameActions gameAction)
        {
            memoryStream.WriteByte((byte)gameAction);
        }
        public byte[] GetPacketBytes()
        {
            return memoryStream.ToArray();
        }
    }
}
