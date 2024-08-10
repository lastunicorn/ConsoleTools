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

using DustInTheWind.ConsoleTools.Controls;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests;

[TestFixture]
public class CalculateSize_WordWrap_OneLineWithNoSpaces_Test
{
    [Test]
    public void HavingWidthGreaterThanLine_WhenLinesAreMeasured_ThenSizeHasWidthEqualToLine()
    {
        MultilineText multilineText = new("1234567890");

        Size size = multilineText.CalculateSize(15, OverflowBehavior.WrapWord);

        Assert.That(size, Is.EqualTo(new Size(10, 1)));
    }

    [Test]
    public void HavingWidthEqualToLine_WhenLinesAreMeasured_ThenSizeHasWidthEqualToLine()
    {
        MultilineText multilineText = new("1234567890");

        Size size = multilineText.CalculateSize(10, OverflowBehavior.WrapWord);

        Assert.That(size, Is.EqualTo(new Size(10, 1)));
    }

    [Test]
    public void HavingWidthSmallerThanLine_WhenLinesAreMeasured_ThenSizeHasWidthEqualToImposedWidthAndHeight2()
    {
        MultilineText multilineText = new("1234567890");

        Size size = multilineText.CalculateSize(7, OverflowBehavior.WrapWord);

        Assert.That(size, Is.EqualTo(new Size(7, 2)));
    }

    [Test]
    public void HavingWidthZero_WhenLinesAreMeasured_ThenSizeIsEmpty()
    {
        MultilineText multilineText = new("1234567890");

        Size size = multilineText.CalculateSize(0, OverflowBehavior.WrapWord);

        Assert.That(size, Is.EqualTo(Size.Empty));
    }
}