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

namespace DustInTheWind.ConsoleTools.Tests.Controls.TextBlockTests;

[TestFixture]
[Explicit("Some Console calls crash in the Resharper's test runner.")]
public class TextBlockTests
{
    private readonly ExpectedOutput expectedOutput = new(typeof(TextBlockTests), "TextBlockTests");

    [Test]
    public void Test1()
    {
        using ConsoleOutput consoleOutput = new();

        TextBlock textBlock = new()
        {
            Text = "alez"
        };

        textBlock.Display();

        string actual = consoleOutput.GetOutput();

        string expected = expectedOutput.GeExpectedOut();
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void Test2()
    {
        using ConsoleOutput consoleOutput = new();

        TextBlock textBlock = new()
        {
            Text = "alez",
            Margin = 1
        };

        textBlock.Display();

        string actual = consoleOutput.GetOutput();

        string expected = expectedOutput.GeExpectedOut();
        Assert.AreEqual(expected, actual);
    }
}