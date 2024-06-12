using System;
using WorkinghoursManagement.Domain.Core.Entities;

namespace WorkinghoursManagement.Domain.Entities
{
    public class WorkingHoursByUser : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime RegisteredDateTime { get; set; }

        /// <summary>
        /// this could be IN or OUT
        /// </summary>
        public string RegisterType { get; set; }

        public string Comment { get; set; }
    }
}