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

using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.InputControls;
using DustInTheWind.ConsoleTools.Demo.Utils;

namespace DustInTheWind.ConsoleTools.Demo.YesNoDemo.Demo;

internal class SimpleDemo : DemoBase
{
    public override string Title => "Yes/No";

    public override MultilineText Description => "This is a simple yes/no control with no default value.";

    protected override void DoExecute()
    {
        YesNoAnswer answer = AskQuestion();
        DisplayAnswer(answer);
    }

    private static YesNoAnswer AskQuestion()
    {
        YesNoQuestion yesNoQuestion = new("Do you want to continue?");
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