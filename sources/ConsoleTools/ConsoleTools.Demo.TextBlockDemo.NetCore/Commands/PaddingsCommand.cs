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

using System;
using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools.Demo.TextBlockDemo.NetCore.Commands
{
    internal class PaddingsCommand : CommandBase
    {
        public override string Title => "Custom Paddings (3 3 3 3)";

        protected override void DoExecute()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam orci purus, luctus in est a, tempor luctus tortor. In tortor metus, lacinia vel sapien suscipit, commodo scelerisque metus.",
                Padding = 3,
                BackgroundColor = ConsoleColor.DarkGray // Added to easyer see the paddings.
            };
            textBlock.Display();
        }
    }
}