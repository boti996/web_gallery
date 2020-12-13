namespace web_gallery
{
    public static class PolicyNames
    {
        public const string RequireAdminRole = "RequireAdminRole";
    }

    public static class WarningMessages
    {
        public const string Default = "Something went wrong!";
        public const string RegistrationTokenInvalid =
            @"Missing or invalid registration token.
            Please ask for an invitation email from an Administrator!";
        public const string UserWasBannedSuccessfully = "User was banned successfully!";
        public const string InvalidValueSelected = "No valid value was selected!";
        public const string InvalidLoginAttempt = "Invalid login attempt!";
        public const string SuspendedAccount = "Account has been suspended!";
        public static string SuspendedDaysLeft(uint daysLeft) 
            => $@"Days left from ban: {(
                (daysLeft == uint.MaxValue)
                ? "forever"
                : daysLeft.ToString()
            )}.";
}