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
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests;

[TestFixture]
public class ImplicitConversionToMultilineTextTests
{
    [Test]
    public void single_line_String_to_MultilineText_conversion()
    {
        string text = "this is a text";
        MultilineText multilineText = text;

        Assert.That(multilineText.RawText, Is.EqualTo("this is a text"));
    }

    [Test]
    public void double_line_String_to_MultilineText_conversion()
    {
        string text = "first line\nsecond line";
        MultilineText multilineText = text;

        Assert.That(multilineText.RawText, Is.EqualTo("first line\nsecond line"));
    }

    [Test]
    public void single_line_list_of_String_to_MultilineText_conversion()
    {
        List<string> text = new() { "this is a text" };
        MultilineText multilineText = text;

        Assert.That(multilineText.RawText, Is.EqualTo("this is a text"));
    }

    [Test]
    public void double_line_list_of_String_to_MultilineText_conversion()
    {
        List<string> text = new() { "first line", "second line" };
        MultilineText multilineText = text;

        string expected = "first line" + Environment.NewLine + "second line";
        Assert.That(multilineText.RawText, Is.EqualTo(expected));
    }
}