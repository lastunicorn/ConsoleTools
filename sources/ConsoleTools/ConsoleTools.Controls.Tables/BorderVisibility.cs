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

namespace DustInTheWind.ConsoleTools.Controls.Tables;

/// <summary>
/// Represents the visibility of a border.
/// It has four components: left, top, right and bottom.
/// </summary>
public readonly record struct BorderVisibility
{
    /// <summary>
    /// Specifies if the left border is visible.
    /// </summary>
    public bool? Left { get; }

    /// <summary>
    /// Specifies if the top border is visible.
    /// </summary>
    public bool? Top { get; }

    /// <summary>
    /// Specifies if the right border is visible.
    /// </summary>
    public bool? Right { get; }

    /// <summary>
    /// Specifies if the bottom border is visible.
    /// </summary>
    public bool? Bottom { get; }

    /// <summary>
    /// Specifies if the inside borders are visible.
    /// </summary>
    public bool? Inside { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BorderVisibility"/> with
    /// left, top, right and bottom values.
    /// </summary>
    /// <param name="left">Specifies if the left border is visible.</param>
    /// <param name="top">Specifies if the top border is visible.</param>
    /// <param name="right">Specifies if the right border is visible.</param>
    /// <param name="bottom">Specifies if the bottom border is visible.</param>
    /// <param name="inside">Specifies if the inside borders are visible.</param>
    public BorderVisibility(bool? left, bool? top, bool? right, bool? bottom, bool? inside)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
        Inside = inside;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BorderVisibility"/> with two values.
    /// The first one specifies the visibility of the left and right borders.
    /// The second one specifies the visibility of the top and bottom borders.
    /// </summary>
    /// <param name="leftRight">Specifies if the left and right borders are visible.</param>
    /// <param name="topBottom">Specifies if the top and bottom borders are visible.</param>
    /// <param name="inside">Specifies if the inside borders are visible.</param>
    public BorderVisibility(bool? leftRight, bool? topBottom, bool? inside)
    {
        Left = leftRight;
        Top = topBottom;
        Right = leftRight;
        Bottom = topBottom;
        Inside = inside;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BorderVisibility"/> with a single value
    /// that represents the visibility of all four components of the border.
    /// </summary>
    /// <param name="outside">Specifies if all four components of the outside border (left, top, right, bottom) should be visible.</param>
    /// <param name="inside">Specifies if the inside borders are visible.</param>
    public BorderVisibility(bool? outside, bool? inside)
    {
        Left = outside;
        Top = outside;
        Right = outside;
        Bottom = outside;
        Inside = inside;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BorderVisibility"/> with a single value
    /// that represents the visibility of all four components of the border.
    /// </summary>
    /// <param name="value">Specifies if all components of the border (left, top, right, bottom, inside) should be visible.</param>
    public BorderVisibility(bool? value)
    {
        Left = value;
        Top = value;
        Right = value;
        Bottom = value;
        Inside = value;
    }

    /// <summary>
    /// Parse the specified text and returns an instance of <see cref="BorderVisibility"/>.
    /// </summary>
    /// <param name="text">The text to be parsed.</param>
    /// <returns>An instance of <see cref="BorderVisibility"/>.</returns>
    /// <exception cref="Exception">Exception thrown if the text does not represents a border visibility value.</exception>
    public static BorderVisibility Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
            return new BorderVisibility(false);

        string[] parts = text.Split(' ');

        return parts.Length switch
        {
            0 => new BorderVisibility(false),
            1 => ParseOnePart(parts),
            2 => ParseTwoParts(parts),
            3 => ParseThreeParts(parts),
            5 => ParseFiveParts(parts),
            _ => throw new Exception("Invalid border visibility value.")
        };
    }

    private static BorderVisibility ParseOnePart(string[] parts)
    {
        bool? value = ParsePart(parts[0]);
        return new BorderVisibility(value);
    }

    private static BorderVisibility ParseTwoParts(string[] parts)
    {
        bool? outside = ParsePart(parts[0]);
        bool? inside = ParsePart(parts[1]);

        return new BorderVisibility(outside, inside);
    }

    private static BorderVisibility ParseThreeParts(string[] parts)
    {
        bool? leftRight = ParsePart(parts[0]);
        bool? topBottom = ParsePart(parts[1]);
        bool? inside = ParsePart(parts[2]);

        return new BorderVisibility(leftRight, topBottom, inside);
    }

    private static BorderVisibility ParseFiveParts(string[] parts)
    {
        bool? left = ParsePart(parts[0]);
        bool? top = ParsePart(parts[1]);
        bool? right = ParsePart(parts[2]);
        bool? bottom = ParsePart(parts[3]);
        bool? inside = ParsePart(parts[4]);

        return new BorderVisibility(left, top, right, bottom, inside);
    }

    private static bool? ParsePart(string part)
    {
        switch (part)
        {
            case "+":
                return true;

            case "-":
                return false;

            case ".":
                return null;

            default:
                bool success = bool.TryParse(part, out bool value);

                if (!success)
                    throw new Exception("Invalid border visibility value.");

                return value;
        }
    }

    /// <summary>
    /// Returns a string representation of the current instance.
    /// </summary>
    /// <returns>A string representation of the current instance.</returns>
    public override string ToString()
    {
        if (Left == Top == Right == Bottom == Inside)
            return ToString(Left);

        if (Left == Top == Right == Bottom)
        {
            string outside = ToString(Left);
            string inside = ToString(Inside);

            return $"{outside} {inside}";
        }

        if (Left == Right && Top == Bottom)
        {
            string leftRight = ToString(Left);
            string topBottom = ToString(Top);
            string inside = ToString(Inside);

            return $"{leftRight} {topBottom} {inside}";
        }

        {
            string left = ToString(Left);
            string top = ToString(Top);
            string right = ToString(Right);
            string bottom = ToString(Bottom);
            string inside = ToString(Inside);

            return $"{left} {top} {right} {bottom} {inside}";
        }
    }

    private static string ToString(bool? value)
    {
        return value switch
        {
            true => "+",
            false => "-",
            null => "."
        };
    }

    /// <summary>
    /// Parses a string and converts it into a <see cref="BorderVisibility"/> instance.
    /// </summary>
    public static implicit operator BorderVisibility(string text)
    {
        return Parse(text);
    }

    /// <summary>
    /// Serializes a <see cref="BorderVisibility"/> instance int a string.
    /// </summary>
    public static implicit operator string(BorderVisibility borderVisibility)
    {
        return borderVisibility.ToString();
    }

    /// <summary>
    /// Converts the <see cref="Boolean"/> into a <see cref="BorderVisibility"/> having all values <c>true</c>.
    /// </summary>
    public static implicit operator BorderVisibility(bool value)
    {
        return new BorderVisibility(value);
    }
}