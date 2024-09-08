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

using DustInTheWind.ConsoleTools.Controls.Tables;
using DustInTheWind.ConsoleTools.Demo.Core;

namespace DustInTheWind.ConsoleTools.Demo.DataGridDemo.Demo.CellPaddingDemo;

internal class PaddingLeftDemo : DemoBase
{
    public override string Title => "Padding left";

    protected override void DoExecute()
    {
        DataGrid dataGrid = new("Padding left = 10");

        dataGrid.Rows.Add("First item", 1.ToString());
        dataGrid.Rows.Add("Second item", 2.ToString());
        dataGrid.Rows.Add("Third item", 3.ToString());
        dataGrid.Rows.Add("Forth item", 4.ToString());
        dataGrid.Rows.Add("Fifth item", 5.ToString());

        dataGrid.CellPaddingLeft = 10;

        dataGrid.Display();
    }
}