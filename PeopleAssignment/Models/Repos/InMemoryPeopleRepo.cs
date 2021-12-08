using System.Collections.Generic;

namespace PeopleAssignment.Models.Repos
{
    public class InMemoryPeopleRepo : IPeopleRepo
    {
        private static List<Person> people = new List<Person>();
        private static int idCounter = 0;

        public Person Create(Person entry)
        {
            idCounter++;
            entry.Id = idCounter;
            people.Add(entry);
            return entry;
        }

        public bool Delete(Person person)
        {
            if (person != null)
            {
                person = Read(person.Id);
                if (person != null)
                {
                    people.Remove(person);
                    return true;
                }

            }

            return false;
        }

        public List<Person> Read()
        {
            return people;
        }

        public Person Read(int id)
        {
            Person entry = null;

            foreach (Person item in people)
            {
                if (item.Id == id)
                {
                    entry = item;
                    break;
                }
            }

            return entry;
        }

        public bool Update(Person person)
        {
            Person orignial = Read(person.Id);

            if (orignial != null)
            {
                orignial.Name = person.Name;
                orignial.City = person.City;
                orignial.Phone = person.Phone;
            }

            return orignial != null;
        }
    }
}
