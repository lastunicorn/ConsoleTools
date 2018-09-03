// ConsoleTools
// Copyright (C) 2017-2018 Dust in the Wind
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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.ComponentModel;
using System.Text;

namespace DustInTheWind.ConsoleTools.Menues.MenuItems
{
    /// <summary>
    /// Represents a menu item that displays a text.
    /// </summary>
    public class LabelMenuItem : IMenuItem
    {
        private const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Center;
        private const HighlightType DefaultHighlightType = HighlightType.OnlyText;

        private bool isVisible = true;

        private string calculatedContent;
        private string text;
        private int paddingLeft = 1;
        private int paddingRight = 1;
        private bool isEnabled = true;

        private string CalculatedContent
        {
            get
            {
                if (calculatedContent == null)
                    calculatedContent = CalculateText();

                return calculatedContent;
            }
        }

        /// <summary>
        /// Gets the location in the console where the current instance was last rendered.
        /// If the current instance was never rendered, this value is <c>null</c>.
        /// </summary>
        public Location? Location { get; private set; }

        /// <summary>
        /// Gets the size in characters necessary for the current instance to be rendered.
        /// </summary>
        public Size Size => new Size(CalculatedContent.Length, 1);

        /// <summary>
        /// Gets or sets the text displayed by the current instance.
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                calculatedContent = null;
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies if the current instance is displayed.
        /// If the <see cref="VisibilityProvider"/> is set, the value provided on the setter,
        /// is ignored.
        /// </summary>
        public bool IsVisible
        {
            get { return VisibilityProvider?.Invoke() ?? isVisible; }
            set { isVisible = value; }
        }

        /// <summary>
        /// Gets or sets a function that specifies if the current instance should be visible.
        /// </summary>
        public Func<bool> VisibilityProvider { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment of the current instance inside the menu.
        /// Default value: <see cref="HorizontalAlignment.Default"/>.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Default;

        /// <summary>
        /// Gets a value that specifies if the current instance can be selected.
        /// Default value: <c>true</c>
        /// </summary>
        public bool IsEnabled
        {
            get { return Command?.IsActive ?? isEnabled; }
            set { isEnabled = value; }
        }

        /// <summary>
        /// Gets or sets the shortcut key that will select the current instance of <see cref="IMenuItem"/>.
        /// Default value: <c>null</c>
        /// </summary>
        public ConsoleKey? ShortcutKey { get; set; }

        /// <summary>
        /// Gets or sets the command to be executed when the current instance is selected.
        /// Default value: <c>null</c>
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        /// Gets or sets the menu that contains the current instance.
        /// Default value: <c>null</c>
        /// </summary>
        public ScrollMenu ParentMenu { get; set; }

        /// <summary>
        /// Gets or sets the padding applied to the left of the text.
        /// The padding is part of the menu item's width.
        /// Default value: 1
        /// </summary>
        public int PaddingLeft
        {
            get { return paddingLeft; }
            set
            {
                paddingLeft = value;
                calculatedContent = null;
            }
        }

        /// <summary>
        /// Gets or sets the padding applied to the right of the text.
        /// The padding is part of the menu item's width.
        /// Default value: 1
        /// </summary>
        public int PaddingRight
        {
            get { return paddingRight; }
            set
            {
                paddingRight = value;
                calculatedContent = null;
            }
        }

        public HighlightType HighlightType { get; set; } = HighlightType.Default;

        /// <summary>
        /// Event raised before the current instance is selected.
        /// It gives the oportunity for a subscriber to cancel the selection of the menu item.
        /// </summary>
        public event EventHandler<CancelEventArgs> BeforeSelect;

        /// <summary>
        /// Displays the current instance to the Console starting from the current location of the cursor.
        /// </summary>
        /// <param name="size">The size in which the current instance must be displayed.</param>
        /// <param name="highlighted">A value that specifies if the menu item must be displayed highlighted.</param>
        public void Display(Size size, bool highlighted)
        {
            HorizontalAlignment calculatedHorizontalAlignment = CalculateHorizontalAlignment();

            Location = new Location(Console.CursorLeft, Console.CursorTop);

            AlignedText alignedText = new AlignedText
            {
                Text = CalculatedContent,
                HorizontalAlignment = calculatedHorizontalAlignment,
                Width = size.Width
            };

            if (highlighted)
            {
                HighlightType calculatedHighliteType = CalculateHighlightType();

                switch (calculatedHighliteType)
                {
                    case HighlightType.OnlyText:
                        CustomConsole.Write(new string(' ', alignedText.SpaceLeftCount));
                        CustomConsole.WriteInverted(alignedText.Text);
                        CustomConsole.Write(new string(' ', alignedText.SpaceRightCount));
                        break;

                    case HighlightType.WholeRow:
                        CustomConsole.WriteInverted(alignedText);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("Invalid calculated highlight type.");
                }
            }
            else
            {
                CustomConsole.Write(alignedText);
            }
        }

        private HighlightType CalculateHighlightType()
        {
            if (HighlightType != HighlightType.Default)
                return HighlightType;

            if (ParentMenu != null && ParentMenu.ItemsHighlightType != HighlightType.Default)
                return ParentMenu.ItemsHighlightType;

            return DefaultHighlightType;
        }

        private string CalculateText()
        {
            StringBuilder sb = new StringBuilder();

            if (PaddingLeft > 0)
                sb.Append(new string(' ', PaddingLeft));

            sb.Append(Text);

            if (PaddingRight > 0)
                sb.Append(new string(' ', PaddingRight));

            return sb.ToString();
        }

        private HorizontalAlignment CalculateHorizontalAlignment()
        {
            if (HorizontalAlignment != HorizontalAlignment.Default)
                return HorizontalAlignment;

            if (ParentMenu != null && ParentMenu.ItemsHorizontalAlignment != HorizontalAlignment.Default)
                return ParentMenu.ItemsHorizontalAlignment;

            return DefaultHorizontalAlignment;
        }

        /// <summary>
        /// Selects the current instance and executes the associated <see cref="Command"/>.
        /// </summary>
        /// <returns><c>true</c> if the menu item was successfully selected; <c>false</c> otherwise.</returns>
        public bool Select()
        {
            CancelEventArgs eventArgs = new CancelEventArgs();
            OnBeforeSelect(eventArgs);

            return !eventArgs.Cancel;
        }

        /// <summary>
        /// Raises the <see cref="BeforeSelect"/> event.
        /// </summary>
        protected virtual void OnBeforeSelect(CancelEventArgs e)
        {
            BeforeSelect?.Invoke(this, e);
        }
    }
}