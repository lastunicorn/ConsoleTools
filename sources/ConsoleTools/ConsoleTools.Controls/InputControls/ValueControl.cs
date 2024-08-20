// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls.InputControls;

/// <summary>
/// Reads a value from the console.
/// </summary>
/// <typeparam name="T">The type of the value that is requested from the user.</typeparam>
public class ValueControl<T> : BlockControl
{
    /// <summary>
    /// Gets or sets a value that specifies if the control should read or write the <see cref="Value"/> property.
    /// </summary>
    public ReadWriteMode ReadWriteMode { get; set; } = ReadWriteMode.Read;

    /// <summary>
    /// Gets or sets the label text to be displayed before the content.
    /// </summary>
    public InlineTextBlock Label { get; set; } = new()
    {
        ForegroundColor = CustomConsole.EmphasizedColor,
        MarginRight = 1
    };

    /// <summary>
    /// Gets or sets a value that specifies if the label should be displayed or not.
    /// </summary>
    public bool ShowLabel { get; set; } = true;

    /// <summary>
    /// Gets the value read from the console.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Gets or sets the default value to be returned if the user just types enter (string empty).
    /// Default value: null
    /// </summary>
    public T DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets a value that specifies if the default value is allowed to be used.
    /// Default value: false
    /// </summary>
    public bool AcceptDefaultValue { get; set; }

    /// <summary>
    /// Gets or sets a value that specifies if the default value should be displayed in the Console
    /// when the user chooses it (types enter without any value).
    /// Default value: <c>true</c>
    /// </summary>
    public bool AutocompleteDefaultValue { get; set; } = true;

    /// <summary>
    /// Gets or sets the text to be displayed when the value provided by the user
    /// cannot be converted into the requested type.
    /// The requested type is provided as parameter {0}.
    /// </summary>
    public string TypeConversionErrorMessage { get; set; } = ValueInputResources.TypeConversionErrorMessage;

    /// <summary>
    /// Gets or sets the function used to parse the text provided by the user.
    /// If the conversion cannot be performed, the convertor must throw an exception.
    /// </summary>
    public Func<string, T> CustomParser { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueControl{T}"/> class.
    /// </summary>
    public ValueControl()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueControl{T}"/> class with
    /// the label to be displayed when the user is requested to provide the value.
    /// </summary>
    /// <param name="label">The label to be displayed when the user is requested to provide the value.</param>
    public ValueControl(string label)
    {
        Label.Text = label;
    }

    /// <summary>
    /// Reads from the console a values that is stored in the <see cref="Value"/> property
    /// and also is returned by this method.
    /// </summary>
    /// <returns>The list of values read from the console.</returns>
    public T Read()
    {
        ReadWriteMode = ReadWriteMode.Read;

        Display();
        return Value;
    }

    /// <summary>
    /// Writes to the console the <see cref="Value"/> property.
    /// </summary>
    public void Write()
    {
        ReadWriteMode = ReadWriteMode.Write;

        Display();
    }

    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Displays the label and waits for the user to provide a value.
    /// </summary>
    protected override void DoRender(IDisplay display, RenderingOptions renderingOptions = null)
    {
        switch (ReadWriteMode)
        {
            case ReadWriteMode.Unknown:
                break;

            case ReadWriteMode.Read:
            {
                ReadValue();
            }
                break;

            case ReadWriteMode.Write:
            {
                if (Label != null && ShowLabel)
                    Label.Display();

                if (Value != null)
                    WriteValue(display);
            }
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ReadValue()
    {
        while (true)
        {
            if (Label != null && ShowLabel)
                DisplayLabel();

            int top = Console.CursorTop;
            int left = Console.CursorLeft;

            string rawValue = Console.ReadLine();

            if (string.IsNullOrEmpty(rawValue) && AcceptDefaultValue)
            {
                if (AutocompleteDefaultValue)
                {
                    Console.SetCursorPosition(left, top);

                    if (DefaultValue == null)
                        Console.WriteLine("<null>");
                    else
                        Console.WriteLine(DefaultValue);
                }

                Value = DefaultValue;
                return;
            }

            try
            {
                Value = ConvertRawValue(rawValue);
                return;
            }
            catch
            {
                CustomConsole.WriteLineError(TypeConversionErrorMessage, typeof(T));
            }
        }
    }

    private void WriteValue(IDisplay display)
    {
        display.Write(Value?.ToString());
        display.DoWriteRootEndLine();
    }

    private void DisplayLabel()
    {
        string temp = Label.Text;

        try
        {
            if (AcceptDefaultValue)
                Label.Text = string.Format(Label.Text, DefaultValue);

            Label.Display();
        }
        finally
        {
            Label.Text = temp;
        }
    }

    private T ConvertRawValue(string value)
    {
        return CustomParser == null
            ? (T)Convert.ChangeType(value, typeof(T))
            : CustomParser(value);
    }

    /// <summary>
    /// Reads a value from the console using a <see cref="ValueControl{T}"/> with default configuration.
    /// </summary>
    /// <param name="label">The label text to be displayed.</param>
    /// <returns>The value read from the console.</returns>
    public static T QuickRead(string label)
    {
        ValueControl<T> valueControl = new(label);
        return valueControl.Read();
    }

    /// <summary>
    /// Reads a value from the console using a <see cref="ValueControl{T}"/> with default configuration.
    /// </summary>
    /// <param name="label">The label text to be displayed.</param>
    /// <param name="value">The value to be displayed.</param>
    /// <returns>The value read from the console.</returns>
    public static void QuickWrite(string label, T value)
    {
        ValueControl<T> valueControl = new(label)
        {
            Value = value
        };
        valueControl.Write();
    }
}