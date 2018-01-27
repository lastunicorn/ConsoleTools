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
        private readonly StringListRead preferedBeveragesRead;

        public AddPersonView()
        {
            firstNameRead = new StringRead("First Name:");
            lastNameRead = new StringRead("Last Name:");
            birthdayRead = new ValueRead<DateTime>("Birthday:");
            phoneNumberRead = new StringRead("Phone Number:");
            heightRead = new FloatRead("Height (in meters)");
            preferedBeveragesRead = new StringListRead("Prefered Beverages:");
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