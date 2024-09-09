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
using DustInTheWind.ConsoleTools.Controls.Spinners.Templates;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.SpinnerDemo.Demo;

internal class BoomerangDemo : DemoBase
{
    public override string Title => "Boomerang";

    protected override void DoExecute()
    {
        DummyWorker worker = new()
        {
            SpinnerTemplate = new BoomerangSpinnerTemplate(),
            SpinnerStepMilliseconds = 400,
            WorkTimeSpan = TimeSpan.FromSeconds(5)
        };

        worker.Run();
    }
}