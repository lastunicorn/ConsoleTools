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
using Moq;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.ControlLayoutTests;

[TestFixture]
public class ActualFullWidthTests_NoWidthConstraints
{
    [Test]
    public void HorizontalAlignment_is_null__call_ActualFullWidth__returns_AvailableWidth()
    {
        Mock<BlockControl> control = new();

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ActualFullWidth;

        Assert.That(actual, Is.EqualTo(100));
    }

    [Test]
    public void HorizontalAlignment_is_Stretch__call_ActualFullWidth__returns_AvailableWidth()
    {
        Mock<BlockControl> control = new();
        control.Object.HorizontalAlignment = HorizontalAlignment.Stretch;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AllocatedWidth = 102
        };
        controlLayout.Calculate();

        int actual = controlLayout.ActualFullWidth;

        Assert.That(actual, Is.EqualTo(102));
    }
}