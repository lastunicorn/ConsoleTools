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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls
{
    public class Line : IEnumerable<LineSection>
    {
        public List<LineSection> PrefixSections { get; } = new List<LineSection>();

        public List<LineSection> ContentSections { get; } = new List<LineSection>();

        public List<LineSection> PostfixSection { get; } = new List<LineSection>();

        public Line()
        {
        }

        public Line(string text)
        {
            LineSection lineSection = new LineSection
            {
                Text = text
            };

            ContentSections.Add(lineSection);
        }

        public Line(string text, ConsoleColor foregroundColor)
        {
            LineSection lineSection = new LineSection
            {
                Text = text,
                ForegroundColor = foregroundColor
            };

            ContentSections.Add(lineSection);
        }

        public Line(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            LineSection lineSection = new LineSection
            {
                Text = text,
                ForegroundColor = foregroundColor,
                BackgroundColor = backgroundColor
            };

            ContentSections.Add(lineSection);
        }

        public void AddContent(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor, string text)
        {
            LineSection lineSection = new LineSection
            {
                Text = text,
                ForegroundColor = foregroundColor,
                BackgroundColor = backgroundColor
            };

            ContentSections.Add(lineSection);
        }

        public IEnumerator<LineSection> GetEnumerator()
        {
            IEnumerable<LineSection> allLineSections = PrefixSections
                .Concat(ContentSections)
                .Concat(PostfixSection);

            return allLineSections.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}