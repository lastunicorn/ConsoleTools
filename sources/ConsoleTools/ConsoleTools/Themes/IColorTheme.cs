using System;

namespace DustInTheWind.ConsoleTools.Themes
{
    public interface IColorTheme
    {
        ///// <summary>
        ///// Gets or sets the color used to write Normal text.
        ///// If the color is null, default color is used.
        ///// </summary>
        //ConsoleColor? NormalColor { get; }

        ///// <summary>
        ///// Gets the background color used to write Emphasis messages.
        ///// If the color is null, default background color is used.
        ///// </summary>
        //ConsoleColor? NormalBackgroundColor { get; }

        ///// <summary>
        ///// Gets the color used to write Emphasized messages.
        ///// If the color is null, default color is used.
        ///// </summary>
        //ConsoleColor? EmphasizedColor { get; }

        ///// <summary>
        ///// Gets the background color used to write Emphasis messages.
        ///// If the color is null, default background color is used.
        ///// </summary>
        //ConsoleColor? EmphasizedBackgroundColor { get; }

        ///// <summary>
        ///// Gets the color used to write Success messages.
        ///// If the color is null, default color is used.
        ///// </summary>
        //ConsoleColor? SuccessColor { get; }

        ///// <summary>
        ///// Gets the background color used to write Success messages.
        ///// If the color is null, default background color is used.
        ///// </summary>
        //ConsoleColor? SuccessBackgroundColor { get; }

        ///// <summary>
        ///// Gets the color used to write Warning messages.
        ///// If the color is null, default color is used.
        ///// </summary>
        //ConsoleColor? WarningColor { get; }

        ///// <summary>
        ///// Gets the background color used to write Warning messages.
        ///// If the color is null, default background color is used.
        ///// </summary>
        //ConsoleColor? WarningBackgroundColor { get; }

        ///// <summary>
        ///// Gets the color used to write Error messages.
        ///// If the color is null, default color is used.
        ///// </summary>
        //ConsoleColor? ErrorColor { get; }

        ///// <summary>
        ///// Gets the background color used to write Error messages.
        ///// If the color is null, default background color is used.
        ///// </summary>
        //ConsoleColor? ErrorBackgroundColor { get; }

        TextType this[string id] { get; }
    }
}