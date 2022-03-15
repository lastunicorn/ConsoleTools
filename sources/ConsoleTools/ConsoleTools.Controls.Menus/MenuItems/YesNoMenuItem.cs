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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.ComponentModel;

namespace DustInTheWind.ConsoleTools.Controls.Menus.MenuItems
{
    /// <summary>
    /// Displays a yes/no question at the right of the menu item.
    /// If the user responds with "No", the menu item is canceled (not selected).
    /// </summary>
    public class YesNoMenuItem : LabelMenuItem
    {
        /// <summary>
        /// Gets or sets the question text to be displayed to the user.
        /// </summary>
        public string QuestionText { get; set; }

        protected override void OnBeforeSelect(CancelEventArgs e)
        {
            Location questionLocation = CalculateQuestionLocation();

            string questionText = BuildQuestionText();
            DisplayQuestion(questionLocation, questionText);

            ConsoleKeyInfo key = Console.ReadKey(true);

            ClearQuestionText(questionLocation, questionText);

            bool allow = key.Key == ConsoleKey.Y || key.Key == ConsoleKey.Enter;
            e.Cancel |= !allow;

            base.OnBeforeSelect(e);
        }

        private Location CalculateQuestionLocation()
        {
            if (Location == null)
                return new Location(Console.CursorLeft, Console.CursorTop);

            int itemLeft = Location.Value.Left;
            int itemTop = Location.Value.Top;

            return new Location(itemLeft + Size.Width + 1, itemTop);
        }

        private string BuildQuestionText()
        {
            return string.IsNullOrEmpty(QuestionText)
                ? "[Y/n]"
                : QuestionText + " [Y/n]";
        }

        private static void DisplayQuestion(Location questionLocation, string questionText)
        {
            Console.SetCursorPosition(questionLocation.Left, questionLocation.Top);
            Console.Write(questionText);
        }

        private static void ClearQuestionText(Location questionLocation, string questionText)
        {
            Console.SetCursorPosition(questionLocation.Left, questionLocation.Top);
            Console.Write(new string(' ', questionText.Length));
        }
    }
}