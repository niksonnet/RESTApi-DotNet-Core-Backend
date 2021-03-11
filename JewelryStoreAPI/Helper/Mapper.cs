using AutoMapper;
using JewelryStoreAPI.DTO;
using JewelryStoreAPI.Entity;

namespace JewelryStoreAPI.Helper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<Discount, DiscountModel>();
            CreateMap<DiscountModel, Discount>();
        }
    }
}
