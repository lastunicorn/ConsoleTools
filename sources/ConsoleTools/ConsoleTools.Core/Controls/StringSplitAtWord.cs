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

namespace DustInTheWind.ConsoleTools.Controls;

internal class StringSplitAtWord
{
    private readonly string text;
    private readonly int maxLength;

    private int index = -1;
    private bool isInsideWord;
    private int lastSplitIndex;
    private int lastSplitLength;

    public int StartIndex { get; private set; } = -1;

    public int Length { get; private set; }

    public StringSplitAtWord(string text, int maxLength)
    {
        this.text = text;
        this.maxLength = maxLength;
    }

    public void Reset()
    {
        StartIndex = -1;
        Length = 0;

        index = -1;
        isInsideWord = false;
        lastSplitIndex = 0;
        lastSplitLength = 0;
    }

    public bool MoveNext()
    {
        if (text == null)
            return false;

        if (index + 1 > text.Length)
            return false;

        StartIndex = lastSplitIndex + lastSplitLength;
        Length = index - StartIndex;

        while (index + 1 < text.Length)
        {
            index++;
            Length = index - StartIndex;

            // Process next character

            char currentChar = text[index];

            if (char.IsWhiteSpace(currentChar))
            {
                if (isInsideWord)
                {
                    isInsideWord = false;
                    lastSplitIndex = index;
                    lastSplitLength = 1;
                }
                else
                {
                    lastSplitLength++;
                }
            }
            else
            {
                if (!isInsideWord)
                    isInsideWord = true;
            }

            // Check if the line has reached its maximum length.
            if (Length == maxLength)
            {
                if (lastSplitIndex <= StartIndex)
                {
                    // No split found, break at the current index.

                    lastSplitIndex = index;
                    lastSplitLength = 0;
                }

                Length = lastSplitIndex - StartIndex;
                return true;
            }
        }

        if (index + 1 == text.Length)
        {
            index++;

            // End of text reached. Publish remaining text.

            lastSplitIndex = text.Length;
            lastSplitLength = 0;

            Length = lastSplitIndex - StartIndex;
            return true;
        }

        return false;
    }

    public string GetLine()
    {
        return text.Substring(StartIndex, Length);
    }
}