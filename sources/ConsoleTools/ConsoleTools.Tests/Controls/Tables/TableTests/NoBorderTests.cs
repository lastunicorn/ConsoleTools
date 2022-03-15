﻿// ConsoleTools
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

using DustInTheWind.ConsoleTools.Controls.Tables;
using NUnit.Framework;

namespace DustInTheWind.ConsoleTools.Tests.Controls.Tables.TableTests
{
    [TestFixture]
    public class NoBorderTests
    {
        [Test]
        public void render_simple_table_without_border()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Border.IsVisible = false;
            dataGrid.Rows.Add("one", "ichi", "eins");
            dataGrid.Rows.Add("two", "ni", "zwei");
            dataGrid.Rows.Add("three", "san", "drei");

            string expected =
                @" one    ichi  eins 
 two    ni    zwei 
 three  san   drei 
";

            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void render_table_with_title_and_no_border()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Border.IsVisible = false;
            dataGrid.Title = "My Title";
            dataGrid.Rows.Add("one", "ichi", "eins");
            dataGrid.Rows.Add("two", "ni", "zwei");
            dataGrid.Rows.Add("three", "san", "drei");

            string expected =
                @" My Title          
 one    ichi  eins 
 two    ni    zwei 
 three  san   drei 
";

            CustomAssert.TableRender(dataGrid, expected);
        }

        [Test]
        public void render_table_with_title_and_headers_and_no_border()
        {
            DataGrid dataGrid = new DataGrid();
            dataGrid.Border.IsVisible = false;
            dataGrid.Title = "My Title";
            dataGrid.Columns.Add("1");
            dataGrid.Columns.Add("2");
            dataGrid.Columns.Add("3");
            dataGrid.Rows.Add("one", "ichi", "eins");
            dataGrid.Rows.Add("two", "ni", "zwei");
            dataGrid.Rows.Add("three", "san", "drei");

            string expected =
                @" My Title          
 1      2     3    
 one    ichi  eins 
 two    ni    zwei 
 three  san   drei 
";

            CustomAssert.TableRender(dataGrid, expected);
        }
    }
}