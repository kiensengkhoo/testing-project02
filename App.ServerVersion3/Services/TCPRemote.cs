using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace App.ServerVersion3.Services
{
    public class TCPRemote
    {
        private Socket socket;

        public TCPRemote(Socket _socket)
        {
            this.socket = _socket;
        }

        public void ReceiveMessage()
        {
            Console.WriteLine("Start to listening...");
            byte[] bytes = new byte[1024];
            string message = string.Empty;
            int i;
            while ((i = socket.Receive(bytes)) != 0)
            {
                message = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received Data:{0}", message);

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                socket.Send(msg);
                Console.WriteLine("Send Data:{0}", message);
            }
        }

    }
}
