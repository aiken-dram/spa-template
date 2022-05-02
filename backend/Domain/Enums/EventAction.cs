using Shared.Domain.Attributes;

namespace Domain.Enums;

/// <summary>
/// Event actions in application
/// </summary>
public enum eEventAction : int
{
    /// <summary>
    /// Create entity
    /// </summary>
    [Dictionary("CREATE")]
    Create = 1,

    /// <summary>
    /// Edit entity
    /// </summary>
    [Dictionary("EDIT")]
    Edit = 2,

    /// <summary>
    /// Delete entity
    /// </summary>
    [Dictionary("DELETE")]
    Delete = 3,
}

/// <summary>
/// Auth event action in application
/// </summary>
public enum eAuthEventAction : int
{
    /// <summary>
    /// User successfully authenticated
    /// </summary>
    [Dictionary("AUTH_LOGIN")]
    Login = 4,

    /// <summary>
    /// User entered wrong password
    /// </summary>
    [Dictionary("AUTH_WRONGPASS")]
    WrongPassword = 5,

    /// <summary>
    /// User's password was expired
    /// </summary>
    [Dictionary("AUTH_EXPIRED")]
    Expired = 6,

    /// <summary>
    /// User was locked
    /// </summary>
    [Dictionary("AUTH_LOCK")]
    Lock = 7,
}

/// <summary>
/// User event action in application
/// </summary>
public enum eUserEventAction : int
{
    /// <summary>
    /// Password updated from file
    /// </summary>
    [Dictionary("USER_UPDATE_PASS")]
    UpdatePassword = 8
}