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
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.BorderTests.RenderingTests;

[TestFixture]
public class Border_Tests : TestsBase
{
    [Test]
    public void HavingContentOneLine()
    {
        Border border = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog."),
            Template = BorderTemplate.SingleLineBorderTemplate
        };

        string expected = GetResourceFileContent("01-content-oneline.txt");
        border.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentTwoLines()
    {
        Border border = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog.", "I am not young enough to know everything."),
            Template = BorderTemplate.SingleLineBorderTemplate
        };

        string expected = GetResourceFileContent("02-content-twolines.txt");
        border.IsEqualTo(expected);
    }

    [Test]
    public void HavingContentOneLineAndMargin()
    {
        Border border = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog."),
            Template = BorderTemplate.SingleLineBorderTemplate,
            Margin = "2 1"
        };

        string expected = GetResourceFileContent("03-content-oneline-margins.txt");
        border.IsEqualTo(expected);
    }
}