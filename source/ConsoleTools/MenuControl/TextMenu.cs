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

        /// <summary>
        /// Gets or sets the text displayed after the menu to ask the user to choose an item.
        /// </summary>
        public string QuestionText { get; set; } = "Your choice";

        /// <summary>
        /// Gets or sets the separator text displayed after the question.
        /// Default value ":"
        /// </summary>
        public string Separator { get; set; } = ":";

        /// <summary>
        /// Gets or sets the number of spaces displayed after the question, before the user writes his choice.
        /// Default value: 1
        /// </summary>
        public int SpaceAfterQuestion { get; set; } = 1;

        /// <summary>
        /// Initialize a new instace of the <see cref="TextMenu"/> calss with
        /// the list of items to be displayed.
        /// </summary>
        /// <param name="menuItems">The list of items to be displayed by the menu.</param>
        public TextMenu(IDictionary<string, string> menuItems)
        {
            if (menuItems == null) throw new ArgumentNullException(nameof(menuItems));
            this.menuItems = new Dictionary<string, string>(menuItems);
        }

        /// <summary>
        /// Displays the menu and waits for the user to choose an item.
        /// This method blocks until the user chooses an item.
        /// </summary>
        public string Display()
        {
            DrawMenu();

            return ReadUserSelection();
        }

        private void DrawMenu()
        {
            foreach (KeyValuePair<string, string> item in menuItems)
                Console.WriteLine($"{item.Key} - {item.Value}");
        }

        private string ReadUserSelection()
        {
            while (true)
            {
                Console.WriteLine();
                DisplayQuestion();

                string inputValue = Console.ReadLine();
                Console.WriteLine();

                if (inputValue != null && menuItems.ContainsKey(inputValue))
                    return inputValue;
            }
        }

        private void DisplayQuestion()
        {
            CustomConsole.WriteEmphasies(QuestionText);
            CustomConsole.WriteEmphasies(Separator);

            if (SpaceAfterQuestion > 0)
            {
                string space = new string(' ', SpaceAfterQuestion);
                Console.Write(space);
            }
        }
    }
}