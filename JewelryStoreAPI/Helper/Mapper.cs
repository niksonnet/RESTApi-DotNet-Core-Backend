using API.Domain.Entity;
using AutoMapper;
using JewelryStoreAPI.DTO;

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
