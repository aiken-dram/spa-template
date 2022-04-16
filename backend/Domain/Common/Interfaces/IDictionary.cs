namespace Domain.Common.Interfaces;

/// <summary>
/// Dictionary type interface
/// </summary>
public interface IDictionaryType
{
    /// <summary>
    /// Identity of type
    /// </summary>
    int IdType { get; set; }

    /// <summary>
    /// Type name
    /// </summary>
    string Type { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    string? Description { get; set; }
}

/// <summary>
/// Dictionary state interface
/// </summary>
public interface IDictionaryState
{
    /// <summary>
    /// Identity of state
    /// </summary>
    int IdState { get; set; }

    /// <summary>
    /// State name
    /// </summary>
    string State { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    string? Description { get; set; }
}

/// <summary>
/// Dictionary action interface
/// </summary>
public interface IDictionaryAction
{
    /// <summary>
    /// Identity of state
    /// </summary>
    int IdAction { get; set; }

    /// <summary>
    /// State name
    /// </summary>
    string Action { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    string? Description { get; set; }
}
