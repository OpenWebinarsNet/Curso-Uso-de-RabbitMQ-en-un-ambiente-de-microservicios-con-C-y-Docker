namespace WorkinghoursManagement.Util
{
    public static class Constants
    {
        public static class Routes
        {
            public const string WorkingHours = "api/v1/WorkingHours";
            public const string Users = "api/v1/Users";
        }

        public static class ReturnMessages
        {
            public const string NotFound = "No record found";
            public const string NullOrEmptyData = "Null or empty data received";
            public const string InvalidData = "Invalid data received";
            public const string InvalidDataInOut = "Invalid data received. Accepted values are IN or OUT";
            public const string InvalidEndDateTimeData = "Invalid end date time data received. End datetime data must be higher than start datetime data";
        }

        public static class RabbitMQConstants
        {
            public const string UsersExchange = "openwebinars.usersmanagement";
            public const string UserOperationsCreateQueue = $"users.operations.create.{nameof(WorkinghoursManagement)}";
            public const string UserOperationsUpdateQueue = $"users.operations.update.{nameof(WorkinghoursManagement)}";
            public const string UserOperationsDeleteQueue = $"users.operations.delete.{nameof(WorkinghoursManagement)}";
            public const string UserOperationsLogInOutQueue = "users.operations.log_in_out";

            public const string RoutingKey_CreateUser = "CREATE_USER";
            public const string RoutingKey_UpdateUser = "UPDATE_USER";
            public const string RoutingKey_DeleteUser = "DELETE_USER";
            public const string RoutingKey_UserLogInOut = "LOG_IN_OUT_USER";
            public const string RoutingKey_UserNotification = "USER_NOTIFICATION";
        }

        public static class RegisterConstants
        {
            public const string RegisterType_In = "IN";
            public const string RegisterType_Out = "OUT";
        }

        public static class NotificationConstants
        {
            public const string Congrats = "DCE80C72-6F19-49D2-8034-2501DFA08CC7";
            public const string Warning = "2E09803F-2D04-4A1B-8CE2-527958B17E59";
        }
    }
}