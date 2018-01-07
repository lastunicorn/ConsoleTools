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

namespace DustInTheWind.ConsoleTools.Spinners.Templates
{
    /// <summary>
    /// A template for the <see cref="Spinner"/> that is configured with a list of strings,
    /// representing the frames of the <see cref="Spinner"/>.
    /// Can be used as base class for other templates.
    /// </summary>
    public class SequenceTemplate : ISpinnerTemplate
    {
        private int counter;
        private readonly string[] sequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceTemplate"/> class with
        /// the list of frames.
        /// </summary>
        /// <param name="sequence">The list of frames.</param>
        public SequenceTemplate(string[] sequence)
        {
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            this.sequence = sequence;
        }

        /// <summary>
        /// Resets the template. It will start from the first frame.
        /// </summary>
        public void Reset()
        {
            counter = -1;
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
    }
}