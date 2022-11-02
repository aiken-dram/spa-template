namespace Domain.Entities;

#warning SAMPLE, remove entire file in actual application
/// <summary>
/// Sample fixed type dictionary
/// </summary>
public partial class SampleType
{
    public SampleType()
    {
        Samples = new HashSet<Sample>();
    }

    /// <summary>
    /// Id of type in database
    /// </summary>
    public eSampleType IdType { get; set; }

    /// <summary>
    /// Name of type
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Description of type
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Collection of samples with this type
    /// </summary>
    public virtual ICollection<Sample> Samples { get; set; }
}
