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
public class TextBlock_TextHorizontalAlignment_Tests : TestsBase
{
    [Test]
    public void HavingShortWidthAndHorizontalAlignmentNotSpecified()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 20
        };

        string expected = GetResourceFileContent("01-halign-unpecifies.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingShortWidthAndHorizontalAlignmentLeft()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 20,
            TextHorizontalAlignment = HorizontalAlignment.Left
        };

        string expected = GetResourceFileContent("02-halign-left.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingShortWidthAndHorizontalAlignmentCenter()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 20,
            TextHorizontalAlignment = HorizontalAlignment.Center
        };

        string expected = GetResourceFileContent("03-halign-center.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingShortWidthAndHorizontalAlignmentRight()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 20,
            TextHorizontalAlignment = HorizontalAlignment.Right
        };

        string expected = GetResourceFileContent("04-halign-right.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingShortWidthAndHorizontalAlignmentDefault()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 20,
            TextHorizontalAlignment = HorizontalAlignment.Default
        };

        string expected = GetResourceFileContent("05-halign-default.txt");
        textBlock.IsEqualTo(expected);
    }

    [Test]
    public void HavingShortWidthAndHorizontalAlignmentInvalidValue()
    {
        TextBlock textBlock = new("The quick brown fox jumps over the lazy dog.")
        {
            Width = 20,
            TextHorizontalAlignment = (HorizontalAlignment)2500
        };

        string expected = GetResourceFileContent("06-halign-invalid.txt");
        textBlock.IsEqualTo(expected);
    }
}