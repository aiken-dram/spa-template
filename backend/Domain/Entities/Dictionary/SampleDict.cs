namespace Domain.Entities;

#warning This is example, remove entire file in actual application
/// <summary>
/// Sample editable dictionary
/// </summary>
[AutoAudit]
public partial class SampleDict : AuditableEntity
{
    public SampleDict()
    {
        Samples = new HashSet<Sample>();
    }

    /// <summary>
    /// Id of dictionary in database
    /// </summary>
    public long IdDict { get; set; }

    /// <summary>
    /// Name of dictionary
    /// </summary>
    public string Dict { get; set; } = null!;

    /// <summary>
    /// Description of dictionary
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of samples with this dictionary
    /// </summary>
    public virtual ICollection<Sample> Samples { get; set; }

    #region AUDIT
    public override int AuditIdTarget => (int)eAuditTarget.DictionarySample;

    public override long? AuditTargetId => this.IdDict;

    public override string AuditTargetName => this.Dict;
    #endregion
}
