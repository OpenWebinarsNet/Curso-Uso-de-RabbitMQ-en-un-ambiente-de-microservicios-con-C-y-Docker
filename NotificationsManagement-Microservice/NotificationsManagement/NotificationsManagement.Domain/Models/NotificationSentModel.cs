using NotificationsManagement.Domain.Core.Models;
using NotificationsManagement.Domain.Entities;
using System;

namespace NotificationsManagement.Domain.Models
{
    public class NotificationSentModel : BaseModel
    {
        public Guid UserId { get; set; }        
        public Guid NotificationTypeId { get; set; }        
        public string NotificationMessage { get; set; }
        public DateTime NotificationDatetime { get; set; }
    }
}
