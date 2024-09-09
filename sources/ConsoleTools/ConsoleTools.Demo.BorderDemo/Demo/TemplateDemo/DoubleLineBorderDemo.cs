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
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.BorderDemo.Demo.TemplateDemo;

internal class DoubleLineBorderDemo : DemoBase
{
    public override string Title => "Double-line Border";

    protected override void DoExecute()
    {
        Border border = new()
        {
            Content = new TextBlock
            {
                Text = new[]
                {
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    "Sed sollicitudin non enim sit amet interdum.",
                    "Nullam quis nisl a dolor convallis rhoncus at sit amet eros."
                }
            },
            Template = BorderTemplate.DoubleLineBorderTemplate
        };

        border.Display();
    }
}