using System;
using System.IO;

namespace cs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!11");
            Console.WriteLine(args);
        }
        void reader(string filename)
        {
            String line;
            try
            {
                StreamReader fs = new StreamReader(filename);

            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
