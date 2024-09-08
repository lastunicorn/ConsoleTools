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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools.Controls;

namespace DustInTheWind.ConsoleTools.Demo.Core;

public abstract class DemoPackageBase : IDemo
{
    public abstract string Title { get; }

    public virtual MultilineText Description => null;

    protected List<IDemo> Demos { get; } = new();

    public bool HasSubPackages => Demos.Any(x => x is DemoPackageBase);

    public void Execute()
    {
        if (HasSubPackages)
        {
            ControlRepeater controlRepeater = new()
            {
                Content = new MainMenu(Demos)
                {
                    IsMain = false
                },
                RepeatCount = -1,
                Margin = (0, 2, 0, 0)
            };

            controlRepeater.Display();
        }
        else
        {
            IEnumerable<IDemo> notNullDemos = Demos.Where(x => x != null);

            foreach (IDemo demo in notNullDemos)
                demo.Execute();
        }
    }
}