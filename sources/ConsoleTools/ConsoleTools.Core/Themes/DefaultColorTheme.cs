using System;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Themes
{
    public class DefaultColorTheme : IColorTheme
    {
        private readonly TextType[] textTypes;

        public DefaultColorTheme()
        {
            textTypes = new[] {
                new TextType(DefaultTextType.Normal, Console.ForegroundColor, Console.BackgroundColor),
                new TextType(DefaultTextType.Inverted, Console.BackgroundColor, Console.ForegroundColor),
                new TextType(DefaultTextType.Emphasized, ConsoleColor.White, null),
                new TextType(DefaultTextType.Success, ConsoleColor.Green, null),
                new TextType(DefaultTextType.Warning, ConsoleColor.Yellow, null),
                new TextType(DefaultTextType.Error, ConsoleColor.Red, null)
            };
        }

        public TextType this[string id]
        {
            get { return textTypes.FirstOrDefault(x => x.Id == id); }
        }
    }
}
