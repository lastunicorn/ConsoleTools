//// ConsoleTools
//// Copyright (C) 2017-2024 Dust in the Wind
//// 
//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
//// 
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace DustInTheWind.ConsoleTools.Controls.Menus;

//public class TextMenuRenderer : BlockControlRenderer<TextMenu>
//{
//    private bool closeWasRequested;

//    public TextMenuRenderer(TextMenu textMenu, IDisplay display, RenderingOptions renderingOptions)
//        : base(textMenu, display, renderingOptions)
//    {
//    }

//    protected override bool DoInitializeContentRendering()
//    {
//        throw new NotImplementedException();
//    }

//    protected override bool DoRenderNextContentLine()
//    {
//        if (Control.TitleText != null)
//            DrawTitle();

//        DrawMenu();
//        ReadUserSelection();
//    }

//    private void DrawTitle()
//    {
//        Display.WriteLine(Control.TitleText);
//        Display.WriteLine();
//        Display.WriteLine();

//        //InnerSize = InnerSize.InflateHeight(2);
//    }

//    private void DrawMenu(IDisplay display)
//    {
//        IEnumerable<TextMenuItem> menuItemsToDisplay = Control.MenuItems
//            .Where(x => x.IsVisible);

//        foreach (TextMenuItem menuItem in menuItemsToDisplay)
//        {
//            display.StartLine();
//            menuItem.Display();
//            display.EndLine();

//            //InnerSize = InnerSize.InflateHeight(menuItem.Size.Height);
//        }
//    }

//    private void ReadUserSelection(IDisplay display)
//    {
//        display.WriteLine();
//        //Console.WriteLine();
//        //InnerSize = InnerSize.InflateHeight(1);

//        while (!closeWasRequested)
//        {
//            DisplayQuestion();

//            string inputValue = Console.ReadLine();

//            if (inputValue == null)
//            {
//                OnClosed();
//                return;
//            }

//            if (inputValue.Length == 0)
//                continue;

//            TextMenuItem selectedMenuItem = Control.MenuItems
//                .FirstOrDefault(x => x.Id == inputValue);

//            if (selectedMenuItem == null || !selectedMenuItem.IsVisible)
//            {
//                DisplayInvalidOptionWarning(display);
//                continue;
//            }

//            if (!selectedMenuItem.CanBeSelected())
//            {
//                DisplayDisabledItemWarning(display);
//                continue;
//            }

//            Control.SelectedItem = selectedMenuItem;

//            return;
//        }
//    }

//    private void DisplayQuestion()
//    {
//        if (Control.QuestionText == null)
//            return;

//        Control.QuestionText.Display();

//        //int textLength = QuestionText.CalculateOuterLength();

//        //int questionHeight = (int)Math.Ceiling((double)textLength / Console.BufferWidth);
//        //InnerSize = InnerSize.InflateHeight(questionHeight);
//    }

//    private void DisplayInvalidOptionWarning(IDisplay display)
//    {
//        display.WriteLine(Control.InvalidOptionText, CustomConsole.WarningColor, CustomConsole.WarningBackgroundColor);
//        display.WriteLine();

//        //CustomConsole.WriteLineWarning(InvalidOptionText);
//        //Console.WriteLine();

//        //InnerSize = InnerSize.InflateHeight(2);
//    }

//    private void DisplayDisabledItemWarning(IDisplay display)
//    {
//        display.WriteLine(Control.OptionDisabledText, CustomConsole.WarningColor, CustomConsole.WarningBackgroundColor);
//        display.WriteLine();

//        //CustomConsole.WriteLineWarning(OptionDisabledText);
//        //Console.WriteLine();

//        //InnerSize = InnerSize.InflateHeight(2);
//    }
//}