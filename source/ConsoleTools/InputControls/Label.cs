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
    public class Label
    {
        public int MarginLeft { get; set; } = 0;
        public int MarginRight { get; set; } = 1;

        public string Text { get; set; }
        public string Separator { get; set; } = ":";
        public bool DisplaySeparator { get; set; } = true;

        public void Display()
        {
            if (MarginLeft > 0)
                DisplayLeftMargin();

            CustomConsole.WriteEmphasies(Text);

            if (Separator != null && DisplaySeparator)
                CustomConsole.WriteEmphasies(Separator);

            if (MarginRight > 0)
                DisplayRightMargin();
        }

        private void DisplayLeftMargin()
        {
            string space = new string(' ', MarginLeft);
            Console.Write(space);
        }

        private void DisplayRightMargin()
        {
            string space = new string(' ', MarginRight);
            Console.Write(space);
        }
    }
}