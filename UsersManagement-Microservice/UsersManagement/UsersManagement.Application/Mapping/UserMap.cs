using AutoMapper;
using UsersManagement.Domain.Entities;
using UsersManagement.Domain.Models;

namespace UsersManagement.Application.Mapping
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserInputModel, User>();
            CreateMap<UserModel, User>();
            CreateMap<User, UserLogInOutModel>();
            CreateMap<UserLoginInputModel, User>();
        }
    }
}