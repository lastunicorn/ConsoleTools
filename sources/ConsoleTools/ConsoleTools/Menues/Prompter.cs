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
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools.CommandLine;

namespace DustInTheWind.ConsoleTools.Menues
{
    /// <summary>
    /// Provides a way for the user to type a command at the console.
    /// </summary>
    public class Prompter : BlockControl, IRepeatableSupport
    {
        private bool closeWasRequested;

        /// <summary>
        /// Gets the list of items contained by the current instance.
        /// </summary>
        private readonly List<PrompterItem> prompterItems = new List<PrompterItem>();

        public IPrompterCommand UnhandledItemCommand { get; set; } = new UnknownPrompterCommand();

        /// <summary>
        /// Gets the last command read from the console.
        /// </summary>
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
        /// Event raised when the current instance cannot be displayed anymore and it is in the "Closed" state.
        /// The <see cref="ControlRepeater"/> must also end its display loop.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Event raised when a command was not handled.
        /// </summary>
        public event EventHandler<UnhandledCommandEventArgs> UnhandledCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="Prompter"/> class.
        /// </summary>
        public Prompter()
        {
            MarginTop = 1;
            MarginBottom = 1;
        }

        /// <summary>
        /// Adds a new item to the current instance.
        /// </summary>
        /// <param name="prompterItem">The item to be added to the current instance.</param>
        public void AddItem(PrompterItem prompterItem)
        {
            if (prompterItem == null) throw new ArgumentNullException(nameof(prompterItem));

            prompterItems.Add(prompterItem);
        }

        /// <summary>
        /// Adds a list of items to the current instance.
        /// </summary>
        /// <param name="prompterItems">The list of items to be added to the current instance.</param>
        public void AddItems(IEnumerable<PrompterItem> prompterItems)
        {
            if (prompterItems == null) throw new ArgumentNullException(nameof(prompterItems));

            bool existsNullItems = prompterItems.Any(x => x == null);

            if (existsNullItems)
                throw new ArgumentException("Null items are not accepted.", nameof(prompterItems));

            this.prompterItems.AddRange(prompterItems.Where(x => x != null));
        }

        /// <summary>
        /// Erases all the information of the previous display.
        /// </summary>
        protected override void OnBeforeDisplay()
        {
            LastCommand = null;
            closeWasRequested = false;

            base.OnBeforeDisplay();
        }

        /// <summary>
        /// Displays the menu and waits for the user to choose an item.
        /// This method blocks until the user chooses an item.
        /// </summary>
        protected override void DoDisplayContent()
        {
            bool success = false;

            while (!success && !closeWasRequested)
            {
                DisplayPrompterText();
                success = ReadUserInput();
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

        protected override void OnAfterDisplay()
        {
            base.OnAfterDisplay();

            if (LastCommand != null)
            {
                bool isHandled = AnnounceNewCommand();

                if (!isHandled)
                    isHandled = ExecuteAssociatedItem();

                if (!isHandled)
                    AnnounceUnhandledCommand();
            }
        }

        private bool AnnounceNewCommand()
        {
            NewCommandEventArgs eventArgs = new NewCommandEventArgs(LastCommand);

            try
            {
                OnNewCommand(eventArgs);
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
            }

            return eventArgs.IsHandled;
        }

        private bool ExecuteAssociatedItem()
        {
            PrompterItem prompterItem = prompterItems.FirstOrDefault(x => x.Name == LastCommand.Name);

            if (prompterItem == null)
                return false;

            prompterItem.Execute(LastCommand);
            return true;
        }

        private void AnnounceUnhandledCommand()
        {
            UnhandledCommandEventArgs eventArgs = new UnhandledCommandEventArgs(LastCommand);
            OnUnhandledCommand(eventArgs);
        }

        /// <summary>
        /// The <see cref="ControlRepeater"/> calls this method to announce the control that it should end its process.
        /// </summary>
        public void RequestClose()
        {
            closeWasRequested = true;
            OnClosed();
        }

        /// <summary>
        /// Raises the <see cref="NewCommand"/> event.
        /// </summary>
        /// <param name="e">A <see cref="NewCommandEventArgs"/> that contains the event data.</param>
        protected virtual void OnNewCommand(NewCommandEventArgs e)
        {
            NewCommand?.Invoke(null, e);
        }

        /// <summary>
        /// Raises the <see cref="Closed"/> event.
        /// </summary>
        protected virtual void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises the <see cref="UnhandledCommand"/> event.
        /// </summary>
        /// <param name="e">A <see cref="UnhandledCommandEventArgs"/> that contains the event data.</param>
        protected virtual void OnUnhandledCommand(UnhandledCommandEventArgs e)
        {
            UnhandledCommand?.Invoke(this, e);
        }
    }
}