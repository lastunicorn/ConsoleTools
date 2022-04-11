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

namespace DustInTheWind.ConsoleTools.Demo.HorizontalLineDemo.Commands
{
    internal class AlignmentsCommand : CommandBase
    {
        public override string Title => "Alignment";

        protected override void DoExecute()
        {
            DisplayLeftAlignedLine();
            DisplayCenteredAlignedLine();
            DisplayRightAlignedLine();
        }

        private void DisplayLeftAlignedLine()
        {
            CustomConsole.WriteLine("Aligned Left");

            HorizontalLine horizontalLine = new HorizontalLine
            {
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            horizontalLine.Display();
        }

        private static void DisplayCenteredAlignedLine()
        {
            CustomConsole.WriteLine("Aligned Center");

            HorizontalLine horizontalLine = new HorizontalLine
            {
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            horizontalLine.Display();
        }

        private void DisplayRightAlignedLine()
        {
            CustomConsole.WriteLine("Aligned Right");

            HorizontalLine horizontalLine = new HorizontalLine
            {
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            horizontalLine.Display();
        }
    }
}