using System;
using DustInTheWind.ConsoleTools.Themes;

namespace DustInTheWind.ConsoleTools
{
    internal class ConsoleX
    {
        private IColorTheme colorTheme = new DefaultColorTheme();

        public IColorTheme ColorTheme
        {
            get => colorTheme;
            set => colorTheme = value ?? throw new ArgumentNullException(nameof(ColorTheme));
        }
    }
}