using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TCPServer.Server.Backup
{
    public class BackupCode
    {
        //string text = Console.ReadLine();
        //if (text == "close"){ mySocket.Close(); }
        //byte[] myBytes = Encoding.ASCII.GetBytes(text);
        //mySocket.Send(myBytes, myBytes.Length, 0);

        public BackupCode()
        {
            string y = File.ReadAllText("myfilename.txt");
            Console.WriteLine(y);
        }
    }
}
