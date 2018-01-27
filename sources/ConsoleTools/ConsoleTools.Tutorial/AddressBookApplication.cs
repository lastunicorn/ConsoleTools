using System.Collections.Generic;
using DustIntheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustIntheWind.ConsoleTools.Tutorial.Commands;
using DustInTheWind.ConsoleTools.MenuControl;
using DustInTheWind.ConsoleTools.MenuControl.MenuItems;

namespace DustIntheWind.ConsoleTools.Tutorial
{
    internal class AddressBookApplication
    {
        private bool isCloseRequested;
        private readonly AddressBook addressBook = new AddressBook();

        public void Run()
        {
            List<IMenuItem> menuItems = new List<IMenuItem>
            {
                new LabelMenuItem
                {
                    Text = "Add Person",
                    Command = new AddPersonCommand(addressBook)
                },
                new LabelMenuItem
                {
                    Text = "Display Persons",
                    Command = new DisplayPersonsCommand(addressBook)
                },
                new LabelMenuItem
                {
                    Text = "Display Person Details",
                    Command = new DisplayPersonDetailsCommand(addressBook)
                },
                new SeparatorMenuItem(),
                new LabelMenuItem
                {
                    Text = "Exit",
                    Command = new ExitCommand(this)
                }
            };

            ScrollableMenu menu = new ScrollableMenu(menuItems);

            while (!isCloseRequested)
            {
                menu.Display();
            }
        }

        public void RequestExit()
        {
            isCloseRequested = true;
        }
    }
}