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
using DustInTheWind.ConsoleTools.InputControls;

namespace DustInTheWind.ConsoleTools.Spinners
{
    /// <summary>
    /// Displays value and a graphical display in the form af a horizontal bar.
    /// Usually it is used to display the progress from 0% to 100%.
    /// </summary>
    public class ProgressBar
    {
        private readonly Label label = new Label();
        private bool isDisplayed;

        private int value;
        private int minValue;
        private int maxValue = 100;

        private int valueCursorLeft;
        private int valueCursonTop;
        private int valueMaxLength;
        private int unitOfMeasurementMaxLength;

        /// <summary>
        /// Gets or sets the text label to be displayed in front of the progress bar.
        /// Default value: "Progress"
        /// </summary>
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        /// <summary>
        /// Gets or sets a velue that specifies if the text label should be displayed.
        /// </summary>
        public bool ShowLabel { get; set; } = true;

        /// <summary>
        /// Gets or sets the left margin of the value range.
        /// It must be a number smaller than the <see cref="MaxValue"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int MinValue
        {
            get { return minValue; }
            set
            {
                if (value > MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(value), "MinValue cannot be grater than MaxValue.");

                minValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the right margin of the value range.
        /// It must be a number greater than the <see cref="MinValue"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int MaxValue
        {
            get { return maxValue; }
            set
            {
                if (value > MinValue)
                    throw new ArgumentOutOfRangeException(nameof(value), "MaxValue cannot be less than MinValue.");

                maxValue = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies if the control will silently accept values outside
        /// the specified range (<see cref="MinValue"/> - <see cref="MaxValue"/>).
        /// If a value smaller then <see cref="MinValue"/> is provides, the control will display <see cref="MinValue"/>.
        /// If a value greater then <see cref="MaxValue"/> is provides, the control will display <see cref="MaxValue"/>.
        /// Default value: <c>true</c>
        /// </summary>
        public bool AcceptOutOfRangeValues { get; set; } = true;

        /// <summary>
        /// Gets or sets the value to be displayed by the current instance.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Value
        {
            get { return value; }
            set
            {
                if (value < MinValue || value > MaxValue && !AcceptOutOfRangeValues)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between MinValue and MaxValue.");

                this.value = value;

                if (isDisplayed)
                    RefreshDisplayedValue();
            }
        }

        /// <summary>
        /// From the start, for the value will be alocated enough space to write all the value in
        /// the range specified by <see cref="MinValue"/> and <see cref="MaxValue"/>.
        /// This meens that some of the numbers will take less space to write.
        /// Thisvalue specifies the alignment of the displayed value in its alocated space.
        /// </summary>
        public ValueAlignment ValueAlignment { get; set; } = ValueAlignment.Right;

        /// <summary>
        /// Gets or sets the position where to display the value, relative to the progress bar.
        /// It can be displayed at the left, before the progress bar or at the right, after the progress bar.
        /// Default value: Left.
        /// </summary>
        public ValuePosition ValuePosition { get; set; } = ValuePosition.Left;

        /// <summary>
        /// Gets or sets the unit of measurement to be displayed next to the value.
        /// Default: '%'
        /// </summary>
        public string UnitOfMeasurement { get; set; } = "%";

        /// <summary>
        /// Gets or sets the length of the displayed progress bar in number of characters.
        /// Default value: 50
        /// </summary>
        public int Length { get; set; } = 50;

        /// <summary>
        /// Gets or sets the character to be used to paint the empty part of the progress bar.
        /// Default value: '.'
        /// </summary>
        public char BarBackground { get; set; } = '.';

        /// <summary>
        /// Gets or sets the character to be used to paint the filled part of the progress bar.
        /// Default value: '█'
        /// </summary>
        public char BarChar { get; set; } = '█';

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/>
        /// </summary>
        public ProgressBar()
        {
            label.Text = "Progress";
        }

        /// <summary>
        /// Defines the accepted range of values by setting bothe the <see cref="MinValue"/> and <see cref="MaxValue"/>.
        /// </summary>
        /// <param name="minValue">Gets or sets the left margin of the value range. It must be a number smaller than the <see cref="maxValue"/>.</param>
        /// <param name="maxValue">Gets or sets the right margin of the value range. It must be a number greater than the <see cref="minValue"/>.</param>
        public void SetLimitValues(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(maxValue), "maxValue must be greater than minValue.");

            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>
        /// Displays the current instance to the Console.
        /// Important: While the <see cref="ProgressBar"/> is displayed, it is important to not write anything else to the Console
        /// until the <see cref="Done"/> method is called.
        /// In the meantime, you can update its displayed value by setting the <see cref="Value"/> property. The control will
        /// automatically update itself in the Console.
        /// </summary>
        public void Display()
        {
            Console.CursorVisible = false;
            isDisplayed = true;

            if (ShowLabel)
                label.Display();

            valueCursorLeft = Console.CursorLeft;
            valueCursonTop = Console.CursorTop;

            valueMaxLength = Math.Max(MinValue.ToString().Length, MaxValue.ToString().Length);
            unitOfMeasurementMaxLength = string.IsNullOrEmpty(UnitOfMeasurement) ? 0 : UnitOfMeasurement.Length;
            RefreshDisplayedValue();
        }

        private void RefreshDisplayedValue()
        {
            Console.SetCursorPosition(valueCursorLeft, valueCursonTop);

            int calculatedValue = CalculateValue();
            int calculatedLength = CalculateBarLength(calculatedValue);

            string valueAsString = calculatedValue.ToString();

            if (UnitOfMeasurement != null)
                valueAsString += UnitOfMeasurement;

            switch (ValueAlignment)
            {
                case ValueAlignment.Left:
                    valueAsString = valueAsString.PadRight(valueMaxLength + unitOfMeasurementMaxLength);
                    break;

                case ValueAlignment.Right:
                    valueAsString = valueAsString.PadLeft(valueMaxLength + unitOfMeasurementMaxLength);
                    break;
            }

            string progressBarAsString = new string(BarChar, calculatedLength).PadRight(Length, BarBackground);

            switch (ValuePosition)
            {
                default:
                    CustomConsole.Write(valueAsString);
                    CustomConsole.Write(" [");
                    CustomConsole.Write(progressBarAsString);
                    CustomConsole.Write("]");
                    break;

                case ValuePosition.Right:
                    CustomConsole.WriteLine("[");
                    CustomConsole.Write(progressBarAsString);
                    CustomConsole.Write("] ");
                    CustomConsole.Write(valueAsString);
                    break;
            }
        }

        private int CalculateValue()
        {
            int trimmedValue = Value;

            if (AcceptOutOfRangeValues)
            {
                trimmedValue = Math.Max(trimmedValue, MinValue);
                trimmedValue = Math.Min(trimmedValue, MaxValue);
            }

            return trimmedValue;
        }

        private int CalculateBarLength(int value)
        {
            return (value - MinValue) * Length / (MaxValue - MinValue);
        }

        /// <summary>
        /// Ends the display of the <see cref="ProgressBar"/> control.
        /// </summary>
        public void Done()
        {
            CustomConsole.WriteLine();
            isDisplayed = false;
            Console.CursorVisible = true;
        }
    }
}