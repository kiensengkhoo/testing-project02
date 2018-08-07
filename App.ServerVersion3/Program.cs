using App.ServerVersion3.Services;
using System;
using System.Net.Sockets;

namespace App.ServerVersion3
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPServer server = new TCPServer();
            server.AcceptPeersAsync();
        }
    }
}
