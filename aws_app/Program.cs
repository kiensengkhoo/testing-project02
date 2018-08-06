using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TCPServer.Server.Service;

namespace TCPServer.Server
{
    class Program
    {
        private static readonly string[] Peers = { "18.212.199.32", "18.212.57.215" };

        static void Main(string[] args)
        {
            FileService fileService = new FileService();

            TcpListener myTcpListener = new TcpListener(IPAddress.Any, 8080);
            myTcpListener.Start();

            Thread thread01 = new Thread(()=> {
                Console.WriteLine("TCP Listener is starting...");
                while (true)
                {
                    Socket mySocket = myTcpListener.AcceptSocket();
                    ServerService service = new ServerService(mySocket, fileService);
                    Thread newthread = new Thread(new ThreadStart(service.GetService));
                    newthread.Start();
                }
            });

            Thread thread02 = new Thread(()=> {
                for (int i = 0; i < Peers.Length; i++)
                {
                    bool connected = false;
                    while (connected == false)
                    {
                        TcpClient myTcpClient = new TcpClient();
                        try
                        {
                            myTcpClient.Connect(Peers[i], 8080);
                            Console.WriteLine("[" + Peers[i] + "]Connect Success...\n");
                            connected = true;
                            byte[] myBytes = Encoding.ASCII.GetBytes("This is Testing");
                            NetworkStream myStream = myTcpClient.GetStream();
                            myStream.Write(myBytes, 0, myBytes.Length);
                            //ClientService clientService = new ClientService(myTcpClient);
                        }
                        catch
                        {
                            Console.WriteLine("Server {0} port {1} cant connect...", Peers[i], 8080);
                            connected = false;
                        }
                        Thread.Sleep(3000);
                    }
                }
            });
            thread01.Start();
            thread02.Start();
        }
        
    }
}
