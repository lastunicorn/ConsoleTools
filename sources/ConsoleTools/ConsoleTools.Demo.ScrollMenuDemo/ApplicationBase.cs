﻿// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Demo.ScrollMenuDemo
{
    internal class ApplicationBase
    {
        public bool IsExitRequested { get; private set; }

        public event EventHandler<CancelEventArgs> Exiting;

        public event EventHandler ExitCanceled;

        public event EventHandler Exited;

        public void RequestExit()
        {
            CancelEventArgs e = new CancelEventArgs();
            OnExiting(e);

            if (e.Cancel)
            {
                OnExitCanceled();
                return;
            }

            IsExitRequested = true;

            OnExited();
        }

        protected virtual void OnExiting(CancelEventArgs e)
        {
            Exiting?.Invoke(this, e);
        }

        protected virtual void OnExitCanceled()
        {
            ExitCanceled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnExited()
        {
            Exited?.Invoke(this, EventArgs.Empty);
        }
    }
}