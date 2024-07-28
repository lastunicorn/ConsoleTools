// ConsoleTools
// Copyright (C) 2017-2022 Dust in the Wind
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

namespace DustInTheWind.ConsoleTools.Controls
{
    public abstract class LineEnumerator : IEnumerator<Line>
    {
        private bool isInitialized;
        private int topMarginRemaining;
        private int topPaddingRemaining;
        private int bottomPaddingRemaining;
        private int bottomMarginRemaining;

        private IEnumerator<Line> contentLineEnumerator;
        private bool hasMoreContentLines;

        private ControlRenderingState renderingState;
        private readonly IDisplay display;

        private bool HasMoreLines
        {
            get
            {
                if (!isInitialized)
                    Initialize();

                return topMarginRemaining > 0 ||
                       topPaddingRemaining > 0 ||
                       hasMoreContentLines ||
                       bottomPaddingRemaining > 0 ||
                       bottomMarginRemaining > 0;
            }
        }
        
        protected LineEnumerator(IDisplay display)
        {
            this.display = display ?? throw new ArgumentNullException(nameof(display));
        }

        private void Initialize()
        {
            IEnumerable<Line> contentLines = GetContentLines(display);
            contentLineEnumerator = contentLines.GetEnumerator();
            hasMoreContentLines = contentLineEnumerator.MoveNext();

            topMarginRemaining = display.ControlLayout.MarginTop;
            topPaddingRemaining = display.ControlLayout.PaddingTop;
            bottomPaddingRemaining = display.ControlLayout.PaddingBottom;
            bottomMarginRemaining = display.ControlLayout.MarginBottom;

            UpdateState();

            isInitialized = true;
        }

        protected abstract IEnumerable<Line> GetContentLines(IDisplay display);

        private void GenerateNextLine()
        {
            if (!isInitialized)
                Initialize();

            switch (renderingState)
            {
                case ControlRenderingState.TopMargin:
                    RenderNextTopMarginLine();
                    break;

                case ControlRenderingState.TopPadding:
                    RenderNextTopPaddingLine();
                    break;

                case ControlRenderingState.Content:
                    RenderNextContentLine();
                    break;

                case ControlRenderingState.BottomPadding:
                    RenderNextBottomPaddingLine();
                    break;

                case ControlRenderingState.BottomMargin:
                    RenderNextBottomMarginLine();
                    break;

                case ControlRenderingState.Finished:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdateState();
        }

        private void RenderNextContentLine()
        {
            Line line = display.CreateNewLine();



            display.StartRow();

            if (contentLineEnumerator.Current != null)
            {
                foreach (LineSection lineSection in contentLineEnumerator.Current)
                    display.Write(lineSection.ForegroundColor, lineSection.BackgroundColor, lineSection.Text);
            }

            display.EndRow();

            hasMoreContentLines = contentLineEnumerator.MoveNext();
        }

        private void UpdateState()
        {
            if (topMarginRemaining > 0)
                renderingState = ControlRenderingState.TopMargin;
            else if (topPaddingRemaining > 0)
                renderingState = ControlRenderingState.TopPadding;
            else if (hasMoreContentLines)
                renderingState = ControlRenderingState.Content;
            else if (bottomPaddingRemaining > 0)
                renderingState = ControlRenderingState.BottomPadding;
            else if (bottomMarginRemaining > 0)
                renderingState = ControlRenderingState.BottomMargin;
            else
                renderingState = ControlRenderingState.Finished;
        }

        private void RenderNextTopMarginLine()
        {
            if (topMarginRemaining > 0)
            {
                Current = new Line(string.Empty);

                topMarginRemaining--;
            }
        }

        private void RenderNextBottomMarginLine()
        {
            if (bottomMarginRemaining > 0)
            {
                Current = new Line(string.Empty);

                bottomMarginRemaining--;
            }
        }

        private void RenderNextTopPaddingLine()
        {
            if (topPaddingRemaining > 0)
            {
                string text = new string(' ', display.ControlLayout.ActualContentWidth);
                display.WriteRow(text);

                topPaddingRemaining--;
            }
        }

        private void RenderNextBottomPaddingLine()
        {
            if (bottomPaddingRemaining > 0)
            {
                string text = new string(' ', display.ControlLayout.ActualContentWidth);
                display.WriteRow(text);

                bottomPaddingRemaining--;
            }
        }

        public bool MoveNext()
        {
            GenerateNextLine();
            return HasMoreLines;
        }

        public void Reset()
        {
            Initialize();
        }

        public Line Current { get; private set; }

        object IEnumerator.Current { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                contentLineEnumerator?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}