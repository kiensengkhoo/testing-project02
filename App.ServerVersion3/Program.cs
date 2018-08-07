using App.ServerVersion3.Models;
using App.ServerVersion3.Services;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace App.ServerVersion3
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] Peers = { "34.227.178.142", "34.227.149.106" };
            foreach(var item in Peers)
            {
                Task.Run(() =>
                {
                    TCPClientNode client = new TCPClientNode();
                    client.ConnectPeersAsync(item);
                });
            }

            Task.Run(()=> {
                TCPServerNode server = new TCPServerNode();
                server.AcceptPeersAsync();
            });

            while(true)
            {
                Thread.Sleep(10000);
                Console.WriteLine("[" + DateTime.Now.ToString() + "] DateTime Now...");
            }
        }
    }
}
