// ConsoleTools
// Copyright (C) 2017 Dust in the Wind
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
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools.TabularData
{
    /// <summary>
    /// Represents a text on multiple lines.
    /// </summary>
    public class MultilineText
    {
        /// <summary>
        /// Gets the text as a single line.
        /// </summary>
        public string RawText { get; }

        /// <summary>
        /// Gets the text splitted in lines.
        /// </summary>
        public IReadOnlyList<string> Lines { get; }

        /// <summary>
        /// Gets the size of the smallest rectangle in which the text will fit.
        /// </summary>
        public Size Size { get; }

        public static MultilineText Empty { get; } = new MultilineText(string.Empty);

        public bool IsEmpty => Size.IsEmpty;

        /// <summary>
        /// Used only internaly when splitting the string in multiple lines.
        /// </summary>
        private enum LineEndChar
        {
            None,
            Cr,
            Lf
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineText"/> class with
        /// the raw text.
        /// </summary>
        /// <param name="text">The text as a single line.</param>
        /// <exception cref="ApplicationException"></exception>
        public MultilineText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                RawText = string.Empty;
                Lines = new ReadOnlyCollection<string>(new string[0]);
                Size = new Size(0, 0);
            }
            else
            {
                try
                {
                    int width = 0;
                    int startLineIndex = 0;
                    LineEndChar lastLineEndChar = LineEndChar.None;
                    List<string> lines = new List<string>();

                    int i;
                    for (i = 0; i < text.Length; i++)
                    {
                        if (text[i] == '\r')
                        {
                            // A line is end.
                            int lineWidth = i - startLineIndex;
                            lines.Add(text.Substring(startLineIndex, lineWidth));
                            if (lineWidth > width) width = lineWidth;

                            startLineIndex = i + 1;
                            lastLineEndChar = LineEndChar.Cr;
                        }
                        else if (text[i] == '\n')
                        {
                            if (i > startLineIndex || lastLineEndChar != LineEndChar.Cr)
                            {
                                // A line is end.
                                int lineWidth = i - startLineIndex;
                                lines.Add(text.Substring(startLineIndex, lineWidth));
                                if (lineWidth > width) width = lineWidth;
                            }

                            startLineIndex = i + 1;
                            lastLineEndChar = LineEndChar.Lf;
                        }
                    }

                    // Process the remaining text.

                    {
                        int lineWidth = i - startLineIndex;
                        lines.Add(text.Substring(startLineIndex, lineWidth));
                        if (lineWidth > width) width = lineWidth;
                    }

                    RawText = text;
                    Lines = lines.AsReadOnly();
                    Size = new Size(width, lines.Count);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error splitting the text in multiple lines.", ex);
                }
            }
        }

        public MultilineText(IEnumerable<string> lines)
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            List<string> linesAsList = lines.ToList();

            RawText = string.Join(Environment.NewLine, linesAsList);
            Lines = linesAsList.AsReadOnly();

            int width = linesAsList.Count == 0
                ? 0
                : linesAsList.Max(x => x.Length);
            int height = linesAsList.Count;
            Size = new Size(width, height);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MultilineText multilineText = obj as MultilineText;

            if (multilineText != null)
                return RawText == multilineText.RawText;

            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return RawText.GetHashCode();
        }

        public static implicit operator MultilineText(string text)
        {
            return new MultilineText(text);
        }

        public static implicit operator string(MultilineText multilineText)
        {
            return string.Join(Environment.NewLine, multilineText.Lines);
        }

        public static implicit operator MultilineText(List<string> lines)
        {
            return new MultilineText(lines);
        }

        public static implicit operator List<string>(MultilineText multilineText)
        {
            return multilineText.Lines.ToList();
        }
    }
}
