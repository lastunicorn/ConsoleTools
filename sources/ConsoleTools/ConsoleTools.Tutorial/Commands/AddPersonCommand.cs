using System;
using DustIntheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustIntheWind.ConsoleTools.Tutorial.CustomControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustIntheWind.ConsoleTools.Tutorial.Commands
{
    internal class AddPersonCommand : ICommand
    {
        private readonly AddressBook addressBook;
        public bool IsActive { get; } = true;

        public AddPersonCommand(AddressBook addressBook)
        {
            if (addressBook == null) throw new ArgumentNullException(nameof(addressBook));
            this.addressBook = addressBook;
        }

        public void Execute()
        {
            AddPersonView addPersonView = new AddPersonView();
            addPersonView.Display();

            Person newPerson = addPersonView.Person;
            addressBook.AddPerson(newPerson);
        }
    }
}