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
        public static Boolean isValidRegistrationToken(Token token)
        {
            var timeElapsed = DateTime.Now.Subtract(token.Timestamp);
            return timeElapsed < RegistrationTokenValidityDuration;
        }
        public static readonly string DefaultRedirectUrl = "/Index";
        public const int DefaultRedirectInSeconds = 5;
    }
}
