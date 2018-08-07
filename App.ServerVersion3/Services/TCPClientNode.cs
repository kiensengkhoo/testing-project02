using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.ServerVersion3.Services
{
    public class TCPClientNode
    {
        private Socket socket;
        private NetworkStream stream;
        private readonly string[] Peers = { "34.227.178.142", "34.227.149.106" };

        public TCPClientNode()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
        }

        public async Task<bool> ConnectPeers(string[] Peers)
        {
            return true;
        }

        public async Task<bool> ConnectAsync(string address)
        {
            try
            {
                await socket.ConnectAsync(address, 8080);
                stream = new NetworkStream(socket);
            }
            catch (SocketException)
            {
                socket.Dispose();
                return false;
            }
            return true;
        }

        public async Task<bool> SendMessageAsync(string Message)
        {
            byte[] myBytes = Encoding.ASCII.GetBytes(Message);

            CancellationTokenSource source = new CancellationTokenSource(30000);
            try
            {
                await stream.WriteAsync(myBytes, 0, myBytes.Length, source.Token);
                return true;
            }
            catch (ObjectDisposedException) { }
            catch (Exception ex) when (ex is IOException || ex is OperationCanceledException)
            {
                Console.WriteLine("SendMessage Error:" + ex.Message);
            }
            finally
            {
                source.Dispose();
            }
            return false;
        }

        public string ReceivedMessage(int bufferSize)
        {
            byte[] myBufferBytes = new byte[bufferSize];
            stream.Read(myBufferBytes, 0, bufferSize);
            string result = Encoding.ASCII.GetString(myBufferBytes, 0, bufferSize).Replace(" ", "").Replace("\0", "");
            return result;
        }

    }
}
