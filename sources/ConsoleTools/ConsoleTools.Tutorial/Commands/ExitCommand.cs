using System;
using DustInTheWind.ConsoleTools.MenuControl;

namespace DustIntheWind.ConsoleTools.Tutorial.Commands
{
    internal class ExitCommand : ICommand
    {
        private readonly AddressBookApplication addressBookApplication;

        public bool IsActive { get; } = true;

        public ExitCommand(AddressBookApplication addressBookApplication)
        {
            if (addressBookApplication == null) throw new ArgumentNullException(nameof(addressBookApplication));
            this.addressBookApplication = addressBookApplication;
        }

        public void Execute()
        {
            addressBookApplication.RequestExit();
        }
    }
}