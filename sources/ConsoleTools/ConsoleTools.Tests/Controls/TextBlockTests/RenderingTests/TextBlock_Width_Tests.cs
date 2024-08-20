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
public class TextBlock_Width_Tests : TestsBase
{
    [Test]
    public void HavingLongerWidth()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 100
        };

        string expected = GetResourceFileContent("01-width-long.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingShorterWidth()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 20
        };

        string expected = GetResourceFileContent("02-width-short.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingExactWidth()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 44
        };

        string expected = GetResourceFileContent("03-width-exact.txt");
        textBlock.IsEqualTo(expected);
    }
}