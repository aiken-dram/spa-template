namespace Domain.Entities;

/// <summary>
/// Access group
/// </summary>
[DisplayName("Group")]
public partial class Group
{
    public Group()
    {
        GroupRoles = new HashSet<GroupRole>();
        UserGroups = new HashSet<UserGroup>();
    }

    /// <summary>
    /// Id of group in database
    /// </summary>
    public long IdGroup { get; set; }

    /// <summary>
    /// Group name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Group description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of group roles
    /// </summary>
    public virtual ICollection<GroupRole> GroupRoles { get; set; }

    /// <summary>
    /// Collection of group users
    /// </summary>
    public virtual ICollection<UserGroup> UserGroups { get; set; }
}
