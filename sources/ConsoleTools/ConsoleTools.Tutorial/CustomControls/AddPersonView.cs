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
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.Tutorial.AddressBookModel;

namespace DustInTheWind.ConsoleTools.Tutorial.CustomControls
{
    internal class AddPersonView
    {
        private readonly StringView firstNameView;
        private readonly StringView lastNameView;
        private readonly ValueView<DateTime> birthdayView;
        private readonly StringView phoneNumberView;
        private readonly FloatView heightView;
        private readonly StringListView preferedBeveragesRead;

        public AddPersonView()
        {
            firstNameView = new StringView("First Name:");
            lastNameView = new StringView("Last Name:");
            birthdayView = new ValueView<DateTime>("Birthday:");
            phoneNumberView = new StringView("Phone Number:");
            heightView = new FloatView("Height (in meters)");
            preferedBeveragesRead = new StringListView("Prefered Beverages:");
        }

        public Person Person { get; private set; }

        public void Display()
        {
            Person = null;

            firstNameView.Display();
            lastNameView.Display();
            birthdayView.Display();
            phoneNumberView.Display();
            heightView.Display();
            preferedBeveragesRead.Display();

            Person = new Person
            {
                FirstName = firstNameView.Value,
                LastName = lastNameView.Value,
                Birthday = birthdayView.Value,
                PhoneNumber = phoneNumberView.Value,
                Height = heightView.Value,
                PreferedBeverages = preferedBeveragesRead.Values
            };
        }
    }
}