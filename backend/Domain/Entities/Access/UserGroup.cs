namespace Domain.Entities;

public partial class UserGroup
{
    public long Id { get; set; }
    public long IdUser { get; set; }
    public long IdGroup { get; set; }

    public virtual Group IdGroupNavigation { get; set; } = null!;
    public virtual User IdUserNavigation { get; set; } = null!;
}

