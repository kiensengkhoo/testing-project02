﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace app.ServerMultiConnection.Models
{
    class Threadtcpserver
    {

        /* 本程序中采用了多线程技术，可以应付多客户的需求。首先，程序的主线程也就是程序的入口即Main（）函数， 
         * 当执行到Accept方法时，线程变会阻塞；当有新连接时，就创建相应的消息服务线程。而主程序则继续进行监听， 
         * 检查有没有新的连接进入。如果客户端有向服务器连接的请求，那么就把连接句柄传递给接收的套接字。由于线程 
         * 的调度和切换是非常快的，快得足以让我们分辨不出程序的运行顺序，所以从宏观上来讲，可以说程序是并行执行 
         * 的。但事实上，从微观的角度上说，只是cpu飞快地调度线程，让我们感觉好像可以同时接收连接和处理消息一样， 
         * 但在一个时刻，只有一个线程是处于运行状态的。 
         */


        /// <summary>  
        /// 下面这段代码的业务逻辑是：  
        ///（1）创建套接字server，并将其与本地终结点iep进行绑定。然后，在13000端口上监听是否有新的客户端进行连接
        ///（2）在无限循环中有一个阻塞的方法Accept，该方法直到有新客户端连接到服务器上时，把  
        /// 客户端的套接字信息传递给client对象。否则，将阻塞 直到有客户机进行连接。  
        ///（3）ClientThread类负责客户端与服务器端之间的通信。先把客户端的套接字句柄传递给  
        /// 负责消息服务的ClientThread类。然后，把ClientThread类 的ClientService方法委托给线程，并启动线程。   
        /// </summary>  
        private Socket server;
        public Threadtcpserver(string ip)
        {
            //初始化IP地址  
            IPAddress local = IPAddress.Parse(ip);
            IPEndPoint iep = new IPEndPoint(local, 8080);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            //将套接字与本地终结点绑定  
            server.Bind(iep);
            //在本地13000端口号上进行监听  
            server.Listen(20);
            Console.WriteLine("waiting client to connect...");
            while (true)
            {
                //得到包含客户端信息的套接字  
                Socket client = server.Accept();
                //创建消息服务线程对象  
                ClientThread newclient = new ClientThread(client);
                //把ClientThread类的ClientService方法委托给线程  
                Thread newthread = new Thread(new ThreadStart(newclient.ClientService));
                //启动消息服务线程  
                newthread.Start();
            }
        }
        
        /// <summary>  
        /// （1）在构造函数中得到接收到的客户套接字client，以后就通过service句柄处理消息的接收  
        ///      和发送。  
        /// （2）ClientService方法是委托给线程的，此方法进行消息的处理工作。在这里实现的功能是，  
        ///       先从客户端接收一条消息，然后把这条消息转换为大写字母，并立即发送一条应答的消息，  
        ///      也就是所谓的echo技术，通常用来进行消息之间的传递。  
        /// （3）还有就是通过connections变量来记录活动的连接数。当有新增连接或断开连接的情况发   
        ///       生时，都会体现出connections的变化。  
        /// </summary>  
        public class ClientThread
        {
            //connections变量表示连接数  
            public static int connections = 0;
            public Socket service;
            int i;
            //构造函数  
            public ClientThread(Socket clientsocket)
            {
                //service对象接管对消息的控制  
                this.service = clientsocket;
            }
            public void ClientService()
            {
                String data = null;
                byte[] bytes = new byte[1024];
                //如果Socket不是空，则连接数加1  
                if (service != null)
                {
                    connections++;
                }
                Console.WriteLine("New Client Connection:{0} connected.", connections);
                while ((i = service.Receive(bytes)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received Data:{0}", data);
                    //处理客户端发来的消息，这是转化为大写字母  
                    data = data.ToUpper();
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    //发送一条应答消息  
                    service.Send(msg);
                    Console.WriteLine("Send Data:{0}", data);
                }
                //关闭套接字  
                service.Close();
                connections--;
                Console.WriteLine("Client Closed:{0} connected.", connections);
            }
            
        }
    }
}  
