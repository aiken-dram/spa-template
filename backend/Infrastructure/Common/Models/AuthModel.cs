namespace Infrastructure.Common.Models;

/// <summary>
/// Authorization request
/// </summary>
public class AuthRequest
{
    /// <summary>
    /// User login
    /// </summary>
    public string Login { get; set; } = null!;

    /// <summary>
    /// User password
    /// </summary>
    public string Password { get; set; } = null!;
}

/// <summary>
/// User information
/// </summary>
public class AuthUserVm
{
    /// <summary>
    /// Id of user in database
    /// </summary>
    /// <example>1</example>
    public long UserID { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    /// <example>Admin</example>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Array of group names for user
    /// </summary>
    public string?[] UserGroups { get; set; } = null!;

    /// <summary>
    /// Array of modules for user
    /// </summary>
    public string[] UserModules { get; set; } = null!;

    /// <summary>
    /// Array of districts for user
    /// </summary>
    public int[]? UserDistricts { get; set; }
}

/// <summary>
/// Authorization response
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// User information
    /// </summary>
    public AuthUserVm User { get; set; } = null!;

    /// <summary>
    /// JWT token
    /// </summary>
    public string Token { get; set; } = null!;
}
