using System;

namespace FileLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            FileLogger filelogger = new FileLogger();
            filelogger.Log("This is a test", DateTime.Today);
            Console.WriteLine("Hello World!");
        }
    }
}
