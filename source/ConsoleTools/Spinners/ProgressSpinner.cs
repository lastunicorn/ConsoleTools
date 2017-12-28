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

namespace DustInTheWind.ConsoleTools.Spinners
{
    public class ProgressSpinner : IDisposable
    {
        private readonly ITemplate template;
        private bool isDisposed;
        private readonly Timer timer;

        public double StepMiliseconds
        {
            get { return timer.Interval; }
            set { timer.Interval = value; }
        }

        public ProgressSpinner(ITemplate template)
        {
            if (template == null) throw new ArgumentNullException(nameof(template));
            this.template = template;

            timer = new Timer(400);
            timer.Elapsed += HandleTimerElapsed;
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Turn();
        }

        public void Start()
        {
            if (isDisposed)
                throw new ObjectDisposedException(GetType().FullName);

            template.Reset();
            Console.CursorVisible = false;

            Turn();
            timer.Start();
        }

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