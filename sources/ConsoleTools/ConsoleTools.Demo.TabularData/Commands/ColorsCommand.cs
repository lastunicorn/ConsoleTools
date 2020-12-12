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

            dataGrid.Border.Template = BorderTemplate.SingleLineBorderTemplate;
            dataGrid.Border.DisplayBorderBetweenRows = true;

            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Display();
        }

        private static void DisplayColoredTable()
        {
            DataGrid dataGrid = CreateTable();

            dataGrid.ForegroundColor = ConsoleColor.Blue;

            dataGrid.Title = "Different foreground colors for border, title and column headers";
            dataGrid.TitleRow.ForegroundColor = ConsoleColor.Yellow;

            dataGrid.Border.ForegroundColor = ConsoleColor.DarkBlue;
            dataGrid.Border.Template = BorderTemplate.SingleLineBorderTemplate;
            dataGrid.Border.DisplayBorderBetweenRows = true;

            dataGrid.HeaderRow.ForegroundColor = ConsoleColor.DarkYellow;
            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Display();
        }

        private static void DisplayBackgroundColoredTable()
        {
            DataGrid dataGrid = CreateTable();

            dataGrid.ForegroundColor = ConsoleColor.Blue;
            dataGrid.BackgroundColor = ConsoleColor.Gray;

            dataGrid.Title = "Custom global background color";
            dataGrid.TitleRow.ForegroundColor = ConsoleColor.Yellow;

            dataGrid.Border.ForegroundColor = ConsoleColor.DarkBlue;
            dataGrid.Border.Template = BorderTemplate.SingleLineBorderTemplate;
            dataGrid.Border.DisplayBorderBetweenRows = true;
            
            dataGrid.HeaderRow.ForegroundColor = ConsoleColor.DarkYellow;
            dataGrid.HeaderRow.IsVisible = true;

            dataGrid.Display();
        }

        private static void DisplayBackgroundColoredTable2()
        {
            DataGrid dataGrid = CreateTable();

            dataGrid.ForegroundColor = ConsoleColor.Blue;
            dataGrid.BackgroundColor = ConsoleColor.Gray;

            dataGrid.Title = "Different background colors for border, title and column headers";
            dataGrid.TitleRow.ForegroundColor = ConsoleColor.Yellow;
            dataGrid.TitleRow.BackgroundColor = ConsoleColor.DarkYellow;

            dataGrid.Border.Template = BorderTemplate.SingleLineBorderTemplate;
            dataGrid.Border.ForegroundColor = ConsoleColor.DarkBlue;
            dataGrid.Border.BackgroundColor = ConsoleColor.White;
            dataGrid.Border.DisplayBorderBetweenRows = true;
            
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