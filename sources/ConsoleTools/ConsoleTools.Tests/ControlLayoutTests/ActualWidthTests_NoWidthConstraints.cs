// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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

using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.ControlLayoutTests
{
    [TestFixture]
    public class ActualWidthTests_NoWidthConstraints
    {
        [Test]
        public void HorizontalAlignment_is_null__returns_AvailableWidth_without_Margins()
        {
            ControlLayout controlLayout = new ControlLayout
            {
                AvailableWidth = 100,
                Margin = 10
            };
            controlLayout.Calculate();
            
            int actual = controlLayout.ActualWidth;

            Assert.That(actual, Is.EqualTo(80));
        }

        [Test]
        public void HorizontalAlignment_is_Stretch__returns_AvailableWidth_without_Margins()
        {
            ControlLayout controlLayout = new ControlLayout
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                AvailableWidth = 102,
                Margin = 10
            };
            controlLayout.Calculate();

            int actual = controlLayout.ActualWidth;

            Assert.That(actual, Is.EqualTo(82));
        }
    }
}