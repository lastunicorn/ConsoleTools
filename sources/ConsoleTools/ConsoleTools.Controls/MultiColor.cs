using System;

namespace DustInTheWind.ConsoleTools.Controls
{
    public class MultiColor : Control
    {
        protected override void DoDisplay()
        {
            CustomConsole.WriteLine("Player", ConsoleColor.Black);
            CustomConsole.WriteLine("Player", ConsoleColor.White);
            CustomConsole.WriteLine("Player", ConsoleColor.Gray);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkGray);

            CustomConsole.WriteLine("Player", ConsoleColor.Blue);
            CustomConsole.WriteLine("Player", ConsoleColor.Green);
            CustomConsole.WriteLine("Player", ConsoleColor.Cyan);
            CustomConsole.WriteLine("Player", ConsoleColor.Red);
            CustomConsole.WriteLine("Player", ConsoleColor.Magenta);
            CustomConsole.WriteLine("Player", ConsoleColor.Yellow);
            
            CustomConsole.WriteLine("Player", ConsoleColor.DarkBlue);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkGreen);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkCyan);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkRed);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkMagenta);
            CustomConsole.WriteLine("Player", ConsoleColor.DarkYellow);
        }
    }
}
