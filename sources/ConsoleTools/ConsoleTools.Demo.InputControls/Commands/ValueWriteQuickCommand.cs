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

using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustInTheWind.ConsoleTools.Demo.InputControls.Commands
{
    internal class ValueWriteQuickCommand : ICommand
    {
        public bool IsActive => true;

        public void Execute()
        {
            StringWrite.QuickDisplay("First Name:", "John");
            StringWrite.QuickDisplay("Last Name:", "Doe");
            Int32Write.QuickDisplay("Age:", 25);

            // or

            //ValueWrite<string>.QuickDisplay("First Name:", "John");
            //ValueWrite<string>.QuickDisplay("Last Name:", "Doe");
            //ValueWrite<int>.QuickDisplay("Age:", 25);
        }
    }
}