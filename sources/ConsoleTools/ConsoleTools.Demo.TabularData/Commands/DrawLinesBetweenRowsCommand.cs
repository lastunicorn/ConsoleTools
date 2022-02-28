// ConsoleTools
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
    internal class DrawLinesBetweenRowsCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            DataGrid dataGrid = new DataGrid("DisplayBorderBetweenRows = true");
            dataGrid.Rows.Add("This is a multiline row:\nline one\nline two\nline three", "1");
            dataGrid.Rows.Add("And this is another\nmultiline row", "2");
            dataGrid.DisplayBorderBetweenRows = true;

            dataGrid.Display();
        }
    }
}