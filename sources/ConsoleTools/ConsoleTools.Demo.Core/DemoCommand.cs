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
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.Core
{
    internal class DemoCommand : ICommand
    {
        private readonly IDemo demo;

        public bool IsActive => true;

        public DemoCommand(IDemo demo)
        {
            this.demo = demo ?? throw new ArgumentNullException(nameof(demo));
        }

        public void Execute()
        {
            try
            {
                demo.Execute();
            }
            finally
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = $"Demo {demo.Name} has finished.",
                    Margin = "0 1",
                    ForegroundColor = ConsoleColor.Cyan
                };
                textBlock.Display();
            }
        }
    }
}