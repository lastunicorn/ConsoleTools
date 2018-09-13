// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Provides base functionality for a block control like top/bottom margin.
    /// A block control does not accept other controls on the same horizontal.
    /// It starts from the beginning of the next line if the currsor is not already
    /// at the beginning of the line.
    /// It also provides a top and a bottom margin.
    /// </summary>
    public abstract class BlockControl : Control
    {
        /// <summary>
        /// Gets or sets a value that specifies who should be considered the parent if none is specified.
        /// This is useful for calculating the alignment for example.
        /// </summary>
        public DefaultParent DefaultParent { get; set; } = DefaultParent.ConsoleWindow;

        /// <summary>
        /// Gets or sets the width of the control. It does not including the left and right margins.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the minimum width allowed for the control.
        /// </summary>
        public int? MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the maximum width allowed for the control.
        /// </summary>
        public int? MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies the horizontal position of the control in respect to its parent container.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the amount of space that should be empty outside the control.
        /// </summary>
        public Thickness Margin { get; set; }

        /// <summary>
        /// Gets or sets the amount of space between the content and the margin of the control.
        /// </summary>
        public Thickness Padding { get; set; }

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
        /// Gets the width available for the control to render itself.
        /// </summary>
        /// <remarks>
        /// The parent's control is deciding this space.
        /// </remarks>
        protected int AvailableWidth
        {
            get
            {
                switch (DefaultParent)
                {
                    case DefaultParent.ConsoleBuffer:
                        return Console.BufferWidth;

                    case DefaultParent.ConsoleWindow:
                        return Console.WindowWidth;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Gets the actual width of the control including the left and right margins.
        /// </summary>
        /// <remarks>
        /// This value is equal to the available width if the control is stretched.
        /// </remarks>
        public int ActualFullWidth => AvailableWidth;

        /// <summary>
        /// Gets the actual width of the control without the left and right margins.
        /// </summary>
        protected int ActualWidth => ActualFullWidth - Margin.Left - Margin.Right;

        /// <summary>
        /// Gets the actual width of the client area (without the left and right paddings).
        /// </summary>
        protected int ActualClientWidth => ActualWidth - Padding.Left - Padding.Right;

        /// <summary>
        /// Gets the actual width of the content.
        /// </summary>
        protected int ActualContentWidth
        {
            get
            {
                int availableClientWidth = AvailableWidth - Margin.Left - Margin.Right - Padding.Left - Padding.Right;
                return Math.Min(CalculatedContentWidth, availableClientWidth);
            }
        }

        /// <summary>
        /// When implemented by an inheritor, gets the actual height of the content calculated
        /// taking in account the <see cref="ActualContentWidth"/>.
        /// </summary>
        protected virtual int ActualContentHeight { get; }

        public int ActualHeight => ActualContentHeight + Padding.Top + Padding.Bottom;

        public int ActualFullHeight => ActualContentHeight + Margin.Top + Margin.Bottom;


        /// <summary>
        /// Gets the desired width of the control calculated by honoring the
        /// <see cref="Width"/>, <see cref="MinWidth"/> and <see cref="MaxWidth"/>.
        /// </summary>
        protected int CalculatedContentWidth
        {
            get
            {
                if (Width == null)
                {
                    if (MinWidth == null)
                    {
                        if (MaxWidth == null)
                        {
                            return DesiredContentWidth;
                        }
                        else
                        {
                            int contentMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
                            return Math.Min(DesiredContentWidth, contentMaxWidth);
                        }
                    }
                    else
                    {
                        if (MaxWidth == null)
                        {
                            int contentMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                            return Math.Max(DesiredContentWidth, contentMinWidth);
                        }
                        else
                        {
                            int contentMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                            int contentMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
                            return Math.Max(Math.Min(DesiredContentWidth, contentMaxWidth), contentMinWidth);
                        }
                    }
                }
                else
                {
                    if (MinWidth == null)
                    {
                        if (MaxWidth == null)
                        {
                            return Width.Value;
                        }
                        else
                        {
                            int contentMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
                            return Math.Min(Width.Value, contentMaxWidth);
                        }
                    }
                    else
                    {
                        if (MaxWidth == null)
                        {
                            int contentMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                            return Math.Max(Width.Value, contentMinWidth);
                        }
                        else
                        {
                            int contentMinWidth = MinWidth.Value - Padding.Left - Padding.Right;
                            int contentMaxWidth = MaxWidth.Value - Padding.Left - Padding.Right;
                            return Math.Max(Math.Min(Width.Value, contentMaxWidth), contentMinWidth);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// When implemented by an inheritor, gets the width of the content when no restrictions are applied of any kind.
        /// Not event the Width, MinWidth and MaxWidth are honored.
        /// </summary>
        protected virtual int DesiredContentWidth { get; }

        /// <summary>
        /// Event raised immediately before writting the top margin.
        /// </summary>
        public event EventHandler BeforeTopMargin;

        /// <summary>
        /// Event raised immediately after writting the bottom margin.
        /// </summary>
        public event EventHandler AfterBottomMargin;

        /// <summary>
        /// Displays the margins and the content of the control.
        /// It also ensures that the control is displayed starting from a new line.
        /// </summary>
        protected override void DoDisplay()
        {
            MoveToNextLineIfNecessary();

            WriteTopMargin();
            WriteTopPadding();
            DoDisplayContent();
            WriteBottomPadding();
            WriteBottomMargin();
        }

        private static void MoveToNextLineIfNecessary()
        {
            if (Console.CursorLeft != 0)
                Console.WriteLine();
        }

        /// <summary>
        /// When implemented by an inheritor it displays the content of the control to the console.
        /// </summary>
        protected abstract void DoDisplayContent();

        private void WriteTopMargin()
        {
            OnBeforeTopMargin();

            for (int i = 0; i < Margin.Top; i++)
                Console.WriteLine();
        }

        private void WriteBottomMargin()
        {
            for (int i = 0; i < Margin.Bottom; i++)
                Console.WriteLine();

            OnAfterBottomMargin();
        }

        private void WriteTopPadding()
        {
            if (Padding.Top <= 0)
                return;

            string text = new string(' ', ActualContentWidth);

            for (int i = 0; i < Padding.Top; i++)
                WriteTextLine(text);
        }

        private void WriteBottomPadding()
        {
            if (Padding.Bottom <= 0)
                return;

            string text = new string(' ', ActualContentWidth);

            for (int i = 0; i < Padding.Bottom; i++)
                WriteTextLine(text);
        }

        /// <summary>
        /// Helper method that writes the specified text to the console using the
        /// <see cref="ForegroundColor"/> and <see cref="BackgroundColor"/> values.
        /// </summary>
        /// <param name="text">The text to be written to the console.</param>
        protected void WriteText(string text)
        {
            if (!ForegroundColor.HasValue && !BackgroundColor.HasValue)
                CustomConsole.Write(text);
            else if (ForegroundColor.HasValue && BackgroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, BackgroundColor.Value, text);
            else if (ForegroundColor.HasValue)
                CustomConsole.Write(ForegroundColor.Value, text);
            else
                CustomConsole.WriteBackgroundColor(BackgroundColor.Value, text);
        }

        protected void WriteTextLine(string text)
        {
            StartTextLine();

            if (text.Length < ActualContentWidth)
                text += new string(' ', ActualContentWidth - text.Length);

            WriteText(text);

            EndTextLine();
        }

        private void StartTextLine()
        {
            WriteLeftEmptySpace();
            WriteLeftMargin();
            WriteLeftPadding();
        }

        private void EndTextLine()
        {
            WriteRightPadding();
            WriteRightMargin();
            WriteRightEmptySpace();

            // Decide if new line is needed.
            if (ActualFullWidth % Console.BufferWidth != 0)
                Console.WriteLine();
        }

        private void WriteLeftEmptySpace()
        {
            int availableWidth = AvailableWidth;
            int fullWidth = ActualFullWidth;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    break;

                case HorizontalAlignment.Center:
                    {
                        int allSpaces = availableWidth - fullWidth;
                        double halfSpaces = (double)allSpaces / 2;
                        int leftSpaces = (int)Math.Floor(halfSpaces);
                        Console.Write(new string(' ', leftSpaces));
                        break;
                    }

                case HorizontalAlignment.Right:
                    {
                        int allSpaces = availableWidth - fullWidth;
                        Console.Write(new string(' ', allSpaces));
                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WriteRightEmptySpace()
        {
            int availableWidth = AvailableWidth;
            int fullWidth = ActualFullWidth;

            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Default:
                case HorizontalAlignment.Left:
                    {
                        int allSpaces = availableWidth - fullWidth;
                        Console.Write(new string(' ', allSpaces));
                        break;
                    }

                case HorizontalAlignment.Center:
                    {
                        int allSpaces = availableWidth - fullWidth;
                        double halfSpaces = (double)allSpaces / 2;
                        int leftSpaces = (int)Math.Ceiling(halfSpaces);
                        Console.Write(new string(' ', leftSpaces));
                        break;
                    }

                case HorizontalAlignment.Right:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WriteLeftMargin()
        {
            if (Margin.Left <= 0)
                return;

            string text = new string(' ', Margin.Left);
            Console.Write(text);
        }

        private void WriteRightMargin()
        {
            if (Margin.Right <= 0)
                return;

            string text = new string(' ', Margin.Right);
            Console.Write(text);
        }

        private void WriteLeftPadding()
        {
            if (Padding.Left <= 0)
                return;

            string text = new string(' ', Padding.Left);
            WriteText(text);
        }

        private void WriteRightPadding()
        {
            if (Padding.Right <= 0)
                return;

            string text = new string(' ', Padding.Right);
            WriteText(text);
        }

        /// <summary>
        /// Method called immediately before writting the top margin.
        /// </summary>
        protected virtual void OnBeforeTopMargin()
        {
            BeforeTopMargin?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method called immediately after writting the bottom margin.
        /// </summary>
        protected virtual void OnAfterBottomMargin()
        {
            AfterBottomMargin?.Invoke(this, EventArgs.Empty);
        }
    }
}