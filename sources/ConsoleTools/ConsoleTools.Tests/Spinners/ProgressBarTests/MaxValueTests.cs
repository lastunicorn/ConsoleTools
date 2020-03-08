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
using DustInTheWind.ConsoleTools.Controls.Spinners;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Spinners.ProgressBarTests
{
    [TestFixture]
    public class MaxValueTests
    {
        [Test]
        public void MinValue_is_10_set_MaxValue_to_12_succeeds()
        {
            ProgressBar progressBar = new ProgressBar { MinValue = 10 };

            progressBar.MaxValue = 12;

            Assert.That(progressBar.MaxValue, Is.EqualTo(12));
        }

        [Test]
        public void MinValue_is_10_set_MaxValue_to_10_succeeds()
        {
            ProgressBar progressBar = new ProgressBar { MinValue = 10 };

            progressBar.MaxValue = 10;

            Assert.That(progressBar.MaxValue, Is.EqualTo(10));
        }

        [Test]
        public void MinValue_is_10_set_MaxValue_to_8_throws()
        {
            ProgressBar progressBar = new ProgressBar { MinValue = 10 };

            Assert.Throws<ArgumentOutOfRangeException>(() => { progressBar.MaxValue = 8; });
        }
    }
}