using API.Domain.Entity;
using System.Collections.Generic;

namespace API.Services.Services
{
    public interface IUserService
    {
        User GetById(int id);
        User GetByName(string username);
        User Authenticate(string username, string password);
        decimal CalculateFinalAmount(decimal Rate, decimal Weight, decimal Discount);
        bool SaveUsers(List<User> Users);
    }
}
