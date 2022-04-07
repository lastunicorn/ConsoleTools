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

namespace DustInTheWind.ConsoleTools.Controls
{
    public class Border : BlockControl
    {
        public BlockControl Control { get; set; }

        public override IControlRenderer GetRenderer(IDisplay display)
        {
            return new BorderRenderer(this, display);
        }

        private class BorderRenderer : ControlRenderer
        {
            private readonly Border border;
            private IControlRenderer controlRenderer;

            protected override bool HasMoreContentRows => controlRenderer.HasMoreRows;

            public BorderRenderer(Border border, IDisplay display)
                : base(display)
            {
                this.border = border ?? throw new ArgumentNullException(nameof(border));
            }

            protected override void Initialize()
            {
                IDisplay childDisplay = border.Control.CreateDisplay(Display);
                controlRenderer = border.Control.GetRenderer(childDisplay);

                base.Initialize();
            }

            protected override void RenderNextContentRow()
            {
                Display.StartRow();
                controlRenderer.RenderNextRow();
                Display.EndRow();
            }
        }
    }
}