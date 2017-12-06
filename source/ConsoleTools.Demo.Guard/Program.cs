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
using DustInTheWind.ConsoleTools.Guard;

namespace DustInTheWind.ConsoleTools.Demo.Guard
{
    internal class Program
    {
        private static Guardian guardian;

        private static int Main()
        {
            DisplayGreeting();

            try
            {
                // Ensure that the application is started only once on the current machine.
                guardian = new Guardian("Alez", GuardLevel.Machine);
                
                CustomConsole.WriteLineSuccess("The application was successfully started.");
                CustomConsole.WriteLine();
                CustomConsole.WriteLine("But this application cannot be started twice.");
                CustomConsole.WriteLine("Leave this instance running and try starting another one.");

                CustomConsole.Pause();
            }
            catch (ApplicationException)
            {
                CustomConsole.WriteLineError("Another instace of this application is already running.");
                CustomConsole.WriteLineError("Current instace will shutdown.");
                CustomConsole.Pause();
                return 2;
            }
            catch (Exception ex)
            {
                CustomConsole.WriteLine(string.Format("Error instantiating the guardian instance. {0}", ex.Message), ConsoleColor.Red);
                CustomConsole.Pause();
                return 1;
            }

            return 0;
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
