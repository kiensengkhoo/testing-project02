using App.ServerVersion3.Models;
using App.ServerVersion3.Services;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace App.ServerVersion3
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPServerNode server = new TCPServerNode();
            server.AcceptPeersAsync();

            while(true)
            {
                Thread.Sleep(5000);
                Console.WriteLine("[" + DateTime.Now.ToString() + "]");
            }
        }
    }
}
