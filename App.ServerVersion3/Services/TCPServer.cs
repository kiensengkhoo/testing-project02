using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.ServerVersion3.Services
{
    public class TCPServer
    {
        int port = 8080;
        private TcpListener listener;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public TCPServer()
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

        private int count = 1;
        public async void AcceptPeersAsync()
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                Socket socket;
                try
                {
                    Console.WriteLine("AcceptSocket Listener " + count + " :");
                    socket = await listener.AcceptSocketAsync();
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
