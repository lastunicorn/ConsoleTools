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
using DustInTheWind.ConsoleTools.Menues;
using DustInTheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustInTheWind.ConsoleTools.Tutorial.CustomControls;

namespace DustInTheWind.ConsoleTools.Tutorial.Commands
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
            Int32View idView = new Int32View("The id of the person:");

            int id = idView.Read();
            CustomConsole.WriteLine();

            return id;
        }
    }
}