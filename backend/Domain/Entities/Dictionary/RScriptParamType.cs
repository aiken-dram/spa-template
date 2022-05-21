namespace Domain.Entities;

/// <summary>
/// RScript parameter type
/// </summary>
public partial class RScriptParamType
{
    public RScriptParamType()
    {
        RScriptParams = new HashSet<Domain.Entities.RScriptParam>();
    }

    /// <summary>
    /// Id of type in database
    /// </summary>
    public eRScriptParamType IdType { get; set; }

    /// <summary>
    /// Name of type
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Description of type
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of R script params with this type
    /// </summary>
    public virtual ICollection<Domain.Entities.RScriptParam> RScriptParams { get; set; }
}
