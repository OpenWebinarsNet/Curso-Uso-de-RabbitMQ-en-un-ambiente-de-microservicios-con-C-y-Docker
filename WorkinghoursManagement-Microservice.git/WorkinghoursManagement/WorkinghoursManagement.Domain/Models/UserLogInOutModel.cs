using System;

namespace WorkinghoursManagement.Domain.Models
{
    public class UserLogInOutModel : UserModel
    {
        public DateTime RegisteredDateTime { get; set; }

        /// <summary>
        /// this could be IN or OUT
        /// </summary>
        public string RegisterType { get; set; }
    }
}