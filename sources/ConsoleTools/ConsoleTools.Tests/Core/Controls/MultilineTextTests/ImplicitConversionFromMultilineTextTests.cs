// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.MultilineTextTests
{
    [TestFixture]
    public class ImplicitConversionFromMultilineTextTests
    {
        [Test]
        public void single_line_MultilineText_to_String_conversion()
        {
            MultilineText multilineText = new MultilineText("this is a text");
            string text = multilineText;

            Assert.That(text, Is.EqualTo("this is a text"));
        }

        [Test]
        public void double_line_MultilineText_to_String_conversion()
        {
            MultilineText multilineText = new MultilineText(new[] { "first line", "second line" });
            string text = multilineText;

            string expected = "first line" + Environment.NewLine + "second line";
            Assert.That(text, Is.EqualTo(expected));
        }

        [Test]
        public void single_line_MultilineText_to_list_of_String_conversion()
        {
            MultilineText multilineText = new MultilineText("this is a text");
            List<string> text = multilineText;

            Assert.That(text, Is.EqualTo(new List<string> { "this is a text" }));
        }

        [Test]
        public void double_line_MultilineText_to_list_of_String_conversion()
        {
            MultilineText multilineText = new MultilineText(new[] { "first line", "second line" });
            List<string> text = multilineText;

            Assert.That(text, Is.EqualTo(new List<string> { "first line", "second line" }));
        }
    }
}