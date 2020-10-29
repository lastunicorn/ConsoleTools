// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DustInTheWind.ConsoleTools
{
    /// <summary>
    /// Represents a text on multiple lines.
    /// </summary>
    public class MultilineText : IEnumerable<string>
    {
        /// <summary>
        /// Gets the text as a single line.
        /// </summary>
        public string RawText { get; }

        /// <summary>
        /// Gets the text split in lines.
        /// </summary>
        public ReadOnlyCollection<string> Lines { get; }

        /// <summary>
        /// Gets the size of the smallest rectangle in which the text will fit.
        /// </summary>
        public Size Size { get; }

        /// <summary>
        /// Gets an instance of the <see cref="MultilineText"/> containing no text.
        /// </summary>
        public static MultilineText Empty { get; } = new MultilineText(string.Empty);

        /// <summary>
        /// Gets a value that specifies if the current instance contains no text.
        /// </summary>
        public bool IsEmpty => Size.IsEmpty;

        /// <summary>
        /// Used only internally when splitting the string in multiple lines.
        /// </summary>
        private enum LineEndChar
        {
            None,
            Cr,
            Lf
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineText"/> class with
        /// the an object to be displayed as text.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public MultilineText(object o)
            : this(o.ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineText"/> class with
        /// the raw text.
        /// </summary>
        /// <param name="text">The text may contain line terminators: CR, LF or CRLF.</param>
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
                            // A line is ended.
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
                                // A line is ended.
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineText"/> class with
        /// the list of lines.
        /// </summary>
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
        /// Calculates the size of the text.
        /// </summary>
        /// <param name="maxWidth">The maximum width allowed. Negative value means no limit.</param>
        /// <returns>Returns a new instance of <see cref="Size"/> representing the size of the text.</returns>
        public Size CalculateSize(int maxWidth = -1)
        {
            if (maxWidth < 0)
                return Size;

            if (maxWidth == 0)
                return Size.Empty;

            int totalWidth = 0;
            int totalHeight = 0;

            foreach (string line in Lines)
            {
                int lineHeight = (int)Math.Ceiling((double)line.Length / maxWidth);
                totalHeight += lineHeight;

                int lineWidth = Math.Min(line.Length, maxWidth);
                totalWidth = Math.Max(totalWidth, lineWidth);
            }

            return new Size(totalWidth, totalHeight);
        }

        /// <summary>
        /// Enumerates the lines. If a line is greater than <see cref="P:maxWidth"/>,
        /// the line is cut in chunks with the length of <see cref="P:maxWidth"/>.
        /// </summary>
        /// <param name="maxWidth">The maximum width allowed. Negative value means no limit.</param>
        /// <returns></returns>
        public IEnumerable<string> GetLines(int maxWidth = -1)
        {
            if (maxWidth < 0)
            {
                foreach (string line in Lines)
                    yield return line;

                yield break;
            }

            if (maxWidth == 0)
                yield break;

            foreach (string line in Lines)
            {
                int index = 0;

                while (index < line.Length)
                {
                    int chunkLength = Math.Min(maxWidth, line.Length - index);
                    string chunk = line.Substring(index, chunkLength);
                    yield return chunk;

                    index += chunkLength;
                }
            }
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is MultilineText multilineText)
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that enumerates through the lines.
        /// </summary>
        public IEnumerator<string> GetEnumerator()
        {
            return Lines.GetEnumerator();
        }

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        public override string ToString()
        {
            return string.Join(Environment.NewLine, Lines);
        }

        /// <summary>
        /// Converts a simple text into a <see cref="MultilineText"/> instance.
        /// </summary>
        /// <param name="text">The text to be converted.</param>
        public static implicit operator MultilineText(string text)
        {
            return new MultilineText(text);
        }

        /// <summary>
        /// Converts a <see cref="MultilineText"/> instance into a simple <see cref="string"/>.
        /// </summary>
        /// <param name="multilineText">The <see cref="MultilineText"/> instance to convert.</param>
        public static implicit operator string(MultilineText multilineText)
        {
            return multilineText.ToString();
        }

        /// <summary>
        /// Converts a <see cref="List{T}"/> of <see cref="string"/> into a <see cref="MultilineText"/> instance.
        /// </summary>
        /// <param name="lines">The list of lines to be contained by the new <see cref="MultilineText"/> instance.</param>
        public static implicit operator MultilineText(List<string> lines)
        {
            return new MultilineText(lines);
        }

        /// <summary>
        /// Converts a <see cref="MultilineText"/> instance into a <see cref="List{T}"/> of <see cref="string"/>.
        /// </summary>
        /// <param name="multilineText">The <see cref="MultilineText"/> instance to convert.</param>
        public static implicit operator List<string>(MultilineText multilineText)
        {
            return multilineText.Lines.ToList();
        }

        /// <summary>
        /// Converts a <see cref="string"/> array into a <see cref="MultilineText"/> instance.
        /// </summary>
        /// <param name="lines">The list of lines to be contained by the new <see cref="MultilineText"/> instance.</param>
        public static implicit operator MultilineText(string[] lines)
        {
            return new MultilineText(lines);
        }

        /// <summary>
        /// Converts a <see cref="MultilineText"/> instance into an array of <see cref="string"/>.
        /// </summary>
        /// <param name="multilineText">The <see cref="MultilineText"/> instance to convert.</param>
        public static implicit operator string[] (MultilineText multilineText)
        {
            return multilineText.Lines.ToArray();
        }
    }
}