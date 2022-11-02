namespace Domain.Entities;

/// <summary>
/// District dictionary
/// </summary>
[AutoAudit]
[DisplayName("District")]
public partial class District : AuditableEntity
{
    #region ENTITY
    public District()
    {
        UserDistricts = new HashSet<UserDistrict>();
#warning SAMPLE
        Samples = new HashSet<Sample>();
    }

    /// <summary>
    /// District number
    /// </summary>
    /// <example>1</example>
    public int IdDistrict { get; set; }

    /// <summary>
    /// District name
    /// </summary>
    /// <example>District name</example>
    [Audit]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Collection of links between access users and this district
    /// </summary>
    public virtual ICollection<UserDistrict> UserDistricts { get; set; }

#warning SAMPLE
    /// <summary>
    /// Collection of links between sample and this district
    /// </summary>
    public virtual ICollection<Sample> Samples { get; set; }
    #endregion

    #region AUDIT
    public override int AuditIdTarget => (int)eAuditTarget.DictionaryDistrict;

    public override long? AuditTargetId => this.IdDistrict;

    public override string AuditTargetName => this.IdDistrict.ToString();
    #endregion
}

