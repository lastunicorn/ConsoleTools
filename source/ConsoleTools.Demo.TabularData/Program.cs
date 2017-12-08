// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using DustInTheWind.ConsoleTools.Demo.TabularData.Flows;

namespace DustInTheWind.ConsoleTools.Demo.TabularData
{
    internal class Program
    {
        private static void Main()
        {
            DisplayGreeting();

            while (true)
            {
                IFlow flow = AskUserToChooseFlow();

                if (flow == null)
                    return;

                flow.Execute();
            }
        }

        private static IFlow AskUserToChooseFlow()
        {
            int errorCount = 0;

            while (true)
            {
                if (errorCount % 3 == 0)
                    DisplayMenu();

                try
                {
                    int selection = ReadUserSelection();
                    return CreateFlow(selection);
                }
                catch (Exception ex)
                {
                    CustomConsole.WriteLineError(ex.Message);
                    errorCount++;
                }
            }
        }

        private static IFlow CreateFlow(int selection)
        {
            switch (selection)
            {
                case 0:
                    return null;

                case 1:
                    return new LongShortTitleFlow();

                case 2:
                    return new MultilineTitleFlow();

                case 3:
                    return new MultilineCellFlow();

                case 4:
                    return new DrawLinesBetweenRowsFlow();

                case 5:
                    return new CellPaddingFlow();

                default:
                    throw new ApplicationException("Invalid choice!");
            }
        }

        private static int ReadUserSelection()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteEmphasies("Make your choice> ");

            string inputRaw = Console.ReadLine();
            CustomConsole.WriteLine();

            int input;
            bool success = int.TryParse(inputRaw, out input);

            if (!success)
                throw new ApplicationException("Invalid value. Please choose a number.");

            return input;
        }

        private static void DisplayMenu()
        {
            CustomConsole.WriteLine("1 - Long/short titles");
            CustomConsole.WriteLine("2 - Multiline titles");
            CustomConsole.WriteLine("3 - Multiline cell content");
            CustomConsole.WriteLine("4 - Draw lines between rows");
            CustomConsole.WriteLine("5 - Cell padding");
            CustomConsole.WriteLine("0 - Exit");
        }

        private static void DisplayGreeting()
        {
            string greeting = BuildGreeting();

            CustomConsole.WriteLineEmphasies(greeting);
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
        }

        private static string BuildGreeting()
        {
            TimeSpan dayTime = DateTime.Now.TimeOfDay;

            if (dayTime < TimeSpan.FromHours(5))
                return "Hello, " + Environment.UserDomainName + "! It is a beautifull night! Is'n it?";

            if (dayTime < TimeSpan.FromHours(12))
                return "Good morning, " + Environment.UserDomainName + "! I wish you a beautiful day!";

            if (dayTime < TimeSpan.FromHours(18))
                return "Good afternoon, " + Environment.UserDomainName + "!";

            if (dayTime < TimeSpan.FromHours(24))
                return "Good evening, " + Environment.UserDomainName + "!";

            return "Hello, " + Environment.UserDomainName + "!";
        }
    }
}
