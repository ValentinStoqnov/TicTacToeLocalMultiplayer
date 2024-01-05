using System;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace TicTacToe.WPFPages
{
    public class PacketReader
    {
        int OperationFlag;
        int MessageLength;
        byte[] MessageDataBytes;

        public PacketReader(byte[] BytesToRead)
        {
            MemoryStream ms = new MemoryStream(BytesToRead);
            OperationFlag = ms.ReadByte();
            byte[] messageLengthBuffer = new byte[4];
            ms.Read(messageLengthBuffer, 0, 4);
            MessageLength = BitConverter.ToInt32(messageLengthBuffer, 0);
            MessageDataBytes = new byte[MessageLength];
            ms.Read(MessageDataBytes, 0, MessageLength);
            
            
        }
        public OperationFlags GetOperationFlag()
        {
            switch (OperationFlag)
            {
                case 1:
                    return OperationFlags.MessageString;
                case 2:
                    return OperationFlags.Actions;
                default:
                    return OperationFlags.Null;

            }
        }
        public string GetMessageString()
        {
            Debug.WriteLine(Encoding.UTF8.GetString(MessageDataBytes, 0, MessageLength));
            return Encoding.UTF8.GetString(MessageDataBytes, 0, MessageLength);
        }
    }
}
