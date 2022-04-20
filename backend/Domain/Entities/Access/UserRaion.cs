namespace Domain.Entities;

/// <summary>
/// Link between user and raion
/// </summary>
public partial class UserRaion
{
    /// <summary>
    /// Id of link in database
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of access user
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// Id of raion
    /// </summary>
    public int IdRaion { get; set; }

    /// <summary>
    /// Navigation to user
    /// </summary>
    public virtual User IdUserNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to raion
    /// </summary>
    /// <value></value>
    public virtual Raion IdRaionNavigation { get; set; } = null!;
}

