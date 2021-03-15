using API.Domain.Entity;
using API.Infrastructure.DbContext;
using API.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

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
                    new User
                    {
                        Id = 1,
                        FirstName = "Normal",
                        LastName = "User",
                        Username = "Regular",
                        Role= "Regular",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Discount = new Discount
                        {
                            Id = 1,
                            UserID = 1,
                            Percentage = 0
                        }
                    },
                    new User
                    {
                        Id = 2,
                        FirstName = "Privileged",
                        LastName = "User",
                        Username = "Privileged",
                        Role= "Privileged",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Discount = new Discount
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
