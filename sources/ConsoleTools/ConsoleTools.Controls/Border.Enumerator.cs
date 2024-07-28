// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls
{
    public partial class Border
    {
        private class Enumerator : LineEnumerator
        {
            private readonly Border border;

            public Enumerator(Border border, IDisplay display)
                : base(display)
            {
                this.border = border ?? throw new ArgumentNullException(nameof(border));
            }

            protected override IEnumerable<Line> GetContentLines(IDisplay display)
            {
                ConsoleColor? calculatedForegroundColor = null;
                ConsoleColor? calculatedBackgroundColor = null;
                IEnumerator<Line> childControlEnumerator = null;

                if (border.Control != null)
                {
                    calculatedForegroundColor = border.BorderForegroundColor ?? border.ForegroundColor ?? border.Control?.ForegroundColor;
                    calculatedBackgroundColor = border.BorderBackgroundColor;

                    IDisplay childDisplay = border.Control.CreateDisplay(display, display.ControlLayout.ActualClientWidth - 2);
                    childControlEnumerator = border.Control.GetLineEnumerator(childDisplay);
                }

                string topBorder = border.BorderTemplate.GenerateTopBorder(display.ControlLayout.ActualClientWidth - 2);
                yield return display.CreateNewLine(calculatedForegroundColor, calculatedBackgroundColor, topBorder);


                if (childControlEnumerator != null)
                {
                    while (childControlEnumerator.MoveNext())
                    {
                        Line line = display.CreateNewLine();

                        line.AddContent(calculatedForegroundColor, calculatedBackgroundColor, border.BorderTemplate.Left.ToString());

                        if (childControlEnumerator.Current != null)
                            line.ContentSections.AddRange(childControlEnumerator.Current);
                        line.AddContent(calculatedForegroundColor, calculatedBackgroundColor, border.BorderTemplate.Right.ToString());

                        yield return line;
                    }
                }

                string bottomBorder = border.BorderTemplate.GenerateBottomBorder(display.ControlLayout.ActualClientWidth - 2);
                yield return display.CreateNewLine(calculatedForegroundColor, calculatedBackgroundColor, bottomBorder);
            }
        }
    }
}