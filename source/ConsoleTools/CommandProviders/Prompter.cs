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

namespace DustInTheWind.ConsoleTools.CommandProviders
{
    /// <summary>
    /// Reads commands from the console.
    /// </summary>
    public class Prompter : ICommandProvider
    {
        private volatile bool stopWasRequested;

        /// <summary>
        /// Gets or sets the text displayed in the prompter.
        /// </summary>
        public string PrompterText { get; set; }

        /// <summary>
        /// Gets or sets the glyph displayed after the prompter text.
        /// </summary>
        public string PrompterGlyph { get; set; } = ">";

        /// <summary>
        /// Gets or sets the count of spaces to be displayed before the prompter (text + glyph).
        /// </summary>
        public int SpaceBeforePrompter { get; set; } = 0;

        /// <summary>
        /// Gets or sets the count of spaces to be displayed after the prompter (text + glyph), before the user can write his command.
        /// </summary>
        public int SpaceAfterPrompter { get; set; } = 1;

        /// <summary>
        /// Gets or sets a value that specifies if the prompter should always be displayed at the beginning of the line.
        /// If this value is <c>true</c> and the cursor is not at the beginning of the line, a new line is written before displaying the prompter.
        /// </summary>
        public bool EnsureBeginOfLine { get; set; } = true;

        /// <summary>
        /// Event raised when the user writes a new command at the console.
        /// </summary>
        public event EventHandler<NewCommandEventArgs> NewCommand;

        /// <summary>
        /// Raises the <see cref="NewCommand"/> event.
        /// </summary>
        /// <param name="e">An <see cref="NewCommandEventArgs"/> that contains the event data.</param>
        public void OnNewCommand(NewCommandEventArgs e)
        {
            NewCommand?.Invoke(null, e);
        }
        
        /// <summary>
        /// Continously read from the console new commands.
        /// After a command is obtained from the console, the <see cref="E:DustInTheWind.ConsoleTools.CommandProviders.Prompter.NewCommand" /> event is raised.
        /// The <see cref="M:DustInTheWind.ConsoleTools.CommandProviders.Prompter.Run" /> method blocks the current execution thread.
        /// The infinite loop that reads commands can be stopped
        /// by setting the <see cref="P:DustInTheWind.ConsoleTools.CommandProviders.NewCommandEventArgs.Exit" /> property in the <see cref="E:DustInTheWind.ConsoleTools.CommandProviders.Prompter.NewCommand" /> event
        /// or by calling the <see cref="M:DustInTheWind.ConsoleTools.CommandProviders.Prompter.RequestStop" /> method.
        /// </summary>
        public void Run()
        {
            stopWasRequested = false;

            do
            {
                DisplayWholePrompter();

                string commandText = Console.ReadLine();

                if (commandText == null)
                    break;

                if (commandText.Length == 0)
                    continue;

                CliCommand command = CliCommand.Parse(commandText);

                Console.WriteLine();

                try
                {
                    NewCommandEventArgs eva = new NewCommandEventArgs(command);
                    OnNewCommand(eva);

                    if (eva.Exit)
                        stopWasRequested = true;
                }
                catch (Exception ex)
                {
                    CustomConsole.WriteError(ex);
                }
            }
            while (!stopWasRequested);
        }

        /// <summary>
        /// Reads a single command (<see cref="CliCommand"/>) from the console and returns it.
        /// </summary>
        /// <returns>A <see cref="CliCommand"/> object containing the command typed by the user.</returns>
        public CliCommand RunOnce()
        {
            DisplayWholePrompter();

            string commandText = Console.ReadLine();
            Console.WriteLine();

            return CliCommand.Parse(commandText);
        }

        protected virtual void DisplayWholePrompter()
        {
            // Move the cursor at the beginning of a new line if necessary.
            if (EnsureBeginOfLine && Console.CursorLeft != 0)
                Console.WriteLine();

            if (SpaceBeforePrompter > 0)
            {
                string leftMargin = new string(' ', SpaceBeforePrompter);
                Console.Write(leftMargin);
            }

            if (!string.IsNullOrEmpty(PrompterText))
                Console.Write(PrompterText);

            if (!string.IsNullOrEmpty(PrompterGlyph))
                Console.Write(PrompterGlyph);

            if (SpaceAfterPrompter > 0)
            {
                string rightMargin = new string(' ', SpaceAfterPrompter);
                Console.Write(rightMargin);
            }
        }

        /// <summary>
        /// Sets the stop flag.
        /// The Prompter's loop will exit next time when it checks the stop flag.
        /// </summary>
        public void RequestStop()
        {
            stopWasRequested = true;
        }
    }
}