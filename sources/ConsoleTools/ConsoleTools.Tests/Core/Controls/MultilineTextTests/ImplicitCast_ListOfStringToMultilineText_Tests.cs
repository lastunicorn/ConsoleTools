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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls;
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests;

[TestFixture]
public class ImplicitCast_ListOfStringToMultilineText_Tests
{
    [Test]
    public void HavingNullListOfStrings_WhenCastToMultilineText_ThenItHasNoLine()
    {
        List<string> text = null;
        MultilineText multilineText = text;

        multilineText.Should().BeEmpty();
    }

    [Test]
    public void HavingEmptyListOfStrings_WhenCastToMultilineText_ThenItHasNoLine()
    {
        List<string> text = new();
        MultilineText multilineText = text;

        multilineText.Should().BeEmpty();
    }

    [Test]
    public void HavingListOfStringsContainingOneString_WhenCastToMultilineText_ThenItHasOneLine()
    {
        List<string> text = new() { "this is a text" };
        MultilineText multilineText = text;

        string[] expected = { "this is a text" };
        multilineText.Should().ContainInOrder(expected);
    }

    [Test]
    public void HavingListOfStringsContainingTwoStrings_WhenCastToMultilineText_ThenItHasTwoLines()
    {
        List<string> text = new() { "first line", "second line" };
        MultilineText multilineText = text;

        string[] expected = { "first line", "second line" };
        multilineText.Should().ContainInOrder(expected);
    }
}