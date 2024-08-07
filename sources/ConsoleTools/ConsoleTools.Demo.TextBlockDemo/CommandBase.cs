﻿// ConsoleTools
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
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.TextBlockDemo
{
    internal abstract class CommandBase : ICommand
    {
        public bool IsActive { get; } = true;

        public abstract string Title { get; }

        public void Execute()
        {
            TextBlock textBlock = new TextBlock
            {
                Text = $"- {Title}:",
                ForegroundColor = CustomConsole.EmphasizedColor,
                Margin = "0 2 0 1"
            };
            textBlock.Display();

            DoExecute();
        }

        protected abstract void DoExecute();
    }
}