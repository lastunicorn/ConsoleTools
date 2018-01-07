// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
    /// <summary>
    /// This control displays a question and expects an answer of "yes" or "no".
    /// </summary>
    public class YesNoControl
    {
        private string questionText;

        /// <summary>
        /// Gets or sets trhe question that is displayed to the user.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public string QuestionText
        {
            get { return questionText; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (value.Length == 0) throw new ArgumentException("The question text cannot be null or empty string.", nameof(value));

                questionText = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of spaces to be displayed after the question and the before the user types the answer.
        /// Default value: 1
        /// </summary>
        public int SpaceAfterQuestion { get; set; } = 1;

        /// <summary>
        /// Gets or sets the text displayed for the "Yes" answer.
        /// Default value: "y"
        /// </summary>
        public string YesText { get; set; } = "y";

        /// <summary>
        /// Gets or sets the text displayed for the "No" answer.
        /// Default value: "n"
        /// </summary>
        public string NoText { get; set; } = "n";

        /// <summary>
        /// Gets or sets the text displayed for the "Cancel" answer.
        /// Default value: "esc"
        /// </summary>
        public string CancelText { get; set; } = "esc";

        /// <summary>
        /// Gets or sets a value that specifies if the first letter of the default answer is displayed with upper case.
        /// </summary>
        public bool CapitalizeDefaultAnswer { get; set; } = true;

        /// <summary>
        /// Gets or sets the key representing the "Yes" answer.
        /// Default value: <see cref="ConsoleKey.Y"/>
        /// </summary>
        public ConsoleKey YesKey { get; set; } = ConsoleKey.Y;

        /// <summary>
        /// Gets or sets the key representing the "No" answer.
        /// Default value: <see cref="ConsoleKey.N"/>
        /// </summary>
        public ConsoleKey NoKey { get; set; } = ConsoleKey.N;

        /// <summary>
        /// Gets or sets the key representing the "Cancel" answer.
        /// Default value: <see cref="ConsoleKey.Escape"/>
        /// </summary>
        public ConsoleKey CancelKey { get; set; } = ConsoleKey.Escape;

        /// <summary>
        /// Gets or sets the key that will accept the default answer.
        /// Default value: <see cref="ConsoleKey.Enter"/>
        /// </summary>
        public ConsoleKey AcceptDefaultKey { get; set; } = ConsoleKey.Enter;

        /// <summary>
        /// Gets or sets a value that specifies if the "Cancel" answer is accepted as a valid answer.
        /// If this value is <c>false</c>, the user is forced to answer with "Yes" or "No".
        /// Default value: <c>false</c>
        /// </summary>
        public bool AcceptCancel { get; set; } = false;

        /// <summary>
        /// Gets or sets a value that specifies if the <see cref="ConsoleKey.Escape"/> is accepted as "Cancel"
        /// beside the key defined by the <see cref="CancelKey"/>.
        /// Default =value: <c>true</c>
        /// </summary>
        public bool AcceptEscapeAsCancel { get; set; } = true;

        /// <summary>
        /// Gets or sets the answer that is issued when the <see cref="AcceptDefaultKey"/> key is pressed.
        /// Default value: null
        /// </summary>
        public YesNoAnswer? DefaultAnswer { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="YesNoControl"/> class with
        /// the question to be displayed to the user.
        /// </summary>
        /// <param name="questionText">The question text to be displayed to the user.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public YesNoControl(string questionText)
        {
            if (questionText == null) throw new ArgumentNullException(nameof(questionText));
            if (questionText.Length == 0) throw new ArgumentException("Please provide a question text.", nameof(questionText));

            QuestionText = questionText;
        }

        /// <summary> 
        /// Displays the question to the user and waits for the answer.
        /// </summary>
        public YesNoAnswer ReadAnswer()
        {
            DisplayWholeQuestion();
            return ReadAnswerInternal();
        }

        private void DisplayWholeQuestion()
        {
            Console.Write(QuestionText);
            Console.Write(" ");

            DisplayPossibleAnswersList();

            if (SpaceAfterQuestion > 0)
            {
                string space = new string(' ', SpaceAfterQuestion);
                Console.Write(space);
            }
        }

        /// <summary>
        /// Displays to the console the list of possible answers.
        /// </summary>
        protected virtual void DisplayPossibleAnswersList()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            string yesText = CapitalizeDefaultAnswer && DefaultAnswer == YesNoAnswer.Yes
                ? YesText.ToUpper()
                : YesText;
            sb.Append(yesText);

            sb.Append("/");

            string noText = CapitalizeDefaultAnswer && DefaultAnswer == YesNoAnswer.No
                ? NoText.ToUpper()
                : NoText;
            sb.Append(noText);

            if (AcceptCancel)
            {
                sb.Append("/");

                string cancelText = CapitalizeDefaultAnswer && DefaultAnswer == YesNoAnswer.Cancel
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

                if (consoleKeyInfo.Key == AcceptDefaultKey && DefaultAnswer.HasValue)
                {
                    string defaultText;

                    switch (DefaultAnswer.Value)
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
                    return DefaultAnswer.Value;
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