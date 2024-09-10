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
using DustInTheWind.ConsoleTools.Controls;
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests;

[TestFixture]
public class ImplicitCast_ArrayOfStringToMultilineText_Tests
{
    [Test]
    public void HavingNullArrayOfStrings_WhenCastToMultilineText_ThenItHasNoLine()
    {
        string[] text = null;
        MultilineText multilineText = text;

        multilineText.Should().BeEmpty();
    }

    [Test]
    public void HavingEmptyArrayOfStrings_WhenCastToMultilineText_ThenItHasNoLine()
    {
        string[] text = Array.Empty<string>();
        MultilineText multilineText = text;

        multilineText.Should().BeEmpty();
    }

    [Test]
    public void HavingArrayOfStringsContainingOneString_WhenCastToMultilineText_ThenItHasOneLine()
    {
        string[] text = { "this is a text" };
        MultilineText multilineText = text;

        string[] expected = { "this is a text" };
        multilineText.Should().ContainInOrder(expected);
    }

    [Test]
    public void HavingArrayOfStringsContainingTwoStrings_WhenCastToMultilineText_ThenItHasTwoLines()
    {
        string[] text = { "first line", "second line" };
        MultilineText multilineText = text;

        string[] expected = { "first line", "second line" };
        multilineText.Should().ContainInOrder(expected);
    }
}