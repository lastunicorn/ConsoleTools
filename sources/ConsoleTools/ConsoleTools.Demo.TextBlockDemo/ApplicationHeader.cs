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

using System;
using System.Reflection;

namespace DustInTheWind.ConsoleTools.Demo.TextBlockDemo
{
    internal class ApplicationHeader
    {
        private TextBlock titleBlock;
        private HorizontalLine horizontalLine;

        public string Title { get; set; }

        public ApplicationHeader()
        {
            InitializeControls();
        }

        private void InitializeControls()
        {
            titleBlock = new TextBlock
            {
                ForegroundColor = CustomConsole.EmphasizedColor
            };

            horizontalLine = new HorizontalLine
            {
                Character = '=',
                ForegroundColor = CustomConsole.EmphasizedColor,
                Margin = "0 0 0 1"
            };
        }

        private static Version GetAssemblyVersion()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            return assemblyName.Version;
        }

        private static string GetAssemblyTitle()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyTitleAttribute assemblyTitleAttribute = assembly.GetCustomAttribute<AssemblyTitleAttribute>();

            return assemblyTitleAttribute?.Title;
        }

        public void Display()
        {
            OnBeforeDisplay();

            titleBlock.Display();
            horizontalLine.Display();
        }

        private void OnBeforeDisplay()
        {
            string title = Title ?? GetAssemblyTitle();

            Version version = GetAssemblyVersion();
            string versionAsString = version.ToString(3);

            titleBlock.Text = $"{title} - ver {versionAsString}";
        }
    }
}