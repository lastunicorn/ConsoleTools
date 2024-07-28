// ConsoleTools
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
using DustInTheWind.ConsoleTools.Controls.Spinners;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Spinners.ProgressBarTests;

[TestFixture]
public class MinValueTests
{
    [Test]
    public void MaxValue_is_10_set_MinValue_to_8_succeeds()
    {
        ProgressBar progressBar = new() { MaxValue = 10 };

        progressBar.MinValue = 8;

        Assert.That(progressBar.MinValue, Is.EqualTo(8));
    }

    [Test]
    public void MaxValue_is_10_set_MinValue_to_10_succeeds()
    {
        ProgressBar progressBar = new() { MaxValue = 10 };

        progressBar.MinValue = 10;

        Assert.That(progressBar.MinValue, Is.EqualTo(10));
    }

    [Test]
    public void MaxValue_is_10_set_MinValue_to_12_throws()
    {
        ProgressBar progressBar = new() { MaxValue = 10 };

        Assert.Throws<ArgumentOutOfRangeException>(() => { progressBar.MinValue = 12; });
    }
}