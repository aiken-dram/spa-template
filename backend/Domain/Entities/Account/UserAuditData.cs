using Shared.Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Data for user audit
/// </summary>
public partial class UserAuditData : IAuditData
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public UserAuditData()
    {

    }

    /// <summary>
    /// Id of audit data
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of user audit
    /// </summary>
    public long IdAudit { get; set; }

    /// <summary>
    /// Id of data type
    /// </summary>
    public int IdType { get; set; }

    /// <summary>
    /// Json with data
    /// </summary>
    public string? Json { get; set; }

    /// <summary>
    /// Navigation to user audit
    /// </summary>
    public virtual UserAudit IdAuditNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to data type
    /// </summary>
    public virtual AuditDataType IdTypeNavigation { get; set; } = null!;
    #endregion

    /// <summary>
    /// Constructor from AuditData
    /// </summary>
    /// <param name="data">AuditData</param>
    public UserAuditData(IAuditData data)
    {
        IdType = data.IdType;
        Json = data.Json;
    }
}