using Microsoft.EntityFrameworkCore;

namespace PeopleAssignment.Models.Data
{
    public class PeopleContext : DbContext
    {
        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}
