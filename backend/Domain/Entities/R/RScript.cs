namespace Domain.Entities;

/// <summary>
/// R script
/// </summary>
[AutoAudit]
public partial class RScript : AuditableEntity
{
    #region ENTITY
    /// <summary>
    /// Default constructor
    /// </summary>
    public RScript()
    {
        RScriptParams = new HashSet<RScriptParam>();
        RScriptTree = new HashSet<RScriptTreeNode>();
    }

    /// <summary>
    /// Id of R script in database
    /// </summary>
    public long IdRScript { get; set; }

    /// <summary>
    /// Name of file with R script
    /// </summary>
    [Audit]
    public string ScriptFile { get; set; } = null!;

    /// <summary>
    /// Name of R script
    /// </summary>
    [Audit]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Content type of result file
    /// </summary>
    [Audit]
    public string ContentType { get; set; } = null!;

    /// <summary>
    /// Result file name (timestamp will be added at the end)
    /// </summary>
    [Audit]
    public string ResultFile { get; set; } = null!;

    /// <summary>
    /// Description of R script
    /// </summary>
    [Audit]
    public string? Description { get; set; }

    /// <summary>
    /// Collection of R script parameters
    /// </summary>
    public virtual ICollection<RScriptParam> RScriptParams { get; set; }

    /// <summary>
    /// Collection of statistic menu tree nodes linked to this R script
    /// </summary>
    public virtual ICollection<RScriptTreeNode> RScriptTree { get; set; }
    #endregion

    #region AUDIT
    public override int AuditIdTarget => (int)eAuditTarget.RScript;

    public override long? AuditTargetId => IdRScript;

    public override string AuditTargetName => Name;
    #endregion
}
