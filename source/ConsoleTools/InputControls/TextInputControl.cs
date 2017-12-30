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

namespace DustInTheWind.ConsoleTools.InputControls
{
    public class TextInputControl
    {
        public string Separator { get; set; } = ":";
        public int SpaceAfterLabel { get; set; } = 1;
        public string DefaultValue { get; set; } = string.Empty;

        public string Read(string label)
        {
            DisplayLabel(label);

            string value = Console.ReadLine();

            return string.IsNullOrEmpty(value)
                ? DefaultValue
                : value;
        }

        private void DisplayLabel(string label)
        {
            CustomConsole.WriteEmphasies(label);
            CustomConsole.WriteEmphasies(Separator);

            if (SpaceAfterLabel > 0)
            {
                string space = new string(' ', SpaceAfterLabel);
                Console.Write(space);
            }
        }
    }
}