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
using System.Timers;
using DustInTheWind.ConsoleTools.InputControls;

namespace DustInTheWind.ConsoleTools.Spinners
{
    /// <summary>
    /// Displays a progress-like visual bar that moves continuously.
    /// It can be used for background jobs for which the remaining work cannot be predicted.
    /// It supports templates that control the aspect of the spinner (the displayed text for each frame).
    /// </summary>
    /// <remarks>
    /// It does not support changing colors while spinning.
    /// </remarks>
    public class ProgressSpinner : IDisposable
    {
        private readonly ISpinnerTemplate template;
        private bool isDisposed;
        private readonly Timer timer;
        private readonly Label label = new Label();

        public string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public string TextSeparator
        {
            get { return label.Separator; }
            set { label.Separator = value; }
        }

        /// <summary>
        /// Gets or sets the time interval of the frames.
        /// It can speed up or slow down the animation.
        /// </summary>
        public double StepMiliseconds
        {
            get { return timer.Interval; }
            set { timer.Interval = value; }
        }

        public ProgressSpinner(ISpinnerTemplate template)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));
            this.template = template;

            label.Text = "Please wait";

            timer = new Timer(400);
            timer.Elapsed += HandleTimerElapsed;
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Turn();
        }

        /// <summary>
        /// Displays the spinner and runs it until the <see cref="Stop"/> method is called.
        /// </summary>
        public void Start()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            template.Reset();
            Console.CursorVisible = false;

            label.Display();

            Turn();
            timer.Start();
        }

        /// <summary>
        /// Stops the animation of the spinner and erases it from the screen by writting sapces over it.
        /// </summary>
        public void Stop()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            timer.Stop();
            EraseAll();

            Console.CursorVisible = true;
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

        public void Dispose()
        {
            if (isDisposed)
                return;

            timer.Dispose();

            isDisposed = true;
        }
    }
}