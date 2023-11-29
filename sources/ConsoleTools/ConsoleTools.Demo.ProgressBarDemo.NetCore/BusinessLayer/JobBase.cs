// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTools.Demo.ProgressBarDemo.NetCore.BusinessLayer
{
    /// <summary>
    /// Base class that asynchrounously runs some code and provides support to announce its progress.
    /// </summary>
    internal abstract class JobBase
    {
        private JobState state = JobState.New;
        private readonly ManualResetEventSlim finishEvent;

        protected JobBase()
        {
            finishEvent = new ManualResetEventSlim(false);
        }

        /// <summary>
        /// Gets a galue that specifies if the job is running.
        /// </summary>
        public JobState State
        {
            get => state;
            private set
            {
                state = value;
                OnStateChanged();
            }
        }

        /// <summary>
        /// Event raised when the state of the jog is changed.
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// Event raised when the progress of the job changed.
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        /// <summary>
        /// Runs the <see cref="DoRun"/> method asynchronously.
        /// The <see cref="Start"/> method immediatelly return and the job remains to be run on a background thread.
        /// </summary>
        public void Start()
        {
            finishEvent.Reset();
            State = JobState.Running;

            Task.Run(() =>
            {
                try
                {
                    DoRun();
                }
                finally
                {
                    finishEvent.Set();
                    State = JobState.Stopped;
                }
            });
        }

        /// <summary>
        /// Runs the <see cref="DoRun"/> method synchronously.
        /// The <see cref="Run"/> methods is blocked until the job is finished.
        /// </summary>
        public void Run()
        {
            State = JobState.Running;

            try
            {
                DoRun();
            }
            finally
            {
                State = JobState.Stopped;
            }
        }

        protected abstract void DoRun();

        protected void AnnounceProgress(int progressPercentage)
        {
            ProgressChangedEventArgs eventArgs = new ProgressChangedEventArgs(progressPercentage, null);
            OnProgressChanged(eventArgs);
        }

        public void WaitToFinish()
        {
            finishEvent.Wait();
        }

        protected virtual void OnStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }
    }
}