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

        IEnumerable<int> lineLengths = MeasureLinesForWrapAtWord(text, maxWidth);

        foreach (int lineLength in lineLengths)
        {
            width = Math.Max(width, lineLength);
            height++;
        }

        return new Size(width, height);
    }

    private static IEnumerable<int> MeasureLinesForWrapAtWord(this string text, int maxWidth)
    {
        if (text == null)
            yield break;

        if (text.Length == 0)
        {
            yield return 0;
            yield break;
        }

        int startIndex = 0;
        int currentLineLength = 0;
        bool isInsideWord = false;
        int lastSpaceIndex = -1;

        for (int i = 0; i < text.Length; i++)
        {
            char currentChar = text[i];

            if (char.IsWhiteSpace(currentChar))
            {
                if (isInsideWord)
                    isInsideWord = false;

                lastSpaceIndex = i;
            }
            else
            {
                if (!isInsideWord)
                    isInsideWord = true;
            }

            currentLineLength++;

            // Check if the line has reached its maximum width.
            if (currentLineLength > maxWidth)
            {
                if (lastSpaceIndex > startIndex)
                {
                    // Break at the last space found
                    int lineLength = lastSpaceIndex - startIndex;
                    yield return lineLength;

                    startIndex = lastSpaceIndex + 1;
                }
                else
                {
                    // No space found, break at the maximum width.
                    int lineLength = i - startIndex;
                    yield return lineLength;

                    startIndex = i;
                }

                // Reset tracking variables.
                currentLineLength = 0;
                isInsideWord = false;
                lastSpaceIndex = -1;
            }
        }

        // Process any remaining text.
        if (startIndex < text.Length)
        {
            int lineLength = text.Length - startIndex;
            yield return lineLength;
        }
    }

    public static IEnumerable<string> WrapAtWord(this string text, int maxWidth)
    {
        if (text == null)
            yield break;

        if (text.Length == 0)
        {
            yield return string.Empty;
            yield break;
        }

        int startIndex = 0;
        int currentLineLength = 0;
        bool isInsideWord = false;
        int lastSpaceIndex = -1;

        for (int i = 0; i < text.Length; i++)
        {
            char currentChar = text[i];

            if (char.IsWhiteSpace(currentChar))
            {
                if (isInsideWord)
                    isInsideWord = false;

                lastSpaceIndex = i;
            }
            else
            {
                if (!isInsideWord)
                    isInsideWord = true;
            }

            currentLineLength++;

            // Check if the line has reached its maximum width.
            if (currentLineLength > maxWidth)
            {
                if (lastSpaceIndex > startIndex)
                {
                    // Break at the last space found
                    string line = text.Substring(startIndex, lastSpaceIndex - startIndex);
                    yield return line;

                    startIndex = lastSpaceIndex + 1;
                }
                else
                {
                    // No space found, break at the maximum width.
                    string line = text.Substring(startIndex, i - startIndex);
                    yield return line;

                    startIndex = i;
                }

                // Reset tracking variables.
                currentLineLength = 0;
                isInsideWord = false;
                lastSpaceIndex = -1;
            }
        }

        // Process any remaining text.
        if (startIndex < text.Length)
        {
            string line = text.Substring(startIndex, text.Length - startIndex);
            yield return line;
        }
    }
    public static Size MeasureCutAtChar(this string text, int maxWidth)
    {
        int lineWidth = Math.Min(text.Length, maxWidth);
        int lineHeight = 1;

        return new Size(lineWidth, lineHeight);
    }

    public static IEnumerable<string> CutAtChar(this string text, int maxWidth, bool addEllipsis = false)
    {
        if (maxWidth == 0)
            yield break;

        if (maxWidth < 0)
        {
            yield return text;
            yield break;
        }

        if (text.Length <= maxWidth)
        {
            yield return text;
            yield break;
        }

        if (addEllipsis)
        {
            if (maxWidth == 1)
            {
                yield return ".";
                yield break;
            }

            if (maxWidth == 2)
            {
                yield return "..";
                yield break;
            }

            if (maxWidth == 3)
            {
                yield return "...";
                yield break;
            }

            string chunk = text.Substring(0, maxWidth - 3);
            yield return chunk + "...";
        }
        else
        {
            yield return text.Substring(0, maxWidth);
        }
    }

    public static Size MeasureCutAtWord(this string text, int maxWidth, bool addEllipsis = false)
    {
        if (string.IsNullOrEmpty(text) || maxWidth == 0)
            return Size.Empty;

        if (maxWidth < 0 || text.Length <= maxWidth)
            return new Size(text.Length, 1);

        if (addEllipsis)
        {
            if (maxWidth <= 3)
                return new Size(maxWidth, 1);

            for (int i = maxWidth - 3; i >= 0; i--)
            {
                char currentChar = text[i];

                if (char.IsWhiteSpace(currentChar))
                    return new Size(i + 3, 1);
            }

            return new Size(maxWidth, 1);
        }
        else
        {
            for (int i = maxWidth; i >= 0; i--)
            {
                char currentChar = text[i];

                if (char.IsWhiteSpace(currentChar))
                    return new Size(i, 1);
            }

            return new Size(maxWidth, 1);
        }
    }

    public static IEnumerable<string> CutAtWord(this string text, int maxWidth, bool addEllipsis = false)
    {
        if (maxWidth == 0)
            yield break;

        if (maxWidth < 0)
        {
            yield return text;
            yield break;
        }

        if (text.Length <= maxWidth)
        {
            yield return text;
            yield break;
        }

        if (addEllipsis)
        {
            if (maxWidth == 1)
            {
                yield return ".";
                yield break;
            }

            if (maxWidth == 2)
            {
                yield return "..";
                yield break;
            }

            if (maxWidth == 3)
            {
                yield return "...";
                yield break;
            }

            for (int i = maxWidth - 3; i >= 0; i--)
            {
                char currentChar = text[i];

                if (char.IsWhiteSpace(currentChar))
                {
                    string chunk = text.Substring(0, i);
                    yield return chunk + "...";
                    yield break;
                }
            }

            {
                string chunk = text.Substring(0, maxWidth - 3);
                yield return chunk + "...";
            }
        }
        else
        {
            for (int i = maxWidth; i >= 0; i--)
            {
                char currentChar = text[i];

                if (char.IsWhiteSpace(currentChar))
                {
                    string line = text.Substring(0, i);
                    yield return line;
                    yield break;
                }
            }

            yield return text.Substring(0, maxWidth);
        }
    }
}