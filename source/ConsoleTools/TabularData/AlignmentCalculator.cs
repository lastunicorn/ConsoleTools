//using System.Collections.Generic;
//using System.Collections.ObjectModel;

//namespace DustInTheWind.ConsoleTools.TabularData
//{
//    internal class AlignmentCalculator
//    {
//        public HorizontalAlignment DefaultHorizontalAlignment { get; set; } = HorizontalAlignment.Left;

//        public HorizontalAlignment TableLevelCellAlignment { get; set; }

//        public ReadOnlyCollection<Column> Columns { get; set; }
//        public List<Row> Rows { get; set; }
        
//        public HorizontalAlignment CalcualteHeaderCellAlignment(int columnIndex)
//        {
//            return CalculateDataCellHorizontalAlignmentAtColumnLevel(columnIndex);
//        }

//        public HorizontalAlignment CalcualteDataCellAlignment(int rowIndex, int columnIndex)
//        {
//            return CalculateDataCellHorizontalAlignmentAtCellLevel(rowIndex, columnIndex);
//        }

//        private HorizontalAlignment CalculateDataCellHorizontalAlignmentAtCellLevel(int rowIndex, int columnIndex)
//        {
//            Cell cell = Rows[rowIndex][columnIndex];

//            HorizontalAlignment alignment = cell.HorizontalAlignment;

//            if (alignment == HorizontalAlignment.Default)
//                alignment = CalculateDataCellHorizontalAlignmentAtColumnLevel(columnIndex);

//            return alignment;
//        }

//        private HorizontalAlignment CalculateDataCellHorizontalAlignmentAtColumnLevel(int columnIndex)
//        {
//            Column column = columnIndex < Columns.Count
//                ? Columns[columnIndex]
//                : null;

//            HorizontalAlignment alignment = column?.CellHorizontalAlignment ?? HorizontalAlignment.Default;

//            if (alignment == HorizontalAlignment.Default)
//                alignment = CalculateDataCellHorizontalAlignmentAtTableLevel();

//            return alignment;
//        }

//        private HorizontalAlignment CalculateDataCellHorizontalAlignmentAtTableLevel()
//        {
//            return TableLevelCellAlignment == HorizontalAlignment.Default
//                ? DefaultHorizontalAlignment
//                : TableLevelCellAlignment;
//        }
//    }
//}