using System;
using System.Collections.Generic;
using WorkinghoursManagement.Domain.Core.Entities;

namespace WorkinghoursManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public DateTime CreationDatetime { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<WorkingHoursByUser> WorkingHoursByUser { get; set; }
    }
}