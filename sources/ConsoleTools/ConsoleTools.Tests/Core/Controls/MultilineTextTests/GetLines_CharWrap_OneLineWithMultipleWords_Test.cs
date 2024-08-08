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
public class GetLines_CharWrap_OneLineWithMultipleWords_Test
{
    [Test]
    public void line_ends_at_the_end_of_a_word()
    {
        MultilineText multilineText = new("The quick brown fox jumps over the lazy dog.");

        string[] lines = multilineText.GetLines(25).ToArray();

        Assert.That(lines, Is.EqualTo(new[] { "The quick brown fox jumps", " over the lazy dog." }));
    }

    [Test]
    public void line_ends_at_the_beginning_of_a_word()
    {
        MultilineText multilineText = new("The quick brown fox jumps over the lazy dog.");

        string[] lines = multilineText.GetLines(26).ToArray();

        Assert.That(lines, Is.EqualTo(new[] { "The quick brown fox jumps ", "over the lazy dog." }));
    }

    [Test]
    public void line_ends_in_the_middle_of_a_word()
    {
        MultilineText multilineText = new("The quick brown fox jumps over the lazy dog.");

        string[] lines = multilineText.GetLines(28).ToArray();

        Assert.That(lines, Is.EqualTo(new[] { "The quick brown fox jumps ov", "er the lazy dog." }));
    }

    [Test]
    public void if_line_width_is_0_no_lines_are_returned()
    {
        MultilineText multilineText = new("The quick brown fox jumps over the lazy dog.");

        string[] lines = multilineText.GetLines(0).ToArray();

        Assert.That(lines, Is.Empty);
    }
}