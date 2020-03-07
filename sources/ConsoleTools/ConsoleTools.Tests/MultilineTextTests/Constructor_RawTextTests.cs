// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.MultilineTextTests
{
    [TestFixture]
    public class Constructor_RawTextTests
    {
        [Test]
        public void RawText_is_same_as_the_original_text()
        {
            const string text = "Some text\r123\nabcd\r\nSome other text";
            MultilineText multilineText = new MultilineText(text);

            Assert.That(multilineText.RawText, Is.EqualTo(text));
        }
    }
}