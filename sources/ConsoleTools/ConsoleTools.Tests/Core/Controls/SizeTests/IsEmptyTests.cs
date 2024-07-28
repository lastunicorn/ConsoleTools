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
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Core.Controls.SizeTests;

[TestFixture]
public class IsEmptyTests
{
    [Test]
    public void if_Width_and_Height_are_0_IsEmpty_is_true()
    {
        Size size = new(0, 0);

        Assert.That(size.IsEmpty, Is.True);
    }

    [Test]
    public void if_Width_is_0_and_Height_is_not_0_IsEmpty_is_false()
    {
        Size size = new(0, 7);

        Assert.That(size.IsEmpty, Is.False);
    }

    [Test]
    public void if_Width_is_not_0_and_Height_is_0_IsEmpty_is_false()
    {
        Size size = new(2, 0);

        Assert.That(size.IsEmpty, Is.False);
    }

    [Test]
    public void if_both_Width_and_Height_are_not_0_IsEmpty_is_false()
    {
        Size size = new(4, 5);

        Assert.That(size.IsEmpty, Is.False);
    }
}