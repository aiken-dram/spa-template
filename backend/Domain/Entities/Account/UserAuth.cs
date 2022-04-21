namespace Domain.Entities;

/// <summary>
/// Authentication event of a user
/// </summary>
public partial class UserAuth
{
    /// <summary>
    /// Id of authentication event in database
    /// </summary>
    public long IdAuth { get; set; }

    /// <summary>
    /// Id of user
    /// </summary>
    public long IdUser { get; set; }

    /// <summary>
    /// Id of authentication action
    /// </summary>
    public int IdAction { get; set; }

    /// <summary>
    /// Date and time of authentication event
    /// </summary>
    public DateTime Stamp { get; set; }

    /// <summary>
    /// User system
    /// </summary>
    public string System { get; set; } = null!;

    /// <summary>
    /// Result of authentication event
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Navigation to user
    /// </summary>
    public virtual User IdUserNavigation { get; set; } = null!;

    /// <summary>
    /// Navigation to authentication action dictionary
    /// </summary>
    public virtual AuthAction IdActionNavigation { get; set; } = null!;

    /// <summary>
    /// Default constructor
    /// </summary>
    public UserAuth() { }

    /// <summary>
    /// Parametrized constructor
    /// </summary>
    /// <param name="idUser">Id of user</param>
    /// <param name="idAction">Id of action</param>
    /// <param name="system">Name of system</param>
    public UserAuth(long idUser, int idAction, string? system)
    {
        this.IdUser = idUser;
        this.Stamp = DateTime.Now;
        this.IdAction = idAction;
        this.System = system ?? string.Empty;
    }
}

