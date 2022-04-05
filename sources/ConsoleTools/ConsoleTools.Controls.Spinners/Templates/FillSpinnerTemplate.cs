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

namespace DustInTheWind.ConsoleTools.Controls.Spinners.Templates
{
    /// <summary>
    /// A template for the <see cref="Spinner"/> that fills a bar with a specified character.
    /// </summary>
    public class FillSpinnerTemplate : ISpinnerTemplate
    {
        private readonly char fillChar;
        private readonly int length;
        private int currentStep;
        private FilledBehavior filledBehavior = FilledBehavior.EmptyFromStart;

        /// <summary>
        /// Gets or sets the border character to be displayed at the left of the bar.
        /// </summary>
        public string BorderStart { get; set; } = "[";

        /// <summary>
        /// Gets or sets the border character to be displayed at the right of the bar.
        /// </summary>
        public string BorderEnd { get; set; } = "]";

        /// <summary>
        /// Gets or sets a value that specifies if the borders are displayed.
        /// </summary>
        public bool ShowBorders { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies what to do when the bar is filled.
        /// </summary>
        public FilledBehavior FilledBehavior
        {
            get => filledBehavior;
            set
            {
                bool valueIsDefined = Enum.IsDefined(typeof(FilledBehavior), value);

                if (!valueIsDefined)
                    throw new ArgumentOutOfRangeException(nameof(value));

                filledBehavior = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FillSpinnerTemplate"/> class.
        /// </summary>
        public FillSpinnerTemplate()
        {
            fillChar = '.';
            length = 4;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FillSpinnerTemplate"/> class with
        /// the character used to fill the bar and
        /// the length of the bar.
        /// </summary>
        /// <param name="fillChar">The character used to fill the bar.</param>
        /// <param name="length">The length of the bar.</param>
        public FillSpinnerTemplate(char fillChar, int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

            this.fillChar = fillChar;
            this.length = length;
        }

        /// <summary>
        /// Resets the template. It will start from the first frame.
        /// </summary>
        public void Reset()
        {
            currentStep = 0;
        }

        /// <summary>
        /// Moves to the next frame and returns it.
        /// </summary>
        public string GetNext()
        {
            currentStep++;

            int totalStepCount = CalculateTotalStepCount();

            if (currentStep > totalStepCount - 1)
                currentStep = 0;

            return GetCurrent();
        }

        private int CalculateTotalStepCount()
        {
            switch (FilledBehavior)
            {
                case FilledBehavior.SuddenEmpty:
                    return length + 1;

                case FilledBehavior.EmptyFromEnd:
                    return 2 * length;

                case FilledBehavior.EmptyFromStart:
                    return 2 * length;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Returns the current frame.
        /// </summary>
        public string GetCurrent()
        {
            string content = BuildContent();

            return ShowBorders
                ? string.Concat(BorderStart, content, BorderEnd)
                : content;
        }

        private string BuildContent()
        {
            switch (FilledBehavior)
            {
                case FilledBehavior.SuddenEmpty:
                    return new string(fillChar, currentStep).PadRight(length);

                case FilledBehavior.EmptyFromEnd:
                    return currentStep < length
                        ? new string(fillChar, currentStep).PadRight(length)
                        : new string(fillChar, 2 * length - currentStep).PadRight(length);

                case FilledBehavior.EmptyFromStart:
                    return currentStep < length
                        ? new string(fillChar, currentStep).PadRight(length)
                        : new string(fillChar, 2 * length - currentStep).PadLeft(length);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}