using System;

namespace DustInTheWind.ConsoleTools.Themes
{
    public class TextType
    {
        public string Id { get; }
        public ConsoleColor? ForegroundColor { get; }
        public ConsoleColor? BackgroundColor { get; }

        public TextType(string id, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}