using System;
using WorkinghoursManagement.Domain.Core.Models;

namespace WorkinghoursManagement.Domain.Models
{
    public class UserModel : BaseModel
    {
        public DateTime CreationDatetime { get; set; }
        public bool IsActive { get; set; }
    }
}