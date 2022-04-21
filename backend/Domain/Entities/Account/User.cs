namespace Domain.Entities;

/// <summary>
/// Application user
/// </summary>
public partial class User
{
    public User()
    {
        UserAuths = new HashSet<UserAuth>();
        UserGroups = new HashSet<UserGroup>();
        UserRoles = new HashSet<UserRole>();
        Requests = new HashSet<Request>();
    }

    /// <summary>
    /// Id of user in database
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// User login
    /// </summary>
    public string Login { get; set; } = null!;

    /// <summary>
    /// Hash of user password
    /// </summary>
    public string Pass { get; set; } = null!;

    /// <summary>
    /// Is user active:
    /// T - true
    /// F - false
    /// </summary>
    public string IsActive { get; set; } = null!;

    /// <summary>
    /// Expire date of the password
    /// </summary>
    public DateTime? PassDate { get; set; }

    /// <summary>
    /// User name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// User description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of user authorizations
    /// </summary>
    public virtual ICollection<UserAuth> UserAuths { get; set; }

    /// <summary>
    /// Collection of user groups
    /// </summary>
    public virtual ICollection<UserGroup> UserGroups { get; set; }

    /// <summary>
    /// Collection of user roles
    /// </summary>
    public virtual ICollection<UserRole> UserRoles { get; set; }

    /// <summary>
    /// Collection of requests from user
    /// </summary>
    public virtual ICollection<Request> Requests { get; set; }
}
