using System;

namespace HostBuilderConsoleSample
{
    internal class ConsolePrinter : IPrinter
    {
        public void PrintMessage(string message) => Console.WriteLine(message);
    }
}