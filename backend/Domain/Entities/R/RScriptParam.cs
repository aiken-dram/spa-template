namespace Domain.Entities;

/// <summary>
/// Parameter of R script
/// </summary>
public partial class RScriptParam
{
    /// <summary>
    /// Id of parameter in database
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Id of R script
    /// </summary>
    public long IdRScript { get; set; }

    /// <summary>
    /// Id of parameter type
    /// </summary>
    public eRScriptParamType IdType { get; set; }

    /// <summary>
    /// Name of parameter
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Hint displayed for parameter in form
    /// </summary>
    public string? Hint { get; set; }

    /// <summary>
    /// Description of parameter
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Navigation to R script
    /// </summary>
    public virtual RScript IdRScriptNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to parameter type
    /// </summary>
    public virtual RScriptParamType IdTypeNavigation { get; set; } = null!;
}
