namespace Domain.Entities;

#warning This is example, remove entire file in actual application
/// <summary>
/// Sample child table
/// </summary>
public class SampleChild
{
    /// <summary>
    /// Id of sample child in database
    /// </summary>
    public long IdChild { get; set; }

    /// <summary>
    /// Id of sample
    /// </summary>
    public long IdSample { get; set; }

    /// <summary>
    /// Text field
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Navigation to sample
    /// </summary>
    public virtual Sample IdSampleNavigation { get; set; } = null!;
}
