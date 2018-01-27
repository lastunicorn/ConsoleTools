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
        private readonly StringListWrite preferedBeveragesWrite;
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
            preferedBeveragesWrite = new StringListWrite("Prefered Beverages:");
        }

        public void Display()
        {
            firstNameWrite.Write(Person.FirstName);
            lastNameWrite.Write(Person.LastName);
            birthdayWrite.Write(Person.Birthday);
            phoneNumberWrite.Write(Person.PhoneNumber);
            heightWrite.Write(Person.Height);
            preferedBeveragesWrite.Write(Person.PreferedBeverages);
        }
    }
}