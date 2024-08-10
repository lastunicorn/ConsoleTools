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
public class CalculateSize_CharCut_EmptyLine_Test
{
    [Test]
    public void HavingNoLine_WhenLinesAreMeasured_ThenSizeIsZero()
    {
        MultilineText multilineText = new();

        Size size = multilineText.CalculateSize(10, OverflowBehavior.CutChar);

        Assert.That(size, Is.EqualTo(Size.Empty));
    }

    [Test]
    public void HavingEmptyText_WhenLinesAreMeasured_ThenSizeHasHeight1()
    {
        MultilineText multilineText = new(string.Empty);

        Size size = multilineText.CalculateSize(10, OverflowBehavior.CutChar);

        Assert.That(size, Is.EqualTo(new Size(0, 1)));
    }

    [Test]
    public void HavingTwoEmptyLines_WhenLinesAreMeasured_ThenSizeHasHeight2()
    {
        MultilineText multilineText = new("", "");

        Size size = multilineText.CalculateSize(10, OverflowBehavior.CutChar);

        Assert.That(size, Is.EqualTo(new Size(0, 2)));
    }
}