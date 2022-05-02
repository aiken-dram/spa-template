using Shared.Domain.Attributes;

namespace Domain.Enums;

/// <summary>
/// Event targets in application
/// </summary>
public enum eEventTarget : int
{
    /// <summary>
    /// Domain/Account/User entity
    /// </summary>
    [Dictionary("AUTH")]
    Auth = 1,

    /// <summary>
    /// Domain/Account/User entity
    /// </summary>
    [Dictionary("ACCOUNT.USERS")]
    AccountUser = 2,

    /// <summary>
    /// Domain/Account/User entity
    /// </summary>
    [Dictionary("MQ.REQUESTS")]
    MessageQueryRequest = 3,

    /// <summary>
    /// Domain/Account/User entity
    /// </summary>
    [Dictionary("DICT.DISTRICTS")]
    DictionaryDistrict = 4,
}