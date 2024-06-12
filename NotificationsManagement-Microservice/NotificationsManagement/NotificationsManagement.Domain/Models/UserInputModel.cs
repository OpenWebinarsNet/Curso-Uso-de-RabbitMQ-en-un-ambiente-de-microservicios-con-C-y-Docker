using System;

namespace NotificationsManagement.Domain.Models
{
    public class UserInputModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDatetime { get; set; }
        public bool IsActive { get; set; }
    }
}
