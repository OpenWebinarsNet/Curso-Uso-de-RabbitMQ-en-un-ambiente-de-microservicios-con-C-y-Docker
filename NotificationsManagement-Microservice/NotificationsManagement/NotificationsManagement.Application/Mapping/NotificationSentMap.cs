using AutoMapper;
using NotificationsManagement.Domain.Entities;
using NotificationsManagement.Domain.Models;

namespace NotificationsManagement.Application.Mapping
{
    public class NotificationSentMap : Profile
    {
        public NotificationSentMap()
        {
            CreateMap<NotificationSent, NotificationSentModel>();
            CreateMap<NotificationSentInputModel, NotificationSent>();
            CreateMap<NotificationSentModel, NotificationSent>();
        }
    }
}