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

namespace DustInTheWind.ConsoleTools.MenuControl.MenuItems
{
    public class LabelMenuItem : IMenuItem
    {
        protected int lastX = -1;
        protected int lastY = -1;
        protected int lastLength = -1;

        public int Id { get; set; }
        public string Text { get; set; }

        public int PaddingLeft { get; set; } = 1;
        public int PaddingRight { get; set; } = 1;
        public HorizontalAlign HorizontalAlign { get; set; } = HorizontalAlign.Left;
        public bool IsSelectable { get; set; } = true;
        public ConsoleKey? ShortcutKey { get; set; }
        public ICommand Command { get; set; }

        public bool IsVisible => VisibilityProvider == null || VisibilityProvider();

        public Func<bool> VisibilityProvider { get; set; }

        public event EventHandler<CancelEventArgs> BeforeSelect;

        public void Display(int x, int y, bool selected, HorizontalAlign horizontalAlign = HorizontalAlign.Default)
        {
            ConsoleColor initialForegroundColor = Console.ForegroundColor;
            ConsoleColor initialBackgroundColor = Console.BackgroundColor;

            try
            {
                Console.ForegroundColor = selected ? initialBackgroundColor : initialForegroundColor;
                Console.BackgroundColor = selected ? initialForegroundColor : initialBackgroundColor;

                string line = Text;

                if (PaddingLeft > 0)
                    line = new string(' ', PaddingLeft) + line;

                if (PaddingRight > 0)
                    line = line + new string(' ', PaddingRight);

                HorizontalAlign calculatedHorizontalAlign = horizontalAlign == HorizontalAlign.Default
                    ? HorizontalAlign
                    : horizontalAlign;

                switch (calculatedHorizontalAlign)
                {
                    case HorizontalAlign.Default:
                    case HorizontalAlign.Left:
                        break;

                    case HorizontalAlign.Center:
                        x = x - line.Length / 2;
                        break;

                    case HorizontalAlign.Right:
                        x = x - line.Length;
                        break;
                }

                Console.SetCursorPosition(x, y);
                Console.Write(line);

                lastX = x;
                lastY = y;
                lastLength = line.Length;
            }
            finally
            {
                Console.ForegroundColor = initialForegroundColor;
                Console.BackgroundColor = initialBackgroundColor;
            }
        }

        public bool Select()
        {
            CancelEventArgs args = new CancelEventArgs();
            OnBeforeSelect(args);

            return !args.Cancel;
        }

        protected virtual void OnBeforeSelect(CancelEventArgs e)
        {
            BeforeSelect?.Invoke(this, e);
        }
    }
}