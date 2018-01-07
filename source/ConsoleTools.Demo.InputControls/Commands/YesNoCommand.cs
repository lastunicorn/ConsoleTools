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

using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.InputControls.Commands
{
    internal class YesNoCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            CustomConsole.WriteLine();

            DisplayYesNo();
            CustomConsole.WriteLine();

            DisplayYesNoCancel();
            CustomConsole.WriteLine();
        }

        private static void DisplayYesNo()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This is a simple yes/no control with no default value.");
            CustomConsole.WriteLine();

            YesNoQuestion yesNoQuestion = new YesNoQuestion("Do you want to continue?");
            YesNoAnswer answer = yesNoQuestion.ReadAnswer();

            CustomConsole.WriteLine();
            CustomConsole.Write("Your answer: ");
            CustomConsole.WriteLineEmphasies(answer);
        }

        private static void DisplayYesNoCancel()
        {
            CustomConsole.WriteLine();
            CustomConsole.WriteLine("This is a yes/no/cancel control with default value 'Yes'.");
            CustomConsole.WriteLine();

            YesNoQuestion yesNoQuestion = new YesNoQuestion("Do you want to continue?")
            {
                AcceptCancel = true,
                DefaultAnswer = YesNoAnswer.Yes
            };
            YesNoAnswer answer = yesNoQuestion.ReadAnswer();

            CustomConsole.WriteLine();
            CustomConsole.Write("Your answer: ");
            CustomConsole.WriteLineEmphasies(answer);
        }
    }
}