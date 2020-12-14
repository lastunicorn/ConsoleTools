// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    public class DataGridBorder
    {
        /// <summary>
        /// Gets or sets the <see cref="DataGrid"/> that contains the current instance.
        /// </summary>
        public DataGrid ParentDataGrid { get; internal set; }

        /// <summary>
        /// Gets or sets a value that specifies if the border is displayed or not.
        /// Default value: <c>true</c>
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets the template to be used.
        /// </summary>
        public BorderTemplate Template { get; set; } = BorderTemplate.PlusMinusBorderTemplate;

        /// <summary>
        /// Gets or sets the foreground color for the border.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color for the border.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Gets a value that specifies if border lines should be drawn between rows.
        /// Default value: false
        /// </summary>
        public bool DisplayBorderBetweenRows { get; set; }

        public ConsoleColor? CalculateForegroundColor()
        {
            return ForegroundColor ??
                   ParentDataGrid?.ForegroundColor;
        }

        public ConsoleColor? CalculateBackgroundColor()
        {
            return BackgroundColor ??
                   ParentDataGrid?.BackgroundColor;
        }
    }
}