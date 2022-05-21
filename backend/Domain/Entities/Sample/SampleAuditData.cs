using Shared.Domain.Interfaces;

namespace Domain.Entities;

#warning This is example, remove entire file in actual application
/// <summary>
/// Data for sample audit
/// </summary>
public partial class SampleAuditData : IAuditData
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public SampleAuditData()
    {

    }

    /// <summary>
    /// Id of audit data
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of sample audit
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
    /// Navigation to sample audit
    /// </summary>
    public virtual SampleAudit IdAuditNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to data type
    /// </summary>
    public virtual AuditDataType IdTypeNavigation { get; set; } = null!;
    #endregion

    /// <summary>
    /// Constructor from AuditEventData
    /// </summary>
    /// <param name="data">AuditData</param>
    public SampleAuditData(IAuditData data)
    {
        IdType = data.IdType;
        Json = data.Json;
    }
}
