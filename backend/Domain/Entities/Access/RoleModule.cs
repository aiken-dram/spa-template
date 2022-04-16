namespace Domain.Entities;

public partial class RoleModule
{
    public long Id { get; set; }
    public long IdRole { get; set; }
    public long IdModule { get; set; }

    public virtual Module IdModuleNavigation { get; set; } = null!;
    public virtual Role IdRoleNavigation { get; set; } = null!;
}

