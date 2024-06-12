using System;

namespace WorkinghoursManagement.Domain.Models
{
    public class NotificationSentInputModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid NotificationTypeId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime NotificationDatetime { get; set; }
    }
}