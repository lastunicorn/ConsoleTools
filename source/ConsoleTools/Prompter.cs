﻿// ConsoleTools
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

namespace DustInTheWind.ConsoleTools
{
    public class Prompter
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

        public int SpaceBeforePrompter { get; set; } = 0;
        public int SpaceAfterPrompter { get; set; } = 1;

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
        /// After a command is obtained from the console, the <see cref="NewCommand"/> event is raised.
        /// The infinite loop that reads commands can be stopped only by setting the Exit property in the
        /// <see cref="NewCommand"/> event or by calling the <see cref="RequestStop"/> method.
        /// </summary>
        public void Run()
        {
            stopWasRequested = false;

            do
            {
                DisplayWholePrompter();

                string commandText = Console.ReadLine();
                Console.WriteLine();

                UserCommand command = UserCommand.Parse(commandText);

                try
                {
                    NewCommandEventArgs eva = new NewCommandEventArgs(command);
                    OnNewCommand(eva);

                    if (eva.Exit)
                        stopWasRequested = true;
                }
                catch { }
            }
            while (!stopWasRequested);
        }

        public UserCommand RunOnce()
        {
            DisplayWholePrompter();

            string commandText = Console.ReadLine();
            Console.WriteLine();

            return UserCommand.Parse(commandText);
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