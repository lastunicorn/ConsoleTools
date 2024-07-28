// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.GuardDemo
{
    internal class DemoPackage : IDemoPackage
    {
        private static MachineLevelGuardian guardian;

        public string Name => "Guard Demo";

        public void ExecuteDemo()
        {
            DisplayApplicationHeader();

            try
            {
                // Ensure that the application is started only once on the current machine.
                guardian = new MachineLevelGuardian();

                CustomConsole.WriteLineSuccess($"A mutex with the name '{guardian.Name}' was created at machine level.");
                CustomConsole.WriteLineSuccess("The application was successfully started.");
                CustomConsole.WriteLine();
                CustomConsole.WriteLine("But this application cannot be started twice.");
                CustomConsole.WriteLine("Leave this instance running and try starting another one.");
            }
            catch (GuardianException)
            {
                CustomConsole.WriteLineError("Another instance of this application is already running.");
                CustomConsole.WriteLineError("Current instance will shutdown.");
            }
            catch (Exception ex)
            {
                CustomConsole.WriteLine(ConsoleColor.Red, $"Error instantiating the guardian instance. {ex.Message}");
            }
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader
            {
                Appendix = "Guard Demo",
                Description = "This demo shows the usage of the Guardian class."
            };
            applicationHeader.Display();
        }
    }
}