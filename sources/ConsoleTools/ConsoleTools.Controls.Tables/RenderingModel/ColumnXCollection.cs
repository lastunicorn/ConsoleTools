// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;

internal class ColumnXCollection : IEnumerable<ColumnX>
{
    private readonly List<ColumnX> columns = new();

    public bool HasBorders { get; set; }

    public int MinWidth { get; set; }

    public int MaxWidth { get; set; } = int.MaxValue;

    public int ActualWidth { get; private set; }

    public ColumnX this[int index] => columns[index];

    public int Count => columns.Count;

    public void AddColumn(ColumnX column)
    {
        if (column == null) throw new ArgumentNullException(nameof(column));

        columns.Add(column);
    }

    /// <summary>
    /// After all columns were added, the column span and grid min width information must be
    /// processed and the columns to be adjusted accordingly.
    /// </summary>
    public void PerformLayout()
    {
        if (columns.Count <= 0)
            return;

        AccomodateColumnSpans();
        AdjustForMinWidthAndMaxWidth();
    }

    private void AccomodateColumnSpans()
    {
        for (int i = 0; i < columns.Count; i++)
        {
            ColumnX column = columns[i];

            foreach (ColumnSpanX columnSpanX in column.Spans)
            {
                int span = columnSpanX.Span ?? int.MaxValue;
                int preferredWidth = columnSpanX.PreferredWidth;
                int minWidth = columnSpanX.MinWidth;

                InflateColumns(i, span, minWidth, true);
                InflateColumns(i, span, preferredWidth, false);
            }
        }

        ActualWidth = CalculateTotalWidth();
    }

    private void AdjustForMinWidthAndMaxWidth()
    {
        if (ActualWidth < MinWidth)
        {
            int delta = MinWidth - ActualWidth;
            InflateEntireGrid(delta);

            ActualWidth = CalculateTotalWidth();
        }
        else if (ActualWidth > MaxWidth)
        {
            int delta = ActualWidth - MaxWidth;
            DeflateEntireGrid(delta);

            ActualWidth = CalculateTotalWidth();
        }
    }

    private int CalculateTotalWidth()
    {
        int totalWidth = columns
            .Sum(x => x.ActualWidth);

        if (HasBorders)
            totalWidth += columns.Count + 1;

        return totalWidth;
    }

    private void InflateColumns(int startColumnIndex, int columnCount, int desiredInnerWidth, bool ensureWidth)
    {
        ColumnX[] spanColumns = columns
            .Skip(startColumnIndex)
            .Take(columnCount)
            .ToArray();

        int actualWidth = spanColumns
            .Sum(x => x.ActualWidth);

        if (HasBorders)
            actualWidth += spanColumns.Length - 1;

        if (actualWidth < desiredInnerWidth)
        {
            int diffWidth = desiredInnerWidth - actualWidth;

            int smallIncreaseWidth = diffWidth / spanColumns.Length;
            int bigIncreaseWidth = smallIncreaseWidth + 1;

            int bigColumnCount = diffWidth % spanColumns.Length;

            for (int i = startColumnIndex; i < startColumnIndex + bigColumnCount; i++)
            {
                columns[i].ActualWidth += bigIncreaseWidth;

                if (ensureWidth)
                    columns[i].MinWidth += columns[i].ActualWidth;
            }

            for (int i = startColumnIndex + bigColumnCount; i < startColumnIndex + spanColumns.Length; i++)
            {
                columns[i].ActualWidth += smallIncreaseWidth;

                if (ensureWidth)
                    columns[i].MinWidth += columns[i].ActualWidth;
            }
        }
    }

    public int GetCellWidth(int cellIndex, int columnSpan = 1)
    {
        if (columnSpan == 1)
            return columns[cellIndex].ActualWidth;

        ColumnX[] spannedColumns = columns
            .Skip(cellIndex)
            .Take(columnSpan)
            .ToArray();

        int contentWidth = spannedColumns
            .Sum(x => x.ActualWidth);

        bool shouldAddBorders = HasBorders && spannedColumns.Length > 0;

        return shouldAddBorders
            ? contentWidth + spannedColumns.Length - 1
            : contentWidth;
    }

    private void InflateEntireGrid(int deltaWidth)
    {
        int smallIncreaseWidth = deltaWidth / columns.Count;
        int bigIncreaseWidth = smallIncreaseWidth + 1;

        int bigColumnCount = deltaWidth % columns.Count;

        for (int i = 0; i < bigColumnCount; i++)
            columns[i].ActualWidth += bigIncreaseWidth;

        for (int i = bigColumnCount; i < columns.Count; i++)
            columns[i].ActualWidth += smallIncreaseWidth;
    }

    private void DeflateEntireGrid(int deltaWidth)
    {
        int columnContentTotalWidth = columns
            .Where(x => x.AllowToShrink)
            .Sum(x => x.ActualWidth - x.MinWidth);

        if (columnContentTotalWidth < deltaWidth)
        {
            foreach (ColumnX column in columns)
                column.ShrinkToMinimum();
        }
        else
        {
            double reductionPercentage = (double)deltaWidth * 100 / columnContentTotalWidth;

            foreach (ColumnX column in columns)
            {
                int columnDelta = (int)Math.Round(reductionPercentage * column.ActualWidth / 100);
                column.ShrinkBy(columnDelta);
            }
        }
    }

    public IEnumerator<ColumnX> GetEnumerator()
    {
        return columns.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public ColumnX GetOrCreate(int index)
    {
        while (columns.Count <= index)
            columns.Add(new ColumnX());

        return columns[index];
    }
}