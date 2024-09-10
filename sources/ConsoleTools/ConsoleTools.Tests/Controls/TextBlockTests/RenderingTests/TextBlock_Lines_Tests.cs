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
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.TextBlockTests.RenderingTests;

[TestFixture]
public class TextBlock_Lines_Tests : TestsBase
{
    [Test]
    public void HavingTextNotSet()
    {
        TextBlock textBlock = new();

        textBlock.IsEqualTo(string.Empty);
    }

    [Test]
    public void HavingNoLine()
    {
        TextBlock textBlock = new(Array.Empty<string>());

        textBlock.IsEqualTo(string.Empty);
    }

    [Test]
    public void HavingOneLine()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.");

        string expected = GetResourceFileContent("01-oneline.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingTwoLines()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.", "I am not young enough to know everything.");

        string expected = GetResourceFileContent("02-twolines.txt");
        textBlock.IsEqualTo(expected);
    }
}