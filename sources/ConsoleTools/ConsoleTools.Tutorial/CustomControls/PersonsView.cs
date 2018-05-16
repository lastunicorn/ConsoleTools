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

using System.Collections.Generic;
using DustIntheWind.ConsoleTools.Tutorial.AddressBookModel;
using DustInTheWind.ConsoleTools.TabularData;

namespace DustIntheWind.ConsoleTools.Tutorial.CustomControls
{
    internal class PersonsView
    {
        private readonly DataGrid dataGrid;

        private IEnumerable<Person> persons;

        public IEnumerable<Person> Persons
        {
            get { return persons; }
            set
            {
                persons = value;

                dataGrid.Rows.Clear();

                foreach (Person person in value)
                    dataGrid.Rows.Add(person.Id, person.FullName, person.PhoneNumber);
            }
        }

        public PersonsView()
        {
            dataGrid = new DataGrid("Persons");

            dataGrid.Columns.Add("Id");
            dataGrid.Columns.Add("Name");
            dataGrid.Columns.Add("Phone Number");
        }

        public void Display()
        {
            dataGrid.Display();
        }
    }
}