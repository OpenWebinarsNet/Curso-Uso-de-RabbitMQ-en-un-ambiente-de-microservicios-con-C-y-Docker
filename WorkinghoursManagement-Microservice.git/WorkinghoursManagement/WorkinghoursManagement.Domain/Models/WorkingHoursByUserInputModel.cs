using System;

namespace WorkinghoursManagement.Domain.Models
{
    public class WorkingHoursByUserInputModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime RegisteredDateTime { get; set; }

        /// <summary>
        /// this could be IN or OUT
        /// </summary>
        public string RegisterType { get; set; }
    }
}