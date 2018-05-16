using System;

namespace DustInTheWind.ConsoleTools.MenuControl
{
    /// <summary>
    /// Contains base functionality for a control that must be displayed multiple times like a menu or a prompter.
    /// </summary>
    public abstract class MultipleDisplayControl
    {
        public bool IsCloseRequested { get; private set; }

        private Location initialLocation;

        /// <summary>
        /// Gets or sets the number of empty lines displayed before the pause text.
        /// Default value: 0
        /// </summary>
        public int MarginTop { get; set; }

        /// <summary>
        /// Gets or sets the number of empty lines displayed after the pause text, after the pause was ended.
        /// Default value: 0
        /// </summary>
        public int MarginBottom { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the cursor is visible while the control is displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool ShowCursor { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that specifies if the control should always be displayed at the beginning of the line.
        /// If this value is <c>true</c> and the cursor is not at the beginning of the line, a new line is written before displaying the control.
        /// </summary>
        public bool EnsureBeginOfLine { get; set; }

        /// <summary>
        /// Gets the size of the control after it was displayed.
        /// </summary>
        protected Size Size { get; private set; }

        /// <summary>
        /// Gets or sets a value that specifies if the control is erased from the Console
        /// after it was displayed.
        /// </summary>
        public bool EraseOnClose { get; set; }

        /// <summary>
        /// Displays the control in the console until the <see cref="RequestClose"/> method is called.
        /// </summary>
        public void Display()
        {
            do
            {
                DisplayOnce();
            }
            while (!IsCloseRequested);
        }

        /// <summary>
        /// Displays the control in the console once.
        /// </summary>
        public void DisplayOnce()
        {
            IsCloseRequested = false;

            OnBeforeDisplay();

            if (ShowCursor)
                DisplayInternal();
            else
                CustomConsole.RunWithoutCursor(DisplayInternal);

            if (EraseOnClose && Size.Height > 0)
                EraseControl();

            OnAfterDisplay();
        }

        private void DisplayInternal()
        {
            MoveToNextLineIfNecessary();

            OnBeforeTopMargin();
            WriteTopMargin();

            DoDisplayContent();

            WriteBottomMargin();
            OnAfterBottomMargin();
        }

        /// <summary>
        /// Method called at the begining of each display, before doing anything else.
        /// </summary>
        protected virtual void OnBeforeDisplay()
        {
        }

        /// <summary>
        /// Method called at the very end, after all the control was displayed.
        /// </summary>
        protected virtual void OnAfterDisplay()
        {
        }

        private void EraseControl()
        {
            string emptyLine = new string(' ', Console.BufferWidth);

            Console.SetCursorPosition(initialLocation.Left, initialLocation.Top);

            for (int i = 0; i < Size.Height; i++)
                Console.Write(emptyLine);

            Console.SetCursorPosition(initialLocation.Left, initialLocation.Top);
        }

        private void MoveToNextLineIfNecessary()
        {
            if (Console.CursorLeft != 0 && (EnsureBeginOfLine || MarginTop > 0))
                Console.WriteLine();
        }

        /// <summary>
        /// Method called immediately before writting the top margin.
        /// </summary>
        protected virtual void OnBeforeTopMargin()
        {
            Size = CalculateControlSize();

            EnsureVerticalSpace();

            initialLocation = new Location(Console.CursorLeft, Console.CursorTop);
        }

        private void EnsureVerticalSpace()
        {
            int initialLeft = Console.CursorLeft;

            int height = Math.Min(Console.BufferHeight - 1, Size.Height);

            for (int i = 0; i < height; i++)
                Console.WriteLine();

            Console.SetCursorPosition(initialLeft, Console.CursorTop - height);
        }

        /// <summary>
        /// Before displaying the control, this method is called in order to calculate the size of the control.
        /// The inheritors must returns e valid size if they need to have a preallocated vertical space in the console,
        /// before starting to display the content.
        /// If this method returns <see cref="Size.Empty"/>, no vertical space is preallocated.
        /// </summary>
        protected abstract Size CalculateControlSize();

        /// <summary>
        /// Method called immediately after writting the bottom margin.
        /// </summary>
        protected virtual void OnAfterBottomMargin()
        {
        }

        private void WriteTopMargin()
        {
            for (int i = 0; i < MarginTop; i++)
                Console.WriteLine();
        }

        private void WriteBottomMargin()
        {
            for (int i = 0; i < MarginBottom; i++)
                Console.WriteLine();
        }

        /// <summary>
        /// When implemented by an inheritor it displays the content of the control to the console.
        /// </summary>
        protected abstract void DoDisplayContent();

        public void RequestClose()
        {
            IsCloseRequested = true;

            OnCloseRequested();
        }

        protected virtual void OnCloseRequested()
        {
        }
    }
}