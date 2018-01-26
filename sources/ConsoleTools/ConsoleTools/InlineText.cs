using System;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Represents a text to be displayed in the console.
    /// </summary>
    public class InlineText
    {
        /// <summary>
        /// Gets or sets the number of spaces to be written before the text (to the left).
        /// Default value: 0
        /// </summary>
        public int MarginLeft { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to be written after the text (to the right).
        /// Default value: 0
        /// </summary>
        public int MarginRight { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the foreground color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background color used to write the text.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleColor? BackgroundColor { get; set; }

        /// <summary>
        /// Displays the control in the Console.
        /// </summary>
        public void Display()
        {
            if (MarginLeft > 0)
                DisplayLeftMargin();

            if (!ForegroundColor.HasValue && !BackgroundColor.HasValue)
                CustomConsole.Write(Text);
            else if (ForegroundColor.HasValue && BackgroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, BackgroundColor.Value, Text);
            else if (ForegroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, Text);
            else
                CustomConsole.WriteBackgroundColor(BackgroundColor.Value, Text);

            if (MarginRight > 0)
                DisplayRightMargin();
        }

        private void DisplayLeftMargin()
        {
            string space = new string(' ', MarginLeft);
            Console.Write(space);
        }

        private void DisplayRightMargin()
        {
            string space = new string(' ', MarginRight);
            Console.Write(space);
        }
    }
}