// ConsoleTools
// Copyright (C) 2017-2024 Dust in the Wind
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

using System;
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

internal class MultiSectionRenderer : IRenderer
{
    private readonly List<SectionRenderer> sections = new();
    private IEnumerator<SectionRenderer> sectionEnumerator;

    public bool HasMoreLines => sectionEnumerator != null;

    public void Add(SectionRenderer sectionRenderer)
    {
        if (sectionEnumerator != null)
            throw new Exception("No section can be added while the rendering is in progress.");

        if (sectionRenderer == null) throw new ArgumentNullException(nameof(sectionRenderer));

        sections.Add(sectionRenderer);
    }

    public void RenderNextLine()
    {
        if (sectionEnumerator == null)
            return;

        sectionEnumerator.Current.RenderNextLine();

        if (!sectionEnumerator.Current.HasMoreLines)
            MoveToNextSection();
    }

    private void MoveToNextSection()
    {
        if (sectionEnumerator == null)
            return;

        while (true)
        {
            bool success = sectionEnumerator.MoveNext();

            if (success)
            {
                sectionEnumerator.Current.Reset();

                if (sectionEnumerator.Current.HasMoreLines)
                    return;
            }
            else
            {
                sectionEnumerator = null;
                return;
            }
        }
    }

    public void Reset()
    {
        sectionEnumerator = sections.GetEnumerator();
        MoveToNextSection();
    }
}