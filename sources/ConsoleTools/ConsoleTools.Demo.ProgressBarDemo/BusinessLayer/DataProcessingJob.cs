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

using System.Threading;

namespace DustInTheWind.ConsoleTools.Demo.ProgressBarDemo.BusinessLayer
{
    /// <summary>
    /// This is a business class that is asynchronously processing some data.
    /// Well... we just emulate the data processing, but you get the point.
    /// The class also provides a ProgressChanged event to announce its progress in percentages from 0 to 100
    /// and a State property to announce when the process starts and finishes.
    /// </summary>
    internal class DataProcessingJob : JobBase
    {
        protected override void DoRun()
        {
            AnnounceProgress(0);

            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                AnnounceProgress(i);
            }
        }
    }
}