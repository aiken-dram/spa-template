namespace Infrastructure.Common
{
    public static class Messages
    {
        #region IDENTITY
        public const string UserWithProvidedLoginNotFound = "No user with provided login.";
        public const string UserLocked = "User has been locked.";
        public const string PasswordExpired = "User's password was expired";
        public static string UserLockTimeout(TimeSpan ts) => $"User's temporarily locked. Try again in {ts.Minutes} min {ts.Seconds} sec";
        public static string WrongPassLock(int cnt) => $"Amount of failed login attempts exceeded {cnt}. User has been locked.";
        public static string WrongPassTimeout(int cnt) => $"Wrong password. Amount of failed login attempts exceeded {cnt}, user has been temporarily locked";
        public const string WrongPass = "Wrong password";
        public const string UserIdMustNotBeEmpty = "User id must not be empty.";
        public const string UserNotFound = "User was not found.";
        public const string UserNotActive = "User is not active.";
        #endregion

        #region MESSAGE QUERY
        public const string AuditSourceUser = "User";
        public const string AuditSourceSample = "Sample";
        #endregion
    }
}