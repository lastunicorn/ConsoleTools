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

using System;
using DustInTheWind.ConsoleTools.CommandLine;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

internal class PrompterRenderer : BlockControlRenderer<Prompter>
{
    private bool success;
    private bool closeWasRequested;

    /// <summary>
    /// Gets the last command read from the console.
    /// </summary>
    public CliCommand LastCommand { get; private set; }

    public PrompterRenderer(Prompter prompter, IDisplay display, RenderingOptions renderingOptions)
        : base(prompter, display, renderingOptions)
    {
    }

    protected override bool InitializeContentRendering()
    {
        success = false;
        closeWasRequested = false;

        return !success && !closeWasRequested;
    }

    protected override bool RenderNextContentLine()
    {
        RenderingContext.StartLine();
        string text = Control.TextFormat == null
            ? Control.Text
            : string.Format(Control.TextFormat, Control.Text);
        RenderingContext.Write(text);
        success = ReadUserInput();
        RenderingContext.EndLine();

        return !success && !closeWasRequested;
    }

    private bool ReadUserInput()
    {
        string commandText = Console.ReadLine();

        if (commandText == null)
        {
            RequestClose();
            return false;
        }

        if (commandText.Length == 0)
            return false;

        CliCommand newCommand = CliCommand.Parse(commandText);

        if (newCommand.IsEmpty)
            return false;

        LastCommand = newCommand;

        return true;
    }

    /// <summary>
    /// The <see cref="ControlRepeater"/> calls this method to announce the control that it should end its process.
    /// </summary>
    public void RequestClose()
    {
        closeWasRequested = true;
        //OnClosed();
    }
}