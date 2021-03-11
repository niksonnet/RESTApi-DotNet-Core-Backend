using JewelryStoreAPI.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreAPI.Helper
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                string defaultPassword = "Test@123";

                byte[] passwordHash, passwordSalt;
                UtilityService.CreatePasswordHash(defaultPassword, out passwordHash, out passwordSalt);

                context.Users.AddRange(
                    new Entity.User
                    {
                        Id = 1,
                        FirstName = "Normal",
                        LastName = "User",
                        Username = "Regular",
                        Role= "Regular",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Discount = new Entity.Discount
                        {
                            Id = 1,
                            UserID = 1,
                            Percentage = 0
                        }
                    },
                    new Entity.User
                    {
                        Id = 2,
                        FirstName = "Privileged",
                        LastName = "User",
                        Username = "Privileged",
                        Role= "Privileged",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Discount = new Entity.Discount
                        {
                            Id = 2,
                            UserID = 2,
                            Percentage = 2
                        }
                    });

                context.SaveChanges();
            }
        }

    }
}
