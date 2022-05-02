using Domain.Enums;
using Shared.Domain.Attributes;
using Shared.Domain.Models;

namespace Domain.Entities;

/// <summary>
/// District dictionary
/// </summary>
[AutoAudit]
public partial class District : AuditableEntity
{
    #region ENTITY
    public District()
    {
        UserDistricts = new HashSet<UserDistrict>();
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
    #endregion

    #region AUDIT
    public override int AuditIdTarget => (int)eEventTarget.DictionaryDistrict;

    public override long? AuditTargetId => this.IdDistrict;

    public override string AuditTargetName => this.IdDistrict.ToString();
    #endregion
}

