using NotificationsManagement.Domain.Core.Entities;
using System;

namespace NotificationsManagement.Domain.Entities
{
    public class NotificationSent : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid NotificationTypeId { get; set; }
        public virtual NotificationType NotificationType { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime NotificationDatetime { get; set; }
    }
}