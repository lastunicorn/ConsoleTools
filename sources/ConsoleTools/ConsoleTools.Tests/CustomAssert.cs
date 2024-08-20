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

using System;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;
using FluentAssertions;

namespace DustInTheWind.ConsoleTools.Tests;

internal static class CustomAssert
{
    //public static void IsEqualTo(this DataGrid dataGrid, string expected)
    //{
    //    string actual = dataGrid.ToString();

    //    Console.WriteLine("actual:");
    //    Console.WriteLine(actual);

    //    Console.WriteLine("expected:");
    //    Console.WriteLine(expected);

    //    actual.Should().Be(expected);
    //}

    //public static void IsEqualTo(this TextBlock textBlock, string expected)
    //{
    //    string actual = textBlock.ToString();

    //    Console.WriteLine("actual:");
    //    Console.WriteLine(actual);

    //    Console.WriteLine("expected:");
    //    Console.WriteLine(expected);

    //    actual.Should().Be(expected);
    //}

    public static void IsEqualTo(this Control textBlock, string expected)
    {
        string actual = textBlock.ToString();

        Console.WriteLine("actual:");
        Console.WriteLine(actual);

        Console.WriteLine("expected:");
        Console.WriteLine(expected);

        actual.Should().Be(expected);
    }
}