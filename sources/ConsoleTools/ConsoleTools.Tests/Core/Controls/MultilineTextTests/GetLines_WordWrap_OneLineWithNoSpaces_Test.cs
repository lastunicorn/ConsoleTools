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

using System.Linq;
using DustInTheWind.ConsoleTools.Controls;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests;

[TestFixture]
public class GetLines_WordWrap_OneLineWithNoSpaces_Test
{
    [Test]
    public void HavingWidthEqualToLine_WhenLinesAreGenerated_ThenThatLineIsReturned()
    {
        MultilineText multilineText = new("1234567890");

        string[] lines = multilineText.GetLines(10, OverflowBehavior.WrapWord).ToArray();

        Assert.That(lines, Is.EqualTo(new[] { "1234567890" }));
    }

    [Test]
    public void HavingWidthSmallerThanLine_WhenLinesAreGenerated_ThenTwoLinesAreReturned()
    {
        MultilineText multilineText = new("1234567890");

        string[] lines = multilineText.GetLines(7, OverflowBehavior.WrapWord).ToArray();

        Assert.That(lines, Is.EqualTo(new[] { "1234567", "890" }));
    }

    [Test]
    public void HavingWidthZero_WhenLinesAreGenerated_ThenNoLinesAreReturned()
    {
        MultilineText multilineText = new("1234567890");

        string[] lines = multilineText.GetLines(0, OverflowBehavior.WrapWord).ToArray();

        Assert.That(lines, Is.Empty);
    }
}