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

internal class StringCutAtWord
{
    private readonly string text;
    private readonly int maxLength;
    private readonly bool allowEllipsis;
    private bool needEllipsis;

    public int Length { get; private set; }

    public StringCutAtWord(string text, int maxLength, bool allowEllipsis)
    {
        this.text = text;
        this.maxLength = maxLength;
        this.allowEllipsis = allowEllipsis;
    }

    public void Execute()
    {
        if (string.IsNullOrEmpty(text) || maxLength == 0)
        {
            Length = 0;
            return;
        }

        if (maxLength < 0 || text.Length <= maxLength)
        {
            Length = text.Length;
            return;
        }

        if (allowEllipsis)
        {
            needEllipsis = true;

            if (maxLength <= 3)
            {
                Length = maxLength;
                return;
            }

            for (int i = maxLength - 3; i >= 0; i--)
            {
                char currentChar = text[i];

                if (char.IsWhiteSpace(currentChar))
                {
                    Length = i + 3;
                    return;
                }
            }

            Length = maxLength;
            return;
        }

        for (int i = maxLength; i >= 0; i--)
        {
            char currentChar = text[i];

            if (char.IsWhiteSpace(currentChar))
            {
                Length = i;
                return;
            }
        }

        Length = maxLength;
    }

    public string GetLine()
    {
        if (text == null)
            return null;

        if (Length == 0)
            return string.Empty;

        if (Length == text.Length)
            return text;

        if (needEllipsis)
        {
            return Length <= 3
                ? new string('.', Length)
                : text.Substring(0, Length) + "...";
        }

        return text.Substring(0, Length);
    }

    public override string ToString()
    {
        return GetLine();
    }
}