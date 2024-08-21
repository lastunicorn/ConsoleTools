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

namespace DustInTheWind.ConsoleTools.Tests.Controls.BorderTests.RenderingTests;

[TestFixture]
public class Border_MaxWidth_Tests : TestsBase
{
    [Test]
    public void HavingMaxWidthBiggerThanContent()
    {
        Border border = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog."),
            MaxWidth = 100
        };

        string expected = GetResourceFileContent("01-lines-1-maxwidth-100.txt");
        border.IsEqualTo(expected);
    }

    [Test]
    public void HavingMaxWidthSmallerThanContent()
    {
        Border border = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog."),
            MaxWidth = 30
        };

        string expected = GetResourceFileContent("02-lines-1-maxwidth-30.txt");
        border.IsEqualTo(expected);
    }

    [Test]
    public void HavingMarginsAndMaxWidthSmallerThanContent()
    {
        Border border = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog."),
            MaxWidth = 30,
            Margin = 1
        };

        string expected = GetResourceFileContent("03-lines-1-maxwidth-30-margin-1.txt");
        border.IsEqualTo(expected);
    }

    [Test]
    public void HavingTwoLinesWithMarginsAndMaxWidthSmallerThanContent()
    {
        Border border = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog.", "I am not young enough to know everything."),
            MaxWidth = 30,
            Margin = 1
        };

        string expected = GetResourceFileContent("04-lines-2-maxwidth-30-margin-1.txt");
        border.IsEqualTo(expected);
    }
}