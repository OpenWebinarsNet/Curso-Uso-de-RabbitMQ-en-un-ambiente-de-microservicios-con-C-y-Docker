using NotificationsManagement.Domain.Core.Models;
using System;

namespace NotificationsManagement.Domain.Models
{
    public class UserModel : BaseModel
    {
        public DateTime CreationDatetime { get; set; }
        public bool IsActive { get; set; }
    }
}
