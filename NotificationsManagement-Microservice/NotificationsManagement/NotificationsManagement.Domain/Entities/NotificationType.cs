using NotificationsManagement.Domain.Core.Entities;
using System.Collections.Generic;

namespace NotificationsManagement.Domain.Entities
{
    public class NotificationType : BaseEntity
    {
        public string NotificationTypeName { get; set; }
        public virtual ICollection<NotificationSent> NotificationsSent { get; set; }

    }
}