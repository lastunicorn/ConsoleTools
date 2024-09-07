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
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.ControlLayoutTests;

[TestFixture]
public class ContentWidthTests
{
    [Test]
    public void WhenHorizontalAlignmentIsNotSpecified()
    {
        Mock<BlockControl> control = new();
        control.Object.Margin = 3;
        control.Object.Padding = 2;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ContentSize.Width;

        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void WhenHorizontalAlignmentIsLeft()
    {
        Mock<BlockControl> control = new();
        control.Object.HorizontalAlignment = HorizontalAlignment.Left;
        control.Object.Margin = 3;
        control.Object.Padding = 2;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ContentSize.Width;

        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void WhenHorizontalAlignmentIsCenter()
    {
        Mock<BlockControl> control = new();
        control.Object.HorizontalAlignment = HorizontalAlignment.Center;
        control.Object.Margin = 3;
        control.Object.Padding = 2;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ContentSize.Width;

        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void WhenHorizontalAlignmentIsRight()
    {
        Mock<BlockControl> control = new();
        control.Object.HorizontalAlignment = HorizontalAlignment.Right;
        control.Object.Margin = 3;
        control.Object.Padding = 2;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ContentSize.Width;

        Assert.That(actual, Is.EqualTo(0));
    }

    [Test]
    public void WhenHorizontalAlignmentIsStretch()
    {
        Mock<BlockControl> control = new();
        control.Object.Margin = 3;
        control.Object.Padding = 2;
        control.Object.HorizontalAlignment = HorizontalAlignment.Stretch;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ContentSize.Width;

        Assert.That(actual, Is.EqualTo(90));
    }
}