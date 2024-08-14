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
using DustInTheWind.ConsoleTools.Controls.Tables;
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.ContentCellTests;

[TestFixture]
public class PaddingBottomTests
{
    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(10)]
    public void HavingContentCellInstance_WhenSetPaddingBottomToPositiveValues_ThenPaddingBottomHasThatValue(int value)
    {
        ContentCell contentCell = new();

        contentCell.PaddingBottom = value;

        contentCell.PaddingBottom.Should().Be(value);
    }
    
    [Test]
    public void HavingContentCellInstance_WhenSetPaddingBottomToZero_ThenPaddingBottomIsZero()
    {
        ContentCell contentCell = new();

        contentCell.PaddingBottom = 0;

        contentCell.PaddingBottom.Should().Be(0);
    }

    [Test]
    public void HavingContentCellInstance_WhenSetPaddingBottomToNegativeValue_ThenThrows()
    {
        ContentCell contentCell = new();

        Action action = () => contentCell.PaddingBottom = -1;

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void HavingContentCellInstance_WhenSetPaddingBottomToNull_ThenPaddingBottomIsNull()
    {
        ContentCell contentCell = new();

        contentCell.PaddingBottom = null;

        contentCell.PaddingBottom.Should().BeNull();
    }
}