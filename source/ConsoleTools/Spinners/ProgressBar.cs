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
using DustInTheWind.ConsoleTools.InputControls;

namespace DustInTheWind.ConsoleTools.Spinners
{
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

        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public bool ShowLabel { get; set; } = true;

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

        public bool AcceptOutOfRangeValues { get; set; }

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

        public ValueAlignment ValueAlignment { get; set; } = ValueAlignment.Right;

        public ValuePosition ValuePosition { get; set; } = ValuePosition.Left;

        public string UnitOfMeasurement { get; set; } = "%";

        public int Length { get; set; } = 50;

        public string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }
        
        public char BarBackground { get; set; } = '.';
        public char BarChar { get; set; } = '█';

        public ProgressBar()
        {
            label.Text = "Progress";
        }

        public void SetLimitValues(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(maxValue), "maxValue must be greater than minValue.");

            this.minValue = minValue;
            this.maxValue = maxValue;
        }

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

        public void Done()
        {
            CustomConsole.WriteLine();
            isDisplayed = false;
            Console.CursorVisible = true;
        }
    }
}