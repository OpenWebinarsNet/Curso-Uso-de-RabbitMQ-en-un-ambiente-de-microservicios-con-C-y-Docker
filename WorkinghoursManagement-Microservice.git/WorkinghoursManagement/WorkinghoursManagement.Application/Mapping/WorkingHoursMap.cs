using AutoMapper;
using WorkinghoursManagement.Domain.Entities;
using WorkinghoursManagement.Domain.Models;

namespace WorkinghoursManagement.Application.Mapping
{
    public class WorkingHoursMap : Profile
    {
        public WorkingHoursMap()
        {
            CreateMap<WorkingHoursByUser, WorkingHoursByUserModel>();
            CreateMap<WorkingHoursByUserInputModel, WorkingHoursByUser>();
            CreateMap<WorkingHoursByUserModel, WorkingHoursByUser>();
        }
    }
}