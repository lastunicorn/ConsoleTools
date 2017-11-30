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
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.Musical;

namespace ConsoleTools.Demo.Musical
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayGreeting();

            CustomConsole.WriteLine("The sound is playing.");
            PlayGreetingSound();
            CustomConsole.WriteLine("The sound played.");

            CustomConsole.Pause();
        }

        private static void PlayGreetingSound()
        {
            Sound.Play(MusicalNote.C4, 150);
            Sound.Play(MusicalNote.D4, 150);
            Sound.Play(MusicalNote.E4, 150);
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
