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