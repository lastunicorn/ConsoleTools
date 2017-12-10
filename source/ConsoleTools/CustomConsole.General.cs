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

namespace DustInTheWind.ConsoleTools
{
    public static partial class CustomConsole
    {
        private const ConsoleColor SuccessColor = ConsoleColor.Green;
        private const ConsoleColor WarningColor = ConsoleColor.Yellow;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;
        private const ConsoleColor EmphasiesColor = ConsoleColor.White;

        public static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a question to the user and wait for a char answer.
        /// The answer is the char associated with the first key the user presses.
        /// </summary>
        /// <param name="question">The question to be displayed to the user.</param>
        /// <returns>The answer received from the user.</returns>
        public static char QuestionChar(string question)
        {
            Console.Write(question);

            ConsoleKeyInfo key = Console.ReadKey();
            WriteLine();

            return key.KeyChar;
        }

        /// <summary>
        /// Displays a question to the user and wait for a string answer.
        /// The answer is finished with Enter.
        /// </summary>
        /// <param name="question">The question to be displayed to the user.</param>
        /// <returns>The answer received from the user.</returns>
        public static string QuestionString(string question)
        {
            Console.Write(question);
            return Console.ReadLine();
        }
    }
}