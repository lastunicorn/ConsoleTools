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

using System;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

internal class TextMenuSelectionSection : SectionRenderer
{
    private readonly TextMenu textMenu;
    private bool hasMoreLines;

    public override bool HasMoreLines => hasMoreLines;

    public TextMenuSelectionSection(TextMenu textMenu, RenderingContext renderingContext)
        : base(renderingContext)
    {
        this.textMenu = textMenu ?? throw new ArgumentNullException(nameof(textMenu));

        hasMoreLines = true;
    }

    public override void RenderNextLine()
    {
        ReadUserSelection();
        hasMoreLines = false;
    }

    private void ReadUserSelection()
    {
        RenderingContext.WriteLine();

        //while (!closeWasRequested)
        while (true)
        {
            DisplayQuestion();

            string inputValue = Console.ReadLine();

            if (inputValue == null)
            {
                //OnClosed();
                return;
            }

            if (inputValue.Length == 0)
                continue;

            TextMenuItem selectedMenuItem = textMenu.MenuItems
                .FirstOrDefault(x => x.Id == inputValue);

            if (selectedMenuItem == null || !selectedMenuItem.IsVisible)
            {
                DisplayInvalidOptionWarning();
                continue;
            }

            if (!selectedMenuItem.CanBeSelected())
            {
                DisplayDisabledItemWarning();
                continue;
            }

            textMenu.SelectedItem = selectedMenuItem;

            return;
        }
    }

    private void DisplayQuestion()
    {
        if (textMenu.QuestionText == null)
            return;

        textMenu.QuestionText.Display();

        //int textLength = QuestionText.CalculateOuterLength();

        //int questionHeight = (int)Math.Ceiling((double)textLength / Console.BufferWidth);
        //InnerSize = InnerSize.InflateHeight(questionHeight);
    }

    private void DisplayInvalidOptionWarning()
    {
        RenderingContext.WriteLine(textMenu.InvalidOptionText, CustomConsole.WarningColor, CustomConsole.WarningBackgroundColor);
        RenderingContext.WriteLine();

        //CustomConsole.WriteLineWarning(InvalidOptionText);
        //Console.WriteLine();

        //InnerSize = InnerSize.InflateHeight(2);
    }

    private void DisplayDisabledItemWarning()
    {
        RenderingContext.WriteLine(textMenu.OptionDisabledText, CustomConsole.WarningColor, CustomConsole.WarningBackgroundColor);
        RenderingContext.WriteLine();

        //CustomConsole.WriteLineWarning(OptionDisabledText);
        //Console.WriteLine();

        //InnerSize = InnerSize.InflateHeight(2);
    }

    public override void Reset()
    {
        hasMoreLines = true;
    }
}