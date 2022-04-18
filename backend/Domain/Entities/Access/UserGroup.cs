namespace Domain.Entities;

/// <summary>
/// Link between user and access group
/// </summary>
public partial class UserGroup
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
    /// Id of access user
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// Navigation to access group
    /// </summary>
    public virtual Group IdGroupNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to user
    /// </summary>
    public virtual User IdUserNavigation { get; set; } = null!;
}

