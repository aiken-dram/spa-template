namespace Domain.Entities;

public partial class GroupRole
{
    public long Id { get; set; }
    public long IdGroup { get; set; }
    public long IdRole { get; set; }

    public virtual Group IdGroupNavigation { get; set; } = null!;
    public virtual Role IdRoleNavigation { get; set; } = null!;
}
