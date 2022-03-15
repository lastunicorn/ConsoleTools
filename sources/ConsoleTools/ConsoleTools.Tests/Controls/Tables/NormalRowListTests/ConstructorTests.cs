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
using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.NormalRowListTests
{
    [TestFixture]
    public class ConstructorTests
    {
        [Test]
        public void HavingNullDataGrid_WhenNormalRowListIsInstantiated_ThenThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ContentRowList contentRowList = new ContentRowList(null);
            });
        }

        [Test]
        public void HavingValidDataGrid_WhenNormalRowListIsInstantiated_ThenSuccess()
        {
            DataGrid dataGrid = new DataGrid();

            ContentRowList contentRowList = new ContentRowList(dataGrid);
        }
    }
}