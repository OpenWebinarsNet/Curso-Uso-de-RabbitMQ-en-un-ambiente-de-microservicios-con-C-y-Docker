namespace UsersManagement.Util
{
    public static class Constants
    {
        public static class Routes
        {
            public const string Users = "api/v1/Users";
        }

        public static class RabbitMQConstants
        {
            public const string UsersExchange = "openwebinars.usersmanagement";
            public const string RoutingKey_CreateUser = "CREATE_USER";
            public const string RoutingKey_UpdateUser = "UPDATE_USER";
            public const string RoutingKey_DeleteUser = "DELETE_USER";
            public const string RoutingKey_UserLogInOut = "LOG_IN_OUT_USER";
        }

        public static class ReturnMessages
        {
            public const string NotFound = "No record found";
            public const string NullOrEmptyData = "Null or empty data received";
            public const string InvalidData = "Invalid data received";
            public const string InvalidEndDateTimeData = "Invalid end date time data received. End datetime data must be higher than start datetime data";
            public const string InvalidLogin = "Invalid login data received. Credentials not found";
        }

        public static class RegisterConstants
        {
            public const string RegisterType_In = "IN";
            public const string RegisterType_Out = "OUT";
        }
    }
}