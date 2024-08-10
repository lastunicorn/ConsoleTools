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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls;

internal static class StringSplitExtensions
{
    public static Size MeasureWrapAtChar(this string text, int maxWidth)
    {
        int lineWidth = Math.Min(text.Length, maxWidth);
        int lineHeight = (int)Math.Ceiling((double)text.Length / maxWidth);

        return new Size(lineWidth, lineHeight);
    }

    public static IEnumerable<string> WrapAtChar(this string text, int maxWidth)
    {
        if (text == null)
            yield break;

        if (text.Length == 0)
        {
            yield return string.Empty;
            yield break;
        }

        int startIndex = 0;

        while (startIndex < text.Length)
        {
            int chunkLength = Math.Min(maxWidth, text.Length - startIndex);
            string chunk = text.Substring(startIndex, chunkLength);
            yield return chunk;

            startIndex += chunkLength;
        }
    }

    public static Size MeasureWrapAtWord(this string text, int maxWidth)
    {
        if (text == null)
            return Size.Empty;

        int width = 0;
        int height = 0;

        StringSplitAtWord stringSplitAtWord = new(text, maxWidth);

        while (stringSplitAtWord.MoveNext())
        {
            width = Math.Max(width, stringSplitAtWord.Length);
            height++;
        }

        return new Size(width, height);
    }

    public static IEnumerable<string> WrapAtWord(this string text, int maxWidth)
    {
        StringSplitAtWord stringSplitAtWord = new(text, maxWidth);

        while (stringSplitAtWord.MoveNext())
            yield return stringSplitAtWord.GetLine();
    }

    public static Size MeasureCutAtChar(this string text, int maxWidth)
    {
        int lineWidth = Math.Min(text.Length, maxWidth);
        int lineHeight = 1;

        return new Size(lineWidth, lineHeight);
    }

    public static string CutAtChar(this string text, int maxWidth, bool addEllipsis = false)
    {
        if (maxWidth == 0)
            return string.Empty;

        if (maxWidth < 0)
            return text;

        if (text.Length <= maxWidth)
            return text;

        if (addEllipsis)
        {
            if (maxWidth == 1)
                return ".";

            if (maxWidth == 2)
                return "..";

            if (maxWidth == 3)
                return "...";

            string chunk = text.Substring(0, maxWidth - 3);
            return chunk + "...";
        }
        
        return text.Substring(0, maxWidth);
    }

    public static Size MeasureCutAtWord(this string text, int maxWidth, bool addEllipsis = false)
    {
        StringCutAtWord stringCutAtWord = new(text, maxWidth, addEllipsis);
        stringCutAtWord.Execute();

        int width = stringCutAtWord.Length;
        return new Size(width, 1);
    }

    public static string CutAtWord(this string text, int maxWidth, bool addEllipsis = false)
    {
        StringCutAtWord stringCutAtWord = new(text, maxWidth, addEllipsis);
        stringCutAtWord.Execute();
        return stringCutAtWord.ToString();
    }
}