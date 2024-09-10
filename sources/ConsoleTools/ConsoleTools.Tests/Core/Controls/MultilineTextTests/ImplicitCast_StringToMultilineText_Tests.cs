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
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests;

[TestFixture]
public class ImplicitCast_StringToMultilineText_Tests
{
    [Test]
    public void HavingNullString_WhenCastToMultilineText_ThenItHasNoLine()
    {
        string text = null;
        MultilineText multilineText = text;

        multilineText.Should().BeEmpty();
    }

    [Test]
    public void HavingEmptyString_WhenCastToMultilineText_ThenItHasOneEmptyLine()
    {
        string text = "";
        MultilineText multilineText = text;

        string[] expected = { string.Empty };
        multilineText.Should().ContainInOrder(expected);
    }

    [Test]
    public void HavingSingleLineString_WhenCastToMultilineText_ThenItHasOneLine()
    {
        string text = "this is a text";
        MultilineText multilineText = text;

        string[] expected = { "this is a text" };
        multilineText.Should().ContainInOrder(expected);
    }

    [Test]
    public void HavingStringWithTwoLinesSeparatedByLF_WhenCastToMultilineText_ThenItHasTwoLines()
    {
        string text = "first line\nsecond line";
        MultilineText multilineText = text;

        string[] expected =
        {
            "first line",
            "second line"
        };
        multilineText.Should().ContainInOrder(expected);
    }
}