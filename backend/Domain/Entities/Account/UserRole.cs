namespace Domain.Entities;

/// <summary>
/// Link between user and access role
/// </summary>
public partial class UserRole
{
    /// <summary>
    /// Id of link in database
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of access role
    /// </summary>
    public long IdRole { get; set; }

    /// <summary>
    /// Id of user
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// Navigation to access role
    /// </summary>
    public virtual Role IdRoleNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to user
    /// </summary>
    public virtual User IdUserNavigation { get; set; } = null!;
}

