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

namespace DustInTheWind.ConsoleTools.MenuControl
{
    public class TextMenu
    {
        private readonly Dictionary<string, string> menuItems;

        public string Question { get; set; } = "Your choice: ";

        public TextMenu(IDictionary<string, string> menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));

            this.menuItems = new Dictionary<string, string>(menuItems);
        }

        public string Display()
        {
            foreach (KeyValuePair<string, string> item in menuItems)
                Console.WriteLine($"{item.Key} - {item.Value}");

            while (true)
            {
                Console.WriteLine();
                Console.Write(Question);

                string inputValue = Console.ReadLine();
                Console.WriteLine();

                if (inputValue != null && menuItems.ContainsKey(inputValue))
                    return inputValue;
            }
        }
    }
}