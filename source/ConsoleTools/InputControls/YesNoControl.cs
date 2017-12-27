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
using System.Text;

namespace DustInTheWind.ConsoleTools.InputControls
{
    public class YesNoControl
    {
        private string questionText;

        public string QuestionText
        {
            get { return questionText; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (value.Length == 0) throw new ArgumentException("Please provide a question text.", nameof(value));

                questionText = value;
            }
        }

        public int SpaceAfterQuestion { get; set; } = 1;

        public string YesText { get; set; } = "y";
        public string NoText { get; set; } = "n";
        public string CancelText { get; set; } = "esc";

        public bool CapitalizeDefaultOption { get; set; } = true;

        public ConsoleKey YesKey { get; set; } = ConsoleKey.Y;
        public ConsoleKey NoKey { get; set; } = ConsoleKey.N;
        public ConsoleKey AcceptKey { get; set; } = ConsoleKey.Enter;
        public ConsoleKey CancelKey { get; set; } = ConsoleKey.Escape;

        public bool AcceptCancel { get; set; } = false;
        public bool AcceptEscapeAsCancel { get; set; } = true;

        public YesNoAnswer? DefaultOption { get; set; } = null;

        public YesNoControl(string questionText)
        {
            if (questionText == null) throw new ArgumentNullException(nameof(questionText));
            if (questionText.Length == 0) throw new ArgumentException("Please provide a question text.", nameof(questionText));

            QuestionText = questionText;
        }

        /// <summary> 
        /// Displays the question to the user and waits for a char answer.
        /// The answer is the char associated with the first key the user presses.
        /// </summary>
        /// <returns></returns>
        public YesNoAnswer ReadAnswer()
        {
            DisplayWholeQuestion();
            return ReadAnswerInternal();
        }

        private void DisplayWholeQuestion()
        {
            Console.Write(QuestionText);
            Console.Write(" ");

            DisplayYesNoCancelText();

            if (SpaceAfterQuestion > 0)
            {
                string space = new string(' ', SpaceAfterQuestion);
                Console.Write(space);
            }
        }

        protected virtual void DisplayYesNoCancelText()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            string yesText = CapitalizeDefaultOption && DefaultOption == YesNoAnswer.Yes
                ? YesText.ToUpper()
                : YesText;
            sb.Append(yesText);

            sb.Append("/");

            string noText = CapitalizeDefaultOption && DefaultOption == YesNoAnswer.No
                ? NoText.ToUpper()
                : NoText;
            sb.Append(noText);

            if (AcceptCancel)
            {
                sb.Append("/");

                string cancelText = CapitalizeDefaultOption && DefaultOption == YesNoAnswer.Cancel
                    ? CancelText.ToUpper()
                    : CancelText;
                sb.Append(cancelText);
            }

            sb.Append("]");

            Console.Write(sb);
        }

        private YesNoAnswer ReadAnswerInternal()
        {
            while (true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                if (consoleKeyInfo.Key == AcceptKey && DefaultOption.HasValue)
                {
                    string defaultText;

                    switch (DefaultOption.Value)
                    {
                        case YesNoAnswer.Yes:
                            defaultText = YesText;
                            break;

                        case YesNoAnswer.No:
                            defaultText = NoText;
                            break;

                        case YesNoAnswer.Cancel:
                            defaultText = CancelText;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    Console.WriteLine(defaultText);
                    return DefaultOption.Value;
                }

                if (AcceptCancel)
                {
                    if (consoleKeyInfo.Key == CancelKey)
                    {
                        Console.WriteLine(consoleKeyInfo.KeyChar);
                        return YesNoAnswer.Cancel;
                    }

                    if (AcceptEscapeAsCancel && consoleKeyInfo.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Canceled");
                        return YesNoAnswer.Cancel;
                    }
                }

                if (consoleKeyInfo.Key == YesKey)
                {
                    Console.WriteLine(consoleKeyInfo.KeyChar);
                    return YesNoAnswer.Yes;
                }

                if (consoleKeyInfo.Key == NoKey)
                {
                    Console.WriteLine(consoleKeyInfo.KeyChar);
                    return YesNoAnswer.No;
                }
            }
        }
    }
}