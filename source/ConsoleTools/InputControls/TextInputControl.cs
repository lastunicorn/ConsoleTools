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

namespace DustInTheWind.ConsoleTools.InputControls
{
    /// <summary>
    /// Reads a value from the console.
    /// </summary>
    /// <typeparam name="T">The type of the value that is requested from the user.</typeparam>
    public class TextInputControl<T>
    {
        private readonly Label labelControl = new Label
        {
            ForegroundColor = CustomConsole.EmphasiesColor
        };

        /// <summary>
        /// Gets or sets the label text to be displayed before the user types the value.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the amount of space to be displayed between the label and the value.
        /// </summary>
        public int SpaceAfterLabel
        {
            get { return labelControl.MarginRight; }
            set { labelControl.MarginRight = value; }
        }

        /// <summary>
        /// Gets or sets the color used to display the label text.
        /// </summary>
        public ConsoleColor? LabelColor
        {
            get { return labelControl.ForegroundColor; }
            set { labelControl.ForegroundColor = value; }
        }

        /// <summary>
        /// Gets or sets the default value to be returned if the user just types enter (string empty).
        /// </summary>
        public T DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the default value is allowed to be used.
        /// </summary>
        public bool AcceptDefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the text to be displayed when the value provided by the user
        /// cannot be converted into the requested type.
        /// The requested type is provided as parameter {0}.
        /// </summary>
        public string TypeValidationErrorMessage { get; set; } = "The input value must be a {0} type.";

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputControl{T}"/> class.
        /// </summary>
        public TextInputControl()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputControl{T}"/> class with
        /// the label to be displayed when the user is requested to provide the value.
        /// </summary>
        /// <param name="label">The label to be displayed when the user is requested to provide the value.</param>
        public TextInputControl(string label)
        {
            Label = label;
        }

        /// <summary>
        /// Displays the label and waits for the user to provide a value.
        /// </summary>
        /// <returns>The value read from the console.</returns>
        public T Read()
        {
            while (true)
            {
                DisplayLabel();

                string rawValue = Console.ReadLine();

                if (string.IsNullOrEmpty(rawValue) && AcceptDefaultValue)
                    return DefaultValue;

                try
                {
                    return ConvertRawValue(rawValue);
                }
                catch
                {
                    CustomConsole.WriteLineError(TypeValidationErrorMessage, typeof(T));
                }
            }
        }

        private void DisplayLabel()
        {
            labelControl.Text = AcceptDefaultValue
                ? string.Format(Label, DefaultValue)
                : Label;
            labelControl.Display();
        }

        private static T ConvertRawValue(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Reads a value from the console using a <see cref="TextInputControl{T}"/> with default configuration.
        /// </summary>
        /// <param name="label">The label text to be displayed.</param>
        /// <returns>The value read from the console.</returns>
        public static T QuickRead(string label)
        {
            return new TextInputControl<T>(label).Read();
        }
    }
}