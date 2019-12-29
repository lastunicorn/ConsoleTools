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

using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.Menues;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Commands
{
    internal class ValueReadWithDefaultValueCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            RunExample();
        }

        private static void RunExample()
        {
            ValueView<int> numberView = new ValueView<int>("Number ({0}):");
            numberView.AcceptDefaultValue = true;
            numberView.DefaultValue = 42;

            CustomConsole.WriteLine("Just hit enter. The default value, 42, is returned by the ValueView control.");
            CustomConsole.WriteLine();

            int number = numberView.Read();

            CustomConsole.WriteLine();
            CustomConsole.WriteLine("You selected {0}.", number);
        }
    }
}