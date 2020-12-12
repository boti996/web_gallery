using System;
using web_gallery.Models.Users;

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
    }

    public static class Preferences
    {
        private static readonly TimeSpan RegistrationTokenValidityDuration = TimeSpan.FromHours(168);
        public static bool isValidRegistrationToken(Token token)
        {
            if (!token.IsValid)
            {
                return false;
            }
            var timeElapsed = DateTime.Now.Subtract(token.Timestamp);
            if (timeElapsed >= RegistrationTokenValidityDuration)
            {
                return false;
            }
            return true;
        }
        public static readonly string DefaultRedirectUrl = "/Index";
        public const int DefaultRedirectInSeconds = 5;
    }
}
