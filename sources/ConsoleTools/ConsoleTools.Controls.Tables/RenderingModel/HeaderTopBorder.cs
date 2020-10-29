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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Tables.RenderingModel
{
    internal class HeaderTopBorder
    {
        private readonly BorderTemplate borderTemplate;
        private string borderText;
        private List<int> columnsWidths;

        public List<int> ColumnsWidths
        {
            get => columnsWidths;
            set
            {
                if (value == columnsWidths)
                    return;

                columnsWidths = value;
                borderText = null;
            }
        }

        public HeaderTopBorder(BorderTemplate borderTemplate)
        {
            this.borderTemplate = borderTemplate ?? throw new ArgumentNullException(nameof(borderTemplate));
        }

        public void Render(ITablePrinter tablePrinter)
        {
            if (borderText == null)
                borderText = borderTemplate.GenerateTopBorder(columnsWidths);

            tablePrinter.WriteLineBorder(borderText);
        }
    }
}