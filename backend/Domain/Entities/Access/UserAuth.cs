namespace Domain.Entities;

public partial class UserAuth
{
    public long IdAuth { get; set; }
    public long IdUser { get; set; }
    public int IdAction { get; set; }
    public DateTime Stamp { get; set; }
    public string System { get; set; } = null!;
    public string? Message { get; set; }

    public virtual AuthAction IdActionNavigation { get; set; } = null!;
    public virtual User IdUserNavigation { get; set; } = null!;

    /// <summary>
    /// Parametrized constructor
    /// </summary>
    /// <param name="idUser">Id of user</param>
    /// <param name="idAction">Id of action</param>
    /// <param name="system">Name of system</param>
    public UserAuth(long idUser, int idAction, string system)
    {
        this.IdUser = idUser;
        this.Stamp = DateTime.Now;
        this.IdAction = idAction;
        this.System = system;
    }
}

