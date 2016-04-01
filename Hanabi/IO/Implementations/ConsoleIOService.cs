using System;

namespace Hanabi.IO.ConsoleIO
{
    public class ConsoleIOService:IOService
    {

        public override string ReadLine()
        {
            return Console.ReadLine();
        }

        public override void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}