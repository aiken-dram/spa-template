using Domain.Common.Interfaces;

namespace Domain.Entities;

public partial class AuthAction : IDictionaryAction
{
    public AuthAction()
    {
        UserAuths = new HashSet<UserAuth>();
    }

    public int IdAction { get; set; }
    public string Action { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<UserAuth> UserAuths { get; set; }
}

