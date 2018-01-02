// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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

using System;
using System.ComponentModel;
using System.Text;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools.MenuControl.MenuItems
{
    public class LabelMenuItem : IMenuItem
    {
        private const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;

        protected int lastX = -1;
        protected int lastY = -1;
        protected int lastLength = -1;

        public int Id { get; set; }
        public string Text { get; set; }

        public int PaddingLeft { get; set; } = 1;
        public int PaddingRight { get; set; } = 1;
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Default;
        public bool IsSelectable { get; set; } = true;
        public ConsoleKey? ShortcutKey { get; set; }
        public ICommand Command { get; set; }
        public SelectableMenu ParentMenu { get; set; }

        public bool IsVisible => VisibilityProvider == null || VisibilityProvider();

        public Func<bool> VisibilityProvider { get; set; }

        public event EventHandler<CancelEventArgs> BeforeSelect;

        public void Display(Size size, bool highlighted)
        {
            StringBuilder calculatedText = CalculateText();

            HorizontalAlignment calculatedHorizontalAlignment = CalculatedHorizontalAlignment();

            int emptySpace = size.Width - calculatedText.Length;

            lastX = Console.CursorLeft;
            lastY = Console.CursorTop;

            switch (calculatedHorizontalAlignment)
            {
                default:
                    if (highlighted)
                        CustomConsole.WriteColor(Console.BackgroundColor, Console.ForegroundColor, calculatedText.ToString());
                    else
                        CustomConsole.Write(calculatedText.ToString());

                    CustomConsole.Write(new string(' ', emptySpace));
                    break;

                case HorizontalAlignment.Center:
                    int leftEmptySpace = emptySpace / 2;
                    CustomConsole.Write(new string(' ', leftEmptySpace));

                    if (highlighted)
                        CustomConsole.WriteColor(Console.BackgroundColor, Console.ForegroundColor, calculatedText.ToString());
                    else
                        CustomConsole.Write(calculatedText.ToString());

                    int rightEmptySpace = emptySpace - leftEmptySpace;
                    CustomConsole.Write(new string(' ', rightEmptySpace));
                    break;

                case HorizontalAlignment.Right:
                    CustomConsole.Write(new string(' ', emptySpace));

                    if (highlighted)
                        CustomConsole.WriteColor(Console.BackgroundColor, Console.ForegroundColor, calculatedText.ToString());
                    else
                        CustomConsole.Write(calculatedText.ToString());
                    break;
            }

            lastLength = calculatedText.Length;
        }

        private StringBuilder CalculateText()
        {
            StringBuilder sb = new StringBuilder();

            if (PaddingLeft > 0)
                sb.Append(new string(' ', PaddingLeft));

            sb.Append(Text);

            if (PaddingRight > 0)
                sb.Append(new string(' ', PaddingRight));

            return sb;
        }

        private HorizontalAlignment CalculatedHorizontalAlignment()
        {
            if (HorizontalAlignment != HorizontalAlignment.Default)
                return HorizontalAlignment;

            if (ParentMenu != null && ParentMenu.ItemsHorizontalAlignment != HorizontalAlignment.Default)
                return ParentMenu.ItemsHorizontalAlignment;

            return DefaultHorizontalAlignment;
        }

        public bool Select()
        {
            CancelEventArgs args = new CancelEventArgs();
            OnBeforeSelect(args);

            return !args.Cancel;
        }

        public Size Measure()
        {
            StringBuilder calculatedText = CalculateText();
            return new Size(calculatedText.Length, 1);
        }

        protected virtual void OnBeforeSelect(CancelEventArgs e)
        {
            BeforeSelect?.Invoke(this, e);
        }
    }
}