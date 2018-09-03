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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.CommandProviders;
using DustInTheWind.ConsoleTools.Demo.Prompter.Controllers;
using DustInTheWind.ConsoleTools.Menues;

namespace DustInTheWind.ConsoleTools.Demo.Prompter
{
    internal class DemoPrompter : Menues.Prompter
    {
        public DemoPrompter()
        {
            IEnumerable<PrompterItem> items = CreatePrompterItems();
            AddItems(items);
        }

        private IEnumerable<PrompterItem> CreatePrompterItems()
        {
            return new[]
            {
                new PrompterItem
                {
                    Name = "q",
                    Command = new ExitCommand()
                },
                new PrompterItem
                {
                    Name = "quit",
                    Command = new ExitCommand()
                },
                new PrompterItem
                {
                    Name = "exit",
                    Command = new ExitCommand()
                },
                new PrompterItem
                {
                    Name = "help",
                    Command = new HelpCommand()
                },
                new PrompterItem
                {
                    Name = "whale",
                    Command = new WhaleCommand()
                },
                new PrompterItem
                {
                    Name = "whales",
                    Command = new WhaleCommand()
                },
                new PrompterItem
                {
                    Name = "prompter",
                    Command = new PrompterCommand(this)
                }
            };
        }
    }
}