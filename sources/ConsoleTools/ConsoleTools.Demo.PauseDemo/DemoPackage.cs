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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Menus;
using DustInTheWind.ConsoleTools.Demo.Core;
using DustInTheWind.ConsoleTools.Demo.PauseDemo.Commands;

namespace DustInTheWind.ConsoleTools.Demo.PauseDemo
{
    internal class DemoPackage : IDemoPackage
    {
        public string ShortDescription => "Pause Demo";


        public void ExecuteDemo()
        {
            DisplayApplicationHeader();
            RunDemos();
        }

        private static void DisplayApplicationHeader()
        {
            ApplicationHeader applicationHeader = new ApplicationHeader
            {
                Appendix = "Pause Demo"
            };
            applicationHeader.Display();
        }

        private static void RunDemos()
        {
            bool exitWasRequested = false;

            TextMenu textMenu = new TextMenu();
            textMenu.AddItems(new[]
            {
                new TextMenuItem
                {
                    Id = "1",
                    Text = "Default",
                    Command = new DefaultCommand()
                },
                new TextMenuItem
                {
                    Id = "2",
                    Text = "Custom Unlock Key",
                    Command = new CustomUnlockKeyCommand()
                },
                new TextMenuItem
                {
                    Id = "3",
                    Text = "Erasable Pause",
                    Command = new ErasablePauseCommand()
                },
                new TextMenuItem
                {
                    Id = "4",
                    Text = "Custom Margins",
                    Command = new CustomMarginsCommand()
                },
                new TextMenuItem
                {
                    Id = "5",
                    Text = "Custom Paddings",
                    Command = new CustomPaddingsCommand()
                },
                new TextMenuItem
                {
                    Id = "6",
                    Text = "Foreground Color",
                    Command = new ForegroundColorCommand()
                },
                new TextMenuItem
                {
                    Id = "7",
                    Text = "Background Color",
                    Command = new BackgroundColorCommand()
                },
                new TextMenuItem
                {
                    Id = "0",
                    Text = "Exit",
                    Command = new ActionCommand(() =>
                    {
                        exitWasRequested = true;
                    })
                }
            });

            while (!exitWasRequested)
            {
                textMenu.Display();
            }
        }
    }
}