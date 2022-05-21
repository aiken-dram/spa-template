namespace Domain.Entities;

/// <summary>
/// Audit data type dictionary
/// </summary>
public partial class AuditDataType
{
    public AuditDataType()
    {
        UserAuditData = new HashSet<UserAuditData>();
        SampleAuditData = new HashSet<SampleAuditData>();
    }

    /// <summary>
    /// Id of type in database
    /// </summary>
    public int IdType { get; set; }

    /// <summary>
    /// Name of type
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Description of type
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of user audit data with this type
    /// </summary>
    public virtual ICollection<UserAuditData> UserAuditData { get; set; }

#warning SAMPLE
    /// <summary>
    /// Collection of sample audit data with this type
    /// </summary>
    public virtual ICollection<SampleAuditData> SampleAuditData { get; set; }
}