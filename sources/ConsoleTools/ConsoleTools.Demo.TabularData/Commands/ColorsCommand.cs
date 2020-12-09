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

            dataGrid.Title = "Table without colors";

            dataGrid.DisplayBorderBetweenRows = true;
            dataGrid.DisplayColumnHeaders = true;
            dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;

            dataGrid.Display();
        }

        private static void DisplayColoredTable()
        {
            DataGrid dataGrid = CreateTable();

            dataGrid.Title = "Different foreground colors for border, title and column headers";

            dataGrid.ForegroundColor = ConsoleColor.Blue;
            dataGrid.BorderColor = ConsoleColor.DarkBlue;
            dataGrid.TitleColor = ConsoleColor.Yellow;
            dataGrid.HeaderColor = ConsoleColor.DarkYellow;

            dataGrid.DisplayBorderBetweenRows = true;
            dataGrid.DisplayColumnHeaders = true;
            dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;

            dataGrid.Display();
        }

        private static void DisplayBackgroundColoredTable()
        {
            DataGrid dataGrid = CreateTable();

            dataGrid.Title = "Custom global background color";

            dataGrid.ForegroundColor = ConsoleColor.Blue;
            dataGrid.BorderColor = ConsoleColor.DarkBlue;
            dataGrid.TitleColor = ConsoleColor.Yellow;
            dataGrid.HeaderColor = ConsoleColor.DarkYellow;

            dataGrid.BackgroundColor = ConsoleColor.Gray;

            dataGrid.DisplayBorderBetweenRows = true;
            dataGrid.DisplayColumnHeaders = true;
            dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;

            dataGrid.Display();
        }

        private static void DisplayBackgroundColoredTable2()
        {
            DataGrid dataGrid = CreateTable();

            dataGrid.Title = "Different background colors for border, title and column headers";

            dataGrid.ForegroundColor = ConsoleColor.Blue;
            dataGrid.BorderColor = ConsoleColor.DarkBlue;
            dataGrid.TitleColor = ConsoleColor.Yellow;
            dataGrid.HeaderColor = ConsoleColor.DarkYellow;

            dataGrid.BackgroundColor = ConsoleColor.Gray;
            dataGrid.BorderBackgroundColor = ConsoleColor.White;
            dataGrid.TitleBackgroundColor = ConsoleColor.DarkYellow;
            dataGrid.HeaderBackgroundColor = ConsoleColor.Yellow;

            dataGrid.DisplayBorderBetweenRows = true;
            dataGrid.DisplayColumnHeaders = true;
            dataGrid.BorderTemplate = BorderTemplate.SingleLineBorderTemplate;

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