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
    public partial class ApplicationHeader
    {
        private class Renderer : ControlRenderer
        {
            private readonly ApplicationHeader applicationHeader;
            private bool isTitleRowRendered;
            private bool isSeparatorRendered;
            private bool isDescriptionRendered;

            private readonly string titleRowText;
            private readonly MultilineText descriptionText;
            private IEnumerator<string> descriptionEnumerator;

            protected override bool HasMoreContentRows => !isTitleRowRendered ||
                                                          (applicationHeader.IsSeparatorVisible && !isSeparatorRendered) ||
                                                          (descriptionText != null && !isDescriptionRendered);

            public Renderer(ApplicationHeader applicationHeader, IDisplay display)
                : base(display)
            {
                this.applicationHeader = applicationHeader ?? throw new ArgumentNullException(nameof(applicationHeader));

                titleRowText = applicationHeader.BuildTitleText();
                descriptionText = applicationHeader.BuildDescription();
            }

            protected override void RenderNextContentRow()
            {
                if (!isTitleRowRendered)
                    DisplayTitleRow();
                else if (applicationHeader.IsSeparatorVisible && !isSeparatorRendered)
                    DisplaySeparatorRow();
                else
                    DisplayNextDescriptionRow();
            }

            private void DisplayTitleRow()
            {
                Display.StartRow();
                Display.Write(titleRowText);
                Display.EndRow();

                isTitleRowRendered = true;
            }

            private void DisplaySeparatorRow()
            {
                string separatorText = new string('=', Display.AvailableWidth);
                Display.WriteRow(separatorText);

                isSeparatorRendered = true;
            }

            private void DisplayNextDescriptionRow()
            {
                if (descriptionText != null)
                {
                    if (descriptionEnumerator == null)
                    {
                        descriptionEnumerator = descriptionText.GetLines(Display.AvailableWidth).GetEnumerator();
                        isDescriptionRendered = !descriptionEnumerator.MoveNext();

                        if (!isDescriptionRendered)
                            Display.WriteRow();
                    }
                    else
                    {
                        Display.WriteRow(descriptionEnumerator.Current);
                        isDescriptionRendered = !descriptionEnumerator.MoveNext();
                    }
                }
                else
                {
                    isDescriptionRendered = true;
                }
            }
        }
    }
}