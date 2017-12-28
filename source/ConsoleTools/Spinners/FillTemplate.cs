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

namespace DustInTheWind.ConsoleTools.Spinners
{
    public class FillTemplate : ITemplate
    {
        private readonly char fillChar;
        private readonly int length;
        private int currentStep;
        private FilledBehavior filledBehavior = FilledBehavior.EmptyFromStart;

        public string BorderStart { get; set; } = "[";
        public string BorderEnd { get; set; } = "]";

        public bool ShowBorders { get; set; }

        public FilledBehavior FilledBehavior
        {
            get { return filledBehavior; }
            set
            {
                bool valueIsDefined = Enum.IsDefined(typeof(FilledBehavior), value);

                if (!valueIsDefined)
                    throw new ArgumentOutOfRangeException(nameof(value));

                filledBehavior = value;
            }
        }

        public FillTemplate()
        {
            fillChar = '.';
            length = 4;
        }

        public FillTemplate(char fillChar, int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

            this.fillChar = fillChar;
            this.length = length;
        }

        public void Reset()
        {
            currentStep = 0;
        }

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