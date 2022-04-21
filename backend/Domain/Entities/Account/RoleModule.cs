namespace Domain.Entities;

/// <summary>
/// Link between application access module and access role
/// </summary>
public partial class RoleModule
{
    /// <summary>
    /// Id of link in database
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of access module
    /// </summary>
    public long IdModule { get; set; }

    /// <summary>
    /// Id of access role
    /// </summary>
    public long IdRole { get; set; }

    /// <summary>
    /// Navigation to access module
    /// </summary>
    public virtual Module IdModuleNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to access role
    /// </summary>
    public virtual Role IdRoleNavigation { get; set; } = null!;
}
