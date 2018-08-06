using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace aws_app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IP Address:" + IPAddress.Any);

            TcpListener myTcpListener = new TcpListener(IPAddress.Any, 8080);
            myTcpListener.Start();
            Console.WriteLine("Port 8080 waiting client connect...");
            while (true)
            {
                Socket mySocket = myTcpListener.AcceptSocket();
                Service service = new Service(mySocket);
                Thread newthread = new Thread(new ThreadStart(service.GetService));
                newthread.Start();
            }
        }

        public class Service
        {
            public Socket mySocket;

            public Service(Socket mySocket)
            {
                this.mySocket = mySocket;
            }

            public void GetService()
            {
                if (mySocket.Connected)
                {
                    while (true)
                    {
                        try
                        {
                            int dataLength;
                            Console.WriteLine("Connect Success...");
                            byte[] myBufferBytes = new byte[1000];
                            dataLength = mySocket.Receive(myBufferBytes);

                            Console.WriteLine("Received data lenght {0} \n ", dataLength.ToString());
                            Console.WriteLine("Get Input Context:");
                            Console.WriteLine(Encoding.ASCII.GetString(myBufferBytes, 0, dataLength) + "\n");
                            Console.WriteLine("Input Message:");
                            //string text = Console.ReadLine();
                            //if (text == "close")
                            //{
                            //    mySocket.Close();
                            //}
                            //byte[] myBytes = Encoding.ASCII.GetBytes(text);
                            //mySocket.Send(myBytes, myBytes.Length, 0);
                            mySocket.Send(myBufferBytes, myBufferBytes.Length,0);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            mySocket.Close();
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Connection disconnect...");
                    mySocket.Close();
                }
            }
        }
    }
}
