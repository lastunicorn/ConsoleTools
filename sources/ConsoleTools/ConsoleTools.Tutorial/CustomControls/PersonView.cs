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
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.Tutorial.AddressBookModel;

namespace DustInTheWind.ConsoleTools.Tutorial.CustomControls
{
    internal class PersonView
    {
        private readonly StringView firstNameView;
        private readonly StringView lastNameView;
        private readonly ValueView<DateTime> birthdayView;
        private readonly StringView phoneNumberView;
        private readonly FloatView heightView;
        private readonly StringListView preferedBeveragesView;
        private Person person;

        public Person Person
        {
            get { return person; }
            set
            {
                person = value;

                firstNameView.Value = Person.FirstName;
                lastNameView.Value = Person.LastName;
                birthdayView.Value = Person.Birthday;
                phoneNumberView.Value = Person.PhoneNumber;
                heightView.Value = Person.Height;
                preferedBeveragesView.Values = Person.PreferedBeverages;
            }
        }

        public PersonView()
        {
            firstNameView = new StringView("First Name:");
            lastNameView = new StringView("Last Name:");
            birthdayView = new ValueView<DateTime>("Birthday:");
            phoneNumberView = new StringView("Phone Number:");
            heightView = new FloatView("Height (in meters)");
            preferedBeveragesView = new StringListView("Prefered Beverages:");
        }

        public void Display()
        {
            firstNameView.Value = Person.FirstName;
            firstNameView.Write();

            lastNameView.Value = Person.LastName;
            lastNameView.Write();

            birthdayView.Value = Person.Birthday;
            birthdayView.Write();

            phoneNumberView.Value = Person.PhoneNumber;
            phoneNumberView.Write();

            heightView.Value = Person.Height;
            heightView.Write();

            preferedBeveragesView.Values = Person.PreferedBeverages;
            preferedBeveragesView.Write();
        }
    }
}