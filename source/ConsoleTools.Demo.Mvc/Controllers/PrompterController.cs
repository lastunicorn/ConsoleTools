// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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
using DustInTheWind.ConsoleTools.CommandProviders;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.Mvc;

namespace DustInTheWind.ConsoleTools.Demo.Mvc.Controllers
{
    internal class PrompterController : IController
    {
        private readonly Prompter prompter;

        public PrompterController(ICommandProvider prompter)
        {
            if (prompter == null) throw new ArgumentNullException(nameof(prompter));
            this.prompter = prompter as Prompter;
        }

        public void Execute()
        {
            ChangePrompter();
        }

        private void ChangePrompter()
        {
            TextInputControl textInputControl = new TextInputControl();
            prompter.PrompterText = textInputControl.Read("New Prompter Text");
        }
    }
}