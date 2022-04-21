namespace Domain.Entities;

/// <summary>
/// Access role
/// </summary>
public partial class Role
{
    public Role()
    {
        GroupRoles = new HashSet<GroupRole>();
        RoleModules = new HashSet<RoleModule>();
        UserRoles = new HashSet<UserRole>();
    }

    /// <summary>
    /// Id of access role in database
    /// </summary>
    public long IdRole { get; set; }

    /// <summary>
    /// Name of role
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description of role
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of role groups
    /// </summary>
    public virtual ICollection<GroupRole> GroupRoles { get; set; }

    /// <summary>
    /// Collection of role modules
    /// </summary>
    public virtual ICollection<RoleModule> RoleModules { get; set; }

    /// <summary>
    /// Collection of role users
    /// </summary>
    public virtual ICollection<UserRole> UserRoles { get; set; }
}

