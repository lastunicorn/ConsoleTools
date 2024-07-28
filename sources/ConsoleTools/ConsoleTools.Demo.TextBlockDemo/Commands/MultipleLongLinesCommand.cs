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

using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools.Demo.TextBlockDemo.Commands
{
    internal class MultipleLongLinesCommand : CommandBase
    {
        public override string Title => "Multiple long lines (text wrapping)";

        protected override void DoExecute()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = new[]
                {
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam orci purus, luctus in est a, tempor luctus tortor. In tortor metus, lacinia vel sapien suscipit, commodo scelerisque metus.",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam orci purus, luctus in est a, tempor luctus tortor. In tortor metus, lacinia vel sapien suscipit, commodo scelerisque metus.",
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam orci purus, luctus in est a, tempor luctus tortor. In tortor metus, lacinia vel sapien suscipit, commodo scelerisque metus."
                }
            };
            textBlock.Display();
        }
    }
}