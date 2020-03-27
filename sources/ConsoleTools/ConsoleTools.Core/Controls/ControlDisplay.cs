using System;

namespace DustInTheWind.ConsoleTools
{
    public class ControlDisplay
    {
        private ConsoleColor? initialForegroundColor;
        private ConsoleColor? initialBackgroundColor;

        public int RowCount { get; private set; }

        public ControlLayout Layout { get; set; }

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

        public void WriteRow(string text)
        {
            WriteRow(ForegroundColor, BackgroundColor, text);
        }

        public void WriteRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            StartRow(foregroundColor, backgroundColor);
            Write(text);
            EndRow();
        }

        public void WriteRow()
        {
            StartRow();
            EndRow();
        }

        public void StartRow()
        {
            StartRow(ForegroundColor, BackgroundColor);
        }

        public void StartRow(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
        {
            WriteOuterLeftEmptySpace();
            WriteLeftMargin();

            SetCustomForegroundColor(foregroundColor);
            SetCustomBackgroundColor(backgroundColor);

            WriteLeftPadding();
        }

        private void SetCustomForegroundColor(ConsoleColor? foregroundColor)
        {
            ConsoleColor? calculatedForegroundColor = foregroundColor ?? ForegroundColor;

            if (calculatedForegroundColor.HasValue)
            {
                initialForegroundColor = Console.ForegroundColor;
                Console.ForegroundColor = calculatedForegroundColor.Value;
            }
            else
            {
                initialForegroundColor = null;
            }
        }

        private void SetCustomBackgroundColor(ConsoleColor? backgroundColor)
        {
            ConsoleColor? calculatedBackgroundColor = backgroundColor ?? BackgroundColor;

            if (calculatedBackgroundColor.HasValue)
            {
                initialBackgroundColor = Console.BackgroundColor;
                Console.BackgroundColor = calculatedBackgroundColor.Value;
            }
            else
            {
                initialBackgroundColor = null;
            }
        }

        public void EndRow()
        {
            bool isConsoleRowFilled = FillEmptySpace();
            WriteRightPadding();

            RestoreForegroundColor();
            RestoreBackgroundColor();

            WriteRightMargin();
            WriteOuterRightEmptySpace();

            if (!isConsoleRowFilled)
                Console.WriteLine();

            RowCount++;
        }

        private bool FillEmptySpace()
        {
            if (Layout == null)
                return false;

            int cursorLeft = Console.CursorLeft;

            if (cursorLeft >= Layout.ActualFullWidth)
                return false;

            int marginRight = Layout.MarginRight;
            int paddingRight = Layout.PaddingRight;
            int emptySpaceRight = Layout.ActualFullWidth - cursorLeft - paddingRight - marginRight;

            if (emptySpaceRight <= 0)
                return false;

            string rightContentEmptySpace = new string(' ', emptySpaceRight);
            CustomConsole.Write(rightContentEmptySpace);

            bool isConsoleRowFilled = cursorLeft + emptySpaceRight + paddingRight + marginRight == Console.BufferWidth;
            return isConsoleRowFilled;
        }

        private void RestoreForegroundColor()
        {
            if (initialForegroundColor.HasValue)
                Console.ForegroundColor = initialForegroundColor.Value;
        }

        private void RestoreBackgroundColor()
        {
            if (initialBackgroundColor.HasValue)
                Console.BackgroundColor = initialBackgroundColor.Value;
        }

        public void Write(string text)
        {
            if (text == null)
                return;

            CustomConsole.Write(text);

            //if (text == null)
            //{
            //    if (Layout != null)
            //    {
            //        string rightContentEmptySpace = new string(' ', Layout.ActualContentWidth);
            //        CustomConsole.Write(rightContentEmptySpace);
            //    }

            //    return;
            //}

            //if (Layout == null)
            //{
            //    CustomConsole.Write(text);
            //    return;
            //}

            //if (text.Length <= Layout.ActualContentWidth)
            //{
            //    CustomConsole.Write(text);

            //    if (text.Length < Layout.ActualContentWidth)
            //    {
            //        string rightContentEmptySpace = new string(' ', Layout.ActualContentWidth - text.Length);
            //        CustomConsole.Write(rightContentEmptySpace);
            //    }
            //}
            //else
            //{
            //    CustomConsole.Write(text.Substring(0, Layout.ActualContentWidth));
            //}
        }

        public void Write(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            if (text == null)
                return;

            if (foregroundColor.HasValue)
            {
                if (backgroundColor.HasValue)
                    CustomConsole.Write(foregroundColor.Value, backgroundColor.Value, text);
                else
                    CustomConsole.Write(foregroundColor.Value, text);
            }
            else
            {
                if (backgroundColor.HasValue)
                    CustomConsole.WriteBackgroundColor(backgroundColor.Value, text);
                else
                    CustomConsole.Write(text);
            }
        }

        private void WriteOuterLeftEmptySpace()
        {
            int spaces = Layout.OuterEmptySpaceLeft;
            Console.Write(new string(' ', spaces));
        }

        private void WriteOuterRightEmptySpace()
        {
            int spaces = Layout.OuterEmptySpaceRight;
            Console.Write(new string(' ', spaces));
        }

        private void WriteLeftMargin()
        {
            if (Layout.MarginLeft <= 0)
                return;

            string text = new string(' ', Layout.MarginLeft);
            CustomConsole.Write(text);
        }

        private void WriteRightMargin()
        {
            if (Layout.MarginRight <= 0)
                return;

            string text = new string(' ', Layout.MarginRight);
            CustomConsole.Write(text);
        }

        private void WriteLeftPadding()
        {
            if (Layout.PaddingLeft <= 0)
                return;

            string text = new string(' ', Layout.PaddingLeft);

            if (BackgroundColor.HasValue)
                CustomConsole.WithBackgroundColor(BackgroundColor.Value, () => CustomConsole.Write(text));
            else
                CustomConsole.Write(text);
        }

        private void WriteRightPadding()
        {
            if (Layout.PaddingRight <= 0)
                return;

            string text = new string(' ', Layout.PaddingRight);

            if (BackgroundColor.HasValue)
                CustomConsole.WithBackgroundColor(BackgroundColor.Value, () => CustomConsole.Write(text));
            else
                CustomConsole.Write(text);
        }
    }
}