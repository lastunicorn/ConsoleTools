// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Spinners;
using DustInTheWind.ConsoleTools.Demo.ProgressBarkDemo.BusinessLayer;

namespace DustInTheWind.ConsoleTools.Demo.ProgressBarkDemo.PresentationLayer
{
    /// <summary>
    /// This view uses a <see cref="DataProcessingJob"/> to simulate some business process and a <see cref="ProgressBar"/> control
    /// to display its percentage progress.
    /// </summary>
    internal class DataProcessingView
    {
        private readonly ProgressBar progressBar;
        private DataProcessingJob job;

        public DataProcessingView()
        {
            progressBar = new ProgressBar
            {
                BarFillChar = '═',
                BarFillForegroundColor = ConsoleColor.Green,
                BarEmptyChar = '-',
                BarEmptyForegroundColor = ConsoleColor.DarkGray,
                ShowCursor = false,
                ShowValue = false
            };
        }

        public void Show()
        {
            TextBlock textBlock = new TextBlock()
            {
                Margin = "0 0 0 1",
                Text = "We create a DataProcessingJob object that will simulate some data processing."
            };
            textBlock.Display();

            job = new DataProcessingJob();
            job.StateChanged += HandleJobStateChanged;
            job.ProgressChanged += HandleJobProgressChanged;

            job.Run();
        }

        private void HandleJobStateChanged(object sender, EventArgs e)
        {
            if (sender is DataProcessingJob job)
            {
                switch (job.State)
                {
                    case JobState.Running:
                        TextBlock textBlockStart = new TextBlock()
                        {
                            Margin = "0 0 0 1",
                            Text = "The job was started."
                        };
                        textBlockStart.Display();
                        progressBar.Display();
                        break;

                    case JobState.Stopped:
                        progressBar.Close();
                        TextBlock textBlockFinish = new TextBlock()
                        {
                            Margin = "0 1 0 0",
                            Text = "The job has finished."
                        };
                        textBlockFinish.Display();
                        break;
                }
            }
        }

        private void HandleJobProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
    }
}