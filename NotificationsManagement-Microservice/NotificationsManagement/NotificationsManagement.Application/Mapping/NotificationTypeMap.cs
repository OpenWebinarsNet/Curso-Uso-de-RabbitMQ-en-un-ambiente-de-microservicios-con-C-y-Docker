using AutoMapper;
using NotificationsManagement.Domain.Entities;
using NotificationsManagement.Domain.Models;

namespace NotificationsManagement.Application.Mapping
{
    public class NotificationTypeMap : Profile
    {
        public NotificationTypeMap()
        {
            CreateMap<NotificationType, NotificationTypeModel>();
            CreateMap<NotificationTypeInputModel, NotificationType>();
            CreateMap<NotificationTypeModel, NotificationType>();
        }
    }
}