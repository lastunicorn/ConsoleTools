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

using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents the title row of a table.
    /// </summary>
    public class TitleRow
    {
        private readonly TitleCell titleCell;

        /// <summary>
        /// Gets or sets the <see cref="Table"/> instance that contains the current title.
        /// </summary>
        public Table ParentTable { get; set; }

        /// <summary>
        /// Gets or sets the content of the current instance.
        /// </summary>
        public MultilineText Content
        {
            get { return titleCell.Content; }
            set { titleCell.Content = value; }
        }

        /// <summary>
        /// Gets or sets the content alignment.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get { return titleCell.HorizontalAlignment;}
            set { titleCell.HorizontalAlignment = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleRow"/> class with
        /// empty content.
        /// </summary>
        public TitleRow()
        {
            titleCell = new TitleCell
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
            titleCell = new TitleCell
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
            titleCell = new TitleCell
            {
                ParentRow = this,
                Content = title ?? MultilineText.Empty
            };
        }

        public void Render(ITablePrinter tablePrinter, int minWidth)
        {
            BorderTemplate borderTemplate = ParentTable?.BorderTemplate;

            bool displayBorder = borderTemplate != null && ParentTable?.DisplayBorder == true;

            int cellInnerWidth = displayBorder
                ? minWidth - 2
                : minWidth;

            List<string> cellContents = titleCell.Render(cellInnerWidth, 0);

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