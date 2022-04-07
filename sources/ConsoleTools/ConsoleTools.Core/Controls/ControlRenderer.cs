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
using System.Collections.Generic;

namespace DustInTheWind.ConsoleTools.Controls
{
    public abstract class ControlRenderer<TItem> : IControlRenderer
    {
        private IEnumerator<TItem> itemsEnumerator;
        private bool hasMoreContentRows;
        private bool isInitialized;
        private ControlRenderingState state;
        private int topMarginRemaining;
        private int topPaddingRemaining;
        private int bottomPaddingRemaining;
        private int bottomMarginRemaining;

        protected IDisplay Display { get; }

        public bool HasMoreRows
        {
            get
            {
                if (!isInitialized)
                    Initialize();

                return topMarginRemaining > 0 ||
                       topPaddingRemaining > 0 ||
                       hasMoreContentRows ||
                       bottomPaddingRemaining > 0 ||
                       bottomMarginRemaining > 0;
            }
        }


        protected ControlRenderer(IDisplay display)
        {
            Display = display ?? throw new ArgumentNullException(nameof(display));
        }

        private void Initialize()
        {
            IEnumerable<TItem> items = EnumerateContentRows();
            itemsEnumerator = items.GetEnumerator();
            hasMoreContentRows = itemsEnumerator.MoveNext();
            topMarginRemaining = Display.ControlLayout.MarginTop;
            topPaddingRemaining = Display.ControlLayout.PaddingTop;
            bottomPaddingRemaining = Display.ControlLayout.PaddingBottom;
            bottomMarginRemaining = Display.ControlLayout.MarginBottom;

            UpdateState();

            isInitialized = true;
        }

        protected abstract IEnumerable<TItem> EnumerateContentRows();

        public void RenderNextRow()
        {
            if (!isInitialized)
                Initialize();

            switch (state)
            {
                case ControlRenderingState.TopMargin:
                    WriteNextTopMarginRow();
                    break;

                case ControlRenderingState.TopPadding:
                    WriteTopPadding();
                    break;

                case ControlRenderingState.Content:
                    TItem item = itemsEnumerator.Current;
                    DoRenderNextContentRow(item);
                    hasMoreContentRows = itemsEnumerator.MoveNext();
                    break;

                case ControlRenderingState.BottomPadding:
                    WriteBottomPadding();
                    break;

                case ControlRenderingState.BottomMargin:
                    WriteBottomMargin();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdateState();
        }

        private void UpdateState()
        {
            if (topMarginRemaining > 0)
                state = ControlRenderingState.TopMargin;
            else if (topPaddingRemaining > 0)
                state = ControlRenderingState.TopPadding;
            else if (hasMoreContentRows)
                state = ControlRenderingState.Content;
            else if (bottomPaddingRemaining > 0)
                state = ControlRenderingState.BottomPadding;
            else if (bottomMarginRemaining > 0)
                state = ControlRenderingState.BottomMargin;
            else
                state = ControlRenderingState.Finished;
        }

        protected abstract void DoRenderNextContentRow(TItem row);

        private void WriteNextTopMarginRow()
        {
            if (topMarginRemaining > 0)
                topMarginRemaining--;
        }

        private void WriteBottomMargin()
        {
            if (bottomMarginRemaining > 0)
                bottomMarginRemaining--;
        }

        private void WriteTopPadding()
        {
            if (topPaddingRemaining > 0)
            {
                string text = new string(' ', Display.ControlLayout.ActualContentWidth);
                Display.WriteRow(text);

                topPaddingRemaining--;
            }
        }

        private void WriteBottomPadding()
        {
            if (bottomPaddingRemaining > 0)
            {
                string text = new string(' ', Display.ControlLayout.ActualContentWidth);
                Display.WriteRow(text);

                bottomPaddingRemaining--;
            }
        }
    }
}