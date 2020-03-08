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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Reflection;
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls
{
    /// <summary>
    /// Displays a header containing the application's name and version.
    /// </summary>
    public class ApplicationHeader : BlockControl
    {
        private readonly ApplicationInformation applicationInformation;

        /// <summary>
        /// Gets or sets the title to be displayed in the header.
        /// If <c>null</c>, the value of the <see cref="AssemblyProductAttribute"/> attribute will be displayed
        /// taken from the entry assembly.
        /// If the attribute is absent, only the version will be displayed.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the version of the application should be displayed
        /// after the title.
        /// Default value: <c>true</c>
        /// </summary>
        public bool ShowVersion { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that specifies if a separator line below the title
        /// should be displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool ShowSeparator { get; set; } = true;
        
        /// <summary>
        /// Event raised before the title is displayed.
        /// It allows to alter the title before it is displayed. 
        /// </summary>
        public event EventHandler<TitleDisplayEventArgs> TitleDisplay;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationHeader"/> class.
        /// </summary>
        public ApplicationHeader()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationHeader"/> class
        /// with a custom title text.
        /// </summary>
        public ApplicationHeader(string title)
        {
            applicationInformation = new ApplicationInformation();
            Title = title ?? applicationInformation.GetProductName();

            Margin = "0 0 0 1";
        }

        /// <summary>
        /// This method actually displays the header.
        /// Can be overwritten in order to fully control what is displayed.
        /// </summary>
        /// <param name="display">This instance can be used in order to interact with the console.</param>
        protected override void DoDisplayContent(ControlDisplay display)
        {
            Version version = applicationInformation.GetVersion();

            StringBuilder titleRowText = new StringBuilder();

            string title = BuildTitle();
            if (title != null)
                titleRowText.Append(title);

            if (ShowVersion)
            {
                if (titleRowText.Length > 0)
                    titleRowText.Append(" ");

                titleRowText.Append(version.ToString(3));
            }
            
            display.StartRow();
            display.Write(titleRowText.ToString());
            Console.WriteLine();

            if (ShowSeparator)
                display.WriteRow(new string('=', Console.WindowWidth - 1));
        }

        private string BuildTitle()
        {
            TitleDisplayEventArgs titleDisplayEventArgs = new TitleDisplayEventArgs(Title);
            OnTitleDisplay(titleDisplayEventArgs);

            return titleDisplayEventArgs.Title;
        }

        protected virtual void OnTitleDisplay(TitleDisplayEventArgs e)
        {
            TitleDisplay?.Invoke(this, e);
        }
    }
}