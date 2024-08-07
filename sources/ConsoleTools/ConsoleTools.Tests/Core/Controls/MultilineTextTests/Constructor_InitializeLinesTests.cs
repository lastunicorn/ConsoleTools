﻿// ConsoleTools
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

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests;

[TestFixture]
public class Constructor_InitializeLinesTests
{
    [Test]
    public void one_line_text_is_not_splited()
    {
        MultilineText multilineText = new("Some text");
        Assert.That(multilineText.Lines, Is.EqualTo(new[] { "Some text" }));
    }

    [Test]
    public void LF_is_recognized_as_new_line()
    {
        MultilineText multilineText = new("Some text\n123\nabcd\nSome other text");
        Assert.That(multilineText.Lines, Is.EqualTo(new[] { "Some text", "123", "abcd", "Some other text" }));
    }

    [Test]
    public void CR_is_recognized_as_new_line()
    {
        MultilineText multilineText = new("Some text\r123\rabcd\rSome other text");
        Assert.That(multilineText.Lines, Is.EqualTo(new[] { "Some text", "123", "abcd", "Some other text" }));
    }

    [Test]
    public void CRLF_is_recognized_as_new_line()
    {
        MultilineText multilineText = new("Some text\r\n123\r\nabcd\r\nSome other text");
        Assert.That(multilineText.Lines, Is.EqualTo(new[] { "Some text", "123", "abcd", "Some other text" }));
    }

    [Test]
    public void combination_of_CR_LF_and_CRLF_are_accepted()
    {
        MultilineText multilineText = new("Some text\r123\nabcd\r\nSome other text");
        Assert.That(multilineText.Lines, Is.EqualTo(new[] { "Some text", "123", "abcd", "Some other text" }));
    }

    [Test]
    public void RawText_is_same_as_the_original_text()
    {
        const string text = "Some text\r123\nabcd\r\nSome other text";
        MultilineText multilineText = new(text);

        Assert.That(multilineText.RawText, Is.EqualTo(text));
    }
}