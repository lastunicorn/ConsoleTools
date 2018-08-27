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
using DustIntheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustInTheWind.ConsoleTools.InputControls;

namespace DustIntheWind.ConsoleTools.Tutorial.CustomControls
{
    internal class AddPersonView
    {
        private readonly StringRead firstNameRead;
        private readonly StringRead lastNameRead;
        private readonly ValueRead<DateTime> birthdayRead;
        private readonly StringRead phoneNumberRead;
        private readonly FloatRead heightRead;
        private readonly StringListView preferedBeveragesRead;

        public AddPersonView()
        {
            firstNameRead = new StringRead("First Name:");
            lastNameRead = new StringRead("Last Name:");
            birthdayRead = new ValueRead<DateTime>("Birthday:");
            phoneNumberRead = new StringRead("Phone Number:");
            heightRead = new FloatRead("Height (in meters)");
            preferedBeveragesRead = new StringListView("Prefered Beverages:");
        }

        public Person Person { get; private set; }

        public void Display()
        {
            Person = null;

            firstNameRead.Display();
            lastNameRead.Display();
            birthdayRead.Display();
            phoneNumberRead.Display();
            heightRead.Display();
            preferedBeveragesRead.Display();

            Person = new Person
            {
                FirstName = firstNameRead.Value,
                LastName = lastNameRead.Value,
                Birthday = birthdayRead.Value,
                PhoneNumber = phoneNumberRead.Value,
                Height = heightRead.Value,
                PreferedBeverages = preferedBeveragesRead.Values
            };
        }
    }
}