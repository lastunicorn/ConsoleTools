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

using System.Collections.Generic;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.MultilineTextTests
{
    [TestFixture]
    public class Constructor_EmptyTests
    {
        [Test]
        public void Constructor_with_null_string()
        {
            MultilineText multilineText = new MultilineText(null as string);

            Assert.That(multilineText.RawText, Is.EqualTo(string.Empty));
            Assert.That(multilineText.Lines.Count, Is.EqualTo(0));
            Assert.That(multilineText.Size, Is.EqualTo(Size.Empty));
        }

        [Test]
        public void Constructor_with_empty_string()
        {
            MultilineText multilineText = new MultilineText(string.Empty);

            Assert.That(multilineText.RawText, Is.EqualTo(string.Empty));
            Assert.That(multilineText.Lines.Count, Is.EqualTo(0));
            Assert.That(multilineText.Size, Is.EqualTo(Size.Empty));
        }

        [Test]
        public void Constructor_with_empty_list()
        {
            MultilineText multilineText = new MultilineText(new List<string>());

            Assert.That(multilineText.RawText, Is.EqualTo(string.Empty));
            Assert.That(multilineText.Lines.Count, Is.EqualTo(0));
            Assert.That(multilineText.Size, Is.EqualTo(Size.Empty));
        }
    }
}