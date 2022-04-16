namespace Domain.Entities;

public partial class Role
{
    public Role()
    {
        GroupRoles = new HashSet<GroupRole>();
        RoleModules = new HashSet<RoleModule>();
        UserRoles = new HashSet<UserRole>();
    }

    public long IdRole { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<GroupRole> GroupRoles { get; set; }
    public virtual ICollection<RoleModule> RoleModules { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}

