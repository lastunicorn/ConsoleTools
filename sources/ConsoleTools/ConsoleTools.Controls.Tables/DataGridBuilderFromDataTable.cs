using System.Data;

namespace DustInTheWind.ConsoleTools.Controls.Tables
{
    internal class DataGridBuilderFromDataTable
    {
        public DataGrid DataGrid { get; }

        public DataGridBuilderFromDataTable(DataTable dataTable)
        {
            DataGrid = new DataGrid(dataTable.TableName);

            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                string columnHeader = string.IsNullOrEmpty(dataColumn.Caption)
                    ? dataColumn.ColumnName
                    : dataColumn.Caption;

                DataGrid.Columns.Add(columnHeader);
            }

            foreach (System.Data.DataRow dataRow in dataTable.Rows)
            {
                DataRow row = new DataRow(dataRow.ItemArray);
                DataGrid.Rows.Add(row);
            }
        }
    }
}