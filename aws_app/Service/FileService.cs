using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TCPServer.Server.Service
{
    public class FileService
    {
        public FileService()
        {
            if (File.Exists("blockchain.txt"))
            {
                Console.WriteLine("File is Exist.");
            }
            else
            {
                Console.WriteLine("File does not exist.");
                string text = "Blockchain record is starting here...";
                File.WriteAllText("blockchain.txt", text);
                Console.WriteLine("Create a new txt file,name=blockchain.txt.");
            }
        }

        public void FileAppend(string text)
        {
            File.AppendAllText("blockchain.txt", text);
        }
    }
}
