// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls.Spinners.Templates
{
    /// <summary>
    /// A template for the <see cref="Spinner"/> that is configured with a list of strings,
    /// representing the frames of the <see cref="Spinner"/>.
    /// Can be used as base class for other templates.
    /// </summary>
    public class SequenceSpinnerTemplate : ISpinnerTemplate
    {
        private int counter;
        private readonly string[] sequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceSpinnerTemplate"/> class with
        /// the list of frames.
        /// </summary>
        /// <param name="sequence">The list of frames.</param>
        public SequenceSpinnerTemplate(string[] sequence)
        {
            this.sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
        }

        /// <summary>
        /// Resets the template. It will start from the first frame.
        /// </summary>
        public void Reset()
        {
            counter = -1;
        }

        /// <summary>
        /// Moves to the next frame and returns it.
        /// </summary>
        public string GetNext()
        {
            counter++;

            if (counter >= sequence.Length)
                counter = 0;

            return sequence[counter];
        }

        /// <summary>
        /// Returns the current frame.
        /// </summary>
        public string GetCurrent()
        {
            if (counter == -1)
                counter = 0;

            return sequence[counter];
        }
    }
}