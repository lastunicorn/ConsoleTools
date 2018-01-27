using System;
using DustInTheWind.ConsoleTools;

namespace DustIntheWind.ConsoleTools.Tutorial
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.BufferWidth = 80;

            DisplayApplicationHeader();

            AddressBookApplication application = new AddressBookApplication();
            application.Run();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Tutorial");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
        }
    }
}