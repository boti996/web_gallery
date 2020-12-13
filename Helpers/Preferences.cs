using System;
using web_gallery.Models.Users;

namespace web_gallery
{
    public static class Preferences
    {
        public const uint SuspendDays = 30;
        public static DateTime getSuspendDate(bool suspendOnly)
        {
            return (
                suspendOnly
                ? DateTime.Now + TimeSpan.FromDays(SuspendDays)
                : DateTime.MaxValue
            );
        }
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