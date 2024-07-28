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
        private class Enumerator : LineEnumerator
        {
            private readonly ApplicationHeader applicationHeader;

            private readonly string titleRowText;
            private readonly MultilineText descriptionText;

            public Enumerator(ApplicationHeader applicationHeader, IDisplay display)
                : base(display)
            {
                this.applicationHeader = applicationHeader ?? throw new ArgumentNullException(nameof(applicationHeader));

                titleRowText = applicationHeader.BuildTitleText();
                descriptionText = applicationHeader.BuildDescription();
            }

            protected override IEnumerable<Line> GetContentLines(IDisplay display)
            {
                yield return GenerateTitleLine(display);

                if (applicationHeader.IsSeparatorVisible)
                    yield return GenerateSeparatorLine(display);

                IEnumerable<Line> descriptionLines = GenerateDescriptionLines(display);

                foreach (Line descriptionLine in descriptionLines)
                    yield return descriptionLine;
            }

            private Line GenerateTitleLine(IDisplay display)
            {
                return display.CreateNewLine(titleRowText);
            }

            private static Line GenerateSeparatorLine(IDisplay display)
            {
                string text = new string('=', display.AvailableWidth);
                return display.CreateNewLine(text);
            }

            private IEnumerable<Line> GenerateDescriptionLines(IDisplay display)
            {
                if (descriptionText == null)
                    yield break;

                IEnumerable<string> enumerable = descriptionText.GetLines(display.AvailableWidth);
                using (IEnumerator<string> enumerator = enumerable?.GetEnumerator())
                {
                    if (enumerator == null)
                        yield break;

                    bool hasMoreLines = enumerator.MoveNext();

                    if (!hasMoreLines)
                        yield break;

                    yield return display.CreateNewLine();

                    do
                    {
                        yield return display.CreateNewLine(enumerator.Current);
                    } while (enumerator.MoveNext());
                }
            }
        }
    }
}