namespace Domain.Entities;

/// <summary>
/// Link between access group and access role
/// </summary>
public partial class GroupRole
{
    /// <summary>
    /// Id of link in database
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of access group
    /// </summary>
    public long IdGroup { get; set; }

    /// <summary>
    /// Id of access role
    /// </summary>
    public long IdRole { get; set; }

    /// <summary>
    /// Navigation to access group
    /// </summary>
    public virtual Group IdGroupNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to access role
    /// </summary>
    public virtual Role IdRoleNavigation { get; set; } = null!;
}

