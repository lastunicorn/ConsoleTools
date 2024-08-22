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

namespace DustInTheWind.ConsoleTools.Tests.Controls.ControlRepeaterTests.RenderingTests;

[TestFixture]
public class ControlRepeater_Tests : TestsBase
{
    [Test]
    public void HavingRenderAsRootAndMaxWidth()
    {
        ControlRepeater controlRepeater = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog.")
            {
                MaxWidth = 20,
                Margin = "2 1"
            },
            RenderContentAsRoot = true
        };

        string expected = GetResourceFileContent("01-as-root.txt");
        controlRepeater.IsEqualTo(expected);
    }

    [Test]
    public void HavingRenderAsChildAndMaxWidth()
    {
        ControlRepeater controlRepeater = new()
        {
            Content = new TextBlock("The quick brown fox jumps over the lazy dog.")
            {
                MaxWidth = 20,
                Margin = "2 1"
            },
            RenderContentAsRoot = false
        };

        string expected = GetResourceFileContent("02-as-child-block.txt");
        controlRepeater.IsEqualTo(expected);
    }
}