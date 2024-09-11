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
using DustInTheWind.ConsoleTools.Controls.Rendering;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.ControlLayoutTests;

[TestFixture]
public class ActualContentWidthTests_NoWidthConstraints
{
    [Test]
    public void HorizontalAlignment_is_null_NaturalContentWidth_is_not_specified__returns_AvailableWidth_without_Margins_and_Paddings()
    {
        Mock<BlockControl> control = new();
        control.Object.Margin = 10;
        control.Object.Padding = 7;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ActualContentWidth;

        Assert.That(actual, Is.EqualTo(66));
    }

    [Test]
    public void HorizontalAlignment_is_Stretch_NaturalContentWidth_is_not_specified__returns_AvailableWidth_without_Margins_and_Paddings()
    {
        Mock<BlockControl> control = new();
        control.Object.HorizontalAlignment = HorizontalAlignment.Stretch;
        control.Object.Margin = 10;
        control.Object.Padding = 7;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 102
        };
        controlLayout.Calculate();

        int actual = controlLayout.ActualContentWidth;

        Assert.That(actual, Is.EqualTo(68));
    }

    [Test]
    public void HorizontalAlignment_is_notset_NaturalContentWidth_is_less_than_allocated_space__returns_maximum_content_width()
    {
        Mock<BlockControl> control = new();
        control.Object.Margin = 10;
        control.Object.Padding = 7;

        control.Protected()
            .Setup<int>("NaturalContentWidth")
            .Returns(20);

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ActualContentWidth;

        Assert.That(actual, Is.EqualTo(100 - 10 - 10 - 7 - 7));
    }

    [Test]
    public void HorizontalAlignment_is_Stretch_NaturalContentWidth_is_more_than_available_space__returns_AvailableWidth_without_Margins_and_Paddings()
    {
        Mock<BlockControl> control = new();
        control.Object.HorizontalAlignment = HorizontalAlignment.Stretch;
        control.Object.Margin = 10;
        control.Object.Padding = 7;

        control.Protected()
            .Setup<int>("NaturalContentWidth")
            .Returns(200);

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 102
        };
        controlLayout.Calculate();

        int actual = controlLayout.ActualContentWidth;

        Assert.That(actual, Is.EqualTo(68));
    }
}