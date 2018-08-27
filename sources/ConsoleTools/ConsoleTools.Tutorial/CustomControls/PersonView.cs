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
    internal class PersonView
    {
        private readonly StringWrite firstNameWrite;
        private readonly StringWrite lastNameWrite;
        private readonly ValueWrite<DateTime> birthdayWrite;
        private readonly StringWrite phoneNumberWrite;
        private readonly FloatWrite heightWrite;
        private readonly StringListView preferedBeveragesWrite;
        private Person person;

        public Person Person
        {
            get { return person; }
            set
            {
                person = value;

                firstNameWrite.Value = Person.FirstName;
                lastNameWrite.Value = Person.LastName;
                birthdayWrite.Value = Person.Birthday;
                phoneNumberWrite.Value = Person.PhoneNumber;
                heightWrite.Value = Person.Height;
                preferedBeveragesWrite.Values = Person.PreferedBeverages;
            }
        }

        public PersonView()
        {
            firstNameWrite = new StringWrite("First Name:");
            lastNameWrite = new StringWrite("Last Name:");
            birthdayWrite = new ValueWrite<DateTime>("Birthday:");
            phoneNumberWrite = new StringWrite("Phone Number:");
            heightWrite = new FloatWrite("Height (in meters)");
            preferedBeveragesWrite = new StringListView("Prefered Beverages:");
        }

        public void Display()
        {
            firstNameWrite.Write(Person.FirstName);
            lastNameWrite.Write(Person.LastName);
            birthdayWrite.Write(Person.Birthday);
            phoneNumberWrite.Write(Person.PhoneNumber);
            heightWrite.Write(Person.Height);
            preferedBeveragesWrite.Values = Person.PreferedBeverages;
            preferedBeveragesWrite.Write();
        }
    }
}