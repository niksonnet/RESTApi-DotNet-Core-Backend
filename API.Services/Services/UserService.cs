using API.Domain.Entity;
using API.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            this._context = context;
        }
        public User GetById(int id)
        {
            return _context.Users
                        .Include(x => x.Discount)
                        .Where(x => x.Id == id)
                        .SingleOrDefault();
        }
        public User GetByName(string username)
        {
            return _context.Users
                        .Include(x => x.Discount)
                        .Where(x => x.Username == username)
                        .SingleOrDefault();
        }
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users
                        .Include(x => x.Discount)
                        .Where(x => x.Username == username)
                        .SingleOrDefault();

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!UtilityService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public decimal CalculateFinalAmount(decimal Rate, decimal Weight, decimal Discount)
        {
            return Discount > 0 ? (Rate * Weight) * Discount / 100 : Rate * Weight;
        }
        
        public bool SaveUsers(List<User> Users)
        {
            if (Users.Count == 0 || !Users.Any() )
                throw new ArgumentNullException("Users List Can not be Empty/Null");
            
            _context.Users.AddRange(Users);

            return true;
        }
    }
}
