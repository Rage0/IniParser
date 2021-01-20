using System;

namespace Sund_Box
{
    class Program
    {
        static void Main(string[] args)
        {
            iniParser test = new iniParser();

            test.fileInfo();
            test.Read();
        }
    } 
}
