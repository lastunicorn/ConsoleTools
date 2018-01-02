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
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.ConsoleTools.InputControls
{
    /// <summary>
    /// Reads a list of items from the Console.
    /// </summary>
    public class ListInputControl<T>
    {
        private readonly Label labelControl = new Label
        {
            MarginLeft = 0,
            MarginRight = 0
        };

        public string Label { get; set; }
        
        public int ItemsIndentation { get; set; } = 1;
        public string Bullet { get; set; } = "-";
        public int SpaceAfterBullet { get; set; } = 1;

        /// <summary>
        /// Gets or sets the text to be displayed when the value provided by the user
        /// cannot be converted into the requested type.
        /// The requested type is provided as parameter {0}.
        /// </summary>
        public string TypeValidationErrorMessage { get; set; } = "The input value must be an {0} type.";

        /// <summary>
        /// Initializes a new instance of the <see cref="ListInputControl{T}"/> class.
        /// </summary>
        public ListInputControl()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListInputControl{T}"/> class with
        /// the label to be displayed when the user is requested to provide the values.
        /// </summary>
        /// <param name="label">The label to be displayed when the user is requested to provide the values.</param>
        public ListInputControl(string label)
        {
            Label = label;
        }

        public List<T> Read()
        {
            if (Label != null)
            {
                labelControl.Text = Label;
                labelControl.Display();
                CustomConsole.WriteLine();
            }

            return ReadAllValues();
        }

        private List<T> ReadAllValues()
        {
            List<T> values = new List<T>();

            string leftpart = BuildItemLeftPart();

            while (true)
            {
                int cursorLeft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;

                CustomConsole.Write(leftpart);
                string rawValue = Console.ReadLine();

                if (string.IsNullOrEmpty(rawValue))
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    string emptyText = new string(' ', leftpart.Length);
                    Console.Write(emptyText);
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    break;
                }

                try
                {
                    T value = ConvertRawValue(rawValue);
                    values.Add(value);
                }
                catch
                {
                    CustomConsole.WriteLineError(TypeValidationErrorMessage, typeof(T));
                }
            }

            return values;
        }

        private static T ConvertRawValue(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        private string BuildItemLeftPart()
        {
            StringBuilder sb = new StringBuilder();

            if (ItemsIndentation > 0)
            {
                string indentation = new string(' ', ItemsIndentation);
                sb.Append(indentation);
            }

            if (Bullet != null)
            {
                sb.Append(Bullet);

                if (SpaceAfterBullet > 0)
                {
                    string bulletSpace = new string(' ', SpaceAfterBullet);
                    sb.Append(bulletSpace);
                }
            }

            return sb.ToString();
        }
    }
}