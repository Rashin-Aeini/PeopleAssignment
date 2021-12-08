using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PeopleAssignment.Models.Data
{
    internal class DbInitializer
    {
        internal static void Initialize(PeopleContext context)
        {
            context.Database.Migrate();

            if (!context.Persons.Any())
            {
                Person aieni = new Person()
                {
                    Name = "Rashin Aeini",
                    City = "Vaxjo",
                    Phone = "123456789"
                };

                context.Persons.Add(aieni);

                context.SaveChanges();
            }
        }
    }
}
