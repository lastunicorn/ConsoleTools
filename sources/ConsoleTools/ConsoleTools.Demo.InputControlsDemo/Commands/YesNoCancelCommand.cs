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

using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Controls.Menus;

namespace DustInTheWind.ConsoleTools.Demo.InputControlsDemo.Commands
{
    internal class YesNoCancelCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            CustomConsole.WriteLine("This is a yes/no/cancel control with default value 'Yes'.");
            CustomConsole.WriteLine();

            YesNoAnswer answer = AskQuestion();
            DisplayAnswer(answer);
        }

        private static YesNoAnswer AskQuestion()
        {
            YesNoQuestion yesNoQuestion = new YesNoQuestion("Do you want to continue?")
            {
                AcceptCancel = true,
                DefaultAnswer = YesNoAnswer.Yes
            };

            return yesNoQuestion.ReadAnswer();
        }

        private static void DisplayAnswer(YesNoAnswer answer)
        {
            CustomConsole.WriteLine();
            CustomConsole.Write("Your answer: ");
            CustomConsole.WriteLineEmphasized(answer);
            CustomConsole.WriteLine();
        }
    }
}