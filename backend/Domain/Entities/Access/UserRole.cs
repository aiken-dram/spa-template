namespace Domain.Entities;

public partial class UserRole
{
    public long Id { get; set; }
    public long IdUser { get; set; }
    public long IdRole { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;
    public virtual User IdUserNavigation { get; set; } = null!;
}

