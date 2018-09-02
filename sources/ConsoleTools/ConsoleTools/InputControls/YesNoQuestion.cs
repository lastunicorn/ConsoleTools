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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Text;

namespace DustInTheWind.ConsoleTools.InputControls
{
    /// <summary>
    /// This control reads a yes/no answer from the console.
    /// </summary>
    public class YesNoQuestion : BlockControl
    {
        private readonly Label labelControl = new Label
        {
            ForegroundColor = CustomConsole.EmphasiesColor
        };

        /// <summary>
        /// Gets or sets trhe question that is displayed to the user.
        /// </summary>
        public string QuestionText { get; set; }

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
        public YesNoAnswer? DefaultAnswer { get; set; }

        /// <summary>
        /// Gets the last answer provided by the user.
        /// </summary>
        public YesNoAnswer Answer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="YesNoQuestion"/> class.
        /// </summary>
        public YesNoQuestion()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YesNoQuestion"/> class with
        /// the question to be displayed to the user.
        /// </summary>
        /// <param name="questionText">The question text to be displayed to the user.</param>
        public YesNoQuestion(string questionText)
        {
            QuestionText = questionText;
        }

        /// <summary> 
        /// Displays the question to the user and waits for the answer.
        /// </summary>
        public YesNoAnswer ReadAnswer()
        {
            Display();
            return Answer;
        }

        /// <summary> 
        /// Displays the question to the user and waits for the answer.
        /// </summary>
        protected override void DoDisplayContent()
        {
            if (QuestionText != null)
                DisplayQuestion();

            DisplayPossibleAnswersList();

            if (SpaceAfterQuestion > 0)
                DisplaySpaceAfterQuestion();

            Answer = ReadAnswerInternal();
        }

        private void DisplayQuestion()
        {
            labelControl.Text = QuestionText;
            labelControl.Display();
        }

        private void DisplaySpaceAfterQuestion()
        {
            string space = new string(' ', SpaceAfterQuestion);
            Console.Write(space);
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
                        Console.WriteLine(YesNoQuestionResources.QuestionCanceled);
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

        /// <summary>
        /// Displays the specified question and returns the answer using a <see cref="YesNoQuestion"/> instance with default configuration.
        /// </summary>
        /// <param name="questionText">The question text to be displayed to the user.</param>
        /// <param name="defaultAnswer">An optional default answer to be returned if the user does not provide a specific answer.</param>
        /// <returns>The answer read from the console.</returns>
        public static YesNoAnswer QuickRead(string questionText, YesNoAnswer? defaultAnswer = null)
        {
            YesNoQuestion yesNoQuestion = new YesNoQuestion(questionText);

            if (defaultAnswer != null)
                yesNoQuestion.DefaultAnswer = defaultAnswer;

            return yesNoQuestion.ReadAnswer();
        }
    }
}