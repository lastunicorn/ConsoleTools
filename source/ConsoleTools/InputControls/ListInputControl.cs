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
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.ConsoleTools.InputControls
{
    public class ListInputControl
    {
        public string Separator { get; set; } = ":";
        public int ItemsIndentation { get; set; } = 1;
        public string Bullet { get; set; } = "-";
        public int BulletSpace { get; set; } = 1;

        public List<string> Read(string label)
        {
            CustomConsole.WriteEmphasies(label);
            CustomConsole.WriteLineEmphasies(Separator);

            List<string> values = new List<string>();

            string leftpart = BuildItemLeftPart();

            while (true)
            {
                int cursorLeft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;

                CustomConsole.Write(leftpart);
                string value = Console.ReadLine();

                if (string.IsNullOrEmpty(value))
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    string emptyText = new string(' ', leftpart.Length);
                    Console.Write(emptyText);
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    break;
                }

                values.Add(value);
            }

            return values;
        }

        private string BuildItemLeftPart()
        {
            StringBuilder sb = new StringBuilder();

            if (ItemsIndentation > 0)
            {
                string indentation = new string(' ', ItemsIndentation);
                sb.Append(indentation);
            }

            if (Bullet != null)
            {
                sb.Append(Bullet);

                if (BulletSpace > 0)
                {
                    string bulletSpace = new string(' ', BulletSpace);
                    sb.Append(bulletSpace);
                }
            }

            return sb.ToString();
        }
    }
}