// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Tables;
using DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.RenderingModel.CellXTests
{
    [TestFixture]
    public class RenderTextTests
    {
        private List<string> renderingOutput;
        private Mock<ITablePrinter> tablePrinter;

        [SetUp]
        public void SetUp()
        {
            renderingOutput = new List<string>();

            tablePrinter = new Mock<ITablePrinter>();
            tablePrinter
                .Setup(x => x.Write(It.IsAny<string>(), It.IsAny<ConsoleColor?>(), It.IsAny<ConsoleColor?>()))
                .Callback<string, ConsoleColor?, ConsoleColor?>((line, fg, bg) =>
                {
                    renderingOutput.Add(line);
                });
        }

        [Test]
        public void HavingContentShorterThanCellWidth_WhenRendered_LineIsFilledWithSpaces()
        {
            CellX cell = new CellX
            {
                Content = "text"
            };

            RenderAllLines(cell, new Size(10, 1));

            Assert.That(renderingOutput, Is.EqualTo(new[] { "text      " }));
        }

        [Test]
        public void HavingContentLongerThanCellWidth_WhenRendered_ThenLineIsNotTrimmed()
        {
            CellX cell = new CellX
            {
                Content = "some long text"
            };

            RenderAllLines(cell, new Size(10, 1));

            Assert.That(renderingOutput, Is.EqualTo(new List<string> { "some long text" }));
        }

        [Test]
        public void HavingContentWithLessLinesThanCellHeight_WhenRendered_ThenEmptyLinesAreAdded()
        {
            CellX cell = new CellX
            {
                Content = "text"
            };

            RenderAllLines(cell, new Size(10, 2));

            Assert.That(renderingOutput, Is.EqualTo(new List<string>
            {
                "text      ",
                "          "
            }));
        }

        [Test]
        public void HavingContentWithMoreLinesThanCellHeight_WhenRendered_ThenOnlyTheRequiredLinesAreRendered()
        {
            CellX cell = new CellX
            {
                Content = new MultilineText(new[] { "line1", "line2", "line3" }),
            };

            RenderAllLines(cell, new Size(10, 2));

            Assert.That(renderingOutput, Is.EqualTo(new List<string>
            {
                "line1     ",
                "line2     "
            }));
        }

        [Test]
        public void HavingPaddingLeftAndContentShorterThanCellWidth_WhenRendered_LineContainsPaddingLeft()
        {
            CellX cell = new CellX
            {
                Content = "text",
                PaddingLeft = 2
            };

            RenderAllLines(cell, new Size(10, 1));

            Assert.That(renderingOutput, Is.EqualTo(new[] { "  text    " }));
        }

        [Test]
        public void HavingPaddingRightAndContentShorterThanCellWidth_WhenRendered_LineContainsPaddingRight()
        {
            CellX cell = new CellX
            {
                Content = "text",
                PaddingRight = 2,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            RenderAllLines(cell, new Size(10, 1));

            Assert.That(renderingOutput, Is.EqualTo(new[] { "    text  " }));
        }

        private void RenderAllLines(CellX cellX, Size size)
        {
            for (int i = 0; i < size.Height; i++)
                cellX.RenderNextLine(tablePrinter.Object, size);
        }
    }
}