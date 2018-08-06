using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TCPServer.Server.Service;

namespace TCPServer.Server
{
    public class ServerService
    {
        public Socket mySocket;
        public FileService fileService;
        public static int connections = 0;
        int i;
        byte[] bytes = new byte[1024];

        public ServerService(Socket mySocket,FileService fileService)
        {
            this.mySocket = mySocket;
            this.fileService = fileService;
        }

        public void GetService()
        {
            if (mySocket.Connected)
            {
                if (mySocket != null)
                {
                    connections++;
                }
                Console.WriteLine("New Client Connection:{0} connected.", connections);
                while ((i = mySocket.Receive(bytes)) != 0)
                {
                    Console.WriteLine("TCP Listener success...");
                    int dataLength;
                    byte[] myBufferBytes = new byte[1000];
                    dataLength = mySocket.Receive(myBufferBytes);
                    string receiveMessage = Encoding.ASCII.GetString(myBufferBytes, 0, dataLength);
                    Console.WriteLine(receiveMessage + "\n");
                    Thread.Sleep(2000);
                    fileService.FileAppend(receiveMessage);
                    Console.WriteLine("[" + DateTime.Now.ToString() + "]Data append success.");
                }
                connections--;
                Console.WriteLine("Client Closed:{0} connected.", connections);
            }
            else
            {
                Console.WriteLine("Connection disconnect...");
                mySocket.Close();
            }
        }
    }
}
