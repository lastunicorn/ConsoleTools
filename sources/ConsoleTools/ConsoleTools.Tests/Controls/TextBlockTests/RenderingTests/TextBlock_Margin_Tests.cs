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

namespace DustInTheWind.ConsoleTools.Tests.Controls.TextBlockTests.RenderingTests;

[TestFixture]
public class TextBlock_Margin_Tests : TestsBase
{
    [Test]
    public void HavingMargin2AllAroundAndBiggerMaxWidth()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            MaxWidth = 100,
            Margin = 2
        };

        string expected = GetResourceFileContent("01-margin-2-width-long.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingMargin2AllAroundAndSmallerMaxWidth()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            MaxWidth = 20,
            Margin = 2
        };

        string expected = GetResourceFileContent("02-margin-2-width-short.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingMargin2AllAroundAndExactMaxWidth()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            MaxWidth = 44 + 2 + 2,
            Margin = 2
        };

        string expected = GetResourceFileContent("03-margin-2-width-exact.txt");
        textBlock.IsEqualTo(expected);
    }
}