namespace Domain.Entities;

/// <summary>
/// Access module
/// </summary>
[DisplayName("Module")]
public partial class Module
{
    public Module()
    {
        RoleModules = new HashSet<RoleModule>();
    }

    /// <summary>
    /// Id of access module in database
    /// </summary>
    public long IdModule { get; set; }

    /// <summary>
    /// Name of module
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description of module
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of module roles
    /// </summary>
    public virtual ICollection<RoleModule> RoleModules { get; set; }
}

