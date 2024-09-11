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

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Reflection;
using DustInTheWind.ConsoleTools.Controls.Rendering;

namespace DustInTheWind.ConsoleTools.Controls;

/// <summary>
/// Displays a header containing the application's name and version.
/// </summary>
public class ApplicationHeader : BlockControl
{
    /// <summary>
    /// Gets or sets the application name to be displayed in the header.
    /// Default value: The value of the <see cref="AssemblyProductAttribute"/> attribute taken from
    /// the entry assembly.
    /// </summary>
    public string ApplicationName { get; set; }

    /// <summary>
    /// Gets or sets a value that specifies if the version of the application should be displayed
    /// after the application name.
    /// Default value: <c>true</c>
    /// </summary>
    public bool ShowVersion { get; set; } = true;

    /// <summary>
    /// Gets or sets the application version to be displayed in the header.
    /// Default value: The value of the <see cref="AssemblyVersionAttribute"/> attribute taken from
    /// the entry assembly.
    /// </summary>
    public Version ApplicationVersion { get; set; }

    /// <summary>
    /// Gets or sets a value that specifies if a separator line should be displayed below the
    /// title.
    /// Default value: <c>true</c>
    /// </summary>
    public bool ShowSeparator { get; set; } = true;

    /// <summary>
    /// The natural width of the <see cref="ApplicationHeader"/> is infinite.
    /// Because infinite cannot be represented by an <see cref="Int32"/>, the actual value is
    /// <see cref="int.MaxValue"/> minus margins and paddings. 
    /// </summary>
    protected override int NaturalContentWidth => int.MaxValue - Padding.Left - Padding.Right - Margin.Left - Margin.Right;

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
        ApplicationInformation applicationInformation = new();
        ApplicationName = title ?? applicationInformation.GetProductName();
        ApplicationVersion = applicationInformation.GetVersion();

        Margin = "0 0 0 1";
    }

    /// <summary>
    /// Returns an object that is able to render the current <see cref="ApplicationHeader"/>
    /// instance into the specified <see cref="IDisplay"/>.
    /// </summary>
    public override IRenderer GetRenderer(IDisplay display, RenderingOptions renderingOptions = null)
    {
        return new ApplicationHeaderRenderer(this, display, renderingOptions);
    }
}