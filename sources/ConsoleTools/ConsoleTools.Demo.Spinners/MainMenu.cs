// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.Spinners.Templates;

namespace DustInTheWind.ConsoleTools.Demo.Spinners
{
    internal class MainMenu : TextMenu
    {
        public MainMenu()
        {
            IEnumerable<TextMenuItem> menuItems = CreateMenuItems();
            AddItems(menuItems);
        }

        private IEnumerable<TextMenuItem> CreateMenuItems()
        {
            yield return new TextMenuItem
            {
                Id = "1",
                Text = "stick",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new StickSpinnerTemplate(),
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "2",
                Text = "bubble",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new BubbleSpinnerTemplate(),
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "3",
                Text = "boomerang",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new BoomerangSpinnerTemplate(),
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "4",
                Text = "half-block spin",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new HalfRotateSpinnerTemplate(),
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "5",
                Text = "half-block vertical",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new HalfBlinkSpinnerTemplate(),
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "6",
                Text = "fan",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new FanSpinnerTemplate(),
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "11",
                Text = "fill (dot, empty from start) - default",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new FillSpinnerTemplate(),
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "12",
                Text = "fill (dot, empty from end)",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new FillSpinnerTemplate { FilledBehavior = FilledBehavior.EmptyFromEnd },
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "13",
                Text = "fill (dot, sudden empty)",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new FillSpinnerTemplate { FilledBehavior = FilledBehavior.SuddenEmpty },
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "14",
                Text = "fill (dot, with borders)",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new FillSpinnerTemplate { ShowBorders = true },
                        SpinnerStepMilliseconds = 400,
                        WorkPeriod = TimeSpan.FromSeconds(5)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "15",
                Text = "fill (block, length: 10 chars, step: 100ms)",
                Command = new ActionCommand(() =>
                {
                    Worker worker = new Worker
                    {
                        SpinnerTemplate = new FillSpinnerTemplate('▓', 10),
                        SpinnerStepMilliseconds = 100,
                        WorkPeriod = TimeSpan.FromSeconds(10)
                    };

                    worker.Run();
                })
            };

            yield return new TextMenuItem
            {
                Id = "0",
                Text = "Exit",
                Command = new ActionCommand(Program.RequestStop)
            };
        }
    }
}