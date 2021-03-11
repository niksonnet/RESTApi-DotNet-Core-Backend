using JewelryStoreAPI.Entity;
using JewelryStoreAPI.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreAPI.Service
{
    public class UserService : IUserService
    {
        private UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }
        public User GetById(int id)
        {
            //return _context.Users.Find(id);
            return _context.Users
                        .Include(x => x.Discount)
                        .Where(x => x.Id == id)
                        .SingleOrDefault();
        }
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            //var user = _context.Users.SingleOrDefault(x => x.Username == username);

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

            // authentication successful
            return user;
        }

    }
}
