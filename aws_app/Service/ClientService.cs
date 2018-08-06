using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace TCPServer.Server
{
    public class ClientService
    {
        NetworkStream myNetworkStream;
        TcpClient myTcpClient;

        public ClientService(TcpClient myTcpClient)
        {
            this.myTcpClient = myTcpClient;
            while (true)
            {
                WriteData();
                ReadData();
            }
        }

        public void WriteData()
        {
            Console.WriteLine("Input Msg:");
            string strTest = Console.ReadLine();
            byte[] myBytes = Encoding.ASCII.GetBytes(strTest);

            myNetworkStream = myTcpClient.GetStream();
            myNetworkStream.Write(myBytes, 0, myBytes.Length);
        }

        public void ReadData()
        {
            Console.WriteLine("Output Msg:");
            int bufferSize = myTcpClient.ReceiveBufferSize;
            byte[] myBufferBytes = new byte[bufferSize];
            myNetworkStream.Read(myBufferBytes, 0, bufferSize);
            string message = Encoding.ASCII.GetString(myBufferBytes, 0, bufferSize).Replace(" ", "").Replace("\0", "");
            Console.WriteLine(message);
        }
    }
}
