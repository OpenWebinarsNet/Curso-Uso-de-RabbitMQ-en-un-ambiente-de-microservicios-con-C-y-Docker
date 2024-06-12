namespace NotificationsManagement.UnitTests
{
    public static class Constants
    {
        public static class Routes
        {
            public const string Users = "api/v1/Users";
        }

        public static class ReturnMessages
        {
            public const string NotFound = "No record found";
            public const string NullOrEmptyData = "Null or empty data received";
            public const string InvalidData = "Invalid data received";
            public const string InvalidEndDateTimeData = "Invalid end date time data received. End datetime data must be higher than start datetime data";
        }
    }
}
