﻿// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.ConsoleTools.Demo.TabularData.Commands
{
    internal class SimpleBorderCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            DataGrid dataGrid = new DataGrid("Simple Border");

            dataGrid.Columns.Add("One");
            dataGrid.Columns.Add("Two");
            dataGrid.Columns.Add("Three");
            dataGrid.Columns.Add("Four");

            dataGrid.Rows.Add("1,1", "1,2", "1,3", "1,4");
            dataGrid.Rows.Add("2,1", "2,2", "2,3", "2,4");
            dataGrid.Rows.Add("3,1", "3,2", "3,3", "3,4");
            dataGrid.Rows.Add("4,1", "4,2", "4,3", "4,4");
            
            dataGrid.DisplayBorderBetweenRows = true;
            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Border.Template = BorderTemplate.PlusMinusBorderTemplate;

            dataGrid.Display();
        }
    }
}