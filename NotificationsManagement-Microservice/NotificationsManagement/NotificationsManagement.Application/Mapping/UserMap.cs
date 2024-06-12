using AutoMapper;
using NotificationsManagement.Domain.Entities;
using NotificationsManagement.Domain.Models;

namespace NotificationsManagement.Application.Mapping
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserInputModel, User>();
            CreateMap<UserModel, User>();
        }
    }
}