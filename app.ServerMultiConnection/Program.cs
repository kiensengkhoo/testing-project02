using app.ServerMultiConnection.Models;
using System;
using System.Net;

namespace app.ServerMultiConnection
{
    /// <summary>  
    /// Main函数十分简单，生成和一个Threadtcpserver实例，然后构造函数就会一步一步地  
    /// 展开，开始执行具体的业务逻辑。  
    /// </summary>  
    /// <param name="args"></param>  
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Machine IPAddress:" + IPAddress.Any.ToString());
            Threadtcpserver server = new Threadtcpserver(IPAddress.Any.ToString());
        } 
    }
}
