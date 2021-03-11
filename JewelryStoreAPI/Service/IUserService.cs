using JewelryStoreAPI.Entity;

namespace JewelryStoreAPI.Service
{
    public interface IUserService
    {
        User GetById(int id);
        User Authenticate(string username, string password);
    }
}
