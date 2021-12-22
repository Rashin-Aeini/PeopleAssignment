using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PeopleAssignment.Models.Data
{
    internal class DbInitializer
    {
        internal static void Initialize(
            PeopleContext context, 
            UserManager<User> manager
            )
        {
            context.Database.Migrate();
            

            if (!context.Countries.Any())
            {
                Country sweden = new Country()
                {
                    Name = "Sweden",
                    Cities = new List<City>()
                };

                City gothenburg = new City()
                {
                    Name = "Gothenburg",
                    People = new List<Person>()
                };

                Person aeini = new Person()
                {
                    Name = "Rashin Aeini",
                    Phone = "123456789"
                };

                gothenburg.People.Add(aeini);

                sweden.Cities.Add(gothenburg);

                context.Countries.Add(sweden);

                context.SaveChanges();

            }

            User super = null;

            if (!context.Users.Any())
            {
                super = new User()
                {
                    FirstName = "Rashin",
                    LastName = "Aeini",
                    UserName = "rashin",
                    NormalizedUserName = "RASHIN",
                    Birthdate = DateTime.Now
                };
                
                PasswordHasher<User> hasher = new PasswordHasher<User>();

                super.PasswordHash = hasher.HashPassword(super, "123456789");

                context.Users.Add(super);

                context.SaveChanges();
            }

            IdentityRole admin = null;

            if (!context.Roles.Any())
            {
                IdentityRole visitor = new IdentityRole("visitor")
                {
                    NormalizedName = "VISITOR"
                };
                context.Roles.Add(visitor);

                admin = new IdentityRole("admin")
                {
                    NormalizedName = "ADMIN"
                };
                context.Roles.Add(admin);

                context.SaveChanges();
            }

            if (!context.UserRoles.Any())
            {

                if (super == null)
                {
                    super = context.Users
                        .SingleOrDefault(user => user.UserName.Equals("rashin"));
                }

                if (admin == null)
                {
                    admin = context.Roles
                        .SingleOrDefault(role => role.Name.Equals("admin"));
                }

                if (super != null && admin != null)
                {
                    IdentityUserRole<string> superToAdmin = new IdentityUserRole<string>
                    {
                        UserId = super.Id,
                        RoleId = admin.Id
                    };

                    context.UserRoles.Add(superToAdmin);

                    context.SaveChanges();
                }

                
            }
        }
    }
}
