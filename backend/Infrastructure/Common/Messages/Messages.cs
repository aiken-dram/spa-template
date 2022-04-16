namespace Infrastructure.Common;

public static class Messages
{
    #region IDENTITY
    public const string UserWithProvidedLoginNotFound = "Provided login does not exist.";
    public const string UserLocked = "User has been locked.";
    public const string PasswordExpired = "User's password has expired";
    public static string UserLockTimeout(TimeSpan ts) => $"User was temporary locked. Try again in {ts.Minutes} minutes {ts.Seconds} seconds";
    public static string WrongPassLock(int cnt) => $"Count of failed authorizations exceeded {cnt} attempts. User has been locked.";
    public static string WrongPassTimeout(int cnt) => $"Wrong password. Count of failed authorizations exceeded {cnt}, user has been temporary locked";
    public const string WrongPass = "Wrong password";
    public const string UserIdMustNotBeEmpty = "User's identity cannot be null.";
    public const string UserNotFound = "User was not found.";
    public const string UserNotActive = "User is not active.";
    #endregion
}