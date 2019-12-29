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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.ConsoleTools.Tutorial.AddressBookModel
{
    internal class AddressBook : IEnumerable<Person>
    {
        private int lastId = -1;
        private readonly List<Person> persons = new List<Person>();

        public void AddPerson(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));

            bool alreadyExists = persons.Any(x => ReferenceEquals(x, person));

            if (alreadyExists)
                return;

            lastId++;
            person.Id = lastId;
            persons.Add(person);
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return persons.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Person GetPerson(int id)
        {
            return persons.FirstOrDefault(x => x.Id == id);
        }
    }
}