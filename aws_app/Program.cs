using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace aws_app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IP Address:" + IPAddress.Any);
            System.Net.IPAddress theIPAddress;
            theIPAddress = System.Net.IPAddress.Parse("127.0.0.1");
            
            TcpListener myTcpListener = new TcpListener(IPAddress.Any, 8080);
            myTcpListener.Start();
            Console.WriteLine("Port 8080 waiting client connect...");
            Socket mySocket = myTcpListener.AcceptSocket();
            do
            {
                try
                {
                    if (mySocket.Connected)
                    {
                        int dataLength;
                        Console.WriteLine("Connect Success...");
                        byte[] myBufferBytes = new byte[1000];
                        dataLength = mySocket.Receive(myBufferBytes);

                        Console.WriteLine("Received data lenght {0} \n ", dataLength.ToString());
                        Console.WriteLine("Get Input Context:");
                        Console.WriteLine(Encoding.ASCII.GetString(myBufferBytes, 0, dataLength) + "\n");
                        Console.WriteLine("Input Message:");
                        string text = Console.ReadLine();
                        if(text == "close")
                        {
                            mySocket.Close();
                        }
                        byte[] myBytes = Encoding.ASCII.GetBytes(text);
                        mySocket.Send(myBytes, myBytes.Length, 0);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    mySocket.Close();
                    break;
                }

            } while (true);
        }
    }
}
