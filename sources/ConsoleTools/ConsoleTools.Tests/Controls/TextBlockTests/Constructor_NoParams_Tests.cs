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
using FluentAssertions;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.TextBlockTests;

[TestFixture]
public class Constructor_NoParams_Tests
{
    private TextBlock textBlock;

    [SetUp]
    public void SetUp()
    {
        textBlock = new TextBlock();
    }

    [Test]
    public void WhenCreatingNewInstance_ThenTextIsNull()
    {
        textBlock.Text.Should().BeNull();
    }

    [Test]
    public void WhenCreatingNewInstance_ThenOverflowBehaviorIsWrapWord()
    {
        textBlock.OverflowBehavior.Should().Be(OverflowBehavior.WrapWord);
    }

    [Test]
    public void WhenCreatingNewInstance_ThenTextHorizontalAlignmentIsDefault()
    {
        textBlock.TextHorizontalAlignment.Should().Be(HorizontalAlignment.Default);
    }

    [Test]
    public void WhenCreatingNewInstance_ThenNaturalContentWidthIsZero()
    {
        textBlock.NaturalContentWidth.Should().Be(0);
    }
}