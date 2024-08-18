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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Controls.Tables;

namespace DustInTheWind.ConsoleTools.Demo.TabularData.Commands
{
    internal class ColorsCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            DisplayUncoloredTable();
            DisplayColoredTable();
            DisplayBackgroundColoredTable();
            DisplayBackgroundColoredTable2();
        }

        private static void DisplayUncoloredTable()
        {
            DataGrid dataGrid = CreateTable();
            dataGrid.Title = "No colors";

            dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;
            dataGrid.DisplayBorderBetweenRows = true;

            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Display();
        }

        private static void DisplayColoredTable()
        {
            DataGrid dataGrid = CreateTable();
            dataGrid.Title = "Text and border colors";

            dataGrid.ForegroundColor = ConsoleColor.Blue;

            dataGrid.TitleRow.ForegroundColor = ConsoleColor.Yellow;

            dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;
            dataGrid.BorderForegroundColor = ConsoleColor.DarkGreen;
            dataGrid.DisplayBorderBetweenRows = true;

            dataGrid.HeaderRow.ForegroundColor = ConsoleColor.DarkYellow;
            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Display();
        }

        private static void DisplayBackgroundColoredTable()
        {
            DataGrid dataGrid = CreateTable();
            dataGrid.Title = "Background colors";

            dataGrid.ForegroundColor = ConsoleColor.Blue;
            dataGrid.BackgroundColor = ConsoleColor.Gray;

            dataGrid.TitleRow.ForegroundColor = ConsoleColor.Yellow;
            dataGrid.TitleRow.BackgroundColor = ConsoleColor.DarkYellow;

            dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;
            dataGrid.BorderForegroundColor = ConsoleColor.DarkGreen;
            dataGrid.DisplayBorderBetweenRows = true;

            dataGrid.HeaderRow.ForegroundColor = ConsoleColor.DarkYellow;
            dataGrid.HeaderRow.BackgroundColor = ConsoleColor.Yellow;
            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Display();
        }

        private static void DisplayBackgroundColoredTable2()
        {
            DataGrid dataGrid = CreateTable();
            dataGrid.Title = "Background color (for borders)";

            dataGrid.ForegroundColor = ConsoleColor.Blue;
            dataGrid.BackgroundColor = ConsoleColor.Gray;

            dataGrid.TitleRow.ForegroundColor = ConsoleColor.Yellow;
            dataGrid.TitleRow.BackgroundColor = ConsoleColor.DarkYellow;

            dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;
            dataGrid.BorderForegroundColor = ConsoleColor.DarkGreen;
            dataGrid.BorderBackgroundColor = ConsoleColor.White;
            dataGrid.DisplayBorderBetweenRows = true;

            dataGrid.HeaderRow.ForegroundColor = ConsoleColor.DarkYellow;
            dataGrid.HeaderRow.BackgroundColor = ConsoleColor.Yellow;
            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Display();
        }

        private static DataGrid CreateTable()
        {
            DataGrid dataGrid = new DataGrid
            {
                Margin = 1,
                MinWidth = 70
            };

            dataGrid.Columns.Add("Name");
            dataGrid.Columns.Add("Age");
            dataGrid.Columns.Add("Salary");

            dataGrid.Rows.Add("Gabriel", 20, 1000);
            dataGrid.Rows.Add("Helen", 50, 2500);
            dataGrid.Rows.Add("Bob", 34, 2000);

            return dataGrid;
        }
    }
}