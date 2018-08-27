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
using System.Timers;
using DustInTheWind.ConsoleTools.Spinners.Templates;

namespace DustInTheWind.ConsoleTools.Spinners
{
    /// <summary>
    /// Displays a progress-like visual bar that moves continuously.
    /// It can be used for background jobs for which the remaining work cannot be predicted.
    /// It supports templates that control the aspect of the spinner (the displayed characters for each frame).
    /// </summary>
    /// <remarks>
    /// It does not support changing colors while spinning.
    /// </remarks>
    public class Spinner : LongRunningControl, IDisposable
    {
        private readonly ISpinnerTemplate template;
        private bool isDisposed;
        private readonly Timer timer;

        /// <summary>
        /// Gets or sets the label displayed in front of the spinner.
        /// Default value: "Please wait"
        /// </summary>
        public InlineTextBlock Label { get; set; } = new InlineTextBlock(SpinnerResources.DefaultLabelText)
        {
            MarginRight = 1
        };

        /// <summary>
        /// Gets or sets a velue that specifies if the text label should be displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool ShowLabel { get; set; } = true;

        /// <summary>
        /// Gets or sets a text to be displayed instead of the spinner after the control is closed.
        /// </summary>
        public InlineTextBlock DoneText { get; set; }

        /// <summary>
        /// Gets or sets the time interval of the frames.
        /// It can speed up or slow down the animation.
        /// </summary>
        public double StepMiliseconds
        {
            get => timer.Interval;
            set => timer.Interval = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spinner"/> class.
        /// </summary>
        public Spinner()
        {
            template = new StickSpinnerTemplate();

            ShowCursor = false;

            timer = new Timer(400);
            timer.Elapsed += HandleTimerElapsed;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spinner"/> class with
        /// the template that controls the visual representation.
        /// </summary>
        /// <param name="template">The <see cref="ISpinnerTemplate"/> instance that controls the visual representation of the spinner.</param>
        public Spinner(ISpinnerTemplate template)
        {
            this.template = template ?? throw new ArgumentNullException(nameof(template));

            ShowCursor = false;

            timer = new Timer(400);
            timer.Elapsed += HandleTimerElapsed;
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Turn();
        }

        protected override void OnDisplaying()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            base.OnDisplaying();
        }

        /// <summary>
        /// Displays the spinner and runs it until the <see cref="LongRunningControl.Close"/> method is called.
        /// </summary>
        protected override void DoDisplayContent()
        {
            template.Reset();

            if (ShowLabel)
                Label?.Display();

            Turn();
            timer.Start();
        }

        protected override void OnClosing()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            base.OnClosing();
        }

        /// <summary>
        /// Stops the animation of the spinner and erases it from the screen by writting spaces over it.
        /// </summary>
        protected override void DoClose()
        {
            timer.Stop();
            EraseAll();

            DoneText?.Display();
            Console.WriteLine();
        }

        private void EraseAll()
        {
            int length = template.GetCurrent().Length;
            string text = new string(' ', length);
            WriteAndGoBack(text);
        }

        private void Turn()
        {
            string text = template.GetNext();
            WriteAndGoBack(text);
        }

        private static void WriteAndGoBack(string text)
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            Console.Write(text);
            Console.SetCursorPosition(left, top);
        }

        /// <summary>
        /// Releases all resources used by the current instance.
        /// (the internal timer used to control the animation.)
        /// </summary>
        public void Dispose()
        {
            if (isDisposed)
                return;

            Close();
            timer.Dispose();

            isDisposed = true;
        }

        /// <summary>
        /// Executes the specified action while displaying the default spinner.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        public static void Run(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RunInternal(new StickSpinnerTemplate(), action);
        }

        /// <summary>
        /// Executes the specified action while displaying a spinner with the specified template.
        /// </summary>
        /// <param name="template">The spinner template to be used.</param>
        /// <param name="action">The action to be executed.</param>
        public static void Run(ISpinnerTemplate template, Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            RunInternal(template, action);
        }

        private static void RunInternal(ISpinnerTemplate template, Action action)
        {
            using (Spinner spinner = new Spinner(template))
            {
                spinner.Display();

                try
                {
                    action();

                    spinner.DoneText = new InlineTextBlock(SpinnerResources.DoneText, CustomConsole.SuccessColor);
                    spinner.Close();
                }
                catch
                {
                    spinner.DoneText = new InlineTextBlock(SpinnerResources.ErrorText, CustomConsole.ErrorColor);
                    spinner.Close();
                    throw;
                }
            }
        }

        /// <summary>
        /// Executes the specified function while displaying the default spinner.
        /// </summary>
        /// <param name="action">The function to be executed.</param>
        public static T Run<T>(Func<T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            return RunInternal(new StickSpinnerTemplate(), action);
        }

        /// <summary>
        /// Executes the specified function while displaying a spinner with the specified template.
        /// </summary>
        /// <param name="template">The spinner template to be used.</param>
        /// <param name="action">The function to be executed.</param>
        public static T Run<T>(ISpinnerTemplate template, Func<T> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            return RunInternal(template, action);
        }

        private static T RunInternal<T>(ISpinnerTemplate template, Func<T> action)
        {
            using (Spinner spinner = new Spinner(template))
            {
                spinner.Display();

                try
                {
                    T result = action();

                    spinner.DoneText = new InlineTextBlock(SpinnerResources.DoneText, CustomConsole.SuccessColor);
                    spinner.Close();

                    return result;
                }
                catch
                {
                    spinner.DoneText = new InlineTextBlock(SpinnerResources.ErrorText, CustomConsole.ErrorColor);
                    spinner.Close();
                    throw;
                }
            }
        }
    }
}