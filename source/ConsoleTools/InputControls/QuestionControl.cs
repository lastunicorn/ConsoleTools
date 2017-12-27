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
    public class QuestionControl
    {
        public int SpaceAfterQuestion { get; set; } = 1;

        /// <summary>
        /// Displays the question to the user and waits for a string answer.
        /// The answer is finished with Enter.
        /// </summary>
        /// <param name="defaultResponse"></param>
        /// <returns></returns>
        public string ReadString(string questionText, string defaultResponse = "")
        {
            DisplayQuestion(questionText);

            string value = Console.ReadLine();

            return string.IsNullOrEmpty(value)
                ? defaultResponse
                : value;
        }

        /// <summary> 
        /// Displays the question to the user and waits for a char answer.
        /// The answer is the char associated with the first key the user presses.
        /// </summary>
        /// <param name="defaultResponse"></param>
        /// <returns></returns>
        public char ReadChar(string questionText, char defaultResponse = '\0')
        {
            DisplayQuestion(questionText);

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.WriteLine();


            return consoleKeyInfo.Key == ConsoleKey.Enter
                ? defaultResponse
                : consoleKeyInfo.KeyChar;
        }

        public int ReadInt32(string questionText, int defaultResponse = 0)
        {
            DisplayQuestion(questionText);

            string rawValue = Console.ReadLine();
            return string.IsNullOrEmpty(rawValue)
                ? defaultResponse
                : int.Parse(rawValue);
        }

        public long ReadInt64(string questionText, long defaultResponse = 0)
        {
            DisplayQuestion(questionText);

            string rawValue = Console.ReadLine();
            return string.IsNullOrEmpty(rawValue)
                ? defaultResponse
                : long.Parse(rawValue);
        }

        private void DisplayQuestion(string questionText)
        {
            Console.Write(questionText);

            if (SpaceAfterQuestion > 0)
            {
                string space = new string(' ', SpaceAfterQuestion);
                Console.Write(space);
            }
        }
    }
}