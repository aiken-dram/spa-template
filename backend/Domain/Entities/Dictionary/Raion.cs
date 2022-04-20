namespace Domain.Entities;

/// <summary>
/// Raion dictionary
/// </summary>
public partial class Raion
{
    public Raion()
    {
        UserRaions = new HashSet<UserRaion>();
    }

    /// <summary>
    /// Raion number
    /// </summary>
    /// <example>1</example>
    public int IdRaion { get; set; }

    /// <summary>
    /// Raion name
    /// </summary>
    /// <example>Raion name</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Collection of links between access users and this raion
    /// </summary>
    public virtual ICollection<UserRaion> UserRaions { get; set; }
}

