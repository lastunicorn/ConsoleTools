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
using System.Reflection;
using System.Text;

namespace DustInTheWind.ConsoleTools.Controls
{
    /// <summary>
    /// Displays a header containing the application's name and version.
    /// </summary>
    public partial class ApplicationHeader : BlockControl
    {
        private readonly ApplicationInformation applicationInformation;
        
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the title to be displayed in the header.
        /// If <c>null</c>, the value of the <see cref="AssemblyProductAttribute"/> attribute will be displayed
        /// taken from the entry assembly.
        /// If the attribute is absent, only the version will be displayed.
        /// </summary>
        public string ProductName { get; set; }

        public Version ProductVersion { get; set; }

        public string Appendix { get; set; }

        public MultilineText Description { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the version of the application should be displayed
        /// after the title.
        /// Default value: <c>true</c>
        /// </summary>
        public bool IsVersionVisible { get; set; } = true;

        /// <summary>
        /// Gets or sets a value that specifies if a separator line below the title
        /// should be displayed.
        /// Default value: <c>true</c>
        /// </summary>
        public bool IsSeparatorVisible { get; set; } = true;

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
            Title = title;

            applicationInformation = new ApplicationInformation();

            Margin = "0 0 0 1";
        }

        public override IControlRenderer GetRenderer(IDisplay display)
        {
            return new Renderer(this, display);
        }

        private string BuildTitleText()
        {
            return BuildTitleText(Title);
        }

        protected virtual string BuildTitleText(string proposedTitle)
        {
            if (proposedTitle != null)
                return proposedTitle;

            return BuildDefaultTitle();
        }

        private string BuildDefaultTitle()
        {
            StringBuilder sb = new StringBuilder();

            string productName = BuildProductName();
            if (productName != null)
                sb.Append(productName);

            if (IsVersionVisible)
            {
                string productVersion = BuildProductVersion();
                if (productVersion != null)
                {
                    if (sb.Length > 0)
                        sb.Append(" ");

                    sb.Append(productVersion);
                }
            }

            string appendix = BuildAppendixText();
            if (appendix != null)
            {
                if (sb.Length > 0)
                    sb.Append(" - ");

                sb.Append(appendix);
            }

            return sb.ToString();
        }

        protected virtual string BuildProductName()
        {
            return ProductName ?? applicationInformation.GetProductName();
        }

        protected virtual string BuildProductVersion()
        {
            Version version = ProductVersion ?? applicationInformation.GetVersion();
            return version?.ToString(3);
        }

        private string BuildAppendixText()
        {
            return Appendix;
        }

        protected virtual MultilineText BuildDescription()
        {
            return Description;
        }
    }
}