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
public class GetLines_CharWrap_EmptyLine_Test
{
    [Test]
    public void HavingNoLine_WhenLinesAreGenerated_ThenNoLineIsReturned()
    {
        MultilineText multilineText = new();

        string[] lines = multilineText.GetLines(10).ToArray();

        Assert.That(lines, Is.Empty);
    }

    [Test]
    public void HavingOneEmptyLine_WhenLinesAreGenerated_ThenOneEmptyLineIsReturned()
    {
        MultilineText multilineText = new(string.Empty);

        string[] lines = multilineText.GetLines(10).ToArray();

        Assert.That(lines, Is.EqualTo(new[] { string.Empty }));
    }

    [Test]
    public void HavingTwoEmptyLines_WhenLinesAreGenerated_ThenNoEmptyLinesAreReturned()
    {
        MultilineText multilineText = new(string.Empty, string.Empty);

        string[] lines = multilineText.GetLines(10).ToArray();

        Assert.That(lines, Is.EqualTo(new[] { string.Empty, string.Empty }));
    }
}