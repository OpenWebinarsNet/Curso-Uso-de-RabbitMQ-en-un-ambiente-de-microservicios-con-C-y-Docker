using AutoMapper;
using WorkinghoursManagement.Domain.Entities;
using WorkinghoursManagement.Domain.Models;

namespace WorkinghoursManagement.Application.Mapping
{
    public class UsersMap : Profile
    {
        public UsersMap()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserInputModel, User>();
            CreateMap<UserModel, User>();
        }
    }
}