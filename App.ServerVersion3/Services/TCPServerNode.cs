using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.ServerVersion3.Services
{
    public class TCPServerNode
    {
        int port = 8080;
        private TcpListener listener;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public TCPServerNode()
        {
            this.listener = new TcpListener(IPAddress.Any, port);
            this.listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            try
            {
                listener.Start();
            }
            catch (SocketException ex) {
                Console.WriteLine("Socket listener Error:" + ex.Message);
            }
        }
        
        public static int connections = 0;

        public async void AcceptPeersAsync()
        {
            Console.WriteLine("[Server] Waiting client connect...");
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                Socket socket;
                try
                {
                    socket = await listener.AcceptSocketAsync();
                    if (socket != null)
                    {
                        connections++;
                    }
                    Console.WriteLine("[Server] New Client Connection:{0} connected.", connections);

                    byte[] myBytes = Encoding.ASCII.GetBytes("[Server]Success to connect...");
                    socket.Send(myBytes);

                    TCPRemote remote = new TCPRemote(socket);
                    Thread thread = new Thread(remote.ReceiveMessage);
                    thread.Start();
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (SocketException)
                {
                    continue;
                }
            }
        }

    }
}
