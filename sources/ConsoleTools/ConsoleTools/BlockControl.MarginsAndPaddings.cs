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

namespace DustInTheWind.ConsoleTools
{
    partial class BlockControl
    {
        /// <summary>
        /// Gets or sets the amount of space that should be empty outside the control.
        /// </summary>
        public Thickness Margin { get; set; }

        /// <summary>
        /// Gets or sets the amount of space between the content and the margin of the control.
        /// </summary>
        public Thickness Padding { get; set; }

        /// <summary>
        /// Event raised immediately before writting the top margin.
        /// </summary>
        public event EventHandler BeforeTopMargin;

        /// <summary>
        /// Event raised immediately after writting the bottom margin.
        /// </summary>
        public event EventHandler AfterBottomMargin;

        private void WriteTopMargin()
        {
            OnBeforeTopMargin();

            if (controlLayout.MarginTop <= 0)
                return;

            for (int i = 0; i < controlLayout.MarginTop; i++)
                Console.WriteLine();
        }

        private void WriteBottomMargin()
        {
            if (controlLayout.MarginBottom > 0)
            {
                for (int i = 0; i < controlLayout.MarginBottom; i++)
                    Console.WriteLine();
            }

            OnAfterBottomMargin();
        }

        private void WriteTopPadding()
        {
            if (controlLayout.PaddingTop <= 0)
                return;

            string text = new string(' ', controlLayout.ActualContentWidth);

            for (int i = 0; i < controlLayout.PaddingTop; i++)
                WriteTextLine(text);
        }

        private void WriteBottomPadding()
        {
            if (controlLayout.PaddingBottom <= 0)
                return;

            string text = new string(' ', controlLayout.ActualContentWidth);

            for (int i = 0; i < controlLayout.PaddingBottom; i++)
                WriteTextLine(text);
        }

        private void WriteLeftMargin()
        {
            if (controlLayout.MarginLeft <= 0)
                return;

            string text = new string(' ', controlLayout.MarginLeft);
            Console.Write(text);
        }

        private void WriteRightMargin()
        {
            if (controlLayout.MarginRight <= 0)
                return;

            string text = new string(' ', controlLayout.MarginRight);
            Console.Write(text);
        }

        private void WriteLeftPadding()
        {
            if (controlLayout.PaddingLeft <= 0)
                return;

            string text = new string(' ', controlLayout.PaddingLeft);
            WriteText(text);
        }

        private void WriteRightPadding()
        {
            if (controlLayout.PaddingRight <= 0)
                return;

            string text = new string(' ', controlLayout.PaddingRight);
            WriteText(text);
        }

        /// <summary>
        /// Method called immediately before writting the top margin.
        /// </summary>
        protected virtual void OnBeforeTopMargin()
        {
            BeforeTopMargin?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method called immediately after writting the bottom margin.
        /// </summary>
        protected virtual void OnAfterBottomMargin()
        {
            AfterBottomMargin?.Invoke(this, EventArgs.Empty);
        }
    }
}