using System;
using DustIntheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustIntheWind.ConsoleTools.Tutorial.CustomControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustIntheWind.ConsoleTools.Tutorial.Commands
{
    internal class DisplayPersonsCommand : ICommand
    {
        private readonly AddressBook addressBook;

        public bool IsActive { get; } = true;

        public DisplayPersonsCommand(AddressBook addressBook)
        {
            if (addressBook == null) throw new ArgumentNullException(nameof(addressBook));
            this.addressBook = addressBook;
        }

        public void Execute()
        {
            PersonsView personsView = new PersonsView
            {
                Persons = addressBook
            };

            personsView.Display();
        }
    }
}