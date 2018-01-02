// ConsoleTools
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

namespace DustInTheWind.ConsoleTools.InputControls
{
    /// <summary>
    /// Displays a value to the Console outut.
    /// </summary>
    public class TextOutputControl
    {
        private readonly Label labelControl = new Label();
        
        /// <summary>
        /// Gets or sets the amount of space to be displayed between the label and the value.
        /// </summary>
        public int SpaceAfterLabel
        {
            get { return labelControl.MarginRight; }
            set { labelControl.MarginRight = value; }
        }

        /// <summary>
        /// Writes a value to the Console output.
        /// </summary>
        public void Write<T>(string label, T value)
        {
            labelControl.Text = label;
            labelControl.Display();

            CustomConsole.WriteLine(value);
        }
    }
}