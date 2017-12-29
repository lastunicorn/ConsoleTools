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
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.Mvc;

namespace DustInTheWind.ConsoleTools.Demo.Prompter.Controllers
{
    internal class ExitController : IController
    {
        private readonly CommandProviders.Prompter prompter;

        public ExitController(CommandProviders.Prompter prompter)
        {
            if (prompter == null) throw new ArgumentNullException(nameof(prompter));
            this.prompter = prompter;
        }

        public void Execute()
        {
            AskToExit();
        }

        private void AskToExit()
        {
            YesNoControl yesNoControl = new YesNoControl("Are you sure?")
            {
                DefaultOption = YesNoAnswer.Yes
            };

            YesNoAnswer answer = yesNoControl.ReadAnswer();

            if (answer == YesNoAnswer.Yes)
            {
                CustomConsole.WriteLine();
                CustomConsole.WriteLine("Bye!");
                prompter.RequestStop();
            }
            else
            {
                CustomConsole.WriteLine();
            }
        }
    }
}