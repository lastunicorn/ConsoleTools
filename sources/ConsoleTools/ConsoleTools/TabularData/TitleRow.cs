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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents the title row of a table.
    /// </summary>
    public class TitleRow
    {
        /// <summary>
        /// Gets or sets the cell displayed in the title row.
        /// This is the unique cell of the row.
        /// </summary>
        public TitleCell TitleCell { get; }

        /// <summary>
        /// Gets or sets the <see cref="DataGrid"/> instance that contains the current title.
        /// </summary>
        public DataGrid ParentDataGrid { get; internal set; }

        /// <summary>
        /// Gets or sets the content alignment.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get => TitleCell.HorizontalAlignment;
            set => TitleCell.HorizontalAlignment = value;
        }

        /// <summary>
        /// Gets a value that specifies if the current instance of the <see cref="TitleRow"/> has a content to be displayed.
        /// </summary>
        public bool HasContent => TitleCell?.Content?.IsEmpty == false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleRow"/> class with
        /// empty content.
        /// </summary>
        public TitleRow()
        {
            TitleCell = new TitleCell
            {
                ParentRow = this,
                Content = MultilineText.Empty
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleRow"/> class with
        /// the text content.
        /// </summary>
        public TitleRow(string title)
        {
            TitleCell = new TitleCell
            {
                ParentRow = this,
                Content = title == null
                    ? MultilineText.Empty
                    : new MultilineText(title)
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleRow"/> class with
        /// a <see cref="MultilineText"/> content.
        /// </summary>
        public TitleRow(MultilineText title)
        {
            TitleCell = new TitleCell
            {
                ParentRow = this,
                Content = title ?? MultilineText.Empty
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleRow"/> class with
        /// an <see cref="object"/> representing the content.
        /// </summary>
        public TitleRow(object title)
        {
            TitleCell = new TitleCell
            {
                ParentRow = this,
                Content = title?.ToString() ?? MultilineText.Empty
            };
        }

        /// <summary>
        /// Calculates the space (in characters) the current instance ocupies without other restrictions.
        /// </summary>
        public Size CalculatePreferredSize()
        {
            bool displayBorder = ParentDataGrid?.DisplayBorder ?? false;

            int titleRowWidth = 0;

            if (displayBorder)
                titleRowWidth += 1;

            Size cellSize = TitleCell.CalculatePreferedSize();
            titleRowWidth += cellSize.Width;

            if (displayBorder)
                titleRowWidth += 1;

            return new Size(titleRowWidth, cellSize.Height);
        }

        /// <summary>
        /// Renders the current instance in the specified <see cref="ITablePrinter"/>.
        /// </summary>
        /// <param name="tablePrinter">The <see cref="ITablePrinter"/> instance that will display the rendered title row.</param>
        /// <param name="size">The minimum width into which the current instance must be rendered.</param>
        public void Render(ITablePrinter tablePrinter, Size size)
        {
            BorderTemplate borderTemplate = ParentDataGrid?.BorderTemplate;

            bool displayBorder = borderTemplate != null && ParentDataGrid?.DisplayBorder == true;

            Size cellSize = displayBorder
                ? size.InflateWidth(-2)
                : size;

            IEnumerable<string> cellContents = TitleCell.Render(cellSize);

            // Write title
            foreach (string line in cellContents)
            {
                if (displayBorder)
                    tablePrinter.WriteBorder(borderTemplate.Left);

                tablePrinter.WriteTitle(line);

                if (displayBorder)
                    tablePrinter.WriteBorder(borderTemplate.Right);

                tablePrinter.WriteLine();
            }
        }
    }
}