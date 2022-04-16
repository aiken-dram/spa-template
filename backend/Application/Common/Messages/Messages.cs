namespace Application.Common;

/// <summary>
/// Static strings for application
/// </summary>
public static class Messages
{
    #region SHARED
    public static string MaximumLength(int length) => $"Maximum allowed lenght is {length} characters";
    public static string MustNotBeEmpty(string field) => $"{field} must not be empty";
    #endregion

    #region ACCOUNT
    /* probably shouldnt've passed html from api, better to use some json with types and parameters and make html on frontend side */
    public static string UserPasswordUpdated(string login) => $"Successfully updated password for user <code>{login}</code>";
    public static string FoundMultipleUsersWithLogin(string login, int cnt) => $"Found {cnt} users with login <code>{login}</code>";
    public static string InvalidFormatLine(string line) => $"Invalid line format: <code>{line}</code>";
    public const string NotMatchingUserId = "Id of provided user does not match to authorized one";
    public const string UserWithProvidedLoginAlreadyExists = "User with provided login already exists";
    public const string Login = "Login";
    public const string Password = "Password";
    public const string UserName = "User name";
    public static string UserTableFileName(string fname) => $"User table {fname}.csv";
    #endregion

    #region COMMON
    public const string LogRequest = "SPA Request: {0} {1} {2}";
    public const string LongRunningRequest = "Overpay Long Running Request: {0} ({1} milliseconds) {2} {3}";
    #endregion

    #region DICTIONARY
    public static string DictionaryNotFound(string dict) => $"Could not find dictinary with requested name '{dict}'.";
    #endregion

    #region QUERY
    public const string NoAccessToDeleteRequest = "User does not have access to delete this request";
    #endregion
}
