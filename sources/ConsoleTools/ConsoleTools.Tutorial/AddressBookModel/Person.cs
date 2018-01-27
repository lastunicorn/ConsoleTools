using System;
using System.Collections.Generic;
using System.Text;

namespace DustIntheWind.ConsoleTools.Tutorial.AddressBookModel
{
    internal class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public float Height { get; set; }
        public List<string> PreferedBeverages { get; set; }

        public string FullName
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrEmpty(FirstName))
                    sb.Append(FirstName);

                if (!string.IsNullOrEmpty(LastName))
                {
                    if (sb.Length > 0)
                        sb.Append(" ");

                    sb.Append(LastName);
                }

                return sb.ToString();
            }
        }
    }
}