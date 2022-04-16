namespace Domain.Entities;

public partial class User
{
    public User()
    {
        Requests = new HashSet<Request>();
        UserAuths = new HashSet<UserAuth>();
        UserGroups = new HashSet<UserGroup>();
        UserRoles = new HashSet<UserRole>();
    }

    public long IdUser { get; set; }
    public string Login { get; set; } = null!;
    public string Pass { get; set; } = null!;
    public char IsActive { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateOnly? PassDate { get; set; }

    public virtual ICollection<Request> Requests { get; set; }
    public virtual ICollection<UserAuth> UserAuths { get; set; }
    public virtual ICollection<UserGroup> UserGroups { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
