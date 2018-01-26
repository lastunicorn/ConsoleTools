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
        /// Initializes a new instance of the <see cref="InlineText"/> class.
        /// </summary>
        public InlineText()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineText"/> class with
        /// the text.
        /// </summary>
        public InlineText(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineText"/> class with
        /// the text and the foreground color.
        /// </summary>
        public InlineText(string text, ConsoleColor foregroundColor)
        {
            Text = text;
            ForegroundColor = foregroundColor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineText"/> class with
        /// the text, the foreground color and the background color.
        /// </summary>
        public InlineText(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Text = text;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

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

        public static implicit operator InlineText(string text)
        {
            return new InlineText(text);
        }

        public static implicit operator string(InlineText inlineText)
        {
            return inlineText.Text;
        }
    }
}