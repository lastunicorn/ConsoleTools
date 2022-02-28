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
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new/choose

using System;
using System.Collections.Generic;
using DustInTheWind.ConsoleTools.Menues;
using DustInTheWind.ConsoleTools.Menues.MenuItems;
using DustInTheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustInTheWind.ConsoleTools.Tutorial.Commands;

namespace DustInTheWind.ConsoleTools.Tutorial
{
    internal class MainMenu : ScrollMenu
    {
        public MainMenu(AddressBookApplication addressBookApplication, AddressBook addressBook)
        {
            if (addressBookApplication == null) throw new ArgumentNullException(nameof(addressBookApplication));
            if (addressBook == null) throw new ArgumentNullException(nameof(addressBook));

            IEnumerable<IMenuItem> menuItems = CreateMenuItems(addressBookApplication, addressBook);
            AddItems(menuItems);
        }

        private static IEnumerable<IMenuItem> CreateMenuItems(AddressBookApplication addressBookApplication, AddressBook addressBook)
        {
            return new List<IMenuItem>
            {
                new LabelMenuItem
                {
                    Text = "Add Person",
                    Command = new AddPersonCommand(addressBook)
                },
                new LabelMenuItem
                {
                    Text = "Display Persons",
                    Command = new DisplayPersonsCommand(addressBook)
                },
                new LabelMenuItem
                {
                    Text = "Display Person Details",
                    Command = new DisplayPersonDetailsCommand(addressBook)
                },
                new SeparatorMenuItem(),
                new LabelMenuItem
                {
                    Text = "Exit",
                    Command = new ExitCommand(addressBookApplication)
                }
            };
        }
    }
}