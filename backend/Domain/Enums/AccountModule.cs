namespace Domain.Enums;

/// <summary>
/// Available access modules in application
/// </summary>
public static class eAccountModule
{
    /// <summary>
    /// Access admin
    /// </summary>
    public const string SecurityAdmin = "SECADM";

    /// <summary>
    /// Dictionary admin
    /// </summary>
    public const string DictionaryAdmin = "DICTADM";

    /// <summary>
    /// Configuration admin (includes R script sctructure)
    /// </summary>
    public const string ConfigurationAdmin = "CFGADM";

    /// <summary>
    /// Supervisor data access
    /// </summary>
    public const string Supervise = "SUPERVISE";

    /// <summary>
    /// Read only restriction
    /// </summary>
    public const string Readonly = "READONLY";
}
