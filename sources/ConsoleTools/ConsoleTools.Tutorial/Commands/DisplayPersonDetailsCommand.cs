using System;
using DustIntheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustIntheWind.ConsoleTools.Tutorial.CustomControls;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustIntheWind.ConsoleTools.Tutorial.Commands
{
    internal class DisplayPersonDetailsCommand : ICommand
    {
        private readonly AddressBook addressBook;

        public bool IsActive { get; } = true;

        public DisplayPersonDetailsCommand(AddressBook addressBook)
        {
            if (addressBook == null) throw new ArgumentNullException(nameof(addressBook));
            this.addressBook = addressBook;
        }

        public void Execute()
        {
            int id = AskForId();
            Person person = addressBook.GetPerson(id);

            if (person == null)
            {
                CustomConsole.WriteError("There is no person with id {0}", id);
                return;
            }

            PersonView personView = new PersonView
            {
                Person = person
            };

            personView.Display();
        }

        private static int AskForId()
        {
            Int32Read idRead = new Int32Read("The id of the person:");

            int id = idRead.Read();
            CustomConsole.WriteLine();

            return id;
        }
    }
}