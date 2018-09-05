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
using DustInTheWind.ConsoleTools.CommandLine;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.Menues;

namespace DustInTheWind.ConsoleTools.Demo.PrompterDemo.Commands
{
    internal class PrompterCommand : IPrompterCommand
    {
        private readonly Menues.Prompter prompter;

        public bool IsActive { get; } = true;

        public PrompterCommand(Menues.Prompter prompter)
        {
            this.prompter = prompter ?? throw new ArgumentNullException(nameof(prompter));
        }

        public void Execute(CliCommand cliCommand)
        {
            if (cliCommand.Parameters.Count > 0)
                prompter.PrompterText = cliCommand.Parameters[0].Value;
            else
                ChangePrompter();
       }

        private void ChangePrompter()
        {
            ValueView<string> valueView = new ValueView<string>("New Prompter Text:");
            prompter.PrompterText = valueView.Read();
        }
    }
}