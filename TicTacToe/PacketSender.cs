using System;
using System.IO;
using System.Text;

namespace TicTacToe
{
    public class PacketSender
    {
        MemoryStream memoryStream;
        public PacketSender()
        {
            memoryStream = new MemoryStream();
        }
        public void WriteOperationFlag(OperationFlags operationFlags)
        {
            memoryStream.WriteByte((byte)operationFlags);
        }
        public void WriteString(string msg)
        {
            memoryStream.Write(BitConverter.GetBytes(msg.Length));
            memoryStream.Write(Encoding.UTF8.GetBytes(msg));
        }
        public byte[] GetPacketBytes()
        {
            return memoryStream.ToArray();
        }
    }
}
