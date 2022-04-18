using Domain.Common.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Authentication event action dictionary
/// </summary>
public partial class AuthAction : IDictionaryAction
{
    public AuthAction()
    {
        UserAuths = new HashSet<UserAuth>();
    }

    /// <summary>
    /// Id of action in database
    /// </summary>
    public int IdAction { get; set; }

    /// <summary>
    /// Name of action
    /// </summary>
    public string Action { get; set; } = null!;

    /// <summary>
    /// Description of action
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of authentication events with this action
    /// </summary>
    public virtual ICollection<UserAuth> UserAuths { get; set; }
}
