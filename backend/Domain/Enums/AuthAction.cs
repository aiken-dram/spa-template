namespace Domain.Enums;

/// <summary>
/// Access actions in application
/// </summary>
public static class eAuthAction
{
    /// <summary>
    /// User entered wrong password
    /// </summary>
    public const string WrongPassword = "WRONGPASS";

    /// <summary>
    /// User's password was expired
    /// </summary>
    public const string Expired = "EXPIRED";

    /// <summary>
    /// User successfully authenticated
    /// </summary>
    public const string Login = "LOGIN";

    /// <summary>
    /// User was locked
    /// </summary>
    public const string Lock = "LOCK";
}
