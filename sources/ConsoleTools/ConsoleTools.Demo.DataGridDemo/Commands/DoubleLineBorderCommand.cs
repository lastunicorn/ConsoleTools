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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.ConsoleTools.Demo.DataGridDemo.Commands
{
    internal class DoubleLineBorderCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            DataGrid dataGrid = new DataGrid("Double-line Border");

            dataGrid.Columns.Add("One");
            dataGrid.Columns.Add("Two");
            dataGrid.Columns.Add("Three");
            dataGrid.Columns.Add("Four");

            dataGrid.Rows.Add("1,1", "1,2", "1,3", "1,4");
            dataGrid.Rows.Add("2,1", "2,2", "2,3", "2,4");
            dataGrid.Rows.Add("3,1", "3,2", "3,3", "3,4");
            dataGrid.Rows.Add("4,1", "4,2", "4,3", "4,4");

            dataGrid.FooterRow.FooterCell.Content = "Something";

            dataGrid.DisplayBorderBetweenRows = true;
            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Border.Template = BorderTemplate.DoubleLineBorderTemplate;

            dataGrid.Display();
        }
    }
}