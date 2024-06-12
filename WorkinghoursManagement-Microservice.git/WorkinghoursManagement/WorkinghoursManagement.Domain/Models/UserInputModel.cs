using System;

namespace WorkinghoursManagement.Domain.Models
{
    public class UserInputModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDatetime { get; set; }
        public bool IsActive { get; set; }
    }
}