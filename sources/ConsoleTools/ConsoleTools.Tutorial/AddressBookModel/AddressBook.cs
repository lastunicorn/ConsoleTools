using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DustIntheWind.ConsoleTools.Tutorial.AddressBookModel
{
    internal class AddressBook : IEnumerable<Person>
    {
        private readonly List<Person> persons = new List<Person>();

        public void AddPerson(Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));

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