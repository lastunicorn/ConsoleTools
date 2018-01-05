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

namespace DustInTheWind.ConsoleTools.TabularData
{
    internal class TitleCell : CellBase
    {
        private static HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Left;

        public TitleRow ParentRow { get; set; }

        protected override int CalculatePaddingLeft()
        {
            return ParentRow?.ParentTable?.PaddingLeft ?? 0;
        }

        protected override int CalculatePaddingRight()
        {
            return ParentRow?.ParentTable?.PaddingRight ?? 0;
        }

        protected override HorizontalAlignment CalculateHorizontalAlignment()
        {
            HorizontalAlignment alignment = HorizontalAlignment;
            
            if (alignment == HorizontalAlignment.Default)
                alignment = DefaultHorizontalAlignment;

            return alignment;
        }
    }
}