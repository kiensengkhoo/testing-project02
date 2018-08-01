using System;

namespace aws_app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input:");
            while(true)
            {
                string value = Console.ReadLine();
                Console.WriteLine("Input value:" + value);
            }
        }
    }
}
