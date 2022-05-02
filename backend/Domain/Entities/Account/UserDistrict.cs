namespace Domain.Entities;

/// <summary>
/// Link between user and district
/// </summary>
public partial class UserDistrict
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
    /// Id of district
    /// </summary>
    public int IdDistrict { get; set; }

    /// <summary>
    /// Navigation to user
    /// </summary>
    public virtual User IdUserNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to district
    /// </summary>
    /// <value></value>
    public virtual District IdDistrictNavigation { get; set; } = null!;
}

