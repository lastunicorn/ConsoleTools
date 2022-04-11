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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.SimpleTextDemo.Commands
{
    internal class AlignmentCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            CustomConsole.WriteLine(HorizontalAlignment.Left, "This is a text aligned to left.");
            CustomConsole.WriteLine(HorizontalAlignment.Left, "This is another text aligned to left.");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine(HorizontalAlignment.Center, "This is a text aligned to center.");
            CustomConsole.WriteLine(HorizontalAlignment.Center, "This is another text aligned to center.");
            CustomConsole.WriteLine();
            CustomConsole.WriteLine(HorizontalAlignment.Right, "This is a text aligned to right.");
            CustomConsole.WriteLine(HorizontalAlignment.Right, "This is another text aligned to right.");
        }
    }
}