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

using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustInTheWind.ConsoleTools.Demo.TabularData.Commands
{
    internal class DoubleLineBorderCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            Table table = new Table("Double-line Border");
            table.Columns.Add(new Column("One"));
            table.Columns.Add(new Column("Two"));
            table.Columns.Add(new Column("Three"));
            table.Columns.Add(new Column("Four"));
            table.AddRow(new[] { "1,1", "1,2", "1,3", "1,4" });
            table.AddRow(new[] { "2,1", "2,2", "2,3", "2,4" });
            table.AddRow(new[] { "3,1", "3,2", "3,3", "3,4" });
            table.AddRow(new[] { "4,1", "4,2", "4,3", "4,4" });
            table.DisplayBorderBetweenRows = true;
            table.DisplayColumnHeaders = true;
            table.BorderTemplate = BorderTemplate.DoubleLineBorderTemplate;

            CustomConsole.WriteLine(table.ToString());
        }
    }
}