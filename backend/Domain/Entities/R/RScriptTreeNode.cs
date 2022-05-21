namespace Domain.Entities;

/// <summary>
/// Statistic menu tree node
/// </summary>
[AutoAudit]
public partial class RScriptTreeNode : AuditableEntity
{
    #region ENTITY
    /// <summary>
    /// Id of statistic menu tree node in database
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of parent node, or null if this is a top level node
    /// </summary>
    public long? IdParent { get; set; }

    /// <summary>
    /// Id of R script, if this node linked to R script
    /// </summary>
    public long? IdRScript { get; set; }

    /// <summary>
    /// Name of node
    /// </summary>
    [Audit]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Modules with access to the tree node, comma separated
    /// </summary>
    [Audit]
    public string? Modules { get; set; }

    /// <summary>
    /// Icon of node (FontAwesome)
    /// </summary>
    [Audit]
    public string? Icon { get; set; }

    /// <summary>
    /// Color of node
    /// </summary>
    [Audit]
    public string? Color { get; set; }

    /// <summary>
    /// Description of node
    /// </summary>
    [Audit]
    public string? Description { get; set; }

    /// <summary>
    /// Navigation to R script
    /// </summary>
    public virtual RScript IdRScriptNavigation { get; set; } = null!;
    #endregion

    #region AUDIT
    public override int AuditIdTarget => (int)eAuditTarget.RScriptTree;

    public override long? AuditTargetId => Id;

    public override string AuditTargetName => Name;
    #endregion
}
