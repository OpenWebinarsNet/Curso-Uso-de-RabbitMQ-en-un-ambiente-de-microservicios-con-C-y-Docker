using System;
using WorkinghoursManagement.Domain.Core.Models;

namespace WorkinghoursManagement.Domain.Models
{
    public class WorkingHoursByUserModel : BaseModel
    {
        public Guid UserId { get; set; }
        public DateTime RegisteredDateTime { get; set; }

        /// <summary>
        /// this could be IN or OUT
        /// </summary>
        public string RegisterType { get; set; }
    }
}