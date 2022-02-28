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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls.InputControls
{
    /// <summary>
    /// Reads or writes a list of values from/to the console.
    /// </summary>
    public class ValueList<T> : BlockControl
    {
        /// <summary>
        /// Gets or sets a value that specifies if the control should read or write the list represented by the <see cref="Values"/> property.
        /// </summary>
        public ReadWriteMode ReadWriteMode { get; set; } = ReadWriteMode.Read;

        /// <summary>
        /// Gets or sets the label text to be displayed before the content.
        /// </summary>
        public TextBlock Label { get; set; } = new TextBlock
        {
            Margin = "0 0 0 1",
            ForegroundColor = CustomConsole.EmphasizedColor
        };

        /// <summary>
        /// Gets or sets a value that specifies if the label should be displayed or not.
        /// </summary>
        public bool ShowLabel { get; set; } = true;

        /// <summary>
        /// Gets the list of values to be read or written from/to the console.
        /// </summary>
        public List<T> Values { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces by which the items are indented.
        /// </summary>
        public int ItemsIndentation { get; set; } = 1;

        /// <summary>
        /// Gets or sets the bullet character displayed in front of each item that is read.
        /// </summary>
        public string Bullet { get; set; } = "-";

        /// <summary>
        /// Gets or sets the number of spaced displayed after the bullet.
        /// </summary>
        public int SpaceAfterBullet { get; set; } = 1;

        /// <summary>
        /// Gets or sets the text to be displayed when the value provided by the user
        /// cannot be converted into the requested type.
        /// The requested type is provided as parameter {0}.
        /// It is used when the control is in "Read" mode.
        /// </summary>
        public string TypeConversionErrorMessage { get; set; } = ListInputResources.TypeConversionErrorMessage;

        /// <summary>
        /// Gets or sets the function used to parse the text provided by the user.
        /// If the conversion cannot be performed, the convertor must throw an exception.
        /// It is used when the control is in "Read" mode.
        /// </summary>
        public Func<string, T> CustomParser { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueList{T}"/> class.
        /// </summary>
        public ValueList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueList{T}"/> class with
        /// the label to be displayed when the user is requested to provide the values.
        /// </summary>
        /// <param name="label">The label to be displayed before the content.</param>
        public ValueList(string label)
        {
            Label.Text = label;
        }

        /// <summary>
        /// Reads from the console a list of values that is stored in the <see cref="Values"/> property
        /// and also is returned by this method.
        /// </summary>
        /// <returns>The list of values read from the console.</returns>
        public List<T> Read()
        {
            ReadWriteMode = ReadWriteMode.Read;

            Display();
            return Values;
        }

        /// <summary>
        /// Writes to the console the list of values from the <see cref="Values"/> property.
        /// </summary>
        public void Write()
        {
            ReadWriteMode = ReadWriteMode.Write;

            Display();
        }

        /// <summary>
        /// Displays the label and waits for the user to type the values.
        /// The control reads values until the user inserts an empty string.
        /// </summary>
        /// <returns>The list with the values provided by the user.</returns>
        protected override void DoDisplayContent(ControlDisplay display)
        {
            switch (ReadWriteMode)
            {
                case ReadWriteMode.Unknown:
                    break;

                case ReadWriteMode.Read:
                    {
                        if (Label != null && ShowLabel)
                            Label.Display();

                        ReadAllValues();
                    }
                    break;

                case ReadWriteMode.Write:
                    {
                        if (Label != null && ShowLabel)
                            Label.Display();

                        if (Values != null)
                            WriteAllValues();
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ReadAllValues()
        {
            Values = new List<T>();

            string leftPart = BuildItemLeftPart();

            while (true)
            {
                int cursorLeft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;

                CustomConsole.Write(leftPart);
                string rawValue = Console.ReadLine();

                if (string.IsNullOrEmpty(rawValue))
                {
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    string emptyText = new string(' ', leftPart.Length);
                    Console.Write(emptyText);
                    Console.SetCursorPosition(cursorLeft, cursorTop);
                    break;
                }

                try
                {
                    T value = ConvertRawValue(rawValue);
                    Values.Add(value);
                }
                catch
                {
                    CustomConsole.WriteLineError(TypeConversionErrorMessage, typeof(T));
                }
            }
        }

        private T ConvertRawValue(string value)
        {
            return CustomParser == null
                ? (T)Convert.ChangeType(value, typeof(T))
                : CustomParser(value);
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

        private void WriteAllValues()
        {
            string leftPart = BuildItemLeftPart();

            foreach (T value in Values)
            {
                CustomConsole.Write(leftPart);
                CustomConsole.WriteLine(value);
            }
        }

        /// <summary>
        /// Reads a list of values from the console using a <see cref="ValueList{T}"/> with default configuration.
        /// </summary>
        /// <param name="label">The label text to be displayed.</param>
        /// <returns>The value read from the console.</returns>
        public static List<T> QuickRead(string label = null)
        {
            ValueList<T> valueList = new ValueList<T>(label)
            {
                ReadWriteMode = ReadWriteMode.Read,
            };
            return valueList.Read();
        }

        /// <summary>
        /// Reads a list of values from the console using a <see cref="ValueList{T}"/> with default configuration.
        /// </summary>
        /// <param name="label">The label text to be displayed.</param>
        /// <param name="values">The list of values to be displayed.</param>
        /// <returns>The value read from the console.</returns>
        public static void QuickWrite(string label, IEnumerable<T> values)
        {
            ValueList<T> valueList = new ValueList<T>(label)
            {
                ReadWriteMode = ReadWriteMode.Write,
                Values = values.ToList()
            };
            valueList.Write();
        }
    }
}