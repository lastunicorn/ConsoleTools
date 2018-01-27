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

                dataGrid.Display();
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