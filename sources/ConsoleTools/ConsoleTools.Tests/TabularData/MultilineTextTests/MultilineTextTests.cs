// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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

using DustInTheWind.ConsoleTools.TabularData;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.TabularData.MultilineTextTests
{
    [TestFixture]
    public class MultilineTextTests
    {
        [Test]
        public void TestOneLineSize()
        {
            MultilineText multilineText = new MultilineText("Some text");
            Assert.That(multilineText.Size, Is.EqualTo(new Size(9, 1)));
        }

        [Test]
        public void LF_is_recognized_as_new_line()
        {
            MultilineText multilineText = new MultilineText("Some text\n123\nabcd\nSome other text");
            Assert.That(multilineText.Size, Is.EqualTo(new Size(15, 4)));
        }

        [Test]
        public void CR_is_recognized_as_new_line()
        {
            MultilineText multilineText = new MultilineText("Some text\r123\rabcd\rSome other text");
            Assert.That(multilineText.Size, Is.EqualTo(new Size(15, 4)));
        }

        [Test]
        public void CRLF_is_recognized_as_new_line()
        {
            MultilineText multilineText = new MultilineText("Some text\r\n123\r\nabcd\r\nSome other text");
            Assert.That(multilineText.Size, Is.EqualTo(new Size(15, 4)));
        }

        [Test]
        public void combination_of_CR_LF_and_CRLF_are_accepted()
        {
            MultilineText multilineText = new MultilineText("Some text\r123\nabcd\r\nSome other text");
            Assert.That(multilineText.Size, Is.EqualTo(new Size(15, 4)));
        }

        [Test]
        public void RawText_is_same_as_the_original_text()
        {
            const string text = "Some text\r123\nabcd\r\nSome other text";
            MultilineText multilineText = new MultilineText(text);

            Assert.That(multilineText.RawText, Is.EqualTo(text));
        }

        [Test]
        public void Lines_contains_the_lines_of_text_extracted_from_provided_text()
        {
            MultilineText multilineText = new MultilineText("Some text\r123\nabcd\r\nSome other text");

            string[] expected = { "Some text", "123", "abcd", "Some other text" };
            Assert.That(multilineText.Lines, Is.EqualTo(expected));
        }
    }
}