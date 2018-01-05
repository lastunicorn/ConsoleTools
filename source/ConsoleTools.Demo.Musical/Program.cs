// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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

using DustInTheWind.ConsoleTools.Musical;

namespace DustInTheWind.ConsoleTools.Demo.Musical
{
    internal class Program
    {
        private static void Main()
        {
            DisplayApplicationHeader();

            CustomConsole.WriteLine("The sound is playing.");
            PlayGreetingSound();
            CustomConsole.WriteLine("The sound was stopped.");

            Pause.DisplayDefault();
        }

        private static void DisplayApplicationHeader()
        {
            CustomConsole.WriteLineEmphasies("ConsoleTools Demo - Musical");
            CustomConsole.WriteLineEmphasies("===============================================================================");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This demo shows how to make sounds in Console.");
            CustomConsole.WriteLine();
        }

        private static void PlayGreetingSound()
        {
            Sound.Play(MusicalNote.C4, 150);
            Sound.Play(MusicalNote.D4, 150);
            Sound.Play(MusicalNote.E4, 150);
        }
    }
}
