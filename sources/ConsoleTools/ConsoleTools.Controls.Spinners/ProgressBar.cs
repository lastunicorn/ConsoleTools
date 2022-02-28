// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls.Spinners
{
    /// <summary>
    /// Displays value and a graphical display in the form af a horizontal bar.
    /// Usually it is used to display the progress from 0% to 100%.
    /// </summary>
    public class ProgressBar : LongRunningControl
    {
        private readonly InlineTextBlock label = new InlineTextBlock { MarginRight = 1 };

        private int value;
        private int minValue;
        private int maxValue = 100;

        private int valueCursorLeft;
        private int valueCursorTop;
        private int valueMaxLength;
        private int unitOfMeasurementMaxLength;

        /// <summary>
        /// Gets or sets the text label to be displayed in front of the progress bar.
        /// Default value: "Progress"
        /// </summary>
        public string LabelText
        {
            get => label.Text;
            set => label.Text = value;
        }

        /// <summary>
        /// Gets or sets a value that specifies if the text label should be displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool ShowLabel { get; set; } = true;

        /// <summary>
        /// Gets or sets the left margin of the value range.
        /// It must be a number smaller than the <see cref="MaxValue"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int MinValue
        {
            get => minValue;
            set
            {
                if (value > MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(value), "MinValue cannot be greater than MaxValue.");

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
            get => maxValue;
            set
            {
                if (value < MinValue)
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
            get => value;
            set
            {
                if (value < MinValue || value > MaxValue && !AcceptOutOfRangeValues)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between MinValue and MaxValue.");

                this.value = value;

                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies if the underlying value should be displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool ShowValue { get; set; } = true;

        /// <summary>
        /// From the start, for the value will be allocated enough space to write all the value in
        /// the range specified by <see cref="MinValue"/> and <see cref="MaxValue"/>.
        /// This means that some of the numbers will take less space to write.
        /// This value specifies the alignment of the displayed value in its allocated space.
        /// </summary>
        public HorizontalAlignment ValueHorizontalAlignment { get; set; } = HorizontalAlignment.Right;

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
        public char BarEmptyChar { get; set; } = '.';

        /// <summary>
        /// Gets or sets the foreground color of the character used for the empty part of the bar.
        /// If this value is <c>null</c>, no specific color is set, the current foreground color set in the <see cref="Console"/> is used.
        /// </summary>
        public ConsoleColor? BarEmptyForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color of the character used for the empty part of the bar.
        /// If this value is <c>null</c>, no specific color is set, the current background color set in the <see cref="Console"/> is used.
        /// </summary>
        public ConsoleColor? BarEmptyBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the character to be used to paint the filled part of the progress bar.
        /// Default value: '█'
        /// </summary>
        public char BarFillChar { get; set; } = '█';

        /// <summary>
        /// Gets or sets the foreground color of the character used for the fill part of the bar.
        /// If this value is <c>null</c>, no specific color is set, the current foreground color set in the <see cref="Console"/> is used.
        /// </summary>
        public ConsoleColor? BarFillForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color of the character used for the fill part of the bar.
        /// If this value is <c>null</c>, no specific color is set, the current background color set in the <see cref="Console"/> is used.
        /// </summary>
        public ConsoleColor? BarFillBackgroundColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/>
        /// </summary>
        public ProgressBar()
        {
            label.Text = "Progress";
        }

        /// <summary>
        /// Defines the accepted range of values by setting both the <see cref="MinValue"/> and <see cref="MaxValue"/>.
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
        /// until the <see cref="LongRunningControl.Close"/> method is called.
        /// In the meantime, you can update its displayed value by setting the <see cref="Value"/> property. The control will
        /// automatically update itself in the Console.
        /// </summary>
        protected override void DoDisplayContent()
        {
            if (ShowLabel)
                label.Display();

            valueCursorLeft = Console.CursorLeft;
            valueCursorTop = Console.CursorTop;

            valueMaxLength = Math.Max(MinValue.ToString().Length, MaxValue.ToString().Length);
            unitOfMeasurementMaxLength = string.IsNullOrEmpty(UnitOfMeasurement) ? 0 : UnitOfMeasurement.Length;
        }

        protected override void DoRefresh()
        {
            Console.SetCursorPosition(valueCursorLeft, valueCursorTop);

            int calculatedValue = CalculateValue();

            switch (ValuePosition)
            {
                default:
                    if (ShowValue)
                    {
                        string valueAsString = ValueToString(calculatedValue);
                        CustomConsole.Write(valueAsString);
                    }

                    DisplayBar(calculatedValue);
                    break;

                case ValuePosition.Right:
                    DisplayBar(calculatedValue);

                    if (ShowValue)
                    {
                        string valueAsString = ValueToString(calculatedValue);
                        CustomConsole.Write(valueAsString);
                    }
                    break;
            }
        }

        private void DisplayBar(int calculatedValue)
        {
            int fillLength = CalculateFillLength(calculatedValue);
            int emptyLength = Length - fillLength;

            string fillString = new string(BarFillChar, fillLength);
            string emptyString = new string(BarEmptyChar, emptyLength);

            CustomConsole.Write(" [");

            if (BarFillForegroundColor.HasValue && BarFillBackgroundColor.HasValue)
                CustomConsole.Write(BarFillForegroundColor.Value, BarFillBackgroundColor.Value, fillString);
            else if (BarFillForegroundColor.HasValue)
                CustomConsole.Write(BarFillForegroundColor.Value, fillString);
            else if (BarFillBackgroundColor.HasValue)
                CustomConsole.WriteBackgroundColor(BarFillBackgroundColor.Value, fillString);
            else
                CustomConsole.Write(fillString);

            if (BarEmptyForegroundColor.HasValue && BarEmptyBackgroundColor.HasValue)
                CustomConsole.Write(BarEmptyForegroundColor.Value, BarEmptyBackgroundColor.Value, emptyString);
            else if (BarEmptyForegroundColor.HasValue)
                CustomConsole.Write(BarEmptyForegroundColor.Value, emptyString);
            else if (BarEmptyBackgroundColor.HasValue)
                CustomConsole.WriteBackgroundColor(BarEmptyBackgroundColor.Value, emptyString);
            else
                CustomConsole.Write(emptyString);

            CustomConsole.Write("]");
        }

        private string ValueToString(int calculatedValue)
        {
            string valueAsString = calculatedValue.ToString();

            if (UnitOfMeasurement != null)
                valueAsString += UnitOfMeasurement;

            int totalWidth = valueMaxLength + unitOfMeasurementMaxLength;

            return AlignedText.QuickAlign(valueAsString, ValueHorizontalAlignment, totalWidth);
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

        private int CalculateFillLength(int value)
        {
            int valueRange = MaxValue - MinValue;

            if (valueRange == 0)
                return Length;

            return (value - MinValue) * Length / valueRange;
        }

        /// <summary>
        /// Ends the display of the <see cref="ProgressBar"/> control.
        /// </summary>
        protected override void DoClose()
        {
            CustomConsole.WriteLine();
        }
    }
}