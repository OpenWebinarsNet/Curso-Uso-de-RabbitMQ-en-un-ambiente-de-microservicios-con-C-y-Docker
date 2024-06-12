using NotificationsManagement.Domain.Core.Entities;
using System.Collections.Generic;
using System;

namespace NotificationsManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public DateTime CreationDatetime { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<NotificationSent> NotificationsSent { get; set; }
    }
}
