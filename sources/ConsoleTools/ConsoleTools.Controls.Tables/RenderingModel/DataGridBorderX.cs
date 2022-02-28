using System;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class DataGridBorderX
    {
        public BorderTemplate Template { get; set; }

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public int TotalWidth { get; set; }

        public List<ColumnX> ColumnWidths { get; set; }

        public void RenderRowLeftBorder(ITablePrinter tablePrinter)
        {
            tablePrinter.Write(Template.Left, ForegroundColor, BackgroundColor);
        }

        public void RenderRowRightBorder(ITablePrinter tablePrinter)
        {
            tablePrinter.Write(Template.Right, ForegroundColor, BackgroundColor);
        }

        public void RenderRowInsideBorder(ITablePrinter tablePrinter)
        {
            tablePrinter.Write(Template.Vertical, ForegroundColor, BackgroundColor);
        }

        public static DataGridBorderX CreateFrom(DataGridBorder dataGridBorder)
        {
            if (dataGridBorder == null)
                return new DataGridBorderX();

            return new DataGridBorderX
            {
                Template = dataGridBorder.Template,
                ForegroundColor = dataGridBorder.CalculateForegroundColor(),
                BackgroundColor = dataGridBorder.CalculateBackgroundColor()
            };
        }
    }
}