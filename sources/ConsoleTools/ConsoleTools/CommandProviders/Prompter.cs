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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;

namespace DustInTheWind.ConsoleTools.CommandProviders
{
    /// <summary>
    /// Reads commands from the console.
    /// </summary>
    public class Prompter : BlockControl, IRepeatableSupport
    {
        private bool closeWasRequested;

        public CliCommand LastCommand { get; private set; }

        /// <summary>
        /// Gets or sets the text displayed in the prompter.
        /// </summary>
        public InlineTextBlock PrompterText { get; set; }

        /// <summary>
        /// Gets or sets the glyph displayed after the prompter text.
        /// </summary>
        public InlineTextBlock PrompterGlyph { get; set; } = ">";

        /// <summary>
        /// Gets or sets the count of spaces to be displayed before the prompter (text + glyph).
        /// </summary>
        public int MarginLeft { get; set; } = 0;

        /// <summary>
        /// Gets or sets the count of spaces to be displayed after the prompter (text + glyph), before the user can write his command.
        /// </summary>
        public int MarginRight { get; set; } = 1;

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

        public event EventHandler CloseNeeded;

        /// <summary>
        /// Initializes a new instance of the <see cref="Prompter"/> class.
        /// </summary>
        public Prompter()
        {
            MarginTop = 1;
            MarginBottom = 1;
        }

        protected override void OnBeforeDisplay()
        {
            LastCommand = null;
            closeWasRequested = false;

            base.OnBeforeDisplay();
        }

        protected override void DoDisplayContent()
        {
            while (!closeWasRequested)
            {
                DisplayPrompterText();
                LastCommand = ReadUserInput();

                if (LastCommand != null)
                    return;
            }
        }

        /// <summary>
        /// Displays the whole prompter (margins, text and glyph) to the console.
        /// </summary>
        protected virtual void DisplayPrompterText()
        {
            WriteLeftMargin();

            PrompterText?.Display();
            PrompterGlyph?.Display();

            WriteRightMargin();
        }

        private void WriteLeftMargin()
        {
            if (MarginLeft <= 0)
                return;

            string leftMargin = new string(' ', MarginLeft);
            Console.Write(leftMargin);
        }

        private void WriteRightMargin()
        {
            if (MarginRight > 0)
            {
                string rightMargin = new string(' ', MarginRight);
                Console.Write(rightMargin);
            }
        }

        private CliCommand ReadUserInput()
        {
            string commandText = Console.ReadLine();

            if (commandText == null)
            {
                OnCloseNeeded();
                return null;
            }

            if (commandText.Length == 0)
                return null;

            CliCommand newCommand = CliCommand.Parse(commandText);

            return newCommand.IsEmpty
                ? null
                : newCommand;
        }

        protected override void OnAfterDisplay()
        {
            if (LastCommand == null)
                return;

            try
            {
                NewCommandEventArgs eventArgs = new NewCommandEventArgs(LastCommand);
                OnNewCommand(eventArgs);

                if (eventArgs.Exit)
                    OnCloseNeeded();
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
            }

            base.OnAfterDisplay();
        }

        public void RequestClose()
        {
            closeWasRequested = true;
        }

        protected virtual void OnCloseNeeded()
        {
            CloseNeeded?.Invoke(this, EventArgs.Empty);
        }
    }
}