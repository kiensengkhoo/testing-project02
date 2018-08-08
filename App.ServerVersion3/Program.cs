﻿using App.ServerVersion3.Models;
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

            string[] Peers = { "18.212.41.155", "54.225.33.58" };
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
