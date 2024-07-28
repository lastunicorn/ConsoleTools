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
using Moq;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.ControlLayoutTests;

[TestFixture]
public class ActualWidthTests_NoWidthConstraints
{
    [Test]
    public void HorizontalAlignment_is_null__call_ActualWidth__returns_AvailableWidth_without_Margins()
    {
        Mock<BlockControl> control = new();
        control.Object.Margin = 10;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AvailableWidth = 100
        };
        controlLayout.Calculate();

        int actual = controlLayout.ActualWidth;

        Assert.That(actual, Is.EqualTo(80));
    }

    [Test]
    public void HorizontalAlignment_is_Stretch__call_ActualWidth__returns_AvailableWidth_without_Margins()
    {
        Mock<BlockControl> control = new();
        control.Object.HorizontalAlignment = HorizontalAlignment.Stretch;
        control.Object.Margin = 10;

        ControlLayout controlLayout = new()
        {
            Control = control.Object,
            AvailableWidth = 102
        };
        controlLayout.Calculate();

        int actual = controlLayout.ActualWidth;

        Assert.That(actual, Is.EqualTo(82));
    }
}