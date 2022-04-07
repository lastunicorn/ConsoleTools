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
    public abstract class RowsControlRenderer<TRow> : ControlRenderer
    {
        private IEnumerator<TRow> rowEnumerator;
        private bool hasMoreContentRows;

        protected override bool HasMoreContentRows => hasMoreContentRows;

        protected RowsControlRenderer(IDisplay display)
            : base(display)
        {
        }

        protected override void Initialize()
        {
            IEnumerable<TRow> items = EnumerateContentRows();
            rowEnumerator = items.GetEnumerator();
            hasMoreContentRows = rowEnumerator.MoveNext();

            base.Initialize();
        }

        protected abstract IEnumerable<TRow> EnumerateContentRows();

        protected override void RenderNextContentRow()
        {
            TRow item = rowEnumerator.Current;
            DoRenderNextContentRow(item);
            hasMoreContentRows = rowEnumerator.MoveNext();
        }

        protected abstract void DoRenderNextContentRow(TRow row);
    }

    public abstract class ControlRenderer : IControlRenderer
    {
        private bool isInitialized;
        private int topMarginRemaining;
        private int topPaddingRemaining;
        private int bottomPaddingRemaining;
        private int bottomMarginRemaining;

        public ControlRenderingState RenderingState { get; private set; }

        protected abstract bool HasMoreContentRows { get; }

        protected IDisplay Display { get; }

        public bool HasMoreRows
        {
            get
            {
                if (!isInitialized)
                    Initialize();

                return topMarginRemaining > 0 ||
                       topPaddingRemaining > 0 ||
                       HasMoreContentRows ||
                       bottomPaddingRemaining > 0 ||
                       bottomMarginRemaining > 0;
            }
        }


        protected ControlRenderer(IDisplay display)
        {
            Display = display ?? throw new ArgumentNullException(nameof(display));
        }

        protected virtual void Initialize()
        {
            topMarginRemaining = Display.ControlLayout.MarginTop;
            topPaddingRemaining = Display.ControlLayout.PaddingTop;
            bottomPaddingRemaining = Display.ControlLayout.PaddingBottom;
            bottomMarginRemaining = Display.ControlLayout.MarginBottom;

            UpdateState();

            isInitialized = true;
        }

        public void RenderNextRow()
        {
            if (!isInitialized)
                Initialize();

            switch (RenderingState)
            {
                case ControlRenderingState.TopMargin:
                    RenderNextTopMarginRow();
                    break;

                case ControlRenderingState.TopPadding:
                    RenderNextTopPaddingRow();
                    break;

                case ControlRenderingState.Content:
                    RenderNextContentRow();
                    break;

                case ControlRenderingState.BottomPadding:
                    RenderNextBottomPaddingRow();
                    break;

                case ControlRenderingState.BottomMargin:
                    RenderNextBottomMarginRow();
                    break;

                case ControlRenderingState.Finished:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdateState();
        }

        protected abstract void RenderNextContentRow();

        private void UpdateState()
        {
            if (topMarginRemaining > 0)
                RenderingState = ControlRenderingState.TopMargin;
            else if (topPaddingRemaining > 0)
                RenderingState = ControlRenderingState.TopPadding;
            else if (HasMoreContentRows)
                RenderingState = ControlRenderingState.Content;
            else if (bottomPaddingRemaining > 0)
                RenderingState = ControlRenderingState.BottomPadding;
            else if (bottomMarginRemaining > 0)
                RenderingState = ControlRenderingState.BottomMargin;
            else
                RenderingState = ControlRenderingState.Finished;
        }

        private void RenderNextTopMarginRow()
        {
            if (topMarginRemaining > 0)
                topMarginRemaining--;
        }

        private void RenderNextBottomMarginRow()
        {
            if (bottomMarginRemaining > 0)
                bottomMarginRemaining--;
        }

        private void RenderNextTopPaddingRow()
        {
            if (topPaddingRemaining > 0)
            {
                string text = new string(' ', Display.ControlLayout.ActualContentWidth);
                Display.WriteRow(text);

                topPaddingRemaining--;
            }
        }

        private void RenderNextBottomPaddingRow()
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