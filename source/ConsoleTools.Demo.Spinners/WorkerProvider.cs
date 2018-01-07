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
    internal class WorkerProvider
    {
        private readonly TextMenu menu;
        private Worker worker;

        public WorkerProvider()
        {
            menu = CreateMenu();
        }

        private TextMenu CreateMenu()
        {
            TextMenuItem[] menuItems =
            {
                new TextMenuItem
                {
                    Id = "1",
                    Text = "stick",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new StickTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "2",
                    Text = "bubble",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new BubbleTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "3",
                    Text = "boomerang",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new ArrowTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "4",
                    Text = "half-block spin",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new HalfBlockRotateTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "5",
                    Text = "half-block vertical",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new HalfBlockBlinkTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "6",
                    Text = "fan",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new FanTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "11",
                    Text = "fill (dot, empty from start) - default",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new FillTemplate(),
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "12",
                    Text = "fill (dot, empty from end)",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new FillTemplate {FilledBehavior = FilledBehavior.EmptyFromEnd},
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "13",
                    Text = "fill (dot, sudden empty)",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new FillTemplate {FilledBehavior = FilledBehavior.SuddenEmpty},
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "14",
                    Text = "fill (dot, with borders)",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new FillTemplate {ShowBorders = true},
                            SpinnerStepMilliseconds = 400,
                            WorkInterval = TimeSpan.FromSeconds(5)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "15",
                    Text = "fill (block, length: 10 chars, step: 100ms)",
                    Command = new ActionCommand(() =>
                    {
                        worker = new Worker
                        {
                            SpinnerTemplate = new FillTemplate('▓', 10),
                            SpinnerStepMilliseconds = 100,
                            WorkInterval = TimeSpan.FromSeconds(10)
                        };
                    })
                },
                new TextMenuItem
                {
                    Id = "0",
                    Text = "Exit"
                }
            };

            return new TextMenu(menuItems);
        }

        public IEnumerable<Worker> CreateWorkers()
        {
            while (true)
            {
                worker = null;
                menu.Display();

                if (worker == null)
                    yield break;

                yield return worker;
            }
        }
    }
}