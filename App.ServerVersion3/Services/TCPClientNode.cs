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

        public TCPClientNode()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
        }

        public async void ConnectPeersAsync(string address)
        {
            Console.WriteLine("[Client] Start to connect [" + address + "]");
            bool check = await ConnectAsync(address);

            while (!check)
            {
                Console.WriteLine("[Client] Try again to connect [" + address + "]");
                check = await ConnectAsync(address);
                if (!check)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("[Try again connect after 5sec.]");
                }
                else
                {
                    Console.WriteLine("[" + DateTime.Now.ToString() + "] " + address + " Success to connect server.");
                }
            }
        }

        public async Task<bool> ConnectAsync(string address)
        {
            CancellationTokenSource source = new CancellationTokenSource(30000);
            try
            {
                await socket.ConnectAsync(address, 8080);
                stream = new NetworkStream(socket);
                string strTest = "[" + DateTime.Now.ToString() + "] " + address + " Success to connect server.";
                byte[] myBytes = Encoding.ASCII.GetBytes(strTest);
                await stream.WriteAsync(myBytes, 0, myBytes.Length, source.Token);
            }
            catch (SocketException ex)
            {
                source.Dispose();
                socket.Dispose();
                Console.WriteLine("[Client] Error Socket :" + ex.Message);
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
            string result;
            try
            {
                byte[] myBufferBytes = new byte[bufferSize];
                stream.Read(myBufferBytes, 0, bufferSize);
                result = Encoding.ASCII.GetString(myBufferBytes, 0, bufferSize).Replace(" ", "").Replace("\0", "");
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return result;
        }

    }
}
