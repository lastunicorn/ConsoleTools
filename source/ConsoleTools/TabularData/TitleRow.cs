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

namespace DustInTheWind.ConsoleTools.TabularData
{
    internal class TitleRow
    {
        public Table ParentTable { get; set; }
        public MultilineText Text { get; set; }

        public TitleRow()
        {
            Text = MultilineText.Empty;
        }

        public TitleRow(string title)
        {
            Text = title == null
                ? MultilineText.Empty
                : new MultilineText(title);
        }

        public TitleRow(MultilineText title)
        {
            Text = title ?? MultilineText.Empty;
        }

        public void Render(int minWidth, ITablePrinter tablePrinter)
        {
            bool displayBorder = ParentTable?.DisplayBorder ?? false;

            int paddingLeftLength = ParentTable?.PaddingLeft ?? 0;
            int paddingRightLength = ParentTable?.PaddingRight ?? 0;

            int cellInnerWidth = minWidth - paddingLeftLength - paddingRightLength;

            if (displayBorder)
                cellInnerWidth -= 2;

            BorderTemplate borderTemplate = ParentTable?.BorderTemplate;

            // Write title
            for (int titleLineIndex = 0; titleLineIndex < Text.Size.Height; titleLineIndex++)
            {
                if (displayBorder && borderTemplate != null)
                    tablePrinter.WriteBorder(borderTemplate.Left);

                string leftPadding = string.Empty.PadRight(paddingLeftLength, ' ');
                string rightPadding = string.Empty.PadRight(paddingRightLength, ' ');
                string innerContent = Text.Lines[titleLineIndex].PadRight(cellInnerWidth, ' ');
                string content = string.Concat(leftPadding, innerContent, rightPadding);

                tablePrinter.WriteTitle(content);

                if (displayBorder && borderTemplate != null)
                    tablePrinter.WriteBorder(borderTemplate.Right);

                tablePrinter.WriteLine();
            }
        }
    }
}