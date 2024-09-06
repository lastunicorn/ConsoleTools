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

using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

internal class TextMenuRenderer : BlockControlRenderer<TextMenu>
{
    private bool closeWasRequested;
    private readonly MultiSectionRenderer multiSectionRenderer = new();

    public TextMenuRenderer(TextMenu textMenu, IDisplay display, RenderingOptions renderingOptions)
        : base(textMenu, display, renderingOptions)
    {
        TextMenuTitleSection textMenuTitleSection = new(Control, RenderingContext);
        multiSectionRenderer.Add(textMenuTitleSection);

        TextMenuItemsSection textMenuItemsSection = new(Control, RenderingContext);
        multiSectionRenderer.Add(textMenuItemsSection);

        TextMenuSelectionSection textMenuSelectionSection = new(Control, RenderingContext);
        multiSectionRenderer.Add(textMenuSelectionSection);
    }

    protected override bool InitializeContentRendering()
    {
        multiSectionRenderer.Reset();
        return multiSectionRenderer.HasMoreLines;
    }

    protected override bool RenderNextContentLine()
    {
        multiSectionRenderer.RenderNextLine();

        if (multiSectionRenderer.HasMoreLines)
            return true;

        Control.SelectedItem?.Execute();
        return false;
    }
}